using EVEMon.Common.Controls;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterSkillsQueueList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterSkillsQueueList));
            this.noSkillsQueueLabel = new System.Windows.Forms.Label();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInSkillExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddSkill = new System.Windows.Forms.ToolStripMenuItem();
            this.addSkillSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCreatePlanFromSkillQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.lbSkillsQueue = new EVEMon.Common.Controls.NoFlickerListBox();
            this.showInSkillBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // noSkillsQueueLabel
            // 
            this.noSkillsQueueLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noSkillsQueueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noSkillsQueueLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noSkillsQueueLabel.Location = new System.Drawing.Point(0, 0);
            this.noSkillsQueueLabel.Margin = new System.Windows.Forms.Padding(0);
            this.noSkillsQueueLabel.Name = "noSkillsQueueLabel";
            this.noSkillsQueueLabel.Size = new System.Drawing.Size(287, 320);
            this.noSkillsQueueLabel.TabIndex = 0;
            this.noSkillsQueueLabel.Text = resources.GetString("this.noSkillsQueueLabel.Text");
            this.noSkillsQueueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 10000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.IsBalloon = true;
            this.ttToolTip.ReshowDelay = 100;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInSkillBrowserMenuItem,
            this.showInSkillExplorerMenuItem,
            this.showInMenuSeparator,
            this.tsmiAddSkill,
            this.addSkillSeparator,
            this.tsmiCreatePlanFromSkillQueue});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(235, 126);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // showInSkillExplorerMenuItem
            // 
            this.showInSkillExplorerMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showInSkillExplorerMenuItem.Image")));
            this.showInSkillExplorerMenuItem.Name = "showInSkillExplorerMenuItem";
            this.showInSkillExplorerMenuItem.Size = new System.Drawing.Size(234, 22);
            this.showInSkillExplorerMenuItem.Text = resources.GetString("this.showInSkillExplorerMenuItem.Text");
            this.showInSkillExplorerMenuItem.Click += new System.EventHandler(this.showInSkillExplorerMenuItem_Click);
            // 
            // showInMenuSeparator
            // 
            this.showInMenuSeparator.Name = "showInMenuSeparator";
            this.showInMenuSeparator.Size = new System.Drawing.Size(231, 6);
            // 
            // tsmiAddSkill
            // 
            this.tsmiAddSkill.Name = "tsmiAddSkill";
            this.tsmiAddSkill.Size = new System.Drawing.Size(234, 22);
            this.tsmiAddSkill.Text = resources.GetString("this.tsmiAddSkill.Text");
            // 
            // addSkillSeparator
            // 
            this.addSkillSeparator.Name = "addSkillSeparator";
            this.addSkillSeparator.Size = new System.Drawing.Size(231, 6);
            // 
            // tsmiCreatePlanFromSkillQueue
            // 
            this.tsmiCreatePlanFromSkillQueue.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCreatePlanFromSkillQueue.Image")));
            this.tsmiCreatePlanFromSkillQueue.Name = "tsmiCreatePlanFromSkillQueue";
            this.tsmiCreatePlanFromSkillQueue.Size = new System.Drawing.Size(234, 22);
            this.tsmiCreatePlanFromSkillQueue.Text = resources.GetString("this.tsmiCreatePlanFromSkillQueue.Text");
            this.tsmiCreatePlanFromSkillQueue.Click += new System.EventHandler(this.tsmiCreatePlanFromSkillQueue_Click);
            // 
            // lbSkillsQueue
            // 
            this.lbSkillsQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbSkillsQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSkillsQueue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbSkillsQueue.FormattingEnabled = true;
            this.lbSkillsQueue.IntegralHeight = false;
            this.lbSkillsQueue.ItemHeight = 15;
            this.lbSkillsQueue.Location = new System.Drawing.Point(0, 0);
            this.lbSkillsQueue.Margin = new System.Windows.Forms.Padding(0);
            this.lbSkillsQueue.Name = "lbSkillsQueue";
            this.lbSkillsQueue.Size = new System.Drawing.Size(287, 320);
            this.lbSkillsQueue.TabIndex = 0;
            this.lbSkillsQueue.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbSkillsQueue_DrawItem);
            this.lbSkillsQueue.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbSkillsQueue_MeasureItem);
            this.lbSkillsQueue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbSkillsQueue_MouseDown);
            this.lbSkillsQueue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbSkillsQueue_MouseMove);
            this.lbSkillsQueue.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lbSkillsQueue_MouseWheel);
            // 
            // showInSkillBrowserMenuItem
            // 
            this.showInSkillBrowserMenuItem.Name = "showInSkillBrowserMenuItem";
            this.showInSkillBrowserMenuItem.Size = new System.Drawing.Size(234, 22);
            this.showInSkillBrowserMenuItem.Text = resources.GetString("this.showInSkillBrowserMenuItem.Text");
            this.showInSkillBrowserMenuItem.Click += new System.EventHandler(this.showInSkillBrowserMenuItem_Click);
            // 
            // CharacterSkillsQueueList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noSkillsQueueLabel);
            this.Controls.Add(this.lbSkillsQueue);
            this.Name = "CharacterSkillsQueueList";
            this.Size = new System.Drawing.Size(287, 320);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noSkillsQueueLabel;
        private System.Windows.Forms.ToolTip ttToolTip;
        private NoFlickerListBox lbSkillsQueue;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showInSkillExplorerMenuItem;
        private System.Windows.Forms.ToolStripSeparator showInMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddSkill;
        private System.Windows.Forms.ToolStripSeparator addSkillSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreatePlanFromSkillQueue;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowserMenuItem;
    }
}
