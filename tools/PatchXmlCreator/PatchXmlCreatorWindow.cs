using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using EVEMon.Common;
using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.Data;
using EVEMon.Common.Helpers;
using EVEMon.Common.Serialization.PatchXml;
using System.Net;

namespace EVEMon.PatchXmlCreator
{
    internal partial class PatchXmlCreatorWindow : EVEMonForm
    {
        #region Fields

        private const string CompatibilityMessage = "\nNOT COMPATIBLE with EVEMon prior to version 2.2.0";
        private const string InstallerFilename = "EVEMon-install-{0}.exe";
        private const string DateTimeFormat = "dd MMMM yyyy";
        private const string DatafilesMessageFormat = "{0} {1} ({2}) {3} data file by the EVEMon Development Team";
        private const string DatafileHeader = "eve-";
        private const string InstallerArgs = "/S /AUTORUN /SKIPDOTNET";
        private const string AdditionalArgs = "/D=%EVEMON_EXECUTABLE_PATH%";

        private static readonly Dictionary<Control, string> s_listOfInitMessages = new Dictionary<Control, string>();
        private static readonly List<Datafile> s_datafiles = new List<Datafile>();
        private static readonly CultureInfo s_enUsCulture = new CultureInfo("en-US");

        private static FileVersionInfo s_fileVersionInfo;

        private readonly Action m_action;

        private Control m_activeTextBox;
        private string m_text;
        private bool m_init;

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private PatchXmlCreatorWindow()
        {
            InitializeComponent();
            gbRelease.Font = new Font(gbRelease.Font, FontStyle.Bold);
            gbDatafiles.Font = new Font(gbDatafiles.Font, FontStyle.Bold);

            rtbReleaseUrl.Text = NetworkConstants.GitHubDownloadsBase;
            rtbDatafileUrl.Text = NetworkConstants.BitBucketDatafilesBase;
            rtbTopicUrl.Text = NetworkConstants.ForumThreadBase;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PatchXmlCreatorWindow(Action action)
            : this()
        {
            m_action = action;
        }

        #endregion


        #region Events

        /// <summary>
        /// On load we create the window and update the controls.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitDatafiles();
            CustomLayout();
            StoreInitMessage();
            UpdateReleaseInfo();
            UpdateDatafilesControls();
            UpdateCreateButtonEnabled();

            MinimumSize = new Size(Width, Height);

            m_init = true;
        }

        /// <summary>
        /// Occurs when the user clicks on the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            ((Control)sender).Focus();
            btnCreate.Focus();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Get EVEMon's assembly version.
        /// </summary>
        /// <returns></returns>
        private static FileVersionInfo GetAssemblyVersion()
        {
            if (s_fileVersionInfo != null)
                return s_fileVersionInfo;

            string path = Path.Combine(Helper.GetSourceFilesDirectory, Helper.EVEMonExecFilename);
            return s_fileVersionInfo = FileVersionInfo.GetVersionInfo(path);
        }

        /// <summary>
        /// Updates the info in the release section.
        /// </summary>
        private void UpdateReleaseInfo()
        {
            if (m_action == Action.DatafilesOnly)
            {
                LoadReleaseInfoFromFile();
                return;
            }

            FileInfo installerFileInfo = GetInstallerPath();

            // Assign info
            var version = GetAssemblyVersion();
            lblEVEMonVersion.Text = version.FileVersion;
            dtpRelease.Value = installerFileInfo.LastWriteTime;
            lblMD5Sum.Text = Util.CreateMD5From(installerFileInfo.FullName);
            rtbReleaseUrl.Text = NetworkConstants.GitHubDownloadsBase + string.Format(
                "/download/{0:D}.{1:D}.{2:D}/", version.FileMajorPart, version.FileMinorPart,
                version.FileBuildPart);
        }

        /// <summary>
        /// Gets the installer path.
        /// </summary>
        /// <returns></returns>
        internal static FileInfo GetInstallerPath()
        {
            string installerFile = string.Format(CultureConstants.InvariantCulture, InstallerFilename, GetAssemblyVersion().ProductVersion);
            string installerPath = Path.Combine(Helper.GetSourceFilesDirectory
                .Replace(Helper.GetOutputPath, "bin\\Installbuilder\\Installer\\"), installerFile);
            return new FileInfo(installerPath);
        }

        /// <summary>
        /// Creates a list of the datafiles found in the release folder.
        /// </summary>
        private static void InitDatafiles()
        {
            DirectoryInfo di = new DirectoryInfo(Helper.GetDataFilesDirectory);
            var filename = $"{DatafileHeader}*-{s_enUsCulture.Name}{Datafile.DatafilesExtension}";
            FileInfo[] directoryFiles = di.GetFiles(filename);
            foreach (FileInfo datafile in directoryFiles)
            {
                s_datafiles.Add(new Datafile(datafile.Name));
            }
        }

        /// <summary>
        /// Adds a control for each data file.
        /// </summary>
        private void CustomLayout()
        {
            int startLocation = 70;
            const int Pad = 5;

            gbDatafiles.Controls.Remove(datafileControl);
            Height -= datafileControl.Height;

            SuspendLayout();
            try
            {
                foreach (Datafile datafile in s_datafiles.OrderBy(x => x.Filename))
                {
                    // Add a new datafile control
                    DatafileControl newDatafileControl = new DatafileControl();
                    gbDatafiles.Controls.Add(newDatafileControl);

                    // Control info
                    UpdateDatafileInfo(newDatafileControl, datafile);

                    // Set Properties
                    newDatafileControl.Location = new Point(9, startLocation);
                    newDatafileControl.Font = new Font(Font, FontStyle.Regular);
                    newDatafileControl.Anchor |= AnchorStyles.Right;
                    newDatafileControl.Size = new Size(gbDatafiles.Width - Pad * 3, newDatafileControl.Height);

                    // Calculate window height and next control point
                    Height += datafileControl.Height + Pad;
                    startLocation += datafileControl.Height + Pad;

                    // Subscribe Events
                    newDatafileControl.rtbDatafileMessage.Enter += Control_Enter;
                    newDatafileControl.rtbDatafileMessage.Leave += Control_Leave;
                    newDatafileControl.rtbDatafileMessage.DoubleClick += Control_DoubleClick;
                }
            }
            finally
            {
                // Update the message of each data file control
                UpdateDatafilesMessage();

                ResumeLayout(false);
            }

            CenterToScreen();
        }

        /// <summary>
        /// Updates the info in the data files section.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="datafile"></param>
        private static void UpdateDatafileInfo(DatafileControl control, Datafile datafile)
        {
            // Data file info
            FileInfo fileInfo = new FileInfo(Path.Combine(Helper.GetDataFilesDirectory, datafile.Filename));

            // Assign info
            control.gbDatafile.Text = datafile.Filename;
            control.lblMD5Sum.Text = datafile.MD5Sum;
            control.dtpDatafiles.Value = fileInfo.LastWriteTime;
        }

        /// <summary>
        /// Updates the data file's header message according to the info provided.
        /// </summary>
        private void UpdateDatafilesMessage()
        {
            foreach (Datafile datafile in s_datafiles)
            {
                foreach (DatafileControl dfControl in gbDatafiles.Controls.OfType<DatafileControl>().Where(
                    x => x != null && x.gbDatafile.Text == datafile.Filename))
                {
                    dfControl.rtbDatafileMessage.BackColor = SystemColors.Window;
                    dfControl.rtbDatafileMessage.ForeColor = SystemColors.WindowText;

                    if (m_init)
                        EnsureHeaderMessage(dfControl.rtbDatafileMessage);
                    else
                    {
                        dfControl.rtbDatafileMessage.Text = string.Format(
                            s_enUsCulture, DatafilesMessageFormat, tbExpansion.Text, tbExpVersion.Text, tbExpRevision.Text,
                            datafile.Filename.Replace(DatafileHeader, string.Empty)
                                .Replace("-" + s_enUsCulture.Name, string.Empty)
                                .Replace(Datafile.DatafilesExtension, string.Empty));
                    }
                }
            }
        }

        /// <summary>
        /// Ensures that each data file message has an appropriate message header.
        /// </summary>
        /// <param name="control"></param>
        private void EnsureHeaderMessage(Control control)
        {
            StringBuilder sb = new StringBuilder();

            // Remove any existing header and text that is before the header
            control.Text = control.Text.Contains("\n")
                ? control.Text.Remove(0, control.Text.IndexOf("\n", StringComparison.OrdinalIgnoreCase) + 1)
                : control.Text.Remove(0, control.Text.LastIndexOf("m", StringComparison.Ordinal) + 1);

            // Create the new header text
            string headerText = string.Format(s_enUsCulture, DatafilesMessageFormat, tbExpansion.Text, tbExpVersion.Text,
                tbExpRevision.Text,
                control.Parent.Text.Replace(DatafileHeader, string.Empty)
                    .Replace("-" + s_enUsCulture.Name, string.Empty)
                    .Replace(Datafile.DatafilesExtension, string.Empty));

            // Check if the new header text is already present and remove it
            if (control.Text.Contains(headerText))
                control.Text = control.Text.Replace(headerText, string.Empty);

            // Assing the remaining text to a new variable
            string newText = control.Text;

            // Add the new header text to string builder
            sb.Append(headerText);

            // Check the remaining text and add it accordingly
            if (!string.IsNullOrEmpty(newText))
            {
                if (!newText.StartsWith("\n", StringComparison.OrdinalIgnoreCase))
                    sb.AppendLine();

                sb.Append(newText);
            }

            // Assing the new text to control text
            control.Text = sb.ToString();
        }

        /// <summary>
        /// Stores the initial texts in the text control for use in the create button enabling.
        /// </summary>
        private void StoreInitMessage()
        {
            // Store the texts from the release section excluding the update url
            foreach (RichTextBox control in gbRelease.Controls.OfType<RichTextBox>().Where(x => x != null && x != rtbReleaseUrl))
            {
                s_listOfInitMessages.Add(control, control.Text);
            }

            // Store the text from the datafiles section excluding the update url
            foreach (Control control in gbDatafiles.Controls.Cast<Control>().Where(
                x => x != rtbDatafileUrl))
            {
                if (control is TextBox || control is RichTextBox)
                    s_listOfInitMessages.Add(control, control.Text);

                DatafileControl dfControl = control as DatafileControl;
                if (dfControl != null)
                    s_listOfInitMessages.Add(control, dfControl.rtbDatafileMessage.Text);
            }
        }

        /// <summary>
        /// Checks if all conditions are met to update the data file's message header.
        /// </summary>
        private void UpdateDatafilesControls()
        {
            if (m_action == Action.ReleaseOnly)
            {
                LoadDatafilesInfoFromFile();
                return;
            }

            bool updateDatafilesText = true;

            // Look into datafiles controls
            foreach (TextBox control in gbDatafiles.Controls.OfType<TextBox>().Where(x => x != null))
            {
                control.BackColor = SystemColors.Window;
                control.ForeColor = SystemColors.WindowText;

                if (s_listOfInitMessages.FirstOrDefault(x => x.Key == control).Value == control.Text)
                {
                    control.ForeColor = SystemColors.Highlight;
                    updateDatafilesText = false;
                }

                if (!string.IsNullOrEmpty(control.Text))
                    continue;

                control.BackColor = SystemColors.Highlight;
                updateDatafilesText = false;
            }

            // If all conditions are met update the messages
            if (updateDatafilesText)
                UpdateDatafilesMessage();
        }

        /// <summary>
        /// Checks if all conditions are met to enable the create button.
        /// </summary>
        private void UpdateCreateButtonEnabled()
        {
            // Look into release controls, datafiles controls and datafileControl controls
            bool buttonEnable = ButtonEnabledFromReleaseControls() &
                                ButtonEnabledFromDatafileControls() &
                                ButtonEnabledFromDatafileControlControls();

            // Enable/Disable Create button
            btnCreate.Enabled = buttonEnable;
        }

        /// <summary>
        /// "Create" button gets enabled from release controls.
        /// </summary>
        /// <returns></returns>
        private bool ButtonEnabledFromReleaseControls()
        {
            bool buttonEnable = true;
            foreach (RichTextBox control in gbRelease.Controls.OfType<RichTextBox>().Where(x => x != null))
            {
                control.BackColor = SystemColors.Window;
                control.ForeColor = SystemColors.WindowText;

                if (s_listOfInitMessages.FirstOrDefault(x => x.Key == control).Value == control.Text)
                {
                    control.ForeColor = SystemColors.Highlight;
                    buttonEnable = false;
                }

                if (string.IsNullOrEmpty(control.Text))
                    control.BackColor = SystemColors.Highlight;
                else if (control == rtbReleaseMessage || (!Path.GetInvalidPathChars().Any(
                    invalidChar => control.Text.Contains(invalidChar))))
                {
                    continue;
                }

                control.ForeColor = SystemColors.Highlight;
                buttonEnable = false;
            }
            return buttonEnable;
        }

        /// <summary>
        /// Create button gets enabled from datafile controls.
        /// </summary>
        /// <returns></returns>
        private bool ButtonEnabledFromDatafileControls()
        {
            bool buttonEnable = true;
            foreach (Control control in gbDatafiles.Controls.Cast<Control>().Where(x => x is TextBox || x is RichTextBox))
            {
                control.BackColor = SystemColors.Window;
                control.ForeColor = SystemColors.WindowText;
                if (s_listOfInitMessages.FirstOrDefault(x => x.Key == control).Value == control.Text)
                {
                    control.ForeColor = SystemColors.Highlight;
                    buttonEnable = false;
                }

                if (string.IsNullOrEmpty(control.Text))
                    control.BackColor = SystemColors.Highlight;
                else if (control != rtbDatafileUrl || (!Path.GetInvalidPathChars().Any(
                    invalidChar => control.Text.Contains(invalidChar)) && !control.Text.Contains("#")))
                {
                    continue;
                }

                control.ForeColor = SystemColors.Highlight;
                buttonEnable = false;
            }
            return buttonEnable;
        }

        /// <summary>
        /// Create button gets enabled from datafile control controls.
        /// </summary>
        /// <returns></returns>
        private bool ButtonEnabledFromDatafileControlControls()
        {
            bool buttonEnable = true;
            foreach (DatafileControl dfControl in gbDatafiles.Controls.OfType<DatafileControl>().Where(x => x != null))
            {
                dfControl.rtbDatafileMessage.BackColor = SystemColors.Window;
                dfControl.rtbDatafileMessage.ForeColor = SystemColors.WindowText;

                if (dfControl.rtbDatafileMessage.Text.Contains(s_listOfInitMessages.FirstOrDefault(x => x.Key == dfControl).Value))
                {
                    dfControl.rtbDatafileMessage.ForeColor = SystemColors.Highlight;
                    buttonEnable = false;
                }

                if (!string.IsNullOrEmpty(dfControl.rtbDatafileMessage.Text))
                    continue;

                dfControl.rtbDatafileMessage.BackColor = SystemColors.Highlight;
                buttonEnable = false;
            }
            return buttonEnable;
        }

        /// <summary>
        /// Deserializes the existing patch file.
        /// </summary>
        /// <returns></returns>
        private static SerializablePatch TryDeserializePatchXml()
        {
            FileInfo file = new FileInfo(Helper.GetPatchFilePath);

            SerializablePatch xmlDoc = File.Exists(file.FullName) ? Util.
                DeserializeXmlFromFile<SerializablePatch>(file.FullName) : null;

            return xmlDoc;
        }

        /// <summary>
        /// Serializes the patch file to string.
        /// </summary>
        /// <returns></returns>
        private string ExportPatchXml()
        {
            SerializablePatch serial = new SerializablePatch();

            ExportRelease(serial.Release);
            ExportReleases(serial.Releases);
            ExportDatafiles(serial.Datafiles);

            XmlDocument doc = (XmlDocument)Util.SerializeToXmlDocument(serial);
            return doc != null ? Util.GetXmlStringRepresentation(doc) : string.Empty;
        }

        /// <summary>
        /// Serializes the release info for the patch file.
        /// </summary>
        /// <param name="serialRelease">The serial release.</param>
        private void ExportRelease(SerializableRelease serialRelease)
        {
            if (GetAssemblyVersion().FileMajorPart == 2)
            {
                serialRelease.Date = dtpRelease.Value.ToString(DateTimeFormat, s_enUsCulture);
                serialRelease.Version = lblEVEMonVersion.Text;
                serialRelease.TopicAddress = rtbTopicUrl.Text;
                serialRelease.PatchAddress = string.Concat(rtbReleaseUrl.Text,
                    string.Format(CultureConstants.InvariantCulture, InstallerFilename, GetAssemblyVersion().ProductVersion));
                serialRelease.MD5Sum = lblMD5Sum.Text;
                serialRelease.InstallerArgs = InstallerArgs;
                serialRelease.AdditionalArgs = AdditionalArgs;
                serialRelease.Message = rtbReleaseMessage.Text.Trim();
                return;
            }

            SerializablePatch patch = TryDeserializePatchXml();
            if (patch == null)
                return;
            var release = patch.Release;
            if ((patch.Releases?.Count ?? 0) > 0)
                release = patch.Releases[0];

            serialRelease.Date = release.Date;
            serialRelease.Version = release.Version;
            serialRelease.TopicAddress = release.TopicAddress;
            serialRelease.PatchAddress = release.PatchAddress;
            serialRelease.MD5Sum = release.MD5Sum;
            serialRelease.InstallerArgs = release.InstallerArgs;
            serialRelease.AdditionalArgs = release.AdditionalArgs;
            serialRelease.Message = release.Message;
        }

        /// <summary>
        /// Serializes the releases info for the patch file.
        /// </summary>
        /// <param name="serialReleases">The serial releases.</param>
        private void ExportReleases(IList<SerializableRelease> serialReleases)
        {
            SerializablePatch patch = TryDeserializePatchXml();
            if (patch == null)
                return;

            foreach (SerializableRelease release in patch.Releases
                .Where(release => Version.Parse(release.Version).Major != GetAssemblyVersion().ProductMajorPart))
            {
                serialReleases.Add(release);
            }

            if (patch.Releases.Any() && patch.Releases.All(release => Version.Parse(release.Version).Major != GetAssemblyVersion().ProductMajorPart))
                return;

            var serialRelease = new SerializableRelease
            {
                Date = dtpRelease.Value.ToString(DateTimeFormat, s_enUsCulture),
                Version = lblEVEMonVersion.Text,
                TopicAddress = rtbTopicUrl.Text,
                PatchAddress = string.Concat(rtbReleaseUrl.Text,
                    string.Format(CultureConstants.InvariantCulture, InstallerFilename, GetAssemblyVersion().ProductVersion)),
                MD5Sum = lblMD5Sum.Text,
                InstallerArgs = InstallerArgs,
                AdditionalArgs = AdditionalArgs,
                Message = rtbReleaseMessage.Text.Trim()
            };
            serialReleases.Add(serialRelease);
            serialReleases.StableSort((release, serializableRelease)
                => string.Compare(release.Version, serializableRelease.Version, StringComparison.Ordinal));
        }

        /// <summary>
        /// Serializes the data files info for the patch file.
        /// </summary>
        /// <param name="datafiles"></param>
        /// <returns></returns>
        private void ExportDatafiles(ICollection<SerializableDatafile> datafiles)
        {
            string url = $"{rtbDatafileUrl.Text}{tbExpansion.Text}{Path.AltDirectorySeparatorChar}{tbExpRevision.Text}";

            foreach (Datafile datafile in s_datafiles)
            {
                SerializableDatafile serialDatafile = new SerializableDatafile();
                datafiles.Add(serialDatafile);

                foreach (DatafileControl dfControl in gbDatafiles.Controls.OfType<DatafileControl>().Where(
                    x => x != null && x.gbDatafile.Text == datafile.Filename))
                {
                    serialDatafile.Name = dfControl.gbDatafile.Text;
                    serialDatafile.Date = dfControl.dtpDatafiles.Value.ToString(DateTimeFormat, s_enUsCulture);
                    serialDatafile.MD5Sum = dfControl.lblMD5Sum.Text;
                    serialDatafile.Address = url;
                    serialDatafile.Message = dfControl.rtbDatafileMessage.Text.Trim();

                    if (!serialDatafile.Message.Contains(CompatibilityMessage))
                        serialDatafile.Message += CompatibilityMessage;
                }
            }
        }

        /// <summary>
        /// Creates the patch xml file.
        /// </summary>
        private async Task SaveFileAsync()
        {
            string patch = ExportPatchXml();
            string filenamePath = Helper.GetPatchFilePath;
            string oldPatchFilePath = filenamePath.Replace(".xml", "-old.xml");

            try
            {
                FileHelper.DeleteFile(oldPatchFilePath);
                File.Move(filenamePath, oldPatchFilePath);

                await FileHelper.OverwriteOrWarnTheUserAsync(filenamePath,
                    async fs =>
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            await sw.WriteAsync(patch);
                            await sw.FlushAsync();
                            await fs.FlushAsync();
                        }
                        return true;
                    });
            }
            catch (Exception exc)
            {
                string msgText = $"The file failed to be created successfully.\r\nReason:{exc.Message}";
                MessageBox.Show(msgText, Helper.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(@"The file was created successfully.",
                Helper.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Loads the release info from existing patch file.
        /// </summary>
        /// <returns></returns>
        private void LoadReleaseInfoFromFile()
        {
            SerializablePatch patch = TryDeserializePatchXml();
            if (patch == null)
                return;

            DateTime date;
            SerializableRelease latestRelease;
            if (!patch.Releases.Any())
            {
                latestRelease = patch.Release;
            }
            else
            {
                string maxVersion = patch.Releases.Select(pi => pi.Version).Max();
                latestRelease = patch.Releases.FirstOrDefault(pi => pi.Version == maxVersion);
            }

            if (latestRelease == null)
                return;

            if (DateTime.TryParse(latestRelease.Date, out date))
                dtpRelease.Value = date;
            
            lblEVEMonVersion.Text = latestRelease.Version;
            rtbTopicUrl.Text = latestRelease.TopicAddress;
            rtbReleaseUrl.Text = new Uri(new Uri(latestRelease.PatchAddress), ".").AbsoluteUri;
            lblMD5Sum.Text = latestRelease.MD5Sum;
            rtbReleaseMessage.Text = latestRelease.Message;
        }

        /// <summary>
        /// Loads the datafiles info from existing patch file.
        /// </summary>
        private void LoadDatafilesInfoFromFile()
        {
            SerializablePatch patch = TryDeserializePatchXml();
            if (patch == null)
                return;

            Uri uri = new Uri(patch.Datafiles.First().Address);

            string revision = uri.Segments.Last().Replace(Path.AltDirectorySeparatorChar.ToString(), string.Empty);
            string expansionName = WebUtility.UrlDecode(uri.Segments[2].Replace(Path.
                AltDirectorySeparatorChar.ToString(), string.Empty));

            int expansionNameLength = patch.Datafiles.First().Message.IndexOf(expansionName, StringComparison.Ordinal) +
                                         expansionName.Length + 1;
            string version = patch.Datafiles[0].Message.Remove(0, expansionNameLength).Split(' ').First();

            Uri baseUrl = new Uri(uri, "..");

            foreach (SerializableDatafile datafile in patch.Datafiles)
            {
                rtbDatafileUrl.Text = baseUrl.AbsoluteUri;
                tbExpansion.Text = expansionName;
                tbExpVersion.Text = version;
                tbExpRevision.Text = revision;

                foreach (DatafileControl dfControl in gbDatafiles.Controls.OfType<DatafileControl>().Where(
                    x => x != null && x.gbDatafile.Text == datafile.Name))
                {
                    DateTime date;
                    if (DateTime.TryParse(datafile.Date, out date))
                        dfControl.dtpDatafiles.Value = date;

                    dfControl.lblMD5Sum.Text = datafile.MD5Sum;
                    dfControl.rtbDatafileMessage.Text = datafile.Message;
                }
            }
        }

        #endregion


        #region Control Handlers

        /// <summary>
        /// Occurs on clicking the "cancel" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Occurs on clicking the "create" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            btnCancel.Text = @"Close";
            await SaveFileAsync();
        }

        /// <summary>
        /// Occurs on clicking the "paste from clipboard" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPaste_Click(object sender, EventArgs e)
        {
            if (m_activeTextBox == null)
                return;

            m_activeTextBox.Text = Clipboard.GetText();

            if (m_activeTextBox.Parent.Parent is DatafileControl)
                EnsureHeaderMessage(m_activeTextBox);

            m_activeTextBox = null;
        }

        /// <summary>
        /// Occurs on clicking the "load for existing file" button in release section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadReleaseInfo_Click(object sender, EventArgs e)
        {
            LoadReleaseInfoFromFile();
            UpdateCreateButtonEnabled();
        }

        /// <summary>
        /// Occurs on clicking the "load for existing file" button in datafiles section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadDatafileInfo_Click(object sender, EventArgs e)
        {
            LoadDatafilesInfoFromFile();
            UpdateCreateButtonEnabled();
        }

        /// <summary>
        /// Occurs on clicking the "clear all" button in release section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReleaseClear_Click(object sender, EventArgs e)
        {
            dtpRelease.ResetText();
            rtbTopicUrl.ResetText();
            rtbReleaseUrl.ResetText();
            rtbReleaseMessage.ResetText();

            UpdateCreateButtonEnabled();
        }

        /// <summary>
        /// Occurs on clicking the "clear all" button in datafiles section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDatafilesClear_Click(object sender, EventArgs e)
        {
            rtbDatafileUrl.ResetText();
            tbExpansion.ResetText();
            tbExpVersion.ResetText();
            tbExpRevision.ResetText();

            foreach (DatafileControl dfControl in gbDatafiles.Controls.OfType<DatafileControl>().Where(x => x != null))
            {
                dfControl.dtpDatafiles.ResetText();
                dfControl.rtbDatafileMessage.ResetText();

                Datafile datafile = s_datafiles.First(x => x.Filename == dfControl.gbDatafile.Text);

                if (datafile != null)
                    UpdateDatafileInfo(dfControl, datafile);
            }

            UpdateCreateButtonEnabled();
        }

        /// <summary>
        /// Occurs on mouse double click inside a text control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_DoubleClick(object sender, EventArgs e)
        {
            Control control = null;

            DatafileControl dfControl = sender as DatafileControl;
            if (dfControl != null)
                control = dfControl.rtbDatafileMessage;

            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null)
                control = richTextBox;

            TextBox textBox = sender as TextBox;
            if (textBox != null)
                control = textBox;

            if (control == null)
                return;

            m_activeTextBox = control;
            m_text = control.Text;
            control.Text = string.Empty;
        }

        /// <summary>
        /// Occurs when entering a text control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
                textBox.ForeColor = SystemColors.WindowText;

            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null)
                richTextBox.ForeColor = SystemColors.WindowText;
        }

        /// <summary>
        /// Occurs when leaving a text control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Leave(object sender, EventArgs e)
        {
            Control control = null;

            RichTextBox richTextBox = sender as RichTextBox;
            if (richTextBox != null)
                control = richTextBox;

            TextBox textBox = sender as TextBox;
            if (textBox != null)
                control = textBox;

            if (control == null)
                return;

            if (control == tbExpansion
                || control == tbExpVersion
                || control == tbExpRevision
                || control.Parent.Parent is DatafileControl)
            {
                UpdateDatafilesControls();
            }

            if (control.Text.Length == 0)
                control.Text = m_text;

            UpdateCreateButtonEnabled();
        }

        #endregion
    }
}
