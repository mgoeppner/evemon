
namespace EVEMon
{
    partial class MainWindow
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.trayIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.planToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plansStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.testTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.trayTestNotificationTSMI = new System.Windows.Forms.ToolStripMenuItem();
			this.trayTestCharacterNotificationTSMI = new System.Windows.Forms.ToolStripMenuItem();
			this.testsToolStripSeperator = new System.Windows.Forms.ToolStripSeparator();
			this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lblTraining = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusSpacerLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblCSSProviderStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsDatafilesLoadingProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.mainMenuBar = new System.Windows.Forms.MenuStrip();
			this.toolbarContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menubarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addAPIKeyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.manageAPIKeysMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileManagerHideToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.deleteCharacterMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.exportCharacterMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.hideCharacterMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.fileExportSaveToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileRestoreResetToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.clearCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileResetExitToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copySkillsToClipboardBBFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNewPlan = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCreatePlanFromSkillQueue = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiImportPlanFromFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiManagePlans = new System.Windows.Forms.ToolStripMenuItem();
			this.plansSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.charactersComparisonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.blankCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skillBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.certificateBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shipBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.itemBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.blueprintBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skillsPieChartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.firstSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.implantsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showOwnedSkillbooksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.secondSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.forumsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.issuesFeaturesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readTheDocsManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpAboutKnownProblemsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exceptionWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exceptionWindowRecursiveExceptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testNotificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testCharacterNotificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testTimeoutOneSecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainToolBar = new System.Windows.Forms.ToolStrip();
			this.addAPIKeyTbMenu = new System.Windows.Forms.ToolStripButton();
			this.apiKeysManagementTbMenu = new System.Windows.Forms.ToolStripButton();
			this.apiKeysSettingsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.manageCharacterTbMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.manageCharacterToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.tsdbSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsLoadSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.clearCacheToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.resetSettingsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.cacheResetToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.exitToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.tsbManagePlans = new System.Windows.Forms.ToolStripButton();
			this.plansTbMenu = new System.Windows.Forms.ToolStripDropDownButton();
			this.plansToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.characterComparisonToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.tsbMineralSheet = new System.Windows.Forms.ToolStripButton();
			this.skillsPieChartTbMenu = new System.Windows.Forms.ToolStripButton();
			this.tsbSchedule = new System.Windows.Forms.ToolStripButton();
			this.tsbImplantGroups = new System.Windows.Forms.ToolStripButton();
			this.tsbShowOwned = new System.Windows.Forms.ToolStripButton();
			this.toolsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.tsbOptions = new System.Windows.Forms.ToolStripButton();
			this.tsbAbout = new System.Windows.Forms.ToolStripButton();
			this.niAlertIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.ofdOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.tcCharacterTabs = new EVEMon.Common.Controls.DraggableTabControl();
			this.tpOverview = new System.Windows.Forms.TabPage();
			this.overview = new EVEMon.Controls.Overview();
			this.trayIcon = new EVEMon.Common.Controls.TrayIcon(this.components);
			this.tabLoadingLabel = new System.Windows.Forms.Label();
			this.noCharactersLabel = new System.Windows.Forms.Label();
			this.mainLoadingThrobber = new EVEMon.Common.Controls.Throbber();
			this.notificationList = new EVEMon.Controls.NotificationList();
			this.trayIconContextMenuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.mainMenuBar.SuspendLayout();
			this.toolbarContext.SuspendLayout();
			this.mainToolBar.SuspendLayout();
			this.tcCharacterTabs.SuspendLayout();
			this.tpOverview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mainLoadingThrobber)).BeginInit();
			this.SuspendLayout();
			// 
			// trayIconContextMenuStrip
			// 
			this.trayIconContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.trayIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planToolStripMenuItem,
            this.plansStripSeparator,
            this.testTrayToolStripMenuItem,
            this.testsToolStripSeperator,
            this.restoreToolStripMenuItem,
            this.closeToolStripMenuItem});
			this.trayIconContextMenuStrip.Name = "trayIconContextMenuStrip";
			this.trayIconContextMenuStrip.Size = new System.Drawing.Size(114, 104);
			this.trayIconContextMenuStrip.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.trayIconToolStrip_Closed);
			this.trayIconContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.trayIconToolStrip_Opening);
			// 
			// planToolStripMenuItem
			// 
			this.planToolStripMenuItem.Name = "planToolStripMenuItem";
			this.planToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.planToolStripMenuItem.Text = resources.GetString("this.planToolStripMenuItem.Text");
			// 
			// plansStripSeparator
			// 
			this.plansStripSeparator.Name = "plansStripSeparator";
			this.plansStripSeparator.Size = new System.Drawing.Size(110, 6);
			// 
			// testTrayToolStripMenuItem
			// 
			this.testTrayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayTestNotificationTSMI,
            this.trayTestCharacterNotificationTSMI});
			this.testTrayToolStripMenuItem.Name = "testTrayToolStripMenuItem";
			this.testTrayToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.testTrayToolStripMenuItem.Text = resources.GetString("this.testTrayToolStripMenuItem.Text");
			this.testTrayToolStripMenuItem.Visible = false;
			// 
			// trayTestNotificationTSMI
			// 
			this.trayTestNotificationTSMI.Name = "trayTestNotificationTSMI";
			this.trayTestNotificationTSMI.Size = new System.Drawing.Size(191, 22);
			this.trayTestNotificationTSMI.Text = resources.GetString("this.trayTestNotificationTSMI.Text");
			this.trayTestNotificationTSMI.Click += new System.EventHandler(this.testNotificationToolstripMenuItem_Click);
			// 
			// trayTestCharacterNotificationTSMI
			// 
			this.trayTestCharacterNotificationTSMI.Name = "trayTestCharacterNotificationTSMI";
			this.trayTestCharacterNotificationTSMI.Size = new System.Drawing.Size(191, 22);
			this.trayTestCharacterNotificationTSMI.Text = resources.GetString("this.trayTestCharacterNotificationTSMI.Text");
			this.trayTestCharacterNotificationTSMI.Click += new System.EventHandler(this.testCharacterNotificationToolStripMenuItem_Click);
			// 
			// testsToolStripSeperator
			// 
			this.testsToolStripSeperator.Name = "testsToolStripSeperator";
			this.testsToolStripSeperator.Size = new System.Drawing.Size(110, 6);
			this.testsToolStripSeperator.Visible = false;
			// 
			// restoreToolStripMenuItem
			// 
			this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
			this.restoreToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.restoreToolStripMenuItem.Text = resources.GetString("this.restoreToolStripMenuItem.Text");
			this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.closeToolStripMenuItem.Text = resources.GetString("this.closeToolStripMenuItem.Text");
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTraining,
            this.lblStatus,
            this.lblServerStatus,
            this.toolStripStatusSpacerLabel,
            this.lblCSSProviderStatus,
            this.tsDatafilesLoadingProgressBar});
			this.statusStrip.Location = new System.Drawing.Point(0, 624);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.ShowItemToolTips = true;
			this.statusStrip.Size = new System.Drawing.Size(600, 22);
			this.statusStrip.TabIndex = 1;
			// 
			// lblTraining
			// 
			this.lblTraining.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.lblTraining.Name = "lblTraining";
			this.lblTraining.Size = new System.Drawing.Size(0, 17);
			this.lblTraining.Text = resources.GetString("this.lblTraining.Text");
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(59, 17);
			this.lblStatus.Text = resources.GetString("this.lblStatus.Text");
			// 
			// lblServerStatus
			// 
			this.lblServerStatus.Name = "lblServerStatus";
			this.lblServerStatus.Size = new System.Drawing.Size(137, 17);
			this.lblServerStatus.Text = resources.GetString("this.lblServerStatus.Text");
			// 
			// toolStripStatusSpacerLabel
			// 
			this.toolStripStatusSpacerLabel.Name = "toolStripStatusSpacerLabel";
			this.toolStripStatusSpacerLabel.Size = new System.Drawing.Size(287, 17);
			this.toolStripStatusSpacerLabel.Spring = true;
			// 
			// lblCSSProviderStatus
			// 
			this.lblCSSProviderStatus.Name = "lblCSSProviderStatus";
			this.lblCSSProviderStatus.Size = new System.Drawing.Size(143, 17);
			this.lblCSSProviderStatus.Text = resources.GetString("this.lblCSSProviderStatus.Text");
			this.lblCSSProviderStatus.Visible = false;
			// 
			// tsDatafilesLoadingProgressBar
			// 
			this.tsDatafilesLoadingProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsDatafilesLoadingProgressBar.Name = "tsDatafilesLoadingProgressBar";
			this.tsDatafilesLoadingProgressBar.Size = new System.Drawing.Size(100, 16);
			this.tsDatafilesLoadingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.tsDatafilesLoadingProgressBar.ToolTipText = resources.GetString("this.tsDatafilesLoadingProgressBar.ToolTipText");
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "bak";
			this.saveFileDialog.FileName = "settings.xml.bak";
			this.saveFileDialog.Filter = resources.GetString("this.saveFileDialog.Filter");
			this.saveFileDialog.RestoreDirectory = true;
			this.saveFileDialog.Title = resources.GetString("this.saveFileDialog.Title");
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "bak";
			this.openFileDialog.FileName = "settings.xml.bak";
			this.openFileDialog.Filter = resources.GetString("this.openFileDialog.Filter");
			this.openFileDialog.RestoreDirectory = true;
			this.openFileDialog.ShowHelp = true;
			this.openFileDialog.Title = resources.GetString("this.openFileDialog.Title");
			// 
			// mainMenuBar
			// 
			this.mainMenuBar.BackColor = System.Drawing.SystemColors.Control;
			this.mainMenuBar.ContextMenuStrip = this.toolbarContext;
			this.mainMenuBar.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.mainMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.plansToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.testsToolStripMenuItem});
			this.mainMenuBar.Location = new System.Drawing.Point(0, 0);
			this.mainMenuBar.Name = "mainMenuBar";
			this.mainMenuBar.Size = new System.Drawing.Size(600, 24);
			this.mainMenuBar.TabIndex = 3;
			this.mainMenuBar.Text = resources.GetString("this.mainMenuBar.Text");
			this.mainMenuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainMenuBar_MouseDown);
			this.mainMenuBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainMenuBar_MouseMove);
			// 
			// toolbarContext
			// 
			this.toolbarContext.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolbarContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menubarToolStripMenuItem,
            this.toolbarToolStripMenuItem});
			this.toolbarContext.Name = "toolbarContext";
			this.toolbarContext.Size = new System.Drawing.Size(123, 48);
			this.toolbarContext.Opening += new System.ComponentModel.CancelEventHandler(this.toolbarContext_Opening);
			// 
			// menubarToolStripMenuItem
			// 
			this.menubarToolStripMenuItem.Name = "menubarToolStripMenuItem";
			this.menubarToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.menubarToolStripMenuItem.Text = resources.GetString("this.menubarToolStripMenuItem.Text");
			this.menubarToolStripMenuItem.Click += new System.EventHandler(this.menubarToolStripMenuItem_Click);
			// 
			// toolbarToolStripMenuItem
			// 
			this.toolbarToolStripMenuItem.Name = "toolbarToolStripMenuItem";
			this.toolbarToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.toolbarToolStripMenuItem.Text = resources.GetString("this.toolbarToolStripMenuItem.Text");
			this.toolbarToolStripMenuItem.Click += new System.EventHandler(this.toolbarToolStripMenuItem_Click);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAPIKeyMenu,
            this.manageAPIKeysMenuItem,
            this.fileManagerHideToolStripSeparator,
            this.deleteCharacterMenu,
            this.exportCharacterMenu,
            this.hideCharacterMenu,
            this.fileExportSaveToolStripSeparator,
            this.loadSettingsToolStripMenuItem,
            this.saveSettingsToolStripMenuItem,
            this.resetSettingsToolStripMenuItem,
            this.fileRestoreResetToolStripSeparator,
            this.clearCacheToolStripMenuItem,
            this.fileResetExitToolStripSeparator,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = resources.GetString("this.fileToolStripMenuItem.Text");
			// 
			// addAPIKeyMenu
			// 
			this.addAPIKeyMenu.Image = ((System.Drawing.Image)(resources.GetObject("addAPIKeyMenu.Image")));
			this.addAPIKeyMenu.Name = "addAPIKeyMenu";
			this.addAPIKeyMenu.Size = new System.Drawing.Size(185, 22);
			this.addAPIKeyMenu.Text = resources.GetString("this.addAPIKeyMenu.Text");
			this.addAPIKeyMenu.Click += new System.EventHandler(this.addAPIKeyMenu_Click);
			// 
			// manageAPIKeysMenuItem
			// 
			this.manageAPIKeysMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manageAPIKeysMenuItem.Image")));
			this.manageAPIKeysMenuItem.Name = "manageAPIKeysMenuItem";
			this.manageAPIKeysMenuItem.Size = new System.Drawing.Size(185, 22);
			this.manageAPIKeysMenuItem.Text = resources.GetString("this.manageAPIKeysMenuItem.Text");
			this.manageAPIKeysMenuItem.Click += new System.EventHandler(this.manageAPIKeysMenuItem_Click);
			// 
			// fileManagerHideToolStripSeparator
			// 
			this.fileManagerHideToolStripSeparator.Name = "fileManagerHideToolStripSeparator";
			this.fileManagerHideToolStripSeparator.Size = new System.Drawing.Size(182, 6);
			// 
			// deleteCharacterMenu
			// 
			this.deleteCharacterMenu.Image = ((System.Drawing.Image)(resources.GetObject("deleteCharacterMenu.Image")));
			this.deleteCharacterMenu.Name = "deleteCharacterMenu";
			this.deleteCharacterMenu.Size = new System.Drawing.Size(185, 22);
			this.deleteCharacterMenu.Text = resources.GetString("this.deleteCharacterMenu.Text");
			this.deleteCharacterMenu.Click += new System.EventHandler(this.deleteCharacterMenu_Click);
			// 
			// exportCharacterMenu
			// 
			this.exportCharacterMenu.Image = ((System.Drawing.Image)(resources.GetObject("exportCharacterMenu.Image")));
			this.exportCharacterMenu.Name = "exportCharacterMenu";
			this.exportCharacterMenu.Size = new System.Drawing.Size(185, 22);
			this.exportCharacterMenu.Text = resources.GetString("this.exportCharacterMenu.Text");
			this.exportCharacterMenu.Click += new System.EventHandler(this.saveCharacterInfosMenuItem_Click);
			// 
			// hideCharacterMenu
			// 
			this.hideCharacterMenu.Image = ((System.Drawing.Image)(resources.GetObject("hideCharacterMenu.Image")));
			this.hideCharacterMenu.Name = "hideCharacterMenu";
			this.hideCharacterMenu.Size = new System.Drawing.Size(185, 22);
			this.hideCharacterMenu.Text = resources.GetString("this.hideCharacterMenu.Text");
			this.hideCharacterMenu.Click += new System.EventHandler(this.hideCharacterMenu_Click);
			// 
			// fileExportSaveToolStripSeparator
			// 
			this.fileExportSaveToolStripSeparator.Name = "fileExportSaveToolStripSeparator";
			this.fileExportSaveToolStripSeparator.Size = new System.Drawing.Size(182, 6);
			// 
			// loadSettingsToolStripMenuItem
			// 
			this.loadSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadSettingsToolStripMenuItem.Image")));
			this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
			this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.loadSettingsToolStripMenuItem.Text = resources.GetString("this.loadSettingsToolStripMenuItem.Text");
			this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
			// 
			// saveSettingsToolStripMenuItem
			// 
			this.saveSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveSettingsToolStripMenuItem.Image")));
			this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
			this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.saveSettingsToolStripMenuItem.Text = resources.GetString("this.saveSettingsToolStripMenuItem.Text");
			this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
			// 
			// resetSettingsToolStripMenuItem
			// 
			this.resetSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resetSettingsToolStripMenuItem.Image")));
			this.resetSettingsToolStripMenuItem.Name = "resetSettingsToolStripMenuItem";
			this.resetSettingsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.resetSettingsToolStripMenuItem.Text = resources.GetString("this.resetSettingsToolStripMenuItem.Text");
			this.resetSettingsToolStripMenuItem.Click += new System.EventHandler(this.resetSettingsToolStripMenuItem_Click);
			// 
			// fileRestoreResetToolStripSeparator
			// 
			this.fileRestoreResetToolStripSeparator.Name = "fileRestoreResetToolStripSeparator";
			this.fileRestoreResetToolStripSeparator.Size = new System.Drawing.Size(182, 6);
			// 
			// clearCacheToolStripMenuItem
			// 
			this.clearCacheToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearCacheToolStripMenuItem.Image")));
			this.clearCacheToolStripMenuItem.Name = "clearCacheToolStripMenuItem";
			this.clearCacheToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.clearCacheToolStripMenuItem.Text = resources.GetString("this.clearCacheToolStripMenuItem.Text");
			this.clearCacheToolStripMenuItem.Click += new System.EventHandler(this.clearCacheToolStripMenuItem_Click);
			// 
			// fileResetExitToolStripSeparator
			// 
			this.fileResetExitToolStripSeparator.Name = "fileResetExitToolStripSeparator";
			this.fileResetExitToolStripSeparator.Size = new System.Drawing.Size(182, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.exitToolStripMenuItem.Text = resources.GetString("this.exitToolStripMenuItem.Text");
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySkillsToClipboardBBFormatToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = resources.GetString("this.editToolStripMenuItem.Text");
			this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
			// 
			// copySkillsToClipboardBBFormatToolStripMenuItem
			// 
			this.copySkillsToClipboardBBFormatToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copySkillsToClipboardBBFormatToolStripMenuItem.Image")));
			this.copySkillsToClipboardBBFormatToolStripMenuItem.Name = "copySkillsToClipboardBBFormatToolStripMenuItem";
			this.copySkillsToClipboardBBFormatToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
			this.copySkillsToClipboardBBFormatToolStripMenuItem.Text = resources.GetString("this.copySkillsToClipboardBBFormatToolStripMenuItem.Text");
			this.copySkillsToClipboardBBFormatToolStripMenuItem.Click += new System.EventHandler(this.copySkillsToClipboardBBFormatToolStripMenuItem_Click);
			// 
			// plansToolStripMenuItem
			// 
			this.plansToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewPlan,
            this.tsmiCreatePlanFromSkillQueue,
            this.tsmiImportPlanFromFile,
            this.tsmiManagePlans,
            this.plansSeparator});
			this.plansToolStripMenuItem.Name = "plansToolStripMenuItem";
			this.plansToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.plansToolStripMenuItem.Text = resources.GetString("this.plansToolStripMenuItem.Text");
			this.plansToolStripMenuItem.DropDownOpening += new System.EventHandler(this.plansToolStripMenuItem_DropDownOpening);
			// 
			// tsmiNewPlan
			// 
			this.tsmiNewPlan.Image = ((System.Drawing.Image)(resources.GetObject("tsmiNewPlan.Image")));
			this.tsmiNewPlan.Name = "tsmiNewPlan";
			this.tsmiNewPlan.Size = new System.Drawing.Size(234, 22);
			this.tsmiNewPlan.Text = resources.GetString("this.tsmiNewPlan.Text");
			this.tsmiNewPlan.Click += new System.EventHandler(this.tsmiNewPlan_Click);
			// 
			// tsmiCreatePlanFromSkillQueue
			// 
			this.tsmiCreatePlanFromSkillQueue.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCreatePlanFromSkillQueue.Image")));
			this.tsmiCreatePlanFromSkillQueue.Name = "tsmiCreatePlanFromSkillQueue";
			this.tsmiCreatePlanFromSkillQueue.Size = new System.Drawing.Size(234, 22);
			this.tsmiCreatePlanFromSkillQueue.Text = resources.GetString("this.tsmiCreatePlanFromSkillQueue.Text");
			this.tsmiCreatePlanFromSkillQueue.Click += new System.EventHandler(this.tsmiCreatePlanFromSkillQueue_Click);
			// 
			// tsmiImportPlanFromFile
			// 
			this.tsmiImportPlanFromFile.Image = ((System.Drawing.Image)(resources.GetObject("tsmiImportPlanFromFile.Image")));
			this.tsmiImportPlanFromFile.Name = "tsmiImportPlanFromFile";
			this.tsmiImportPlanFromFile.Size = new System.Drawing.Size(234, 22);
			this.tsmiImportPlanFromFile.Text = resources.GetString("this.tsmiImportPlanFromFile.Text");
			this.tsmiImportPlanFromFile.Click += new System.EventHandler(this.tsmiImportPlanFromFile_Click);
			// 
			// tsmiManagePlans
			// 
			this.tsmiManagePlans.Image = ((System.Drawing.Image)(resources.GetObject("tsmiManagePlans.Image")));
			this.tsmiManagePlans.Name = "tsmiManagePlans";
			this.tsmiManagePlans.Size = new System.Drawing.Size(234, 22);
			this.tsmiManagePlans.Text = resources.GetString("this.tsmiManagePlans.Text");
			this.tsmiManagePlans.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
			// 
			// plansSeparator
			// 
			this.plansSeparator.Name = "plansSeparator";
			this.plansSeparator.Size = new System.Drawing.Size(231, 6);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.charactersComparisonMenuItem,
            this.blankCreatorToolStripMenuItem,
            this.dataBrowserMenuItem,
            this.skillsPieChartMenuItem,
            this.firstSeparator,
            this.implantsMenuItem,
            this.showOwnedSkillbooksMenuItem,
            this.secondSeparator,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.toolsToolStripMenuItem.Text = resources.GetString("this.toolsToolStripMenuItem.Text");
			// 
			// charactersComparisonMenuItem
			// 
			this.charactersComparisonMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("charactersComparisonMenuItem.Image")));
			this.charactersComparisonMenuItem.Name = "charactersComparisonMenuItem";
			this.charactersComparisonMenuItem.Size = new System.Drawing.Size(208, 22);
			this.charactersComparisonMenuItem.Text = resources.GetString("this.charactersComparisonMenuItem.Text");
			this.charactersComparisonMenuItem.Click += new System.EventHandler(this.charactersComparisonToolStripMenuItem_Click);
			// 
			// blankCreatorToolStripMenuItem
			// 
			this.blankCreatorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("blankCreatorToolStripMenuItem.Image")));
			this.blankCreatorToolStripMenuItem.Name = "blankCreatorToolStripMenuItem";
			this.blankCreatorToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.blankCreatorToolStripMenuItem.Text = resources.GetString("this.blankCreatorToolStripMenuItem.Text");
			this.blankCreatorToolStripMenuItem.Click += new System.EventHandler(this.blankCreatorToolStripMenuItem_Click);
			// 
			// dataBrowserMenuItem
			// 
			this.dataBrowserMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skillBrowserMenuItem,
            this.certificateBrowserMenuItem,
            this.shipBrowserMenuItem,
            this.itemBrowserMenuItem,
            this.blueprintBrowserMenuItem});
			this.dataBrowserMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dataBrowserMenuItem.Image")));
			this.dataBrowserMenuItem.Name = "dataBrowserMenuItem";
			this.dataBrowserMenuItem.Size = new System.Drawing.Size(208, 22);
			this.dataBrowserMenuItem.Text = resources.GetString("this.dataBrowserMenuItem.Text");
			// 
			// skillBrowserMenuItem
			// 
			this.skillBrowserMenuItem.Name = "skillBrowserMenuItem";
			this.skillBrowserMenuItem.Size = new System.Drawing.Size(182, 22);
			this.skillBrowserMenuItem.Text = resources.GetString("this.skillBrowserMenuItem.Text");
			this.skillBrowserMenuItem.Click += new System.EventHandler(this.skillBrowserMenuItem_Click);
			// 
			// certificateBrowserMenuItem
			// 
			this.certificateBrowserMenuItem.Name = "certificateBrowserMenuItem";
			this.certificateBrowserMenuItem.Size = new System.Drawing.Size(182, 22);
			this.certificateBrowserMenuItem.Text = resources.GetString("this.certificateBrowserMenuItem.Text");
			this.certificateBrowserMenuItem.Click += new System.EventHandler(this.certificateBrowserMenuItem_Click);
			// 
			// shipBrowserMenuItem
			// 
			this.shipBrowserMenuItem.Name = "shipBrowserMenuItem";
			this.shipBrowserMenuItem.Size = new System.Drawing.Size(182, 22);
			this.shipBrowserMenuItem.Text = resources.GetString("this.shipBrowserMenuItem.Text");
			this.shipBrowserMenuItem.Click += new System.EventHandler(this.shipBrowserMenuItem_Click);
			// 
			// itemBrowserMenuItem
			// 
			this.itemBrowserMenuItem.Name = "itemBrowserMenuItem";
			this.itemBrowserMenuItem.Size = new System.Drawing.Size(182, 22);
			this.itemBrowserMenuItem.Text = resources.GetString("this.itemBrowserMenuItem.Text");
			this.itemBrowserMenuItem.Click += new System.EventHandler(this.itemBrowserMenuItem_Click);
			// 
			// blueprintBrowserMenuItem
			// 
			this.blueprintBrowserMenuItem.Name = "blueprintBrowserMenuItem";
			this.blueprintBrowserMenuItem.Size = new System.Drawing.Size(182, 22);
			this.blueprintBrowserMenuItem.Text = resources.GetString("this.blueprintBrowserMenuItem.Text");
			this.blueprintBrowserMenuItem.Click += new System.EventHandler(this.blueprintBrowserMenuItem_Click);
			// 
			// skillsPieChartMenuItem
			// 
			this.skillsPieChartMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("skillsPieChartMenuItem.Image")));
			this.skillsPieChartMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.skillsPieChartMenuItem.Name = "skillsPieChartMenuItem";
			this.skillsPieChartMenuItem.Size = new System.Drawing.Size(208, 22);
			this.skillsPieChartMenuItem.Text = resources.GetString("this.skillsPieChartMenuItem.Text");
			this.skillsPieChartMenuItem.Click += new System.EventHandler(this.tsSkillsPieChartTool_Click);
			// 
			// firstSeparator
			// 
			this.firstSeparator.Name = "firstSeparator";
			this.firstSeparator.Size = new System.Drawing.Size(205, 6);
			// 
			// implantsMenuItem
			// 
			this.implantsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("implantsMenuItem.Image")));
			this.implantsMenuItem.Name = "implantsMenuItem";
			this.implantsMenuItem.Size = new System.Drawing.Size(208, 22);
			this.implantsMenuItem.Text = resources.GetString("this.implantsMenuItem.Text");
			this.implantsMenuItem.Click += new System.EventHandler(this.manualImplantGroupsToolStripMenuItem_Click);
			// 
			// showOwnedSkillbooksMenuItem
			// 
			this.showOwnedSkillbooksMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showOwnedSkillbooksMenuItem.Image")));
			this.showOwnedSkillbooksMenuItem.Name = "showOwnedSkillbooksMenuItem";
			this.showOwnedSkillbooksMenuItem.Size = new System.Drawing.Size(208, 22);
			this.showOwnedSkillbooksMenuItem.Text = resources.GetString("this.showOwnedSkillbooksMenuItem.Text");
			this.showOwnedSkillbooksMenuItem.Click += new System.EventHandler(this.tsShowOwnedSkillbooks_Click);
			// 
			// secondSeparator
			// 
			this.secondSeparator.Name = "secondSeparator";
			this.secondSeparator.Size = new System.Drawing.Size(205, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsToolStripMenuItem.Image")));
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.optionsToolStripMenuItem.Text = resources.GetString("this.optionsToolStripMenuItem.Text");
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forumsMenuItem,
            this.issuesFeaturesMenuItem,
            this.readTheDocsManualToolStripMenuItem,
            this.helpAboutKnownProblemsToolStripSeparator,
            this.aboutMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = resources.GetString("this.helpToolStripMenuItem.Text");
			// 
			// forumsMenuItem
			// 
			this.forumsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("forumsMenuItem.Image")));
			this.forumsMenuItem.Name = "forumsMenuItem";
			this.forumsMenuItem.Size = new System.Drawing.Size(224, 22);
			this.forumsMenuItem.Text = resources.GetString("this.forumsMenuItem.Text");
			this.forumsMenuItem.Click += new System.EventHandler(this.forumsMenuItem_Click);
			// 
			// issuesFeaturesMenuItem
			// 
			this.issuesFeaturesMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("issuesFeaturesMenuItem.Image")));
			this.issuesFeaturesMenuItem.Name = "issuesFeaturesMenuItem";
			this.issuesFeaturesMenuItem.Size = new System.Drawing.Size(224, 22);
			this.issuesFeaturesMenuItem.Text = resources.GetString("this.issuesFeaturesMenuItem.Text");
			this.issuesFeaturesMenuItem.Click += new System.EventHandler(this.issuesFeaturesMenuItem_Click);
			// 
			// readTheDocsManualToolStripMenuItem
			// 
			this.readTheDocsManualToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("readTheDocsManualToolStripMenuItem.Image")));
			this.readTheDocsManualToolStripMenuItem.Name = "readTheDocsManualToolStripMenuItem";
			this.readTheDocsManualToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.readTheDocsManualToolStripMenuItem.Text = resources.GetString("this.readTheDocsManualToolStripMenuItem.Text");
			this.readTheDocsManualToolStripMenuItem.Click += new System.EventHandler(this.readTheDocsManualMenuItem_Click);
			// 
			// helpAboutKnownProblemsToolStripSeparator
			// 
			this.helpAboutKnownProblemsToolStripSeparator.Name = "helpAboutKnownProblemsToolStripSeparator";
			this.helpAboutKnownProblemsToolStripSeparator.Size = new System.Drawing.Size(221, 6);
			// 
			// aboutMenuItem
			// 
			this.aboutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutMenuItem.Image")));
			this.aboutMenuItem.Name = "aboutMenuItem";
			this.aboutMenuItem.Size = new System.Drawing.Size(224, 22);
			this.aboutMenuItem.Text = resources.GetString("this.aboutMenuItem.Text");
			this.aboutMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// testsToolStripMenuItem
			// 
			this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exceptionWindowToolStripMenuItem,
            this.exceptionWindowRecursiveExceptionToolStripMenuItem,
            this.testNotificationToolStripMenuItem,
            this.testCharacterNotificationToolStripMenuItem,
            this.testTimeoutOneSecToolStripMenuItem,
            this.restartToolStripMenuItem});
			this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
			this.testsToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.testsToolStripMenuItem.Text = resources.GetString("this.testsToolStripMenuItem.Text");
			this.testsToolStripMenuItem.Visible = false;
			this.testsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.testToolStripMenuItem_DropDownOpening);
			// 
			// exceptionWindowToolStripMenuItem
			// 
			this.exceptionWindowToolStripMenuItem.Name = "exceptionWindowToolStripMenuItem";
			this.exceptionWindowToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
			this.exceptionWindowToolStripMenuItem.Text = resources.GetString("this.exceptionWindowToolStripMenuItem.Text");
			this.exceptionWindowToolStripMenuItem.Click += new System.EventHandler(this.ExceptionWindowToolStripMenuItem_Click);
			// 
			// exceptionWindowRecursiveExceptionToolStripMenuItem
			// 
			this.exceptionWindowRecursiveExceptionToolStripMenuItem.Name = "exceptionWindowRecursiveExceptionToolStripMenuItem";
			this.exceptionWindowRecursiveExceptionToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
			this.exceptionWindowRecursiveExceptionToolStripMenuItem.Text = resources.GetString("this.exceptionWindowRecursiveExceptionToolStripMenuItem.Text");
			this.exceptionWindowRecursiveExceptionToolStripMenuItem.Click += new System.EventHandler(this.exceptionWindowRecursiveExceptionToolStripMenuItem_Click);
			// 
			// testNotificationToolStripMenuItem
			// 
			this.testNotificationToolStripMenuItem.Name = "testNotificationToolStripMenuItem";
			this.testNotificationToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
			this.testNotificationToolStripMenuItem.Text = resources.GetString("this.testNotificationToolStripMenuItem.Text");
			this.testNotificationToolStripMenuItem.Click += new System.EventHandler(this.testNotificationToolstripMenuItem_Click);
			// 
			// testCharacterNotificationToolStripMenuItem
			// 
			this.testCharacterNotificationToolStripMenuItem.Name = "testCharacterNotificationToolStripMenuItem";
			this.testCharacterNotificationToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
			this.testCharacterNotificationToolStripMenuItem.Text = resources.GetString("this.testCharacterNotificationToolStripMenuItem.Text");
			this.testCharacterNotificationToolStripMenuItem.Click += new System.EventHandler(this.testCharacterNotificationToolStripMenuItem_Click);
			// 
			// testTimeoutOneSecToolStripMenuItem
			// 
			this.testTimeoutOneSecToolStripMenuItem.Name = "testTimeoutOneSecToolStripMenuItem";
			this.testTimeoutOneSecToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
			this.testTimeoutOneSecToolStripMenuItem.Text = resources.GetString("this.testTimeoutOneSecToolStripMenuItem.Text");
			this.testTimeoutOneSecToolStripMenuItem.Click += new System.EventHandler(this.testTimeoutOneSecToolStripMenuItem_Click);
			// 
			// restartToolStripMenuItem
			// 
			this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
			this.restartToolStripMenuItem.Size = new System.Drawing.Size(287, 22);
			this.restartToolStripMenuItem.Text = resources.GetString("this.restartToolStripMenuItem.Text");
			this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
			// 
			// mainToolBar
			// 
			this.mainToolBar.ContextMenuStrip = this.toolbarContext;
			this.mainToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainToolBar.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.mainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAPIKeyTbMenu,
            this.apiKeysManagementTbMenu,
            this.apiKeysSettingsToolStripSeparator,
            this.manageCharacterTbMenu,
            this.manageCharacterToolStripSeparator,
            this.tsdbSettings,
            this.settingsToolStripSeparator,
            this.clearCacheToolStripButton,
            this.resetSettingsToolStripButton,
            this.cacheResetToolStripSeparator,
            this.exitToolStripButton,
            this.exitToolStripSeparator,
            this.tsbManagePlans,
            this.plansTbMenu,
            this.plansToolStripSeparator,
            this.characterComparisonToolStripButton,
            this.tsbMineralSheet,
            this.skillsPieChartTbMenu,
            this.tsbSchedule,
            this.tsbImplantGroups,
            this.tsbShowOwned,
            this.toolsToolStripSeparator,
            this.tsbOptions,
            this.tsbAbout});
			this.mainToolBar.Location = new System.Drawing.Point(0, 24);
			this.mainToolBar.Name = "mainToolBar";
			this.mainToolBar.Size = new System.Drawing.Size(600, 25);
			this.mainToolBar.TabIndex = 5;
			this.mainToolBar.Text = resources.GetString("this.mainToolBar.Text");
			this.mainToolBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainToolBar_MouseDown);
			this.mainToolBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainToolBar_MouseMove);
			// 
			// addAPIKeyTbMenu
			// 
			this.addAPIKeyTbMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addAPIKeyTbMenu.Image = ((System.Drawing.Image)(resources.GetObject("addAPIKeyTbMenu.Image")));
			this.addAPIKeyTbMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addAPIKeyTbMenu.Name = "addAPIKeyTbMenu";
			this.addAPIKeyTbMenu.Size = new System.Drawing.Size(23, 22);
			this.addAPIKeyTbMenu.ToolTipText = resources.GetString("this.addAPIKeyTbMenu.ToolTipText");
			this.addAPIKeyTbMenu.Click += new System.EventHandler(this.addAPIKeyMenu_Click);
			// 
			// apiKeysManagementTbMenu
			// 
			this.apiKeysManagementTbMenu.Image = ((System.Drawing.Image)(resources.GetObject("apiKeysManagementTbMenu.Image")));
			this.apiKeysManagementTbMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.apiKeysManagementTbMenu.Name = "apiKeysManagementTbMenu";
			this.apiKeysManagementTbMenu.Size = new System.Drawing.Size(23, 22);
			this.apiKeysManagementTbMenu.ToolTipText = resources.GetString("this.apiKeysManagementTbMenu.ToolTipText");
			this.apiKeysManagementTbMenu.Click += new System.EventHandler(this.manageAPIKeysMenuItem_Click);
			// 
			// apiKeysSettingsToolStripSeparator
			// 
			this.apiKeysSettingsToolStripSeparator.Name = "apiKeysSettingsToolStripSeparator";
			this.apiKeysSettingsToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// manageCharacterTbMenu
			// 
			this.manageCharacterTbMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.manageCharacterTbMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
			this.manageCharacterTbMenu.Image = ((System.Drawing.Image)(resources.GetObject("manageCharacterTbMenu.Image")));
			this.manageCharacterTbMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.manageCharacterTbMenu.Name = "manageCharacterTbMenu";
			this.manageCharacterTbMenu.Size = new System.Drawing.Size(29, 22);
			this.manageCharacterTbMenu.Text = resources.GetString("this.manageCharacterTbMenu.Text");
			this.manageCharacterTbMenu.ToolTipText = resources.GetString("this.manageCharacterTbMenu.ToolTipText");
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
			this.toolStripMenuItem1.Text = resources.GetString("this.toolStripMenuItem1.Text");
			this.toolStripMenuItem1.Click += new System.EventHandler(this.deleteCharacterMenu_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(170, 22);
			this.toolStripMenuItem2.Text = resources.GetString("this.toolStripMenuItem2.Text");
			this.toolStripMenuItem2.Click += new System.EventHandler(this.saveCharacterInfosMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(170, 22);
			this.toolStripMenuItem3.Text = resources.GetString("this.toolStripMenuItem3.Text");
			this.toolStripMenuItem3.Click += new System.EventHandler(this.hideCharacterMenu_Click);
			// 
			// manageCharacterToolStripSeparator
			// 
			this.manageCharacterToolStripSeparator.Name = "manageCharacterToolStripSeparator";
			this.manageCharacterToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// tsdbSettings
			// 
			this.tsdbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsdbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLoadSettings,
            this.tsSaveSettings});
			this.tsdbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsdbSettings.Image")));
			this.tsdbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsdbSettings.Name = "tsdbSettings";
			this.tsdbSettings.Size = new System.Drawing.Size(29, 22);
			this.tsdbSettings.Text = resources.GetString("this.tsdbSettings.Text");
			this.tsdbSettings.ToolTipText = resources.GetString("this.tsdbSettings.ToolTipText");
			// 
			// tsLoadSettings
			// 
			this.tsLoadSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsLoadSettings.Image")));
			this.tsLoadSettings.Name = "tsLoadSettings";
			this.tsLoadSettings.Size = new System.Drawing.Size(167, 22);
			this.tsLoadSettings.Text = resources.GetString("this.tsLoadSettings.Text");
			this.tsLoadSettings.Click += new System.EventHandler(this.loadSettingsToolStripMenuItem_Click);
			// 
			// tsSaveSettings
			// 
			this.tsSaveSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveSettings.Image")));
			this.tsSaveSettings.Name = "tsSaveSettings";
			this.tsSaveSettings.Size = new System.Drawing.Size(167, 22);
			this.tsSaveSettings.Text = resources.GetString("this.tsSaveSettings.Text");
			this.tsSaveSettings.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
			// 
			// settingsToolStripSeparator
			// 
			this.settingsToolStripSeparator.Name = "settingsToolStripSeparator";
			this.settingsToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// clearCacheToolStripButton
			// 
			this.clearCacheToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearCacheToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearCacheToolStripButton.Image")));
			this.clearCacheToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.clearCacheToolStripButton.Name = "clearCacheToolStripButton";
			this.clearCacheToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.clearCacheToolStripButton.Text = resources.GetString("this.clearCacheToolStripButton.Text");
			this.clearCacheToolStripButton.Click += new System.EventHandler(this.clearCacheToolStripMenuItem_Click);
			// 
			// resetSettingsToolStripButton
			// 
			this.resetSettingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.resetSettingsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("resetSettingsToolStripButton.Image")));
			this.resetSettingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.resetSettingsToolStripButton.Name = "resetSettingsToolStripButton";
			this.resetSettingsToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.resetSettingsToolStripButton.Text = resources.GetString("this.resetSettingsToolStripButton.Text");
			this.resetSettingsToolStripButton.Click += new System.EventHandler(this.resetSettingsToolStripMenuItem_Click);
			// 
			// cacheResetToolStripSeparator
			// 
			this.cacheResetToolStripSeparator.Name = "cacheResetToolStripSeparator";
			this.cacheResetToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// exitToolStripButton
			// 
			this.exitToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.exitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripButton.Image")));
			this.exitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.exitToolStripButton.Name = "exitToolStripButton";
			this.exitToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.exitToolStripButton.Text = resources.GetString("this.exitToolStripButton.Text");
			this.exitToolStripButton.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// exitToolStripSeparator
			// 
			this.exitToolStripSeparator.Name = "exitToolStripSeparator";
			this.exitToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbManagePlans
			// 
			this.tsbManagePlans.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbManagePlans.Image = ((System.Drawing.Image)(resources.GetObject("tsbManagePlans.Image")));
			this.tsbManagePlans.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbManagePlans.Name = "tsbManagePlans";
			this.tsbManagePlans.Size = new System.Drawing.Size(23, 22);
			this.tsbManagePlans.Text = resources.GetString("this.tsbManagePlans.Text");
			this.tsbManagePlans.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
			// 
			// plansTbMenu
			// 
			this.plansTbMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.plansTbMenu.Image = ((System.Drawing.Image)(resources.GetObject("plansTbMenu.Image")));
			this.plansTbMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.plansTbMenu.Name = "plansTbMenu";
			this.plansTbMenu.Size = new System.Drawing.Size(57, 22);
			this.plansTbMenu.Text = resources.GetString("this.plansTbMenu.Text");
			this.plansTbMenu.DropDownOpening += new System.EventHandler(this.tsdbPlans_DropDownOpening);
			// 
			// plansToolStripSeparator
			// 
			this.plansToolStripSeparator.Name = "plansToolStripSeparator";
			this.plansToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// characterComparisonToolStripButton
			// 
			this.characterComparisonToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.characterComparisonToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("characterComparisonToolStripButton.Image")));
			this.characterComparisonToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.characterComparisonToolStripButton.Name = "characterComparisonToolStripButton";
			this.characterComparisonToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.characterComparisonToolStripButton.Text = resources.GetString("this.characterComparisonToolStripButton.Text");
			this.characterComparisonToolStripButton.Click += new System.EventHandler(this.charactersComparisonToolStripMenuItem_Click);
			// 
			// tsbMineralSheet
			// 
			this.tsbMineralSheet.Name = "tsbMineralSheet";
			this.tsbMineralSheet.Size = new System.Drawing.Size(23, 22);
			// 
			// skillsPieChartTbMenu
			// 
			this.skillsPieChartTbMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.skillsPieChartTbMenu.Image = ((System.Drawing.Image)(resources.GetObject("skillsPieChartTbMenu.Image")));
			this.skillsPieChartTbMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.skillsPieChartTbMenu.Name = "skillsPieChartTbMenu";
			this.skillsPieChartTbMenu.Size = new System.Drawing.Size(23, 22);
			this.skillsPieChartTbMenu.Text = resources.GetString("this.skillsPieChartTbMenu.Text");
			this.skillsPieChartTbMenu.Click += new System.EventHandler(this.tsSkillsPieChartTool_Click);
			// 
			// tsbSchedule
			// 
			this.tsbSchedule.Name = "tsbSchedule";
			this.tsbSchedule.Size = new System.Drawing.Size(23, 22);
			// 
			// tsbImplantGroups
			// 
			this.tsbImplantGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbImplantGroups.Image = ((System.Drawing.Image)(resources.GetObject("tsbImplantGroups.Image")));
			this.tsbImplantGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbImplantGroups.Name = "tsbImplantGroups";
			this.tsbImplantGroups.Size = new System.Drawing.Size(23, 22);
			this.tsbImplantGroups.Text = resources.GetString("this.tsbImplantGroups.Text");
			this.tsbImplantGroups.Click += new System.EventHandler(this.manualImplantGroupsToolStripMenuItem_Click);
			// 
			// tsbShowOwned
			// 
			this.tsbShowOwned.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbShowOwned.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowOwned.Image")));
			this.tsbShowOwned.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbShowOwned.Name = "tsbShowOwned";
			this.tsbShowOwned.Size = new System.Drawing.Size(23, 22);
			this.tsbShowOwned.Text = resources.GetString("this.tsbShowOwned.Text");
			this.tsbShowOwned.Click += new System.EventHandler(this.tsShowOwnedSkillbooks_Click);
			// 
			// toolsToolStripSeparator
			// 
			this.toolsToolStripSeparator.Name = "toolsToolStripSeparator";
			this.toolsToolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbOptions
			// 
			this.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
			this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOptions.Name = "tsbOptions";
			this.tsbOptions.Size = new System.Drawing.Size(23, 22);
			this.tsbOptions.Text = resources.GetString("this.tsbOptions.Text");
			this.tsbOptions.Click += new System.EventHandler(this.optionsMenuItem_Click);
			// 
			// tsbAbout
			// 
			this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
			this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAbout.Name = "tsbAbout";
			this.tsbAbout.Size = new System.Drawing.Size(23, 22);
			this.tsbAbout.Text = resources.GetString("this.tsbAbout.Text");
			this.tsbAbout.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// niAlertIcon
			// 
			this.niAlertIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niAlertIcon.Icon")));
			this.niAlertIcon.Text = resources.GetString("this.niAlertIcon.Text");
			this.niAlertIcon.BalloonTipClicked += new System.EventHandler(this.niAlertIcon_BalloonTipClicked);
			this.niAlertIcon.Click += new System.EventHandler(this.niAlertIcon_Click);
			this.niAlertIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.niAlertIcon_MouseClick);
			// 
			// ofdOpenDialog
			// 
			this.ofdOpenDialog.Filter = resources.GetString("this.ofdOpenDialog.Filter");
			this.ofdOpenDialog.Title = resources.GetString("this.ofdOpenDialog.Title");
			// 
			// tcCharacterTabs
			// 
			this.tcCharacterTabs.AllowDrop = true;
			this.tcCharacterTabs.Controls.Add(this.tpOverview);
			this.tcCharacterTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcCharacterTabs.Location = new System.Drawing.Point(0, 49);
			this.tcCharacterTabs.Multiline = true;
			this.tcCharacterTabs.Name = "tcCharacterTabs";
			this.tcCharacterTabs.Padding = new System.Drawing.Point(0, 0);
			this.tcCharacterTabs.SelectedIndex = 0;
			this.tcCharacterTabs.ShowToolTips = true;
			this.tcCharacterTabs.Size = new System.Drawing.Size(600, 503);
			this.tcCharacterTabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tcCharacterTabs.TabIndex = 0;
			this.tcCharacterTabs.SelectedIndexChanged += new System.EventHandler(this.tcCharacterTabs_SelectedIndexChanged);
			this.tcCharacterTabs.DragDrop += new System.Windows.Forms.DragEventHandler(this.tcCharacterTabs_DragDrop);
			// 
			// tpOverview
			// 
			this.tpOverview.Controls.Add(this.overview);
			this.tpOverview.Location = new System.Drawing.Point(4, 22);
			this.tpOverview.Name = "tpOverview";
			this.tpOverview.Size = new System.Drawing.Size(592, 477);
			this.tpOverview.TabIndex = 0;
			this.tpOverview.Text = resources.GetString("this.tpOverview.Text");
			this.tpOverview.UseVisualStyleBackColor = true;
			// 
			// overview
			// 
			this.overview.AutoScroll = true;
			this.overview.BackColor = System.Drawing.Color.Transparent;
			this.overview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.overview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.overview.Location = new System.Drawing.Point(0, 0);
			this.overview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.overview.Name = "overview";
			this.overview.Size = new System.Drawing.Size(592, 477);
			this.overview.TabIndex = 0;
			this.overview.CharacterClicked += new System.EventHandler<EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs>(this.overview_CharacterClicked);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenuStrip = this.trayIconContextMenuStrip;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.MouseHoverTime = 300;
			this.trayIcon.Text = "";
			this.trayIcon.Click += new System.EventHandler(this.trayIcon_Click);
			this.trayIcon.MouseHover += new System.EventHandler(this.trayIcon_MouseHover);
			this.trayIcon.MouseLeave += new System.EventHandler(this.trayIcon_MouseLeave);
			// 
			// tabLoadingLabel
			// 
			this.tabLoadingLabel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tabLoadingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabLoadingLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.tabLoadingLabel.Location = new System.Drawing.Point(0, 49);
			this.tabLoadingLabel.Name = "tabLoadingLabel";
			this.tabLoadingLabel.Size = new System.Drawing.Size(600, 503);
			this.tabLoadingLabel.TabIndex = 7;
			this.tabLoadingLabel.Text = resources.GetString("this.tabLoadingLabel.Text");
			this.tabLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// noCharactersLabel
			// 
			this.noCharactersLabel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.noCharactersLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noCharactersLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.noCharactersLabel.Location = new System.Drawing.Point(0, 49);
			this.noCharactersLabel.Name = "noCharactersLabel";
			this.noCharactersLabel.Size = new System.Drawing.Size(600, 503);
			this.noCharactersLabel.TabIndex = 8;
			this.noCharactersLabel.Text = "No character loaded or monitored\r\n\r\nTo add characters, click the File > Add API k" +
    "ey... menu option\r\nTo monitor characters, click the File > Manage API Keys... me" +
    "nu option";
			this.noCharactersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mainLoadingThrobber
			// 
			this.mainLoadingThrobber.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.mainLoadingThrobber.Location = new System.Drawing.Point(285, 314);
			this.mainLoadingThrobber.MaximumSize = new System.Drawing.Size(24, 24);
			this.mainLoadingThrobber.MinimumSize = new System.Drawing.Size(24, 24);
			this.mainLoadingThrobber.Name = "mainLoadingThrobber";
			this.mainLoadingThrobber.Size = new System.Drawing.Size(24, 24);
			this.mainLoadingThrobber.State = EVEMon.Common.Enumerations.ThrobberState.Rotating;
			this.mainLoadingThrobber.TabIndex = 1;
			this.mainLoadingThrobber.TabStop = false;
			// 
			// notificationList
			// 
			this.notificationList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.notificationList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
			this.notificationList.Location = new System.Drawing.Point(0, 552);
			this.notificationList.Margin = new System.Windows.Forms.Padding(0);
			this.notificationList.Name = "notificationList";
			this.notificationList.Size = new System.Drawing.Size(600, 72);
			this.notificationList.TabIndex = 6;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 646);
			this.Controls.Add(this.tcCharacterTabs);
			this.Controls.Add(this.mainLoadingThrobber);
			this.Controls.Add(this.tabLoadingLabel);
			this.Controls.Add(this.noCharactersLabel);
			this.Controls.Add(this.notificationList);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.mainToolBar);
			this.Controls.Add(this.mainMenuBar);
			this.MainMenuStrip = this.mainMenuBar;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimumSize = new System.Drawing.Size(614, 344);
			this.Name = "MainWindow";
			this.trayIconContextMenuStrip.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.mainMenuBar.ResumeLayout(false);
			this.mainMenuBar.PerformLayout();
			this.toolbarContext.ResumeLayout(false);
			this.mainToolBar.ResumeLayout(false);
			this.mainToolBar.PerformLayout();
			this.tcCharacterTabs.ResumeLayout(false);
			this.tpOverview.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mainLoadingThrobber)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ContextMenuStrip trayIconContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planToolStripMenuItem;
        private EVEMon.Common.Controls.DraggableTabControl tcCharacterTabs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel lblServerStatus;
        private System.Windows.Forms.MenuStrip mainMenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageAPIKeysMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileRestoreResetToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileResetExitToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exportCharacterMenu;
        private System.Windows.Forms.ToolStripSeparator fileExportSaveToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem plansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiManagePlans;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewPlan;
        private System.Windows.Forms.ToolStripStatusLabel lblTraining;
        private System.Windows.Forms.ContextMenuStrip toolbarContext;
        private System.Windows.Forms.ToolStripMenuItem menubarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolbarToolStripMenuItem;
        private System.Windows.Forms.ToolStrip mainToolBar;
        private System.Windows.Forms.ToolStripButton apiKeysManagementTbMenu;
        private System.Windows.Forms.ToolStripSeparator apiKeysSettingsToolStripSeparator;
        private System.Windows.Forms.ToolStripDropDownButton tsdbSettings;
        private System.Windows.Forms.ToolStripMenuItem tsSaveSettings;
        private System.Windows.Forms.ToolStripMenuItem tsLoadSettings;
        private System.Windows.Forms.ToolStripSeparator settingsToolStripSeparator;
        private System.Windows.Forms.ToolStripButton tsbMineralSheet;
        private System.Windows.Forms.ToolStripButton skillsPieChartTbMenu;
        private System.Windows.Forms.ToolStripButton tsbSchedule;
        private System.Windows.Forms.ToolStripButton tsbOptions;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripButton tsbManagePlans;
        private System.Windows.Forms.ToolStripDropDownButton plansTbMenu;
        private System.Windows.Forms.ToolStripSeparator plansToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySkillsToClipboardBBFormatToolStripMenuItem;
        private EVEMon.Common.Controls.TrayIcon trayIcon;
        private System.Windows.Forms.ToolStripSeparator helpAboutKnownProblemsToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem forumsMenuItem;
        private EVEMon.Controls.NotificationList notificationList;
        private System.Windows.Forms.NotifyIcon niAlertIcon;
        private System.Windows.Forms.ToolStripMenuItem addAPIKeyMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteCharacterMenu;
        private System.Windows.Forms.ToolStripSeparator fileManagerHideToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem hideCharacterMenu;
        private System.Windows.Forms.ToolStripButton addAPIKeyTbMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportPlanFromFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenDialog;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exceptionWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exceptionWindowRecursiveExceptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator plansStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem issuesFeaturesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator testsToolStripSeperator;
        private System.Windows.Forms.ToolStripMenuItem testNotificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayTestNotificationTSMI;
        private System.Windows.Forms.ToolStripMenuItem testCharacterNotificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayTestCharacterNotificationTSMI;
        private System.Windows.Forms.ToolStripMenuItem testTimeoutOneSecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator plansSeparator;
        private System.Windows.Forms.ToolStripButton characterComparisonToolStripButton;
        private System.Windows.Forms.ToolStripSeparator manageCharacterToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator cacheResetToolStripSeparator;
        private System.Windows.Forms.ToolStripButton exitToolStripButton;
        private System.Windows.Forms.ToolStripSeparator exitToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolsToolStripSeparator;
        private System.Windows.Forms.ToolStripDropDownButton manageCharacterTbMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripButton clearCacheToolStripButton;
        private System.Windows.Forms.ToolStripButton resetSettingsToolStripButton;
        private System.Windows.Forms.ToolStripButton tsbImplantGroups;
        private System.Windows.Forms.ToolStripButton tsbShowOwned;
        private System.Windows.Forms.Label tabLoadingLabel;
        private System.Windows.Forms.Label noCharactersLabel;
        private System.Windows.Forms.TabPage tpOverview;
        private Controls.Overview overview;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSpacerLabel;
        private System.Windows.Forms.ToolStripStatusLabel lblCSSProviderStatus;
        private Common.Controls.Throbber mainLoadingThrobber;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar tsDatafilesLoadingProgressBar;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreatePlanFromSkillQueue;
        private System.Windows.Forms.ToolStripMenuItem readTheDocsManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem charactersComparisonMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataBrowserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skillBrowserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certificateBrowserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shipBrowserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemBrowserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueprintBrowserMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skillsPieChartMenuItem;
        private System.Windows.Forms.ToolStripSeparator firstSeparator;
        private System.Windows.Forms.ToolStripMenuItem implantsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOwnedSkillbooksMenuItem;
        private System.Windows.Forms.ToolStripSeparator secondSeparator;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blankCreatorToolStripMenuItem;
    }
}
