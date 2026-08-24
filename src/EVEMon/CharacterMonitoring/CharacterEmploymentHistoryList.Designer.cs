namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterEmploymentHistoryList
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
            this.noEmploymentHistoryLabel = new System.Windows.Forms.Label();
            this.lbEmploymentHistory = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // noEmploymentHistoryLabel
            // 
            this.noEmploymentHistoryLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noEmploymentHistoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noEmploymentHistoryLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noEmploymentHistoryLabel.Location = new System.Drawing.Point(0, 0);
            this.noEmploymentHistoryLabel.Name = "noEmploymentHistoryLabel";
            this.noEmploymentHistoryLabel.Size = new System.Drawing.Size(328, 372);
            this.noEmploymentHistoryLabel.TabIndex = 3;
            this.noEmploymentHistoryLabel.Text = "Employment History information not available.";
            this.noEmploymentHistoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbEmploymentHistory
            // 
            this.lbEmploymentHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbEmploymentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEmploymentHistory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbEmploymentHistory.FormattingEnabled = true;
            this.lbEmploymentHistory.IntegralHeight = false;
            this.lbEmploymentHistory.ItemHeight = 15;
            this.lbEmploymentHistory.Location = new System.Drawing.Point(0, 0);
            this.lbEmploymentHistory.Margin = new System.Windows.Forms.Padding(0);
            this.lbEmploymentHistory.Name = "lbEmploymentHistory";
            this.lbEmploymentHistory.Size = new System.Drawing.Size(328, 372);
            this.lbEmploymentHistory.TabIndex = 3;
            this.lbEmploymentHistory.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbEmploymentHistory_DrawItem);
            this.lbEmploymentHistory.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbEmploymentHistory_MeasureItem);
            this.lbEmploymentHistory.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbEmploymentHistory_MouseWheel);
            // 
            // CharacterEmploymentHistoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noEmploymentHistoryLabel);
            this.Controls.Add(this.lbEmploymentHistory);
            this.Name = "CharacterEmploymentHistoryList";
            this.Size = new System.Drawing.Size(328, 372);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noEmploymentHistoryLabel;
        private System.Windows.Forms.ListBox lbEmploymentHistory;
    }
}
