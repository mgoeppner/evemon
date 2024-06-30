using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.ExternalCalendar;
using EVEMon.Common.Factories;
using EVEMon.Common.Serialization;
using EVEMon.Common.Serialization.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEMon.SettingsUI
{
    public partial class ExternalCalendarControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalCalendarControl"/> class.
        /// </summary>
        public ExternalCalendarControl()
        {
            InitializeComponent();

            apiResponseLabel.Font = FontFactory.GetFont("Tahoma", FontStyle.Bold);
            apiResponseLabel.ResetText();

            throbber.Visible = false;
            throbber.BringToFront();

            btnRevokeAuth.Enabled = false;
            tbGoogleCalendarName.Enabled = false;
            cbGoogleReminder.Enabled = false;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await RequestGoogleCalendarAuthentication(true);
        }

        /// <summary>
        /// Sets the external calendar.
        /// </summary>
        /// <param name="settings">The settings.</param>
        internal void SetExternalCalendar(SerializableSettings settings)
        {
            // Google calendar reminder method
            cbGoogleReminder.Items.Clear();
            cbGoogleReminder.Items.AddRange(GoogleCalendarEvent.ReminderMethods.ToArray());

            rbGoogle.Checked = true;

            rbDefaultCalendar.Checked = settings.Calendar.UseOutlookDefaultCalendar;
            rbCustomCalendar.Checked = !rbDefaultCalendar.Checked;
            tbOutlookCalendarPath.Text = settings.Calendar.OutlookCustomCalendarPath;

            tbGoogleCalendarName.Text = settings.Calendar.GoogleCalendarName;
            cbGoogleReminder.SelectedIndex = (int)settings.Calendar.GoogleEventReminder;
            cbSetReminder.Checked = settings.Calendar.UseReminding;
            tbReminder.Text = settings.Calendar.RemindingInterval.ToString(CultureConstants.
                DefaultCulture);
            cbUseAlterateReminder.Checked = settings.Calendar.UseAlternateReminding;
            dtpEarlyReminder.Value = settings.Calendar.EarlyReminding;
            dtpLateReminder.Value = settings.Calendar.LateReminding;
            cbLastQueuedSkillOnly.Checked = settings.Calendar.LastQueuedSkillOnly;
            
            UpdateControlsVisibility();
        }

        /// <summary>
        /// Applies the external calendar settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        internal void ApplyExternalCalendarSettings(SerializableSettings settings)
        {
            settings.Calendar.Provider = CalendarProvider.Google;

            settings.Calendar.UseOutlookDefaultCalendar = rbDefaultCalendar.Checked;
            settings.Calendar.OutlookCustomCalendarPath = tbOutlookCalendarPath.Text.Trim();

            settings.Calendar.GoogleEventReminder = cbGoogleReminder.SelectedIndex != -1
                ? (GoogleCalendarReminder)cbGoogleReminder.SelectedIndex
                : GoogleCalendarReminder.Email;
            settings.Calendar.GoogleCalendarName = tbGoogleCalendarName.Text;

            settings.Calendar.UseReminding = cbSetReminder.Checked;
            int interval;
            if (int.TryParse(tbReminder.Text, out interval) && interval > 0)
                settings.Calendar.RemindingInterval = interval;
            settings.Calendar.UseAlternateReminding = cbUseAlterateReminder.Checked;
            settings.Calendar.EarlyReminding = dtpEarlyReminder.Value;
            settings.Calendar.LateReminding = dtpLateReminder.Value;
            settings.Calendar.LastQueuedSkillOnly = cbLastQueuedSkillOnly.Checked;
        }


        #region Helper Methods
        
        /// <summary>
        /// Updates the controls visibility.
        /// </summary>
        private void UpdateControlsVisibility()
        {
            gbMSOutlook.Visible = true;
            gbGoogle.Visible = rbGoogle.Checked;
            calendarPathLabel.Enabled = tbOutlookCalendarPath.Enabled = rbCustomCalendar.Checked;
            calendarPathExampleLabel.Visible = rbCustomCalendar.Checked;
            cbSetReminder.Enabled = lblMinutes.Enabled = !cbUseAlterateReminder.Checked;
            tbReminder.Enabled = cbSetReminder.Checked;
            cbUseAlterateReminder.Enabled = lblEarlyReminder.Enabled = lblLateReminder.Enabled = !cbSetReminder.Checked;
            dtpEarlyReminder.Enabled = cbUseAlterateReminder.Checked;
            dtpLateReminder.Enabled = cbUseAlterateReminder.Checked;

            if (rbCustomCalendar.Checked)
                tbOutlookCalendarPath.Focus();
        }

        /// <summary>
        /// Requests the Google calendar authentication.
        /// </summary>
        /// <param name="checkAuth">if set to <c>true</c> [check authentication].</param>
        private async Task RequestGoogleCalendarAuthentication(bool checkAuth = false)
        {
            if (!Enabled || !rbGoogle.Checked)
                return;

            apiResponseLabel.ResetText();
            apiResponseLabel.ResetForeColor();

            if (checkAuth && !GoogleCalendarEvent.HasCredentialsStored())
                return;

            throbber.State = ThrobberState.Rotating;
            throbber.Visible = true;

            SerializableAPIResult<SerializableAPICredentials> result = await GoogleCalendarEvent.RequestAuth(checkAuth);

            throbber.State = ThrobberState.Stopped;
            throbber.Visible = false;

            btnRequestAuth.Enabled = result.HasError;
            btnRevokeAuth.Enabled = tbGoogleCalendarName.Enabled = cbGoogleReminder.Enabled = !result.HasError;

            apiResponseLabel.ForeColor = result.HasError ? Color.Red : Color.Green;
            apiResponseLabel.Text = result.HasError ? result.Error.ErrorCode ?? result.Error.ErrorMessage : @"Authenticated";
        }

        #endregion


        #region Local Events

        /// <summary>
        /// This handler occurs when the radio buttons check changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMustEnableOrDisable(object sender, EventArgs e)
        {
            UpdateControlsVisibility();
        }

        /// <summary>
        /// Handles the EnabledChanged event of the ExternalCalendarControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ExternalCalendarControl_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
            {
                UpdateControlsVisibility();
                await RequestGoogleCalendarAuthentication(true);
            }
        }

        /// <summary>
        /// Outlook custom calendar path validation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void tbCalendarPath_Validating(object sender, CancelEventArgs e)
        {
            var badChars = Path.GetInvalidPathChars();
            e.Cancel = Enabled && rbCustomCalendar.Checked &&
                (tbOutlookCalendarPath.Text.Any(x => badChars.Contains(x)) ||
                string.IsNullOrWhiteSpace(tbOutlookCalendarPath.Text));
            if (e.Cancel)
                MessageBox.Show(Properties.Resources.ErrorNoCalendar, @"MS Outlook",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Reminder value validation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void tbReminder_Validating(object sender, CancelEventArgs e)
        {
            int value;
            e.Cancel = !int.TryParse(tbReminder.Text, out value) && value > 0;
            if (e.Cancel)
                MessageBox.Show(Properties.Resources.ErrorBadReminder, @"Reminder Interval",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Occurs when clicking on Google radio button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void rbGoogle_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlsVisibility();
            await RequestGoogleCalendarAuthentication(true);
        }

        /// <summary>
        /// Handles the Click event of the btnRequestAuth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRequestAuth_Click(object sender, EventArgs e)
        {
            await RequestGoogleCalendarAuthentication();
        }

        /// <summary>
        /// Handles the Click event of the btnRevokeAuth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRevokeAuth_Click(object sender, EventArgs e)
        {
            apiResponseLabel.ResetText();
            apiResponseLabel.ResetForeColor();

            throbber.State = ThrobberState.Rotating;
            throbber.Visible = true;

            var result = await GoogleCalendarEvent.RevokeAuth();

            throbber.State = ThrobberState.Stopped;
            throbber.Visible = false;

            bool error = result.HasError;
            btnRequestAuth.Enabled = !error;
            btnRevokeAuth.Enabled = tbGoogleCalendarName.Enabled = cbGoogleReminder.Enabled =
                error;
            if (error)
            {
                apiResponseLabel.ForeColor = Color.Red;
                apiResponseLabel.Text = result.Error.ErrorMessage;
            }
        }

        /// <summary>
        /// Occurs when clicking on the calendarIDLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void calendarIDLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.OpenURL(new Uri(NetworkConstants.GoogleCalendarSetup));
        }

        #endregion
    }
}
