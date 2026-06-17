namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterWalletJournalList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterWalletJournalList));
            this.noWalletJournalLabel = new System.Windows.Forms.Label();
            this.lvWalletJournal = new System.Windows.Forms.ListView();
            this.chDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBalance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // noWalletJournalLabel
            // 
            this.noWalletJournalLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noWalletJournalLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noWalletJournalLabel.Location = new System.Drawing.Point(0, 0);
            this.noWalletJournalLabel.Name = "noWalletJournalLabel";
            this.noWalletJournalLabel.Size = new System.Drawing.Size(454, 434);
            this.noWalletJournalLabel.TabIndex = 3;
            this.noWalletJournalLabel.Text = resources.GetString("this.noWalletJournalLabel.Text");
            this.noWalletJournalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvWalletJournal
            // 
            this.lvWalletJournal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvWalletJournal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chType,
            this.chAmount,
            this.chBalance});
            this.lvWalletJournal.ContextMenuStrip = this.contextMenu;
            this.lvWalletJournal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWalletJournal.FullRowSelect = true;
            this.lvWalletJournal.HideSelection = false;
            this.lvWalletJournal.Location = new System.Drawing.Point(0, 0);
            this.lvWalletJournal.MultiSelect = false;
            this.lvWalletJournal.Name = "lvWalletJournal";
            this.lvWalletJournal.Size = new System.Drawing.Size(454, 434);
            this.lvWalletJournal.SmallImageList = this.ilIcons;
            this.lvWalletJournal.TabIndex = 4;
            this.lvWalletJournal.UseCompatibleStateImageBehavior = false;
            this.lvWalletJournal.View = System.Windows.Forms.View.Details;
            // 
            // chDate
            // 
            this.chDate.Text = resources.GetString("this.chDate.Text");
            this.chDate.Width = 106;
            // 
            // chType
            // 
            this.chType.Text = resources.GetString("this.chType.Text");
            this.chType.Width = 100;
            // 
            // chAmount
            // 
            this.chAmount.Text = resources.GetString("this.chAmount.Text");
            this.chAmount.Width = 99;
            // 
            // chBalance
            // 
            this.chBalance.Text = resources.GetString("this.chBalance.Text");
            this.chBalance.Width = 106;
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "arrow_up.png");
            this.ilIcons.Images.SetKeyName(1, "arrow_down.png");
            this.ilIcons.Images.SetKeyName(2, "16x16Transparant.png");
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
            // CharacterWalletJournalList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvWalletJournal);
            this.Controls.Add(this.noWalletJournalLabel);
            this.Name = "CharacterWalletJournalList";
            this.Size = new System.Drawing.Size(454, 434);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noWalletJournalLabel;
        private System.Windows.Forms.ListView lvWalletJournal;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chAmount;
        private System.Windows.Forms.ColumnHeader chBalance;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
    }
}
