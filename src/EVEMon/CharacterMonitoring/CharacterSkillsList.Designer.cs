using EVEMon.Common.Controls;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterSkillsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterSkillsList));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInSkillExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddSkill = new System.Windows.Forms.ToolStripMenuItem();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.noSkillsLabel = new System.Windows.Forms.Label();
            this.lbSkills = new EVEMon.Common.Controls.NoFlickerListBox();
            this.showInSkillBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInSkillBrowserMenuItem,
            this.showInSkillExplorerMenuItem,
            this.showInMenuSeparator,
            this.tsmiAddSkill});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(195, 98);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // showInSkillExplorerMenuItem
            // 
            this.showInSkillExplorerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showInSkillExplorerMenuItem.Image")));
            this.showInSkillExplorerMenuItem.Name = "showInSkillExplorerMenuItem";
            this.showInSkillExplorerMenuItem.Size = new System.Drawing.Size(194, 22);
            this.showInSkillExplorerMenuItem.Text = resources.GetString("this.showInSkillExplorerMenuItem.Text");
            this.showInSkillExplorerMenuItem.Click += new System.EventHandler(this.showInSkillExplorerMenuItem_Click);
            // 
            // showInMenuSeparator
            // 
            this.showInMenuSeparator.Name = "showInMenuSeparator";
            this.showInMenuSeparator.Size = new System.Drawing.Size(191, 6);
            // 
            // tsmiAddSkill
            // 
            this.tsmiAddSkill.Name = "tsmiAddSkill";
            this.tsmiAddSkill.Size = new System.Drawing.Size(194, 22);
            this.tsmiAddSkill.Text = resources.GetString("this.tsmiAddSkill.Text");
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 32000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.IsBalloon = true;
            this.ttToolTip.ReshowDelay = 100;
            // 
            // noSkillsLabel
            // 
            this.noSkillsLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noSkillsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noSkillsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noSkillsLabel.Location = new System.Drawing.Point(0, 0);
            this.noSkillsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.noSkillsLabel.Name = "noSkillsLabel";
            this.noSkillsLabel.Size = new System.Drawing.Size(287, 320);
            this.noSkillsLabel.TabIndex = 4;
            this.noSkillsLabel.Text = resources.GetString("this.noSkillsLabel.Text");
            this.noSkillsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSkills
            // 
            this.lbSkills.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSkills.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbSkills.FormattingEnabled = true;
            this.lbSkills.IntegralHeight = false;
            this.lbSkills.ItemHeight = 15;
            this.lbSkills.Location = new System.Drawing.Point(0, 0);
            this.lbSkills.Margin = new System.Windows.Forms.Padding(0);
            this.lbSkills.Name = "lbSkills";
            this.lbSkills.Size = new System.Drawing.Size(287, 320);
            this.lbSkills.TabIndex = 0;
            this.lbSkills.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbSkills_DrawItem);
            this.lbSkills.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbSkills_MeasureItem);
            this.lbSkills.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSkills_MouseDown);
            this.lbSkills.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbSkills_MouseMove);
            this.lbSkills.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbSkills_MouseWheel);
            // 
            // showInSkillBrowserMenuItem
            // 
            this.showInSkillBrowserMenuItem.Name = "showInSkillBrowserMenuItem";
            this.showInSkillBrowserMenuItem.Size = new System.Drawing.Size(194, 22);
            this.showInSkillBrowserMenuItem.Text = resources.GetString("this.showInSkillBrowserMenuItem.Text");
            this.showInSkillBrowserMenuItem.Click += new System.EventHandler(this.showInSkillBrowserMenuItem_Click);
            // 
            // CharacterSkillsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noSkillsLabel);
            this.Controls.Add(this.lbSkills);
            this.Name = "CharacterSkillsList";
            this.Size = new System.Drawing.Size(287, 320);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private NoFlickerListBox lbSkills;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Label noSkillsLabel;
        private System.Windows.Forms.ToolStripMenuItem showInSkillExplorerMenuItem;
        private System.Windows.Forms.ToolStripSeparator showInMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddSkill;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowserMenuItem;
    }
}
