using EVEMon.Common.Controls;
using EVEMon.Common.Controls.MultiPanel;
using EVEMon.Common.Helpers;
using System;

namespace EVEMon.ApiCredentialsManagement
{
    partial class EsiKeyUpdateOrAdditionWindow
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
            try
            {
                m_server.Dispose();
            }
            catch (Exception ex)
            {
                // Do not rethrow while disposing
                ExceptionHandler.LogException(ex, true);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EsiKeyUpdateOrAdditionWindow));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Mary Jane");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Ali Baba");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("John Doe");
			this.GuideLabel = new System.Windows.Forms.Label();
			this.ButtonNext = new System.Windows.Forms.Button();
			this.ButtonPrevious = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.MultiPanel = new EVEMon.Common.Controls.MultiPanel.MultiPanel();
			this.CredentialsPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.Throbber = new EVEMon.Common.Controls.Throbber();
			this.ButtonESILogin = new System.Windows.Forms.Button();
			this.ResultPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.KeyTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.KeyLabel = new System.Windows.Forms.Label();
			this.KeyPicture = new System.Windows.Forms.PictureBox();
			this.CharactersGroupBox = new System.Windows.Forms.GroupBox();
			this.ResultsMultiPanel = new EVEMon.Common.Controls.MultiPanel.MultiPanel();
			this.CharactersListPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.CharactersListView = new System.Windows.Forms.ListView();
			this.WarningLabel = new System.Windows.Forms.Label();
			this.AuthenticationErrorPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.AuthenticationErrorGuideLabel = new System.Windows.Forms.Label();
			this.LoginDeniedErrorPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.LoginDeniedLinkLabel = new System.Windows.Forms.LinkLabel();
			this.GeneralErrorPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.GeneralErrorLabel = new System.Windows.Forms.Label();
			this.ESITokenFailedErrorPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.ESITokenFailedLabel = new System.Windows.Forms.Label();
			this.CachedWarningPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.CachedWarningLabel = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.LabelShiftClick = new System.Windows.Forms.Label();
			this.MultiPanel.SuspendLayout();
			this.CredentialsPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Throbber)).BeginInit();
			this.ResultPage.SuspendLayout();
			this.KeyTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.KeyPicture)).BeginInit();
			this.CharactersGroupBox.SuspendLayout();
			this.ResultsMultiPanel.SuspendLayout();
			this.CharactersListPage.SuspendLayout();
			this.AuthenticationErrorPage.SuspendLayout();
			this.LoginDeniedErrorPage.SuspendLayout();
			this.GeneralErrorPage.SuspendLayout();
			this.ESITokenFailedErrorPage.SuspendLayout();
			this.CachedWarningPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// GuideLabel
			// 
			this.GuideLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GuideLabel.Location = new System.Drawing.Point(3, 3);
			this.GuideLabel.Name = "GuideLabel";
			this.GuideLabel.Size = new System.Drawing.Size(267, 79);
			this.GuideLabel.TabIndex = 3;
			this.GuideLabel.Text = "Ensure that the character shown is the correct character to import.\r\n\r\nCharacters" +
    " can be imported and hidden later through the ESI keys management window.";
			// 
			// ButtonNext
			// 
			this.ButtonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonNext.Location = new System.Drawing.Point(337, 177);
			this.ButtonNext.Name = "ButtonNext";
			this.ButtonNext.Size = new System.Drawing.Size(75, 23);
			this.ButtonNext.TabIndex = 1;
			this.ButtonNext.Text = resources.GetString("this.ButtonNext.Text");
			this.ButtonNext.UseVisualStyleBackColor = true;
			this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
			// 
			// ButtonPrevious
			// 
			this.ButtonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonPrevious.Enabled = false;
			this.ButtonPrevious.Location = new System.Drawing.Point(256, 177);
			this.ButtonPrevious.Name = "ButtonPrevious";
			this.ButtonPrevious.Size = new System.Drawing.Size(75, 23);
			this.ButtonPrevious.TabIndex = 0;
			this.ButtonPrevious.Text = resources.GetString("this.ButtonPrevious.Text");
			this.ButtonPrevious.UseVisualStyleBackColor = true;
			this.ButtonPrevious.Click += new System.EventHandler(this.ButtonPrevious_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.CausesValidation = false;
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(435, 177);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 2;
			this.ButtonCancel.Text = resources.GetString("this.ButtonCancel.Text");
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// MultiPanel
			// 
			this.MultiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MultiPanel.Controls.Add(this.CredentialsPage);
			this.MultiPanel.Controls.Add(this.ResultPage);
			this.MultiPanel.Location = new System.Drawing.Point(0, 0);
			this.MultiPanel.Name = "MultiPanel";
			this.MultiPanel.SelectedPage = this.CredentialsPage;
			this.MultiPanel.Size = new System.Drawing.Size(522, 171);
			this.MultiPanel.TabIndex = 0;
			// 
			// CredentialsPage
			// 
			this.CredentialsPage.CausesValidation = false;
			this.CredentialsPage.Controls.Add(this.LabelShiftClick);
			this.CredentialsPage.Controls.Add(this.Throbber);
			this.CredentialsPage.Controls.Add(this.ButtonESILogin);
			this.CredentialsPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CredentialsPage.Location = new System.Drawing.Point(0, 0);
			this.CredentialsPage.Name = "CredentialsPage";
			this.CredentialsPage.Size = new System.Drawing.Size(522, 171);
			this.CredentialsPage.TabIndex = 0;
			this.CredentialsPage.Text = resources.GetString("this.CredentialsPage.Text");
			// 
			// Throbber
			// 
			this.Throbber.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.Throbber.Location = new System.Drawing.Point(249, 76);
			this.Throbber.MaximumSize = new System.Drawing.Size(24, 24);
			this.Throbber.MinimumSize = new System.Drawing.Size(24, 24);
			this.Throbber.Name = "Throbber";
			this.Throbber.Size = new System.Drawing.Size(24, 24);
			this.Throbber.State = EVEMon.Common.Enumerations.ThrobberState.Stopped;
			this.Throbber.TabIndex = 1;
			this.Throbber.TabStop = false;
			this.Throbber.Visible = false;
			// 
			// ButtonESILogin
			// 
			this.ButtonESILogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.ButtonESILogin.Image = ((System.Drawing.Image)(resources.GetObject("ButtonESILogin.Image")));
			this.ButtonESILogin.Location = new System.Drawing.Point(126, 12);
			this.ButtonESILogin.Name = "ButtonESILogin";
			this.ButtonESILogin.Size = new System.Drawing.Size(270, 45);
			this.ButtonESILogin.TabIndex = 0;
			this.ButtonESILogin.UseVisualStyleBackColor = true;
			this.ButtonESILogin.Click += new System.EventHandler(this.ButtonESILogin_Click);
			// 
			// ResultPage
			// 
			this.ResultPage.Controls.Add(this.KeyTableLayoutPanel);
			this.ResultPage.Controls.Add(this.CharactersGroupBox);
			this.ResultPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResultPage.Location = new System.Drawing.Point(0, 0);
			this.ResultPage.Name = "ResultPage";
			this.ResultPage.Size = new System.Drawing.Size(522, 171);
			this.ResultPage.TabIndex = 2;
			this.ResultPage.Text = resources.GetString("this.ResultPage.Text");
			// 
			// KeyTableLayoutPanel
			// 
			this.KeyTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.KeyTableLayoutPanel.AutoSize = true;
			this.KeyTableLayoutPanel.ColumnCount = 2;
			this.KeyTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.141962F));
			this.KeyTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.85804F));
			this.KeyTableLayoutPanel.Controls.Add(this.KeyLabel, 1, 0);
			this.KeyTableLayoutPanel.Controls.Add(this.KeyPicture, 0, 0);
			this.KeyTableLayoutPanel.Location = new System.Drawing.Point(12, 12);
			this.KeyTableLayoutPanel.Name = "KeyTableLayoutPanel";
			this.KeyTableLayoutPanel.RowCount = 1;
			this.KeyTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.KeyTableLayoutPanel.Size = new System.Drawing.Size(498, 38);
			this.KeyTableLayoutPanel.TabIndex = 0;
			// 
			// KeyLabel
			// 
			this.KeyLabel.AutoSize = true;
			this.KeyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.KeyLabel.Location = new System.Drawing.Point(43, 0);
			this.KeyLabel.Name = "KeyLabel";
			this.KeyLabel.Size = new System.Drawing.Size(452, 38);
			this.KeyLabel.TabIndex = 1;
			this.KeyLabel.Text = resources.GetString("this.KeyLabel.Text");
			this.KeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// KeyPicture
			// 
			this.KeyPicture.Image = ((System.Drawing.Image)(resources.GetObject("KeyPicture.Image")));
			this.KeyPicture.Location = new System.Drawing.Point(3, 3);
			this.KeyPicture.Name = "KeyPicture";
			this.KeyPicture.Size = new System.Drawing.Size(32, 32);
			this.KeyPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.KeyPicture.TabIndex = 0;
			this.KeyPicture.TabStop = false;
			// 
			// CharactersGroupBox
			// 
			this.CharactersGroupBox.Controls.Add(this.ResultsMultiPanel);
			this.CharactersGroupBox.Location = new System.Drawing.Point(12, 50);
			this.CharactersGroupBox.Name = "CharactersGroupBox";
			this.CharactersGroupBox.Size = new System.Drawing.Size(479, 118);
			this.CharactersGroupBox.TabIndex = 3;
			this.CharactersGroupBox.TabStop = false;
			this.CharactersGroupBox.Text = resources.GetString("this.CharactersGroupBox.Text");
			// 
			// ResultsMultiPanel
			// 
			this.ResultsMultiPanel.Controls.Add(this.CharactersListPage);
			this.ResultsMultiPanel.Controls.Add(this.AuthenticationErrorPage);
			this.ResultsMultiPanel.Controls.Add(this.LoginDeniedErrorPage);
			this.ResultsMultiPanel.Controls.Add(this.GeneralErrorPage);
			this.ResultsMultiPanel.Controls.Add(this.ESITokenFailedErrorPage);
			this.ResultsMultiPanel.Controls.Add(this.CachedWarningPage);
			this.ResultsMultiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResultsMultiPanel.Location = new System.Drawing.Point(3, 16);
			this.ResultsMultiPanel.Name = "ResultsMultiPanel";
			this.ResultsMultiPanel.SelectedPage = this.ESITokenFailedErrorPage;
			this.ResultsMultiPanel.Size = new System.Drawing.Size(473, 99);
			this.ResultsMultiPanel.TabIndex = 5;
			// 
			// CharactersListPage
			// 
			this.CharactersListPage.Controls.Add(this.CharactersListView);
			this.CharactersListPage.Controls.Add(this.GuideLabel);
			this.CharactersListPage.Controls.Add(this.WarningLabel);
			this.CharactersListPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CharactersListPage.Location = new System.Drawing.Point(0, 0);
			this.CharactersListPage.Name = "CharactersListPage";
			this.CharactersListPage.Size = new System.Drawing.Size(473, 99);
			this.CharactersListPage.TabIndex = 0;
			this.CharactersListPage.Text = resources.GetString("this.CharactersListPage.Text");
			// 
			// CharactersListView
			// 
			this.CharactersListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.CharactersListView.FullRowSelect = true;
			this.CharactersListView.HideSelection = false;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			listViewItem3.Checked = true;
			listViewItem3.StateImageIndex = 1;
			this.CharactersListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
			this.CharactersListView.Location = new System.Drawing.Point(276, 3);
			this.CharactersListView.Name = "CharactersListView";
			this.CharactersListView.Size = new System.Drawing.Size(197, 76);
			this.CharactersListView.TabIndex = 2;
			this.CharactersListView.UseCompatibleStateImageBehavior = false;
			this.CharactersListView.View = System.Windows.Forms.View.List;
			// 
			// WarningLabel
			// 
			this.WarningLabel.AutoSize = true;
			this.WarningLabel.ForeColor = System.Drawing.Color.DarkRed;
			this.WarningLabel.Location = new System.Drawing.Point(3, 82);
			this.WarningLabel.Name = "WarningLabel";
			this.WarningLabel.Size = new System.Drawing.Size(402, 13);
			this.WarningLabel.TabIndex = 4;
			this.WarningLabel.Text = "Beware! When you remove characters, all their data and plans will be definitely l" +
    "ost !";
			// 
			// AuthenticationErrorPage
			// 
			this.AuthenticationErrorPage.Controls.Add(this.AuthenticationErrorGuideLabel);
			this.AuthenticationErrorPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AuthenticationErrorPage.Location = new System.Drawing.Point(0, 0);
			this.AuthenticationErrorPage.Name = "AuthenticationErrorPage";
			this.AuthenticationErrorPage.Size = new System.Drawing.Size(473, 99);
			this.AuthenticationErrorPage.TabIndex = 1;
			this.AuthenticationErrorPage.Text = resources.GetString("this.AuthenticationErrorPage.Text");
			// 
			// AuthenticationErrorGuideLabel
			// 
			this.AuthenticationErrorGuideLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AuthenticationErrorGuideLabel.Location = new System.Drawing.Point(0, 0);
			this.AuthenticationErrorGuideLabel.Name = "AuthenticationErrorGuideLabel";
			this.AuthenticationErrorGuideLabel.Padding = new System.Windows.Forms.Padding(54, 0, 0, 0);
			this.AuthenticationErrorGuideLabel.Size = new System.Drawing.Size(473, 99);
			this.AuthenticationErrorGuideLabel.TabIndex = 0;
			this.AuthenticationErrorGuideLabel.Text = resources.GetString("AuthenticationErrorGuideLabel.Text");
			this.AuthenticationErrorGuideLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LoginDeniedErrorPage
			// 
			this.LoginDeniedErrorPage.Controls.Add(this.LoginDeniedLinkLabel);
			this.LoginDeniedErrorPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LoginDeniedErrorPage.Location = new System.Drawing.Point(0, 0);
			this.LoginDeniedErrorPage.Name = "LoginDeniedErrorPage";
			this.LoginDeniedErrorPage.Size = new System.Drawing.Size(473, 99);
			this.LoginDeniedErrorPage.TabIndex = 2;
			this.LoginDeniedErrorPage.Text = resources.GetString("this.LoginDeniedErrorPage.Text");
			// 
			// LoginDeniedLinkLabel
			// 
			this.LoginDeniedLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LoginDeniedLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(149, 49);
			this.LoginDeniedLinkLabel.Location = new System.Drawing.Point(0, 0);
			this.LoginDeniedLinkLabel.Name = "LoginDeniedLinkLabel";
			this.LoginDeniedLinkLabel.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
			this.LoginDeniedLinkLabel.Size = new System.Drawing.Size(473, 99);
			this.LoginDeniedLinkLabel.TabIndex = 0;
			this.LoginDeniedLinkLabel.TabStop = true;
			this.LoginDeniedLinkLabel.Text = resources.GetString("LoginDeniedLinkLabel.Text");
			this.LoginDeniedLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LoginDeniedLinkLabel.UseCompatibleTextRendering = true;
			this.LoginDeniedLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoginDeniedLinkLabel_LinkClicked);
			// 
			// GeneralErrorPage
			// 
			this.GeneralErrorPage.Controls.Add(this.GeneralErrorLabel);
			this.GeneralErrorPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GeneralErrorPage.Location = new System.Drawing.Point(0, 0);
			this.GeneralErrorPage.Name = "GeneralErrorPage";
			this.GeneralErrorPage.Size = new System.Drawing.Size(473, 99);
			this.GeneralErrorPage.TabIndex = 3;
			this.GeneralErrorPage.Text = resources.GetString("this.GeneralErrorPage.Text");
			// 
			// GeneralErrorLabel
			// 
			this.GeneralErrorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GeneralErrorLabel.Location = new System.Drawing.Point(0, 0);
			this.GeneralErrorLabel.Name = "GeneralErrorLabel";
			this.GeneralErrorLabel.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
			this.GeneralErrorLabel.Size = new System.Drawing.Size(473, 99);
			this.GeneralErrorLabel.TabIndex = 0;
			this.GeneralErrorLabel.Text = "An error occurred while retrieving the information.\r\n\r\nThe error message was: {0}" +
    "";
			this.GeneralErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ESITokenFailedErrorPage
			// 
			this.ESITokenFailedErrorPage.Controls.Add(this.ESITokenFailedLabel);
			this.ESITokenFailedErrorPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ESITokenFailedErrorPage.Location = new System.Drawing.Point(0, 0);
			this.ESITokenFailedErrorPage.Name = "ESITokenFailedErrorPage";
			this.ESITokenFailedErrorPage.Size = new System.Drawing.Size(473, 99);
			this.ESITokenFailedErrorPage.TabIndex = 5;
			this.ESITokenFailedErrorPage.Text = "";
			// 
			// ESITokenFailedLabel
			// 
			this.ESITokenFailedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ESITokenFailedLabel.Location = new System.Drawing.Point(0, 0);
			this.ESITokenFailedLabel.Name = "ESITokenFailedLabel";
			this.ESITokenFailedLabel.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
			this.ESITokenFailedLabel.Size = new System.Drawing.Size(473, 99);
			this.ESITokenFailedLabel.TabIndex = 0;
			this.ESITokenFailedLabel.Text = "EVEMon did not receive a valid response from the CCP SSO server.\r\n\r\nTry again in " +
    "a few minutes.";
			this.ESITokenFailedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CachedWarningPage
			// 
			this.CachedWarningPage.Controls.Add(this.CachedWarningLabel);
			this.CachedWarningPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CachedWarningPage.Location = new System.Drawing.Point(0, 0);
			this.CachedWarningPage.Name = "CachedWarningPage";
			this.CachedWarningPage.Size = new System.Drawing.Size(473, 98);
			this.CachedWarningPage.TabIndex = 6;
			this.CachedWarningPage.Text = resources.GetString("this.CachedWarningPage.Text");
			// 
			// CachedWarningLabel
			// 
			this.CachedWarningLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CachedWarningLabel.Location = new System.Drawing.Point(0, 0);
			this.CachedWarningLabel.Name = "CachedWarningLabel";
			this.CachedWarningLabel.Size = new System.Drawing.Size(473, 98);
			this.CachedWarningLabel.TabIndex = 0;
			this.CachedWarningLabel.Text = "Due to the fact that the cached timer has not yet expired,\r\nyour query attempt wi" +
    "ll result in getting the same data you already have.\r\nTry again after: {0}";
			this.CachedWarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// LabelShiftClick
			// 
			this.LabelShiftClick.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.LabelShiftClick.AutoSize = true;
			this.LabelShiftClick.Location = new System.Drawing.Point(111, 60);
			this.LabelShiftClick.Name = "LabelShiftClick";
			this.LabelShiftClick.Size = new System.Drawing.Size(301, 13);
			this.LabelShiftClick.TabIndex = 2;
			this.LabelShiftClick.Text = resources.GetString("this.LabelShiftClick.Text");
			this.LabelShiftClick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// EsiKeyUpdateOrAdditionWindow
			// 
			this.AcceptButton = this.ButtonNext;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(522, 212);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonPrevious);
			this.Controls.Add(this.ButtonNext);
			this.Controls.Add(this.MultiPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EsiKeyUpdateOrAdditionWindow";
			this.Text = resources.GetString("this.Text");
			this.MultiPanel.ResumeLayout(false);
			this.CredentialsPage.ResumeLayout(false);
			this.CredentialsPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Throbber)).EndInit();
			this.ResultPage.ResumeLayout(false);
			this.ResultPage.PerformLayout();
			this.KeyTableLayoutPanel.ResumeLayout(false);
			this.KeyTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.KeyPicture)).EndInit();
			this.CharactersGroupBox.ResumeLayout(false);
			this.ResultsMultiPanel.ResumeLayout(false);
			this.CharactersListPage.ResumeLayout(false);
			this.CharactersListPage.PerformLayout();
			this.AuthenticationErrorPage.ResumeLayout(false);
			this.LoginDeniedErrorPage.ResumeLayout(false);
			this.GeneralErrorPage.ResumeLayout(false);
			this.ESITokenFailedErrorPage.ResumeLayout(false);
			this.CachedWarningPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private MultiPanel MultiPanel;
        private MultiPanelPage CredentialsPage;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.Button ButtonPrevious;
        private System.Windows.Forms.Button ButtonCancel;
        private MultiPanelPage ResultPage;
        private System.Windows.Forms.PictureBox KeyPicture;
        private System.Windows.Forms.GroupBox CharactersGroupBox;
        private System.Windows.Forms.ListView CharactersListView;
        private System.Windows.Forms.Label WarningLabel;
        private System.Windows.Forms.TableLayoutPanel KeyTableLayoutPanel;
        private System.Windows.Forms.Label KeyLabel;
        private MultiPanel ResultsMultiPanel;
        private MultiPanelPage CharactersListPage;
        private MultiPanelPage AuthenticationErrorPage;
        private System.Windows.Forms.Label AuthenticationErrorGuideLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private MultiPanelPage LoginDeniedErrorPage;
        private System.Windows.Forms.LinkLabel LoginDeniedLinkLabel;
        private MultiPanelPage GeneralErrorPage;
        private System.Windows.Forms.Label GeneralErrorLabel;
        private MultiPanelPage ESITokenFailedErrorPage;
        private System.Windows.Forms.Label ESITokenFailedLabel;
        private MultiPanelPage CachedWarningPage;
        private System.Windows.Forms.Label CachedWarningLabel;
        private System.Windows.Forms.Label GuideLabel;
        private System.Windows.Forms.Button ButtonESILogin;
        private Throbber Throbber;
		private System.Windows.Forms.Label LabelShiftClick;
	}
}
