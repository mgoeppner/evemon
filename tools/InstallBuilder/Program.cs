using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace EVEMon.InstallBuilder
{
    internal static class Program
    {
        private static string s_sourceFilesDir;
        private static string s_outputPath;
        private static string s_installerDir;
        private static string s_snapshotDir;
        private static string s_binariesDir;
        private static string s_solutionDir;
        private static string s_projectDir;
        private static string s_nsisExe;
        private static bool s_isSnapshot;

        private static FileVersionInfo s_fileVersionInfo;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        [STAThread]
        private static int Main(string[] args)
        {
            CheckIsSnapshot();

            if (!HasVersion())
                return 1;

            if (args.Any())
            {
                if (args[0] == "-version" || args[0] == "-v")
                {
                    Console.WriteLine(s_fileVersionInfo.ProductVersion);
                    return 0;
                }

                if (args[0] == "-version=tc" || args[0] == "-v=tc")
                {
                    Console.WriteLine("##teamcity[buildNumber '{0}']", s_fileVersionInfo.ProductVersion);
                    return 0;
                }
            }

            try
            {
                if (CheckNsisPresent() && !s_isSnapshot)
                {
                    // Create the appropriate folder if it doesn't exist
                    if (!Directory.Exists(InstallerDirectory))
                        Directory.CreateDirectory(InstallerDirectory);

                    // Create an installer in the appropriate folder
                    Console.WriteLine("Starting Installer creation.");
                    if (BuildInstaller() != 0)
                        return 1;
                    Console.WriteLine("Installer creation finished.");
                    Console.WriteLine();
                }

                // Create the appropriate folder if it doesn't exist
                string directory = s_isSnapshot ? SnapshotDirectory : BinariesDirectory;
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Create a zip file in the appropriate folder
                string description = s_isSnapshot ? "Snapshot" : "Binaries";
                Console.WriteLine("Starting {0} Zip creation.", description);
                if (BuildZip() != 0)
                    return 1;
                Console.WriteLine("{0} Zip creation finished.", description);
                Console.WriteLine("Done");

                if (Debugger.IsAttached)
                    Console.ReadLine();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0} in {1}", ex.Message, ex.Source);
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                if (Debugger.IsAttached)
                    Console.ReadLine();
                return 1;
            }
        }

        /// <summary>
        /// Gets the solution directory.
        /// </summary>
        /// <returns></returns>
        private static string SolutionDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_solutionDir))
                {
                    s_solutionDir = Regex.Match(Directory.GetCurrentDirectory(), @"[a-zA-Z]+:.*\\(?=tools)",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString();
                }
                return s_solutionDir;
            }
        }

        /// <summary>
        /// Gets the project directory.
        /// </summary>
        /// <returns></returns>
        private static string ProjectDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_projectDir))
                {
                    s_projectDir = Regex.Match(Directory.GetCurrentDirectory(), @"[a-zA-Z]+:.*\\(?=bin)",
                        RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString();
                }
                return s_projectDir;
            }
        }

        /// <summary>
        /// Gets the output path.
        /// </summary>
        /// <returns></returns>
        private static string OutputPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_outputPath))
                {
                    s_outputPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Remove(0, ProjectDirectory.Length);
                }
                return s_outputPath;
            }
        }

        /// <summary>
        /// Gets the source files directory.
        /// </summary>
        /// <returns></returns>
        private static string SourceFilesDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_sourceFilesDir))
                {
                    s_sourceFilesDir = Path.GetFullPath(Path.Combine(SolutionDirectory, @"src\EVEMon\", OutputPath));
                }
                return s_sourceFilesDir;
            }
        }

        /// <summary>
        /// Gets the installer directory.
        /// </summary>
        /// <returns></returns>
        private static string InstallerDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_installerDir))
                {
                    s_installerDir = GetInstallbuilderDirectory("Installer");
                }
                return s_installerDir;
            }
        }

        /// <summary>
        /// Gets the snapshot directory.
        /// </summary>
        /// <returns></returns>
        private static string SnapshotDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_snapshotDir))
                {
                    s_snapshotDir = GetInstallbuilderDirectory("Snapshot");
                }
                return s_snapshotDir;
            }
        }

        /// <summary>
        /// Gets the binaries directory.
        /// </summary>
        /// <returns></returns>
        private static string BinariesDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(s_binariesDir))
                {
                    s_binariesDir = GetInstallbuilderDirectory("Binaries");
                }
                return s_binariesDir;
            }
        }

        /// <summary>
        /// Gets the installbuilder directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        private static string GetInstallbuilderDirectory(string directory)
            => Path.GetFullPath(
                Regex.Replace(SourceFilesDirectory, "Debug|Release",
                    Path.Combine("Installbuilder", directory),
                    RegexOptions.Compiled | RegexOptions.IgnoreCase));

        /// <summary>
        /// Checks the configuration is Snapshot.
        /// </summary>
        [Conditional("SNAPSHOT")]
        private static void CheckIsSnapshot()
        {
            s_isSnapshot = true;
        }

        /// <summary>
        /// Checks that NSIS is present.
        /// </summary>
        private static bool CheckNsisPresent()
        {
            s_nsisExe = FindMakeNsisExe();
            Console.WriteLine("NSIS : {0}", string.IsNullOrEmpty(s_nsisExe)
                ? "Not Found - Installer will not be created."
                : s_nsisExe);

            Console.WriteLine();

            Console.WriteLine("Project directory : {0}", ProjectDirectory);
            Console.WriteLine("Source directory : {0}", SourceFilesDirectory);
            if (s_isSnapshot)
                Console.WriteLine("Snapshot directory : {0}", SnapshotDirectory);
            else
            {
                Console.WriteLine("Installer directory : {0}", InstallerDirectory);
                Console.WriteLine("Binaries directory : {0}", BinariesDirectory);
            }
            Console.WriteLine();

            return !string.IsNullOrEmpty(s_nsisExe);
        }

        /// <summary>
        /// Finds the 'makensis' executable.
        /// </summary>
        /// <returns></returns>
        private static string FindMakeNsisExe()
        {
            string path = Path.Combine(ProjectDirectory, @"NSIS\makensis.exe");
            return File.Exists(path) ? path : string.Empty;
        }

        /// <summary>
        /// Gets true if a release version has been compiled.
        /// </summary>
        private static bool HasVersion()
        {
            try
            {
                s_fileVersionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(SourceFilesDirectory, "EVEMon.exe"));
            }
            catch (Exception)
            {
                Console.WriteLine("A \"Release\" has to be compiled first.");
                Console.WriteLine("Install Builder will now close.");
                Console.ReadLine();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Builds the zip.
        /// </summary>
        private static int BuildZip()
        {
            string directory = s_isSnapshot ? SnapshotDirectory : BinariesDirectory;

            // Delete any existing files in directory
            DeleteFiles(directory);

            string filename = s_isSnapshot
                ? string.Format(CultureInfo.InvariantCulture, "EVEMon_{0}_{1:yyyy-MM-dd}.zip",
                    s_fileVersionInfo.ProductPrivatePart, DateTime.Now)
                : string.Format(CultureInfo.InvariantCulture, "EVEMon-binaries-{0}.zip", s_fileVersionInfo.ProductVersion);

            string zipFileName = Path.Combine(directory, filename);

            string[] filenames = Directory.GetFiles(SourceFilesDirectory, "*", SearchOption.AllDirectories);

            Stream stream = null;
            try
            {
                stream = File.Create(zipFileName);

                using (ZipOutputStream zipStream = new ZipOutputStream(stream))
                {
                    stream = null;
                    zipStream.SetLevel(9);
                    zipStream.UseZip64 = UseZip64.Off;

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames.Where(file => !file.Contains("vshost") && !file.Contains(".config")))
                    {
                        string entryName = string.Format(CultureInfo.InvariantCulture, "EVEMon\\{0}",
                            file.Remove(0, SourceFilesDirectory.Length));
                        Console.WriteLine("Zipping {0}", entryName);
                        ZipEntry entry = new ZipEntry(entryName) { DateTime = DateTime.Now };

                        zipStream.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                zipStream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    zipStream.Finish();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                stream?.Dispose();
            }

            return 0;
        }

        /// <summary>
        /// Builds the installer.
        /// </summary>
        private static int BuildInstaller()
        {
            // Delete any existing files in directory
            DeleteFiles(InstallerDirectory);

            int exitCode;
            try
            {
                string nsisScript = Path.Combine(ProjectDirectory, OutputPath, "EVEMonInstallerScript.nsi");
                string resourcesDir = Path.Combine(SolutionDirectory, @"src\\EVEMon.Common\Resources");
                string productName = string.Format(CultureInfo.InvariantCulture, "/DPRODUCTNAME=\"{0}\"", "EVEMon");
                string companyName = string.Format(CultureInfo.InvariantCulture, "/DCOMPANYNAME=\"{0}\"", "EVEMon Development Team");
                string copyright = string.Format(CultureInfo.InvariantCulture, "/DCOPYRIGHT=\"{0}\"", "Copyright © 2006-2022, EVEMon Development Team");
                string description = string.Format(CultureInfo.InvariantCulture, "/DDESCRIPTION=\"{0}\"", "EVEMon");
                string version = string.Format(CultureInfo.InvariantCulture, "/DVERSION={0}", s_fileVersionInfo.ProductVersion);
                string fullVersion = string.Format(CultureInfo.InvariantCulture, "/DFULLVERSION={0}",
                    s_fileVersionInfo.FileVersion);
                string installerDir = string.Format(CultureInfo.InvariantCulture, "/DOUTDIR={0}", InstallerDirectory);
                string sourceDir = string.Format(CultureInfo.InvariantCulture, "/DSOURCEDIR={0}", SourceFilesDirectory);
                string resourceDir = string.Format(CultureInfo.InvariantCulture, "/DRESOURCESDIR={0}", resourcesDir);

                string param = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                    productName, companyName, copyright, description, version, fullVersion, installerDir, sourceDir, resourceDir,
                    nsisScript);

                Console.WriteLine("NSIS script : {0}", nsisScript);
                Console.WriteLine("Output directory : {0}", InstallerDirectory);

                ProcessStartInfo psi = new ProcessStartInfo(s_nsisExe, param)
                {
                    WorkingDirectory = ProjectDirectory,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using (Process makensisProcess = new Process())
                {
                    makensisProcess.StartInfo = psi;
                    makensisProcess.Start();
                    makensisProcess.ProcessorAffinity = (IntPtr)0x3;
                    Console.WriteLine(makensisProcess.StandardOutput.ReadToEnd());
                    makensisProcess.WaitForExit();
                    exitCode = makensisProcess.ExitCode;
                }

                if (exitCode != 0)
                    Console.WriteLine("MakeNSIS exited with errors.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }

            return exitCode;
        }

        /// <summary>
        /// Deletes the files in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        private static void DeleteFiles(string directoryPath)
        {
            Console.WriteLine("Deleting all files in {0}", directoryPath);

            foreach (string file in Directory.GetFiles(directoryPath))
            {
                try
                {
                    File.Delete(file);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
