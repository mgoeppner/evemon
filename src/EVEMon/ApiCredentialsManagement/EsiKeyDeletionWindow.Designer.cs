namespace EVEMon.ApiCredentialsManagement
{
    sealed partial class EsiKeyDeletionWindow
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("John Doe");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EsiKeyDeletionWindow));
            this.deletionLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.charactersListView = new System.Windows.Forms.ListView();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.deleteWarningLabel = new System.Windows.Forms.Label();
            this.charactersListGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.charactersListGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // deletionLabel
            // 
            this.deletionLabel.AutoSize = true;
            this.deletionLabel.Location = new System.Drawing.Point(51, 22);
            this.deletionLabel.Name = "deletionLabel";
            this.deletionLabel.Size = new System.Drawing.Size(238, 13);
            this.deletionLabel.TabIndex = 4;
            this.deletionLabel.Text = resources.GetString("this.deletionLabel.Text");
            this.deletionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(435, 180);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = resources.GetString("this.cancelButton.Text");
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.deleteButton.Location = new System.Drawing.Point(354, 180);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = resources.GetString("this.deleteButton.Text");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // charactersListView
            // 
            this.charactersListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.charactersListView.CheckBoxes = true;
            listViewItem1.StateImageIndex = 0;
            this.charactersListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.charactersListView.Location = new System.Drawing.Point(308, 28);
            this.charactersListView.Name = "charactersListView";
            this.charactersListView.Size = new System.Drawing.Size(181, 77);
            this.charactersListView.TabIndex = 2;
            this.charactersListView.UseCompatibleStateImageBehavior = false;
            this.charactersListView.View = System.Windows.Forms.View.List;
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(13, 13);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(32, 32);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // infoLabel
            // 
            this.infoLabel.Location = new System.Drawing.Point(6, 28);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(296, 48);
            this.infoLabel.TabIndex = 6;
            this.infoLabel.Text = "If you choose to leave your character, it will be displayed as cached and automat" +
    "ically reconnected once it is found on another one of your ESI keys.";
            // 
            // deleteWarningLabel
            // 
            this.deleteWarningLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteWarningLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.deleteWarningLabel.Location = new System.Drawing.Point(6, 90);
            this.deleteWarningLabel.Name = "deleteWarningLabel";
            this.deleteWarningLabel.Size = new System.Drawing.Size(304, 15);
            this.deleteWarningLabel.TabIndex = 7;
            this.deleteWarningLabel.Text = resources.GetString("this.deleteWarningLabel.Text");
            // 
            // charactersListGroupBox
            // 
            this.charactersListGroupBox.Controls.Add(this.deleteWarningLabel);
            this.charactersListGroupBox.Controls.Add(this.infoLabel);
            this.charactersListGroupBox.Controls.Add(this.charactersListView);
            this.charactersListGroupBox.Location = new System.Drawing.Point(15, 60);
            this.charactersListGroupBox.Name = "charactersListGroupBox";
            this.charactersListGroupBox.Size = new System.Drawing.Size(495, 114);
            this.charactersListGroupBox.TabIndex = 8;
            this.charactersListGroupBox.TabStop = false;
            this.charactersListGroupBox.Text = resources.GetString("this.charactersListGroupBox.Text");
            // 
            // EsiKeyDeletionWindow
            // 
            this.AcceptButton = this.cancelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(522, 215);
            this.Controls.Add(this.charactersListGroupBox);
            this.Controls.Add(this.deletionLabel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EsiKeyDeletionWindow";
            this.Text = resources.GetString("this.Text");
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.charactersListGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ListView charactersListView;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label deleteWarningLabel;
        private System.Windows.Forms.GroupBox charactersListGroupBox;
        private System.Windows.Forms.Label deletionLabel;
    }
}