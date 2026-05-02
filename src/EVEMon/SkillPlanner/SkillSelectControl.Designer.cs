namespace EVEMon.SkillPlanner
{
    partial class SkillSelectControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillSelectControl));
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.cmSkills = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiPlanToLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.level0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planToMenuSkillSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInSkillBrowserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showInSkillExplorerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiExpandSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCollapseSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.expandCollapseSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.lbSearchList = new System.Windows.Forms.ListBox();
            this.cmListSkills = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiLvPlanTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel0 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel5 = new System.Windows.Forms.ToolStripMenuItem();
            this.planToMenuSkillListSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInSkillBrowserListMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showInSkillExplorerListMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lbSearchTextHint = new System.Windows.Forms.Label();
            this.lbNoMatches = new System.Windows.Forms.Label();
            this.cbShowNonPublic = new System.Windows.Forms.CheckBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cbSorting = new System.Windows.Forms.ComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.lvSortedSkillList = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSortKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilSkillIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.pbSearchTextDel = new System.Windows.Forms.PictureBox();
            this.cbFilterByAttributes = new System.Windows.Forms.ComboBox();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.tvItems = new EVEMon.Common.Controls.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.cmSkills.SuspendLayout();
            this.cmListSkills.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchTextDel)).BeginInit();
            this.pnlResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSkillFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkillFilter.FormattingEnabled = true;
            this.cbSkillFilter.Location = new System.Drawing.Point(45, 3);
            this.cbSkillFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSkillFilter.MaxDropDownItems = 12;
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(182, 21);
            this.cbSkillFilter.Sorted = true;
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // tbSearchText
            // 
            this.tbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchText.Location = new System.Drawing.Point(45, 76);
            this.tbSearchText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(182, 20);
            this.tbSearchText.TabIndex = 3;
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            this.tbSearchText.Enter += new System.EventHandler(this.tbSearch_Enter);
            this.tbSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            this.tbSearchText.Leave += new System.EventHandler(this.tbSearch_Leave);
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(9, 76);
            this.pbSearchImage.Margin = new System.Windows.Forms.Padding(2);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(20, 20);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 19;
            this.pbSearchImage.TabStop = false;
            // 
            // cmSkills
            // 
            this.cmSkills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiPlanToLevel,
            this.planToMenuSkillSeparator,
            this.showInSkillBrowserMenu,
            this.showInSkillExplorerMenu,
            this.cmiExpandSelected,
            this.cmiCollapseSelected,
            this.expandCollapseSeparator,
            this.cmiExpandAll,
            this.cmiCollapseAll});
            this.cmSkills.Name = "contextMenuStrip1";
            this.cmSkills.Size = new System.Drawing.Size(195, 170);
            this.cmSkills.Opening += new System.ComponentModel.CancelEventHandler(this.cmSkills_Opening);
            // 
            // cmiPlanToLevel
            // 
            this.cmiPlanToLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level0ToolStripMenuItem,
            this.level1ToolStripMenuItem,
            this.level2ToolStripMenuItem,
            this.level3ToolStripMenuItem,
            this.level4ToolStripMenuItem,
            this.level5ToolStripMenuItem});
            this.cmiPlanToLevel.Image = ((System.Drawing.Image)(resources.GetObject("cmiPlanToLevel.Image")));
            this.cmiPlanToLevel.Name = "cmiPlanToLevel";
            this.cmiPlanToLevel.Size = new System.Drawing.Size(194, 22);
            this.cmiPlanToLevel.Text = resources.GetString("this.cmiPlanToLevel.Text");
            // 
            // level0ToolStripMenuItem
            // 
            this.level0ToolStripMenuItem.Name = "level0ToolStripMenuItem";
            this.level0ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.level0ToolStripMenuItem.Text = resources.GetString("this.level0ToolStripMenuItem.Text");
            this.level0ToolStripMenuItem.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // level1ToolStripMenuItem
            // 
            this.level1ToolStripMenuItem.Name = "level1ToolStripMenuItem";
            this.level1ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.level1ToolStripMenuItem.Text = resources.GetString("this.level1ToolStripMenuItem.Text");
            this.level1ToolStripMenuItem.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // level2ToolStripMenuItem
            // 
            this.level2ToolStripMenuItem.Name = "level2ToolStripMenuItem";
            this.level2ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.level2ToolStripMenuItem.Text = resources.GetString("this.level2ToolStripMenuItem.Text");
            this.level2ToolStripMenuItem.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // level3ToolStripMenuItem
            // 
            this.level3ToolStripMenuItem.Name = "level3ToolStripMenuItem";
            this.level3ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.level3ToolStripMenuItem.Text = resources.GetString("this.level3ToolStripMenuItem.Text");
            this.level3ToolStripMenuItem.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // level4ToolStripMenuItem
            // 
            this.level4ToolStripMenuItem.Name = "level4ToolStripMenuItem";
            this.level4ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.level4ToolStripMenuItem.Text = resources.GetString("this.level4ToolStripMenuItem.Text");
            this.level4ToolStripMenuItem.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // level5ToolStripMenuItem
            // 
            this.level5ToolStripMenuItem.Name = "level5ToolStripMenuItem";
            this.level5ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.level5ToolStripMenuItem.Text = resources.GetString("this.level5ToolStripMenuItem.Text");
            this.level5ToolStripMenuItem.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // planToMenuSkillSeparator
            // 
            this.planToMenuSkillSeparator.Name = "planToMenuSkillSeparator";
            this.planToMenuSkillSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // showInSkillBrowserMenu
            // 
            this.showInSkillBrowserMenu.Name = "showInSkillBrowserMenu";
            this.showInSkillBrowserMenu.Size = new System.Drawing.Size(194, 22);
            this.showInSkillBrowserMenu.Text = resources.GetString("this.showInSkillBrowserMenu.Text");
            this.showInSkillBrowserMenu.Click += new System.EventHandler(this.showInSkillBrowserMenu_Click);
            // 
            // showInSkillExplorerMenu
            // 
            this.showInSkillExplorerMenu.Image = ((System.Drawing.Image)(resources.GetObject("showInSkillExplorerMenu.Image")));
            this.showInSkillExplorerMenu.Name = "showInSkillExplorerMenu";
            this.showInSkillExplorerMenu.Size = new System.Drawing.Size(194, 22);
            this.showInSkillExplorerMenu.Text = resources.GetString("this.showInSkillExplorerMenu.Text");
            this.showInSkillExplorerMenu.Click += new System.EventHandler(this.showInSkillExplorerMenu_Click);
            // 
            // cmiExpandSelected
            // 
            this.cmiExpandSelected.Name = "cmiExpandSelected";
            this.cmiExpandSelected.Size = new System.Drawing.Size(194, 22);
            this.cmiExpandSelected.Text = resources.GetString("this.cmiExpandSelected.Text");
            this.cmiExpandSelected.Click += new System.EventHandler(this.cmiExpandSelected_Click);
            // 
            // cmiCollapseSelected
            // 
            this.cmiCollapseSelected.Name = "cmiCollapseSelected";
            this.cmiCollapseSelected.Size = new System.Drawing.Size(194, 22);
            this.cmiCollapseSelected.Text = resources.GetString("this.cmiCollapseSelected.Text");
            this.cmiCollapseSelected.Click += new System.EventHandler(this.cmiCollapseSelected_Click);
            // 
            // expandCollapseSeparator
            // 
            this.expandCollapseSeparator.Name = "expandCollapseSeparator";
            this.expandCollapseSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // cmiExpandAll
            // 
            this.cmiExpandAll.Name = "cmiExpandAll";
            this.cmiExpandAll.Size = new System.Drawing.Size(194, 22);
            this.cmiExpandAll.Text = resources.GetString("this.cmiExpandAll.Text");
            this.cmiExpandAll.Click += new System.EventHandler(this.cmiExpandAll_Click);
            // 
            // cmiCollapseAll
            // 
            this.cmiCollapseAll.Name = "cmiCollapseAll";
            this.cmiCollapseAll.Size = new System.Drawing.Size(194, 22);
            this.cmiCollapseAll.Text = resources.GetString("this.cmiCollapseAll.Text");
            this.cmiCollapseAll.Click += new System.EventHandler(this.cmiCollapseAll_Click);
            // 
            // lbSearchList
            // 
            this.lbSearchList.ContextMenuStrip = this.cmListSkills;
            this.lbSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSearchList.FormattingEnabled = true;
            this.lbSearchList.IntegralHeight = false;
            this.lbSearchList.Location = new System.Drawing.Point(0, 0);
            this.lbSearchList.Margin = new System.Windows.Forms.Padding(2);
            this.lbSearchList.Name = "lbSearchList";
            this.lbSearchList.Size = new System.Drawing.Size(227, 299);
            this.lbSearchList.TabIndex = 1;
            this.lbSearchList.Visible = false;
            this.lbSearchList.SelectedIndexChanged += new System.EventHandler(this.lbSearchList_SelectedIndexChanged);
            this.lbSearchList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSearchList_MouseDown);
            this.lbSearchList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbSearchList_MouseMove);
            // 
            // cmListSkills
            // 
            this.cmListSkills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiLvPlanTo,
            this.planToMenuSkillListSeparator,
            this.showInSkillBrowserListMenu,
            this.showInSkillExplorerListMenu});
            this.cmListSkills.Name = "cmListSkills";
            this.cmListSkills.Size = new System.Drawing.Size(195, 98);
            this.cmListSkills.Opening += new System.ComponentModel.CancelEventHandler(this.cmListSkills_Opening);
            // 
            // cmiLvPlanTo
            // 
            this.cmiLvPlanTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLevel0,
            this.tsmLevel1,
            this.tsmLevel2,
            this.tsmLevel3,
            this.tsmLevel4,
            this.tsmLevel5});
            this.cmiLvPlanTo.Image = ((System.Drawing.Image)(resources.GetObject("cmiLvPlanTo.Image")));
            this.cmiLvPlanTo.Name = "cmiLvPlanTo";
            this.cmiLvPlanTo.Size = new System.Drawing.Size(194, 22);
            this.cmiLvPlanTo.Text = resources.GetString("this.cmiLvPlanTo.Text");
            // 
            // tsmLevel0
            // 
            this.tsmLevel0.Name = "tsmLevel0";
            this.tsmLevel0.Size = new System.Drawing.Size(117, 22);
            this.tsmLevel0.Text = resources.GetString("this.tsmLevel0.Text");
            this.tsmLevel0.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel1
            // 
            this.tsmLevel1.Name = "tsmLevel1";
            this.tsmLevel1.Size = new System.Drawing.Size(117, 22);
            this.tsmLevel1.Text = resources.GetString("this.tsmLevel1.Text");
            this.tsmLevel1.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel2
            // 
            this.tsmLevel2.Name = "tsmLevel2";
            this.tsmLevel2.Size = new System.Drawing.Size(117, 22);
            this.tsmLevel2.Text = resources.GetString("this.tsmLevel2.Text");
            this.tsmLevel2.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel3
            // 
            this.tsmLevel3.Name = "tsmLevel3";
            this.tsmLevel3.Size = new System.Drawing.Size(117, 22);
            this.tsmLevel3.Text = resources.GetString("this.tsmLevel3.Text");
            this.tsmLevel3.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel4
            // 
            this.tsmLevel4.Name = "tsmLevel4";
            this.tsmLevel4.Size = new System.Drawing.Size(117, 22);
            this.tsmLevel4.Text = resources.GetString("this.tsmLevel4.Text");
            this.tsmLevel4.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel5
            // 
            this.tsmLevel5.Name = "tsmLevel5";
            this.tsmLevel5.Size = new System.Drawing.Size(117, 22);
            this.tsmLevel5.Text = resources.GetString("this.tsmLevel5.Text");
            this.tsmLevel5.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // planToMenuSkillListSeparator
            // 
            this.planToMenuSkillListSeparator.Name = "planToMenuSkillListSeparator";
            this.planToMenuSkillListSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // showInSkillBrowserListMenu
            // 
            this.showInSkillBrowserListMenu.Name = "showInSkillBrowserListMenu";
            this.showInSkillBrowserListMenu.Size = new System.Drawing.Size(194, 22);
            this.showInSkillBrowserListMenu.Text = resources.GetString("this.showInSkillBrowserListMenu.Text");
            this.showInSkillBrowserListMenu.Click += new System.EventHandler(this.showInSkillBrowserMenu_Click);
            // 
            // showInSkillExplorerListMenu
            // 
            this.showInSkillExplorerListMenu.Image = ((System.Drawing.Image)(resources.GetObject("showInSkillExplorerListMenu.Image")));
            this.showInSkillExplorerListMenu.Name = "showInSkillExplorerListMenu";
            this.showInSkillExplorerListMenu.Size = new System.Drawing.Size(194, 22);
            this.showInSkillExplorerListMenu.Text = resources.GetString("this.showInSkillExplorerListMenu.Text");
            this.showInSkillExplorerListMenu.Click += new System.EventHandler(this.showInSkillExplorerMenu_Click);
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.AutoSize = true;
            this.lbSearchTextHint.BackColor = System.Drawing.SystemColors.Window;
            this.lbSearchTextHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSearchTextHint.Location = new System.Drawing.Point(49, 79);
            this.lbSearchTextHint.Name = "lbSearchTextHint";
            this.lbSearchTextHint.Size = new System.Drawing.Size(65, 13);
            this.lbSearchTextHint.TabIndex = 22;
            this.lbSearchTextHint.Text = resources.GetString("this.lbSearchTextHint.Text");
            this.lbSearchTextHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSearchTextHint.Click += new System.EventHandler(this.lblSearchTextHint_Click);
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.BackColor = System.Drawing.SystemColors.Window;
            this.lbNoMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbNoMatches.Location = new System.Drawing.Point(0, 0);
            this.lbNoMatches.Margin = new System.Windows.Forms.Padding(0);
            this.lbNoMatches.Name = "lbNoMatches";
            this.lbNoMatches.Padding = new System.Windows.Forms.Padding(4, 30, 4, 4);
            this.lbNoMatches.Size = new System.Drawing.Size(227, 299);
            this.lbNoMatches.TabIndex = 1;
            this.lbNoMatches.Text = resources.GetString("this.lbNoMatches.Text");
            this.lbNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbNoMatches.Visible = false;
            // 
            // cbShowNonPublic
            // 
            this.cbShowNonPublic.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbShowNonPublic.Location = new System.Drawing.Point(0, 399);
            this.cbShowNonPublic.Margin = new System.Windows.Forms.Padding(2);
            this.cbShowNonPublic.Name = "cbShowNonPublic";
            this.cbShowNonPublic.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.cbShowNonPublic.Size = new System.Drawing.Size(227, 22);
            this.cbShowNonPublic.TabIndex = 2;
            this.cbShowNonPublic.Text = resources.GetString("this.cbShowNonPublic.Text");
            this.cbShowNonPublic.UseVisualStyleBackColor = true;
            this.cbShowNonPublic.CheckedChanged += new System.EventHandler(this.cbShowNonPublic_CheckedChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(6, 6);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 25;
            this.lblFilter.Text = resources.GetString("this.lblFilter.Text");
            // 
            // cbSorting
            // 
            this.cbSorting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSorting.FormattingEnabled = true;
            this.cbSorting.Location = new System.Drawing.Point(45, 51);
            this.cbSorting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSorting.Name = "cbSorting";
            this.cbSorting.Size = new System.Drawing.Size(182, 21);
            this.cbSorting.TabIndex = 2;
            this.cbSorting.SelectedIndexChanged += new System.EventHandler(this.cbSorting_SelectedIndexChanged);
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(9, 54);
            this.lblSort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(29, 13);
            this.lblSort.TabIndex = 27;
            this.lblSort.Text = resources.GetString("this.lblSort.Text");
            // 
            // lvSortedSkillList
            // 
            this.lvSortedSkillList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSortKey});
            this.lvSortedSkillList.ContextMenuStrip = this.cmListSkills;
            this.lvSortedSkillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSortedSkillList.FullRowSelect = true;
            this.lvSortedSkillList.HideSelection = false;
            this.lvSortedSkillList.Location = new System.Drawing.Point(0, 0);
            this.lvSortedSkillList.Margin = new System.Windows.Forms.Padding(2);
            this.lvSortedSkillList.MultiSelect = false;
            this.lvSortedSkillList.Name = "lvSortedSkillList";
            this.lvSortedSkillList.Size = new System.Drawing.Size(227, 299);
            this.lvSortedSkillList.TabIndex = 1;
            this.lvSortedSkillList.TileSize = new System.Drawing.Size(16, 16);
            this.lvSortedSkillList.UseCompatibleStateImageBehavior = false;
            this.lvSortedSkillList.View = System.Windows.Forms.View.Details;
            this.lvSortedSkillList.Visible = false;
            this.lvSortedSkillList.SelectedIndexChanged += new System.EventHandler(this.lvSortedSkillList_SelectedIndexChanged);
            this.lvSortedSkillList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvSortedSkillList_MouseDown);
            this.lvSortedSkillList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvSortedSkillList_MouseMove);
            // 
            // chName
            // 
            this.chName.Text = resources.GetString("this.chName.Text");
            this.chName.Width = 120;
            // 
            // chSortKey
            // 
            this.chSortKey.Text = resources.GetString("this.chSortKey.Text");
            // 
            // ilSkillIcons
            // 
            this.ilSkillIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSkillIcons.ImageStream")));
            this.ilSkillIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSkillIcons.Images.SetKeyName(0, "book");
            this.ilSkillIcons.Images.SetKeyName(1, "PrereqsNOTMet");
            this.ilSkillIcons.Images.SetKeyName(2, "PrereqsMet");
            this.ilSkillIcons.Images.SetKeyName(3, "lvl0");
            this.ilSkillIcons.Images.SetKeyName(4, "lvl1");
            this.ilSkillIcons.Images.SetKeyName(5, "lvl2");
            this.ilSkillIcons.Images.SetKeyName(6, "lvl3");
            this.ilSkillIcons.Images.SetKeyName(7, "lvl4");
            this.ilSkillIcons.Images.SetKeyName(8, "lvl5");
            this.ilSkillIcons.Images.SetKeyName(9, "Skill");
            this.ilSkillIcons.Images.SetKeyName(10, "Skills");
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.pbSearchTextDel);
            this.pnlFilter.Controls.Add(this.cbFilterByAttributes);
            this.pnlFilter.Controls.Add(this.lblFilterBy);
            this.pnlFilter.Controls.Add(this.cbSorting);
            this.pnlFilter.Controls.Add(this.cbSkillFilter);
            this.pnlFilter.Controls.Add(this.lblSort);
            this.pnlFilter.Controls.Add(this.pbSearchImage);
            this.pnlFilter.Controls.Add(this.lbSearchTextHint);
            this.pnlFilter.Controls.Add(this.lblFilter);
            this.pnlFilter.Controls.Add(this.tbSearchText);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(227, 100);
            this.pnlFilter.TabIndex = 0;
            // 
            // pbSearchTextDel
            // 
            this.pbSearchTextDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSearchTextDel.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchTextDel.Image")));
            this.pbSearchTextDel.InitialImage = null;
            this.pbSearchTextDel.Location = new System.Drawing.Point(209, 78);
            this.pbSearchTextDel.Name = "pbSearchTextDel";
            this.pbSearchTextDel.Size = new System.Drawing.Size(16, 16);
            this.pbSearchTextDel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchTextDel.TabIndex = 38;
            this.pbSearchTextDel.TabStop = false;
            this.pbSearchTextDel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbSearchTextDel_MouseUp);
            // 
            // cbFilterByAttributes
            // 
            this.cbFilterByAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilterByAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterByAttributes.FormattingEnabled = true;
            this.cbFilterByAttributes.Location = new System.Drawing.Point(45, 27);
            this.cbFilterByAttributes.Name = "cbFilterByAttributes";
            this.cbFilterByAttributes.Size = new System.Drawing.Size(182, 21);
            this.cbFilterByAttributes.Sorted = true;
            this.cbFilterByAttributes.TabIndex = 1;
            this.cbFilterByAttributes.SelectedIndexChanged += new System.EventHandler(this.cbFilterByAttributes_SelectedIndexChanged);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Location = new System.Drawing.Point(16, 30);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(22, 13);
            this.lblFilterBy.TabIndex = 37;
            this.lblFilterBy.Text = resources.GetString("this.lblFilterBy.Text");
            // 
            // pnlResults
            // 
            this.pnlResults.AutoSize = true;
            this.pnlResults.Controls.Add(this.lbNoMatches);
            this.pnlResults.Controls.Add(this.lbSearchList);
            this.pnlResults.Controls.Add(this.tvItems);
            this.pnlResults.Controls.Add(this.lvSortedSkillList);
            this.pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResults.Location = new System.Drawing.Point(0, 100);
            this.pnlResults.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(227, 299);
            this.pnlResults.TabIndex = 1;
            // 
            // tvItems
            // 
            this.tvItems.ContextMenuStrip = this.cmSkills;
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.HideSelection = false;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Margin = new System.Windows.Forms.Padding(2);
            this.tvItems.Name = "tvItems";
            this.tvItems.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.tvItems.SelectionMode = EVEMon.Common.Controls.TreeViewSelectionMode.SingleSelect;
            this.tvItems.Size = new System.Drawing.Size(227, 299);
            this.tvItems.TabIndex = 0;
            this.tvItems.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvItems_ItemDrag);
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSkillList_AfterSelect);
            this.tvItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseDown);
            this.tvItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseMove);
            // 
            // SkillSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlResults);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.cbShowNonPublic);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SkillSelectControl";
            this.Size = new System.Drawing.Size(227, 421);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.cmSkills.ResumeLayout(false);
            this.cmListSkills.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchTextDel)).EndInit();
            this.pnlResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.ListBox lbSearchList;
        private System.Windows.Forms.Label lbSearchTextHint;
        private System.Windows.Forms.Label lbNoMatches;
        private System.Windows.Forms.CheckBox cbShowNonPublic;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cbSorting;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.ListView lvSortedSkillList;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSortKey;
        private System.Windows.Forms.ImageList ilSkillIcons;
        private EVEMon.Common.Controls.TreeView tvItems;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.ComboBox cbSkillFilter;
        private System.Windows.Forms.ContextMenuStrip cmSkills;
        private System.Windows.Forms.ToolStripMenuItem cmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem cmiCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem cmiExpandSelected;
        private System.Windows.Forms.ToolStripMenuItem cmiCollapseSelected;
        private System.Windows.Forms.ToolStripSeparator planToMenuSkillSeparator;
        private System.Windows.Forms.ToolStripMenuItem cmiPlanToLevel;
        private System.Windows.Forms.ToolStripMenuItem level1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level5ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmListSkills;
        private System.Windows.Forms.ToolStripMenuItem cmiLvPlanTo;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel1;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel2;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel3;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel4;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel5;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowserMenu;
        private System.Windows.Forms.ToolStripMenuItem showInSkillExplorerMenu;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowserListMenu;
        private System.Windows.Forms.ToolStripMenuItem showInSkillExplorerListMenu;
        private System.Windows.Forms.ToolStripSeparator expandCollapseSeparator;
        private System.Windows.Forms.ToolStripSeparator planToMenuSkillListSeparator;
        private System.Windows.Forms.ToolStripMenuItem level0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel0;
        private System.Windows.Forms.ComboBox cbFilterByAttributes;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.PictureBox pbSearchTextDel;
    }
}
