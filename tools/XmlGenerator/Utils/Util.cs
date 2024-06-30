using EVEMon.Common.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.Utils
{
    public static class Util
    {
        private static string s_text;
        private static string s_solutionDir;
        private static string s_outputPath;
        private static string s_projectDir;
        private static int s_counter;
        private static int s_tablesCount;
        private static int s_percentOld;


        /// <summary>
        /// Serializes a XML file to EVEMon.Common\Resources.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datafile">The datafile.</param>
        /// <param name="filename">The filename.</param>
        internal static void SerializeXml<T>(T datafile, string filename)
        {
            string path = Path.Combine(GetSolutionDirectory(), @"src\EVEMon.Common\Resources", filename);

            FileStream stream = Common.Util.GetFileStream(path, FileMode.Create, FileAccess.Write);

            using (GZipStream zstream = new GZipStream(stream, CompressionMode.Compress))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(zstream, datafile);
                zstream.Flush();
            }

            Console.WriteLine(@"-----------------------------------------------");
            Console.WriteLine(@"Updated : {0}", filename);
            Console.WriteLine(@"-----------------------------------------------");

            // As long as EVEMon.Common is not rebuilt, files are not updated in output directories
            Copy(path, Path.Combine(GetSolutionDirectory(), @"src\EVEMon.Common\", GetOutputPath(), "Resources", filename));

            // Update the file in the settings directory
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Copy(path, Path.Combine(appData, "EVEMon", filename));

            Console.WriteLine();
        }

        /// <summary>
        /// Serializes a XML file to EVEMon.Common\Serialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serial">The serial.</param>
        /// <param name="xmlRootName">Name of the xml root.</param>
        /// <param name="filename">The filename.</param>
        internal static void SerializeXmlTo<T>(T serial, string xmlRootName, string filename)
        {
            string path = Path.Combine(GetSolutionDirectory(), @"src\EVEMon.Common\Serialization", filename);
            using (FileStream stream = Common.Util.GetFileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));
                serializer.Serialize(stream, serial);
                stream.Flush();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Creates one file alongside the resources file containing
        /// the MD5 sums for each resource.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        internal static void CreateMD5SumsFile(string filename)
        {
            ResetCounters();

            Console.WriteLine();

            string resourcesPath = Path.Combine(GetSolutionDirectory(), @"src\EVEMon.Common\Resources");
            string md5SumsFileFullPath = Path.Combine(resourcesPath, filename);

            using (StreamWriter md5SumsFile = File.CreateText(md5SumsFileFullPath))
            {
                foreach (string file in Datafile.GetFilesFrom(resourcesPath, Datafile.DatafilesExtension))
                {
                    FileInfo datafile = new FileInfo(file);
                    if (!datafile.Exists)
                        throw new FileNotFoundException($"{file} not found!");

                    string line = $"{Common.Util.CreateMD5From(file)} *{datafile.Name}";
                    md5SumsFile.WriteLine(line);
                }
            }

            Console.WriteLine(@"MD5Sums file created successfully");
            Console.WriteLine();
        }

        /// <summary>
        /// Copies a file from source to destination.
        /// </summary>
        /// <param name="srcFile">Fully qualified source filename</param>
        /// <param name="destFile">Fully quallified destination filename</param>
        private static void Copy(string srcFile, string destFile)
        {
            try
            {
                FileInfo fi = new FileInfo(destFile);
                if (fi.Directory == null)
                    return;

                if (fi.Directory.Exists && fi.Directory.Parent != null && fi.Directory.Parent.Parent != null)
                {
                    File.Copy(srcFile, destFile, true);
                    Console.WriteLine(@"*** {0}\{1}\{2}", fi.Directory.Parent.Parent.Name, fi.Directory.Parent.Name,
                        fi.Directory.Name);
                }
                else
                {
                    Trace.WriteLine($"{fi.Directory.FullName} doesn't exist, copy failed");
                }
            }
            catch (IOException exc)
            {
                Trace.WriteLine(exc);
            }
            catch (UnauthorizedAccessException exc)
            {
                Trace.WriteLine(exc);
            }
        }


        #region Helper Methods

        /// <summary>
        /// Gets the solution directory.
        /// </summary>
        /// <returns></returns>
        private static string GetSolutionDirectory()
        {
            if (string.IsNullOrWhiteSpace(s_solutionDir))
                s_solutionDir = Regex.Match(Directory.GetCurrentDirectory(), @"[a-zA-Z]+:.*\\(?=tools)",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString();
            return s_solutionDir;
        }

        /// <summary>
        /// Gets the project directory.
        /// </summary>
        /// <returns></returns>
        private static string GetProjectDirectory()
        {
            if (string.IsNullOrWhiteSpace(s_projectDir))
            {
                s_projectDir = Regex.Match(Directory.GetCurrentDirectory(), @"[a-zA-Z]+:.*\\(?=bin)",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString();
            }
            return s_projectDir;
        }

        /// <summary>
        /// Gets the output path.
        /// </summary>
        private static string GetOutputPath()
        {
            if (string.IsNullOrWhiteSpace(s_outputPath))
            {
                s_outputPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Remove(0, GetProjectDirectory().Length);
            }
            return s_outputPath;
        }

        /// <summary>
        /// Resets the counters.
        /// </summary>
        internal static void ResetCounters()
        {
            if (Debugger.IsAttached)
                Console.WriteLine(s_counter);

            s_counter = 0;
            s_percentOld = -1;
            s_text = string.Empty;
        }

        /// <summary>
        /// Updates the percantage done of the datafile generating procedure.
        /// </summary>
        /// <param name="totalCount">The total count.</param>
        internal static void UpdatePercentDone(double totalCount)
        {
            s_counter++;
            double percent = totalCount > 0d ? s_counter / totalCount : 0d;
            int percentRounded = (int)(percent * 100);

            if (s_counter != 1 && s_percentOld >= percentRounded)
                return;

            s_percentOld = percentRounded;

            if (!string.IsNullOrEmpty(s_text))
                Console.SetCursorPosition(Console.CursorLeft - s_text.Length, Console.CursorTop);

            s_text = $"{percent:P0}";
            Console.Write(s_text);
        }

        /// <summary>
        /// Updates the progress of data loaded from SQL server.
        /// </summary>
        /// <param name="totalCount">The total count.</param>
        internal static void UpdateProgress(int totalCount)
        {
            if (!string.IsNullOrEmpty(s_text))
                Console.SetCursorPosition(Console.CursorLeft - s_text.Length, Console.CursorTop);

            s_tablesCount++;
            s_text = $"{s_tablesCount / (double)totalCount:P0}";
            Console.Write(s_text);
        }

        /// <summary>
        /// Gets the count of types in a specified namespace.
        /// </summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <returns></returns>
        internal static int GetCountOfTypesInNamespace(string nameSpace)
            => typeof(Program).Assembly.GetTypes().Count(type => type.Namespace == nameSpace);

        /// <summary>
        /// Displays the end time.
        /// </summary>
        /// <param name="stopwatch">The stopwatch.</param>
        internal static void DisplayEndTime(Stopwatch stopwatch)
        {
            Console.WriteLine(@" in {0:g}", stopwatch.Elapsed);
        }

        #endregion
    }
}
