namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterResearchPointsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterResearchPointsList));
            this.noResearchPointsLabel = new System.Windows.Forms.Label();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.chAgentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSolarSystem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSkill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCurrentRP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPRPerDay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvResearchPoints = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInSkillBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showObjectInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInSkillBrowserMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // noResearchPointsLabel
            // 
            this.noResearchPointsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noResearchPointsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noResearchPointsLabel.Location = new System.Drawing.Point(0, 0);
            this.noResearchPointsLabel.Name = "noResearchPointsLabel";
            this.noResearchPointsLabel.Size = new System.Drawing.Size(454, 434);
            this.noResearchPointsLabel.TabIndex = 1;
            this.noResearchPointsLabel.Text = resources.GetString("this.noResearchPointsLabel.Text");
            this.noResearchPointsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "arrow_up.png");
            this.ilIcons.Images.SetKeyName(1, "arrow_down.png");
            this.ilIcons.Images.SetKeyName(2, "16x16Transparant.png");
            // 
            // chAgentName
            // 
            this.chAgentName.Text = resources.GetString("this.chAgentName.Text");
            // 
            // chSolarSystem
            // 
            this.chSolarSystem.Text = resources.GetString("this.chSolarSystem.Text");
            // 
            // chSkill
            // 
            this.chSkill.Text = resources.GetString("this.chSkill.Text");
            // 
            // chCurrentRP
            // 
            this.chCurrentRP.Text = resources.GetString("this.chCurrentRP.Text");
            // 
            // chPRPerDay
            // 
            this.chPRPerDay.Text = resources.GetString("this.chPRPerDay.Text");
            // 
            // lvResearchPoints
            // 
            this.lvResearchPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvResearchPoints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAgentName,
            this.chSolarSystem,
            this.chSkill,
            this.chCurrentRP,
            this.chPRPerDay});
            this.lvResearchPoints.ContextMenuStrip = this.contextMenu;
            this.lvResearchPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvResearchPoints.FullRowSelect = true;
            this.lvResearchPoints.HideSelection = false;
            this.lvResearchPoints.Location = new System.Drawing.Point(0, 0);
            this.lvResearchPoints.MultiSelect = false;
            this.lvResearchPoints.Name = "lvResearchPoints";
            this.lvResearchPoints.Size = new System.Drawing.Size(454, 434);
            this.lvResearchPoints.SmallImageList = this.ilIcons;
            this.lvResearchPoints.TabIndex = 0;
            this.lvResearchPoints.UseCompatibleStateImageBehavior = false;
            this.lvResearchPoints.View = System.Windows.Forms.View.Details;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInSkillBrowserMenuItem,
            this.showObjectInBrowserMenuItem,
            this.showInSkillBrowserMenuSeparator,
            this.exportToCSVToolStripMenuItem});
            this.contextMenu.Name = "ShipPropertiesContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(261, 98);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // showInSkillBrowserMenuItem
            // 
            this.showInSkillBrowserMenuItem.Name = "showInSkillBrowserMenuItem";
            this.showInSkillBrowserMenuItem.Size = new System.Drawing.Size(260, 22);
            this.showInSkillBrowserMenuItem.Text = resources.GetString("this.showInSkillBrowserMenuItem.Text");
            this.showInSkillBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // showObjectInBrowserMenuItem
            // 
            this.showObjectInBrowserMenuItem.Name = "showObjectInBrowserMenuItem";
            this.showObjectInBrowserMenuItem.Size = new System.Drawing.Size(260, 22);
            this.showObjectInBrowserMenuItem.Text = resources.GetString("this.showObjectInBrowserMenuItem.Text");
            this.showObjectInBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // showInSkillBrowserMenuSeparator
            // 
            this.showInSkillBrowserMenuSeparator.Name = "showInSkillBrowserMenuSeparator";
            this.showInSkillBrowserMenuSeparator.Size = new System.Drawing.Size(257, 6);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.exportToCSVToolStripMenuItem.Text = resources.GetString("this.exportToCSVToolStripMenuItem.Text");
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
            // 
            // CharacterResearchPointsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvResearchPoints);
            this.Controls.Add(this.noResearchPointsLabel);
            this.Name = "CharacterResearchPointsList";
            this.Size = new System.Drawing.Size(454, 434);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noResearchPointsLabel;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ColumnHeader chAgentName;
        private System.Windows.Forms.ColumnHeader chSolarSystem;
        private System.Windows.Forms.ColumnHeader chSkill;
        private System.Windows.Forms.ColumnHeader chCurrentRP;
        private System.Windows.Forms.ColumnHeader chPRPerDay;
        private System.Windows.Forms.ListView lvResearchPoints;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowserMenuItem;
        private System.Windows.Forms.ToolStripSeparator showInSkillBrowserMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem showObjectInBrowserMenuItem;
    }
}
