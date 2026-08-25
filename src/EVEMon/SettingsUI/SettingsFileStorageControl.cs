using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.CloudStorageServices;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Enumerations;

namespace EVEMon.SettingsUI
{
    public partial class SettingsFileStorageControl : UserControl
    {
        private static bool s_queryPending;


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsFileStorageControl"/> class.
        /// </summary>
        public SettingsFileStorageControl()
        {
            InitializeComponent();
            apiResponseLabel.Font = new Font(apiResponseLabel.Font, FontStyle.Bold);
            apiResponseLabel.Text = string.Empty;
            throbber.Visible = false;
            throbber.BringToFront();
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        [Browsable(false)]
        private static CloudStorageServiceProvider Provider => Settings.CloudStorageServiceProvider.Provider;

        #endregion


        #region Local Events

        /// <summary>
        /// Occurs when the control loads.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SettingsFileStorageControl_Load(object sender, EventArgs e)
        {

            CloudStorageServiceProvider.CredentialsChecked += CloudStorageServiceProvider_CredentialsChecked;
            CloudStorageServiceProvider.SettingsReset += CloudStorageServiceProvider_SettingsReset;
            CloudStorageServiceProvider.FileUploaded += CloudStorageServiceProvider_FileUploaded;
            CloudStorageServiceProvider.FileDownloaded += CloudStorageServiceProvider_FileDownloaded;

            Disposed += OnDisposed;

            alwaysUploadCheckBox.Checked = CloudStorageServiceSettings.Default.UploadAlways;
            alwaysDownloadCheckBox.Checked = CloudStorageServiceSettings.Default.DownloadAlways;
            useImmediatelyCheckBox.Checked = CloudStorageServiceSettings.Default.UseImmediately;
            useImmediatelyCheckBox.Enabled = alwaysDownloadCheckBox.Checked;
            Enabled = false;
        }

        /// <summary>
        /// Occurs when the control gets disposed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnDisposed(object sender, EventArgs e)
        {
            CloudStorageServiceProvider.CredentialsChecked -= CloudStorageServiceProvider_CredentialsChecked;
            CloudStorageServiceProvider.SettingsReset -= CloudStorageServiceProvider_SettingsReset;
            CloudStorageServiceProvider.FileUploaded -= CloudStorageServiceProvider_FileUploaded;
            CloudStorageServiceProvider.FileDownloaded -= CloudStorageServiceProvider_FileDownloaded;

            Disposed -= OnDisposed;
        }

        /// <summary>
        /// When the control becomes visible again, we update the content.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (!Visible)
                return;

            ResetTextAndColor();

            Enabled = Provider != null && Provider.HasCredentialsStored && CloudStorageServiceProvider.IsAuthenticated;
        }

        /// <summary>
        /// Occurs when the checkbox state changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void alwaysUploadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CloudStorageServiceSettings.Default.UploadAlways = alwaysUploadCheckBox.Checked;
            CloudStorageServiceSettings.Default.Save();
        }

        /// <summary>
        /// Occurs when the checkbox state changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void alwaysDownloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useImmediatelyCheckBox.Enabled = alwaysDownloadCheckBox.Checked;
            CloudStorageServiceSettings.Default.DownloadAlways = alwaysDownloadCheckBox.Checked;
            CloudStorageServiceSettings.Default.Save();
        }

        /// <summary>
        /// Occurs when the checkbox state changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void useImmediatelyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CloudStorageServiceSettings.Default.UseImmediately = useImmediatelyCheckBox.Checked;
            CloudStorageServiceSettings.Default.Save();
        }

        /// <summary>
        /// Occurs when clicking the "Upload settings file" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private async void uploadSettingsFileButton_Click(object sender, EventArgs e)
        {
            ResetTextAndColor();

            if (s_queryPending)
                return;

            s_queryPending = true;
            throbber.State = ThrobberState.Rotating;
            throbber.Visible = true;

            await Settings.SaveImmediateAsync();

            Task uploadSettingsFileAsync = Provider?.UploadSettingsFileAsync();
            if (uploadSettingsFileAsync != null)
                await uploadSettingsFileAsync;
        }

        /// <summary>
        /// Occurs when clicking the "Download settings file" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private async void downloadSettingsFileButton_Click(object sender, EventArgs e)
        {
            ResetTextAndColor();

            if (s_queryPending)
                return;

            s_queryPending = true;
            throbber.State = ThrobberState.Rotating;
            throbber.Visible = true;

            Task downloadSettingsFileAsync = Provider?.DownloadSettingsFileAsync();
            if (downloadSettingsFileAsync != null)
                await downloadSettingsFileAsync;
        }

        #endregion


        #region Global Events

        /// <summary>
        /// Occurs when the cloud storage service provider credentials get authenticated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CloudStorageServiceProvider_CredentialsChecked(object sender, CloudStorageServiceProviderEventArgs e)
        {
            ResetTextAndColor();

            Enabled = Provider != null && Provider.HasCredentialsStored && CloudStorageServiceProvider.IsAuthenticated;
        }

        /// <summary>
        /// Occurs when the cloud storage service provider credentials get authenticated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CloudStorageServiceProvider_SettingsReset(object sender, CloudStorageServiceProviderEventArgs e)
        {
            ResetTextAndColor();

            Enabled = Provider != null && Provider.HasCredentialsStored;
        }

        /// <summary>
        /// Occurs when the file has been uploaded to the cloud storage service provider.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CloudStorageServiceProviderEventArgs"/> instance containing the event data.</param>
        private void CloudStorageServiceProvider_FileUploaded(object sender, CloudStorageServiceProviderEventArgs e)
        {
            s_queryPending = false;

            throbber.State = ThrobberState.Stopped;
            throbber.Visible = false;

            apiResponseLabel.ForeColor = e.HasError ? Color.Red : Color.Green;
            apiResponseLabel.Text = e.HasError ? e.ErrorMessage : @"File uploaded successfully";
        }

        /// <summary>
        /// Occurs when the file has been dwloaded from the cloud storage service provider.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CloudStorageServiceProviderEventArgs"/> instance containing the event data.</param>
        private void CloudStorageServiceProvider_FileDownloaded(object sender, CloudStorageServiceProviderEventArgs e)
        {
            s_queryPending = false;

            throbber.State = ThrobberState.Stopped;
            throbber.Visible = false;

            if (!e.HasError)
                return;

            apiResponseLabel.ForeColor = Color.Red;
            apiResponseLabel.Text = e.ErrorMessage;
        }

        #endregion


        #region Helper Methods
        
        /// <summary>
        /// Resets the color of the text and.
        /// </summary>
        private void ResetTextAndColor()
        {
            apiResponseLabel.ResetForeColor();
            apiResponseLabel.ResetText();
        }

        #endregion
    }
}