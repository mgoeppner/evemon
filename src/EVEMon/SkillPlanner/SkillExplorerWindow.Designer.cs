using EVEMon.Common.Controls;

namespace EVEMon.SkillPlanner
{
    partial class SkillExplorerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillExplorerWindow));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode(resources.GetString("treeNode1.Text"));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode(resources.GetString("treeNode2.Text"));
            this.grpPlanName = new System.Windows.Forms.GroupBox();
            this.lblSkillInfo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lowerPanel = new System.Windows.Forms.Panel();
            this.lblRedFont = new System.Windows.Forms.Label();
            this.lblDimmedFont = new System.Windows.Forms.Label();
            this.lblRedFontInfo = new System.Windows.Forms.Label();
            this.lblDimmedFontInfo = new System.Windows.Forms.Label();
            this.lblNormalFont = new System.Windows.Forms.Label();
            this.middlePanel = new System.Windows.Forms.Panel();
            this.splitContainer = new EVEMon.Common.Controls.PersistentSplitContainer();
            this.tvSkills = new System.Windows.Forms.TreeView();
            this.pnlSkillHeader = new System.Windows.Forms.Panel();
            this.lblSkills = new System.Windows.Forms.Label();
            this.tvEntity = new System.Windows.Forms.TreeView();
            this.pnlItemHeader = new System.Windows.Forms.Panel();
            this.lblItems = new System.Windows.Forms.Label();
            this.cmSkills = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.planToMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddL1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddL2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddL3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddL4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddL5 = new System.Windows.Forms.ToolStripMenuItem();
            this.planToSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInSkillBrowserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showInBrowserSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.showInSkillExplorerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showPrerequisitiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showInMenuSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSkillsExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSkillsCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmEntity = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.planToObject = new System.Windows.Forms.ToolStripMenuItem();
            this.planToObjectSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.showObjectInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showObjectInBrowserSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.showObjectPrerequisitiesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showObjectInMenuSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiObjectsExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiObjectsCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbHistory = new System.Windows.Forms.ComboBox();
            this.cbShowBaseOnly = new System.Windows.Forms.CheckBox();
            this.rbShowAlpha = new System.Windows.Forms.RadioButton();
            this.rbShowTree = new System.Windows.Forms.RadioButton();
            this.tmrAutoUpdate = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpPlanName.SuspendLayout();
            this.lowerPanel.SuspendLayout();
            this.middlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlSkillHeader.SuspendLayout();
            this.pnlItemHeader.SuspendLayout();
            this.cmSkills.SuspendLayout();
            this.cmEntity.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPlanName
            // 
            this.grpPlanName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPlanName.Controls.Add(this.lblSkillInfo);
            this.grpPlanName.Location = new System.Drawing.Point(12, 12);
            this.grpPlanName.Name = "grpPlanName";
            this.grpPlanName.Size = new System.Drawing.Size(560, 40);
            this.grpPlanName.TabIndex = 0;
            this.grpPlanName.TabStop = false;
            this.grpPlanName.Text = resources.GetString("this.grpPlanName.Text");
            // 
            // lblSkillInfo
            // 
            this.lblSkillInfo.AutoSize = true;
            this.lblSkillInfo.Location = new System.Drawing.Point(12, 16);
            this.lblSkillInfo.Name = "lblSkillInfo";
            this.lblSkillInfo.Size = new System.Drawing.Size(47, 13);
            this.lblSkillInfo.TabIndex = 0;
            this.lblSkillInfo.Text = resources.GetString("this.lblSkillInfo.Text");
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(482, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = resources.GetString("this.btnClose.Text");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lowerPanel
            // 
            this.lowerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerPanel.Controls.Add(this.lblRedFont);
            this.lowerPanel.Controls.Add(this.lblDimmedFont);
            this.lowerPanel.Controls.Add(this.lblRedFontInfo);
            this.lowerPanel.Controls.Add(this.lblDimmedFontInfo);
            this.lowerPanel.Controls.Add(this.lblNormalFont);
            this.lowerPanel.Controls.Add(this.btnClose);
            this.lowerPanel.Location = new System.Drawing.Point(12, 389);
            this.lowerPanel.Name = "lowerPanel";
            this.lowerPanel.Size = new System.Drawing.Size(557, 47);
            this.lowerPanel.TabIndex = 4;
            // 
            // lblRedFont
            // 
            this.lblRedFont.AutoSize = true;
            this.lblRedFont.ForeColor = System.Drawing.Color.Red;
            this.lblRedFont.Location = new System.Drawing.Point(337, 3);
            this.lblRedFont.Name = "lblRedFont";
            this.lblRedFont.Size = new System.Drawing.Size(27, 13);
            this.lblRedFont.TabIndex = 8;
            this.lblRedFont.Text = resources.GetString("this.lblRedFont.Text");
            // 
            // lblDimmedFont
            // 
            this.lblDimmedFont.AutoSize = true;
            this.lblDimmedFont.ForeColor = System.Drawing.Color.DimGray;
            this.lblDimmedFont.Location = new System.Drawing.Point(153, 3);
            this.lblDimmedFont.Name = "lblDimmedFont";
            this.lblDimmedFont.Size = new System.Drawing.Size(45, 13);
            this.lblDimmedFont.TabIndex = 6;
            this.lblDimmedFont.Text = resources.GetString("this.lblDimmedFont.Text");
            // 
            // lblRedFontInfo
            // 
            this.lblRedFontInfo.AutoSize = true;
            this.lblRedFontInfo.Location = new System.Drawing.Point(361, 3);
            this.lblRedFontInfo.Name = "lblRedFontInfo";
            this.lblRedFontInfo.Size = new System.Drawing.Size(151, 13);
            this.lblRedFontInfo.TabIndex = 9;
            this.lblRedFontInfo.Text = resources.GetString("this.lblRedFontInfo.Text");
            // 
            // lblDimmedFontInfo
            // 
            this.lblDimmedFontInfo.AutoSize = true;
            this.lblDimmedFontInfo.Location = new System.Drawing.Point(194, 3);
            this.lblDimmedFontInfo.Name = "lblDimmedFontInfo";
            this.lblDimmedFontInfo.Size = new System.Drawing.Size(138, 13);
            this.lblDimmedFontInfo.TabIndex = 7;
            this.lblDimmedFontInfo.Text = resources.GetString("this.lblDimmedFontInfo.Text");
            // 
            // lblNormalFont
            // 
            this.lblNormalFont.AutoSize = true;
            this.lblNormalFont.Location = new System.Drawing.Point(6, 3);
            this.lblNormalFont.Name = "lblNormalFont";
            this.lblNormalFont.Size = new System.Drawing.Size(142, 13);
            this.lblNormalFont.TabIndex = 5;
            this.lblNormalFont.Text = resources.GetString("this.lblNormalFont.Text");
            // 
            // middlePanel
            // 
            this.middlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.middlePanel.Controls.Add(this.splitContainer);
            this.middlePanel.Location = new System.Drawing.Point(12, 115);
            this.middlePanel.Name = "middlePanel";
            this.middlePanel.Size = new System.Drawing.Size(560, 268);
            this.middlePanel.TabIndex = 5;
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tvSkills);
            this.splitContainer.Panel1.Controls.Add(this.pnlSkillHeader);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tvEntity);
            this.splitContainer.Panel2.Controls.Add(this.pnlItemHeader);
            this.splitContainer.RememberDistanceKey = null;
            this.splitContainer.Size = new System.Drawing.Size(560, 268);
            this.splitContainer.SplitterDistance = 278;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 0;
            // 
            // tvSkills
            // 
            this.tvSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSkills.Location = new System.Drawing.Point(0, 18);
            this.tvSkills.Name = "tvSkills";
            treeNode1.Name = "Node0";
            treeNode1.Text = resources.GetString("treeNode1.Text");
            this.tvSkills.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvSkills.ShowNodeToolTips = true;
            this.tvSkills.Size = new System.Drawing.Size(274, 246);
            this.tvSkills.TabIndex = 0;
            this.tvSkills.DoubleClick += new System.EventHandler(this.tvSkills_DoubleClick);
            // 
            // pnlSkillHeader
            // 
            this.pnlSkillHeader.BackColor = System.Drawing.Color.Cornsilk;
            this.pnlSkillHeader.Controls.Add(this.lblSkills);
            this.pnlSkillHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSkillHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlSkillHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSkillHeader.Name = "pnlSkillHeader";
            this.pnlSkillHeader.Size = new System.Drawing.Size(274, 18);
            this.pnlSkillHeader.TabIndex = 1;
            // 
            // lblSkills
            // 
            this.lblSkills.AutoSize = true;
            this.lblSkills.Location = new System.Drawing.Point(1, 2);
            this.lblSkills.Name = "lblSkills";
            this.lblSkills.Size = new System.Drawing.Size(73, 13);
            this.lblSkills.TabIndex = 0;
            this.lblSkills.Text = resources.GetString("this.lblSkills.Text");
            // 
            // tvEntity
            // 
            this.tvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvEntity.Location = new System.Drawing.Point(0, 18);
            this.tvEntity.Name = "tvEntity";
            treeNode2.Name = "Node0";
            treeNode2.Text = resources.GetString("treeNode2.Text");
            this.tvEntity.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvEntity.ShowNodeToolTips = true;
            this.tvEntity.Size = new System.Drawing.Size(275, 246);
            this.tvEntity.TabIndex = 0;
            this.tvEntity.DoubleClick += new System.EventHandler(this.tvEntity_DoubleClick);
            // 
            // pnlItemHeader
            // 
            this.pnlItemHeader.BackColor = System.Drawing.Color.LightCyan;
            this.pnlItemHeader.Controls.Add(this.lblItems);
            this.pnlItemHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlItemHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlItemHeader.Name = "pnlItemHeader";
            this.pnlItemHeader.Size = new System.Drawing.Size(275, 18);
            this.pnlItemHeader.TabIndex = 0;
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Location = new System.Drawing.Point(1, 2);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(74, 13);
            this.lblItems.TabIndex = 0;
            this.lblItems.Text = resources.GetString("this.lblItems.Text");
            // 
            // cmSkills
            // 
            this.cmSkills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planToMenu,
            this.planToSeparator,
            this.showInSkillBrowserMenu,
            this.showInBrowserSeperator,
            this.showInSkillExplorerMenu,
            this.showPrerequisitiesMenu,
            this.showInMenuSeperator,
            this.tsmiSkillsExpandAll,
            this.tsmiSkillsCollapseAll});
            this.cmSkills.Name = "cmSkills";
            this.cmSkills.Size = new System.Drawing.Size(242, 154);
            this.cmSkills.Opening += new System.ComponentModel.CancelEventHandler(this.cmSkills_Opening);
            // 
            // planToMenu
            // 
            this.planToMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRemove,
            this.tsAddL1,
            this.tsAddL2,
            this.tsAddL3,
            this.tsAddL4,
            this.tsAddL5});
            this.planToMenu.Image = ((System.Drawing.Image)(resources.GetObject("planToMenu.Image")));
            this.planToMenu.Name = "planToMenu";
            this.planToMenu.Size = new System.Drawing.Size(241, 22);
            this.planToMenu.Text = resources.GetString("this.planToMenu.Text");
            // 
            // tsRemove
            // 
            this.tsRemove.Name = "tsRemove";
            this.tsRemove.Size = new System.Drawing.Size(117, 22);
            this.tsRemove.Text = resources.GetString("this.tsRemove.Text");
            this.tsRemove.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsAddL1
            // 
            this.tsAddL1.Name = "tsAddL1";
            this.tsAddL1.Size = new System.Drawing.Size(117, 22);
            this.tsAddL1.Text = resources.GetString("this.tsAddL1.Text");
            this.tsAddL1.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsAddL2
            // 
            this.tsAddL2.Name = "tsAddL2";
            this.tsAddL2.Size = new System.Drawing.Size(117, 22);
            this.tsAddL2.Text = resources.GetString("this.tsAddL2.Text");
            this.tsAddL2.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsAddL3
            // 
            this.tsAddL3.Name = "tsAddL3";
            this.tsAddL3.Size = new System.Drawing.Size(117, 22);
            this.tsAddL3.Text = resources.GetString("this.tsAddL3.Text");
            this.tsAddL3.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsAddL4
            // 
            this.tsAddL4.Name = "tsAddL4";
            this.tsAddL4.Size = new System.Drawing.Size(117, 22);
            this.tsAddL4.Text = resources.GetString("this.tsAddL4.Text");
            this.tsAddL4.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsAddL5
            // 
            this.tsAddL5.Name = "tsAddL5";
            this.tsAddL5.Size = new System.Drawing.Size(117, 22);
            this.tsAddL5.Text = resources.GetString("this.tsAddL5.Text");
            this.tsAddL5.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // planToSeparator
            // 
            this.planToSeparator.Name = "planToSeparator";
            this.planToSeparator.Size = new System.Drawing.Size(238, 6);
            // 
            // showInSkillBrowserMenu
            // 
            this.showInSkillBrowserMenu.Name = "showInSkillBrowserMenu";
            this.showInSkillBrowserMenu.Size = new System.Drawing.Size(241, 22);
            this.showInSkillBrowserMenu.Text = resources.GetString("this.showInSkillBrowserMenu.Text");
            this.showInSkillBrowserMenu.Click += new System.EventHandler(this.showInSkillBrowserMenu_Click);
            // 
            // showInBrowserSeperator
            // 
            this.showInBrowserSeperator.Name = "showInBrowserSeperator";
            this.showInBrowserSeperator.Size = new System.Drawing.Size(238, 6);
            // 
            // showInSkillExplorerMenu
            // 
            this.showInSkillExplorerMenu.Name = "showInSkillExplorerMenu";
            this.showInSkillExplorerMenu.Size = new System.Drawing.Size(241, 22);
            this.showInSkillExplorerMenu.Text = resources.GetString("this.showInSkillExplorerMenu.Text");
            this.showInSkillExplorerMenu.Click += new System.EventHandler(this.showInSkillExplorerMenu_Click);
            // 
            // showPrerequisitiesMenu
            // 
            this.showPrerequisitiesMenu.Name = "showPrerequisitiesMenu";
            this.showPrerequisitiesMenu.Size = new System.Drawing.Size(241, 22);
            this.showPrerequisitiesMenu.Text = resources.GetString("this.showPrerequisitiesMenu.Text");
            this.showPrerequisitiesMenu.Click += new System.EventHandler(this.showPrerequisitiesMenu_Click);
            // 
            // showInMenuSeperator
            // 
            this.showInMenuSeperator.Name = "showInMenuSeperator";
            this.showInMenuSeperator.Size = new System.Drawing.Size(238, 6);
            // 
            // tsmiSkillsExpandAll
            // 
            this.tsmiSkillsExpandAll.Name = "tsmiSkillsExpandAll";
            this.tsmiSkillsExpandAll.Size = new System.Drawing.Size(241, 22);
            this.tsmiSkillsExpandAll.Text = resources.GetString("this.tsmiSkillsExpandAll.Text");
            this.tsmiSkillsExpandAll.Click += new System.EventHandler(this.tsmiExpandAll_Click);
            // 
            // tsmiSkillsCollapseAll
            // 
            this.tsmiSkillsCollapseAll.Name = "tsmiSkillsCollapseAll";
            this.tsmiSkillsCollapseAll.Size = new System.Drawing.Size(241, 22);
            this.tsmiSkillsCollapseAll.Text = resources.GetString("this.tsmiSkillsCollapseAll.Text");
            this.tsmiSkillsCollapseAll.Click += new System.EventHandler(this.tsmiColapseAll_Click);
            // 
            // cmEntity
            // 
            this.cmEntity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planToObject,
            this.planToObjectSeperator,
            this.showObjectInBrowserMenuItem,
            this.showObjectInBrowserSeperator,
            this.showObjectPrerequisitiesMenu,
            this.showObjectInMenuSeperator,
            this.tsmiObjectsExpandAll,
            this.tsmiObjectsCollapseAll});
            this.cmEntity.Name = "cmShips";
            this.cmEntity.Size = new System.Drawing.Size(229, 154);
            this.cmEntity.Opening += new System.ComponentModel.CancelEventHandler(this.cmEntity_Opening);
            // 
            // planToObject
            // 
            this.planToObject.Name = "planToObject";
            this.planToObject.Size = new System.Drawing.Size(228, 22);
            this.planToObject.Text = resources.GetString("this.planToObject.Text");
            this.planToObject.Click += new System.EventHandler(this.planToObjectMenuItem_Click);
            // 
            // planToObjectSeperator
            // 
            this.planToObjectSeperator.Name = "planToObjectSeperator";
            this.planToObjectSeperator.Size = new System.Drawing.Size(225, 6);
            // 
            // showObjectInBrowserMenuItem
            // 
            this.showObjectInBrowserMenuItem.Name = "showObjectInBrowserMenuItem";
            this.showObjectInBrowserMenuItem.Size = new System.Drawing.Size(228, 22);
            this.showObjectInBrowserMenuItem.Text = resources.GetString("this.showObjectInBrowserMenuItem.Text");
            this.showObjectInBrowserMenuItem.Click += new System.EventHandler(this.tvEntity_DoubleClick);
            // 
            // showObjectInBrowserSeperator
            // 
            this.showObjectInBrowserSeperator.Name = "showObjectInBrowserSeperator";
            this.showObjectInBrowserSeperator.Size = new System.Drawing.Size(225, 6);
            // 
            // showObjectPrerequisitiesMenu
            // 
            this.showObjectPrerequisitiesMenu.Name = "showObjectPrerequisitiesMenu";
            this.showObjectPrerequisitiesMenu.Size = new System.Drawing.Size(228, 22);
            this.showObjectPrerequisitiesMenu.Text = resources.GetString("this.showObjectPrerequisitiesMenu.Text");
            this.showObjectPrerequisitiesMenu.Click += new System.EventHandler(this.tsShowItemPrereqs_Click);
            // 
            // showObjectInMenuSeperator
            // 
            this.showObjectInMenuSeperator.Name = "showObjectInMenuSeperator";
            this.showObjectInMenuSeperator.Size = new System.Drawing.Size(225, 6);
            // 
            // tsmiObjectsExpandAll
            // 
            this.tsmiObjectsExpandAll.Name = "tsmiObjectsExpandAll";
            this.tsmiObjectsExpandAll.Size = new System.Drawing.Size(228, 22);
            this.tsmiObjectsExpandAll.Text = resources.GetString("this.tsmiObjectsExpandAll.Text");
            this.tsmiObjectsExpandAll.Click += new System.EventHandler(this.tsmiObjectExpandAll_Click);
            // 
            // tsmiObjectsCollapseAll
            // 
            this.tsmiObjectsCollapseAll.Name = "tsmiObjectsCollapseAll";
            this.tsmiObjectsCollapseAll.Size = new System.Drawing.Size(228, 22);
            this.tsmiObjectsCollapseAll.Text = resources.GetString("this.tsmiObjectsCollapseAll.Text");
            this.tsmiObjectsCollapseAll.Click += new System.EventHandler(this.tsmiObjectCollapseAll_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbHistory);
            this.groupBox2.Controls.Add(this.cbShowBaseOnly);
            this.groupBox2.Controls.Add(this.rbShowAlpha);
            this.groupBox2.Controls.Add(this.rbShowTree);
            this.groupBox2.Location = new System.Drawing.Point(12, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 51);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = resources.GetString("this.groupBox2.Text");
            // 
            // cbHistory
            // 
            this.cbHistory.DisplayMember = "Name";
            this.cbHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHistory.FormattingEnabled = true;
            this.cbHistory.Location = new System.Drawing.Point(5, 19);
            this.cbHistory.Name = "cbHistory";
            this.cbHistory.Size = new System.Drawing.Size(254, 21);
            this.cbHistory.TabIndex = 3;
            this.cbHistory.SelectedIndexChanged += new System.EventHandler(this.cbHistory_SelectedIndexChanged);
            // 
            // cbShowBaseOnly
            // 
            this.cbShowBaseOnly.AutoSize = true;
            this.cbShowBaseOnly.Location = new System.Drawing.Point(288, 30);
            this.cbShowBaseOnly.Name = "cbShowBaseOnly";
            this.cbShowBaseOnly.Size = new System.Drawing.Size(109, 17);
            this.cbShowBaseOnly.TabIndex = 2;
            this.cbShowBaseOnly.Text = resources.GetString("this.cbShowBaseOnly.Text");
            this.cbShowBaseOnly.UseVisualStyleBackColor = true;
            this.cbShowBaseOnly.CheckedChanged += new System.EventHandler(this.cbShowBaseOnly_CheckedChanged);
            // 
            // rbShowAlpha
            // 
            this.rbShowAlpha.AutoSize = true;
            this.rbShowAlpha.Checked = true;
            this.rbShowAlpha.Location = new System.Drawing.Point(288, 10);
            this.rbShowAlpha.Name = "rbShowAlpha";
            this.rbShowAlpha.Size = new System.Drawing.Size(124, 17);
            this.rbShowAlpha.TabIndex = 1;
            this.rbShowAlpha.TabStop = true;
            this.rbShowAlpha.Text = resources.GetString("this.rbShowAlpha.Text");
            this.rbShowAlpha.UseVisualStyleBackColor = true;
            this.rbShowAlpha.CheckedChanged += new System.EventHandler(this.rbShowAlpha_CheckedChanged);
            // 
            // rbShowTree
            // 
            this.rbShowTree.AutoSize = true;
            this.rbShowTree.Location = new System.Drawing.Point(418, 10);
            this.rbShowTree.Name = "rbShowTree";
            this.rbShowTree.Size = new System.Drawing.Size(127, 17);
            this.rbShowTree.TabIndex = 0;
            this.rbShowTree.Text = resources.GetString("this.rbShowTree.Text");
            this.rbShowTree.UseVisualStyleBackColor = true;
            // 
            // tmrAutoUpdate
            // 
            this.tmrAutoUpdate.Interval = 30000;
            this.tmrAutoUpdate.Tick += new System.EventHandler(this.tmrAutoUpdate_Tick);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // SkillExplorerWindow
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.grpPlanName);
            this.Controls.Add(this.middlePanel);
            this.Controls.Add(this.lowerPanel);
            this.Controls.Add(this.groupBox2);
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.Name = "SkillExplorerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("this.Text");
            this.grpPlanName.ResumeLayout(false);
            this.grpPlanName.PerformLayout();
            this.lowerPanel.ResumeLayout(false);
            this.lowerPanel.PerformLayout();
            this.middlePanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlSkillHeader.ResumeLayout(false);
            this.pnlSkillHeader.PerformLayout();
            this.pnlItemHeader.ResumeLayout(false);
            this.pnlItemHeader.PerformLayout();
            this.cmSkills.ResumeLayout(false);
            this.cmEntity.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.GroupBox grpPlanName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel lowerPanel;
        private System.Windows.Forms.Panel middlePanel;
        private PersistentSplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbShowBaseOnly;
        private System.Windows.Forms.RadioButton rbShowAlpha;
        private System.Windows.Forms.RadioButton rbShowTree;
        private System.Windows.Forms.Label lblSkillInfo;
        private System.Windows.Forms.TreeView tvSkills;
        private System.Windows.Forms.ContextMenuStrip cmSkills;
        private System.Windows.Forms.ToolStripMenuItem planToMenu;
        private System.Windows.Forms.ToolStripMenuItem showInSkillExplorerMenu;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowserMenu;
        private System.Windows.Forms.ToolStripMenuItem showPrerequisitiesMenu;
        private System.Windows.Forms.ToolStripMenuItem tsAddL1;
        private System.Windows.Forms.ToolStripMenuItem tsAddL2;
        private System.Windows.Forms.ToolStripMenuItem tsAddL3;
        private System.Windows.Forms.ToolStripMenuItem tsAddL4;
        private System.Windows.Forms.ToolStripMenuItem tsAddL5;
        private System.Windows.Forms.Panel pnlSkillHeader;
        private System.Windows.Forms.Label lblSkills;
        private System.Windows.Forms.TreeView tvEntity;
        private System.Windows.Forms.Panel pnlItemHeader;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.ContextMenuStrip cmEntity;
        private System.Windows.Forms.ToolStripMenuItem planToObject;
        private System.Windows.Forms.ToolStripMenuItem showObjectPrerequisitiesMenu;
        private System.Windows.Forms.ToolStripMenuItem showObjectInBrowserMenuItem;
        private System.Windows.Forms.Label lblDimmedFont;
        private System.Windows.Forms.Label lblNormalFont;
        private System.Windows.Forms.Label lblRedFontInfo;
        private System.Windows.Forms.Label lblRedFont;
        private System.Windows.Forms.Label lblDimmedFontInfo;
        private System.Windows.Forms.Timer tmrAutoUpdate;
        private System.Windows.Forms.ComboBox cbHistory;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator planToSeparator;
        private System.Windows.Forms.ToolStripSeparator showInBrowserSeperator;
        private System.Windows.Forms.ToolStripSeparator showInMenuSeperator;
        private System.Windows.Forms.ToolStripMenuItem tsmiSkillsExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiSkillsCollapseAll;
        private System.Windows.Forms.ToolStripSeparator planToObjectSeperator;
        private System.Windows.Forms.ToolStripSeparator showObjectInBrowserSeperator;
        private System.Windows.Forms.ToolStripSeparator showObjectInMenuSeperator;
        private System.Windows.Forms.ToolStripMenuItem tsmiObjectsExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiObjectsCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem tsRemove;
    }
}
