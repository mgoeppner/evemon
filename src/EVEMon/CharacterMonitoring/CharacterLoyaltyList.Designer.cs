namespace EVEMon.CharacterMonitoring
{
    partial class CharacterLoyaltyList
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
            this.noLoyaltyLabel = new System.Windows.Forms.Label();
            this.lbLoyalty = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // noLoyaltyLabel
            // 
            this.noLoyaltyLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noLoyaltyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noLoyaltyLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noLoyaltyLabel.Location = new System.Drawing.Point(0, 0);
            this.noLoyaltyLabel.Name = "noLoyaltyLabel";
            this.noLoyaltyLabel.Size = new System.Drawing.Size(328, 372);
            this.noLoyaltyLabel.TabIndex = 2;
            this.noLoyaltyLabel.Text = "Loyalty point information not available.";
            this.noLoyaltyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbLoyalty
            // 
            this.lbLoyalty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLoyalty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLoyalty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbLoyalty.FormattingEnabled = true;
            this.lbLoyalty.IntegralHeight = false;
            this.lbLoyalty.ItemHeight = 15;
            this.lbLoyalty.Location = new System.Drawing.Point(0, 0);
            this.lbLoyalty.Margin = new System.Windows.Forms.Padding(0);
            this.lbLoyalty.Name = "lbLoyalty";
            this.lbLoyalty.Size = new System.Drawing.Size(328, 372);
            this.lbLoyalty.TabIndex = 4;
            this.lbLoyalty.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbLoyalty_DrawItem);
            this.lbLoyalty.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbLoyalty_MeasureItem);
            this.lbLoyalty.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbLoyalty_MouseDown);
            this.lbLoyalty.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbLoyalty_MouseWheel);
            // 
            // CharacterLoyaltyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noLoyaltyLabel);
            this.Controls.Add(this.lbLoyalty);
            this.Name = "CharacterLoyaltyList";
            this.Size = new System.Drawing.Size(328, 372);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noLoyaltyLabel;
        private System.Windows.Forms.ListBox lbLoyalty;
    }
}
