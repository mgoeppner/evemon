namespace EVEMon.SkillPlanner
{
    sealed partial class CertificateTreeDisplayControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificateTreeDisplayControl));
            this.cmListSkills = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.planToLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.planToLevelSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInBrowserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showInExplorerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showInMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExpandSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCollapseSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.imageListCertLevels = new System.Windows.Forms.ImageList(this.components);
            this.treeView = new EVEMon.SkillPlanner.OverridenTreeView();
            this.cmListSkills.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmListSkills
            // 
            this.cmListSkills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planToLevel,
            this.planToLevelSeparator,
            this.showInBrowserMenu,
            this.showInExplorerMenu,
            this.showInMenuSeparator,
            this.tsmExpandSelected,
            this.tsmCollapseSelected,
            this.toggleSeparator,
            this.tsmExpandAll,
            this.tsmCollapseAll});
            this.cmListSkills.Name = "cmListSkills";
            this.cmListSkills.Size = new System.Drawing.Size(195, 198);
            this.cmListSkills.Opening += new System.ComponentModel.CancelEventHandler(this.cmListSkills_Opening);
            // 
            // planToLevel
            // 
            this.planToLevel.Image = ((System.Drawing.Image)(resources.GetObject("planToLevel.Image")));
            this.planToLevel.Name = "planToLevel";
            this.planToLevel.Size = new System.Drawing.Size(194, 22);
            this.planToLevel.Text = resources.GetString("this.planToLevel.Text");
            this.planToLevel.Click += new System.EventHandler(this.tsmAddToPlan_Click);
            // 
            // planToLevelSeparator
            // 
            this.planToLevelSeparator.Name = "planToLevelSeparator";
            this.planToLevelSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // showInBrowserMenu
            // 
            this.showInBrowserMenu.Name = "showInBrowserMenu";
            this.showInBrowserMenu.Size = new System.Drawing.Size(194, 22);
            this.showInBrowserMenu.Text = resources.GetString("this.showInBrowserMenu.Text");
            this.showInBrowserMenu.Click += new System.EventHandler(this.showInBrowserMenu_Click);
            // 
            // showInExplorerMenu
            // 
            this.showInExplorerMenu.Image = ((System.Drawing.Image)(resources.GetObject("showInExplorerMenu.Image")));
            this.showInExplorerMenu.Name = "showInExplorerMenu";
            this.showInExplorerMenu.Size = new System.Drawing.Size(194, 22);
            this.showInExplorerMenu.Text = resources.GetString("this.showInExplorerMenu.Text");
            this.showInExplorerMenu.Click += new System.EventHandler(this.showInExplorerMenu_Click);
            // 
            // showInMenuSeparator
            // 
            this.showInMenuSeparator.Name = "showInMenuSeparator";
            this.showInMenuSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmExpandSelected
            // 
            this.tsmExpandSelected.Name = "tsmExpandSelected";
            this.tsmExpandSelected.Size = new System.Drawing.Size(194, 22);
            this.tsmExpandSelected.Text = resources.GetString("this.tsmExpandSelected.Text");
            this.tsmExpandSelected.Click += new System.EventHandler(this.tsmExpandSelected_Click);
            // 
            // tsmCollapseSelected
            // 
            this.tsmCollapseSelected.Name = "tsmCollapseSelected";
            this.tsmCollapseSelected.Size = new System.Drawing.Size(194, 22);
            this.tsmCollapseSelected.Text = resources.GetString("this.tsmCollapseSelected.Text");
            this.tsmCollapseSelected.Click += new System.EventHandler(this.tsmCollapseSelected_Click);
            // 
            // toggleSeparator
            // 
            this.toggleSeparator.Name = "toggleSeparator";
            this.toggleSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmExpandAll
            // 
            this.tsmExpandAll.Name = "tsmExpandAll";
            this.tsmExpandAll.Size = new System.Drawing.Size(194, 22);
            this.tsmExpandAll.Text = resources.GetString("this.tsmExpandAll.Text");
            this.tsmExpandAll.Click += new System.EventHandler(this.tsmExpandAll_Click);
            // 
            // tsmCollapseAll
            // 
            this.tsmCollapseAll.Name = "tsmCollapseAll";
            this.tsmCollapseAll.Size = new System.Drawing.Size(194, 22);
            this.tsmCollapseAll.Text = resources.GetString("this.tsmCollapseAll.Text");
            this.tsmCollapseAll.Click += new System.EventHandler(this.tsmCollapseAll_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Trained");
            this.imageList.Images.SetKeyName(1, "Trainable");
            this.imageList.Images.SetKeyName(2, "Untrainable");
            this.imageList.Images.SetKeyName(3, "Certificate");
            this.imageList.Images.SetKeyName(4, "Skillbook");
            this.imageList.Images.SetKeyName(5, "Planned");
            // 
            // imageListCertLevels
            // 
            this.imageListCertLevels.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCertLevels.ImageStream")));
            this.imageListCertLevels.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCertLevels.Images.SetKeyName(0, "level0");
            this.imageListCertLevels.Images.SetKeyName(1, "level1");
            this.imageListCertLevels.Images.SetKeyName(2, "level2");
            this.imageListCertLevels.Images.SetKeyName(3, "level3");
            this.imageListCertLevels.Images.SetKeyName(4, "level4");
            this.imageListCertLevels.Images.SetKeyName(5, "level5");
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.ContextMenuStrip = this.cmListSkills;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView.FullRowSelect = true;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Indent = 27;
            this.treeView.ItemHeight = 1;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(226, 282);
            this.treeView.TabIndex = 0;
            this.treeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_DrawNode);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            this.treeView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseMove);
            // 
            // CertificateTreeDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.treeView);
            this.Name = "CertificateTreeDisplayControl";
            this.Size = new System.Drawing.Size(226, 282);
            this.cmListSkills.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OverridenTreeView treeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip cmListSkills;
        private System.Windows.Forms.ToolStripSeparator toggleSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem planToLevel;
        private System.Windows.Forms.ToolStripMenuItem showInBrowserMenu;
        private System.Windows.Forms.ToolStripSeparator planToLevelSeparator;
        private System.Windows.Forms.ToolStripMenuItem showInExplorerMenu;
        private System.Windows.Forms.ToolStripSeparator showInMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmExpandSelected;
        private System.Windows.Forms.ToolStripMenuItem tsmCollapseSelected;
        private System.Windows.Forms.ImageList imageListCertLevels;
    }
}
