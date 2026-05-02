namespace EVEMon.Controls
{
    partial class KillReportFittingContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KillReportFittingContent));
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.ToggleColorKeyPictureBox = new System.Windows.Forms.PictureBox();
            this.SaveFittingButton = new System.Windows.Forms.Button();
            this.FittingContentLabel = new System.Windows.Forms.Label();
            this.FooterPanel = new System.Windows.Forms.Panel();
            this.ItemsCostLabel = new System.Windows.Forms.Label();
            this.EstimatedTotalLossLabel = new System.Windows.Forms.Label();
            this.BorderPanel = new EVEMon.Common.Controls.BorderPanel();
            this.noItemsLabel = new System.Windows.Forms.Label();
            this.FittingContentListBox = new EVEMon.Common.Controls.NoFlickerListBox();
            this.ColorKeyPanel = new System.Windows.Forms.Panel();
            this.ColorKeyGroupBox = new System.Windows.Forms.GroupBox();
            this.ColorKeyGroupBoxPanel = new System.Windows.Forms.Panel();
            this.DroppedItemLabel = new System.Windows.Forms.Label();
            this.DestroyedItemLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTableLayoutPanel.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleColorKeyPictureBox)).BeginInit();
            this.FooterPanel.SuspendLayout();
            this.BorderPanel.SuspendLayout();
            this.ColorKeyPanel.SuspendLayout();
            this.ColorKeyGroupBox.SuspendLayout();
            this.ColorKeyGroupBoxPanel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.AutoSize = true;
            this.MainTableLayoutPanel.ColumnCount = 1;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.Controls.Add(this.HeaderPanel, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.FooterPanel, 0, 3);
            this.MainTableLayoutPanel.Controls.Add(this.BorderPanel, 0, 2);
            this.MainTableLayoutPanel.Controls.Add(this.ColorKeyPanel, 0, 1);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 4;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(338, 530);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.AutoSize = true;
            this.HeaderPanel.Controls.Add(this.ToggleColorKeyPictureBox);
            this.HeaderPanel.Controls.Add(this.SaveFittingButton);
            this.HeaderPanel.Controls.Add(this.FittingContentLabel);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(338, 23);
            this.HeaderPanel.TabIndex = 1;
            // 
            // ToggleColorKeyPictureBox
            // 
            this.ToggleColorKeyPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ToggleColorKeyPictureBox.Image")));
            this.ToggleColorKeyPictureBox.Location = new System.Drawing.Point(123, 3);
            this.ToggleColorKeyPictureBox.Name = "ToggleColorKeyPictureBox";
            this.ToggleColorKeyPictureBox.Size = new System.Drawing.Size(16, 16);
            this.ToggleColorKeyPictureBox.TabIndex = 2;
            this.ToggleColorKeyPictureBox.TabStop = false;
            this.toolTip.SetToolTip(this.ToggleColorKeyPictureBox, resources.GetString("this.ToggleColorKeyPictureBox.ToolTip"));
            this.ToggleColorKeyPictureBox.Click += new System.EventHandler(this.ToggleColorKeyPictureBox_Click);
            // 
            // SaveFittingButton
            // 
            this.SaveFittingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFittingButton.Location = new System.Drawing.Point(263, 0);
            this.SaveFittingButton.Margin = new System.Windows.Forms.Padding(0);
            this.SaveFittingButton.Name = "SaveFittingButton";
            this.SaveFittingButton.Size = new System.Drawing.Size(75, 23);
            this.SaveFittingButton.TabIndex = 1;
            this.SaveFittingButton.Text = resources.GetString("this.SaveFittingButton.Text");
            this.SaveFittingButton.UseVisualStyleBackColor = true;
            // 
            // FittingContentLabel
            // 
            this.FittingContentLabel.AutoSize = true;
            this.FittingContentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FittingContentLabel.Location = new System.Drawing.Point(0, 3);
            this.FittingContentLabel.Name = "FittingContentLabel";
            this.FittingContentLabel.Size = new System.Drawing.Size(117, 16);
            this.FittingContentLabel.TabIndex = 0;
            this.FittingContentLabel.Text = resources.GetString("this.FittingContentLabel.Text");
            // 
            // FooterPanel
            // 
            this.FooterPanel.AutoSize = true;
            this.FooterPanel.Controls.Add(this.ItemsCostLabel);
            this.FooterPanel.Controls.Add(this.EstimatedTotalLossLabel);
            this.FooterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FooterPanel.Location = new System.Drawing.Point(0, 514);
            this.FooterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FooterPanel.Name = "FooterPanel";
            this.FooterPanel.Size = new System.Drawing.Size(338, 16);
            this.FooterPanel.TabIndex = 3;
            // 
            // ItemsCostLabel
            // 
            this.ItemsCostLabel.AutoSize = true;
            this.ItemsCostLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ItemsCostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.ItemsCostLabel.ForeColor = System.Drawing.Color.Red;
            this.ItemsCostLabel.Location = new System.Drawing.Point(275, 0);
            this.ItemsCostLabel.Name = "ItemsCostLabel";
            this.ItemsCostLabel.Size = new System.Drawing.Size(63, 16);
            this.ItemsCostLabel.TabIndex = 0;
            this.ItemsCostLabel.Text = resources.GetString("this.ItemsCostLabel.Text");
            // 
            // EstimatedTotalLossLabel
            // 
            this.EstimatedTotalLossLabel.AutoSize = true;
            this.EstimatedTotalLossLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.EstimatedTotalLossLabel.Location = new System.Drawing.Point(0, 0);
            this.EstimatedTotalLossLabel.Name = "EstimatedTotalLossLabel";
            this.EstimatedTotalLossLabel.Size = new System.Drawing.Size(99, 16);
            this.EstimatedTotalLossLabel.TabIndex = 2;
            this.EstimatedTotalLossLabel.Text = resources.GetString("this.EstimatedTotalLossLabel.Text");
            // 
            // BorderPanel
            // 
            this.BorderPanel.AutoSize = true;
            this.BorderPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderPanel.BackColor = System.Drawing.SystemColors.Window;
            this.BorderPanel.Controls.Add(this.noItemsLabel);
            this.BorderPanel.Controls.Add(this.FittingContentListBox);
            this.BorderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BorderPanel.Location = new System.Drawing.Point(0, 63);
            this.BorderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Padding = new System.Windows.Forms.Padding(2, 2, 1, 2);
            this.BorderPanel.Size = new System.Drawing.Size(338, 451);
            this.BorderPanel.TabIndex = 4;
            // 
            // noItemsLabel
            // 
            this.noItemsLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noItemsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noItemsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noItemsLabel.Location = new System.Drawing.Point(2, 2);
            this.noItemsLabel.Name = "noItemsLabel";
            this.noItemsLabel.Size = new System.Drawing.Size(335, 447);
            this.noItemsLabel.TabIndex = 5;
            this.noItemsLabel.Text = resources.GetString("this.noItemsLabel.Text");
            this.noItemsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FittingContentListBox
            // 
            this.FittingContentListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FittingContentListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FittingContentListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FittingContentListBox.FormattingEnabled = true;
            this.FittingContentListBox.Location = new System.Drawing.Point(2, 2);
            this.FittingContentListBox.Name = "FittingContentListBox";
            this.FittingContentListBox.Size = new System.Drawing.Size(335, 447);
            this.FittingContentListBox.TabIndex = 4;
            this.FittingContentListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FittingContentListBox_DrawItem);
            this.FittingContentListBox.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.FittingContentListBox_MeasureItem);
            this.FittingContentListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FittingContentListBox_MouseDown);
            this.FittingContentListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FittingContentListBox_MouseMove);
            this.FittingContentListBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FittingContentListBox_MouseWheel);
            this.FittingContentListBox.Resize += new System.EventHandler(this.FittingContentListBox_Resize);
            // 
            // ColorKeyPanel
            // 
            this.ColorKeyPanel.AutoSize = true;
            this.ColorKeyPanel.Controls.Add(this.ColorKeyGroupBox);
            this.ColorKeyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorKeyPanel.Location = new System.Drawing.Point(0, 23);
            this.ColorKeyPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.ColorKeyPanel.Name = "ColorKeyPanel";
            this.ColorKeyPanel.Size = new System.Drawing.Size(338, 38);
            this.ColorKeyPanel.TabIndex = 5;
            // 
            // ColorKeyGroupBox
            // 
            this.ColorKeyGroupBox.AutoSize = true;
            this.ColorKeyGroupBox.Controls.Add(this.ColorKeyGroupBoxPanel);
            this.ColorKeyGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorKeyGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ColorKeyGroupBox.Name = "ColorKeyGroupBox";
            this.ColorKeyGroupBox.Size = new System.Drawing.Size(338, 38);
            this.ColorKeyGroupBox.TabIndex = 2;
            this.ColorKeyGroupBox.TabStop = false;
            this.ColorKeyGroupBox.Text = resources.GetString("this.ColorKeyGroupBox.Text");
            // 
            // ColorKeyGroupBoxPanel
            // 
            this.ColorKeyGroupBoxPanel.AutoSize = true;
            this.ColorKeyGroupBoxPanel.Controls.Add(this.DroppedItemLabel);
            this.ColorKeyGroupBoxPanel.Controls.Add(this.DestroyedItemLabel);
            this.ColorKeyGroupBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorKeyGroupBoxPanel.Location = new System.Drawing.Point(3, 16);
            this.ColorKeyGroupBoxPanel.Name = "ColorKeyGroupBoxPanel";
            this.ColorKeyGroupBoxPanel.Size = new System.Drawing.Size(332, 19);
            this.ColorKeyGroupBoxPanel.TabIndex = 2;
            // 
            // DroppedItemLabel
            // 
            this.DroppedItemLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DroppedItemLabel.BackColor = System.Drawing.Color.Green;
            this.DroppedItemLabel.Location = new System.Drawing.Point(207, 0);
            this.DroppedItemLabel.Name = "DroppedItemLabel";
            this.DroppedItemLabel.Size = new System.Drawing.Size(71, 19);
            this.DroppedItemLabel.TabIndex = 0;
            this.DroppedItemLabel.Text = resources.GetString("this.DroppedItemLabel.Text");
            this.DroppedItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DestroyedItemLabel
            // 
            this.DestroyedItemLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DestroyedItemLabel.BackColor = System.Drawing.Color.LightGray;
            this.DestroyedItemLabel.Location = new System.Drawing.Point(42, 0);
            this.DestroyedItemLabel.Name = "DestroyedItemLabel";
            this.DestroyedItemLabel.Size = new System.Drawing.Size(80, 19);
            this.DestroyedItemLabel.TabIndex = 1;
            this.DestroyedItemLabel.Text = resources.GetString("this.DestroyedItemLabel.Text");
            this.DestroyedItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Hold.png");
            this.imageList.Images.SetKeyName(1, "Cargo.png");
            this.imageList.Images.SetKeyName(2, "HighSlot.png");
            this.imageList.Images.SetKeyName(3, "MediumSlot.png");
            this.imageList.Images.SetKeyName(4, "LowSlot.png");
            this.imageList.Images.SetKeyName(5, "RigSlot.png");
            this.imageList.Images.SetKeyName(6, "SubsystemSlot.png");
            this.imageList.Images.SetKeyName(7, "DroneBay.png");
            this.imageList.Images.SetKeyName(8, "Implant.png");
            this.imageList.Images.SetKeyName(9, "Booster.png");
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
            // KillReportFittingContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "KillReportFittingContent";
            this.Size = new System.Drawing.Size(338, 530);
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ToggleColorKeyPictureBox)).EndInit();
            this.FooterPanel.ResumeLayout(false);
            this.FooterPanel.PerformLayout();
            this.BorderPanel.ResumeLayout(false);
            this.ColorKeyPanel.ResumeLayout(false);
            this.ColorKeyPanel.PerformLayout();
            this.ColorKeyGroupBox.ResumeLayout(false);
            this.ColorKeyGroupBox.PerformLayout();
            this.ColorKeyGroupBoxPanel.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Button SaveFittingButton;
        private System.Windows.Forms.Label FittingContentLabel;
        private System.Windows.Forms.Panel FooterPanel;
        private System.Windows.Forms.Label ItemsCostLabel;
        private System.Windows.Forms.Label EstimatedTotalLossLabel;
        private Common.Controls.NoFlickerListBox FittingContentListBox;
        private System.Windows.Forms.Label noItemsLabel;
        private Common.Controls.BorderPanel BorderPanel;
        private System.Windows.Forms.Panel ColorKeyPanel;
        private System.Windows.Forms.PictureBox ToggleColorKeyPictureBox;
        private System.Windows.Forms.Label DestroyedItemLabel;
        private System.Windows.Forms.Label DroppedItemLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox ColorKeyGroupBox;
        private System.Windows.Forms.Panel ColorKeyGroupBoxPanel;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showInBrowserMenuItem;
    }
}
