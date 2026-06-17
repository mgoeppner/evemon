namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterWalletTransactionsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterWalletTransactionsList));
            this.noWalletTransactionsLabel = new System.Windows.Forms.Label();
            this.lvWalletTransactions = new System.Windows.Forms.ListView();
            this.chDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // noWalletTransactionsLabel
            // 
            this.noWalletTransactionsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noWalletTransactionsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noWalletTransactionsLabel.Location = new System.Drawing.Point(0, 0);
            this.noWalletTransactionsLabel.Name = "noWalletTransactionsLabel";
            this.noWalletTransactionsLabel.Size = new System.Drawing.Size(454, 434);
            this.noWalletTransactionsLabel.TabIndex = 3;
            this.noWalletTransactionsLabel.Text = resources.GetString("this.noWalletTransactionsLabel.Text");
            this.noWalletTransactionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvWalletTransactions
            // 
            this.lvWalletTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvWalletTransactions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chItem,
            this.chPrice,
            this.chQuantity});
            this.lvWalletTransactions.ContextMenuStrip = this.contextMenu;
            this.lvWalletTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWalletTransactions.FullRowSelect = true;
            this.lvWalletTransactions.HideSelection = false;
            this.lvWalletTransactions.Location = new System.Drawing.Point(0, 0);
            this.lvWalletTransactions.MultiSelect = false;
            this.lvWalletTransactions.Name = "lvWalletTransactions";
            this.lvWalletTransactions.Size = new System.Drawing.Size(454, 434);
            this.lvWalletTransactions.SmallImageList = this.ilIcons;
            this.lvWalletTransactions.TabIndex = 4;
            this.lvWalletTransactions.UseCompatibleStateImageBehavior = false;
            this.lvWalletTransactions.View = System.Windows.Forms.View.Details;
            // 
            // chDate
            // 
            this.chDate.Text = resources.GetString("this.chDate.Text");
            this.chDate.Width = 125;
            // 
            // chItem
            // 
            this.chItem.Text = resources.GetString("this.chItem.Text");
            this.chItem.Width = 169;
            // 
            // chPrice
            // 
            this.chPrice.Text = resources.GetString("this.chPrice.Text");
            this.chPrice.Width = 77;
            // 
            // chQuantity
            // 
            this.chQuantity.Text = resources.GetString("this.chQuantity.Text");
            this.chQuantity.Width = 81;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToCSVToolStripMenuItem});
            this.contextMenu.Name = "ShipPropertiesContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(157, 26);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
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
            // CharacterWalletTransactionsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvWalletTransactions);
            this.Controls.Add(this.noWalletTransactionsLabel);
            this.Name = "CharacterWalletTransactionsList";
            this.Size = new System.Drawing.Size(454, 434);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noWalletTransactionsLabel;
        private System.Windows.Forms.ListView lvWalletTransactions;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.ColumnHeader chPrice;
        private System.Windows.Forms.ColumnHeader chQuantity;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
    }
}
