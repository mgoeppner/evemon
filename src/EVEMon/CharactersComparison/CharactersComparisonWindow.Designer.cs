namespace EVEMon.CharactersComparison
{
    partial class CharactersComparisonWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharactersComparisonWindow));
			this.persistentSplitContainer = new EVEMon.Common.Controls.PersistentSplitContainer();
			this.lvCharacterList = new System.Windows.Forms.ListView();
			this.chCharacters = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.filterPanel = new System.Windows.Forms.Panel();
			this.cbFilter = new System.Windows.Forms.ComboBox();
			this.filterLabel = new System.Windows.Forms.Label();
			this.gbSkills = new System.Windows.Forms.GroupBox();
			this.lvCharacterInfo = new System.Windows.Forms.ListView();
			this.chSkill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chCharacter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.characterInfoContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportSelectedSkillsAsPlanFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lblHelp = new System.Windows.Forms.Label();
			this.characterListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportCharacterSkillsAsPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.persistentSplitContainer)).BeginInit();
			this.persistentSplitContainer.Panel1.SuspendLayout();
			this.persistentSplitContainer.Panel2.SuspendLayout();
			this.persistentSplitContainer.SuspendLayout();
			this.filterPanel.SuspendLayout();
			this.gbSkills.SuspendLayout();
			this.characterInfoContextMenu.SuspendLayout();
			this.characterListContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// persistentSplitContainer
			// 
			this.persistentSplitContainer.BackColor = System.Drawing.SystemColors.Window;
			this.persistentSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.persistentSplitContainer.IsSplitterFixed = true;
			this.persistentSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.persistentSplitContainer.Name = "persistentSplitContainer";
			// 
			// persistentSplitContainer.Panel1
			// 
			this.persistentSplitContainer.Panel1.Controls.Add(this.lvCharacterList);
			this.persistentSplitContainer.Panel1.Controls.Add(this.filterPanel);
			this.persistentSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3);
			// 
			// persistentSplitContainer.Panel2
			// 
			this.persistentSplitContainer.Panel2.Controls.Add(this.gbSkills);
			this.persistentSplitContainer.Panel2.Controls.Add(this.lblHelp);
			this.persistentSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(3);
			this.persistentSplitContainer.RememberDistanceKey = null;
			this.persistentSplitContainer.Size = new System.Drawing.Size(784, 442);
			this.persistentSplitContainer.SplitterDistance = 200;
			this.persistentSplitContainer.TabIndex = 0;
			this.persistentSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.persistentSplitContainer_SplitterMoved);
			this.persistentSplitContainer.Resize += new System.EventHandler(this.persistentSplitContainer_Resize);
			// 
			// lvCharacterList
			// 
			this.lvCharacterList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCharacters});
			this.lvCharacterList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCharacterList.FullRowSelect = true;
			this.lvCharacterList.HideSelection = false;
			this.lvCharacterList.Location = new System.Drawing.Point(3, 43);
			this.lvCharacterList.Name = "lvCharacterList";
			this.lvCharacterList.Size = new System.Drawing.Size(194, 396);
			this.lvCharacterList.TabIndex = 1;
			this.lvCharacterList.UseCompatibleStateImageBehavior = false;
			this.lvCharacterList.View = System.Windows.Forms.View.Details;
			this.lvCharacterList.SelectedIndexChanged += new System.EventHandler(this.lvCharacterList_SelectedIndexChanged);
			this.lvCharacterList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvCharacterList_MouseDown);
			this.lvCharacterList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvCharacterList_MouseMove);
			// 
			// chCharacters
			// 
			this.chCharacters.Text = resources.GetString("this.chCharacters.Text");
			this.chCharacters.Width = 190;
			// 
			// filterPanel
			// 
			this.filterPanel.Controls.Add(this.cbFilter);
			this.filterPanel.Controls.Add(this.filterLabel);
			this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.filterPanel.Location = new System.Drawing.Point(3, 3);
			this.filterPanel.Name = "filterPanel";
			this.filterPanel.Size = new System.Drawing.Size(194, 40);
			this.filterPanel.TabIndex = 0;
			// 
			// cbFilter
			// 
			this.cbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFilter.FormattingEnabled = true;
			this.cbFilter.Items.AddRange(new object[] {
            "All",
            "Only Monitored"});
			this.cbFilter.Location = new System.Drawing.Point(41, 10);
			this.cbFilter.Name = "cbFilter";
			this.cbFilter.Size = new System.Drawing.Size(153, 21);
			this.cbFilter.TabIndex = 1;
			this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
			// 
			// filterLabel
			// 
			this.filterLabel.AutoSize = true;
			this.filterLabel.Location = new System.Drawing.Point(3, 13);
			this.filterLabel.Name = "filterLabel";
			this.filterLabel.Size = new System.Drawing.Size(32, 13);
			this.filterLabel.TabIndex = 0;
			this.filterLabel.Text = resources.GetString("this.filterLabel.Text");
			// 
			// gbSkills
			// 
			this.gbSkills.Controls.Add(this.lvCharacterInfo);
			this.gbSkills.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbSkills.Location = new System.Drawing.Point(3, 3);
			this.gbSkills.Name = "gbSkills";
			this.gbSkills.Size = new System.Drawing.Size(574, 436);
			this.gbSkills.TabIndex = 1;
			this.gbSkills.TabStop = false;
			this.gbSkills.Text = resources.GetString("this.gbSkills.Text");
			// 
			// lvCharacterInfo
			// 
			this.lvCharacterInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSkill,
            this.chCharacter});
			this.lvCharacterInfo.ContextMenuStrip = this.characterInfoContextMenu;
			this.lvCharacterInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCharacterInfo.FullRowSelect = true;
			this.lvCharacterInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvCharacterInfo.HideSelection = false;
			this.lvCharacterInfo.Location = new System.Drawing.Point(3, 16);
			this.lvCharacterInfo.Name = "lvCharacterInfo";
			this.lvCharacterInfo.ShowItemToolTips = true;
			this.lvCharacterInfo.Size = new System.Drawing.Size(568, 417);
			this.lvCharacterInfo.TabIndex = 0;
			this.lvCharacterInfo.UseCompatibleStateImageBehavior = false;
			this.lvCharacterInfo.View = System.Windows.Forms.View.Details;
			this.lvCharacterInfo.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvCharacterInfo_ColumnWidthChanging);
			this.lvCharacterInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvCharacterInfo_MouseDown);
			this.lvCharacterInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvCharacterInfo_MouseMove);
			// 
			// chSkill
			// 
			this.chSkill.Text = resources.GetString("this.chSkill.Text");
			this.chSkill.Width = 166;
			// 
			// chCharacter
			// 
			this.chCharacter.Text = resources.GetString("this.chCharacter.Text");
			this.chCharacter.Width = 131;
			// 
			// characterInfoContextMenu
			// 
			this.characterInfoContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSelectedSkillsAsPlanFromToolStripMenuItem,
            this.exportToCSVToolStripMenuItem});
			this.characterInfoContextMenu.Name = "characterInfoContextMenu";
			this.characterInfoContextMenu.Size = new System.Drawing.Size(262, 48);
			this.characterInfoContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.characterInfoContextMenu_Opening);
			// 
			// exportSelectedSkillsAsPlanFromToolStripMenuItem
			// 
			this.exportSelectedSkillsAsPlanFromToolStripMenuItem.Name = "exportSelectedSkillsAsPlanFromToolStripMenuItem";
			this.exportSelectedSkillsAsPlanFromToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.exportSelectedSkillsAsPlanFromToolStripMenuItem.Text = resources.GetString("this.exportSelectedSkillsAsPlanFromToolStripMenuItem.Text");
			this.exportSelectedSkillsAsPlanFromToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.exportSelectedSkillsAsPlanFromToolStripMenuItem_DropDownItemClicked);
			// 
			// exportToCSVToolStripMenuItem
			// 
			this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
			this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.exportToCSVToolStripMenuItem.Text = resources.GetString("this.exportToCSVToolStripMenuItem.Text");
			this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
			// 
			// lblHelp
			// 
			this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblHelp.AutoSize = true;
			this.lblHelp.Location = new System.Drawing.Point(6, 43);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(398, 52);
			this.lblHelp.TabIndex = 2;
			this.lblHelp.Text = "Use the list on the left to select characters to compare.\r\n\r\nTo do this, hold dow" +
    "n the CTRL key and click the characters you wish to compare.\r\nYou may compare se" +
    "veral characters at once.";
			// 
			// characterListContextMenu
			// 
			this.characterListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCharacterSkillsAsPlanToolStripMenuItem});
			this.characterListContextMenu.Name = "characterListContextMenu";
			this.characterListContextMenu.Size = new System.Drawing.Size(240, 26);
			this.characterListContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.characterListContextMenu_Opening);
			this.characterListContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.characterListContextMenu_ItemClicked);
			// 
			// exportCharacterSkillsAsPlanToolStripMenuItem
			// 
			this.exportCharacterSkillsAsPlanToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportCharacterSkillsAsPlanToolStripMenuItem.Image")));
			this.exportCharacterSkillsAsPlanToolStripMenuItem.Name = "exportCharacterSkillsAsPlanToolStripMenuItem";
			this.exportCharacterSkillsAsPlanToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.exportCharacterSkillsAsPlanToolStripMenuItem.Text = resources.GetString("this.exportCharacterSkillsAsPlanToolStripMenuItem.Text");
			// 
			// CharactersComparisonWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 442);
			this.Controls.Add(this.persistentSplitContainer);
			this.MinimumSize = new System.Drawing.Size(800, 480);
			this.Name = "CharactersComparisonWindow";
			this.Text = resources.GetString("this.Text");
			this.persistentSplitContainer.Panel1.ResumeLayout(false);
			this.persistentSplitContainer.Panel2.ResumeLayout(false);
			this.persistentSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.persistentSplitContainer)).EndInit();
			this.persistentSplitContainer.ResumeLayout(false);
			this.filterPanel.ResumeLayout(false);
			this.filterPanel.PerformLayout();
			this.gbSkills.ResumeLayout(false);
			this.characterInfoContextMenu.ResumeLayout(false);
			this.characterListContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Common.Controls.PersistentSplitContainer persistentSplitContainer;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.ListView lvCharacterInfo;
        private System.Windows.Forms.ListView lvCharacterList;
        private System.Windows.Forms.GroupBox gbSkills;
        private System.Windows.Forms.ColumnHeader chSkill;
        private System.Windows.Forms.ColumnHeader chCharacter;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.ColumnHeader chCharacters;
        private System.Windows.Forms.ContextMenuStrip characterListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportCharacterSkillsAsPlanToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip characterInfoContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedSkillsAsPlanFromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
    }
}