using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.CloudStorageServices;
using EVEMon.Common.Helpers;
using EVEMon.Common.Service;
using EVEMon.Common.Threading;
using EVEMon.ExceptionHandling;
using EVEMon.WindowsApi;

namespace EVEMon
{
    internal static class Program
    {
        /// <summary>
        /// The main window of the application.
        /// </summary>
        private static Form s_mainWindow;

        private static bool s_exitRequested;
        private static bool s_errorWindowIsShown;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            StartupAsync().Wait();
        }

        /// <summary>
        /// Starts up the application asynchronously.
        /// </summary>
        /// <returns></returns>
        private static async Task StartupAsync()
        {
            // Quits if another instance already exists
            if (!IsInstanceUnique)
                return;

            // Check if we are in DEBUG mode 
            EveMonClient.CheckIsDebug();

            // Check if we are in SNAPSHOT mode 
            EveMonClient.CheckIsSnapshot();

            // Subscribe application's events (especially the unhandled exceptions management for the crash box)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.ThreadException += Application_ThreadException;
            Application.ApplicationExit += ApplicationExitCallback;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Find our files
            EveMonClient.InitializeFileSystemPaths();

            // Creates a trace file
            EveMonClient.StartTraceLogging();
            EveMonClient.Trace("Starting up", false);

            // Make our windows nice
            MakeWindowsJuicy();

            // Ensures the installation file downloaded through the autoupdate is correctly deleted
            UpdateManager.DeleteInstallationFiles();

            // Upgrades the Cloud Storage Service Provider settings
            CloudStorageServiceProvider.UpgradeSettings();

            // Initialization
            EveMonClient.Initialize();
            Settings.Initialize();

            // Did something requested an exit before we entered Run() ?
            if (s_exitRequested)
                return;

            try
            {
                // Fires the main window
                EveMonClient.Trace("Main loop - start", printMethod: false);
                s_mainWindow = new MainWindow();
                Application.Run(s_mainWindow);
                EveMonClient.Trace("Main loop - done", printMethod: false);

                // Save before we quit
                await Task.WhenAll(Settings.SaveImmediateAsync(), EveIDToName.SaveImmediateAsync());
            }
            finally
            {
                // Stop the one-second timer right now
                EveMonClient.Shutdown();
                EveMonClient.Trace("Closed", printMethod: false);
                EveMonClient.StopTraceLogging();
            }
        }


        #region Properties

                /// <summary>
                /// Ensures that only one instance of EVEMon is run at once.
                /// </summary>
            private static
            bool IsInstanceUnique
        {
            get
            {
                InstanceManager im = InstanceManager.Instance;
                if (im.CreatedNew)
                    return true;

                im.Signal();
                return false;
            }
        }

        #endregion


        #region Helpers

        /// <summary>
        /// Makes the windows nice.
        /// </summary>
        private static void MakeWindowsJuicy()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                string appId = "EVEMon";

                if (EveMonClient.IsDebugBuild)
                    appId = $"{appId}-DEBUG";

                Windows7.SetProcessAppId(appId);
            }
            catch (InvalidOperationException ex)
            {
                // On some systems, a crash may occur here because of some skinning programs or others
                ExceptionHandler.LogException(ex, false);
            }
        }

        #endregion


        #region Callbacks

        /// <summary>
        /// If <see cref="Application.Exit()"/> is called before the <see cref="Application.Run()"/> method, then it won't occur. 
        /// So, here, we set up a boolean to prevent that.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ApplicationExitCallback(object sender, EventArgs e)
        {
            s_exitRequested = true;
        }

        /// <summary>
        /// Occurs when an exception reach the entry point of the
        /// application. We then display our custom crash box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleUnhandledException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// Occurs when the application tries to resolve an assembly dependency.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e) => HandleAssemblyResolve(e);

        /// <summary>
        /// Handles exceptions in WinForms threads, such exceptions
        /// would never reach the entry point of the application, 
        /// generally causing a CTD or trigger WER.
        /// We display our custom crash box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
        }

        /// <summary>
        /// Handles an exception through the Unhandled Exception window.
        /// </summary>
        /// <param name="ex">Exception to display</param>
        private static void HandleUnhandledException(Exception ex)
        {
            if (Debugger.IsAttached)
                return;

            if (s_errorWindowIsShown)
                return;

            s_errorWindowIsShown = true;

            try
            {
                // Some exceptions may be thrown on a worker thread so we need to invoke them to the UI thread,
                // if we are already on the UI thread nothing changes
                if (s_mainWindow!= null && s_mainWindow.InvokeRequired)
                {
                    Dispatcher.Invoke(() => HandleUnhandledException(ex));
                    return;
                }

                using (UnhandledExceptionWindow form = new UnhandledExceptionWindow(ex))
                {
                    if (s_mainWindow != null && !s_mainWindow.IsDisposed)
                        form.ShowDialog(s_mainWindow);
                    else
                        form.ShowDialog();
                }
            }
            catch (Exception e)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendLine(string.Format(Properties.Resources.ErrorUnhandled,
                    e.Message));
                if (ex.GetBaseException().Message != e.Message)
                    messageBuilder.AppendLine(string.Format(Properties.Resources.
                        ErrorUnhandledBase, ex.GetBaseException().Message));
                messageBuilder.AppendLine(Properties.Resources.ErrorUnhandledEnd);
                if (EveMonClient.IsDebugBuild)
                    messageBuilder.AppendLine().AppendLine(UnhandledExceptionWindow.
                        GetRecursiveStackTrace(ex));
                MessageBox.Show(messageBuilder.ToString(), @"EVEMon Error", MessageBoxButtons.
                    OK, MessageBoxIcon.Error);
            }

            Environment.Exit(1);
        }

        /// <summary>
        /// Handles the assembly resolve.
        /// </summary>
        /// <param name="e">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns>The resolved assembly</returns>
        /// <remarks>
        /// Because we are not distributing the config file along with the application,
        /// we need to resolve the assembly dependencies internally.
        /// First we determine which assembly was requested and then we load the distributed one and return it.
        /// </remarks>
        private static Assembly HandleAssemblyResolve(ResolveEventArgs e)
        {
            try
            {
                AssemblyName requestedAssembly = new AssemblyName(e.Name);
                AssemblyName assembly = AssemblyName.GetAssemblyName($"{requestedAssembly.Name}.dll");
                if (requestedAssembly.Version < assembly.Version)
                    return Assembly.Load(assembly);
            }
            catch (Exception)
            {
                return null;
            }
            return Assembly.Load(e.Name);
        }

        #endregion
    }
}
