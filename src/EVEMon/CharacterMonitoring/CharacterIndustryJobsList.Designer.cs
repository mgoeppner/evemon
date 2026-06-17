using EVEMon.Common.Controls;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterIndustryJobsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterIndustryJobsList));
            this.lvJobs = new System.Windows.Forms.ListView();
            this.chState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTTC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chInstalledItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOutputItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInstalledInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInBrowserMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.noJobsLabel = new System.Windows.Forms.Label();
            this.industryExpPanelControl = new EVEMon.Common.Controls.ExpandablePanelControl();
            this.showProducedInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvJobs
            // 
            this.lvJobs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chState,
            this.chTTC,
            this.chInstalledItem,
            this.chOutputItem});
            this.lvJobs.ContextMenuStrip = this.contextMenu;
            this.lvJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvJobs.FullRowSelect = true;
            this.lvJobs.HideSelection = false;
            this.lvJobs.Location = new System.Drawing.Point(0, 0);
            this.lvJobs.MultiSelect = false;
            this.lvJobs.Name = "lvJobs";
            this.lvJobs.Size = new System.Drawing.Size(454, 334);
            this.lvJobs.SmallImageList = this.ilIcons;
            this.lvJobs.TabIndex = 0;
            this.lvJobs.UseCompatibleStateImageBehavior = false;
            this.lvJobs.View = System.Windows.Forms.View.Details;
            // 
            // chState
            // 
            this.chState.Text = resources.GetString("this.chState.Text");
            // 
            // chTTC
            // 
            this.chTTC.Text = resources.GetString("this.chTTC.Text");
            // 
            // chInstalledItem
            // 
            this.chInstalledItem.Text = resources.GetString("this.chInstalledItem.Text");
            // 
            // chOutputItem
            // 
            this.chOutputItem.Text = resources.GetString("this.chOutputItem.Text");
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInstalledInBrowserMenuItem,
            this.showProducedInBrowserMenuItem,
            this.showInBrowserMenuSeparator,
            this.exportToCSVToolStripMenuItem});
            this.contextMenu.Name = "ShipPropertiesContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(218, 98);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // showInstalledInBrowserMenuItem
            // 
            this.showInstalledInBrowserMenuItem.Name = "showInstalledInBrowserMenuItem";
            this.showInstalledInBrowserMenuItem.Size = new System.Drawing.Size(217, 22);
            this.showInstalledInBrowserMenuItem.Text = resources.GetString("this.showInstalledInBrowserMenuItem.Text");
            this.showInstalledInBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // showProducedInBrowserMenuItem
            // 
            this.showProducedInBrowserMenuItem.Name = "showProducedInBrowserMenuItem";
            this.showProducedInBrowserMenuItem.Size = new System.Drawing.Size(217, 22);
            this.showProducedInBrowserMenuItem.Text = resources.GetString("this.showProducedInBrowserMenuItem.Text");
            this.showProducedInBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // showInBrowserMenuSeparator
            // 
            this.showInBrowserMenuSeparator.Name = "showInBrowserMenuSeparator";
            this.showInBrowserMenuSeparator.Size = new System.Drawing.Size(214, 6);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exportToCSVToolStripMenuItem.Text = resources.GetString("this.exportToCSVToolStripMenuItem.Text");
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "arrow_up.png");
            this.ilIcons.Images.SetKeyName(1, "arrow_down.png");
            this.ilIcons.Images.SetKeyName(2, "16x16Transparant.png");
            // 
            // noJobsLabel
            // 
            this.noJobsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noJobsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noJobsLabel.Location = new System.Drawing.Point(0, 0);
            this.noJobsLabel.Name = "noJobsLabel";
            this.noJobsLabel.Size = new System.Drawing.Size(454, 434);
            this.noJobsLabel.TabIndex = 2;
            this.noJobsLabel.Text = resources.GetString("this.noJobsLabel.Text");
            this.noJobsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // industryExpPanelControl
            // 
            this.industryExpPanelControl.AnimationSpeed = EVEMon.Common.Controls.AnimationSpeed.Medium;
            this.industryExpPanelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.industryExpPanelControl.EnableContextMenu = false;
            this.industryExpPanelControl.ExpandDirection = EVEMon.Common.Controls.Direction.Down;
            this.industryExpPanelControl.ExpandedHeight = 100;
            this.industryExpPanelControl.ExpandedOnStartup = false;
            this.industryExpPanelControl.HeaderHeight = 30;
            this.industryExpPanelControl.HeaderText = resources.GetString("this.industryExpPanelControl.HeaderText");
            this.industryExpPanelControl.ImageCollapse = ((System.Drawing.Bitmap)(resources.GetObject("industryExpPanelControl.ImageCollapse")));
            this.industryExpPanelControl.ImageExpand = ((System.Drawing.Bitmap)(resources.GetObject("industryExpPanelControl.ImageExpand")));
            this.industryExpPanelControl.Location = new System.Drawing.Point(0, 334);
            this.industryExpPanelControl.Name = "industryExpPanelControl";
            this.industryExpPanelControl.Size = new System.Drawing.Size(454, 100);
            this.industryExpPanelControl.TabIndex = 1;
            // 
            // CharacterIndustryJobsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvJobs);
            this.Controls.Add(this.industryExpPanelControl);
            this.Controls.Add(this.noJobsLabel);
            this.Name = "CharacterIndustryJobsList";
            this.Size = new System.Drawing.Size(454, 434);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvJobs;
        private System.Windows.Forms.Label noJobsLabel;
        private System.Windows.Forms.ColumnHeader chState;
        private System.Windows.Forms.ColumnHeader chTTC;
        private System.Windows.Forms.ColumnHeader chInstalledItem;
        private System.Windows.Forms.ColumnHeader chOutputItem;
        private System.Windows.Forms.ImageList ilIcons;
        private ExpandablePanelControl industryExpPanelControl;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInstalledInBrowserMenuItem;
        private System.Windows.Forms.ToolStripSeparator showInBrowserMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem showProducedInBrowserMenuItem;
    }
}
