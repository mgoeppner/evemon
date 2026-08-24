using EVEMon.Common.Controls;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterStandingsList
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
            this.noStandingsLabel = new System.Windows.Forms.Label();
            this.lbStandings = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // noStandingsLabel
            // 
            this.noStandingsLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noStandingsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noStandingsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noStandingsLabel.Location = new System.Drawing.Point(0, 0);
            this.noStandingsLabel.Name = "noStandingsLabel";
            this.noStandingsLabel.Size = new System.Drawing.Size(328, 372);
            this.noStandingsLabel.TabIndex = 2;
            this.noStandingsLabel.Text = "Standings information not available.";
            this.noStandingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbStandings
            // 
            this.lbStandings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbStandings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStandings.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbStandings.FormattingEnabled = true;
            this.lbStandings.IntegralHeight = false;
            this.lbStandings.ItemHeight = 15;
            this.lbStandings.Location = new System.Drawing.Point(0, 0);
            this.lbStandings.Margin = new System.Windows.Forms.Padding(0);
            this.lbStandings.Name = "lbStandings";
            this.lbStandings.Size = new System.Drawing.Size(328, 372);
            this.lbStandings.TabIndex = 3;
            this.lbStandings.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbStandings_DrawItem);
            this.lbStandings.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbStandings_MeasureItem);
            this.lbStandings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbStandings_MouseDown);
            this.lbStandings.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbStandings_MouseWheel);
            // 
            // CharacterStandingsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noStandingsLabel);
            this.Controls.Add(this.lbStandings);
            this.Name = "CharacterStandingsList";
            this.Size = new System.Drawing.Size(328, 372);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noStandingsLabel;
        private System.Windows.Forms.ListBox lbStandings;
    }
}
