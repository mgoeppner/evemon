using EVEMon.Common.Controls;

namespace EVEMon.SkillPlanner
{
    partial class CertificateBrowserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificateBrowserControl));
            this.leftSplitContainer = new EVEMon.Common.Controls.PersistentSplitContainer();
            this.certSelectControl = new EVEMon.SkillPlanner.CertificateSelectControl();
            this.panelRight = new System.Windows.Forms.Panel();
            this.rightSplitContainer = new EVEMon.Common.Controls.PersistentSplitContainer();
            this.certDisplayControl = new EVEMon.SkillPlanner.CertificateTreeDisplayControl();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblLevel5Time = new System.Windows.Forms.Label();
            this.tspControl = new System.Windows.Forms.ToolStrip();
            this.tslbTextForEligibility = new System.Windows.Forms.ToolStripLabel();
            this.tslbEligible = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.planToLevel = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsPlanToLevelOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelThree = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelFour = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelFive = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.textboxDescription = new System.Windows.Forms.TextBox();
            this.lblLevel4Time = new System.Windows.Forms.Label();
            this.lblLevel3Time = new System.Windows.Forms.Label();
            this.lblLevel2Time = new System.Windows.Forms.Label();
            this.lblLevel1Time = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tspControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // leftSplitContainer
            // 
            this.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.leftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftSplitContainer.Name = "leftSplitContainer";
            // 
            // leftSplitContainer.Panel1
            // 
            this.leftSplitContainer.Panel1.Controls.Add(this.certSelectControl);
            this.leftSplitContainer.Panel1MinSize = 45;
            // 
            // leftSplitContainer.Panel2
            // 
            this.leftSplitContainer.Panel2.Controls.Add(this.panelRight);
            this.leftSplitContainer.Panel2.Controls.Add(this.lblHelp);
            this.leftSplitContainer.RememberDistanceKey = null;
            this.leftSplitContainer.Size = new System.Drawing.Size(824, 550);
            this.leftSplitContainer.SplitterDistance = 232;
            this.leftSplitContainer.TabIndex = 3;
            // 
            // certSelectControl
            // 
            this.certSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.certSelectControl.Location = new System.Drawing.Point(0, 0);
            this.certSelectControl.Margin = new System.Windows.Forms.Padding(2);
            this.certSelectControl.Name = "certSelectControl";
            this.certSelectControl.Size = new System.Drawing.Size(232, 550);
            this.certSelectControl.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.rightSplitContainer);
            this.panelRight.Controls.Add(this.panelHeader);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(588, 550);
            this.panelRight.TabIndex = 3;
            this.panelRight.Visible = false;
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.BackColor = System.Drawing.SystemColors.Control;
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 133);
            this.rightSplitContainer.Name = "rightSplitContainer";
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.Controls.Add(this.certDisplayControl);
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.AutoScroll = true;
            this.rightSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.rightSplitContainer.RememberDistanceKey = null;
            this.rightSplitContainer.Size = new System.Drawing.Size(588, 417);
            this.rightSplitContainer.SplitterDistance = 420;
            this.rightSplitContainer.TabIndex = 3;
            // 
            // certDisplayControl
            // 
            this.certDisplayControl.BackColor = System.Drawing.SystemColors.Window;
            this.certDisplayControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.certDisplayControl.Location = new System.Drawing.Point(0, 0);
            this.certDisplayControl.Name = "certDisplayControl";
            this.certDisplayControl.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.certDisplayControl.Size = new System.Drawing.Size(420, 417);
            this.certDisplayControl.TabIndex = 2;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblLevel5Time);
            this.panelHeader.Controls.Add(this.tspControl);
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.lblCategory);
            this.panelHeader.Controls.Add(this.textboxDescription);
            this.panelHeader.Controls.Add(this.lblLevel4Time);
            this.panelHeader.Controls.Add(this.lblLevel3Time);
            this.panelHeader.Controls.Add(this.lblLevel2Time);
            this.panelHeader.Controls.Add(this.lblLevel1Time);
            this.panelHeader.Controls.Add(this.lblName);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(588, 133);
            this.panelHeader.TabIndex = 1;
            // 
            // lblLevel5Time
            // 
            this.lblLevel5Time.AutoSize = true;
            this.lblLevel5Time.Location = new System.Drawing.Point(3, 84);
            this.lblLevel5Time.Name = "lblLevel5Time";
            this.lblLevel5Time.Size = new System.Drawing.Size(131, 13);
            this.lblLevel5Time.TabIndex = 29;
            this.lblLevel5Time.Text = resources.GetString("this.lblLevel5Time.Text");
            // 
            // tspControl
            // 
            this.tspControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspControl.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbTextForEligibility,
            this.tslbEligible,
            this.toolStripSeparator1,
            this.planToLevel});
            this.tspControl.Location = new System.Drawing.Point(0, 108);
            this.tspControl.Name = "tspControl";
            this.tspControl.Size = new System.Drawing.Size(588, 25);
            this.tspControl.TabIndex = 28;
            // 
            // tslbTextForEligibility
            // 
            this.tslbTextForEligibility.Name = "tslbTextForEligibility";
            this.tslbTextForEligibility.Size = new System.Drawing.Size(202, 22);
            this.tslbTextForEligibility.Text = resources.GetString("this.tslbTextForEligibility.Text");
            // 
            // tslbEligible
            // 
            this.tslbEligible.Name = "tslbEligible";
            this.tslbEligible.Size = new System.Drawing.Size(34, 22);
            this.tslbEligible.Text = resources.GetString("this.tslbEligible.Text");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // planToLevel
            // 
            this.planToLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPlanToLevelOne,
            this.tsPlanToLevelTwo,
            this.tsPlanToLevelThree,
            this.tsPlanToLevelFour,
            this.tsPlanToLevelFive});
            this.planToLevel.Image = ((System.Drawing.Image)(resources.GetObject("planToLevel.Image")));
            this.planToLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.planToLevel.Name = "planToLevel";
            this.planToLevel.Size = new System.Drawing.Size(84, 22);
            this.planToLevel.Text = resources.GetString("this.planToLevel.Text");
            this.planToLevel.ToolTipText = resources.GetString("this.planToLevel.ToolTipText");
            // 
            // tsPlanToLevelOne
            // 
            this.tsPlanToLevelOne.Name = "tsPlanToLevelOne";
            this.tsPlanToLevelOne.Size = new System.Drawing.Size(152, 22);
            this.tsPlanToLevelOne.Text = resources.GetString("this.tsPlanToLevelOne.Text");
            this.tsPlanToLevelOne.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelTwo
            // 
            this.tsPlanToLevelTwo.Name = "tsPlanToLevelTwo";
            this.tsPlanToLevelTwo.Size = new System.Drawing.Size(152, 22);
            this.tsPlanToLevelTwo.Text = resources.GetString("this.tsPlanToLevelTwo.Text");
            this.tsPlanToLevelTwo.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelThree
            // 
            this.tsPlanToLevelThree.Name = "tsPlanToLevelThree";
            this.tsPlanToLevelThree.Size = new System.Drawing.Size(152, 22);
            this.tsPlanToLevelThree.Text = resources.GetString("this.tsPlanToLevelThree.Text");
            this.tsPlanToLevelThree.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelFour
            // 
            this.tsPlanToLevelFour.Name = "tsPlanToLevelFour";
            this.tsPlanToLevelFour.Size = new System.Drawing.Size(152, 22);
            this.tsPlanToLevelFour.Text = resources.GetString("this.tsPlanToLevelFour.Text");
            this.tsPlanToLevelFour.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelFive
            // 
            this.tsPlanToLevelFive.Name = "tsPlanToLevelFive";
            this.tsPlanToLevelFive.Size = new System.Drawing.Size(152, 22);
            this.tsPlanToLevelFive.Text = resources.GetString("this.tsPlanToLevelFive.Text");
            this.tsPlanToLevelFive.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(306, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(3, 3);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(71, 13);
            this.lblCategory.TabIndex = 26;
            this.lblCategory.Text = resources.GetString("this.lblCategory.Text");
            // 
            // textboxDescription
            // 
            this.textboxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textboxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxDescription.Location = new System.Drawing.Point(376, 3);
            this.textboxDescription.Multiline = true;
            this.textboxDescription.Name = "textboxDescription";
            this.textboxDescription.ReadOnly = true;
            this.textboxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxDescription.Size = new System.Drawing.Size(209, 102);
            this.textboxDescription.TabIndex = 25;
            // 
            // lblLevel4Time
            // 
            this.lblLevel4Time.AutoSize = true;
            this.lblLevel4Time.Location = new System.Drawing.Point(3, 71);
            this.lblLevel4Time.Name = "lblLevel4Time";
            this.lblLevel4Time.Size = new System.Drawing.Size(140, 13);
            this.lblLevel4Time.TabIndex = 23;
            this.lblLevel4Time.Text = resources.GetString("this.lblLevel4Time.Text");
            // 
            // lblLevel3Time
            // 
            this.lblLevel3Time.AutoSize = true;
            this.lblLevel3Time.Location = new System.Drawing.Point(3, 58);
            this.lblLevel3Time.Name = "lblLevel3Time";
            this.lblLevel3Time.Size = new System.Drawing.Size(139, 13);
            this.lblLevel3Time.TabIndex = 22;
            this.lblLevel3Time.Text = resources.GetString("this.lblLevel3Time.Text");
            // 
            // lblLevel2Time
            // 
            this.lblLevel2Time.AutoSize = true;
            this.lblLevel2Time.Location = new System.Drawing.Point(3, 45);
            this.lblLevel2Time.Name = "lblLevel2Time";
            this.lblLevel2Time.Size = new System.Drawing.Size(112, 13);
            this.lblLevel2Time.TabIndex = 21;
            this.lblLevel2Time.Text = resources.GetString("this.lblLevel2Time.Text");
            // 
            // lblLevel1Time
            // 
            this.lblLevel1Time.AutoSize = true;
            this.lblLevel1Time.Location = new System.Drawing.Point(3, 32);
            this.lblLevel1Time.Name = "lblLevel1Time";
            this.lblLevel1Time.Size = new System.Drawing.Size(109, 13);
            this.lblLevel1Time.TabIndex = 20;
            this.lblLevel1Time.Text = resources.GetString("this.lblLevel1Time.Text");
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = resources.GetString("this.lblName.Text");
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Location = new System.Drawing.Point(0, 0);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(247, 13);
            this.lblHelp.TabIndex = 29;
            this.lblHelp.Text = resources.GetString("this.lblHelp.Text");
            // 
            // CertificateBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.leftSplitContainer);
            this.Name = "CertificateBrowserControl";
            this.Size = new System.Drawing.Size(824, 550);
            this.leftSplitContainer.Panel1.ResumeLayout(false);
            this.leftSplitContainer.Panel2.ResumeLayout(false);
            this.leftSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).EndInit();
            this.leftSplitContainer.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tspControl.ResumeLayout(false);
            this.tspControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CertificateSelectControl certSelectControl;
        private System.Windows.Forms.Panel panelHeader;
        private CertificateTreeDisplayControl certDisplayControl;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox textboxDescription;
        private System.Windows.Forms.Label lblLevel4Time;
        private System.Windows.Forms.Label lblLevel3Time;
        private System.Windows.Forms.Label lblLevel2Time;
        private System.Windows.Forms.Label lblLevel1Time;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStrip tspControl;
        private System.Windows.Forms.ToolStripLabel tslbTextForEligibility;
        private System.Windows.Forms.ToolStripLabel tslbEligible;
        private System.Windows.Forms.ToolStripDropDownButton planToLevel;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelTwo;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelThree;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelFive;
        private PersistentSplitContainer leftSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelOne;
        private System.Windows.Forms.Panel panelRight;
        private PersistentSplitContainer rightSplitContainer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblLevel5Time;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelFour;
    }
}