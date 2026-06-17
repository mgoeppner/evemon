namespace EVEMon.SettingsUI
{
    partial class PortableEveClientsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortableEveClientsControl));
            this.EveInstallationPathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.DeletePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DeletePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EveInstallationPathTextBox
            // 
            this.EveInstallationPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EveInstallationPathTextBox.Location = new System.Drawing.Point(33, 5);
            this.EveInstallationPathTextBox.Name = "EveInstallationPathTextBox";
            this.EveInstallationPathTextBox.ReadOnly = true;
            this.EveInstallationPathTextBox.Size = new System.Drawing.Size(292, 20);
            this.EveInstallationPathTextBox.TabIndex = 0;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseButton.Location = new System.Drawing.Point(331, 3);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 1;
            this.BrowseButton.Text = resources.GetString("this.BrowseButton.Text");
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // DeletePictureBox
            // 
            this.DeletePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("DeletePictureBox.Image")));
            this.DeletePictureBox.Location = new System.Drawing.Point(11, 6);
            this.DeletePictureBox.Name = "DeletePictureBox";
            this.DeletePictureBox.Size = new System.Drawing.Size(16, 16);
            this.DeletePictureBox.TabIndex = 2;
            this.DeletePictureBox.TabStop = false;
            this.DeletePictureBox.Click += new System.EventHandler(this.DeletePictureBox_Click);
            // 
            // PortableEveClientsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EveInstallationPathTextBox);
            this.Controls.Add(this.DeletePictureBox);
            this.Controls.Add(this.BrowseButton);
            this.Name = "PortableEveClientsControl";
            this.Size = new System.Drawing.Size(417, 29);
            ((System.ComponentModel.ISupportInitialize)(this.DeletePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EveInstallationPathTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.PictureBox DeletePictureBox;

    }
}
