using EVEMon.XmlGenerator.Datafiles;
using EVEMon.XmlGenerator.Models;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.Utils;
using EVEMon.XmlGenerator.Xmlfiles;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace EVEMon.XmlGenerator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <returns></returns>
        [STAThread]
        private static void Main()
        {
            SQLitePCL.Batteries_V2.Init();

            Stopwatch stopwatch = Stopwatch.StartNew();

            // Setting a standard format for the generated files
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // Create tables from database
            Database.CreateTables();

            Console.WriteLine();

            // Generate datafiles
            Properties.GenerateDatafile();
            Skills.GenerateDatafile();

            //Masteries.GenerateDatafile();

            Geography.GenerateDatafile();
            Blueprints.GenerateDatafile();
            Items.GenerateDatafile(); // Requires GenerateProperties()
            Reprocessing.GenerateDatafile(); // Requires GenerateItems()

            // Generate MD5 Sums file
            Util.CreateMD5SumsFile("MD5Sums.txt");

            // Generate support xml files
            Flags.GenerateXmlfile();

            Console.WriteLine(@"Generating files completed in {0:g}", stopwatch.Elapsed);
            Console.WriteLine();
            Console.Write(@"Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}
