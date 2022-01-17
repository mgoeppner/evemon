using System;
using System.Windows.Forms;

namespace EVEMon.PatchXmlCreator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);

            Action action = Helper.SelectAction();

            // Ensure that the prerequisites for a release are met
            if (!Helper.ExitRequested && action != Action.DatafilesOnly)
                Helper.EnsurePrerequisites();

            // When prerequisites are not met we exit before Run()
            if (Helper.ExitRequested)
                return;

            Application.Run(new PatchXmlCreatorWindow(action));
        }
    }
}
