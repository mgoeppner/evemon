using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using EVEMon.Common.Collections;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Serialization.Datafiles;
using EVEMon.XmlGenerator.Interfaces;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.StaticData;
using EVEMon.XmlGenerator.Utils;

namespace EVEMon.XmlGenerator.Datafiles
{
    internal static class Certificates
    {
        /// <summary>
        /// Generate the certificates datafile.
        /// </summary>        
        internal static void GenerateDatafile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Util.ResetCounters();

            Console.WriteLine();
            Console.Write(@"Generating certificates datafile... ");

            // Export certificates groups
            List<SerializableCertificateGroup> listOfCertGroups = new List<SerializableCertificateGroup>();

            foreach (InvGroups group in Database.CrtCertificatesTable.GroupBy(x => x.GroupID)
                .Select(x => Database.InvGroupsTable[x.Key]).OrderBy(x => x.Name))
            {
                SerializableCertificateGroup crtGroup = new SerializableCertificateGroup
                {
                    ID = group.ID,
                    Name = group.Name,
                    Description = group.Description
                };

                // Add classes to categories (each certificate gets its own class entry)
                var classes = ExportCertificateClasses(group).OrderBy(x => x.Name).ToList();
                if (classes.Any())
                    crtGroup.Classes.AddRange(classes);
                
                // Add category if it has certificates
                if (crtGroup.Classes.Count > 0)
                    listOfCertGroups.Add(crtGroup);
            }


            // Serialize
            CertificatesDatafile datafile = new CertificatesDatafile();
            datafile.Groups.AddRange(listOfCertGroups);

            Util.DisplayEndTime(stopwatch);

            Util.SerializeXml(datafile, DatafileConstants.CertificatesDatafile);
        }

        /// <summary>
        /// Exports the certificates classes.
        /// Each certificate gets its own class entry to ensure all certificate IDs are exported.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        private static IEnumerable<SerializableCertificateClass> ExportCertificateClasses(IHasID group)
        {
            List<SerializableCertificateClass> listOfCertClasses = new List<SerializableCertificateClass>();

            // Get all certificates for this group
            var certificates = Database.CrtCertificatesTable
                .Where(x => x.GroupID == group.ID)
                .OrderBy(x => x.ID);

            foreach (var cert in certificates)
            {
                Util.UpdatePercentDone(Database.CertificatesTotalCount);

                // Get class info
                CrtClasses certClass = Database.CrtClassesTable.FirstOrDefault(x => x.ID == cert.ClassID);

                string className = cert.Name;
                string classDescription = certClass?.Description ?? "";

                // Create a class entry for this certificate
                // Use the certificate ID as the class ID to ensure uniqueness
                SerializableCertificateClass crtClass = new SerializableCertificateClass
                {
                    ID = cert.ID,  // Use certificate ID for unique lookup
                    Name = className,
                    Description = classDescription
                };

                // Export the certificate
                SerializableCertificate certificate = ExportCertificate(cert);
                if (certificate == null)
                    continue;

                crtClass.Certificate = certificate;
                listOfCertClasses.Add(crtClass);
            }

            return listOfCertClasses;
        }

        /// <summary>
        /// Exports the certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        private static SerializableCertificate ExportCertificate(CrtCertificates certificate)
        {

            SerializableCertificate crtCertificate = new SerializableCertificate
            {
                ID = certificate.ID,
                Description = certificate.Description
            };

            // Export prerequesities
            IEnumerable<SerializableCertificatePrerequisite> listOfPrereq = Database.CrtRelationshipsTable
                .Where(x => x.ChildID == certificate.ID && x.ParentLevel != 0)
                .Select(relationship => new SerializableCertificatePrerequisite
                {
                    ID = Database.InvTypesTable[relationship.ParentTypeID].ID,
                    Skill = Database.InvTypesTable[relationship.ParentTypeID].Name,
                    Level = relationship.ParentLevel.ToString(CultureInfo.InvariantCulture),
                    Grade = (CertificateGrade)Enum.ToObject(typeof(CertificateGrade), relationship.Grade),
                });

            //Add prerequisites to certificate
            crtCertificate.Prerequisites.AddRange(listOfPrereq.OrderBy(x => x.Grade));

            // Add recommendations to certificate
            IEnumerable<SerializableCertificateRecommendation> listOfRecommendations = Database.CrtRecommendationsTable.Where(
                x => x.CertificateID == certificate.ID)
                .Select(recommendation => new SerializableCertificateRecommendation
                {
                    ID = recommendation.ShipTypeID,
                    ShipName = Database.InvTypesTable[recommendation.ShipTypeID].Name,
                });

            crtCertificate.Recommendations.AddRange(listOfRecommendations);

            // Add certificate
            return crtCertificate;
        }
    }
}
