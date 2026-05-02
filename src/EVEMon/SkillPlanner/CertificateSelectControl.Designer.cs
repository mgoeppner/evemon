namespace EVEMon.SkillPlanner
{
    partial class CertificateSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificateSelectControl));
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.cbSorting = new System.Windows.Forms.ComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.lbSearchTextHint = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.lbNoMatches = new System.Windows.Forms.Label();
            this.lbSearchList = new System.Windows.Forms.ListBox();
            this.cmListCerts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiLvPlanToLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLevel5 = new System.Windows.Forms.ToolStripMenuItem();
            this.planToSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cmiExpandSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCollapseSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.expandCollapseSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tvItems = new EVEMon.Common.Controls.TreeView();
            this.ilCertIcons = new System.Windows.Forms.ImageList(this.components);
            this.lvSortedList = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSortKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.pbSearchTextDel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.pnlResults.SuspendLayout();
            this.cmListCerts.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchTextDel)).BeginInit();
            this.SuspendLayout();
            // 
            // cbFilter
            // 
            this.cbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Items.AddRange(new object[] {
            "All",
            "Completed",
            "Hide completed",
            "Next level is trainable",
            "Next level is untrainable"});
            this.cbFilter.Location = new System.Drawing.Point(45, 3);
            this.cbFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbFilter.MaxDropDownItems = 12;
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(182, 21);
            this.cbFilter.TabIndex = 51;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // cbSorting
            // 
            this.cbSorting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSorting.FormattingEnabled = true;
            this.cbSorting.Items.AddRange(new object[] {
            "Name",
            "Time to next level",
            "Time to max level"});
            this.cbSorting.Location = new System.Drawing.Point(45, 29);
            this.cbSorting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSorting.Name = "cbSorting";
            this.cbSorting.Size = new System.Drawing.Size(182, 21);
            this.cbSorting.TabIndex = 55;
            this.cbSorting.SelectedIndexChanged += new System.EventHandler(this.cbSorting_SelectedIndexChanged);
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(9, 32);
            this.lblSort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(29, 13);
            this.lblSort.TabIndex = 56;
            this.lblSort.Text = resources.GetString("this.lblSort.Text");
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(9, 54);
            this.pbSearchImage.Margin = new System.Windows.Forms.Padding(2);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(20, 20);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 52;
            this.pbSearchImage.TabStop = false;
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSearchTextHint.AutoSize = true;
            this.lbSearchTextHint.BackColor = System.Drawing.SystemColors.Window;
            this.lbSearchTextHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSearchTextHint.Location = new System.Drawing.Point(49, 57);
            this.lbSearchTextHint.Name = "lbSearchTextHint";
            this.lbSearchTextHint.Size = new System.Drawing.Size(65, 13);
            this.lbSearchTextHint.TabIndex = 53;
            this.lbSearchTextHint.Text = resources.GetString("this.lbSearchTextHint.Text");
            this.lbSearchTextHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSearchTextHint.Click += new System.EventHandler(this.lbSearchTextHint_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(6, 6);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 54;
            this.lblFilter.Text = resources.GetString("this.lblFilter.Text");
            // 
            // tbSearchText
            // 
            this.tbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchText.Location = new System.Drawing.Point(45, 54);
            this.tbSearchText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(182, 20);
            this.tbSearchText.TabIndex = 57;
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            // 
            // pnlResults
            // 
            this.pnlResults.AutoSize = true;
            this.pnlResults.Controls.Add(this.lbNoMatches);
            this.pnlResults.Controls.Add(this.lbSearchList);
            this.pnlResults.Controls.Add(this.tvItems);
            this.pnlResults.Controls.Add(this.lvSortedList);
            this.pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResults.Location = new System.Drawing.Point(0, 78);
            this.pnlResults.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(227, 343);
            this.pnlResults.TabIndex = 60;
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
            this.lbNoMatches.Size = new System.Drawing.Size(227, 343);
            this.lbNoMatches.TabIndex = 23;
            this.lbNoMatches.Text = resources.GetString("this.lbNoMatches.Text");
            this.lbNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbNoMatches.Visible = false;
            // 
            // lbSearchList
            // 
            this.lbSearchList.ContextMenuStrip = this.cmListCerts;
            this.lbSearchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSearchList.FormattingEnabled = true;
            this.lbSearchList.IntegralHeight = false;
            this.lbSearchList.Location = new System.Drawing.Point(0, 0);
            this.lbSearchList.Margin = new System.Windows.Forms.Padding(2);
            this.lbSearchList.Name = "lbSearchList";
            this.lbSearchList.Size = new System.Drawing.Size(227, 343);
            this.lbSearchList.TabIndex = 46;
            this.lbSearchList.Visible = false;
            this.lbSearchList.SelectedIndexChanged += new System.EventHandler(this.lbSearchList_SelectedIndexChanged);
            this.lbSearchList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSearchList_MouseDown);
            this.lbSearchList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbSearchList_MouseMove);
            // 
            // cmListCerts
            // 
            this.cmListCerts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiLvPlanToLevel,
            this.planToSeparator,
            this.cmiExpandSelected,
            this.cmiCollapseSelected,
            this.expandCollapseSeparator,
            this.cmiExpandAll,
            this.cmiCollapseAll});
            this.cmListCerts.Name = "cmListSkills";
            this.cmListCerts.Size = new System.Drawing.Size(167, 126);
            // 
            // cmiLvPlanToLevel
            // 
            this.cmiLvPlanToLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLevel1,
            this.tsmLevel2,
            this.tsmLevel3,
            this.tsmLevel4,
            this.tsmLevel5});
            this.cmiLvPlanToLevel.Image = ((System.Drawing.Image)(resources.GetObject("cmiLvPlanToLevel.Image")));
            this.cmiLvPlanToLevel.Name = "cmiLvPlanToLevel";
            this.cmiLvPlanToLevel.Size = new System.Drawing.Size(166, 22);
            this.cmiLvPlanToLevel.Text = resources.GetString("this.cmiLvPlanToLevel.Text");
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
            // planToSeparator
            // 
            this.planToSeparator.Name = "planToSeparator";
            this.planToSeparator.Size = new System.Drawing.Size(163, 6);
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
            // expandCollapseSeparator
            // 
            this.expandCollapseSeparator.Name = "expandCollapseSeparator";
            this.expandCollapseSeparator.Size = new System.Drawing.Size(163, 6);
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
            // tvItems
            // 
            this.tvItems.ContextMenuStrip = this.cmListCerts;
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.HideSelection = false;
            this.tvItems.ImageIndex = 0;
            this.tvItems.ImageList = this.ilCertIcons;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Margin = new System.Windows.Forms.Padding(2);
            this.tvItems.Name = "tvItems";
            this.tvItems.SelectedImageIndex = 0;
            this.tvItems.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.tvItems.SelectionMode = EVEMon.Common.Controls.TreeViewSelectionMode.SingleSelect;
            this.tvItems.Size = new System.Drawing.Size(227, 343);
            this.tvItems.TabIndex = 20;
            this.tvItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseDown);
            this.tvItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseMove);
            // 
            // ilCertIcons
            // 
            this.ilCertIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCertIcons.ImageStream")));
            this.ilCertIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilCertIcons.Images.SetKeyName(0, "level0");
            this.ilCertIcons.Images.SetKeyName(1, "level1");
            this.ilCertIcons.Images.SetKeyName(2, "level2");
            this.ilCertIcons.Images.SetKeyName(3, "level3");
            this.ilCertIcons.Images.SetKeyName(4, "level4");
            this.ilCertIcons.Images.SetKeyName(5, "level5");
            this.ilCertIcons.Images.SetKeyName(6, "Certificate");
            // 
            // lvSortedList
            // 
            this.lvSortedList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSortKey});
            this.lvSortedList.ContextMenuStrip = this.cmListCerts;
            this.lvSortedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSortedList.FullRowSelect = true;
            this.lvSortedList.HideSelection = false;
            this.lvSortedList.Location = new System.Drawing.Point(0, 0);
            this.lvSortedList.Margin = new System.Windows.Forms.Padding(2);
            this.lvSortedList.Name = "lvSortedList";
            this.lvSortedList.Size = new System.Drawing.Size(227, 343);
            this.lvSortedList.TabIndex = 28;
            this.lvSortedList.TileSize = new System.Drawing.Size(16, 16);
            this.lvSortedList.UseCompatibleStateImageBehavior = false;
            this.lvSortedList.View = System.Windows.Forms.View.Details;
            this.lvSortedList.Visible = false;
            this.lvSortedList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvSortedList_MouseDown);
            this.lvSortedList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvSortedList_MouseMove);
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
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lbSearchTextHint);
            this.pnlFilter.Controls.Add(this.pbSearchTextDel);
            this.pnlFilter.Controls.Add(this.cbSorting);
            this.pnlFilter.Controls.Add(this.cbFilter);
            this.pnlFilter.Controls.Add(this.tbSearchText);
            this.pnlFilter.Controls.Add(this.lblFilter);
            this.pnlFilter.Controls.Add(this.pbSearchImage);
            this.pnlFilter.Controls.Add(this.lblSort);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(227, 78);
            this.pnlFilter.TabIndex = 47;
            // 
            // pbSearchTextDel
            // 
            this.pbSearchTextDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSearchTextDel.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchTextDel.Image")));
            this.pbSearchTextDel.InitialImage = null;
            this.pbSearchTextDel.Location = new System.Drawing.Point(209, 56);
            this.pbSearchTextDel.Name = "pbSearchTextDel";
            this.pbSearchTextDel.Size = new System.Drawing.Size(16, 16);
            this.pbSearchTextDel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchTextDel.TabIndex = 58;
            this.pbSearchTextDel.TabStop = false;
            this.pbSearchTextDel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbSearchTextDel_MouseUp);
            // 
            // CertificateSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlResults);
            this.Controls.Add(this.pnlFilter);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CertificateSelectControl";
            this.Size = new System.Drawing.Size(227, 421);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.pnlResults.ResumeLayout(false);
            this.cmListCerts.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchTextDel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.ComboBox cbSorting;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.Label lbSearchTextHint;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.Label lbNoMatches;
        private System.Windows.Forms.ListBox lbSearchList;
        private EVEMon.Common.Controls.TreeView tvItems;
        private System.Windows.Forms.ListView lvSortedList;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSortKey;
        private System.Windows.Forms.ImageList ilCertIcons;
        private System.Windows.Forms.ContextMenuStrip cmListCerts;
        private System.Windows.Forms.ToolStripMenuItem cmiLvPlanToLevel;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel1;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel2;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel3;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel4;
        private System.Windows.Forms.ToolStripMenuItem tsmLevel5;
        private System.Windows.Forms.ToolStripSeparator planToSeparator;
        private System.Windows.Forms.ToolStripMenuItem cmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem cmiCollapseAll;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.ToolStripMenuItem cmiExpandSelected;
        private System.Windows.Forms.ToolStripMenuItem cmiCollapseSelected;
        private System.Windows.Forms.ToolStripSeparator expandCollapseSeparator;
        private System.Windows.Forms.PictureBox pbSearchTextDel;
    }
}
