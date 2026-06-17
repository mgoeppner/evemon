using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EVEMon.Watchdog
{
    /// <summary>
    /// Window that monitors the EVEMon process, restarting it when it has closed.
    /// </summary>
    public partial class WatchdogWindow : Form
    {
        private readonly string[] m_args;
        private bool m_executableLaunched;

        /// <summary>
        /// Creates the Watchdog Window.
        /// </summary>
        /// <param name="args"></param>
        public WatchdogWindow(string[] args)
        {
            InitializeComponent();
            m_args = args;
        }

        /// <summary>
        /// Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WatchdogWindow_Load(object sender, EventArgs e)
        {
            WaitTimer.Start();
            StatusLabel.Text = "Waiting for EVEMon to close.";
        }

        /// <summary>
        /// Returns true EVEMon process currently executing.
        /// </summary>
        private static bool IsEVEMonRunning
        {
            get
            {
                Process[] processes = Process.GetProcessesByName("EVEMon");
                return processes.Length != 0;
            }
        }

        /// <summary>
        /// Timer triggered every 1000msec (1 second)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaitTimer_Tick(object sender, EventArgs e)
        {
            // First time through after 'EVEMon' has closed
            if (!m_executableLaunched && !IsEVEMonRunning)
            {
                m_executableLaunched = true;
                StartEVEMonProcess();
                StatusLabel.Text = "Restarting EVEMon.";
                return;
            }

            // 'EVEMon' has been restarted and is running
            if (m_executableLaunched && IsEVEMonRunning)
                Application.Exit();
        }

        /// <summary>
        /// Starts the new EVEMon process.
        /// </summary>
        private void StartEVEMonProcess()
        {
            // Find the expected path for 'EVEMon.exe'
            string executable = Path.Combine(AppContext.BaseDirectory, "EVEMon.exe");

            // If 'EVEMon.exe' doesn't exist we don't have anything to do
            if (!File.Exists(executable))
            {
                Application.Exit();
                return;
            }

            StartProcess(executable, m_args);
        }

        /// <summary>
        /// Starts a process with arguments.
        /// </summary>
        /// <param name="executable">Executable to start (i.e. EVEMon.exe).</param>
        /// <param name="arguments">Arguments to pass to the executable.</param>
        private static void StartProcess(string executable, string[] arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
                                             {
                                                 FileName = executable,
                                                 Arguments = string.Join(" ", arguments),
                                                 UseShellExecute = false
                                             };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
            }
        }
    }
}
