using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEMon.Common.Attributes;
using EVEMon.Common.Collections.Global;
using EVEMon.Common.Constants;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Extended;
using EVEMon.Common.Net;
using EVEMon.Common.Threading;

namespace EVEMon.Common
{
    /// <summary>
    /// Provides a controller layer for the application. This class manages API querying, objects lifecycle, etc. 
    /// See it as the entry point of the library and its collections as databases with stored procedures (the public ones).
    /// </summary>
    [EnforceUIThreadAffinity]
    public static partial class EveMonClient
    {
        #region Fields

        private static StreamWriter s_traceStream;
        private static TextWriterTraceListener s_traceListener;
        private static readonly DateTime s_startTime = DateTime.UtcNow;

        private static IEnumerable<string> s_defaultEvePortraitCacheFolders;
        private static bool s_initialized;
        private static string s_traceFile;

        #endregion


        #region Initialization and threading

        /// <summary>
        /// Initializes paths, static objects, check and load datafiles, etc.
        /// </summary>
        /// <remarks>May be called more than once without causing redundant operations to occur.</remarks>
        public static void Initialize()
        {
            if (s_initialized)
                return;

            s_initialized = true;

            Trace("begin");

            // Network monitoring (connection availability changes)
            NetworkMonitor.Initialize();

            // ESIMethods collection initialization (always before members instatiation)
            ESIMethods.Initialize();

            // Members instantiations
            APIProviders = new GlobalAPIProviderCollection();
            MonitoredCharacters = new GlobalMonitoredCharacterCollection();
            CharacterIdentities = new GlobalCharacterIdentityCollection();
            Notifications = new GlobalNotificationCollection();
            Characters = new GlobalCharacterCollection();
            Datafiles = new GlobalDatafileCollection();
            ESIKeys = new GlobalAPIKeyCollection();
            EVEServer = new EveServer();

            Trace("done");
        }

        /// <summary>
        /// Starts the event processing on a multi-threaded model, with the UI actor being the main actor.
        /// </summary>
        /// <param name="thread">The thread.</param>
        public static void Run(TaskScheduler taskScheduler)
        {
            Dispatcher.Run(taskScheduler);
            Trace();
        }

        /// <summary>
        /// Shutdowns the timer.
        /// </summary>
        public static void Shutdown()
        {
            Closed = true;
            Dispatcher.Shutdown();
            Trace();
        }

        /// <summary>
        /// Resets collection that need to be cleared.
        /// </summary>
        internal static void ResetCollections()
        {
            ESIKeys = new GlobalAPIKeyCollection();
            Characters = new GlobalCharacterCollection();
            Notifications = new GlobalNotificationCollection();
            CharacterIdentities = new GlobalCharacterIdentityCollection();
            MonitoredCharacters = new GlobalMonitoredCharacterCollection();
        }

        /// <summary>
        /// Gets true whether the client has been shut down.
        /// </summary>
        public static bool Closed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is debug build.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug build; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDebugBuild { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is snapshot build.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is snapshot build; otherwise, <c>false</c>.
        /// </value>
        public static bool IsSnapshotBuild { get; private set; }

        #endregion


        #region Version

        /// <summary>
        /// Gets the file version information.
        /// </summary>
        /// <value>
        /// The file version.
        /// </value>
        public static FileVersionInfo FileVersionInfo
            => FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

        #endregion


        #region File paths

        /// <summary>
        /// Gets the EVE Online installations default portrait cache folder.
        /// </summary>
        public static IEnumerable<string> DefaultEvePortraitCacheFolders
        {
            get
            {
                if (s_defaultEvePortraitCacheFolders != null && s_defaultEvePortraitCacheFolders.Any())
                    return s_defaultEvePortraitCacheFolders;

                s_defaultEvePortraitCacheFolders = Settings.PortableEveInstallations.EVEClients
                    .Select(eveClientInstallation => $"{eveClientInstallation.Path}\\cache\\Pictures\\Characters")
                    .Where(Directory.Exists).ToList();

                if (s_defaultEvePortraitCacheFolders.Any())
                    EvePortraitCacheFolders = s_defaultEvePortraitCacheFolders;

                return s_defaultEvePortraitCacheFolders;
            }
        }

        /// <summary>
        /// Gets or sets the portrait cache folder defined by the user.
        /// </summary>
        public static IEnumerable<string> EvePortraitCacheFolders { get; internal set; }

        /// <summary>
        /// Gets or sets the EVE Online application data folder.
        /// </summary>
        public static string EVEApplicationDataDir { get; private set; }

        /// <summary>
        /// Returns the state of the EVE database.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if EVE database is out of service; otherwise, <c>false</c>.
        /// </value>
        public static bool EVEDatabaseDisabled { get; internal set; }

        /// <summary>
        /// Returns the current data storage directory.
        /// </summary>
        public static string EVEMonDataDir { get; private set; }

        /// <summary>
        /// Returns the current cache directory.
        /// </summary>
        public static string EVEMonCacheDir { get; private set; }

        /// <summary>
        /// Returns the current xml cache directory.
        /// </summary>
        public static string EVEMonXmlCacheDir { get; private set; }

        /// <summary>
        /// Returns the current image cache directory (not portraits).
        /// </summary>
        public static string EVEMonImageCacheDir { get; private set; }

        /// <summary>
        /// Returns the current portraits cache directory.
        /// </summary>
        /// <remarks>
        /// We're talking about the cache in %APPDATA%\cache\portraits
        /// This is different from the ImageService's hit cache (%APPDATA%\cache\image)
        /// or the game's portrait cache (in EVE Online folder)
        ///</remarks>
        public static string EVEMonPortraitCacheDir { get; private set; }

        /// <summary>
        /// Gets the name of the current settings file.
        /// </summary>
        public static string SettingsFileName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether cache folder in EVE default location exist.
        /// </summary>
        public static bool EveAppDataFoldersExistInDefaultLocation { get; private set; }

        /// <summary>
        /// Gets the fully qualified path to the current settings file.
        /// </summary>
        public static string SettingsFileNameFullPath => Path.Combine(EVEMonDataDir, SettingsFileName);

        /// <summary>
        /// Gets the fully qualified path to the trace file.
        /// </summary>
        public static string TraceFileNameFullPath => Path.Combine(EVEMonDataDir, s_traceFile);

        /// <summary>
        /// Creates the file system paths (settings file name, appdata directory, etc).
        /// </summary>
        public static void InitializeFileSystemPaths()
        {
            // Ensure it is made once only
            if (!string.IsNullOrEmpty(SettingsFileName))
                return;

            string debugAddition = IsDebugBuild ? "-debug" : string.Empty;
            SettingsFileName = $"settings{debugAddition}.xml";
            s_traceFile = $"trace{debugAddition}.txt";

            while (true)
            {
                try
                {
                    InitializeEVEMonPaths();
                    InitializeDefaultEvePortraitCachePath();
                    return;
                }
                catch (UnauthorizedAccessException exc)
                {
                    string msg = "An error occurred while EVEMon was looking for its data directory. " +
                                 "You may have insufficient rights or a synchronization may be taking place.\n\n" +
                                 $"The message was :{Environment.NewLine}{exc.Message}";

                    DialogResult result = MessageBox.Show(msg, @"EVEMon Error", MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);

                    if (result != DialogResult.Cancel)
                        continue;

                    Application.Exit();
                    return;
                }
            }
        }

        /// <summary>
        /// Initializes all needed EVEMon paths.
        /// </summary>
        private static void InitializeEVEMonPaths()
        {
            // Assign or create the EVEMon data directory
            if (!Directory.Exists(EVEMonDataDir))
            {
                string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EVEMon");

                // If settings.xml exists in the app's directory, we use this one
                EVEMonDataDir = Path.GetDirectoryName(Application.ExecutablePath) ?? appDataPath;

                // Else, we use %APPDATA%\EVEMon
                if (!File.Exists(SettingsFileNameFullPath))
                    EVEMonDataDir = appDataPath;

                // Create the directory if it does not exist already
                if (!Directory.Exists(EVEMonDataDir))
                    Directory.CreateDirectory(EVEMonDataDir);
            }

            // Create the cache subfolder
            EVEMonCacheDir = Path.Combine(EVEMonDataDir, "cache");
            if (!Directory.Exists(EVEMonCacheDir))
                Directory.CreateDirectory(EVEMonCacheDir);

            // Create the xml cache subfolder
            EVEMonXmlCacheDir = Path.Combine(EVEMonCacheDir, "xml");
            if (!Directory.Exists(EVEMonXmlCacheDir))
                Directory.CreateDirectory(EVEMonXmlCacheDir);

            // Create the images cache subfolder (not portraits)
            EVEMonImageCacheDir = Path.Combine(EVEMonCacheDir, "images");
            if (!Directory.Exists(EVEMonImageCacheDir))
                Directory.CreateDirectory(EVEMonImageCacheDir);

            // Create the portraits cache subfolder
            EVEMonPortraitCacheDir = Path.Combine(EVEMonCacheDir, "portraits");
            if (!Directory.Exists(EVEMonPortraitCacheDir))
                Directory.CreateDirectory(EVEMonPortraitCacheDir);
        }

        /// <summary>
        /// Retrieves the portrait cache folder, from the game installation.
        /// </summary>
        private static void InitializeDefaultEvePortraitCachePath()
        {
            string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            EVEApplicationDataDir = $"{localApplicationData}\\CCP\\EVE";

            // Check folder exists
            if (!Directory.Exists(EVEApplicationDataDir))
                return;

            // Create a pattern that matches anything "*_tranquility"
            // Enumerate files in the EVE cache directory
            DirectoryInfo di = new DirectoryInfo(EVEApplicationDataDir);
            DirectoryInfo[] tranquilityFolders = di.GetDirectories("*_tranquility");

            EveAppDataFoldersExistInDefaultLocation = tranquilityFolders.Any();

            if (!tranquilityFolders.Any())
                return;

            s_defaultEvePortraitCacheFolders = tranquilityFolders
                .Select(traquilityFolder => $"{EVEApplicationDataDir}\\{traquilityFolder.Name}\\cache\\Pictures\\Characters")
                .Where(Directory.Exists);

            EvePortraitCacheFolders = s_defaultEvePortraitCacheFolders;
        }

        /// <summary>
        /// Ensures the cache directories are initialized.
        /// </summary>
        internal static void EnsureCacheDirInit()
        {
            InitializeEVEMonPaths();
        }

        #endregion


        #region Services

        /// <summary>
        /// Gets the current synchronization context.
        /// </summary>
        /// <value>
        /// The current synchronization context.
        /// </value>
        public static TaskScheduler CurrentSynchronizationContext => TaskScheduler.FromCurrentSynchronizationContext();

        /// <summary>
        /// Gets an enumeration over the datafiles checksums.
        /// </summary>
        public static GlobalDatafileCollection Datafiles { get; private set; }

        /// <summary>
        /// Gets the API providers collection.
        /// </summary>
        public static GlobalAPIProviderCollection APIProviders { get; private set; }

        /// <summary>
        /// Gets the EVE server's informations.
        /// </summary>
        public static EveServer EVEServer { get; private set; }

        /// <summary>
        /// Apply some settings changes.
        /// </summary>
        private static void UpdateSettings()
        {
            HttpWebClientServiceState.Proxy = Settings.Proxy;
        }

        #endregion


        #region Cache Clearing

        public static void ClearCache()
        {
            try
            {
                List<FileInfo> cachedFiles = new List<FileInfo>();
                cachedFiles.AddRange(new DirectoryInfo(EVEMonImageCacheDir).GetFiles());
                cachedFiles.AddRange(new DirectoryInfo(EVEMonXmlCacheDir).GetFiles());
                cachedFiles.AddRange(new DirectoryInfo(EVEMonPortraitCacheDir).GetFiles());

                cachedFiles.ForEach(x => x.Delete());
            }
            finally
            {
                InitializeEVEMonPaths();
            }
        }

        #endregion


        #region API Keys management

        /// <summary>
        /// Gets the collection of all known API keys.
        /// </summary>
        public static GlobalAPIKeyCollection ESIKeys { get; private set; }

        /// <summary>
        /// Gets the collection of all characters.
        /// </summary>
        public static GlobalCharacterCollection Characters { get; private set; }

        /// <summary>
        /// Gets the collection of all known character identities. For monitored character, see <see cref="MonitoredCharacters"/>.
        /// </summary>
        public static GlobalCharacterIdentityCollection CharacterIdentities { get; private set; }

        /// <summary>
        /// Gets the collection of all monitored characters.
        /// </summary>
        public static GlobalMonitoredCharacterCollection MonitoredCharacters { get; private set; }

        /// <summary>
        /// Gets the collection of notifications.
        /// </summary>
        public static GlobalNotificationCollection Notifications { get; private set; }

        #endregion


        #region Diagnostics

        /// <summary>
        /// Sends a message to the trace with the prepended time since
        /// startup, in addition to argument inserting into the format.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Trace(string format, params object[] args)
        {
            string message = string.Format(CultureConstants.DefaultCulture, format, args);
            Trace(message);
        }

        /// <summary>
        /// Sends a message to the trace with the prepended time since startup.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="printMethod">if set to <c>true</c> [print method].</param>
        public static void Trace(string message = null, bool printMethod = true)
        {
            string header = string.Empty;

            if (printMethod)
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame frame = stackTrace.GetFrame(1);
                MethodBase method = frame.GetMethod();
                if (method.Name == "MoveNext")
                    method = stackTrace.GetFrame(3).GetMethod();

                Type declaringType = method.DeclaringType;
                header = $"{declaringType?.Name}.{method.Name}";
            }

            TimeSpan time = DateTime.UtcNow.Subtract(s_startTime);
            string timeStr = $"{time.Days:#0}d {time.Hours:#0}h {time.Minutes:00}m {time.Seconds:00}s > ";
            message = string.IsNullOrWhiteSpace(message) || !printMethod ? message : $" - {message}";
            string msgStr = $"{header}{message}";

            System.Diagnostics.Trace.WriteLine($"{timeStr}{msgStr.TrimEnd(Environment.NewLine.ToCharArray())}");
        }

        /// <summary>
        /// Sends a message to the trace with the calling method, time
        /// and the types of any arguments passed to the method.
        /// </summary>
        public static void TraceMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(1);
            MethodBase method = frame.GetMethod();
            string parameters = FormatParameters(method.GetParameters());
            string declaringType = method.DeclaringType?.ToString().Replace("EVEMon.", string.Empty);

            Trace($"{declaringType}.{method.Name}({parameters})");
        }

        /// <summary>
        /// Formats the parameters of a method into a string.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A comma seperated string of paramater types and names.</returns>
        private static string FormatParameters(IEnumerable<ParameterInfo> parameters)
        {
            StringBuilder paramDetail = new StringBuilder();

            foreach (ParameterInfo param in parameters)
            {
                if (paramDetail.Length != 0)
                    paramDetail.Append(", ");

                paramDetail.Append($"{param.GetType().Name} {param.Name}");
            }

            return paramDetail.ToString();
        }

        /// <summary>
        /// Starts the logging of trace messages to a file.
        /// </summary>
        public static void StartTraceLogging()
        {
            try
            {
                System.Diagnostics.Trace.AutoFlush = true;
                s_traceStream = File.CreateText(TraceFileNameFullPath);
                s_traceListener = new TextWriterTraceListener(s_traceStream);
                System.Diagnostics.Trace.Listeners.Add(s_traceListener);
            }
            catch (IOException e)
            {
                string text = "EVEMon has encountered an error and needs to terminate.\n" +
                              $"The error message is:\n\n\"{e.Message}\"";

                MessageBox.Show(text, @"EVEMon Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Stops the logging of trace messages to a file.
        /// </summary>
        public static void StopTraceLogging()
        {
            System.Diagnostics.Trace.Listeners.Remove(s_traceListener);
            s_traceListener.Close();
            s_traceStream.Close();
        }

        /// <summary>
        /// Will only execute if DEBUG is set, thus lets us avoid #IFDEF.
        /// </summary>
        [Conditional("DEBUG")]
        public static void CheckIsDebug()
        {
            IsDebugBuild = true;
        }

        /// <summary>
        /// Will only execute if SHAPSHOT is set, thus lets us avoid #IFDEF.
        /// </summary>
        [Conditional("SNAPSHOT")]
        public static void CheckIsSnapshot()
        {
            IsSnapshotBuild = true;
        }

        #endregion
    }
}
