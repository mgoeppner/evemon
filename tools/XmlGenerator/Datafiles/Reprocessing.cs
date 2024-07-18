using EVEMon.Common.Collections;
using EVEMon.Common.Serialization.Datafiles;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EVEMon.XmlGenerator.Datafiles
{
    internal static class Reprocessing
    {
        /// <summary>
        /// Generates the reprocessing datafile.
        /// </summary>
        internal static void GenerateDatafile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Util.ResetCounters();

            Console.WriteLine();
            Console.Write(@"Generating reprocessing datafile... ");

            List<SerializableItemMaterials> types = new List<SerializableItemMaterials>();

            foreach (int typeID in Database.InvTypesTable.Where(x => x.Generated).Select(x => x.ID))
            {
                Util.UpdatePercentDone(Database.ReprocessingTotalCount);

                List<SerializableMaterialQuantity> materials = Database.InvTypeMaterialsTable.Where(
                    x => x.ID == typeID).Select(
                        srcMaterial => new SerializableMaterialQuantity
                        {
                            ID = srcMaterial.MaterialTypeID,
                            Quantity = srcMaterial.Quantity
                        }).ToList();

                if (!materials.Any())
                    continue;

                SerializableItemMaterials itemMaterials = new SerializableItemMaterials { ID = typeID };
                itemMaterials.Materials.AddRange(materials.OrderBy(x => x.ID));
                types.Add(itemMaterials);
            }

            // Serialize
            ReprocessingDatafile datafile = new ReprocessingDatafile();
            datafile.Items.AddRange(types);

            Util.DisplayEndTime(stopwatch);

            Util.SerializeXml(datafile, DatafileConstants.ReprocessingDatafile);
        }
    }
}
