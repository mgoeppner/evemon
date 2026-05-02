namespace EVEMon.SkillPlanner
{
    partial class EveObjectSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EveObjectSelectControl));
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.lbSearchTextHint = new System.Windows.Forms.Label();
            this.lbNoMatches = new System.Windows.Forms.Label();
            this.lbSearchList = new System.Windows.Forms.ListBox();
            this.upperPanel = new System.Windows.Forms.Panel();
            this.pbSerachTextDel = new System.Windows.Forms.PictureBox();
            this.cbUsabilityFilter = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.lowerPanel = new System.Windows.Forms.Panel();
            this.tvItems = new EVEMon.Common.Controls.TreeView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiLvPlanTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparatorPlanTo = new System.Windows.Forms.ToolStripSeparator();
            this.cmiExpandSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCollapseSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparatorExpandCollapse = new System.Windows.Forms.ToolStripSeparator();
            this.cmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.upperPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSerachTextDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.lowerPanel.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSearchText
            // 
            this.tbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchText.Location = new System.Drawing.Point(45, 29);
            this.tbSearchText.Margin = new System.Windows.Forms.Padding(0);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(140, 20);
            this.tbSearchText.TabIndex = 21;
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            this.tbSearchText.Enter += new System.EventHandler(this.tbSearchText_Enter);
            this.tbSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearchText_KeyPress);
            this.tbSearchText.Leave += new System.EventHandler(this.tbSearchText_Leave);
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.AutoSize = true;
            this.lbSearchTextHint.BackColor = System.Drawing.SystemColors.Window;
            this.lbSearchTextHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSearchTextHint.Location = new System.Drawing.Point(47, 32);
            this.lbSearchTextHint.Margin = new System.Windows.Forms.Padding(0);
            this.lbSearchTextHint.Name = "lbSearchTextHint";
            this.lbSearchTextHint.Size = new System.Drawing.Size(65, 13);
            this.lbSearchTextHint.TabIndex = 23;
            this.lbSearchTextHint.Text = resources.GetString("this.lbSearchTextHint.Text");
            this.lbSearchTextHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSearchTextHint.Click += new System.EventHandler(this.lbSearchTextHint_Click);
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.BackColor = System.Drawing.Color.Transparent;
            this.lbNoMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbNoMatches.Location = new System.Drawing.Point(0, 0);
            this.lbNoMatches.Margin = new System.Windows.Forms.Padding(0);
            this.lbNoMatches.Name = "lbNoMatches";
            this.lbNoMatches.Padding = new System.Windows.Forms.Padding(4, 30, 4, 4);
            this.lbNoMatches.Size = new System.Drawing.Size(185, 344);
            this.lbNoMatches.TabIndex = 24;
            this.lbNoMatches.Text = resources.GetString("this.lbNoMatches.Text");
            this.lbNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbSearchList
            // 
            this.lbSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSearchList.FormattingEnabled = true;
            this.lbSearchList.IntegralHeight = false;
            this.lbSearchList.Location = new System.Drawing.Point(0, 0);
            this.lbSearchList.Margin = new System.Windows.Forms.Padding(0);
            this.lbSearchList.Name = "lbSearchList";
            this.lbSearchList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSearchList.Size = new System.Drawing.Size(185, 344);
            this.lbSearchList.TabIndex = 25;
            this.lbSearchList.Visible = false;
            this.lbSearchList.SelectedIndexChanged += new System.EventHandler(this.lbSearchList_SelectedIndexChanged);
            this.lbSearchList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSearchList_MouseDown);
            this.lbSearchList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbSearchList_MouseMove);
            // 
            // upperPanel
            // 
            this.upperPanel.Controls.Add(this.pbSerachTextDel);
            this.upperPanel.Controls.Add(this.cbUsabilityFilter);
            this.upperPanel.Controls.Add(this.lblFilter);
            this.upperPanel.Controls.Add(this.lbSearchTextHint);
            this.upperPanel.Controls.Add(this.tbSearchText);
            this.upperPanel.Controls.Add(this.pbSearchImage);
            this.upperPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.upperPanel.Location = new System.Drawing.Point(0, 0);
            this.upperPanel.Margin = new System.Windows.Forms.Padding(0);
            this.upperPanel.Name = "upperPanel";
            this.upperPanel.Size = new System.Drawing.Size(185, 54);
            this.upperPanel.TabIndex = 26;
            // 
            // pbSerachTextDel
            // 
            this.pbSerachTextDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSerachTextDel.Image = ((System.Drawing.Image)(resources.GetObject("pbSerachTextDel.Image")));
            this.pbSerachTextDel.InitialImage = null;
            this.pbSerachTextDel.Location = new System.Drawing.Point(167, 31);
            this.pbSerachTextDel.Name = "pbSerachTextDel";
            this.pbSerachTextDel.Size = new System.Drawing.Size(16, 16);
            this.pbSerachTextDel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSerachTextDel.TabIndex = 25;
            this.pbSerachTextDel.TabStop = false;
            this.pbSerachTextDel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbSearchTextDel_MouseUp);
            // 
            // cbUsabilityFilter
            // 
            this.cbUsabilityFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUsabilityFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsabilityFilter.FormattingEnabled = true;
            this.cbUsabilityFilter.Items.AddRange(new object[] {
            "All",
            "Can",
            "Can not"});
            this.cbUsabilityFilter.Location = new System.Drawing.Point(45, 3);
            this.cbUsabilityFilter.Margin = new System.Windows.Forms.Padding(0);
            this.cbUsabilityFilter.Name = "cbUsabilityFilter";
            this.cbUsabilityFilter.Size = new System.Drawing.Size(140, 21);
            this.cbUsabilityFilter.TabIndex = 0;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(6, 6);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 24;
            this.lblFilter.Text = resources.GetString("this.lblFilter.Text");
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(9, 30);
            this.pbSearchImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(20, 20);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 20;
            this.pbSearchImage.TabStop = false;
            // 
            // lowerPanel
            // 
            this.lowerPanel.AutoSize = true;
            this.lowerPanel.Controls.Add(this.lbNoMatches);
            this.lowerPanel.Controls.Add(this.lbSearchList);
            this.lowerPanel.Controls.Add(this.tvItems);
            this.lowerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lowerPanel.Location = new System.Drawing.Point(0, 54);
            this.lowerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.lowerPanel.Name = "lowerPanel";
            this.lowerPanel.Size = new System.Drawing.Size(185, 344);
            this.lowerPanel.TabIndex = 27;
            // 
            // tvItems
            // 
            this.tvItems.ContextMenuStrip = this.contextMenu;
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Margin = new System.Windows.Forms.Padding(0);
            this.tvItems.Name = "tvItems";
            this.tvItems.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.tvItems.SelectionMode = EVEMon.Common.Controls.TreeViewSelectionMode.MultiSelect;
            this.tvItems.Size = new System.Drawing.Size(185, 344);
            this.tvItems.TabIndex = 22;
            this.tvItems.SelectionsChanged += new System.EventHandler(this.tvItems_SelectionsChanged);
            this.tvItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseDown);
            this.tvItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseMove);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiLvPlanTo,
            this.tsSeparatorPlanTo,
            this.cmiExpandSelected,
            this.cmiCollapseSelected,
            this.tsSeparatorExpandCollapse,
            this.cmiExpandAll,
            this.cmiCollapseAll});
            this.contextMenu.Name = "cmListSkills";
            this.contextMenu.Size = new System.Drawing.Size(167, 126);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // cmiLvPlanTo
            // 
            this.cmiLvPlanTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLevel1,
            this.tsmLevel2,
            this.tsmLevel3,
            this.tsmLevel4,
            this.tsmLevel5});
            this.cmiLvPlanTo.Image = ((System.Drawing.Image)(resources.GetObject("cmiLvPlanTo.Image")));
            this.cmiLvPlanTo.Name = "cmiLvPlanTo";
            this.cmiLvPlanTo.Size = new System.Drawing.Size(166, 22);
            this.cmiLvPlanTo.Text = resources.GetString("this.cmiLvPlanTo.Text");
            // 
            // tsmLevel1
            // 
            this.tsmLevel1.Name = "tsmLevel1";
            this.tsmLevel1.Size = new System.Drawing.Size(114, 22);
            this.tsmLevel1.Text = resources.GetString("this.tsmLevel1.Text");
            this.tsmLevel1.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel2
            // 
            this.tsmLevel2.Name = "tsmLevel2";
            this.tsmLevel2.Size = new System.Drawing.Size(114, 22);
            this.tsmLevel2.Text = resources.GetString("this.tsmLevel2.Text");
            this.tsmLevel2.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel3
            // 
            this.tsmLevel3.Name = "tsmLevel3";
            this.tsmLevel3.Size = new System.Drawing.Size(114, 22);
            this.tsmLevel3.Text = resources.GetString("this.tsmLevel3.Text");
            this.tsmLevel3.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel4
            // 
            this.tsmLevel4.Name = "tsmLevel4";
            this.tsmLevel4.Size = new System.Drawing.Size(114, 22);
            this.tsmLevel4.Text = resources.GetString("this.tsmLevel4.Text");
            this.tsmLevel4.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsmLevel5
            // 
            this.tsmLevel5.Name = "tsmLevel5";
            this.tsmLevel5.Size = new System.Drawing.Size(114, 22);
            this.tsmLevel5.Text = resources.GetString("this.tsmLevel5.Text");
            this.tsmLevel5.Click += new System.EventHandler(this.planToLevelMenuItem_Click);
            // 
            // tsSeparatorPlanTo
            // 
            this.tsSeparatorPlanTo.Name = "tsSeparatorPlanTo";
            this.tsSeparatorPlanTo.Size = new System.Drawing.Size(163, 6);
            // 
            // cmiExpandSelected
            // 
            this.cmiExpandSelected.Name = "cmiExpandSelected";
            this.cmiExpandSelected.Size = new System.Drawing.Size(166, 22);
            this.cmiExpandSelected.Text = resources.GetString("this.cmiExpandSelected.Text");
            this.cmiExpandSelected.Click += new System.EventHandler(this.cmiExpandSelected_Click);
            // 
            // cmiCollapseSelected
            // 
            this.cmiCollapseSelected.Name = "cmiCollapseSelected";
            this.cmiCollapseSelected.Size = new System.Drawing.Size(166, 22);
            this.cmiCollapseSelected.Text = resources.GetString("this.cmiCollapseSelected.Text");
            this.cmiCollapseSelected.Click += new System.EventHandler(this.cmiCollapseSelected_Click);
            // 
            // tsSeparatorExpandCollapse
            // 
            this.tsSeparatorExpandCollapse.Name = "tsSeparatorExpandCollapse";
            this.tsSeparatorExpandCollapse.Size = new System.Drawing.Size(163, 6);
            // 
            // cmiExpandAll
            // 
            this.cmiExpandAll.Name = "cmiExpandAll";
            this.cmiExpandAll.Size = new System.Drawing.Size(166, 22);
            this.cmiExpandAll.Text = resources.GetString("this.cmiExpandAll.Text");
            this.cmiExpandAll.Click += new System.EventHandler(this.cmiExpandAll_Click);
            // 
            // cmiCollapseAll
            // 
            this.cmiCollapseAll.Name = "cmiCollapseAll";
            this.cmiCollapseAll.Size = new System.Drawing.Size(166, 22);
            this.cmiCollapseAll.Text = resources.GetString("this.cmiCollapseAll.Text");
            this.cmiCollapseAll.Click += new System.EventHandler(this.cmiCollapseAll_Click);
            // 
            // EveObjectSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lowerPanel);
            this.Controls.Add(this.upperPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "EveObjectSelectControl";
            this.Size = new System.Drawing.Size(185, 398);
            this.upperPanel.ResumeLayout(false);
            this.upperPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSerachTextDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.lowerPanel.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TextBox tbSearchText;
        protected System.Windows.Forms.Label lbNoMatches;
        protected System.Windows.Forms.Panel upperPanel;
        protected System.Windows.Forms.Panel lowerPanel;
        protected System.Windows.Forms.Label lbSearchTextHint;
        protected System.Windows.Forms.PictureBox pbSearchImage;
        protected System.Windows.Forms.Label lblFilter;
        protected System.Windows.Forms.ComboBox cbUsabilityFilter;
        protected internal System.Windows.Forms.ListBox lbSearchList;
        protected internal EVEMon.Common.Controls.TreeView tvItems;
        protected System.Windows.Forms.PictureBox pbSerachTextDel;
        private System.Windows.Forms.ToolStripMenuItem cmiLvPlanTo;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel1;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel2;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel3;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel4;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel5;
        private System.Windows.Forms.ToolStripSeparator tsSeparatorPlanTo;
        private System.Windows.Forms.ToolStripMenuItem cmiExpandSelected;
        private System.Windows.Forms.ToolStripMenuItem cmiCollapseSelected;
        private System.Windows.Forms.ToolStripSeparator tsSeparatorExpandCollapse;
        private System.Windows.Forms.ToolStripMenuItem cmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem cmiCollapseAll;
        protected System.Windows.Forms.ContextMenuStrip contextMenu;
    }
}
