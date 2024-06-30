namespace EVEMon.SettingsUI
{
    partial class ExternalCalendarControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalCalendarControl));
            externalCalendarPanel = new System.Windows.Forms.Panel();
            gbReminder = new System.Windows.Forms.GroupBox();
            lblMinutes = new System.Windows.Forms.Label();
            cbSetReminder = new System.Windows.Forms.CheckBox();
            lblLateReminder = new System.Windows.Forms.Label();
            dtpLateReminder = new System.Windows.Forms.DateTimePicker();
            lblEarlyReminder = new System.Windows.Forms.Label();
            tbReminder = new System.Windows.Forms.TextBox();
            cbUseAlterateReminder = new System.Windows.Forms.CheckBox();
            dtpEarlyReminder = new System.Windows.Forms.DateTimePicker();
            cbLastQueuedSkillOnly = new System.Windows.Forms.CheckBox();
            rbGoogle = new System.Windows.Forms.RadioButton();
            gbGoogle = new System.Windows.Forms.GroupBox();
            calendarIDLinkLabel = new System.Windows.Forms.LinkLabel();
            apiResponseLabel = new System.Windows.Forms.Label();
            btnRevokeAuth = new System.Windows.Forms.Button();
            btnRequestAuth = new System.Windows.Forms.Button();
            cbGoogleReminder = new System.Windows.Forms.ComboBox();
            lblReminder = new System.Windows.Forms.Label();
            lblCalendarId = new System.Windows.Forms.Label();
            tbGoogleCalendarName = new System.Windows.Forms.TextBox();
            throbber = new Common.Controls.Throbber();
            gbMSOutlook = new System.Windows.Forms.GroupBox();
            calendarPathExampleLabel = new System.Windows.Forms.Label();
            rbCustomCalendar = new System.Windows.Forms.RadioButton();
            rbDefaultCalendar = new System.Windows.Forms.RadioButton();
            tbOutlookCalendarPath = new System.Windows.Forms.TextBox();
            calendarPathLabel = new System.Windows.Forms.Label();
            externalCalendarPanel.SuspendLayout();
            gbReminder.SuspendLayout();
            gbGoogle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)throbber).BeginInit();
            gbMSOutlook.SuspendLayout();
            SuspendLayout();
            // 
            // externalCalendarPanel
            // 
            externalCalendarPanel.Controls.Add(gbReminder);
            externalCalendarPanel.Controls.Add(cbLastQueuedSkillOnly);
            externalCalendarPanel.Controls.Add(rbGoogle);
            externalCalendarPanel.Controls.Add(gbGoogle);
            externalCalendarPanel.Controls.Add(gbMSOutlook);
            externalCalendarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            externalCalendarPanel.Location = new System.Drawing.Point(0, 0);
            externalCalendarPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            externalCalendarPanel.Name = "externalCalendarPanel";
            externalCalendarPanel.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            externalCalendarPanel.Size = new System.Drawing.Size(512, 343);
            externalCalendarPanel.TabIndex = 13;
            // 
            // gbReminder
            // 
            gbReminder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbReminder.Controls.Add(lblMinutes);
            gbReminder.Controls.Add(cbSetReminder);
            gbReminder.Controls.Add(lblLateReminder);
            gbReminder.Controls.Add(dtpLateReminder);
            gbReminder.Controls.Add(lblEarlyReminder);
            gbReminder.Controls.Add(tbReminder);
            gbReminder.Controls.Add(cbUseAlterateReminder);
            gbReminder.Controls.Add(dtpEarlyReminder);
            gbReminder.Location = new System.Drawing.Point(7, 195);
            gbReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbReminder.Name = "gbReminder";
            gbReminder.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbReminder.Size = new System.Drawing.Size(497, 111);
            gbReminder.TabIndex = 13;
            gbReminder.TabStop = false;
            gbReminder.Text = "Reminder Setting";
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.Location = new System.Drawing.Point(168, 24);
            lblMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new System.Drawing.Size(50, 15);
            lblMinutes.TabIndex = 11;
            lblMinutes.Text = "minutes";
            // 
            // cbSetReminder
            // 
            cbSetReminder.AutoSize = true;
            cbSetReminder.Location = new System.Drawing.Point(9, 23);
            cbSetReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbSetReminder.Name = "cbSetReminder";
            cbSetReminder.Size = new System.Drawing.Size(96, 19);
            cbSetReminder.TabIndex = 6;
            cbSetReminder.Text = "Use reminder";
            cbSetReminder.UseVisualStyleBackColor = true;
            cbSetReminder.Click += OnMustEnableOrDisable;
            // 
            // lblLateReminder
            // 
            lblLateReminder.AutoSize = true;
            lblLateReminder.Location = new System.Drawing.Point(224, 81);
            lblLateReminder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblLateReminder.Name = "lblLateReminder";
            lblLateReminder.Size = new System.Drawing.Size(86, 15);
            lblLateReminder.TabIndex = 6;
            lblLateReminder.Text = "Late Reminder:";
            // 
            // dtpLateReminder
            // 
            dtpLateReminder.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dtpLateReminder.Location = new System.Drawing.Point(324, 76);
            dtpLateReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dtpLateReminder.Name = "dtpLateReminder";
            dtpLateReminder.ShowUpDown = true;
            dtpLateReminder.Size = new System.Drawing.Size(83, 23);
            dtpLateReminder.TabIndex = 10;
            dtpLateReminder.Value = new System.DateTime(2007, 9, 21, 0, 0, 0, 0);
            // 
            // lblEarlyReminder
            // 
            lblEarlyReminder.AutoSize = true;
            lblEarlyReminder.Location = new System.Drawing.Point(31, 81);
            lblEarlyReminder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblEarlyReminder.Name = "lblEarlyReminder";
            lblEarlyReminder.Size = new System.Drawing.Size(89, 15);
            lblEarlyReminder.TabIndex = 4;
            lblEarlyReminder.Text = "Early Reminder:";
            // 
            // tbReminder
            // 
            tbReminder.Location = new System.Drawing.Point(119, 21);
            tbReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbReminder.Name = "tbReminder";
            tbReminder.Size = new System.Drawing.Size(40, 23);
            tbReminder.TabIndex = 7;
            tbReminder.Text = "10";
            tbReminder.Validating += tbReminder_Validating;
            // 
            // cbUseAlterateReminder
            // 
            cbUseAlterateReminder.AutoSize = true;
            cbUseAlterateReminder.Location = new System.Drawing.Point(9, 52);
            cbUseAlterateReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbUseAlterateReminder.Name = "cbUseAlterateReminder";
            cbUseAlterateReminder.Size = new System.Drawing.Size(145, 19);
            cbUseAlterateReminder.TabIndex = 8;
            cbUseAlterateReminder.Text = "Use alternate reminder";
            cbUseAlterateReminder.UseVisualStyleBackColor = true;
            cbUseAlterateReminder.Click += OnMustEnableOrDisable;
            // 
            // dtpEarlyReminder
            // 
            dtpEarlyReminder.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            dtpEarlyReminder.Location = new System.Drawing.Point(135, 76);
            dtpEarlyReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dtpEarlyReminder.Name = "dtpEarlyReminder";
            dtpEarlyReminder.ShowUpDown = true;
            dtpEarlyReminder.Size = new System.Drawing.Size(81, 23);
            dtpEarlyReminder.TabIndex = 9;
            dtpEarlyReminder.Value = new System.DateTime(2007, 9, 21, 0, 0, 0, 0);
            // 
            // cbLastQueuedSkillOnly
            // 
            cbLastQueuedSkillOnly.AutoSize = true;
            cbLastQueuedSkillOnly.Location = new System.Drawing.Point(16, 313);
            cbLastQueuedSkillOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbLastQueuedSkillOnly.Name = "cbLastQueuedSkillOnly";
            cbLastQueuedSkillOnly.Size = new System.Drawing.Size(144, 19);
            cbLastQueuedSkillOnly.TabIndex = 12;
            cbLastQueuedSkillOnly.Text = "Last Queued Skill Only";
            cbLastQueuedSkillOnly.UseVisualStyleBackColor = true;
            // 
            // rbGoogle
            // 
            rbGoogle.AutoSize = true;
            rbGoogle.CausesValidation = false;
            rbGoogle.Location = new System.Drawing.Point(13, 11);
            rbGoogle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbGoogle.Name = "rbGoogle";
            rbGoogle.Size = new System.Drawing.Size(63, 19);
            rbGoogle.TabIndex = 2;
            rbGoogle.Text = "Google";
            rbGoogle.UseVisualStyleBackColor = true;
            rbGoogle.CheckedChanged += rbGoogle_CheckedChanged;
            // 
            // gbGoogle
            // 
            gbGoogle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbGoogle.Controls.Add(calendarIDLinkLabel);
            gbGoogle.Controls.Add(apiResponseLabel);
            gbGoogle.Controls.Add(btnRevokeAuth);
            gbGoogle.Controls.Add(btnRequestAuth);
            gbGoogle.Controls.Add(cbGoogleReminder);
            gbGoogle.Controls.Add(lblReminder);
            gbGoogle.Controls.Add(lblCalendarId);
            gbGoogle.Controls.Add(tbGoogleCalendarName);
            gbGoogle.Controls.Add(throbber);
            gbGoogle.Location = new System.Drawing.Point(6, 36);
            gbGoogle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbGoogle.Name = "gbGoogle";
            gbGoogle.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbGoogle.Size = new System.Drawing.Size(498, 158);
            gbGoogle.TabIndex = 3;
            gbGoogle.TabStop = false;
            gbGoogle.Text = "Google Information";
            // 
            // calendarIDLinkLabel
            // 
            calendarIDLinkLabel.AutoSize = true;
            calendarIDLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(54, 25);
            calendarIDLinkLabel.Location = new System.Drawing.Point(8, 136);
            calendarIDLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            calendarIDLinkLabel.Name = "calendarIDLinkLabel";
            calendarIDLinkLabel.Size = new System.Drawing.Size(436, 21);
            calendarIDLinkLabel.TabIndex = 12;
            calendarIDLinkLabel.TabStop = true;
            calendarIDLinkLabel.Text = "Tip: Leave Calendar ID blank to use default calendar. How to find a Calendar ID.";
            calendarIDLinkLabel.UseCompatibleTextRendering = true;
            calendarIDLinkLabel.LinkClicked += calendarIDLinkLabel_LinkClicked;
            // 
            // apiResponseLabel
            // 
            apiResponseLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            apiResponseLabel.Location = new System.Drawing.Point(7, 18);
            apiResponseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            apiResponseLabel.Name = "apiResponseLabel";
            apiResponseLabel.Size = new System.Drawing.Size(484, 28);
            apiResponseLabel.TabIndex = 11;
            apiResponseLabel.Text = "APIResponse";
            apiResponseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRevokeAuth
            // 
            btnRevokeAuth.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnRevokeAuth.Location = new System.Drawing.Point(405, 50);
            btnRevokeAuth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnRevokeAuth.Name = "btnRevokeAuth";
            btnRevokeAuth.Size = new System.Drawing.Size(88, 27);
            btnRevokeAuth.TabIndex = 9;
            btnRevokeAuth.Text = "Reset";
            btnRevokeAuth.UseVisualStyleBackColor = true;
            btnRevokeAuth.Click += btnRevokeAuth_Click;
            // 
            // btnRequestAuth
            // 
            btnRequestAuth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            btnRequestAuth.Location = new System.Drawing.Point(163, 50);
            btnRequestAuth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnRequestAuth.Name = "btnRequestAuth";
            btnRequestAuth.Size = new System.Drawing.Size(172, 27);
            btnRequestAuth.TabIndex = 8;
            btnRequestAuth.Text = "Request Authentication";
            btnRequestAuth.UseVisualStyleBackColor = true;
            btnRequestAuth.Click += btnRequestAuth_Click;
            // 
            // cbGoogleReminder
            // 
            cbGoogleReminder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbGoogleReminder.FormattingEnabled = true;
            cbGoogleReminder.Location = new System.Drawing.Point(91, 107);
            cbGoogleReminder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbGoogleReminder.Name = "cbGoogleReminder";
            cbGoogleReminder.Size = new System.Drawing.Size(107, 23);
            cbGoogleReminder.TabIndex = 7;
            // 
            // lblReminder
            // 
            lblReminder.AutoSize = true;
            lblReminder.Location = new System.Drawing.Point(7, 111);
            lblReminder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblReminder.Name = "lblReminder";
            lblReminder.Size = new System.Drawing.Size(61, 15);
            lblReminder.TabIndex = 6;
            lblReminder.Text = "Reminder:";
            // 
            // lblCalendarId
            // 
            lblCalendarId.AutoSize = true;
            lblCalendarId.Location = new System.Drawing.Point(7, 84);
            lblCalendarId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCalendarId.Name = "lblCalendarId";
            lblCalendarId.Size = new System.Drawing.Size(71, 15);
            lblCalendarId.TabIndex = 5;
            lblCalendarId.Text = "Calendar ID:";
            // 
            // tbGoogleCalendarName
            // 
            tbGoogleCalendarName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbGoogleCalendarName.Location = new System.Drawing.Point(91, 80);
            tbGoogleCalendarName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbGoogleCalendarName.Name = "tbGoogleCalendarName";
            tbGoogleCalendarName.Size = new System.Drawing.Size(400, 23);
            tbGoogleCalendarName.TabIndex = 5;
            tbGoogleCalendarName.Text = "primary";
            // 
            // throbber
            // 
            throbber.Location = new System.Drawing.Point(234, 18);
            throbber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            throbber.MaximumSize = new System.Drawing.Size(28, 28);
            throbber.MinimumSize = new System.Drawing.Size(28, 28);
            throbber.Name = "throbber";
            throbber.Size = new System.Drawing.Size(28, 28);
            throbber.State = Common.Enumerations.ThrobberState.Stopped;
            throbber.TabIndex = 13;
            throbber.TabStop = false;
            // 
            // gbMSOutlook
            // 
            gbMSOutlook.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbMSOutlook.Controls.Add(calendarPathExampleLabel);
            gbMSOutlook.Controls.Add(rbCustomCalendar);
            gbMSOutlook.Controls.Add(rbDefaultCalendar);
            gbMSOutlook.Controls.Add(tbOutlookCalendarPath);
            gbMSOutlook.Controls.Add(calendarPathLabel);
            gbMSOutlook.Location = new System.Drawing.Point(6, 36);
            gbMSOutlook.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbMSOutlook.Name = "gbMSOutlook";
            gbMSOutlook.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbMSOutlook.Size = new System.Drawing.Size(498, 158);
            gbMSOutlook.TabIndex = 14;
            gbMSOutlook.TabStop = false;
            gbMSOutlook.Text = "MS Outlook Information";
            // 
            // calendarPathExampleLabel
            // 
            calendarPathExampleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            calendarPathExampleLabel.AutoSize = true;
            calendarPathExampleLabel.Location = new System.Drawing.Point(7, 85);
            calendarPathExampleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            calendarPathExampleLabel.Name = "calendarPathExampleLabel";
            calendarPathExampleLabel.Size = new System.Drawing.Size(460, 60);
            calendarPathExampleLabel.TabIndex = 4;
            calendarPathExampleLabel.Text = resources.GetString("calendarPathExampleLabel.Text");
            // 
            // rbCustomCalendar
            // 
            rbCustomCalendar.AutoSize = true;
            rbCustomCalendar.Location = new System.Drawing.Point(164, 23);
            rbCustomCalendar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbCustomCalendar.Name = "rbCustomCalendar";
            rbCustomCalendar.Size = new System.Drawing.Size(139, 19);
            rbCustomCalendar.TabIndex = 3;
            rbCustomCalendar.TabStop = true;
            rbCustomCalendar.Text = "Use Custom Calendar";
            rbCustomCalendar.UseVisualStyleBackColor = true;
            rbCustomCalendar.Click += OnMustEnableOrDisable;
            // 
            // rbDefaultCalendar
            // 
            rbDefaultCalendar.AutoSize = true;
            rbDefaultCalendar.CausesValidation = false;
            rbDefaultCalendar.Location = new System.Drawing.Point(10, 23);
            rbDefaultCalendar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbDefaultCalendar.Name = "rbDefaultCalendar";
            rbDefaultCalendar.Size = new System.Drawing.Size(135, 19);
            rbDefaultCalendar.TabIndex = 2;
            rbDefaultCalendar.TabStop = true;
            rbDefaultCalendar.Text = "Use Default Calendar";
            rbDefaultCalendar.UseVisualStyleBackColor = true;
            rbDefaultCalendar.Click += OnMustEnableOrDisable;
            // 
            // tbOutlookCalendarPath
            // 
            tbOutlookCalendarPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbOutlookCalendarPath.Location = new System.Drawing.Point(111, 53);
            tbOutlookCalendarPath.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbOutlookCalendarPath.Name = "tbOutlookCalendarPath";
            tbOutlookCalendarPath.Size = new System.Drawing.Size(380, 23);
            tbOutlookCalendarPath.TabIndex = 1;
            tbOutlookCalendarPath.Validating += tbCalendarPath_Validating;
            // 
            // calendarPathLabel
            // 
            calendarPathLabel.AutoSize = true;
            calendarPathLabel.Location = new System.Drawing.Point(7, 57);
            calendarPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            calendarPathLabel.Name = "calendarPathLabel";
            calendarPathLabel.Size = new System.Drawing.Size(84, 15);
            calendarPathLabel.TabIndex = 0;
            calendarPathLabel.Text = "Calendar Path:";
            // 
            // ExternalCalendarControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(externalCalendarPanel);
            Enabled = false;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ExternalCalendarControl";
            Size = new System.Drawing.Size(512, 343);
            EnabledChanged += ExternalCalendarControl_EnabledChanged;
            externalCalendarPanel.ResumeLayout(false);
            externalCalendarPanel.PerformLayout();
            gbReminder.ResumeLayout(false);
            gbReminder.PerformLayout();
            gbGoogle.ResumeLayout(false);
            gbGoogle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)throbber).EndInit();
            gbMSOutlook.ResumeLayout(false);
            gbMSOutlook.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox gbReminder;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.CheckBox cbSetReminder;
        private System.Windows.Forms.Label lblLateReminder;
        private System.Windows.Forms.DateTimePicker dtpLateReminder;
        private System.Windows.Forms.Label lblEarlyReminder;
        private System.Windows.Forms.TextBox tbReminder;
        private System.Windows.Forms.CheckBox cbUseAlterateReminder;
        private System.Windows.Forms.DateTimePicker dtpEarlyReminder;
        private System.Windows.Forms.CheckBox cbLastQueuedSkillOnly;
        private System.Windows.Forms.RadioButton rbGoogle;
        private System.Windows.Forms.GroupBox gbGoogle;
        private System.Windows.Forms.ComboBox cbGoogleReminder;
        private System.Windows.Forms.Label lblReminder;
        private System.Windows.Forms.Label lblCalendarId;
        private System.Windows.Forms.TextBox tbGoogleCalendarName;
        private System.Windows.Forms.GroupBox gbMSOutlook;
        private System.Windows.Forms.Label calendarPathExampleLabel;
        private System.Windows.Forms.RadioButton rbCustomCalendar;
        private System.Windows.Forms.RadioButton rbDefaultCalendar;
        private System.Windows.Forms.TextBox tbOutlookCalendarPath;
        private System.Windows.Forms.Label calendarPathLabel;
        private System.Windows.Forms.Panel externalCalendarPanel;
        private System.Windows.Forms.Button btnRevokeAuth;
        private System.Windows.Forms.Button btnRequestAuth;
        private System.Windows.Forms.Label apiResponseLabel;
        private System.Windows.Forms.LinkLabel calendarIDLinkLabel;
        private Common.Controls.Throbber throbber;
    }
}
