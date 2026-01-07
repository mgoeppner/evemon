using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EVEMon.ResFileCreator
{
    internal static class Program
    {
        private static readonly Dictionary<string, object> s_dictionary = new Dictionary<string, object>();
        private static string s_resScriptfile;
        private static string s_rcexe;
        private static string s_solutionDir;
        private static string s_projectDir;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <returns></returns>
        [STAThread]
        private static void Main()
        {
            s_rcexe = FindRcExe();
            if (string.IsNullOrEmpty(s_rcexe))
            {
                Console.WriteLine("RC.exe : Not Found - Resource file will not be created.");
                return;
            }

            ParserAssemblyInfo();

            if (!GenerateRcFile())
                return;

            CreateResFile();
            File.Delete(s_resScriptfile);
        }

        /// <summary>
        /// Parsers the assembly info.
        /// </summary>
        private static void ParserAssemblyInfo()
        {
            Directory.SetCurrentDirectory(GetSolutionDirectory());

            string assemblyInfoFileContent = File.ReadAllText(Path.GetFullPath(@"SharedAssemblyInfo.cs"));
            s_dictionary["AssemblyTitle"] = "EVEMon";

            assemblyInfoFileContent = File.ReadAllText(Path.GetFullPath(@"SharedAssemblyInfo.cs"));
            s_dictionary["AssemblyDescription"] = GetValueOf(assemblyInfoFileContent, "AssemblyDescription");
            s_dictionary["AssemblyCompany"] = GetValueOf(assemblyInfoFileContent, "AssemblyCompany");
            s_dictionary["AssemblyProduct"] = GetValueOf(assemblyInfoFileContent, "AssemblyProduct");
            s_dictionary["AssemblyCopyright"] = GetValueOf(assemblyInfoFileContent, "AssemblyCopyright");
            s_dictionary["AssemblyFileVersion"] = GetValueOf(assemblyInfoFileContent, "AssemblyFileVersion");
            s_dictionary["AssemblyInformationalVersion"] = GetValueOf(assemblyInfoFileContent, "AssemblyInformationalVersion");
        }

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="assemblyInfoFileContent">Content of the assembly information file.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetValueOf(string assemblyInfoFileContent, string key)
        {
            int index = assemblyInfoFileContent.IndexOf(key, StringComparison.Ordinal) + key.Length;
            string substring = assemblyInfoFileContent.Substring(index);
            int length = substring.IndexOf(")", StringComparison.Ordinal) - 1;
            string value = assemblyInfoFileContent.Substring(index, length)
                .Replace("(\"", string.Empty).Replace("\")", string.Empty);
            return value;
        }

        /// <summary>
        /// Generates the rc file.
        /// </summary>
        /// <returns></returns>
        private static bool GenerateRcFile()
        {
            s_resScriptfile = Path.GetFullPath(Path.Combine("src", "EVEMon", "EVEMon.rc"));

            StringBuilder sb = new StringBuilder();

            AddIcons(sb);
            AddManifest(sb);
            AddVersionInfo(sb);

            try
            {
                File.WriteAllText(s_resScriptfile, sb.ToString(), Encoding.Default);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            Console.WriteLine("Resource script file created successfully.");
            return true;
        }

        /// <summary>
        /// Adds the version info.
        /// </summary>
        /// <param name="sb">The sb.</param>
        private static void AddVersionInfo(StringBuilder sb)
        {
            string commaFileVersion = s_dictionary["AssemblyFileVersion"].ToString().Replace(".", ",");

            sb
                .AppendLine("// Version")
                .AppendLine("1 VERSIONINFO")
                .AppendLine($" FILEVERSION {commaFileVersion}")
                .AppendLine($" PRODUCTVERSION {commaFileVersion}")
                .AppendLine(" FILEFLAGSMASK 0x3fL")
                .AppendLine("#ifdef _DEBUG")
                .AppendLine(" FILEFLAGS 0x1L")
                .AppendLine("#else")
                .AppendLine(" FILEFLAGS 0x0L")
                .AppendLine("#endif")
                .AppendLine(" FILEOS 0x40004L")
                .AppendLine(" FILETYPE 0x0L")
                .AppendLine(" FILESUBTYPE 0x0L")

                .AppendLine("BEGIN")
                .AppendLine("    BLOCK \"StringFileInfo\"")
                .AppendLine("    BEGIN")
                .AppendLine("        BLOCK \"000004b0\"")
                .AppendLine("        BEGIN")
                .AppendLine($"            VALUE \"CompanyName\", \"{s_dictionary["AssemblyCompany"]}\"")
                .AppendLine($"            VALUE \"FileDescription\", \"{s_dictionary["AssemblyTitle"]}\"")
                .AppendLine($"            VALUE \"FileVersion\", \"{s_dictionary["AssemblyFileVersion"]}\"")
                .AppendLine($"            VALUE \"InternalName\", \"{s_dictionary["AssemblyProduct"]}.exe\"")
                .AppendLine($"            VALUE \"LegalCopyright\", \"{s_dictionary["AssemblyCopyright"]}\"")
                .AppendLine($"            VALUE \"OriginalFilename\", \"{s_dictionary["AssemblyProduct"]}.exe\"")
                .AppendLine($"            VALUE \"ProductName\", \"{s_dictionary["AssemblyProduct"]}\"")
                .AppendLine($"            VALUE \"ProductVersion\", \"{s_dictionary["AssemblyInformationalVersion"]}\"")
                .AppendLine("        END")
                .AppendLine("    END")
                .AppendLine("    BLOCK \"VarFileInfo\"")
                .AppendLine("    BEGIN")
                .AppendLine("        VALUE \"Translation\", 0x000, 1200")
                .AppendLine("    END")
                .AppendLine("END");
        }

        /// <summary>
        /// Adds the manifest.
        /// </summary>
        /// <param name="sb">The sb.</param>
        private static void AddManifest(StringBuilder sb)
        {
            var manifestFile = Path.GetFullPath(Path.Combine("src", "EVEMon", "app.manifest"));

            if (!File.Exists(manifestFile))
                return;

            sb
                .AppendLine("// Manifest")
                .AppendLine($"1 24 \"{string.Concat(OperatingSystem.IsLinux() ? "Z:" : "", manifestFile)}\"");
        }

        /// <summary>
        /// Adds the icons.
        /// </summary>
        /// <param name="sb">The sb.</param>
        private static void AddIcons(StringBuilder sb)
        {
            var iconsDir = Path.GetFullPath(Path.Combine("src", "EVEMon.Common", "Resources", "Icons"));
            List<string> iconFilesPath = new List<string>();

            if (Directory.Exists(iconsDir))
                iconFilesPath = Directory.GetFiles(iconsDir, "EVEMon.ico", SearchOption.AllDirectories).ToList();

            if (!iconFilesPath.Any())
                return;

            int count = 1;
            string iconEVEMon = iconFilesPath.FirstOrDefault(file => file.Contains("EVEMon.ico"));

            sb.AppendLine("// Icon");
            if (iconEVEMon != null)
            {
                sb.AppendLine($"{count} ICON \"{string.Concat(OperatingSystem.IsLinux() ? @"Z:" : "", iconEVEMon)}\"");
                count++;
                iconFilesPath.Remove(iconEVEMon);
            }

            foreach (string iconFilePath in iconFilesPath)
            {
                sb.AppendLine($"{count} ICON \"{string.Concat(OperatingSystem.IsLinux() ? @"Z:" : "", iconFilePath)}\"");
                count++;
            }

            sb.AppendLine();
        }

        /// <summary>
        /// Creates the resource file.
        /// </summary>
        private static void CreateResFile()
        {
            var file = OperatingSystem.IsLinux() ? "wine" : s_rcexe;
            var arguments = OperatingSystem.IsLinux()
                ? string.Format(CultureInfo.InvariantCulture, "{0} /v /nologo /r \"{1}\"", s_rcexe, string.Concat("Z:", s_resScriptfile))
                : string.Format(CultureInfo.InvariantCulture, "/v /nologo /r \"{0}\"", s_resScriptfile);
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = file,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = GetSolutionDirectory()
            };

            int exitCode;
            using (Process makeResProcess = new Process())
            {
                makeResProcess.StartInfo = startInfo;
                makeResProcess.Start();
                makeResProcess.WaitForExit();
                exitCode = makeResProcess.ExitCode;

                if (true)
                {
                    Console.WriteLine();
                    Console.WriteLine(makeResProcess.StandardOutput.ReadToEnd());
                    Console.WriteLine(makeResProcess.StandardError.ReadToEnd());
                }
            }

            Console.Write(exitCode != 0
                ? "Resource Compiler exited with errors."
                : "Resource file compiled successfully.");
        }

        /// <summary>
        /// Finds the rc executable.
        /// </summary>
        /// <returns></returns>
        private static string FindRcExe()
        {
            // Lookup for 'RC.exe' for the particular process architecture
            string architecture = Environment.Is64BitProcess ? "x64" : "x86";
            string filePath = Path.Combine(GetProjectDirectory(), "Dependencies", "ResCompiler", architecture, "rc.exe");

            return File.Exists(filePath) ? filePath : null;
        }

        /// <summary>
        /// Gets the solution directory.
        /// </summary>
        /// <returns></returns>
        private static string GetSolutionDirectory()
        {
            if (string.IsNullOrWhiteSpace(s_solutionDir))
            {
                s_solutionDir = Regex.Match(Directory.GetCurrentDirectory(), @"([a-zA-Z]+:){0,1}.*[\\/](?=tools)",
                                            RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString();
            }
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
                s_projectDir = Regex.Match(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"([a-zA-Z]+:){0,1}.*[\\/](?=bin)",
                                            RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString();
            }
            return s_projectDir;
        }
    }
}
