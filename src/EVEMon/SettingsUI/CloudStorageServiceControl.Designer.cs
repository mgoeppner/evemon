namespace EVEMon.SettingsUI
{
    partial class CloudStorageServiceControl
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
            this.btnRequestApply = new System.Windows.Forms.Button();
            this.lblAuthCode = new System.Windows.Forms.Label();
            this.txtBoxAuthCode = new System.Windows.Forms.TextBox();
            this.apiResponseLabel = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.createAccountLinkLabel = new System.Windows.Forms.LinkLabel();
            this.throbber = new EVEMon.Common.Controls.Throbber();
            ((System.ComponentModel.ISupportInitialize)(this.throbber)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRequestApply
            // 
            this.btnRequestApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRequestApply.Location = new System.Drawing.Point(147, 77);
            this.btnRequestApply.Name = "btnRequestApply";
            this.btnRequestApply.Size = new System.Drawing.Size(169, 27);
            this.btnRequestApply.TabIndex = 0;
            this.btnRequestApply.Text = "Request Authentication";
            this.btnRequestApply.UseVisualStyleBackColor = true;
            this.btnRequestApply.Click += new System.EventHandler(this.btnRequestApply_Click);
            // 
            // lblAuthCode
            // 
            this.lblAuthCode.AutoSize = true;
            this.lblAuthCode.Location = new System.Drawing.Point(18, 46);
            this.lblAuthCode.Name = "lblAuthCode";
            this.lblAuthCode.Size = new System.Drawing.Size(70, 15);
            this.lblAuthCode.TabIndex = 1;
            this.lblAuthCode.Text = "Auth Code:";
            // 
            // txtBoxAuthCode
            // 
            this.txtBoxAuthCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxAuthCode.Location = new System.Drawing.Point(93, 43);
            this.txtBoxAuthCode.Name = "txtBoxAuthCode";
            this.txtBoxAuthCode.Size = new System.Drawing.Size(350, 23);
            this.txtBoxAuthCode.TabIndex = 2;
            this.txtBoxAuthCode.UseSystemPasswordChar = true;
            // 
            // apiResponseLabel
            // 
            this.apiResponseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apiResponseLabel.Location = new System.Drawing.Point(0, 8);
            this.apiResponseLabel.Name = "apiResponseLabel";
            this.apiResponseLabel.Size = new System.Drawing.Size(463, 28);
            this.apiResponseLabel.TabIndex = 3;
            this.apiResponseLabel.Text = "API Response";
            this.apiResponseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(355, 77);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 27);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // createAccountLinkLabel
            // 
            this.createAccountLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createAccountLinkLabel.AutoSize = true;
            this.createAccountLinkLabel.Location = new System.Drawing.Point(18, 83);
            this.createAccountLinkLabel.Name = "createAccountLinkLabel";
            this.createAccountLinkLabel.Size = new System.Drawing.Size(52, 15);
            this.createAccountLinkLabel.TabIndex = 11;
            this.createAccountLinkLabel.TabStop = true;
            this.createAccountLinkLabel.Tag = "";
            this.createAccountLinkLabel.Text = "Sign Up";
            this.createAccountLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.getAnAccountLinkLabel_LinkClicked);
            // 
            // throbber
            // 
            this.throbber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.throbber.Location = new System.Drawing.Point(222, 8);
            this.throbber.MaximumSize = new System.Drawing.Size(24, 24);
            this.throbber.MinimumSize = new System.Drawing.Size(24, 24);
            this.throbber.Name = "throbber";
            this.throbber.Size = new System.Drawing.Size(24, 24);
            this.throbber.State = EVEMon.Common.Enumerations.ThrobberState.Stopped;
            this.throbber.TabIndex = 10;
            this.throbber.TabStop = false;
            // 
            // CloudStorageServiceControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.createAccountLinkLabel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.apiResponseLabel);
            this.Controls.Add(this.txtBoxAuthCode);
            this.Controls.Add(this.lblAuthCode);
            this.Controls.Add(this.btnRequestApply);
            this.Controls.Add(this.throbber);
            this.Name = "CloudStorageServiceControl";
            this.Size = new System.Drawing.Size(467, 115);
            this.Load += new System.EventHandler(this.CloudStorageServiceControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.throbber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRequestApply;
        private System.Windows.Forms.Label lblAuthCode;
        private System.Windows.Forms.TextBox txtBoxAuthCode;
        private System.Windows.Forms.Label apiResponseLabel;
        private System.Windows.Forms.Button btnReset;
        private Common.Controls.Throbber throbber;
        private System.Windows.Forms.LinkLabel createAccountLinkLabel;
    }
}
