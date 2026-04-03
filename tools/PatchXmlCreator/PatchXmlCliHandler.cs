using EVEMon.Common;
using EVEMon.Common.Helpers;
using EVEMon.Common.Serialization.PatchXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace EVEMon.PatchXmlCreator
{
    /// <summary>
    /// Handles CLI-mode patch.xml generation for datafiles-only updates.
    /// Bypasses the WinForms UI entirely, using the same serialization
    /// classes as the client to ensure format consistency.
    /// </summary>
    internal static class PatchXmlCliHandler
    {
        private const string DateTimeFormat = "dd MMMM yyyy";
        private const string CompatibilityMessage =
            "\nNOT COMPATIBLE with EVEMon prior to version 2.2.0";

        /// <summary>
        /// Entry point for CLI mode.
        /// Usage: PatchXmlCreator datafiles-only &lt;patch-xml-path&gt; &lt;resources-dir&gt; &lt;base-url&gt; &lt;build-label&gt;
        /// </summary>
        internal static int Run(string[] args)
        {
            if (args.Length < 5 || args[0] != "datafiles-only")
            {
                Console.WriteLine("Usage: PatchXmlCreator datafiles-only <patch-xml-path> <resources-dir> <base-url> <build-label>");
                Console.WriteLine();
                Console.WriteLine("  patch-xml-path  Path to the existing patch.xml to update");
                Console.WriteLine("  resources-dir   Directory containing eve-*-en-US.xml.gzip files");
                Console.WriteLine("  base-url        Base download URL (filename is appended by the client)");
                Console.WriteLine("  build-label     Build identifier for the datafile message");
                return 1;
            }

            string patchXmlPath = Path.GetFullPath(args[1]);
            string resourcesDir = Path.GetFullPath(args[2]);
            string baseUrl = args[3];
            string buildLabel = args[4];

            if (!File.Exists(patchXmlPath))
            {
                Console.Error.WriteLine($"patch.xml not found: {patchXmlPath}");
                return 1;
            }

            if (!Directory.Exists(resourcesDir))
            {
                Console.Error.WriteLine($"Resources directory not found: {resourcesDir}");
                return 1;
            }

            // Deserialize the existing patch.xml to preserve release sections
            SerializablePatch patch;
            try
            {
                patch = Util.DeserializeXmlFromFile<SerializablePatch>(patchXmlPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse {patchXmlPath}: {ex.Message}");
                return 1;
            }

            // Discover datafiles and rebuild the datafiles section
            patch.Datafiles.Clear();
            string date = DateTime.UtcNow.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
            string[] files = Directory.GetFiles(resourcesDir, "eve-*-en-US.xml.gzip");
            Array.Sort(files);

            if (files.Length == 0)
            {
                Console.Error.WriteLine($"No datafiles found in {resourcesDir}");
                return 1;
            }

            foreach (string filePath in files)
            {
                string name = Path.GetFileName(filePath);
                string md5 = Util.CreateMD5From(filePath);
                string type = name.Replace("eve-", "").Replace("-en-US.xml.gzip", "");
                string message = $"SDE {buildLabel} {type} data file by the EVEMon Development Team"
                    + CompatibilityMessage;

                patch.Datafiles.Add(new SerializableDatafile
                {
                    Name = name,
                    Date = date,
                    MD5Sum = md5,
                    Address = baseUrl,
                    Message = message
                });
            }

            // Serialize and write
            string xml = Util.GetXmlStringRepresentation(
                Util.SerializeToXmlDocument(patch));
            File.WriteAllText(patchXmlPath, xml, Encoding.UTF8);

            Console.WriteLine($"Updated {patchXmlPath} with {files.Length} datafiles (build {buildLabel})");
            return 0;
        }
    }
}
