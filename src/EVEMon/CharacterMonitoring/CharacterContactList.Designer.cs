namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterContactList
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
            this.noContactsLabel = new System.Windows.Forms.Label();
            this.lbContacts = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // noContactsLabel
            // 
            this.noContactsLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noContactsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noContactsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noContactsLabel.Location = new System.Drawing.Point(0, 0);
            this.noContactsLabel.Name = "noContactsLabel";
            this.noContactsLabel.Size = new System.Drawing.Size(328, 372);
            this.noContactsLabel.TabIndex = 3;
            this.noContactsLabel.Text = "Contacts information not available.";
            this.noContactsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbContacts
            // 
            this.lbContacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbContacts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbContacts.FormattingEnabled = true;
            this.lbContacts.IntegralHeight = false;
            this.lbContacts.ItemHeight = 15;
            this.lbContacts.Location = new System.Drawing.Point(0, 0);
            this.lbContacts.Margin = new System.Windows.Forms.Padding(0);
            this.lbContacts.Name = "lbContacts";
            this.lbContacts.Size = new System.Drawing.Size(328, 372);
            this.lbContacts.TabIndex = 4;
            this.lbContacts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbContacts_DrawItem);
            this.lbContacts.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbContacts_MeasureItem);
            this.lbContacts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbContacts_MouseDown);
            this.lbContacts.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbContacts_MouseWheel);
            // 
            // CharacterContactList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noContactsLabel);
            this.Controls.Add(this.lbContacts);
            this.Name = "CharacterContactList";
            this.Size = new System.Drawing.Size(328, 372);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noContactsLabel;
        private System.Windows.Forms.ListBox lbContacts;
    }
}
