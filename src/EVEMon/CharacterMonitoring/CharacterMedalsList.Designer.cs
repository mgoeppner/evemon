namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterMedalsList
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
            this.noMedalsLabel = new System.Windows.Forms.Label();
            this.lbMedals = new System.Windows.Forms.ListBox();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // noMedalsLabel
            // 
            this.noMedalsLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noMedalsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noMedalsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noMedalsLabel.Location = new System.Drawing.Point(0, 0);
            this.noMedalsLabel.Name = "noMedalsLabel";
            this.noMedalsLabel.Size = new System.Drawing.Size(372, 402);
            this.noMedalsLabel.TabIndex = 2;
            this.noMedalsLabel.Text = "Medals information not available.";
            this.noMedalsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMedals
            // 
            this.lbMedals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbMedals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMedals.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbMedals.FormattingEnabled = true;
            this.lbMedals.IntegralHeight = false;
            this.lbMedals.ItemHeight = 15;
            this.lbMedals.Location = new System.Drawing.Point(0, 0);
            this.lbMedals.Margin = new System.Windows.Forms.Padding(0);
            this.lbMedals.Name = "lbMedals";
            this.lbMedals.Size = new System.Drawing.Size(372, 402);
            this.lbMedals.TabIndex = 4;
            this.lbMedals.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbMedals_DrawItem);
            this.lbMedals.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbMedals_MeasureItem);
            this.lbMedals.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbMedals_MouseDown);
            this.lbMedals.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbMedals_MouseMove);
            this.lbMedals.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbMedals_MouseWheel);
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 32000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.IsBalloon = true;
            this.ttToolTip.ReshowDelay = 100;
            // 
            // CharacterMedalsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noMedalsLabel);
            this.Controls.Add(this.lbMedals);
            this.Name = "CharacterMedalsList";
            this.Size = new System.Drawing.Size(372, 402);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noMedalsLabel;
        private System.Windows.Forms.ListBox lbMedals;
        private System.Windows.Forms.ToolTip ttToolTip;
    }
}
