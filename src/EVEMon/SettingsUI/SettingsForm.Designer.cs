using System;
using EVEMon.Common.Controls.MultiPanel;

namespace EVEMon.SettingsUI
{
    partial class SettingsForm
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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Updates", 11, 11);
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Network", 7, 7);
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Portable EVE Clients", 15, 15);
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Market Price Providers", 16, 16);
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("General", 10, 10, new System.Windows.Forms.TreeNode[] { treeNode16, treeNode17, treeNode18, treeNode19 });
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Main Window", 6, 6);
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Icons", 13, 13);
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Messages", 14, 14);
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Skill Planner", 8, 8, new System.Windows.Forms.TreeNode[] { treeNode22, treeNode23 });
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("System Tray Icon", 2, 2);
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("External Calendar", 5, 5);
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Scheduler", 1, 1, new System.Windows.Forms.TreeNode[] { treeNode26 });
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Skill Completion Mails", 12, 12);
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Notifications", 9, 9, new System.Windows.Forms.TreeNode[] { treeNode28 });
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Cloud Storage Service", 17, 17);
            systemTrayIconGroupBox = new System.Windows.Forms.GroupBox();
            rbSystemTrayOptionsNever = new System.Windows.Forms.RadioButton();
            rbSystemTrayOptionsAlways = new System.Windows.Forms.RadioButton();
            rbSystemTrayOptionsMinimized = new System.Windows.Forms.RadioButton();
            bottomPanel = new System.Windows.Forms.Panel();
            applyButton = new System.Windows.Forms.Button();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            lblMainWindowPage = new System.Windows.Forms.Label();
            lblSize = new System.Windows.Forms.Label();
            CharacterMonitorGroupBox = new System.Windows.Forms.GroupBox();
            nudSkillQueueWarningThresholdDays = new System.Windows.Forms.NumericUpDown();
            lblSkillQueueWarningThresholdDays = new System.Windows.Forms.Label();
            lblSkillQueuWarningThreshold = new System.Windows.Forms.Label();
            cbColorQueuedSkills = new System.Windows.Forms.CheckBox();
            cbShowPrereqMetSkills = new System.Windows.Forms.CheckBox();
            cbColorPartialSkills = new System.Windows.Forms.CheckBox();
            cbAlwaysShowSkillQueueTime = new System.Windows.Forms.CheckBox();
            cbShowNonPublicSkills = new System.Windows.Forms.CheckBox();
            cbShowAllPublicSkills = new System.Windows.Forms.CheckBox();
            WindowTitleGroupBox = new System.Windows.Forms.GroupBox();
            cbWindowsTitleList = new System.Windows.Forms.ComboBox();
            cbSkillInTitle = new System.Windows.Forms.CheckBox();
            cbTitleToTime = new System.Windows.Forms.CheckBox();
            lblGeneralPage = new System.Windows.Forms.Label();
            lblEnvironment = new System.Windows.Forms.Label();
            lblSkillPlannerPage = new System.Windows.Forms.Label();
            lblNetworkPageProxy = new System.Windows.Forms.Label();
            lblProxyHostIPAddress = new System.Windows.Forms.Label();
            lblProxyPort = new System.Windows.Forms.Label();
            lblHTTP = new System.Windows.Forms.Label();
            lblEmailNotificationPage = new System.Windows.Forms.Label();
            lblNotificationsPage = new System.Windows.Forms.Label();
            lblTrayIconPage = new System.Windows.Forms.Label();
            lblSchedulerUIPage = new System.Windows.Forms.Label();
            lblText = new System.Windows.Forms.Label();
            lblBlockingEvents = new System.Windows.Forms.Label();
            lblRecurringEvents = new System.Windows.Forms.Label();
            lblSimpleEvents = new System.Windows.Forms.Label();
            lblExternalCalendarPage = new System.Windows.Forms.Label();
            lblIconsPage = new System.Windows.Forms.Label();
            gbSkillBrowserIconSet = new System.Windows.Forms.GroupBox();
            iconsSetTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            cbSkillIconSet = new System.Windows.Forms.ComboBox();
            tvlist = new System.Windows.Forms.TreeView();
            lblObsoletePlanEntries = new System.Windows.Forms.Label();
            ttToolTipCodes = new System.Windows.Forms.ToolTip(components);
            cbUseIncreasedContrastOnOverview = new System.Windows.Forms.CheckBox();
            overviewGroupCharactersInTrainingCheckBox = new System.Windows.Forms.CheckBox();
            overviewShowSkillQueueTrainingTimeCheckBox = new System.Windows.Forms.CheckBox();
            overviewShowWalletCheckBox = new System.Windows.Forms.CheckBox();
            overviewShowPortraitCheckBox = new System.Windows.Forms.CheckBox();
            cbShowOverViewTab = new System.Windows.Forms.CheckBox();
            colorDialog = new System.Windows.Forms.ColorDialog();
            treeView = new System.Windows.Forms.TreeView();
            imageList = new System.Windows.Forms.ImageList(components);
            leftPanel = new System.Windows.Forms.Panel();
            multiPanel = new MultiPanel();
            generalPage = new MultiPanelPage();
            btnEVEMonDataDir = new System.Windows.Forms.Button();
            cbWorksafeMode = new System.Windows.Forms.CheckBox();
            compatibilityCombo = new System.Windows.Forms.ComboBox();
            runAtStartupComboBox = new System.Windows.Forms.CheckBox();
            mainWindowPage = new MultiPanelPage();
            OverviewGroupBox = new System.Windows.Forms.GroupBox();
            overviewPanel = new System.Windows.Forms.Panel();
            extraInfoComboBox = new System.Windows.Forms.ComboBox();
            cbShowSkillpointsOnOverview = new System.Windows.Forms.CheckBox();
            overviewPortraitSizeComboBox = new System.Windows.Forms.ComboBox();
            skillPlannerPage = new MultiPanelPage();
            cbAdvanceEntryAdd = new System.Windows.Forms.CheckBox();
            cbSummaryOnMultiSelectOnly = new System.Windows.Forms.CheckBox();
            cbHighlightQueuedSiklls = new System.Windows.Forms.CheckBox();
            cbHighlightPartialSkills = new System.Windows.Forms.CheckBox();
            cbHighlightConflicts = new System.Windows.Forms.CheckBox();
            cbHighlightPrerequisites = new System.Windows.Forms.CheckBox();
            cbHighlightPlannedSkills = new System.Windows.Forms.CheckBox();
            networkPage = new MultiPanelPage();
            esiSettingsGroupBox = new System.Windows.Forms.GroupBox();
            lblClientSecret = new System.Windows.Forms.Label();
            lblClientID = new System.Windows.Forms.Label();
            esiSettingsLabel = new System.Windows.Forms.LinkLabel();
            clientSecretTextBox = new System.Windows.Forms.TextBox();
            clientIDTextBox = new System.Windows.Forms.TextBox();
            ProxyServerGroupBox = new System.Windows.Forms.GroupBox();
            customProxyCheckBox = new System.Windows.Forms.CheckBox();
            customProxyPanel = new System.Windows.Forms.Panel();
            proxyPortTextBox = new System.Windows.Forms.TextBox();
            proxyAuthenticationButton = new System.Windows.Forms.Button();
            proxyHttpHostTextBox = new System.Windows.Forms.TextBox();
            emailNotificationsPage = new MultiPanelPage();
            mailNotificationCheckBox = new System.Windows.Forms.CheckBox();
            emailNotificationsControl = new EmailNotificationsControl();
            notificationsPage = new MultiPanelPage();
            cbPlaySoundOnSkillComplete = new System.Windows.Forms.CheckBox();
            notificationsControl = new NotificationsControl();
            trayIconPage = new MultiPanelPage();
            mainWindowBehaviourGroupBox = new System.Windows.Forms.GroupBox();
            rbMinToTaskBar = new System.Windows.Forms.RadioButton();
            rbMinToTray = new System.Windows.Forms.RadioButton();
            rbExitEVEMon = new System.Windows.Forms.RadioButton();
            trayIconPopupGroupBox = new System.Windows.Forms.GroupBox();
            trayPopupDisabledRadio = new System.Windows.Forms.RadioButton();
            trayPopupButton = new System.Windows.Forms.Button();
            trayPopupRadio = new System.Windows.Forms.RadioButton();
            trayTooltipRadio = new System.Windows.Forms.RadioButton();
            trayTooltipButton = new System.Windows.Forms.Button();
            updatesPage = new MultiPanelPage();
            updateSettingsControl = new UpdateSettingsControl();
            lblUpdatesPage = new System.Windows.Forms.Label();
            cbCheckTime = new System.Windows.Forms.CheckBox();
            cbCheckForUpdates = new System.Windows.Forms.CheckBox();
            schedulerUIPage = new MultiPanelPage();
            panelColorText = new System.Windows.Forms.Panel();
            panelColorRecurring2 = new System.Windows.Forms.Panel();
            panelColorRecurring1 = new System.Windows.Forms.Panel();
            panelColorSingle2 = new System.Windows.Forms.Panel();
            panelColorSingle1 = new System.Windows.Forms.Panel();
            panelColorBlocking = new System.Windows.Forms.Panel();
            externalCalendarPage = new MultiPanelPage();
            externalCalendarControl = new ExternalCalendarControl();
            externalCalendarCheckbox = new System.Windows.Forms.CheckBox();
            iconsPage = new MultiPanelPage();
            messagesPage = new MultiPanelPage();
            gbMessageBox = new System.Windows.Forms.GroupBox();
            lblPrioritesConflict = new System.Windows.Forms.Label();
            btnPrioritiesReset = new System.Windows.Forms.Button();
            ObsoleteEntryRemovalGroupBox = new System.Windows.Forms.GroupBox();
            RemoveAllLabel = new System.Windows.Forms.Label();
            AlwaysAskLabel = new System.Windows.Forms.Label();
            RemoveConfirmedLabel = new System.Windows.Forms.Label();
            alwaysAskRadioButton = new System.Windows.Forms.RadioButton();
            removeAllRadioButton = new System.Windows.Forms.RadioButton();
            removeConfirmedRadioButton = new System.Windows.Forms.RadioButton();
            portableEveClientsPage = new MultiPanelPage();
            lblPECIDescription = new System.Windows.Forms.Label();
            PECIGroupBox = new System.Windows.Forms.GroupBox();
            portableEveClientsControl = new PortableEveClientsControl();
            marketPriceProvidersPage = new MultiPanelPage();
            gbMarketPriceProviders = new System.Windows.Forms.GroupBox();
            cbProvidersList = new System.Windows.Forms.ComboBox();
            SelectedProviderLabel = new System.Windows.Forms.Label();
            marketPriceProviderPageLabel = new System.Windows.Forms.Label();
            cloudStorageServicePage = new MultiPanelPage();
            providerAuthenticationGroupBox = new System.Windows.Forms.GroupBox();
            cloudStorageServiceControl = new CloudStorageServiceControl();
            cloudStorageGroupBox = new System.Windows.Forms.GroupBox();
            cloudStorageProviderLogoPictureBox = new System.Windows.Forms.PictureBox();
            cloudStorageProvidersComboBox = new System.Windows.Forms.ComboBox();
            lblSelectedProvider = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            settingsFileStorageGroupBox = new System.Windows.Forms.GroupBox();
            settingsFileStorageControl = new SettingsFileStorageControl();
            systemTrayIconGroupBox.SuspendLayout();
            bottomPanel.SuspendLayout();
            CharacterMonitorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSkillQueueWarningThresholdDays).BeginInit();
            WindowTitleGroupBox.SuspendLayout();
            gbSkillBrowserIconSet.SuspendLayout();
            iconsSetTableLayoutPanel.SuspendLayout();
            leftPanel.SuspendLayout();
            multiPanel.SuspendLayout();
            generalPage.SuspendLayout();
            mainWindowPage.SuspendLayout();
            OverviewGroupBox.SuspendLayout();
            overviewPanel.SuspendLayout();
            skillPlannerPage.SuspendLayout();
            networkPage.SuspendLayout();
            esiSettingsGroupBox.SuspendLayout();
            ProxyServerGroupBox.SuspendLayout();
            customProxyPanel.SuspendLayout();
            emailNotificationsPage.SuspendLayout();
            notificationsPage.SuspendLayout();
            trayIconPage.SuspendLayout();
            mainWindowBehaviourGroupBox.SuspendLayout();
            trayIconPopupGroupBox.SuspendLayout();
            updatesPage.SuspendLayout();
            schedulerUIPage.SuspendLayout();
            externalCalendarPage.SuspendLayout();
            iconsPage.SuspendLayout();
            messagesPage.SuspendLayout();
            gbMessageBox.SuspendLayout();
            ObsoleteEntryRemovalGroupBox.SuspendLayout();
            portableEveClientsPage.SuspendLayout();
            PECIGroupBox.SuspendLayout();
            marketPriceProvidersPage.SuspendLayout();
            gbMarketPriceProviders.SuspendLayout();
            cloudStorageServicePage.SuspendLayout();
            providerAuthenticationGroupBox.SuspendLayout();
            cloudStorageGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cloudStorageProviderLogoPictureBox).BeginInit();
            settingsFileStorageGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // systemTrayIconGroupBox
            // 
            systemTrayIconGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            systemTrayIconGroupBox.Controls.Add(rbSystemTrayOptionsNever);
            systemTrayIconGroupBox.Controls.Add(rbSystemTrayOptionsAlways);
            systemTrayIconGroupBox.Controls.Add(rbSystemTrayOptionsMinimized);
            systemTrayIconGroupBox.Location = new System.Drawing.Point(10, 78);
            systemTrayIconGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            systemTrayIconGroupBox.Name = "systemTrayIconGroupBox";
            systemTrayIconGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            systemTrayIconGroupBox.Size = new System.Drawing.Size(489, 115);
            systemTrayIconGroupBox.TabIndex = 9;
            systemTrayIconGroupBox.TabStop = false;
            systemTrayIconGroupBox.Text = "Show System Tray Icon";
            // 
            // rbSystemTrayOptionsNever
            // 
            rbSystemTrayOptionsNever.AutoSize = true;
            rbSystemTrayOptionsNever.Location = new System.Drawing.Point(7, 23);
            rbSystemTrayOptionsNever.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbSystemTrayOptionsNever.Name = "rbSystemTrayOptionsNever";
            rbSystemTrayOptionsNever.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            rbSystemTrayOptionsNever.Size = new System.Drawing.Size(60, 19);
            rbSystemTrayOptionsNever.TabIndex = 1;
            rbSystemTrayOptionsNever.TabStop = true;
            rbSystemTrayOptionsNever.Tag = "";
            rbSystemTrayOptionsNever.Text = "Never";
            rbSystemTrayOptionsNever.UseVisualStyleBackColor = true;
            rbSystemTrayOptionsNever.CheckedChanged += OnMustEnableOrDisable;
            // 
            // rbSystemTrayOptionsAlways
            // 
            rbSystemTrayOptionsAlways.AutoSize = true;
            rbSystemTrayOptionsAlways.Location = new System.Drawing.Point(7, 76);
            rbSystemTrayOptionsAlways.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbSystemTrayOptionsAlways.Name = "rbSystemTrayOptionsAlways";
            rbSystemTrayOptionsAlways.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            rbSystemTrayOptionsAlways.Size = new System.Drawing.Size(66, 19);
            rbSystemTrayOptionsAlways.TabIndex = 3;
            rbSystemTrayOptionsAlways.TabStop = true;
            rbSystemTrayOptionsAlways.Tag = "";
            rbSystemTrayOptionsAlways.Text = "Always";
            rbSystemTrayOptionsAlways.UseVisualStyleBackColor = true;
            // 
            // rbSystemTrayOptionsMinimized
            // 
            rbSystemTrayOptionsMinimized.AutoSize = true;
            rbSystemTrayOptionsMinimized.Location = new System.Drawing.Point(7, 50);
            rbSystemTrayOptionsMinimized.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbSystemTrayOptionsMinimized.Name = "rbSystemTrayOptionsMinimized";
            rbSystemTrayOptionsMinimized.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            rbSystemTrayOptionsMinimized.Size = new System.Drawing.Size(119, 19);
            rbSystemTrayOptionsMinimized.TabIndex = 2;
            rbSystemTrayOptionsMinimized.TabStop = true;
            rbSystemTrayOptionsMinimized.Tag = "";
            rbSystemTrayOptionsMinimized.Text = "When Minimized";
            rbSystemTrayOptionsMinimized.UseVisualStyleBackColor = true;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            bottomPanel.Controls.Add(applyButton);
            bottomPanel.Controls.Add(okButton);
            bottomPanel.Controls.Add(cancelButton);
            bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            bottomPanel.Location = new System.Drawing.Point(0, 503);
            bottomPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new System.Drawing.Size(751, 53);
            bottomPanel.TabIndex = 8;
            // 
            // applyButton
            // 
            applyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            applyButton.Location = new System.Drawing.Point(650, 13);
            applyButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            applyButton.Name = "applyButton";
            applyButton.Size = new System.Drawing.Size(88, 27);
            applyButton.TabIndex = 4;
            applyButton.Text = "&Apply";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // okButton
            // 
            okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            okButton.Location = new System.Drawing.Point(461, 13);
            okButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(88, 27);
            okButton.TabIndex = 3;
            okButton.Text = "&OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += btnOk_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(555, 13);
            cancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(88, 27);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "&Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += btnCancel_Click;
            // 
            // lblMainWindowPage
            // 
            lblMainWindowPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblMainWindowPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblMainWindowPage.Location = new System.Drawing.Point(5, 14);
            lblMainWindowPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMainWindowPage.Name = "lblMainWindowPage";
            lblMainWindowPage.Size = new System.Drawing.Size(495, 51);
            lblMainWindowPage.TabIndex = 19;
            lblMainWindowPage.Text = resources.GetString("lblMainWindowPage.Text");
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new System.Drawing.Point(23, 60);
            lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSize.Name = "lblSize";
            lblSize.Size = new System.Drawing.Size(27, 15);
            lblSize.TabIndex = 31;
            lblSize.Text = "Size";
            // 
            // CharacterMonitorGroupBox
            // 
            CharacterMonitorGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CharacterMonitorGroupBox.Controls.Add(nudSkillQueueWarningThresholdDays);
            CharacterMonitorGroupBox.Controls.Add(lblSkillQueueWarningThresholdDays);
            CharacterMonitorGroupBox.Controls.Add(lblSkillQueuWarningThreshold);
            CharacterMonitorGroupBox.Controls.Add(cbColorQueuedSkills);
            CharacterMonitorGroupBox.Controls.Add(cbShowPrereqMetSkills);
            CharacterMonitorGroupBox.Controls.Add(cbColorPartialSkills);
            CharacterMonitorGroupBox.Controls.Add(cbAlwaysShowSkillQueueTime);
            CharacterMonitorGroupBox.Controls.Add(cbShowNonPublicSkills);
            CharacterMonitorGroupBox.Controls.Add(cbShowAllPublicSkills);
            CharacterMonitorGroupBox.Location = new System.Drawing.Point(4, 186);
            CharacterMonitorGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CharacterMonitorGroupBox.Name = "CharacterMonitorGroupBox";
            CharacterMonitorGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CharacterMonitorGroupBox.Size = new System.Drawing.Size(497, 145);
            CharacterMonitorGroupBox.TabIndex = 7;
            CharacterMonitorGroupBox.TabStop = false;
            CharacterMonitorGroupBox.Text = "Character Monitor";
            // 
            // nudSkillQueueWarningThresholdDays
            // 
            nudSkillQueueWarningThresholdDays.Location = new System.Drawing.Point(76, 117);
            nudSkillQueueWarningThresholdDays.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            nudSkillQueueWarningThresholdDays.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudSkillQueueWarningThresholdDays.Name = "nudSkillQueueWarningThresholdDays";
            nudSkillQueueWarningThresholdDays.Size = new System.Drawing.Size(38, 23);
            nudSkillQueueWarningThresholdDays.TabIndex = 16;
            nudSkillQueueWarningThresholdDays.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblSkillQueueWarningThresholdDays
            // 
            lblSkillQueueWarningThresholdDays.AutoSize = true;
            lblSkillQueueWarningThresholdDays.Location = new System.Drawing.Point(26, 119);
            lblSkillQueueWarningThresholdDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSkillQueueWarningThresholdDays.Name = "lblSkillQueueWarningThresholdDays";
            lblSkillQueueWarningThresholdDays.Size = new System.Drawing.Size(38, 15);
            lblSkillQueueWarningThresholdDays.TabIndex = 15;
            lblSkillQueueWarningThresholdDays.Text = "Days :";
            // 
            // lblSkillQueuWarningThreshold
            // 
            lblSkillQueuWarningThreshold.AutoSize = true;
            lblSkillQueuWarningThreshold.Location = new System.Drawing.Point(10, 97);
            lblSkillQueuWarningThreshold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSkillQueuWarningThreshold.Name = "lblSkillQueuWarningThreshold";
            lblSkillQueuWarningThreshold.Size = new System.Drawing.Size(169, 15);
            lblSkillQueuWarningThreshold.TabIndex = 14;
            lblSkillQueuWarningThreshold.Text = "Skill Queue Warning Threshold";
            // 
            // cbColorQueuedSkills
            // 
            cbColorQueuedSkills.AutoSize = true;
            cbColorQueuedSkills.Location = new System.Drawing.Point(219, 44);
            cbColorQueuedSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbColorQueuedSkills.Name = "cbColorQueuedSkills";
            cbColorQueuedSkills.Size = new System.Drawing.Size(150, 19);
            cbColorQueuedSkills.TabIndex = 13;
            cbColorQueuedSkills.Text = "Highlight Queued Skills";
            ttToolTipCodes.SetToolTip(cbColorQueuedSkills, "When enabled, highlights all\r\nqueued skills in character's skill list");
            cbColorQueuedSkills.UseVisualStyleBackColor = true;
            // 
            // cbShowPrereqMetSkills
            // 
            cbShowPrereqMetSkills.AutoSize = true;
            cbShowPrereqMetSkills.Location = new System.Drawing.Point(18, 66);
            cbShowPrereqMetSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbShowPrereqMetSkills.Name = "cbShowPrereqMetSkills";
            cbShowPrereqMetSkills.Size = new System.Drawing.Size(173, 19);
            cbShowPrereqMetSkills.TabIndex = 12;
            cbShowPrereqMetSkills.Text = "Show Also Prereq-Met Skills";
            ttToolTipCodes.SetToolTip(cbShowPrereqMetSkills, "When enabled, shows all prerequisites\r\nmet skills in character's skill list");
            cbShowPrereqMetSkills.UseVisualStyleBackColor = true;
            // 
            // cbColorPartialSkills
            // 
            cbColorPartialSkills.AutoSize = true;
            cbColorPartialSkills.Location = new System.Drawing.Point(219, 23);
            cbColorPartialSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbColorPartialSkills.Name = "cbColorPartialSkills";
            cbColorPartialSkills.Size = new System.Drawing.Size(191, 19);
            cbColorPartialSkills.TabIndex = 11;
            cbColorPartialSkills.Text = "Highlight Partially Trained Skills";
            ttToolTipCodes.SetToolTip(cbColorPartialSkills, "When enabled, highlights all partially\r\ntrained skills in character's skill list");
            cbColorPartialSkills.UseVisualStyleBackColor = true;
            // 
            // cbAlwaysShowSkillQueueTime
            // 
            cbAlwaysShowSkillQueueTime.AutoSize = true;
            cbAlwaysShowSkillQueueTime.Location = new System.Drawing.Point(219, 66);
            cbAlwaysShowSkillQueueTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbAlwaysShowSkillQueueTime.Name = "cbAlwaysShowSkillQueueTime";
            cbAlwaysShowSkillQueueTime.Size = new System.Drawing.Size(235, 19);
            cbAlwaysShowSkillQueueTime.TabIndex = 2;
            cbAlwaysShowSkillQueueTime.Text = "Always show time above the skill queue";
            ttToolTipCodes.SetToolTip(cbAlwaysShowSkillQueueTime, "When enabled, always displays the total\r\nqueue time above the skill queue bar");
            cbAlwaysShowSkillQueueTime.UseVisualStyleBackColor = true;
            // 
            // cbShowNonPublicSkills
            // 
            cbShowNonPublicSkills.AutoSize = true;
            cbShowNonPublicSkills.Enabled = false;
            cbShowNonPublicSkills.Location = new System.Drawing.Point(18, 44);
            cbShowNonPublicSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbShowNonPublicSkills.Name = "cbShowNonPublicSkills";
            cbShowNonPublicSkills.Size = new System.Drawing.Size(174, 19);
            cbShowNonPublicSkills.TabIndex = 1;
            cbShowNonPublicSkills.Text = "Show Also Non-Public Skills";
            ttToolTipCodes.SetToolTip(cbShowNonPublicSkills, "When enabled, shows all non-public skills in character's skill list");
            cbShowNonPublicSkills.UseVisualStyleBackColor = true;
            // 
            // cbShowAllPublicSkills
            // 
            cbShowAllPublicSkills.AutoSize = true;
            cbShowAllPublicSkills.Location = new System.Drawing.Point(18, 23);
            cbShowAllPublicSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbShowAllPublicSkills.Name = "cbShowAllPublicSkills";
            cbShowAllPublicSkills.Size = new System.Drawing.Size(163, 19);
            cbShowAllPublicSkills.TabIndex = 0;
            cbShowAllPublicSkills.Text = "Show Also All Public Skills";
            ttToolTipCodes.SetToolTip(cbShowAllPublicSkills, "When enabled, shows all public skills in character's skill list");
            cbShowAllPublicSkills.UseVisualStyleBackColor = true;
            cbShowAllPublicSkills.CheckedChanged += OnMustEnableOrDisable;
            // 
            // WindowTitleGroupBox
            // 
            WindowTitleGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            WindowTitleGroupBox.Controls.Add(cbWindowsTitleList);
            WindowTitleGroupBox.Controls.Add(cbSkillInTitle);
            WindowTitleGroupBox.Controls.Add(cbTitleToTime);
            WindowTitleGroupBox.Location = new System.Drawing.Point(4, 68);
            WindowTitleGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            WindowTitleGroupBox.Name = "WindowTitleGroupBox";
            WindowTitleGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            WindowTitleGroupBox.Size = new System.Drawing.Size(497, 111);
            WindowTitleGroupBox.TabIndex = 14;
            WindowTitleGroupBox.TabStop = false;
            WindowTitleGroupBox.Text = "Window Title";
            // 
            // cbWindowsTitleList
            // 
            cbWindowsTitleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbWindowsTitleList.FormattingEnabled = true;
            cbWindowsTitleList.Items.AddRange(new object[] { "Single character - finishing skill next", "Single character - selected character", "Multi character - finishing skill next first", "Multi character - selected character first " });
            cbWindowsTitleList.Location = new System.Drawing.Point(18, 76);
            cbWindowsTitleList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbWindowsTitleList.Name = "cbWindowsTitleList";
            cbWindowsTitleList.Size = new System.Drawing.Size(261, 23);
            cbWindowsTitleList.TabIndex = 1;
            // 
            // cbSkillInTitle
            // 
            cbSkillInTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cbSkillInTitle.AutoSize = true;
            cbSkillInTitle.Location = new System.Drawing.Point(18, 50);
            cbSkillInTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbSkillInTitle.Name = "cbSkillInTitle";
            cbSkillInTitle.Size = new System.Drawing.Size(135, 19);
            cbSkillInTitle.TabIndex = 7;
            cbSkillInTitle.Text = "Show skill in training";
            ttToolTipCodes.SetToolTip(cbSkillInTitle, "When enabled, shows the character's skill\r\nin training according to choice below");
            cbSkillInTitle.UseVisualStyleBackColor = true;
            // 
            // cbTitleToTime
            // 
            cbTitleToTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cbTitleToTime.AutoSize = true;
            cbTitleToTime.Location = new System.Drawing.Point(18, 23);
            cbTitleToTime.Margin = new System.Windows.Forms.Padding(14, 3, 4, 3);
            cbTitleToTime.Name = "cbTitleToTime";
            cbTitleToTime.Size = new System.Drawing.Size(212, 19);
            cbTitleToTime.TabIndex = 6;
            cbTitleToTime.Text = "Show character info in window title";
            ttToolTipCodes.SetToolTip(cbTitleToTime, "When enabled, shows the character's info in window title");
            cbTitleToTime.UseVisualStyleBackColor = true;
            cbTitleToTime.CheckedChanged += OnMustEnableOrDisable;
            // 
            // lblGeneralPage
            // 
            lblGeneralPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblGeneralPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblGeneralPage.Location = new System.Drawing.Point(5, 27);
            lblGeneralPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblGeneralPage.Name = "lblGeneralPage";
            lblGeneralPage.Size = new System.Drawing.Size(495, 48);
            lblGeneralPage.TabIndex = 20;
            lblGeneralPage.Text = resources.GetString("lblGeneralPage.Text");
            // 
            // lblEnvironment
            // 
            lblEnvironment.AutoSize = true;
            lblEnvironment.Location = new System.Drawing.Point(4, 209);
            lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblEnvironment.Name = "lblEnvironment";
            lblEnvironment.Size = new System.Drawing.Size(236, 15);
            lblEnvironment.TabIndex = 1;
            lblEnvironment.Text = "Environment (requires restart to take effect)";
            // 
            // lblSkillPlannerPage
            // 
            lblSkillPlannerPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblSkillPlannerPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblSkillPlannerPage.Location = new System.Drawing.Point(5, 23);
            lblSkillPlannerPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSkillPlannerPage.Name = "lblSkillPlannerPage";
            lblSkillPlannerPage.Size = new System.Drawing.Size(495, 32);
            lblSkillPlannerPage.TabIndex = 19;
            lblSkillPlannerPage.Text = "You can select whether to highlight any entry in the Skill Planner according to its status and more.";
            // 
            // lblNetworkPageProxy
            // 
            lblNetworkPageProxy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblNetworkPageProxy.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblNetworkPageProxy.Location = new System.Drawing.Point(10, 20);
            lblNetworkPageProxy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblNetworkPageProxy.Name = "lblNetworkPageProxy";
            lblNetworkPageProxy.Size = new System.Drawing.Size(465, 37);
            lblNetworkPageProxy.TabIndex = 8;
            lblNetworkPageProxy.Text = "By default, EVEMon will use the same Proxy settings as Internet Explorer (can be configured through the Control Panel).";
            // 
            // lblProxyHostIPAddress
            // 
            lblProxyHostIPAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblProxyHostIPAddress.Location = new System.Drawing.Point(58, 9);
            lblProxyHostIPAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblProxyHostIPAddress.Name = "lblProxyHostIPAddress";
            lblProxyHostIPAddress.Size = new System.Drawing.Size(212, 15);
            lblProxyHostIPAddress.TabIndex = 3;
            lblProxyHostIPAddress.Text = "Host/IP Address";
            lblProxyHostIPAddress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProxyPort
            // 
            lblProxyPort.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblProxyPort.Location = new System.Drawing.Point(258, 9);
            lblProxyPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblProxyPort.Name = "lblProxyPort";
            lblProxyPort.Size = new System.Drawing.Size(65, 15);
            lblProxyPort.TabIndex = 4;
            lblProxyPort.Text = "Port";
            lblProxyPort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblHTTP
            // 
            lblHTTP.AutoSize = true;
            lblHTTP.Location = new System.Drawing.Point(9, 31);
            lblHTTP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblHTTP.Name = "lblHTTP";
            lblHTTP.Size = new System.Drawing.Size(38, 15);
            lblHTTP.TabIndex = 0;
            lblHTTP.Text = "HTTP:";
            lblHTTP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmailNotificationPage
            // 
            lblEmailNotificationPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblEmailNotificationPage.AutoSize = true;
            lblEmailNotificationPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblEmailNotificationPage.Location = new System.Drawing.Point(5, 23);
            lblEmailNotificationPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblEmailNotificationPage.Name = "lblEmailNotificationPage";
            lblEmailNotificationPage.Size = new System.Drawing.Size(405, 15);
            lblEmailNotificationPage.TabIndex = 19;
            lblEmailNotificationPage.Text = "EVEMon can send you an email whenever a skill level completes its training.";
            // 
            // lblNotificationsPage
            // 
            lblNotificationsPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblNotificationsPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblNotificationsPage.Location = new System.Drawing.Point(5, 16);
            lblNotificationsPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblNotificationsPage.Name = "lblNotificationsPage";
            lblNotificationsPage.Size = new System.Drawing.Size(495, 50);
            lblNotificationsPage.TabIndex = 19;
            lblNotificationsPage.Text = "You can choose what notifications will be shown in your system's tray notification area or in EVEMon's main window and when. You can also toggle the sound notification upon skill completion on or off.";
            // 
            // lblTrayIconPage
            // 
            lblTrayIconPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblTrayIconPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblTrayIconPage.Location = new System.Drawing.Point(5, 23);
            lblTrayIconPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblTrayIconPage.Name = "lblTrayIconPage";
            lblTrayIconPage.Size = new System.Drawing.Size(495, 36);
            lblTrayIconPage.TabIndex = 18;
            lblTrayIconPage.Text = "Here you can set the visible status of EVEMon's Tray Icon, configure the style of the Tray Icon's popup info and EVEMon's behaviour upon pressing the Close button.\r\n";
            // 
            // lblSchedulerUIPage
            // 
            lblSchedulerUIPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblSchedulerUIPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblSchedulerUIPage.Location = new System.Drawing.Point(7, 39);
            lblSchedulerUIPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSchedulerUIPage.Name = "lblSchedulerUIPage";
            lblSchedulerUIPage.Size = new System.Drawing.Size(492, 52);
            lblSchedulerUIPage.TabIndex = 6;
            lblSchedulerUIPage.Text = "Select the colors used in the scheduler. Using the scheduler, EVEMon can warn you about skill that will complete at times you will be away from your computer.";
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Location = new System.Drawing.Point(7, 126);
            lblText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblText.Name = "lblText";
            lblText.Size = new System.Drawing.Size(31, 15);
            lblText.TabIndex = 5;
            lblText.Text = "Text:";
            // 
            // lblBlockingEvents
            // 
            lblBlockingEvents.AutoSize = true;
            lblBlockingEvents.Location = new System.Drawing.Point(7, 152);
            lblBlockingEvents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblBlockingEvents.Name = "lblBlockingEvents";
            lblBlockingEvents.Size = new System.Drawing.Size(93, 15);
            lblBlockingEvents.TabIndex = 0;
            lblBlockingEvents.Text = "Blocking Events:";
            // 
            // lblRecurringEvents
            // 
            lblRecurringEvents.AutoSize = true;
            lblRecurringEvents.Location = new System.Drawing.Point(7, 208);
            lblRecurringEvents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblRecurringEvents.Name = "lblRecurringEvents";
            lblRecurringEvents.Size = new System.Drawing.Size(98, 15);
            lblRecurringEvents.TabIndex = 1;
            lblRecurringEvents.Text = "Recurring Events:";
            // 
            // lblSimpleEvents
            // 
            lblSimpleEvents.AutoSize = true;
            lblSimpleEvents.Location = new System.Drawing.Point(7, 181);
            lblSimpleEvents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSimpleEvents.Name = "lblSimpleEvents";
            lblSimpleEvents.Size = new System.Drawing.Size(83, 15);
            lblSimpleEvents.TabIndex = 2;
            lblSimpleEvents.Text = "Simple Events:";
            // 
            // lblExternalCalendarPage
            // 
            lblExternalCalendarPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblExternalCalendarPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblExternalCalendarPage.Location = new System.Drawing.Point(2, 20);
            lblExternalCalendarPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblExternalCalendarPage.Name = "lblExternalCalendarPage";
            lblExternalCalendarPage.Size = new System.Drawing.Size(500, 83);
            lblExternalCalendarPage.TabIndex = 11;
            lblExternalCalendarPage.Text = resources.GetString("lblExternalCalendarPage.Text");
            // 
            // lblIconsPage
            // 
            lblIconsPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblIconsPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblIconsPage.Location = new System.Drawing.Point(5, 23);
            lblIconsPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblIconsPage.Name = "lblIconsPage";
            lblIconsPage.Size = new System.Drawing.Size(495, 37);
            lblIconsPage.TabIndex = 15;
            lblIconsPage.Text = "You can customize the icons used in the skill planner; if you have a good idea for a set of icons instructions to create your own can be found on wiki.";
            // 
            // gbSkillBrowserIconSet
            // 
            gbSkillBrowserIconSet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbSkillBrowserIconSet.Controls.Add(iconsSetTableLayoutPanel);
            gbSkillBrowserIconSet.Location = new System.Drawing.Point(8, 63);
            gbSkillBrowserIconSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbSkillBrowserIconSet.Name = "gbSkillBrowserIconSet";
            gbSkillBrowserIconSet.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbSkillBrowserIconSet.Size = new System.Drawing.Size(262, 235);
            gbSkillBrowserIconSet.TabIndex = 14;
            gbSkillBrowserIconSet.TabStop = false;
            gbSkillBrowserIconSet.Text = "Skill Browser Icon Set";
            // 
            // iconsSetTableLayoutPanel
            // 
            iconsSetTableLayoutPanel.AutoSize = true;
            iconsSetTableLayoutPanel.ColumnCount = 1;
            iconsSetTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            iconsSetTableLayoutPanel.Controls.Add(cbSkillIconSet, 0, 0);
            iconsSetTableLayoutPanel.Controls.Add(tvlist, 0, 1);
            iconsSetTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            iconsSetTableLayoutPanel.Location = new System.Drawing.Point(4, 19);
            iconsSetTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            iconsSetTableLayoutPanel.Name = "iconsSetTableLayoutPanel";
            iconsSetTableLayoutPanel.RowCount = 2;
            iconsSetTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            iconsSetTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            iconsSetTableLayoutPanel.Size = new System.Drawing.Size(254, 213);
            iconsSetTableLayoutPanel.TabIndex = 15;
            // 
            // cbSkillIconSet
            // 
            cbSkillIconSet.Dock = System.Windows.Forms.DockStyle.Fill;
            cbSkillIconSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbSkillIconSet.FormattingEnabled = true;
            cbSkillIconSet.Location = new System.Drawing.Point(4, 3);
            cbSkillIconSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbSkillIconSet.Name = "cbSkillIconSet";
            cbSkillIconSet.Size = new System.Drawing.Size(246, 23);
            cbSkillIconSet.TabIndex = 3;
            cbSkillIconSet.SelectedIndexChanged += skillIconSetComboBox_SelectedIndexChanged;
            // 
            // tvlist
            // 
            tvlist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tvlist.Location = new System.Drawing.Point(4, 32);
            tvlist.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tvlist.Name = "tvlist";
            tvlist.Size = new System.Drawing.Size(246, 178);
            tvlist.TabIndex = 9;
            // 
            // lblObsoletePlanEntries
            // 
            lblObsoletePlanEntries.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblObsoletePlanEntries.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblObsoletePlanEntries.Location = new System.Drawing.Point(6, 35);
            lblObsoletePlanEntries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblObsoletePlanEntries.Name = "lblObsoletePlanEntries";
            lblObsoletePlanEntries.Size = new System.Drawing.Size(495, 32);
            lblObsoletePlanEntries.TabIndex = 24;
            lblObsoletePlanEntries.Text = "You can configure how EVEMon handles skills that appear to be completed and reset the appearing messages behavior.";
            // 
            // cbUseIncreasedContrastOnOverview
            // 
            cbUseIncreasedContrastOnOverview.AutoSize = true;
            cbUseIncreasedContrastOnOverview.Location = new System.Drawing.Point(188, 43);
            cbUseIncreasedContrastOnOverview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbUseIncreasedContrastOnOverview.Name = "cbUseIncreasedContrastOnOverview";
            cbUseIncreasedContrastOnOverview.Size = new System.Drawing.Size(146, 19);
            cbUseIncreasedContrastOnOverview.TabIndex = 34;
            cbUseIncreasedContrastOnOverview.Text = "Use Increased Contrast";
            ttToolTipCodes.SetToolTip(cbUseIncreasedContrastOnOverview, "When enabled, increases the contrast of the shown info");
            cbUseIncreasedContrastOnOverview.UseVisualStyleBackColor = true;
            // 
            // overviewGroupCharactersInTrainingCheckBox
            // 
            overviewGroupCharactersInTrainingCheckBox.AutoSize = true;
            overviewGroupCharactersInTrainingCheckBox.Location = new System.Drawing.Point(188, 63);
            overviewGroupCharactersInTrainingCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            overviewGroupCharactersInTrainingCheckBox.Name = "overviewGroupCharactersInTrainingCheckBox";
            overviewGroupCharactersInTrainingCheckBox.Size = new System.Drawing.Size(176, 19);
            overviewGroupCharactersInTrainingCheckBox.TabIndex = 33;
            overviewGroupCharactersInTrainingCheckBox.Text = "Group Characters In Training";
            ttToolTipCodes.SetToolTip(overviewGroupCharactersInTrainingCheckBox, "When enabled, groups the character's\r\nthat are currently in training");
            overviewGroupCharactersInTrainingCheckBox.UseVisualStyleBackColor = true;
            // 
            // overviewShowSkillQueueTrainingTimeCheckBox
            // 
            overviewShowSkillQueueTrainingTimeCheckBox.AutoSize = true;
            overviewShowSkillQueueTrainingTimeCheckBox.Location = new System.Drawing.Point(188, 3);
            overviewShowSkillQueueTrainingTimeCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            overviewShowSkillQueueTrainingTimeCheckBox.Name = "overviewShowSkillQueueTrainingTimeCheckBox";
            overviewShowSkillQueueTrainingTimeCheckBox.Size = new System.Drawing.Size(191, 19);
            overviewShowSkillQueueTrainingTimeCheckBox.TabIndex = 32;
            overviewShowSkillQueueTrainingTimeCheckBox.Text = "Show Skill Queue Training Time";
            ttToolTipCodes.SetToolTip(overviewShowSkillQueueTrainingTimeCheckBox, "When enabled, shows the character's\r\nskill queue training time");
            overviewShowSkillQueueTrainingTimeCheckBox.UseVisualStyleBackColor = true;
            // 
            // overviewShowWalletCheckBox
            // 
            overviewShowWalletCheckBox.AutoSize = true;
            overviewShowWalletCheckBox.Location = new System.Drawing.Point(4, 3);
            overviewShowWalletCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            overviewShowWalletCheckBox.Name = "overviewShowWalletCheckBox";
            overviewShowWalletCheckBox.Size = new System.Drawing.Size(135, 19);
            overviewShowWalletCheckBox.TabIndex = 30;
            overviewShowWalletCheckBox.Text = "Show Wallet Balance";
            ttToolTipCodes.SetToolTip(overviewShowWalletCheckBox, "When enabled, shows the character's wallet balance");
            overviewShowWalletCheckBox.UseVisualStyleBackColor = true;
            // 
            // overviewShowPortraitCheckBox
            // 
            overviewShowPortraitCheckBox.AutoSize = true;
            overviewShowPortraitCheckBox.Location = new System.Drawing.Point(4, 23);
            overviewShowPortraitCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            overviewShowPortraitCheckBox.Name = "overviewShowPortraitCheckBox";
            overviewShowPortraitCheckBox.Size = new System.Drawing.Size(151, 19);
            overviewShowPortraitCheckBox.TabIndex = 26;
            overviewShowPortraitCheckBox.Text = "Show Character Portrait";
            ttToolTipCodes.SetToolTip(overviewShowPortraitCheckBox, "When enabled, shows the character's portrait\r\nas a thumbnail alongside the character's name");
            overviewShowPortraitCheckBox.UseVisualStyleBackColor = true;
            // 
            // cbShowOverViewTab
            // 
            cbShowOverViewTab.AutoSize = true;
            cbShowOverViewTab.Location = new System.Drawing.Point(16, 20);
            cbShowOverViewTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbShowOverViewTab.Name = "cbShowOverViewTab";
            cbShowOverViewTab.Size = new System.Drawing.Size(137, 19);
            cbShowOverViewTab.TabIndex = 0;
            cbShowOverViewTab.Text = "Show \"Overview\" tab";
            ttToolTipCodes.SetToolTip(cbShowOverViewTab, "When enabled, shows the Overview tab");
            cbShowOverViewTab.UseVisualStyleBackColor = true;
            cbShowOverViewTab.CheckedChanged += OnMustEnableOrDisable;
            // 
            // treeView
            // 
            treeView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView.FullRowSelect = true;
            treeView.HideSelection = false;
            treeView.ImageIndex = 0;
            treeView.ImageList = imageList;
            treeView.ItemHeight = 20;
            treeView.Location = new System.Drawing.Point(7, 7);
            treeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            treeView.Name = "treeView";
            treeNode16.ImageIndex = 11;
            treeNode16.Name = "UpdatesNode";
            treeNode16.SelectedImageIndex = 11;
            treeNode16.Tag = "updatesPage";
            treeNode16.Text = "Updates";
            treeNode17.ImageIndex = 7;
            treeNode17.Name = "networkNode";
            treeNode17.SelectedImageIndex = 7;
            treeNode17.Tag = "networkPage";
            treeNode17.Text = "Network";
            treeNode18.ImageIndex = 15;
            treeNode18.Name = "PortableEveClientsNode";
            treeNode18.SelectedImageIndex = 15;
            treeNode18.Tag = "portableEveClientsPage";
            treeNode18.Text = "Portable EVE Clients";
            treeNode19.ImageIndex = 16;
            treeNode19.Name = "MarketPriceProvidersNode";
            treeNode19.SelectedImageIndex = 16;
            treeNode19.Tag = "marketPriceProvidersPage";
            treeNode19.Text = "Market Price Providers";
            treeNode20.ImageIndex = 10;
            treeNode20.Name = "generalNode";
            treeNode20.SelectedImageIndex = 10;
            treeNode20.Tag = "generalPage";
            treeNode20.Text = "General";
            treeNode21.ImageIndex = 6;
            treeNode21.Name = "Node3";
            treeNode21.SelectedImageIndex = 6;
            treeNode21.Tag = "mainWindowPage";
            treeNode21.Text = "Main Window";
            treeNode22.ImageIndex = 13;
            treeNode22.Name = "IconsNode";
            treeNode22.SelectedImageIndex = 13;
            treeNode22.Tag = "iconsPage";
            treeNode22.Text = "Icons";
            treeNode23.ImageIndex = 14;
            treeNode23.Name = "MassagesNode";
            treeNode23.SelectedImageIndex = 14;
            treeNode23.Tag = "messagesPage";
            treeNode23.Text = "Messages";
            treeNode24.ImageIndex = 8;
            treeNode24.Name = "Node4";
            treeNode24.SelectedImageIndex = 8;
            treeNode24.Tag = "skillPlannerPage";
            treeNode24.Text = "Skill Planner";
            treeNode25.ImageIndex = 2;
            treeNode25.Name = "trayIconNode";
            treeNode25.SelectedImageIndex = 2;
            treeNode25.Tag = "trayIconPage";
            treeNode25.Text = "System Tray Icon";
            treeNode26.ImageIndex = 5;
            treeNode26.Name = "Node11";
            treeNode26.SelectedImageIndex = 5;
            treeNode26.Tag = "externalCalendarPage";
            treeNode26.Text = "External Calendar";
            treeNode27.ImageIndex = 1;
            treeNode27.Name = "Node10";
            treeNode27.SelectedImageIndex = 1;
            treeNode27.Tag = "schedulerUIPage";
            treeNode27.Text = "Scheduler";
            treeNode28.ImageIndex = 12;
            treeNode28.Name = "Node7";
            treeNode28.SelectedImageIndex = 12;
            treeNode28.Tag = "emailNotificationsPage";
            treeNode28.Text = "Skill Completion Mails";
            treeNode29.ImageIndex = 9;
            treeNode29.Name = "Node2";
            treeNode29.SelectedImageIndex = 9;
            treeNode29.Tag = "notificationsPage";
            treeNode29.Text = "Notifications";
            treeNode30.ImageIndex = 17;
            treeNode30.Name = "CloudStorageServiceNode";
            treeNode30.SelectedImageIndex = 17;
            treeNode30.Tag = "cloudStorageServicePage";
            treeNode30.Text = "Cloud Storage Service";
            treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode20, treeNode21, treeNode24, treeNode25, treeNode27, treeNode29, treeNode30 });
            treeView.SelectedImageIndex = 0;
            treeView.ShowLines = false;
            treeView.ShowPlusMinus = false;
            treeView.ShowRootLines = false;
            treeView.Size = new System.Drawing.Size(218, 489);
            treeView.TabIndex = 6;
            treeView.AfterSelect += treeView_AfterSelect;
            // 
            // imageList
            // 
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = System.Drawing.Color.Empty;
            imageList.Images.SetKeyName(0, "Transparent");
            imageList.Images.SetKeyName(1, "Calendar");
            imageList.Images.SetKeyName(2, "EVEMon");
            imageList.Images.SetKeyName(3, "IGB");
            imageList.Images.SetKeyName(4, "LogitechKeyboard");
            imageList.Images.SetKeyName(5, "gcalendar");
            imageList.Images.SetKeyName(6, "MainWindow");
            imageList.Images.SetKeyName(7, "Network");
            imageList.Images.SetKeyName(8, "Plan");
            imageList.Images.SetKeyName(9, "Problem");
            imageList.Images.SetKeyName(10, "Settings");
            imageList.Images.SetKeyName(11, "Update");
            imageList.Images.SetKeyName(12, "Email");
            imageList.Images.SetKeyName(13, "book");
            imageList.Images.SetKeyName(14, "messagebox");
            imageList.Images.SetKeyName(15, "EveClient");
            imageList.Images.SetKeyName(16, "Wallet");
            imageList.Images.SetKeyName(17, "CloudStorage");
            // 
            // leftPanel
            // 
            leftPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            leftPanel.Controls.Add(treeView);
            leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            leftPanel.Location = new System.Drawing.Point(0, 0);
            leftPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            leftPanel.Name = "leftPanel";
            leftPanel.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            leftPanel.Size = new System.Drawing.Size(232, 503);
            leftPanel.TabIndex = 9;
            // 
            // multiPanel
            // 
            multiPanel.CausesValidation = false;
            multiPanel.Controls.Add(generalPage);
            multiPanel.Controls.Add(mainWindowPage);
            multiPanel.Controls.Add(skillPlannerPage);
            multiPanel.Controls.Add(networkPage);
            multiPanel.Controls.Add(emailNotificationsPage);
            multiPanel.Controls.Add(notificationsPage);
            multiPanel.Controls.Add(trayIconPage);
            multiPanel.Controls.Add(updatesPage);
            multiPanel.Controls.Add(schedulerUIPage);
            multiPanel.Controls.Add(externalCalendarPage);
            multiPanel.Controls.Add(iconsPage);
            multiPanel.Controls.Add(messagesPage);
            multiPanel.Controls.Add(portableEveClientsPage);
            multiPanel.Controls.Add(marketPriceProvidersPage);
            multiPanel.Controls.Add(cloudStorageServicePage);
            multiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            multiPanel.Location = new System.Drawing.Point(232, 0);
            multiPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            multiPanel.Name = "multiPanel";
            multiPanel.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            multiPanel.SelectedPage = externalCalendarPage;
            multiPanel.Size = new System.Drawing.Size(519, 503);
            multiPanel.TabIndex = 7;
            // 
            // generalPage
            // 
            generalPage.Controls.Add(btnEVEMonDataDir);
            generalPage.Controls.Add(lblGeneralPage);
            generalPage.Controls.Add(cbWorksafeMode);
            generalPage.Controls.Add(compatibilityCombo);
            generalPage.Controls.Add(lblEnvironment);
            generalPage.Controls.Add(runAtStartupComboBox);
            generalPage.Dock = System.Windows.Forms.DockStyle.Fill;
            generalPage.Location = new System.Drawing.Point(6, 6);
            generalPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            generalPage.Name = "generalPage";
            generalPage.Size = new System.Drawing.Size(507, 491);
            generalPage.TabIndex = 0;
            generalPage.Text = "generalPage";
            // 
            // btnEVEMonDataDir
            // 
            btnEVEMonDataDir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnEVEMonDataDir.AutoSize = true;
            btnEVEMonDataDir.Location = new System.Drawing.Point(337, 449);
            btnEVEMonDataDir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnEVEMonDataDir.Name = "btnEVEMonDataDir";
            btnEVEMonDataDir.Size = new System.Drawing.Size(162, 29);
            btnEVEMonDataDir.TabIndex = 21;
            btnEVEMonDataDir.Text = "EVEMon Data Directory";
            btnEVEMonDataDir.UseVisualStyleBackColor = true;
            btnEVEMonDataDir.Click += btnEVEMonDataDir_Click;
            // 
            // cbWorksafeMode
            // 
            cbWorksafeMode.AutoSize = true;
            cbWorksafeMode.Location = new System.Drawing.Point(4, 151);
            cbWorksafeMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbWorksafeMode.Name = "cbWorksafeMode";
            cbWorksafeMode.Size = new System.Drawing.Size(296, 19);
            cbWorksafeMode.TabIndex = 6;
            cbWorksafeMode.Text = "Run in \"safe for work\" mode (no portraits or colors)";
            cbWorksafeMode.UseVisualStyleBackColor = true;
            cbWorksafeMode.CheckedChanged += OnMustEnableOrDisable;
            // 
            // compatibilityCombo
            // 
            compatibilityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            compatibilityCombo.FormattingEnabled = true;
            compatibilityCombo.Items.AddRange(new object[] { "Windows", "Wine" });
            compatibilityCombo.Location = new System.Drawing.Point(270, 205);
            compatibilityCombo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            compatibilityCombo.Name = "compatibilityCombo";
            compatibilityCombo.Size = new System.Drawing.Size(140, 23);
            compatibilityCombo.TabIndex = 7;
            // 
            // runAtStartupComboBox
            // 
            runAtStartupComboBox.AutoSize = true;
            runAtStartupComboBox.Location = new System.Drawing.Point(4, 102);
            runAtStartupComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            runAtStartupComboBox.Name = "runAtStartupComboBox";
            runAtStartupComboBox.Size = new System.Drawing.Size(148, 19);
            runAtStartupComboBox.TabIndex = 5;
            runAtStartupComboBox.Text = "Run EVEMon at Startup";
            runAtStartupComboBox.UseVisualStyleBackColor = true;
            // 
            // mainWindowPage
            // 
            mainWindowPage.Controls.Add(lblMainWindowPage);
            mainWindowPage.Controls.Add(OverviewGroupBox);
            mainWindowPage.Controls.Add(CharacterMonitorGroupBox);
            mainWindowPage.Controls.Add(WindowTitleGroupBox);
            mainWindowPage.Dock = System.Windows.Forms.DockStyle.Fill;
            mainWindowPage.Location = new System.Drawing.Point(6, 6);
            mainWindowPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainWindowPage.Name = "mainWindowPage";
            mainWindowPage.Size = new System.Drawing.Size(507, 492);
            mainWindowPage.TabIndex = 1;
            mainWindowPage.Text = "mainWindowPage";
            // 
            // OverviewGroupBox
            // 
            OverviewGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            OverviewGroupBox.Controls.Add(overviewPanel);
            OverviewGroupBox.Controls.Add(cbShowOverViewTab);
            OverviewGroupBox.Location = new System.Drawing.Point(1, 338);
            OverviewGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            OverviewGroupBox.Name = "OverviewGroupBox";
            OverviewGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            OverviewGroupBox.Size = new System.Drawing.Size(499, 147);
            OverviewGroupBox.TabIndex = 15;
            OverviewGroupBox.TabStop = false;
            OverviewGroupBox.Text = "Overview";
            // 
            // overviewPanel
            // 
            overviewPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            overviewPanel.Controls.Add(extraInfoComboBox);
            overviewPanel.Controls.Add(cbShowSkillpointsOnOverview);
            overviewPanel.Controls.Add(cbUseIncreasedContrastOnOverview);
            overviewPanel.Controls.Add(overviewGroupCharactersInTrainingCheckBox);
            overviewPanel.Controls.Add(overviewShowSkillQueueTrainingTimeCheckBox);
            overviewPanel.Controls.Add(overviewShowWalletCheckBox);
            overviewPanel.Controls.Add(lblSize);
            overviewPanel.Controls.Add(overviewShowPortraitCheckBox);
            overviewPanel.Controls.Add(overviewPortraitSizeComboBox);
            overviewPanel.Location = new System.Drawing.Point(34, 42);
            overviewPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            overviewPanel.Name = "overviewPanel";
            overviewPanel.Size = new System.Drawing.Size(458, 98);
            overviewPanel.TabIndex = 32;
            // 
            // extraInfoComboBox
            // 
            extraInfoComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            extraInfoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            extraInfoComboBox.Items.AddRange(new object[] { "No Extra Info", "Show Location", "Show Jobs" });
            extraInfoComboBox.Location = new System.Drawing.Point(320, 21);
            extraInfoComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            extraInfoComboBox.Name = "extraInfoComboBox";
            extraInfoComboBox.Size = new System.Drawing.Size(135, 23);
            extraInfoComboBox.TabIndex = 36;
            // 
            // cbShowSkillpointsOnOverview
            // 
            cbShowSkillpointsOnOverview.AutoSize = true;
            cbShowSkillpointsOnOverview.Location = new System.Drawing.Point(188, 23);
            cbShowSkillpointsOnOverview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbShowSkillpointsOnOverview.Name = "cbShowSkillpointsOnOverview";
            cbShowSkillpointsOnOverview.Size = new System.Drawing.Size(115, 19);
            cbShowSkillpointsOnOverview.TabIndex = 35;
            cbShowSkillpointsOnOverview.Text = "Show Skill Points";
            cbShowSkillpointsOnOverview.UseVisualStyleBackColor = true;
            // 
            // overviewPortraitSizeComboBox
            // 
            overviewPortraitSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            overviewPortraitSizeComboBox.FormattingEnabled = true;
            overviewPortraitSizeComboBox.Location = new System.Drawing.Point(61, 57);
            overviewPortraitSizeComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            overviewPortraitSizeComboBox.Name = "overviewPortraitSizeComboBox";
            overviewPortraitSizeComboBox.Size = new System.Drawing.Size(92, 23);
            overviewPortraitSizeComboBox.TabIndex = 28;
            // 
            // skillPlannerPage
            // 
            skillPlannerPage.Controls.Add(cbAdvanceEntryAdd);
            skillPlannerPage.Controls.Add(cbSummaryOnMultiSelectOnly);
            skillPlannerPage.Controls.Add(lblSkillPlannerPage);
            skillPlannerPage.Controls.Add(cbHighlightQueuedSiklls);
            skillPlannerPage.Controls.Add(cbHighlightPartialSkills);
            skillPlannerPage.Controls.Add(cbHighlightConflicts);
            skillPlannerPage.Controls.Add(cbHighlightPrerequisites);
            skillPlannerPage.Controls.Add(cbHighlightPlannedSkills);
            skillPlannerPage.Dock = System.Windows.Forms.DockStyle.Fill;
            skillPlannerPage.Location = new System.Drawing.Point(6, 6);
            skillPlannerPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            skillPlannerPage.Name = "skillPlannerPage";
            skillPlannerPage.Size = new System.Drawing.Size(507, 492);
            skillPlannerPage.TabIndex = 3;
            skillPlannerPage.Text = "skillPlannerPage";
            skillPlannerPage.Visible = false;
            // 
            // cbAdvanceEntryAdd
            // 
            cbAdvanceEntryAdd.AutoSize = true;
            cbAdvanceEntryAdd.Location = new System.Drawing.Point(16, 228);
            cbAdvanceEntryAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbAdvanceEntryAdd.Name = "cbAdvanceEntryAdd";
            cbAdvanceEntryAdd.Size = new System.Drawing.Size(229, 19);
            cbAdvanceEntryAdd.TabIndex = 21;
            cbAdvanceEntryAdd.Text = "Set Priority When Adding Skills To Plan";
            cbAdvanceEntryAdd.UseVisualStyleBackColor = true;
            // 
            // cbSummaryOnMultiSelectOnly
            // 
            cbSummaryOnMultiSelectOnly.AutoSize = true;
            cbSummaryOnMultiSelectOnly.Location = new System.Drawing.Point(16, 202);
            cbSummaryOnMultiSelectOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbSummaryOnMultiSelectOnly.Name = "cbSummaryOnMultiSelectOnly";
            cbSummaryOnMultiSelectOnly.Size = new System.Drawing.Size(259, 19);
            cbSummaryOnMultiSelectOnly.TabIndex = 20;
            cbSummaryOnMultiSelectOnly.Text = "Show Plan Summary Only On \"Multi-Select\"";
            cbSummaryOnMultiSelectOnly.UseVisualStyleBackColor = true;
            // 
            // cbHighlightQueuedSiklls
            // 
            cbHighlightQueuedSiklls.AutoSize = true;
            cbHighlightQueuedSiklls.Location = new System.Drawing.Point(16, 175);
            cbHighlightQueuedSiklls.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbHighlightQueuedSiklls.Name = "cbHighlightQueuedSiklls";
            cbHighlightQueuedSiklls.Size = new System.Drawing.Size(150, 19);
            cbHighlightQueuedSiklls.TabIndex = 14;
            cbHighlightQueuedSiklls.Text = "Highlight Queued Skills";
            cbHighlightQueuedSiklls.UseVisualStyleBackColor = true;
            // 
            // cbHighlightPartialSkills
            // 
            cbHighlightPartialSkills.AutoSize = true;
            cbHighlightPartialSkills.Location = new System.Drawing.Point(16, 149);
            cbHighlightPartialSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbHighlightPartialSkills.Name = "cbHighlightPartialSkills";
            cbHighlightPartialSkills.Size = new System.Drawing.Size(191, 19);
            cbHighlightPartialSkills.TabIndex = 10;
            cbHighlightPartialSkills.Text = "Highlight Partially Trained Skills";
            cbHighlightPartialSkills.UseVisualStyleBackColor = true;
            // 
            // cbHighlightConflicts
            // 
            cbHighlightConflicts.AutoSize = true;
            cbHighlightConflicts.Location = new System.Drawing.Point(16, 96);
            cbHighlightConflicts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbHighlightConflicts.Name = "cbHighlightConflicts";
            cbHighlightConflicts.Size = new System.Drawing.Size(177, 19);
            cbHighlightConflicts.TabIndex = 9;
            cbHighlightConflicts.Text = "Highlight Schedule Conflicts";
            cbHighlightConflicts.UseVisualStyleBackColor = true;
            // 
            // cbHighlightPrerequisites
            // 
            cbHighlightPrerequisites.AutoSize = true;
            cbHighlightPrerequisites.Location = new System.Drawing.Point(16, 122);
            cbHighlightPrerequisites.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbHighlightPrerequisites.Name = "cbHighlightPrerequisites";
            cbHighlightPrerequisites.Size = new System.Drawing.Size(146, 19);
            cbHighlightPrerequisites.TabIndex = 8;
            cbHighlightPrerequisites.Text = "Highlight Prerequisites";
            cbHighlightPrerequisites.UseVisualStyleBackColor = true;
            // 
            // cbHighlightPlannedSkills
            // 
            cbHighlightPlannedSkills.AutoSize = true;
            cbHighlightPlannedSkills.Location = new System.Drawing.Point(16, 69);
            cbHighlightPlannedSkills.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbHighlightPlannedSkills.Name = "cbHighlightPlannedSkills";
            cbHighlightPlannedSkills.Size = new System.Drawing.Size(151, 19);
            cbHighlightPlannedSkills.TabIndex = 0;
            cbHighlightPlannedSkills.Text = "Highlight Planned Skills";
            cbHighlightPlannedSkills.UseVisualStyleBackColor = true;
            // 
            // networkPage
            // 
            networkPage.Controls.Add(esiSettingsGroupBox);
            networkPage.Controls.Add(ProxyServerGroupBox);
            networkPage.Dock = System.Windows.Forms.DockStyle.Fill;
            networkPage.Location = new System.Drawing.Point(6, 6);
            networkPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            networkPage.Name = "networkPage";
            networkPage.Size = new System.Drawing.Size(507, 492);
            networkPage.TabIndex = 4;
            networkPage.Text = "networkPage";
            networkPage.Visible = false;
            // 
            // esiSettingsGroupBox
            // 
            esiSettingsGroupBox.Controls.Add(lblClientSecret);
            esiSettingsGroupBox.Controls.Add(lblClientID);
            esiSettingsGroupBox.Controls.Add(esiSettingsLabel);
            esiSettingsGroupBox.Controls.Add(clientSecretTextBox);
            esiSettingsGroupBox.Controls.Add(clientIDTextBox);
            esiSettingsGroupBox.Location = new System.Drawing.Point(4, 224);
            esiSettingsGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            esiSettingsGroupBox.Name = "esiSettingsGroupBox";
            esiSettingsGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            esiSettingsGroupBox.Size = new System.Drawing.Size(477, 102);
            esiSettingsGroupBox.TabIndex = 1;
            esiSettingsGroupBox.TabStop = false;
            esiSettingsGroupBox.Text = "ESI Client Settings";
            // 
            // lblClientSecret
            // 
            lblClientSecret.AutoSize = true;
            lblClientSecret.Location = new System.Drawing.Point(10, 72);
            lblClientSecret.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblClientSecret.Name = "lblClientSecret";
            lblClientSecret.Size = new System.Drawing.Size(73, 15);
            lblClientSecret.TabIndex = 2;
            lblClientSecret.Text = "Client Secret";
            // 
            // lblClientID
            // 
            lblClientID.AutoSize = true;
            lblClientID.Location = new System.Drawing.Point(34, 45);
            lblClientID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblClientID.Name = "lblClientID";
            lblClientID.Size = new System.Drawing.Size(52, 15);
            lblClientID.TabIndex = 2;
            lblClientID.Text = "Client ID";
            // 
            // esiSettingsLabel
            // 
            esiSettingsLabel.AutoSize = true;
            esiSettingsLabel.LinkArea = new System.Windows.Forms.LinkArea(31, 40);
            esiSettingsLabel.Location = new System.Drawing.Point(10, 18);
            esiSettingsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            esiSettingsLabel.Name = "esiSettingsLabel";
            esiSettingsLabel.Size = new System.Drawing.Size(395, 21);
            esiSettingsLabel.TabIndex = 1;
            esiSettingsLabel.TabStop = true;
            esiSettingsLabel.Text = "Create ESI client application: https://peterhaneve.github.io/evemon-esi\r\n";
            esiSettingsLabel.UseCompatibleTextRendering = true;
            esiSettingsLabel.LinkClicked += esiSettingsLabel_LinkClicked;
            // 
            // clientSecretTextBox
            // 
            clientSecretTextBox.Location = new System.Drawing.Point(96, 68);
            clientSecretTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            clientSecretTextBox.Name = "clientSecretTextBox";
            clientSecretTextBox.Size = new System.Drawing.Size(367, 23);
            clientSecretTextBox.TabIndex = 7;
            clientSecretTextBox.UseSystemPasswordChar = true;
            // 
            // clientIDTextBox
            // 
            clientIDTextBox.Location = new System.Drawing.Point(96, 42);
            clientIDTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            clientIDTextBox.Name = "clientIDTextBox";
            clientIDTextBox.Size = new System.Drawing.Size(367, 23);
            clientIDTextBox.TabIndex = 6;
            // 
            // ProxyServerGroupBox
            // 
            ProxyServerGroupBox.Controls.Add(customProxyCheckBox);
            ProxyServerGroupBox.Controls.Add(lblNetworkPageProxy);
            ProxyServerGroupBox.Controls.Add(customProxyPanel);
            ProxyServerGroupBox.Location = new System.Drawing.Point(4, 36);
            ProxyServerGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProxyServerGroupBox.Name = "ProxyServerGroupBox";
            ProxyServerGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ProxyServerGroupBox.Size = new System.Drawing.Size(477, 181);
            ProxyServerGroupBox.TabIndex = 0;
            ProxyServerGroupBox.TabStop = false;
            ProxyServerGroupBox.Text = "Proxy Server Settings";
            // 
            // customProxyCheckBox
            // 
            customProxyCheckBox.AutoSize = true;
            customProxyCheckBox.Location = new System.Drawing.Point(10, 85);
            customProxyCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customProxyCheckBox.Name = "customProxyCheckBox";
            customProxyCheckBox.Size = new System.Drawing.Size(130, 19);
            customProxyCheckBox.TabIndex = 9;
            customProxyCheckBox.Text = "Use a custom proxy";
            customProxyCheckBox.UseVisualStyleBackColor = true;
            customProxyCheckBox.CheckedChanged += OnMustEnableOrDisable;
            // 
            // customProxyPanel
            // 
            customProxyPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            customProxyPanel.Controls.Add(proxyPortTextBox);
            customProxyPanel.Controls.Add(lblProxyHostIPAddress);
            customProxyPanel.Controls.Add(proxyAuthenticationButton);
            customProxyPanel.Controls.Add(lblProxyPort);
            customProxyPanel.Controls.Add(lblHTTP);
            customProxyPanel.Controls.Add(proxyHttpHostTextBox);
            customProxyPanel.Location = new System.Drawing.Point(20, 112);
            customProxyPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            customProxyPanel.Name = "customProxyPanel";
            customProxyPanel.Size = new System.Drawing.Size(450, 62);
            customProxyPanel.TabIndex = 6;
            // 
            // proxyPortTextBox
            // 
            proxyPortTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            proxyPortTextBox.Location = new System.Drawing.Point(279, 28);
            proxyPortTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            proxyPortTextBox.MaxLength = 5;
            proxyPortTextBox.Name = "proxyPortTextBox";
            proxyPortTextBox.Size = new System.Drawing.Size(44, 23);
            proxyPortTextBox.TabIndex = 2;
            proxyPortTextBox.Validating += proxyPortTextBox_Validating;
            // 
            // proxyAuthenticationButton
            // 
            proxyAuthenticationButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            proxyAuthenticationButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            proxyAuthenticationButton.Location = new System.Drawing.Point(330, 25);
            proxyAuthenticationButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 0);
            proxyAuthenticationButton.Name = "proxyAuthenticationButton";
            proxyAuthenticationButton.Size = new System.Drawing.Size(113, 27);
            proxyAuthenticationButton.TabIndex = 5;
            proxyAuthenticationButton.Text = "Authentication...";
            proxyAuthenticationButton.UseVisualStyleBackColor = true;
            proxyAuthenticationButton.Click += proxyAuthenticationButton_Click;
            // 
            // proxyHttpHostTextBox
            // 
            proxyHttpHostTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            proxyHttpHostTextBox.Location = new System.Drawing.Point(58, 28);
            proxyHttpHostTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            proxyHttpHostTextBox.Name = "proxyHttpHostTextBox";
            proxyHttpHostTextBox.Size = new System.Drawing.Size(212, 23);
            proxyHttpHostTextBox.TabIndex = 1;
            // 
            // emailNotificationsPage
            // 
            emailNotificationsPage.Controls.Add(lblEmailNotificationPage);
            emailNotificationsPage.Controls.Add(mailNotificationCheckBox);
            emailNotificationsPage.Controls.Add(emailNotificationsControl);
            emailNotificationsPage.Dock = System.Windows.Forms.DockStyle.Fill;
            emailNotificationsPage.Location = new System.Drawing.Point(6, 6);
            emailNotificationsPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            emailNotificationsPage.Name = "emailNotificationsPage";
            emailNotificationsPage.Size = new System.Drawing.Size(507, 492);
            emailNotificationsPage.TabIndex = 6;
            emailNotificationsPage.Text = "emailNotificationsPage";
            emailNotificationsPage.Visible = false;
            // 
            // mailNotificationCheckBox
            // 
            mailNotificationCheckBox.AutoSize = true;
            mailNotificationCheckBox.Location = new System.Drawing.Point(8, 59);
            mailNotificationCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mailNotificationCheckBox.Name = "mailNotificationCheckBox";
            mailNotificationCheckBox.Size = new System.Drawing.Size(241, 19);
            mailNotificationCheckBox.TabIndex = 0;
            mailNotificationCheckBox.Text = "Send email when skill training completes";
            mailNotificationCheckBox.UseVisualStyleBackColor = true;
            mailNotificationCheckBox.CheckedChanged += OnMustEnableOrDisable;
            // 
            // emailNotificationsControl
            // 
            emailNotificationsControl.Location = new System.Drawing.Point(8, 85);
            emailNotificationsControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            emailNotificationsControl.Name = "emailNotificationsControl";
            emailNotificationsControl.Settings = null;
            emailNotificationsControl.Size = new System.Drawing.Size(414, 389);
            emailNotificationsControl.TabIndex = 20;
            // 
            // notificationsPage
            // 
            notificationsPage.Controls.Add(lblNotificationsPage);
            notificationsPage.Controls.Add(cbPlaySoundOnSkillComplete);
            notificationsPage.Controls.Add(notificationsControl);
            notificationsPage.Dock = System.Windows.Forms.DockStyle.Fill;
            notificationsPage.Location = new System.Drawing.Point(6, 6);
            notificationsPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            notificationsPage.Name = "notificationsPage";
            notificationsPage.Size = new System.Drawing.Size(507, 492);
            notificationsPage.TabIndex = 7;
            notificationsPage.Text = "notificationsPage";
            notificationsPage.Visible = false;
            // 
            // cbPlaySoundOnSkillComplete
            // 
            cbPlaySoundOnSkillComplete.AutoSize = true;
            cbPlaySoundOnSkillComplete.Location = new System.Drawing.Point(4, 463);
            cbPlaySoundOnSkillComplete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbPlaySoundOnSkillComplete.Name = "cbPlaySoundOnSkillComplete";
            cbPlaySoundOnSkillComplete.Size = new System.Drawing.Size(241, 19);
            cbPlaySoundOnSkillComplete.TabIndex = 3;
            cbPlaySoundOnSkillComplete.Text = "Play sound when skill training completes";
            cbPlaySoundOnSkillComplete.UseVisualStyleBackColor = true;
            // 
            // notificationsControl
            // 
            notificationsControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            notificationsControl.AutoScroll = true;
            notificationsControl.BackColor = System.Drawing.SystemColors.Window;
            notificationsControl.Location = new System.Drawing.Point(4, 65);
            notificationsControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            notificationsControl.Name = "notificationsControl";
            notificationsControl.Settings = null;
            notificationsControl.Size = new System.Drawing.Size(500, 389);
            notificationsControl.TabIndex = 4;
            // 
            // trayIconPage
            // 
            trayIconPage.Controls.Add(lblTrayIconPage);
            trayIconPage.Controls.Add(mainWindowBehaviourGroupBox);
            trayIconPage.Controls.Add(trayIconPopupGroupBox);
            trayIconPage.Controls.Add(systemTrayIconGroupBox);
            trayIconPage.Dock = System.Windows.Forms.DockStyle.Fill;
            trayIconPage.Location = new System.Drawing.Point(6, 6);
            trayIconPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayIconPage.Name = "trayIconPage";
            trayIconPage.Size = new System.Drawing.Size(507, 492);
            trayIconPage.TabIndex = 8;
            trayIconPage.Text = "trayIconPage";
            trayIconPage.Visible = false;
            // 
            // mainWindowBehaviourGroupBox
            // 
            mainWindowBehaviourGroupBox.Controls.Add(rbMinToTaskBar);
            mainWindowBehaviourGroupBox.Controls.Add(rbMinToTray);
            mainWindowBehaviourGroupBox.Controls.Add(rbExitEVEMon);
            mainWindowBehaviourGroupBox.Location = new System.Drawing.Point(10, 354);
            mainWindowBehaviourGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainWindowBehaviourGroupBox.Name = "mainWindowBehaviourGroupBox";
            mainWindowBehaviourGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainWindowBehaviourGroupBox.Size = new System.Drawing.Size(489, 105);
            mainWindowBehaviourGroupBox.TabIndex = 17;
            mainWindowBehaviourGroupBox.TabStop = false;
            mainWindowBehaviourGroupBox.Text = "Main Window Close Behaviour";
            // 
            // rbMinToTaskBar
            // 
            rbMinToTaskBar.AutoSize = true;
            rbMinToTaskBar.Location = new System.Drawing.Point(14, 76);
            rbMinToTaskBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbMinToTaskBar.Name = "rbMinToTaskBar";
            rbMinToTaskBar.Size = new System.Drawing.Size(149, 19);
            rbMinToTaskBar.TabIndex = 2;
            rbMinToTaskBar.TabStop = true;
            rbMinToTaskBar.Text = "Minimize to the taskbar";
            rbMinToTaskBar.UseVisualStyleBackColor = true;
            // 
            // rbMinToTray
            // 
            rbMinToTray.AutoSize = true;
            rbMinToTray.Location = new System.Drawing.Point(14, 50);
            rbMinToTray.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbMinToTray.Name = "rbMinToTray";
            rbMinToTray.Size = new System.Drawing.Size(171, 19);
            rbMinToTray.TabIndex = 1;
            rbMinToTray.TabStop = true;
            rbMinToTray.Text = "Minimize to the system tray";
            rbMinToTray.UseVisualStyleBackColor = true;
            // 
            // rbExitEVEMon
            // 
            rbExitEVEMon.AutoSize = true;
            rbExitEVEMon.Location = new System.Drawing.Point(14, 23);
            rbExitEVEMon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rbExitEVEMon.Name = "rbExitEVEMon";
            rbExitEVEMon.Size = new System.Drawing.Size(91, 19);
            rbExitEVEMon.TabIndex = 0;
            rbExitEVEMon.TabStop = true;
            rbExitEVEMon.Text = "Exit EVEMon";
            rbExitEVEMon.UseVisualStyleBackColor = true;
            // 
            // trayIconPopupGroupBox
            // 
            trayIconPopupGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            trayIconPopupGroupBox.Controls.Add(trayPopupDisabledRadio);
            trayIconPopupGroupBox.Controls.Add(trayPopupButton);
            trayIconPopupGroupBox.Controls.Add(trayPopupRadio);
            trayIconPopupGroupBox.Controls.Add(trayTooltipRadio);
            trayIconPopupGroupBox.Controls.Add(trayTooltipButton);
            trayIconPopupGroupBox.Location = new System.Drawing.Point(10, 212);
            trayIconPopupGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayIconPopupGroupBox.Name = "trayIconPopupGroupBox";
            trayIconPopupGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayIconPopupGroupBox.Size = new System.Drawing.Size(489, 120);
            trayIconPopupGroupBox.TabIndex = 10;
            trayIconPopupGroupBox.TabStop = false;
            trayIconPopupGroupBox.Text = "Icon Popup Style";
            // 
            // trayPopupDisabledRadio
            // 
            trayPopupDisabledRadio.AutoSize = true;
            trayPopupDisabledRadio.Location = new System.Drawing.Point(7, 90);
            trayPopupDisabledRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayPopupDisabledRadio.Name = "trayPopupDisabledRadio";
            trayPopupDisabledRadio.Size = new System.Drawing.Size(70, 19);
            trayPopupDisabledRadio.TabIndex = 5;
            trayPopupDisabledRadio.TabStop = true;
            trayPopupDisabledRadio.Text = "Disabled";
            trayPopupDisabledRadio.UseVisualStyleBackColor = true;
            // 
            // trayPopupButton
            // 
            trayPopupButton.Location = new System.Drawing.Point(80, 53);
            trayPopupButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayPopupButton.Name = "trayPopupButton";
            trayPopupButton.Size = new System.Drawing.Size(88, 27);
            trayPopupButton.TabIndex = 4;
            trayPopupButton.Text = "Configure";
            trayPopupButton.UseVisualStyleBackColor = true;
            trayPopupButton.Click += trayPopupButton_Click;
            // 
            // trayPopupRadio
            // 
            trayPopupRadio.AutoSize = true;
            trayPopupRadio.Location = new System.Drawing.Point(7, 57);
            trayPopupRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayPopupRadio.Name = "trayPopupRadio";
            trayPopupRadio.Size = new System.Drawing.Size(60, 19);
            trayPopupRadio.TabIndex = 3;
            trayPopupRadio.TabStop = true;
            trayPopupRadio.Text = "Popup";
            trayPopupRadio.UseVisualStyleBackColor = true;
            // 
            // trayTooltipRadio
            // 
            trayTooltipRadio.AutoSize = true;
            trayTooltipRadio.Location = new System.Drawing.Point(7, 23);
            trayTooltipRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayTooltipRadio.Name = "trayTooltipRadio";
            trayTooltipRadio.Size = new System.Drawing.Size(61, 19);
            trayTooltipRadio.TabIndex = 0;
            trayTooltipRadio.TabStop = true;
            trayTooltipRadio.Text = "Tooltip";
            trayTooltipRadio.UseVisualStyleBackColor = true;
            // 
            // trayTooltipButton
            // 
            trayTooltipButton.Location = new System.Drawing.Point(80, 20);
            trayTooltipButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trayTooltipButton.Name = "trayTooltipButton";
            trayTooltipButton.Size = new System.Drawing.Size(88, 27);
            trayTooltipButton.TabIndex = 2;
            trayTooltipButton.Text = "Configure";
            trayTooltipButton.UseVisualStyleBackColor = true;
            trayTooltipButton.Click += trayTooltipButton_Click;
            // 
            // updatesPage
            // 
            updatesPage.Controls.Add(updateSettingsControl);
            updatesPage.Controls.Add(lblUpdatesPage);
            updatesPage.Controls.Add(cbCheckTime);
            updatesPage.Controls.Add(cbCheckForUpdates);
            updatesPage.Dock = System.Windows.Forms.DockStyle.Fill;
            updatesPage.Location = new System.Drawing.Point(6, 6);
            updatesPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            updatesPage.Name = "updatesPage";
            updatesPage.Size = new System.Drawing.Size(507, 492);
            updatesPage.TabIndex = 9;
            updatesPage.Text = "updatesPage";
            updatesPage.Visible = false;
            // 
            // updateSettingsControl
            // 
            updateSettingsControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            updateSettingsControl.AutoScroll = true;
            updateSettingsControl.Location = new System.Drawing.Point(13, 98);
            updateSettingsControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            updateSettingsControl.Name = "updateSettingsControl";
            updateSettingsControl.Settings = null;
            updateSettingsControl.Size = new System.Drawing.Size(482, 392);
            updateSettingsControl.TabIndex = 10;
            // 
            // lblUpdatesPage
            // 
            lblUpdatesPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblUpdatesPage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblUpdatesPage.Location = new System.Drawing.Point(4, 2);
            lblUpdatesPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblUpdatesPage.Name = "lblUpdatesPage";
            lblUpdatesPage.Size = new System.Drawing.Size(500, 39);
            lblUpdatesPage.TabIndex = 9;
            lblUpdatesPage.Text = "The following settings help reducing the network load, especially for high-latency connections and clients with many characters.";
            // 
            // cbCheckTime
            // 
            cbCheckTime.AutoSize = true;
            cbCheckTime.Location = new System.Drawing.Point(18, 45);
            cbCheckTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCheckTime.Name = "cbCheckTime";
            cbCheckTime.Size = new System.Drawing.Size(157, 19);
            cbCheckTime.TabIndex = 0;
            cbCheckTime.Text = "Check system clock sync";
            cbCheckTime.UseVisualStyleBackColor = true;
            // 
            // cbCheckForUpdates
            // 
            cbCheckForUpdates.AutoSize = true;
            cbCheckForUpdates.Location = new System.Drawing.Point(18, 72);
            cbCheckForUpdates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbCheckForUpdates.Name = "cbCheckForUpdates";
            cbCheckForUpdates.Size = new System.Drawing.Size(169, 19);
            cbCheckForUpdates.TabIndex = 0;
            cbCheckForUpdates.Text = "Check for EVEMon updates";
            cbCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // schedulerUIPage
            // 
            schedulerUIPage.Controls.Add(lblSchedulerUIPage);
            schedulerUIPage.Controls.Add(panelColorText);
            schedulerUIPage.Controls.Add(lblText);
            schedulerUIPage.Controls.Add(panelColorRecurring2);
            schedulerUIPage.Controls.Add(lblBlockingEvents);
            schedulerUIPage.Controls.Add(panelColorRecurring1);
            schedulerUIPage.Controls.Add(lblRecurringEvents);
            schedulerUIPage.Controls.Add(panelColorSingle2);
            schedulerUIPage.Controls.Add(lblSimpleEvents);
            schedulerUIPage.Controls.Add(panelColorSingle1);
            schedulerUIPage.Controls.Add(panelColorBlocking);
            schedulerUIPage.Dock = System.Windows.Forms.DockStyle.Fill;
            schedulerUIPage.Location = new System.Drawing.Point(6, 6);
            schedulerUIPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            schedulerUIPage.Name = "schedulerUIPage";
            schedulerUIPage.Size = new System.Drawing.Size(507, 492);
            schedulerUIPage.TabIndex = 10;
            schedulerUIPage.Text = "schedulerUIPage";
            schedulerUIPage.Visible = false;
            // 
            // panelColorText
            // 
            panelColorText.BackColor = System.Drawing.Color.White;
            panelColorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelColorText.Cursor = System.Windows.Forms.Cursors.Hand;
            panelColorText.Location = new System.Drawing.Point(125, 121);
            panelColorText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelColorText.Name = "panelColorText";
            panelColorText.Size = new System.Drawing.Size(50, 19);
            panelColorText.TabIndex = 4;
            panelColorText.Click += colorPanel_Click;
            // 
            // panelColorRecurring2
            // 
            panelColorRecurring2.BackColor = System.Drawing.Color.LightGreen;
            panelColorRecurring2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelColorRecurring2.Cursor = System.Windows.Forms.Cursors.Hand;
            panelColorRecurring2.Location = new System.Drawing.Point(182, 203);
            panelColorRecurring2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelColorRecurring2.Name = "panelColorRecurring2";
            panelColorRecurring2.Size = new System.Drawing.Size(50, 19);
            panelColorRecurring2.TabIndex = 4;
            panelColorRecurring2.Click += colorPanel_Click;
            // 
            // panelColorRecurring1
            // 
            panelColorRecurring1.BackColor = System.Drawing.Color.Green;
            panelColorRecurring1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelColorRecurring1.Cursor = System.Windows.Forms.Cursors.Hand;
            panelColorRecurring1.Location = new System.Drawing.Point(125, 203);
            panelColorRecurring1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelColorRecurring1.Name = "panelColorRecurring1";
            panelColorRecurring1.Size = new System.Drawing.Size(50, 19);
            panelColorRecurring1.TabIndex = 4;
            panelColorRecurring1.Click += colorPanel_Click;
            // 
            // panelColorSingle2
            // 
            panelColorSingle2.BackColor = System.Drawing.Color.LightBlue;
            panelColorSingle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelColorSingle2.Cursor = System.Windows.Forms.Cursors.Hand;
            panelColorSingle2.Location = new System.Drawing.Point(182, 177);
            panelColorSingle2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelColorSingle2.Name = "panelColorSingle2";
            panelColorSingle2.Size = new System.Drawing.Size(50, 19);
            panelColorSingle2.TabIndex = 4;
            panelColorSingle2.Click += colorPanel_Click;
            // 
            // panelColorSingle1
            // 
            panelColorSingle1.BackColor = System.Drawing.Color.Blue;
            panelColorSingle1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelColorSingle1.Cursor = System.Windows.Forms.Cursors.Hand;
            panelColorSingle1.Location = new System.Drawing.Point(125, 177);
            panelColorSingle1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelColorSingle1.Name = "panelColorSingle1";
            panelColorSingle1.Size = new System.Drawing.Size(50, 19);
            panelColorSingle1.TabIndex = 4;
            panelColorSingle1.Click += colorPanel_Click;
            // 
            // panelColorBlocking
            // 
            panelColorBlocking.BackColor = System.Drawing.Color.Red;
            panelColorBlocking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelColorBlocking.Cursor = System.Windows.Forms.Cursors.Hand;
            panelColorBlocking.Location = new System.Drawing.Point(125, 148);
            panelColorBlocking.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelColorBlocking.Name = "panelColorBlocking";
            panelColorBlocking.Size = new System.Drawing.Size(50, 19);
            panelColorBlocking.TabIndex = 3;
            panelColorBlocking.Click += colorPanel_Click;
            // 
            // externalCalendarPage
            // 
            externalCalendarPage.Controls.Add(externalCalendarControl);
            externalCalendarPage.Controls.Add(lblExternalCalendarPage);
            externalCalendarPage.Controls.Add(externalCalendarCheckbox);
            externalCalendarPage.Dock = System.Windows.Forms.DockStyle.Fill;
            externalCalendarPage.Location = new System.Drawing.Point(6, 6);
            externalCalendarPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            externalCalendarPage.Name = "externalCalendarPage";
            externalCalendarPage.Size = new System.Drawing.Size(507, 491);
            externalCalendarPage.TabIndex = 11;
            externalCalendarPage.Text = "externalCalendarPage";
            externalCalendarPage.Visible = false;
            // 
            // externalCalendarControl
            // 
            externalCalendarControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            externalCalendarControl.Enabled = false;
            externalCalendarControl.Location = new System.Drawing.Point(0, 141);
            externalCalendarControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            externalCalendarControl.Name = "externalCalendarControl";
            externalCalendarControl.Size = new System.Drawing.Size(507, 350);
            externalCalendarControl.TabIndex = 12;
            // 
            // externalCalendarCheckbox
            // 
            externalCalendarCheckbox.AutoSize = true;
            externalCalendarCheckbox.CausesValidation = false;
            externalCalendarCheckbox.Location = new System.Drawing.Point(7, 113);
            externalCalendarCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            externalCalendarCheckbox.Name = "externalCalendarCheckbox";
            externalCalendarCheckbox.Size = new System.Drawing.Size(140, 19);
            externalCalendarCheckbox.TabIndex = 0;
            externalCalendarCheckbox.Text = "Use External Calendar";
            externalCalendarCheckbox.UseVisualStyleBackColor = true;
            externalCalendarCheckbox.Click += OnMustEnableOrDisable;
            // 
            // iconsPage
            // 
            iconsPage.Controls.Add(lblIconsPage);
            iconsPage.Controls.Add(gbSkillBrowserIconSet);
            iconsPage.Dock = System.Windows.Forms.DockStyle.Fill;
            iconsPage.Location = new System.Drawing.Point(6, 6);
            iconsPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            iconsPage.Name = "iconsPage";
            iconsPage.Size = new System.Drawing.Size(507, 492);
            iconsPage.TabIndex = 16;
            iconsPage.Text = "iconsPage";
            // 
            // messagesPage
            // 
            messagesPage.Controls.Add(gbMessageBox);
            messagesPage.Controls.Add(lblObsoletePlanEntries);
            messagesPage.Controls.Add(ObsoleteEntryRemovalGroupBox);
            messagesPage.Dock = System.Windows.Forms.DockStyle.Fill;
            messagesPage.Location = new System.Drawing.Point(6, 6);
            messagesPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            messagesPage.Name = "messagesPage";
            messagesPage.Size = new System.Drawing.Size(507, 492);
            messagesPage.TabIndex = 17;
            messagesPage.Text = "messagesPage";
            // 
            // gbMessageBox
            // 
            gbMessageBox.Controls.Add(lblPrioritesConflict);
            gbMessageBox.Controls.Add(btnPrioritiesReset);
            gbMessageBox.Location = new System.Drawing.Point(6, 362);
            gbMessageBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbMessageBox.Name = "gbMessageBox";
            gbMessageBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbMessageBox.Size = new System.Drawing.Size(495, 67);
            gbMessageBox.TabIndex = 25;
            gbMessageBox.TabStop = false;
            gbMessageBox.Text = "Pop-up Messages";
            // 
            // lblPrioritesConflict
            // 
            lblPrioritesConflict.AutoSize = true;
            lblPrioritesConflict.Location = new System.Drawing.Point(23, 29);
            lblPrioritesConflict.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPrioritesConflict.Name = "lblPrioritesConflict";
            lblPrioritesConflict.Size = new System.Drawing.Size(98, 15);
            lblPrioritesConflict.TabIndex = 1;
            lblPrioritesConflict.Text = "Priorities Conflict";
            // 
            // btnPrioritiesReset
            // 
            btnPrioritiesReset.Location = new System.Drawing.Point(387, 23);
            btnPrioritiesReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPrioritiesReset.Name = "btnPrioritiesReset";
            btnPrioritiesReset.Size = new System.Drawing.Size(88, 27);
            btnPrioritiesReset.TabIndex = 0;
            btnPrioritiesReset.Text = "Reset";
            btnPrioritiesReset.UseVisualStyleBackColor = true;
            btnPrioritiesReset.Click += btnPrioritiesReset_Click;
            // 
            // ObsoleteEntryRemovalGroupBox
            // 
            ObsoleteEntryRemovalGroupBox.Controls.Add(RemoveAllLabel);
            ObsoleteEntryRemovalGroupBox.Controls.Add(AlwaysAskLabel);
            ObsoleteEntryRemovalGroupBox.Controls.Add(RemoveConfirmedLabel);
            ObsoleteEntryRemovalGroupBox.Controls.Add(alwaysAskRadioButton);
            ObsoleteEntryRemovalGroupBox.Controls.Add(removeAllRadioButton);
            ObsoleteEntryRemovalGroupBox.Controls.Add(removeConfirmedRadioButton);
            ObsoleteEntryRemovalGroupBox.Location = new System.Drawing.Point(6, 138);
            ObsoleteEntryRemovalGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ObsoleteEntryRemovalGroupBox.Name = "ObsoleteEntryRemovalGroupBox";
            ObsoleteEntryRemovalGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ObsoleteEntryRemovalGroupBox.Size = new System.Drawing.Size(496, 216);
            ObsoleteEntryRemovalGroupBox.TabIndex = 23;
            ObsoleteEntryRemovalGroupBox.TabStop = false;
            ObsoleteEntryRemovalGroupBox.Text = "Obsolete Plan Entry Removal";
            // 
            // RemoveAllLabel
            // 
            RemoveAllLabel.Location = new System.Drawing.Point(28, 166);
            RemoveAllLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            RemoveAllLabel.Name = "RemoveAllLabel";
            RemoveAllLabel.Size = new System.Drawing.Size(460, 31);
            RemoveAllLabel.TabIndex = 5;
            RemoveAllLabel.Text = "If EVEMon believes a skill level has been completed, whether it has been confirmed by the API or not it will be removed when the plan is opened.";
            // 
            // AlwaysAskLabel
            // 
            AlwaysAskLabel.Location = new System.Drawing.Point(28, 105);
            AlwaysAskLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            AlwaysAskLabel.Name = "AlwaysAskLabel";
            AlwaysAskLabel.Size = new System.Drawing.Size(460, 31);
            AlwaysAskLabel.TabIndex = 4;
            AlwaysAskLabel.Text = "Always display the \"Obsolete Entries\" link at the bottom of the skill planner before removing entries.";
            // 
            // RemoveConfirmedLabel
            // 
            RemoveConfirmedLabel.Location = new System.Drawing.Point(28, 45);
            RemoveConfirmedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            RemoveConfirmedLabel.Name = "RemoveConfirmedLabel";
            RemoveConfirmedLabel.Size = new System.Drawing.Size(460, 31);
            RemoveConfirmedLabel.TabIndex = 3;
            RemoveConfirmedLabel.Text = "Once the API has confirmed a skill level has completed it is removed the next time a plan is opened. This is the default behaviour.";
            // 
            // alwaysAskRadioButton
            // 
            alwaysAskRadioButton.AutoSize = true;
            alwaysAskRadioButton.Location = new System.Drawing.Point(8, 84);
            alwaysAskRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            alwaysAskRadioButton.Name = "alwaysAskRadioButton";
            alwaysAskRadioButton.Size = new System.Drawing.Size(82, 19);
            alwaysAskRadioButton.TabIndex = 2;
            alwaysAskRadioButton.TabStop = true;
            alwaysAskRadioButton.Text = "Always ask";
            alwaysAskRadioButton.UseVisualStyleBackColor = true;
            // 
            // removeAllRadioButton
            // 
            removeAllRadioButton.AutoSize = true;
            removeAllRadioButton.Location = new System.Drawing.Point(8, 145);
            removeAllRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            removeAllRadioButton.Name = "removeAllRadioButton";
            removeAllRadioButton.Size = new System.Drawing.Size(229, 19);
            removeAllRadioButton.TabIndex = 1;
            removeAllRadioButton.TabStop = true;
            removeAllRadioButton.Text = "Remove entry once training completes";
            removeAllRadioButton.UseVisualStyleBackColor = true;
            // 
            // removeConfirmedRadioButton
            // 
            removeConfirmedRadioButton.AutoSize = true;
            removeConfirmedRadioButton.Location = new System.Drawing.Point(8, 24);
            removeConfirmedRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            removeConfirmedRadioButton.Name = "removeConfirmedRadioButton";
            removeConfirmedRadioButton.Size = new System.Drawing.Size(248, 19);
            removeConfirmedRadioButton.TabIndex = 0;
            removeConfirmedRadioButton.TabStop = true;
            removeConfirmedRadioButton.Text = "Remove confirmed entry (Recommended)";
            removeConfirmedRadioButton.UseVisualStyleBackColor = true;
            // 
            // portableEveClientsPage
            // 
            portableEveClientsPage.Controls.Add(lblPECIDescription);
            portableEveClientsPage.Controls.Add(PECIGroupBox);
            portableEveClientsPage.Dock = System.Windows.Forms.DockStyle.Fill;
            portableEveClientsPage.Location = new System.Drawing.Point(6, 6);
            portableEveClientsPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            portableEveClientsPage.Name = "portableEveClientsPage";
            portableEveClientsPage.Size = new System.Drawing.Size(507, 492);
            portableEveClientsPage.TabIndex = 20;
            portableEveClientsPage.Text = "portableEveClientsPage";
            // 
            // lblPECIDescription
            // 
            lblPECIDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblPECIDescription.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lblPECIDescription.Location = new System.Drawing.Point(5, 17);
            lblPECIDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPECIDescription.Name = "lblPECIDescription";
            lblPECIDescription.Size = new System.Drawing.Size(495, 110);
            lblPECIDescription.TabIndex = 21;
            lblPECIDescription.Text = resources.GetString("lblPECIDescription.Text");
            // 
            // PECIGroupBox
            // 
            PECIGroupBox.Controls.Add(portableEveClientsControl);
            PECIGroupBox.Location = new System.Drawing.Point(8, 149);
            PECIGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            PECIGroupBox.Name = "PECIGroupBox";
            PECIGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            PECIGroupBox.Size = new System.Drawing.Size(491, 327);
            PECIGroupBox.TabIndex = 0;
            PECIGroupBox.TabStop = false;
            PECIGroupBox.Text = "Portable EVE Client Installations";
            // 
            // portableEveClientsControl
            // 
            portableEveClientsControl.AutoScroll = true;
            portableEveClientsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            portableEveClientsControl.Location = new System.Drawing.Point(4, 19);
            portableEveClientsControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            portableEveClientsControl.Name = "portableEveClientsControl";
            portableEveClientsControl.Size = new System.Drawing.Size(483, 305);
            portableEveClientsControl.TabIndex = 0;
            // 
            // marketPriceProvidersPage
            // 
            marketPriceProvidersPage.Controls.Add(gbMarketPriceProviders);
            marketPriceProvidersPage.Controls.Add(marketPriceProviderPageLabel);
            marketPriceProvidersPage.Dock = System.Windows.Forms.DockStyle.Fill;
            marketPriceProvidersPage.Location = new System.Drawing.Point(6, 6);
            marketPriceProvidersPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            marketPriceProvidersPage.Name = "marketPriceProvidersPage";
            marketPriceProvidersPage.Size = new System.Drawing.Size(507, 492);
            marketPriceProvidersPage.TabIndex = 21;
            marketPriceProvidersPage.Text = "marketPriceProvidersPage";
            // 
            // gbMarketPriceProviders
            // 
            gbMarketPriceProviders.Controls.Add(cbProvidersList);
            gbMarketPriceProviders.Controls.Add(SelectedProviderLabel);
            gbMarketPriceProviders.Location = new System.Drawing.Point(4, 78);
            gbMarketPriceProviders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbMarketPriceProviders.Name = "gbMarketPriceProviders";
            gbMarketPriceProviders.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gbMarketPriceProviders.Size = new System.Drawing.Size(250, 77);
            gbMarketPriceProviders.TabIndex = 22;
            gbMarketPriceProviders.TabStop = false;
            gbMarketPriceProviders.Text = "Market Price Provider";
            // 
            // cbProvidersList
            // 
            cbProvidersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbProvidersList.FormattingEnabled = true;
            cbProvidersList.Location = new System.Drawing.Point(71, 32);
            cbProvidersList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cbProvidersList.Name = "cbProvidersList";
            cbProvidersList.Size = new System.Drawing.Size(171, 23);
            cbProvidersList.TabIndex = 1;
            // 
            // SelectedProviderLabel
            // 
            SelectedProviderLabel.AutoSize = true;
            SelectedProviderLabel.Location = new System.Drawing.Point(7, 36);
            SelectedProviderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            SelectedProviderLabel.Name = "SelectedProviderLabel";
            SelectedProviderLabel.Size = new System.Drawing.Size(54, 15);
            SelectedProviderLabel.TabIndex = 0;
            SelectedProviderLabel.Text = "Provider:";
            // 
            // marketPriceProviderPageLabel
            // 
            marketPriceProviderPageLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            marketPriceProviderPageLabel.AutoSize = true;
            marketPriceProviderPageLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            marketPriceProviderPageLabel.Location = new System.Drawing.Point(4, 44);
            marketPriceProviderPageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            marketPriceProviderPageLabel.Name = "marketPriceProviderPageLabel";
            marketPriceProviderPageLabel.Size = new System.Drawing.Size(327, 15);
            marketPriceProviderPageLabel.TabIndex = 21;
            marketPriceProviderPageLabel.Text = "Request prices for all EVE items from a market price provider.";
            // 
            // cloudStorageServicePage
            // 
            cloudStorageServicePage.Controls.Add(providerAuthenticationGroupBox);
            cloudStorageServicePage.Controls.Add(cloudStorageGroupBox);
            cloudStorageServicePage.Controls.Add(linkLabel1);
            cloudStorageServicePage.Controls.Add(settingsFileStorageGroupBox);
            cloudStorageServicePage.Dock = System.Windows.Forms.DockStyle.Fill;
            cloudStorageServicePage.Location = new System.Drawing.Point(6, 6);
            cloudStorageServicePage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cloudStorageServicePage.Name = "cloudStorageServicePage";
            cloudStorageServicePage.Size = new System.Drawing.Size(507, 492);
            cloudStorageServicePage.TabIndex = 22;
            cloudStorageServicePage.Text = "cloudStorageServicePage";
            // 
            // providerAuthenticationGroupBox
            // 
            providerAuthenticationGroupBox.Controls.Add(cloudStorageServiceControl);
            providerAuthenticationGroupBox.Location = new System.Drawing.Point(7, 332);
            providerAuthenticationGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            providerAuthenticationGroupBox.Name = "providerAuthenticationGroupBox";
            providerAuthenticationGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            providerAuthenticationGroupBox.Size = new System.Drawing.Size(491, 148);
            providerAuthenticationGroupBox.TabIndex = 24;
            providerAuthenticationGroupBox.TabStop = false;
            providerAuthenticationGroupBox.Text = "Cloud Storage Provider Authentication";
            // 
            // cloudStorageServiceControl
            // 
            cloudStorageServiceControl.Dock = System.Windows.Forms.DockStyle.Fill;
            cloudStorageServiceControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
            cloudStorageServiceControl.Location = new System.Drawing.Point(4, 19);
            cloudStorageServiceControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            cloudStorageServiceControl.Name = "cloudStorageServiceControl";
            cloudStorageServiceControl.Size = new System.Drawing.Size(483, 126);
            cloudStorageServiceControl.TabIndex = 0;
            // 
            // cloudStorageGroupBox
            // 
            cloudStorageGroupBox.Controls.Add(cloudStorageProviderLogoPictureBox);
            cloudStorageGroupBox.Controls.Add(cloudStorageProvidersComboBox);
            cloudStorageGroupBox.Controls.Add(lblSelectedProvider);
            cloudStorageGroupBox.Location = new System.Drawing.Point(7, 93);
            cloudStorageGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cloudStorageGroupBox.Name = "cloudStorageGroupBox";
            cloudStorageGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cloudStorageGroupBox.Size = new System.Drawing.Size(271, 77);
            cloudStorageGroupBox.TabIndex = 23;
            cloudStorageGroupBox.TabStop = false;
            cloudStorageGroupBox.Text = "Cloud Storage Provider";
            // 
            // cloudStorageProviderLogoPictureBox
            // 
            cloudStorageProviderLogoPictureBox.Location = new System.Drawing.Point(205, 13);
            cloudStorageProviderLogoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cloudStorageProviderLogoPictureBox.Name = "cloudStorageProviderLogoPictureBox";
            cloudStorageProviderLogoPictureBox.Size = new System.Drawing.Size(50, 50);
            cloudStorageProviderLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            cloudStorageProviderLogoPictureBox.TabIndex = 2;
            cloudStorageProviderLogoPictureBox.TabStop = false;
            // 
            // cloudStorageProvidersComboBox
            // 
            cloudStorageProvidersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cloudStorageProvidersComboBox.FormattingEnabled = true;
            cloudStorageProvidersComboBox.Location = new System.Drawing.Point(71, 32);
            cloudStorageProvidersComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cloudStorageProvidersComboBox.Name = "cloudStorageProvidersComboBox";
            cloudStorageProvidersComboBox.Size = new System.Drawing.Size(124, 23);
            cloudStorageProvidersComboBox.TabIndex = 1;
            cloudStorageProvidersComboBox.SelectedIndexChanged += cloudStorageProvidersComboBox_SelectedIndexChanged;
            // 
            // lblSelectedProvider
            // 
            lblSelectedProvider.AutoSize = true;
            lblSelectedProvider.Location = new System.Drawing.Point(7, 36);
            lblSelectedProvider.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSelectedProvider.Name = "lblSelectedProvider";
            lblSelectedProvider.Size = new System.Drawing.Size(54, 15);
            lblSelectedProvider.TabIndex = 0;
            lblSelectedProvider.Text = "Provider:";
            // 
            // linkLabel1
            // 
            linkLabel1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            linkLabel1.Location = new System.Drawing.Point(9, 29);
            linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(489, 47);
            linkLabel1.TabIndex = 5;
            linkLabel1.Text = resources.GetString("linkLabel1.Text");
            // 
            // settingsFileStorageGroupBox
            // 
            settingsFileStorageGroupBox.Controls.Add(settingsFileStorageControl);
            settingsFileStorageGroupBox.Location = new System.Drawing.Point(7, 178);
            settingsFileStorageGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            settingsFileStorageGroupBox.Name = "settingsFileStorageGroupBox";
            settingsFileStorageGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            settingsFileStorageGroupBox.Size = new System.Drawing.Size(492, 147);
            settingsFileStorageGroupBox.TabIndex = 4;
            settingsFileStorageGroupBox.TabStop = false;
            settingsFileStorageGroupBox.Text = "Settings File Storage";
            // 
            // settingsFileStorageControl
            // 
            settingsFileStorageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsFileStorageControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
            settingsFileStorageControl.Location = new System.Drawing.Point(4, 19);
            settingsFileStorageControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            settingsFileStorageControl.Name = "settingsFileStorageControl";
            settingsFileStorageControl.Size = new System.Drawing.Size(484, 125);
            settingsFileStorageControl.TabIndex = 0;
            // 
            // SettingsForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.SystemColors.Window;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(751, 556);
            Controls.Add(multiPanel);
            Controls.Add(leftPanel);
            Controls.Add(bottomPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(464, 456);
            Name = "SettingsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "EVEMon Settings";
            Load += SettingsForm_Load;
            systemTrayIconGroupBox.ResumeLayout(false);
            systemTrayIconGroupBox.PerformLayout();
            bottomPanel.ResumeLayout(false);
            CharacterMonitorGroupBox.ResumeLayout(false);
            CharacterMonitorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSkillQueueWarningThresholdDays).EndInit();
            WindowTitleGroupBox.ResumeLayout(false);
            WindowTitleGroupBox.PerformLayout();
            gbSkillBrowserIconSet.ResumeLayout(false);
            gbSkillBrowserIconSet.PerformLayout();
            iconsSetTableLayoutPanel.ResumeLayout(false);
            leftPanel.ResumeLayout(false);
            multiPanel.ResumeLayout(false);
            generalPage.ResumeLayout(false);
            generalPage.PerformLayout();
            mainWindowPage.ResumeLayout(false);
            OverviewGroupBox.ResumeLayout(false);
            OverviewGroupBox.PerformLayout();
            overviewPanel.ResumeLayout(false);
            overviewPanel.PerformLayout();
            skillPlannerPage.ResumeLayout(false);
            skillPlannerPage.PerformLayout();
            networkPage.ResumeLayout(false);
            esiSettingsGroupBox.ResumeLayout(false);
            esiSettingsGroupBox.PerformLayout();
            ProxyServerGroupBox.ResumeLayout(false);
            ProxyServerGroupBox.PerformLayout();
            customProxyPanel.ResumeLayout(false);
            customProxyPanel.PerformLayout();
            emailNotificationsPage.ResumeLayout(false);
            emailNotificationsPage.PerformLayout();
            notificationsPage.ResumeLayout(false);
            notificationsPage.PerformLayout();
            trayIconPage.ResumeLayout(false);
            mainWindowBehaviourGroupBox.ResumeLayout(false);
            mainWindowBehaviourGroupBox.PerformLayout();
            trayIconPopupGroupBox.ResumeLayout(false);
            trayIconPopupGroupBox.PerformLayout();
            updatesPage.ResumeLayout(false);
            updatesPage.PerformLayout();
            schedulerUIPage.ResumeLayout(false);
            schedulerUIPage.PerformLayout();
            externalCalendarPage.ResumeLayout(false);
            externalCalendarPage.PerformLayout();
            iconsPage.ResumeLayout(false);
            messagesPage.ResumeLayout(false);
            gbMessageBox.ResumeLayout(false);
            gbMessageBox.PerformLayout();
            ObsoleteEntryRemovalGroupBox.ResumeLayout(false);
            ObsoleteEntryRemovalGroupBox.PerformLayout();
            portableEveClientsPage.ResumeLayout(false);
            PECIGroupBox.ResumeLayout(false);
            marketPriceProvidersPage.ResumeLayout(false);
            marketPriceProvidersPage.PerformLayout();
            gbMarketPriceProviders.ResumeLayout(false);
            gbMarketPriceProviders.PerformLayout();
            cloudStorageServicePage.ResumeLayout(false);
            providerAuthenticationGroupBox.ResumeLayout(false);
            cloudStorageGroupBox.ResumeLayout(false);
            cloudStorageGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cloudStorageProviderLogoPictureBox).EndInit();
            settingsFileStorageGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion


        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox cbPlaySoundOnSkillComplete;
        private System.Windows.Forms.GroupBox ProxyServerGroupBox;
        private System.Windows.Forms.TextBox proxyPortTextBox;
        private System.Windows.Forms.TextBox proxyHttpHostTextBox;
        private System.Windows.Forms.Button proxyAuthenticationButton;
        private System.Windows.Forms.CheckBox cbCheckForUpdates;
        private System.Windows.Forms.CheckBox cbWorksafeMode;
        private System.Windows.Forms.CheckBox cbHighlightPlannedSkills;
        private System.Windows.Forms.CheckBox cbHighlightPrerequisites;
        private System.Windows.Forms.CheckBox runAtStartupComboBox;
        private System.Windows.Forms.CheckBox cbTitleToTime;
        private System.Windows.Forms.ComboBox cbWindowsTitleList;
        private System.Windows.Forms.RadioButton rbSystemTrayOptionsNever;
        private System.Windows.Forms.RadioButton rbSystemTrayOptionsMinimized;
        private System.Windows.Forms.RadioButton rbSystemTrayOptionsAlways;
        private System.Windows.Forms.ToolTip ttToolTipCodes;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox cbSkillInTitle;
        private System.Windows.Forms.CheckBox cbHighlightConflicts;
        private System.Windows.Forms.CheckBox cbHighlightPartialSkills;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel panelColorBlocking;
        private System.Windows.Forms.Panel panelColorRecurring2;
        private System.Windows.Forms.Panel panelColorRecurring1;
        private System.Windows.Forms.Panel panelColorSingle2;
        private System.Windows.Forms.Panel panelColorSingle1;
        private System.Windows.Forms.Panel panelColorText;
        private System.Windows.Forms.Button trayTooltipButton;
        private System.Windows.Forms.CheckBox externalCalendarCheckbox;
        private System.Windows.Forms.CheckBox cbShowAllPublicSkills;
        private System.Windows.Forms.CheckBox cbShowNonPublicSkills;
        private System.Windows.Forms.CheckBox cbCheckTime;
        private System.Windows.Forms.CheckBox cbShowOverViewTab;
        private System.Windows.Forms.ComboBox compatibilityCombo;
        private EVEMon.SettingsUI.NotificationsControl notificationsControl;
        private System.Windows.Forms.TreeView treeView;
        private MultiPanel multiPanel;
        private MultiPanelPage generalPage;
        private MultiPanelPage mainWindowPage;
        private MultiPanelPage skillPlannerPage;
        private MultiPanelPage networkPage;
        private MultiPanelPage emailNotificationsPage;
        private MultiPanelPage notificationsPage;
        private MultiPanelPage trayIconPage;
        private MultiPanelPage updatesPage;
        private MultiPanelPage schedulerUIPage;
        private MultiPanelPage externalCalendarPage;
        private System.Windows.Forms.Panel customProxyPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.CheckBox customProxyCheckBox;
        private System.Windows.Forms.GroupBox trayIconPopupGroupBox;
        private System.Windows.Forms.Button trayPopupButton;
        private System.Windows.Forms.RadioButton trayPopupRadio;
        private System.Windows.Forms.RadioButton trayTooltipRadio;
        private System.Windows.Forms.Label lblUpdatesPage;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.GroupBox OverviewGroupBox;
        private System.Windows.Forms.ComboBox overviewPortraitSizeComboBox;
        private System.Windows.Forms.CheckBox overviewShowPortraitCheckBox;
        private System.Windows.Forms.CheckBox overviewShowWalletCheckBox;
        private System.Windows.Forms.Panel overviewPanel;
        private UpdateSettingsControl updateSettingsControl;
        private System.Windows.Forms.GroupBox mainWindowBehaviourGroupBox;
        private System.Windows.Forms.RadioButton rbMinToTaskBar;
        private System.Windows.Forms.RadioButton rbMinToTray;
        private System.Windows.Forms.RadioButton rbExitEVEMon;
        private System.Windows.Forms.CheckBox cbAlwaysShowSkillQueueTime;
        private System.Windows.Forms.CheckBox cbColorPartialSkills;
        private System.Windows.Forms.CheckBox cbShowPrereqMetSkills;
        private System.Windows.Forms.CheckBox cbColorQueuedSkills;
        private System.Windows.Forms.CheckBox cbHighlightQueuedSiklls;
        private System.Windows.Forms.CheckBox overviewShowSkillQueueTrainingTimeCheckBox;
        private System.Windows.Forms.CheckBox cbSummaryOnMultiSelectOnly;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.RadioButton trayPopupDisabledRadio;
        private System.Windows.Forms.CheckBox overviewGroupCharactersInTrainingCheckBox;
        private System.Windows.Forms.CheckBox cbAdvanceEntryAdd;
        private System.Windows.Forms.CheckBox cbUseIncreasedContrastOnOverview;
        private MultiPanelPage iconsPage;
        private System.Windows.Forms.TableLayoutPanel iconsSetTableLayoutPanel;
        private System.Windows.Forms.ComboBox cbSkillIconSet;
        private System.Windows.Forms.TreeView tvlist;
        private MultiPanelPage messagesPage;
        private System.Windows.Forms.GroupBox ObsoleteEntryRemovalGroupBox;
        private System.Windows.Forms.Label RemoveAllLabel;
        private System.Windows.Forms.Label AlwaysAskLabel;
        private System.Windows.Forms.Label RemoveConfirmedLabel;
        private System.Windows.Forms.RadioButton alwaysAskRadioButton;
        private System.Windows.Forms.RadioButton removeAllRadioButton;
        private System.Windows.Forms.RadioButton removeConfirmedRadioButton;
        private System.Windows.Forms.GroupBox gbMessageBox;
        private System.Windows.Forms.Label lblPrioritesConflict;
        private System.Windows.Forms.Button btnPrioritiesReset;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label lblMainWindowPage;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.GroupBox CharacterMonitorGroupBox;
        private System.Windows.Forms.GroupBox WindowTitleGroupBox;
        private System.Windows.Forms.Label lblGeneralPage;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.Label lblSkillPlannerPage;
        private System.Windows.Forms.Label lblNetworkPageProxy;
        private System.Windows.Forms.Label lblProxyHostIPAddress;
        private System.Windows.Forms.Label lblProxyPort;
        private System.Windows.Forms.Label lblHTTP;
        private System.Windows.Forms.Label lblEmailNotificationPage;
        private System.Windows.Forms.Label lblNotificationsPage;
        private System.Windows.Forms.Label lblTrayIconPage;
        private System.Windows.Forms.Label lblSchedulerUIPage;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblBlockingEvents;
        private System.Windows.Forms.Label lblRecurringEvents;
        private System.Windows.Forms.Label lblSimpleEvents;
        private System.Windows.Forms.Label lblExternalCalendarPage;
        private System.Windows.Forms.Label lblIconsPage;
        private System.Windows.Forms.GroupBox gbSkillBrowserIconSet;
        private System.Windows.Forms.Label lblObsoletePlanEntries;
        private System.Windows.Forms.GroupBox systemTrayIconGroupBox;
        private System.Windows.Forms.Button btnEVEMonDataDir;
        private EmailNotificationsControl emailNotificationsControl;
        private System.Windows.Forms.CheckBox mailNotificationCheckBox;
        private MultiPanelPage portableEveClientsPage;
        private System.Windows.Forms.Label lblPECIDescription;
        private System.Windows.Forms.GroupBox PECIGroupBox;
        private PortableEveClientsControl portableEveClientsControl;
        private MultiPanelPage marketPriceProvidersPage;
        private System.Windows.Forms.Label marketPriceProviderPageLabel;
        private System.Windows.Forms.GroupBox gbMarketPriceProviders;
        private System.Windows.Forms.ComboBox cbProvidersList;
        private System.Windows.Forms.Label SelectedProviderLabel;
        private MultiPanelPage cloudStorageServicePage;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox settingsFileStorageGroupBox;
        private SettingsFileStorageControl settingsFileStorageControl;
        private System.Windows.Forms.GroupBox cloudStorageGroupBox;
        private System.Windows.Forms.ComboBox cloudStorageProvidersComboBox;
        private System.Windows.Forms.Label lblSelectedProvider;
        private System.Windows.Forms.GroupBox providerAuthenticationGroupBox;
        private CloudStorageServiceControl cloudStorageServiceControl;
        private System.Windows.Forms.PictureBox cloudStorageProviderLogoPictureBox;
        private ExternalCalendarControl externalCalendarControl;
        private System.Windows.Forms.NumericUpDown nudSkillQueueWarningThresholdDays;
        private System.Windows.Forms.Label lblSkillQueueWarningThresholdDays;
        private System.Windows.Forms.Label lblSkillQueuWarningThreshold;
        private System.Windows.Forms.CheckBox cbShowSkillpointsOnOverview;
        private System.Windows.Forms.GroupBox esiSettingsGroupBox;
        private System.Windows.Forms.TextBox clientIDTextBox;
        private System.Windows.Forms.LinkLabel esiSettingsLabel;
        private System.Windows.Forms.Label lblClientSecret;
        private System.Windows.Forms.Label lblClientID;
        private System.Windows.Forms.TextBox clientSecretTextBox;
        private System.Windows.Forms.ComboBox extraInfoComboBox;
    }
}
