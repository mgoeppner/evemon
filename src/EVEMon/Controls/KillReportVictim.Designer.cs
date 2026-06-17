namespace EVEMon.Controls
{
    partial class KillReportVictim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KillReportVictim));
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CharacterPictureBox = new System.Windows.Forms.PictureBox();
            this.ShipPictureBox = new System.Windows.Forms.PictureBox();
            this.VictimInfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CorpPictureBox = new System.Windows.Forms.PictureBox();
            this.AlliancePictureBox = new System.Windows.Forms.PictureBox();
            this.KillTimeLabel = new System.Windows.Forms.Label();
            this.SolarSystemFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SolarSystemLabel = new System.Windows.Forms.Label();
            this.SecStatusLabel = new System.Windows.Forms.Label();
            this.Separator1Label = new System.Windows.Forms.Label();
            this.ConstelationLabel = new System.Windows.Forms.Label();
            this.Separator2Label = new System.Windows.Forms.Label();
            this.RegionLabel = new System.Windows.Forms.Label();
            this.CorpAllianceFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.CorpNameLabel = new System.Windows.Forms.Label();
            this.AllianceNameLabel = new System.Windows.Forms.Label();
            this.ShipNameGroupFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ShipNameLabel = new System.Windows.Forms.Label();
            this.ShipGroupLabel = new System.Windows.Forms.Label();
            this.CharacterNamePanel = new System.Windows.Forms.Panel();
            this.CopyPictureBox = new System.Windows.Forms.PictureBox();
            this.CharacterNameLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInShipBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CharacterPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipPictureBox)).BeginInit();
            this.VictimInfoTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CorpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlliancePictureBox)).BeginInit();
            this.SolarSystemFlowLayoutPanel.SuspendLayout();
            this.CorpAllianceFlowLayoutPanel.SuspendLayout();
            this.ShipNameGroupFlowLayoutPanel.SuspendLayout();
            this.CharacterNamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CopyPictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.AutoSize = true;
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.Controls.Add(this.CharacterPictureBox, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.ShipPictureBox, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.VictimInfoTableLayoutPanel, 2, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 1;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(538, 134);
            this.MainTableLayoutPanel.TabIndex = 1;
            // 
            // CharacterPictureBox
            // 
            this.CharacterPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CharacterPictureBox.Image")));
            this.CharacterPictureBox.Location = new System.Drawing.Point(3, 3);
            this.CharacterPictureBox.Name = "CharacterPictureBox";
            this.CharacterPictureBox.Size = new System.Drawing.Size(128, 128);
            this.CharacterPictureBox.TabIndex = 0;
            this.CharacterPictureBox.TabStop = false;
            // 
            // ShipPictureBox
            // 
            this.ShipPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ShipPictureBox.Image")));
            this.ShipPictureBox.Location = new System.Drawing.Point(137, 3);
            this.ShipPictureBox.Name = "ShipPictureBox";
            this.ShipPictureBox.Size = new System.Drawing.Size(128, 128);
            this.ShipPictureBox.TabIndex = 1;
            this.ShipPictureBox.TabStop = false;
            this.ShipPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShipPictureBox_MouseDown);
            this.ShipPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ShipPictureBox_MouseMove);
            // 
            // VictimInfoTableLayoutPanel
            // 
            this.VictimInfoTableLayoutPanel.ColumnCount = 3;
            this.VictimInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.VictimInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.VictimInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.VictimInfoTableLayoutPanel.Controls.Add(this.CorpPictureBox, 0, 1);
            this.VictimInfoTableLayoutPanel.Controls.Add(this.AlliancePictureBox, 1, 1);
            this.VictimInfoTableLayoutPanel.Controls.Add(this.KillTimeLabel, 0, 3);
            this.VictimInfoTableLayoutPanel.Controls.Add(this.SolarSystemFlowLayoutPanel, 0, 4);
            this.VictimInfoTableLayoutPanel.Controls.Add(this.CorpAllianceFlowLayoutPanel, 2, 1);
            this.VictimInfoTableLayoutPanel.Controls.Add(this.ShipNameGroupFlowLayoutPanel, 0, 2);
            this.VictimInfoTableLayoutPanel.Controls.Add(this.CharacterNamePanel, 0, 0);
            this.VictimInfoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VictimInfoTableLayoutPanel.Location = new System.Drawing.Point(268, 0);
            this.VictimInfoTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.VictimInfoTableLayoutPanel.Name = "VictimInfoTableLayoutPanel";
            this.VictimInfoTableLayoutPanel.RowCount = 5;
            this.VictimInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VictimInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VictimInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VictimInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VictimInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VictimInfoTableLayoutPanel.Size = new System.Drawing.Size(270, 134);
            this.VictimInfoTableLayoutPanel.TabIndex = 2;
            // 
            // CorpPictureBox
            // 
            this.CorpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CorpPictureBox.Image")));
            this.CorpPictureBox.Location = new System.Drawing.Point(3, 33);
            this.CorpPictureBox.Name = "CorpPictureBox";
            this.CorpPictureBox.Size = new System.Drawing.Size(32, 32);
            this.CorpPictureBox.TabIndex = 1;
            this.CorpPictureBox.TabStop = false;
            // 
            // AlliancePictureBox
            // 
            this.AlliancePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("AlliancePictureBox.Image")));
            this.AlliancePictureBox.Location = new System.Drawing.Point(41, 33);
            this.AlliancePictureBox.Name = "AlliancePictureBox";
            this.AlliancePictureBox.Size = new System.Drawing.Size(32, 32);
            this.AlliancePictureBox.TabIndex = 2;
            this.AlliancePictureBox.TabStop = false;
            // 
            // KillTimeLabel
            // 
            this.KillTimeLabel.AutoSize = true;
            this.VictimInfoTableLayoutPanel.SetColumnSpan(this.KillTimeLabel, 3);
            this.KillTimeLabel.Location = new System.Drawing.Point(0, 98);
            this.KillTimeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.KillTimeLabel.Name = "KillTimeLabel";
            this.KillTimeLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.KillTimeLabel.Size = new System.Drawing.Size(46, 16);
            this.KillTimeLabel.TabIndex = 5;
            this.KillTimeLabel.Text = resources.GetString("this.KillTimeLabel.Text");
            // 
            // SolarSystemFlowLayoutPanel
            // 
            this.VictimInfoTableLayoutPanel.SetColumnSpan(this.SolarSystemFlowLayoutPanel, 3);
            this.SolarSystemFlowLayoutPanel.Controls.Add(this.SolarSystemLabel);
            this.SolarSystemFlowLayoutPanel.Controls.Add(this.SecStatusLabel);
            this.SolarSystemFlowLayoutPanel.Controls.Add(this.Separator1Label);
            this.SolarSystemFlowLayoutPanel.Controls.Add(this.ConstelationLabel);
            this.SolarSystemFlowLayoutPanel.Controls.Add(this.Separator2Label);
            this.SolarSystemFlowLayoutPanel.Controls.Add(this.RegionLabel);
            this.SolarSystemFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SolarSystemFlowLayoutPanel.Location = new System.Drawing.Point(0, 114);
            this.SolarSystemFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SolarSystemFlowLayoutPanel.Name = "SolarSystemFlowLayoutPanel";
            this.SolarSystemFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.SolarSystemFlowLayoutPanel.Size = new System.Drawing.Size(270, 20);
            this.SolarSystemFlowLayoutPanel.TabIndex = 6;
            // 
            // SolarSystemLabel
            // 
            this.SolarSystemLabel.AutoSize = true;
            this.SolarSystemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.SolarSystemLabel.Location = new System.Drawing.Point(0, 3);
            this.SolarSystemLabel.Margin = new System.Windows.Forms.Padding(0);
            this.SolarSystemLabel.Name = "SolarSystemLabel";
            this.SolarSystemLabel.Size = new System.Drawing.Size(80, 13);
            this.SolarSystemLabel.TabIndex = 0;
            this.SolarSystemLabel.Text = resources.GetString("this.SolarSystemLabel.Text");
            // 
            // SecStatusLabel
            // 
            this.SecStatusLabel.AutoSize = true;
            this.SecStatusLabel.Location = new System.Drawing.Point(80, 3);
            this.SecStatusLabel.Margin = new System.Windows.Forms.Padding(0);
            this.SecStatusLabel.Name = "SecStatusLabel";
            this.SecStatusLabel.Size = new System.Drawing.Size(22, 13);
            this.SecStatusLabel.TabIndex = 1;
            this.SecStatusLabel.Text = "0.0";
            // 
            // Separator1Label
            // 
            this.Separator1Label.AutoSize = true;
            this.Separator1Label.Location = new System.Drawing.Point(102, 3);
            this.Separator1Label.Margin = new System.Windows.Forms.Padding(0);
            this.Separator1Label.Name = "Separator1Label";
            this.Separator1Label.Size = new System.Drawing.Size(13, 13);
            this.Separator1Label.TabIndex = 3;
            this.Separator1Label.Text = "<";
            // 
            // ConstelationLabel
            // 
            this.ConstelationLabel.AutoSize = true;
            this.ConstelationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ConstelationLabel.Location = new System.Drawing.Point(115, 3);
            this.ConstelationLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ConstelationLabel.Name = "ConstelationLabel";
            this.ConstelationLabel.Size = new System.Drawing.Size(77, 13);
            this.ConstelationLabel.TabIndex = 2;
            this.ConstelationLabel.Text = resources.GetString("this.ConstelationLabel.Text");
            // 
            // Separator2Label
            // 
            this.Separator2Label.AutoSize = true;
            this.Separator2Label.Location = new System.Drawing.Point(192, 3);
            this.Separator2Label.Margin = new System.Windows.Forms.Padding(0);
            this.Separator2Label.Name = "Separator2Label";
            this.Separator2Label.Size = new System.Drawing.Size(13, 13);
            this.Separator2Label.TabIndex = 4;
            this.Separator2Label.Text = "<";
            // 
            // RegionLabel
            // 
            this.RegionLabel.AutoSize = true;
            this.RegionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.RegionLabel.Location = new System.Drawing.Point(205, 3);
            this.RegionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.RegionLabel.Name = "RegionLabel";
            this.RegionLabel.Size = new System.Drawing.Size(47, 13);
            this.RegionLabel.TabIndex = 5;
            this.RegionLabel.Text = resources.GetString("this.RegionLabel.Text");
            // 
            // CorpAllianceFlowLayoutPanel
            // 
            this.CorpAllianceFlowLayoutPanel.Controls.Add(this.CorpNameLabel);
            this.CorpAllianceFlowLayoutPanel.Controls.Add(this.AllianceNameLabel);
            this.CorpAllianceFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CorpAllianceFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.CorpAllianceFlowLayoutPanel.Location = new System.Drawing.Point(76, 30);
            this.CorpAllianceFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CorpAllianceFlowLayoutPanel.Name = "CorpAllianceFlowLayoutPanel";
            this.CorpAllianceFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.CorpAllianceFlowLayoutPanel.Size = new System.Drawing.Size(194, 38);
            this.CorpAllianceFlowLayoutPanel.TabIndex = 7;
            // 
            // CorpNameLabel
            // 
            this.CorpNameLabel.AutoSize = true;
            this.CorpNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.CorpNameLabel.Location = new System.Drawing.Point(3, 6);
            this.CorpNameLabel.Name = "CorpNameLabel";
            this.CorpNameLabel.Size = new System.Drawing.Size(108, 13);
            this.CorpNameLabel.TabIndex = 4;
            this.CorpNameLabel.Text = resources.GetString("this.CorpNameLabel.Text");
            // 
            // AllianceNameLabel
            // 
            this.AllianceNameLabel.AutoSize = true;
            this.AllianceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.AllianceNameLabel.Location = new System.Drawing.Point(3, 19);
            this.AllianceNameLabel.Name = "AllianceNameLabel";
            this.AllianceNameLabel.Size = new System.Drawing.Size(88, 13);
            this.AllianceNameLabel.TabIndex = 5;
            this.AllianceNameLabel.Text = resources.GetString("this.AllianceNameLabel.Text");
            // 
            // ShipNameGroupFlowLayoutPanel
            // 
            this.VictimInfoTableLayoutPanel.SetColumnSpan(this.ShipNameGroupFlowLayoutPanel, 3);
            this.ShipNameGroupFlowLayoutPanel.Controls.Add(this.ShipNameLabel);
            this.ShipNameGroupFlowLayoutPanel.Controls.Add(this.ShipGroupLabel);
            this.ShipNameGroupFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShipNameGroupFlowLayoutPanel.Location = new System.Drawing.Point(0, 68);
            this.ShipNameGroupFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ShipNameGroupFlowLayoutPanel.Name = "ShipNameGroupFlowLayoutPanel";
            this.ShipNameGroupFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 6);
            this.ShipNameGroupFlowLayoutPanel.Size = new System.Drawing.Size(270, 30);
            this.ShipNameGroupFlowLayoutPanel.TabIndex = 8;
            // 
            // ShipNameLabel
            // 
            this.ShipNameLabel.AutoSize = true;
            this.ShipNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ShipNameLabel.Location = new System.Drawing.Point(0, 8);
            this.ShipNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ShipNameLabel.Name = "ShipNameLabel";
            this.ShipNameLabel.Size = new System.Drawing.Size(68, 13);
            this.ShipNameLabel.TabIndex = 3;
            this.ShipNameLabel.Text = resources.GetString("this.ShipNameLabel.Text");
            // 
            // ShipGroupLabel
            // 
            this.ShipGroupLabel.AutoSize = true;
            this.ShipGroupLabel.Location = new System.Drawing.Point(71, 8);
            this.ShipGroupLabel.Name = "ShipGroupLabel";
            this.ShipGroupLabel.Size = new System.Drawing.Size(27, 13);
            this.ShipGroupLabel.TabIndex = 4;
            this.ShipGroupLabel.Text = "({0})";
            // 
            // CharacterNamePanel
            // 
            this.VictimInfoTableLayoutPanel.SetColumnSpan(this.CharacterNamePanel, 3);
            this.CharacterNamePanel.Controls.Add(this.CopyPictureBox);
            this.CharacterNamePanel.Controls.Add(this.CharacterNameLabel);
            this.CharacterNamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CharacterNamePanel.Location = new System.Drawing.Point(0, 0);
            this.CharacterNamePanel.Margin = new System.Windows.Forms.Padding(0);
            this.CharacterNamePanel.Name = "CharacterNamePanel";
            this.CharacterNamePanel.Size = new System.Drawing.Size(270, 30);
            this.CharacterNamePanel.TabIndex = 9;
            // 
            // CopyPictureBox
            // 
            this.CopyPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CopyPictureBox.Image")));
            this.CopyPictureBox.Location = new System.Drawing.Point(251, 3);
            this.CopyPictureBox.Name = "CopyPictureBox";
            this.CopyPictureBox.Size = new System.Drawing.Size(16, 16);
            this.CopyPictureBox.TabIndex = 1;
            this.CopyPictureBox.TabStop = false;
            this.toolTip.SetToolTip(this.CopyPictureBox, resources.GetString("this.CopyPictureBox.ToolTip"));
            this.CopyPictureBox.Click += new System.EventHandler(this.CopyPictureBox_Click);
            // 
            // CharacterNameLabel
            // 
            this.CharacterNameLabel.AutoSize = true;
            this.CharacterNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.CharacterNameLabel.Location = new System.Drawing.Point(-1, 3);
            this.CharacterNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.CharacterNameLabel.Name = "CharacterNameLabel";
            this.CharacterNameLabel.Size = new System.Drawing.Size(146, 20);
            this.CharacterNameLabel.TabIndex = 0;
            this.CharacterNameLabel.Text = resources.GetString("this.CharacterNameLabel.Text");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInShipBrowserMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(197, 48);
            // 
            // showInShipBrowserMenuItem
            // 
            this.showInShipBrowserMenuItem.Name = "showInShipBrowserMenuItem";
            this.showInShipBrowserMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showInShipBrowserMenuItem.Text = resources.GetString("this.showInShipBrowserMenuItem.Text");
            this.showInShipBrowserMenuItem.Click += new System.EventHandler(this.showInShipBrowserMenuItem_Click);
            // 
            // KillReportVictim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "KillReportVictim";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.Size = new System.Drawing.Size(538, 140);
            this.MainTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CharacterPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipPictureBox)).EndInit();
            this.VictimInfoTableLayoutPanel.ResumeLayout(false);
            this.VictimInfoTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CorpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlliancePictureBox)).EndInit();
            this.SolarSystemFlowLayoutPanel.ResumeLayout(false);
            this.SolarSystemFlowLayoutPanel.PerformLayout();
            this.CorpAllianceFlowLayoutPanel.ResumeLayout(false);
            this.CorpAllianceFlowLayoutPanel.PerformLayout();
            this.ShipNameGroupFlowLayoutPanel.ResumeLayout(false);
            this.ShipNameGroupFlowLayoutPanel.PerformLayout();
            this.CharacterNamePanel.ResumeLayout(false);
            this.CharacterNamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CopyPictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.PictureBox CharacterPictureBox;
        private System.Windows.Forms.TableLayoutPanel VictimInfoTableLayoutPanel;
        private System.Windows.Forms.Label CharacterNameLabel;
        private System.Windows.Forms.PictureBox CorpPictureBox;
        private System.Windows.Forms.PictureBox AlliancePictureBox;
        private System.Windows.Forms.Label ShipNameLabel;
        private System.Windows.Forms.Label CorpNameLabel;
        private System.Windows.Forms.Label KillTimeLabel;
        private System.Windows.Forms.Label SolarSystemLabel;
        private System.Windows.Forms.Label SecStatusLabel;
        private System.Windows.Forms.Label ConstelationLabel;
        private System.Windows.Forms.FlowLayoutPanel CorpAllianceFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel ShipNameGroupFlowLayoutPanel;
        private System.Windows.Forms.Label ShipGroupLabel;
        private System.Windows.Forms.Label AllianceNameLabel;
        private System.Windows.Forms.Label Separator1Label;
        private System.Windows.Forms.Label Separator2Label;
        private System.Windows.Forms.Label RegionLabel;
        private System.Windows.Forms.FlowLayoutPanel SolarSystemFlowLayoutPanel;
        private System.Windows.Forms.PictureBox ShipPictureBox;
        private System.Windows.Forms.Panel CharacterNamePanel;
        private System.Windows.Forms.PictureBox CopyPictureBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showInShipBrowserMenuItem;
    }
}
