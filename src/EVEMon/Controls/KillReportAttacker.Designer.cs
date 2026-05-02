namespace EVEMon.Controls
{
    partial class KillReportAttacker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KillReportAttacker));
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DamageDoneLabel = new System.Windows.Forms.Label();
            this.CharacterPictureBox = new System.Windows.Forms.PictureBox();
            this.ShipPictureBox = new System.Windows.Forms.PictureBox();
            this.WeaponPictureBox = new System.Windows.Forms.PictureBox();
            this.AttackerInfoPanel = new System.Windows.Forms.Panel();
            this.CharacterNameLabel = new System.Windows.Forms.Label();
            this.AllianceNameLabel = new System.Windows.Forms.Label();
            this.CorpNameLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CharacterPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeaponPictureBox)).BeginInit();
            this.AttackerInfoPanel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.AutoSize = true;
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.DamageDoneLabel, 2, 1);
            this.MainTableLayoutPanel.Controls.Add(this.CharacterPictureBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.ShipPictureBox, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.WeaponPictureBox, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(this.AttackerInfoPanel, 2, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 2;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(214, 71);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // DamageDoneLabel
            // 
            this.DamageDoneLabel.AutoSize = true;
            this.DamageDoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.DamageDoneLabel.Location = new System.Drawing.Point(102, 53);
            this.DamageDoneLabel.Margin = new System.Windows.Forms.Padding(3, 18, 3, 0);
            this.DamageDoneLabel.Name = "DamageDoneLabel";
            this.DamageDoneLabel.Size = new System.Drawing.Size(72, 13);
            this.DamageDoneLabel.TabIndex = 0;
            this.DamageDoneLabel.Text = resources.GetString("this.DamageDoneLabel.Text");
            // 
            // CharacterPictureBox
            // 
            this.CharacterPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CharacterPictureBox.Image")));
            this.CharacterPictureBox.Location = new System.Drawing.Point(3, 3);
            this.CharacterPictureBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.CharacterPictureBox.Name = "CharacterPictureBox";
            this.MainTableLayoutPanel.SetRowSpan(this.CharacterPictureBox, 2);
            this.CharacterPictureBox.Size = new System.Drawing.Size(64, 64);
            this.CharacterPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CharacterPictureBox.TabIndex = 0;
            this.CharacterPictureBox.TabStop = false;
            // 
            // ShipPictureBox
            // 
            this.ShipPictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ShipPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ShipPictureBox.Image")));
            this.ShipPictureBox.Location = new System.Drawing.Point(67, 3);
            this.ShipPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.ShipPictureBox.Name = "ShipPictureBox";
            this.ShipPictureBox.Size = new System.Drawing.Size(32, 32);
            this.ShipPictureBox.TabIndex = 1;
            this.ShipPictureBox.TabStop = false;
            this.ShipPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.ShipPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // WeaponPictureBox
            // 
            this.WeaponPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.WeaponPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("WeaponPictureBox.Image")));
            this.WeaponPictureBox.Location = new System.Drawing.Point(67, 35);
            this.WeaponPictureBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.WeaponPictureBox.Name = "WeaponPictureBox";
            this.WeaponPictureBox.Size = new System.Drawing.Size(32, 32);
            this.WeaponPictureBox.TabIndex = 2;
            this.WeaponPictureBox.TabStop = false;
            this.WeaponPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.WeaponPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // AttackerInfoPanel
            // 
            this.AttackerInfoPanel.AutoSize = true;
            this.AttackerInfoPanel.Controls.Add(this.CharacterNameLabel);
            this.AttackerInfoPanel.Controls.Add(this.AllianceNameLabel);
            this.AttackerInfoPanel.Controls.Add(this.CorpNameLabel);
            this.AttackerInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttackerInfoPanel.Location = new System.Drawing.Point(99, 0);
            this.AttackerInfoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.AttackerInfoPanel.Name = "AttackerInfoPanel";
            this.AttackerInfoPanel.Size = new System.Drawing.Size(115, 35);
            this.AttackerInfoPanel.TabIndex = 5;
            // 
            // CharacterNameLabel
            // 
            this.CharacterNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CharacterNameLabel.AutoEllipsis = true;
            this.CharacterNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.CharacterNameLabel.Location = new System.Drawing.Point(3, 2);
            this.CharacterNameLabel.Name = "CharacterNameLabel";
            this.CharacterNameLabel.Size = new System.Drawing.Size(112, 11);
            this.CharacterNameLabel.TabIndex = 0;
            this.CharacterNameLabel.Text = resources.GetString("this.CharacterNameLabel.Text");
            // 
            // AllianceNameLabel
            // 
            this.AllianceNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AllianceNameLabel.AutoEllipsis = true;
            this.AllianceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.AllianceNameLabel.Location = new System.Drawing.Point(3, 24);
            this.AllianceNameLabel.Name = "AllianceNameLabel";
            this.AllianceNameLabel.Size = new System.Drawing.Size(112, 11);
            this.AllianceNameLabel.TabIndex = 2;
            this.AllianceNameLabel.Text = resources.GetString("this.AllianceNameLabel.Text");
            // 
            // CorpNameLabel
            // 
            this.CorpNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorpNameLabel.AutoEllipsis = true;
            this.CorpNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.CorpNameLabel.Location = new System.Drawing.Point(3, 13);
            this.CorpNameLabel.Name = "CorpNameLabel";
            this.CorpNameLabel.Size = new System.Drawing.Size(112, 11);
            this.CorpNameLabel.TabIndex = 1;
            this.CorpNameLabel.Text = resources.GetString("this.CorpNameLabel.Text");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInBrowserMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(171, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // showInBrowserMenuItem
            // 
            this.showInBrowserMenuItem.Name = "showInBrowserMenuItem";
            this.showInBrowserMenuItem.Size = new System.Drawing.Size(170, 22);
            this.showInBrowserMenuItem.Text = resources.GetString("this.showInBrowserMenuItem.Text");
            this.showInBrowserMenuItem.Click += new System.EventHandler(this.showInBrowserMenuItem_Click);
            // 
            // KillReportAttacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "KillReportAttacker";
            this.Size = new System.Drawing.Size(214, 71);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CharacterPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeaponPictureBox)).EndInit();
            this.AttackerInfoPanel.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.PictureBox CharacterPictureBox;
        private System.Windows.Forms.PictureBox ShipPictureBox;
        private System.Windows.Forms.PictureBox WeaponPictureBox;
        private System.Windows.Forms.Label CharacterNameLabel;
        private System.Windows.Forms.Label CorpNameLabel;
        private System.Windows.Forms.Label AllianceNameLabel;
        private System.Windows.Forms.Label DamageDoneLabel;
        private System.Windows.Forms.Panel AttackerInfoPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showInBrowserMenuItem;
    }
}
