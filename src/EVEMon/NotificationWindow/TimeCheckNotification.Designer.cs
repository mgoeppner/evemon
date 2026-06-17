namespace EVEMon.NotificationWindow
{
    partial class TimeCheckNotification
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeCheckNotification));
            this.uxButtonPanel = new System.Windows.Forms.Panel();
            this.uxCloseButton = new System.Windows.Forms.Button();
            this.uxTitlePanel = new System.Windows.Forms.Panel();
            this.uxWarningIconPictureBox = new System.Windows.Forms.PictureBox();
            this.uxTitleLabel = new System.Windows.Forms.Label();
            this.uxContentPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.uxTimeZoneLabel = new System.Windows.Forms.Label();
            this.uxExpectedTimeLabel = new System.Windows.Forms.Label();
            this.uxActualTimeLabel = new System.Windows.Forms.Label();
            this.uxTimeZoneField = new System.Windows.Forms.Label();
            this.uxExpectedTimeField = new System.Windows.Forms.Label();
            this.uxActualTimeField = new System.Windows.Forms.Label();
            this.uxMessageLabel1 = new System.Windows.Forms.Label();
            this.uxCheckTimeOnStartUpCheckBox = new System.Windows.Forms.CheckBox();
            this.uxMessageLabel = new System.Windows.Forms.Label();
            this.uxButtonPanel.SuspendLayout();
            this.uxTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxWarningIconPictureBox)).BeginInit();
            this.uxContentPanel.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxButtonPanel
            // 
            this.uxButtonPanel.Controls.Add(this.uxCloseButton);
            this.uxButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxButtonPanel.Location = new System.Drawing.Point(0, 245);
            this.uxButtonPanel.Name = "uxButtonPanel";
            this.uxButtonPanel.Size = new System.Drawing.Size(384, 47);
            this.uxButtonPanel.TabIndex = 0;
            // 
            // uxCloseButton
            // 
            this.uxCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxCloseButton.Location = new System.Drawing.Point(297, 12);
            this.uxCloseButton.Name = "uxCloseButton";
            this.uxCloseButton.Size = new System.Drawing.Size(75, 23);
            this.uxCloseButton.TabIndex = 0;
            this.uxCloseButton.Text = resources.GetString("this.uxCloseButton.Text");
            this.uxCloseButton.UseVisualStyleBackColor = true;
            this.uxCloseButton.Click += new System.EventHandler(this.uxCloseButton_Click);
            // 
            // uxTitlePanel
            // 
            this.uxTitlePanel.BackColor = System.Drawing.SystemColors.Window;
            this.uxTitlePanel.Controls.Add(this.uxWarningIconPictureBox);
            this.uxTitlePanel.Controls.Add(this.uxTitleLabel);
            this.uxTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.uxTitlePanel.Name = "uxTitlePanel";
            this.uxTitlePanel.Size = new System.Drawing.Size(384, 54);
            this.uxTitlePanel.TabIndex = 2;
            // 
            // uxWarningIconPictureBox
            // 
            this.uxWarningIconPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("uxWarningIconPictureBox.Image")));
            this.uxWarningIconPictureBox.Location = new System.Drawing.Point(12, 12);
            this.uxWarningIconPictureBox.Name = "uxWarningIconPictureBox";
            this.uxWarningIconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.uxWarningIconPictureBox.TabIndex = 2;
            this.uxWarningIconPictureBox.TabStop = false;
            // 
            // uxTitleLabel
            // 
            this.uxTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxTitleLabel.Location = new System.Drawing.Point(50, 12);
            this.uxTitleLabel.Name = "uxTitleLabel";
            this.uxTitleLabel.Size = new System.Drawing.Size(322, 32);
            this.uxTitleLabel.TabIndex = 1;
            this.uxTitleLabel.Text = resources.GetString("this.uxTitleLabel.Text");
            this.uxTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxContentPanel
            // 
            this.uxContentPanel.BackColor = System.Drawing.SystemColors.Window;
            this.uxContentPanel.Controls.Add(this.tableLayoutPanel);
            this.uxContentPanel.Controls.Add(this.uxMessageLabel1);
            this.uxContentPanel.Controls.Add(this.uxCheckTimeOnStartUpCheckBox);
            this.uxContentPanel.Controls.Add(this.uxMessageLabel);
            this.uxContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxContentPanel.Location = new System.Drawing.Point(0, 54);
            this.uxContentPanel.Name = "uxContentPanel";
            this.uxContentPanel.Size = new System.Drawing.Size(384, 191);
            this.uxContentPanel.TabIndex = 3;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.uxTimeZoneLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.uxExpectedTimeLabel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.uxActualTimeLabel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.uxTimeZoneField, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.uxExpectedTimeField, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.uxActualTimeField, 1, 2);
            this.tableLayoutPanel.Location = new System.Drawing.Point(30, 60);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(325, 58);
            this.tableLayoutPanel.TabIndex = 6;
            // 
            // uxTimeZoneLabel
            // 
            this.uxTimeZoneLabel.AutoSize = true;
            this.uxTimeZoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxTimeZoneLabel.Location = new System.Drawing.Point(3, 0);
            this.uxTimeZoneLabel.Name = "uxTimeZoneLabel";
            this.uxTimeZoneLabel.Size = new System.Drawing.Size(91, 19);
            this.uxTimeZoneLabel.TabIndex = 3;
            this.uxTimeZoneLabel.Text = resources.GetString("this.uxTimeZoneLabel.Text");
            this.uxTimeZoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxExpectedTimeLabel
            // 
            this.uxExpectedTimeLabel.AutoSize = true;
            this.uxExpectedTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxExpectedTimeLabel.Location = new System.Drawing.Point(3, 19);
            this.uxExpectedTimeLabel.Name = "uxExpectedTimeLabel";
            this.uxExpectedTimeLabel.Size = new System.Drawing.Size(91, 19);
            this.uxExpectedTimeLabel.TabIndex = 4;
            this.uxExpectedTimeLabel.Text = resources.GetString("this.uxExpectedTimeLabel.Text");
            this.uxExpectedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxActualTimeLabel
            // 
            this.uxActualTimeLabel.AutoSize = true;
            this.uxActualTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxActualTimeLabel.Location = new System.Drawing.Point(3, 38);
            this.uxActualTimeLabel.Name = "uxActualTimeLabel";
            this.uxActualTimeLabel.Size = new System.Drawing.Size(91, 20);
            this.uxActualTimeLabel.TabIndex = 5;
            this.uxActualTimeLabel.Text = resources.GetString("this.uxActualTimeLabel.Text");
            this.uxActualTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uxTimeZoneField
            // 
            this.uxTimeZoneField.AutoSize = true;
            this.uxTimeZoneField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxTimeZoneField.Location = new System.Drawing.Point(100, 0);
            this.uxTimeZoneField.Name = "uxTimeZoneField";
            this.uxTimeZoneField.Size = new System.Drawing.Size(222, 19);
            this.uxTimeZoneField.TabIndex = 6;
            this.uxTimeZoneField.Text = resources.GetString("this.uxTimeZoneField.Text");
            this.uxTimeZoneField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxExpectedTimeField
            // 
            this.uxExpectedTimeField.AutoSize = true;
            this.uxExpectedTimeField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxExpectedTimeField.Location = new System.Drawing.Point(100, 19);
            this.uxExpectedTimeField.Name = "uxExpectedTimeField";
            this.uxExpectedTimeField.Size = new System.Drawing.Size(222, 19);
            this.uxExpectedTimeField.TabIndex = 7;
            this.uxExpectedTimeField.Text = resources.GetString("this.uxExpectedTimeField.Text");
            this.uxExpectedTimeField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxActualTimeField
            // 
            this.uxActualTimeField.AutoSize = true;
            this.uxActualTimeField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxActualTimeField.Location = new System.Drawing.Point(100, 38);
            this.uxActualTimeField.Name = "uxActualTimeField";
            this.uxActualTimeField.Size = new System.Drawing.Size(222, 20);
            this.uxActualTimeField.TabIndex = 8;
            this.uxActualTimeField.Text = resources.GetString("this.uxActualTimeField.Text");
            this.uxActualTimeField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxMessageLabel1
            // 
            this.uxMessageLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxMessageLabel1.Location = new System.Drawing.Point(9, 3);
            this.uxMessageLabel1.Name = "uxMessageLabel1";
            this.uxMessageLabel1.Size = new System.Drawing.Size(363, 54);
            this.uxMessageLabel1.TabIndex = 2;
            this.uxMessageLabel1.Text = resources.GetString("uxMessageLabel1.Text");
            // 
            // uxCheckTimeOnStartUpCheckBox
            // 
            this.uxCheckTimeOnStartUpCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uxCheckTimeOnStartUpCheckBox.AutoSize = true;
            this.uxCheckTimeOnStartUpCheckBox.Location = new System.Drawing.Point(12, 165);
            this.uxCheckTimeOnStartUpCheckBox.Name = "uxCheckTimeOnStartUpCheckBox";
            this.uxCheckTimeOnStartUpCheckBox.Size = new System.Drawing.Size(246, 17);
            this.uxCheckTimeOnStartUpCheckBox.TabIndex = 1;
            this.uxCheckTimeOnStartUpCheckBox.Text = resources.GetString("this.uxCheckTimeOnStartUpCheckBox.Text");
            this.uxCheckTimeOnStartUpCheckBox.UseVisualStyleBackColor = true;
            // 
            // uxMessageLabel
            // 
            this.uxMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxMessageLabel.Location = new System.Drawing.Point(9, 120);
            this.uxMessageLabel.Name = "uxMessageLabel";
            this.uxMessageLabel.Size = new System.Drawing.Size(360, 44);
            this.uxMessageLabel.TabIndex = 0;
            this.uxMessageLabel.Text = "Please check your system\'s time settings. If your clock is wrong, EVEMon may disp" +
    "lay unexpected Skill Point values for some skills. Skill completion notification" +
    "s may also be shown incorrectly.";
            // 
            // TimeCheckNotification
            // 
            this.AcceptButton = this.uxCloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(384, 292);
            this.Controls.Add(this.uxContentPanel);
            this.Controls.Add(this.uxTitlePanel);
            this.Controls.Add(this.uxButtonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimeCheckNotification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("this.Text");
            this.uxButtonPanel.ResumeLayout(false);
            this.uxTitlePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxWarningIconPictureBox)).EndInit();
            this.uxContentPanel.ResumeLayout(false);
            this.uxContentPanel.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel uxButtonPanel;
        private System.Windows.Forms.Button uxCloseButton;
        private System.Windows.Forms.Panel uxTitlePanel;
        private System.Windows.Forms.Label uxTitleLabel;
        private System.Windows.Forms.PictureBox uxWarningIconPictureBox;
        private System.Windows.Forms.Panel uxContentPanel;
        private System.Windows.Forms.Label uxMessageLabel;
        private System.Windows.Forms.CheckBox uxCheckTimeOnStartUpCheckBox;
        private System.Windows.Forms.Label uxActualTimeLabel;
        private System.Windows.Forms.Label uxExpectedTimeLabel;
        private System.Windows.Forms.Label uxTimeZoneLabel;
        private System.Windows.Forms.Label uxMessageLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label uxTimeZoneField;
        private System.Windows.Forms.Label uxExpectedTimeField;
        private System.Windows.Forms.Label uxActualTimeField;

    }
}
