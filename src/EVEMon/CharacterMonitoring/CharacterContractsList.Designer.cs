namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterContractsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterContractsList));
            this.noContractsLabel = new System.Windows.Forms.Label();
            this.lvContracts = new System.Windows.Forms.ListView();
            this.chContract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIssuer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAssignee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIssued = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRemainingTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDetailsMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInBrowserMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // noContractsLabel
            // 
            this.noContractsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noContractsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noContractsLabel.Location = new System.Drawing.Point(0, 0);
            this.noContractsLabel.Name = "noContractsLabel";
            this.noContractsLabel.Size = new System.Drawing.Size(454, 434);
            this.noContractsLabel.TabIndex = 2;
            this.noContractsLabel.Text = resources.GetString("this.noContractsLabel.Text");
            this.noContractsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvContracts
            // 
            this.lvContracts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvContracts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chContract,
            this.chType,
            this.chStatus,
            this.chIssuer,
            this.chAssignee,
            this.chIssued,
            this.chRemainingTime});
            this.lvContracts.ContextMenuStrip = this.contextMenu;
            this.lvContracts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvContracts.FullRowSelect = true;
            this.lvContracts.HideSelection = false;
            this.lvContracts.Location = new System.Drawing.Point(0, 0);
            this.lvContracts.MultiSelect = false;
            this.lvContracts.Name = "lvContracts";
            this.lvContracts.Size = new System.Drawing.Size(454, 434);
            this.lvContracts.SmallImageList = this.ilIcons;
            this.lvContracts.TabIndex = 3;
            this.lvContracts.UseCompatibleStateImageBehavior = false;
            this.lvContracts.View = System.Windows.Forms.View.Details;
            this.lvContracts.DoubleClick += new System.EventHandler(this.lvContracts_DoubleClick);
            // 
            // chContract
            // 
            this.chContract.Text = resources.GetString("this.chContract.Text");
            // 
            // chType
            // 
            this.chType.Text = resources.GetString("this.chType.Text");
            // 
            // chStatus
            // 
            this.chStatus.Text = resources.GetString("this.chStatus.Text");
            // 
            // chIssuer
            // 
            this.chIssuer.Text = resources.GetString("this.chIssuer.Text");
            // 
            // chAssignee
            // 
            this.chAssignee.Text = resources.GetString("this.chAssignee.Text");
            // 
            // chIssued
            // 
            this.chIssued.Text = resources.GetString("this.chIssued.Text");
            // 
            // chRemainingTime
            // 
            this.chRemainingTime.Text = resources.GetString("this.chRemainingTime.Text");
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.showDetailsMenuSeparator,
            this.showInBrowserMenuItem,
            this.showInBrowserMenuSeparator,
            this.exportToCSVToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(171, 82);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.showDetailsToolStripMenuItem.Text = resources.GetString("this.showDetailsToolStripMenuItem.Text");
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // showDetailsMenuSeparator
            // 
            this.showDetailsMenuSeparator.Name = "showDetailsMenuSeparator";
            this.showDetailsMenuSeparator.Size = new System.Drawing.Size(167, 6);
            // 
            // showInBrowserMenuItem
            // 
            this.showInBrowserMenuItem.Name = "showInBrowserMenuItem";
            this.showInBrowserMenuItem.Size = new System.Drawing.Size(170, 22);
            this.showInBrowserMenuItem.Text = resources.GetString("this.showInBrowserMenuItem.Text");
            this.showInBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // showInBrowserMenuSeparator
            // 
            this.showInBrowserMenuSeparator.Name = "showInBrowserMenuSeparator";
            this.showInBrowserMenuSeparator.Size = new System.Drawing.Size(167, 6);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
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
            // CharacterContractsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvContracts);
            this.Controls.Add(this.noContractsLabel);
            this.Name = "CharacterContractsList";
            this.Size = new System.Drawing.Size(454, 434);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noContractsLabel;
        private System.Windows.Forms.ListView lvContracts;
        private System.Windows.Forms.ColumnHeader chContract;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chIssuer;
        private System.Windows.Forms.ColumnHeader chAssignee;
        private System.Windows.Forms.ColumnHeader chIssued;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chRemainingTime;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator showDetailsMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInBrowserMenuItem;
        private System.Windows.Forms.ToolStripSeparator showInBrowserMenuSeparator;
    }
}
