namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterAssetsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterAssetsList));
            this.noAssetsLabel = new System.Windows.Forms.Label();
            this.lvAssets = new System.Windows.Forms.ListView();
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVolume = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.noPricesFoundLabel = new System.Windows.Forms.Label();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.totalCostThrobber = new EVEMon.Common.Controls.Throbber();
            this.estimatedCostPanel = new System.Windows.Forms.Panel();
            this.throbber = new EVEMon.Common.Controls.Throbber();
            this.showInBrowserMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalCostThrobber)).BeginInit();
            this.estimatedCostPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.throbber)).BeginInit();
            this.SuspendLayout();
            // 
            // noAssetsLabel
            // 
            this.noAssetsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noAssetsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noAssetsLabel.Location = new System.Drawing.Point(0, 0);
            this.noAssetsLabel.Name = "noAssetsLabel";
            this.noAssetsLabel.Size = new System.Drawing.Size(472, 401);
            this.noAssetsLabel.TabIndex = 3;
            this.noAssetsLabel.Text = resources.GetString("this.noAssetsLabel.Text");
            this.noAssetsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvAssets
            // 
            this.lvAssets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvAssets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem,
            this.chQuantity,
            this.chVolume,
            this.chGroup,
            this.chCategory});
            this.lvAssets.ContextMenuStrip = this.contextMenu;
            this.lvAssets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssets.FullRowSelect = true;
            this.lvAssets.HideSelection = false;
            this.lvAssets.Location = new System.Drawing.Point(0, 0);
            this.lvAssets.Name = "lvAssets";
            this.lvAssets.Size = new System.Drawing.Size(472, 401);
            this.lvAssets.SmallImageList = this.ilIcons;
            this.lvAssets.TabIndex = 3;
            this.lvAssets.UseCompatibleStateImageBehavior = false;
            this.lvAssets.View = System.Windows.Forms.View.Details;
            // 
            // chItem
            // 
            this.chItem.Text = resources.GetString("this.chItem.Text");
            this.chItem.Width = 166;
            // 
            // chQuantity
            // 
            this.chQuantity.Text = resources.GetString("this.chQuantity.Text");
            this.chQuantity.Width = 72;
            // 
            // chVolume
            // 
            this.chVolume.Text = resources.GetString("this.chVolume.Text");
            this.chVolume.Width = 74;
            // 
            // chGroup
            // 
            this.chGroup.Text = resources.GetString("this.chGroup.Text");
            this.chGroup.Width = 80;
            // 
            // chCategory
            // 
            this.chCategory.Text = resources.GetString("this.chCategory.Text");
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInBrowserMenuItem,
            this.showInBrowserMenuSeparator,
            this.exportToCSVToolStripMenuItem});
            this.contextMenu.Name = "ItemAttributeContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(171, 76);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
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
            // noPricesFoundLabel
            // 
            this.noPricesFoundLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.noPricesFoundLabel.AutoSize = true;
            this.noPricesFoundLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.noPricesFoundLabel.Location = new System.Drawing.Point(258, 9);
            this.noPricesFoundLabel.Name = "noPricesFoundLabel";
            this.noPricesFoundLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.noPricesFoundLabel.Size = new System.Drawing.Size(208, 13);
            this.noPricesFoundLabel.TabIndex = 1;
            this.noPricesFoundLabel.Text = resources.GetString("this.noPricesFoundLabel.Text");
            this.noPricesFoundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(3, 9);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(207, 13);
            this.lblTotalCost.TabIndex = 0;
            this.lblTotalCost.Text = resources.GetString("this.lblTotalCost.Text");
            this.lblTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalCostThrobber
            // 
            this.totalCostThrobber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCostThrobber.Location = new System.Drawing.Point(442, 4);
            this.totalCostThrobber.MaximumSize = new System.Drawing.Size(24, 24);
            this.totalCostThrobber.MinimumSize = new System.Drawing.Size(24, 24);
            this.totalCostThrobber.Name = "totalCostThrobber";
            this.totalCostThrobber.Size = new System.Drawing.Size(24, 24);
            this.totalCostThrobber.State = EVEMon.Common.Enumerations.ThrobberState.Stopped;
            this.totalCostThrobber.TabIndex = 3;
            this.totalCostThrobber.TabStop = false;
            this.totalCostThrobber.Visible = false;
            // 
            // estimatedCostPanel
            // 
            this.estimatedCostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.estimatedCostPanel.Controls.Add(this.lblTotalCost);
            this.estimatedCostPanel.Controls.Add(this.noPricesFoundLabel);
            this.estimatedCostPanel.Controls.Add(this.totalCostThrobber);
            this.estimatedCostPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.estimatedCostPanel.Location = new System.Drawing.Point(0, 401);
            this.estimatedCostPanel.Name = "estimatedCostPanel";
            this.estimatedCostPanel.Size = new System.Drawing.Size(472, 33);
            this.estimatedCostPanel.TabIndex = 5;
            // 
            // throbber
            // 
            this.throbber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.throbber.Location = new System.Drawing.Point(229, 205);
            this.throbber.MaximumSize = new System.Drawing.Size(24, 24);
            this.throbber.MinimumSize = new System.Drawing.Size(24, 24);
            this.throbber.Name = "throbber";
            this.throbber.Size = new System.Drawing.Size(24, 24);
            this.throbber.State = EVEMon.Common.Enumerations.ThrobberState.Stopped;
            this.throbber.TabIndex = 6;
            this.throbber.TabStop = false;
            this.throbber.Visible = false;
            // 
            // showInBrowserMenuSeparator
            // 
            this.showInBrowserMenuSeparator.Name = "showInBrowserMenuSeparator";
            this.showInBrowserMenuSeparator.Size = new System.Drawing.Size(167, 6);
            // 
            // showInBrowserMenuItem
            // 
            this.showInBrowserMenuItem.Name = "showInBrowserMenuItem";
            this.showInBrowserMenuItem.Size = new System.Drawing.Size(170, 22);
            this.showInBrowserMenuItem.Text = resources.GetString("this.showInBrowserMenuItem.Text");
            this.showInBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // CharacterAssetsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvAssets);
            this.Controls.Add(this.throbber);
            this.Controls.Add(this.noAssetsLabel);
            this.Controls.Add(this.estimatedCostPanel);
            this.Name = "CharacterAssetsList";
            this.Size = new System.Drawing.Size(472, 434);
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.totalCostThrobber)).EndInit();
            this.estimatedCostPanel.ResumeLayout(false);
            this.estimatedCostPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.throbber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noAssetsLabel;
        private System.Windows.Forms.ListView lvAssets;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.ColumnHeader chQuantity;
        private System.Windows.Forms.ColumnHeader chVolume;
        private System.Windows.Forms.ColumnHeader chGroup;
        private System.Windows.Forms.ColumnHeader chCategory;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label noPricesFoundLabel;
        private Common.Controls.Throbber totalCostThrobber;
        private System.Windows.Forms.Panel estimatedCostPanel;
        private Common.Controls.Throbber throbber;
        private System.Windows.Forms.ToolStripMenuItem showInBrowserMenuItem;
        private System.Windows.Forms.ToolStripSeparator showInBrowserMenuSeparator;
    }
}
