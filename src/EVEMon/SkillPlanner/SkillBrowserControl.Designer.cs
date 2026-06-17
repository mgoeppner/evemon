using EVEMon.Common.Controls;

namespace EVEMon.SkillPlanner
{
    partial class SkillBrowserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillBrowserControl));
            this.cmsSkillContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPlanTo0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrTrainingSkillTick = new System.Windows.Forms.Timer(this.components);
            this.verticalSplitContainer = new EVEMon.Common.Controls.PersistentSplitContainer();
            this.skillSelectControl = new EVEMon.SkillPlanner.SkillSelectControl();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.skillTreeDisplay = new EVEMon.SkillPlanner.SkillTreeDisplayControl();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.showSkillExplorerMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ownsBookToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.planToLevel = new System.Windows.Forms.ToolStripDropDownButton();
            this.planTo0Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.planTo1Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.planTo2Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.planTo3Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.planTo4Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.planTo5Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlPlanControl = new System.Windows.Forms.Panel();
            this.flpSkillNameCost = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSkillName = new System.Windows.Forms.Label();
            this.lblSkillCost = new System.Windows.Forms.Label();
            this.lblSkillClass = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.lblAttributes = new System.Windows.Forms.Label();
            this.lblLevel5Time = new System.Windows.Forms.Label();
            this.lblLevel4Time = new System.Windows.Forms.Label();
            this.lblLevel3Time = new System.Windows.Forms.Label();
            this.lblLevel2Time = new System.Windows.Forms.Label();
            this.lblLevel1Time = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.cmsSkillContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitContainer)).BeginInit();
            this.verticalSplitContainer.Panel1.SuspendLayout();
            this.verticalSplitContainer.Panel2.SuspendLayout();
            this.verticalSplitContainer.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.pnlPlanControl.SuspendLayout();
            this.flpSkillNameCost.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsSkillContext
            // 
            this.cmsSkillContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPlanTo0,
            this.miPlanTo1,
            this.miPlanTo2,
            this.miPlanTo3,
            this.miPlanTo4,
            this.miPlanTo5});
            this.cmsSkillContext.Name = "cmsSkillContext";
            this.cmsSkillContext.Size = new System.Drawing.Size(151, 136);
            // 
            // miPlanTo0
            // 
            this.miPlanTo0.Name = "miPlanTo0";
            this.miPlanTo0.Size = new System.Drawing.Size(150, 22);
            this.miPlanTo0.Text = resources.GetString("this.miPlanTo0.Text");
            this.miPlanTo0.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // miPlanTo1
            // 
            this.miPlanTo1.Name = "miPlanTo1";
            this.miPlanTo1.Size = new System.Drawing.Size(150, 22);
            this.miPlanTo1.Text = resources.GetString("this.miPlanTo1.Text");
            this.miPlanTo1.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // miPlanTo2
            // 
            this.miPlanTo2.Name = "miPlanTo2";
            this.miPlanTo2.Size = new System.Drawing.Size(150, 22);
            this.miPlanTo2.Text = resources.GetString("this.miPlanTo2.Text");
            this.miPlanTo2.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // miPlanTo3
            // 
            this.miPlanTo3.Name = "miPlanTo3";
            this.miPlanTo3.Size = new System.Drawing.Size(150, 22);
            this.miPlanTo3.Text = resources.GetString("this.miPlanTo3.Text");
            this.miPlanTo3.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // miPlanTo4
            // 
            this.miPlanTo4.Name = "miPlanTo4";
            this.miPlanTo4.Size = new System.Drawing.Size(150, 22);
            this.miPlanTo4.Text = resources.GetString("this.miPlanTo4.Text");
            this.miPlanTo4.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // miPlanTo5
            // 
            this.miPlanTo5.Name = "miPlanTo5";
            this.miPlanTo5.Size = new System.Drawing.Size(150, 22);
            this.miPlanTo5.Text = resources.GetString("this.miPlanTo5.Text");
            this.miPlanTo5.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // tmrTrainingSkillTick
            // 
            this.tmrTrainingSkillTick.Interval = 30000;
            this.tmrTrainingSkillTick.Tick += new System.EventHandler(this.tmrTrainingSkill_Tick);
            // 
            // verticalSplitContainer
            // 
            this.verticalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.verticalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.verticalSplitContainer.Name = "verticalSplitContainer";
            // 
            // verticalSplitContainer.Panel1
            // 
            this.verticalSplitContainer.Panel1.Controls.Add(this.skillSelectControl);
            this.verticalSplitContainer.Panel1MinSize = 45;
            // 
            // verticalSplitContainer.Panel2
            // 
            this.verticalSplitContainer.Panel2.Controls.Add(this.rightPanel);
            this.verticalSplitContainer.Panel2.Controls.Add(this.lblHelp);
            this.verticalSplitContainer.RememberDistanceKey = null;
            this.verticalSplitContainer.Size = new System.Drawing.Size(840, 513);
            this.verticalSplitContainer.SplitterDistance = 200;
            this.verticalSplitContainer.SplitterWidth = 3;
            this.verticalSplitContainer.TabIndex = 1;
            // 
            // skillSelectControl
            // 
            this.skillSelectControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillSelectControl.HostedInSkillBrowser = true;
            this.skillSelectControl.Location = new System.Drawing.Point(0, 0);
            this.skillSelectControl.Margin = new System.Windows.Forms.Padding(2);
            this.skillSelectControl.Name = "skillSelectControl";
            this.skillSelectControl.Size = new System.Drawing.Size(200, 513);
            this.skillSelectControl.TabIndex = 0;
            this.skillSelectControl.SelectedSkillChanged += new System.EventHandler<System.EventArgs>(this.skillSelectControl_SelectedSkillChanged);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.skillTreeDisplay);
            this.rightPanel.Controls.Add(this.headerPanel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(637, 513);
            this.rightPanel.TabIndex = 2;
            // 
            // skillTreeDisplay
            // 
            this.skillTreeDisplay.AutoScroll = true;
            this.skillTreeDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillTreeDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTreeDisplay.Location = new System.Drawing.Point(0, 131);
            this.skillTreeDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.skillTreeDisplay.Name = "skillTreeDisplay";
            this.skillTreeDisplay.Size = new System.Drawing.Size(637, 382);
            this.skillTreeDisplay.TabIndex = 1;
            this.skillTreeDisplay.SkillClicked += new System.EventHandler<EVEMon.Common.CustomEventArgs.SkillClickedEventArgs>(this.skillTreeDisplay_SkillClicked);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.toolStrip);
            this.headerPanel.Controls.Add(this.pnlPlanControl);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(637, 131);
            this.headerPanel.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSkillExplorerMenu,
            this.toolStripSeparator1,
            this.ownsBookToolStripButton,
            this.planToLevel});
            this.toolStrip.Location = new System.Drawing.Point(0, 106);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(637, 25);
            this.toolStrip.TabIndex = 20;
            this.toolStrip.Text = resources.GetString("this.toolStrip.Text");
            // 
            // showSkillExplorerMenu
            // 
            this.showSkillExplorerMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showSkillExplorerMenu.Image = ((System.Drawing.Image)(resources.GetObject("showSkillExplorerMenu.Image")));
            this.showSkillExplorerMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showSkillExplorerMenu.Name = "showSkillExplorerMenu";
            this.showSkillExplorerMenu.Size = new System.Drawing.Size(147, 22);
            this.showSkillExplorerMenu.Text = resources.GetString("this.showSkillExplorerMenu.Text");
            this.showSkillExplorerMenu.ToolTipText = resources.GetString("this.showSkillExplorerMenu.ToolTipText");
            this.showSkillExplorerMenu.Click += new System.EventHandler(this.showSkillExplorerMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ownsBookToolStripButton
            // 
            this.ownsBookToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ownsBookToolStripButton.CheckOnClick = true;
            this.ownsBookToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ownsBookToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ownsBookToolStripButton.Image")));
            this.ownsBookToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ownsBookToolStripButton.Name = "ownsBookToolStripButton";
            this.ownsBookToolStripButton.Size = new System.Drawing.Size(71, 22);
            this.ownsBookToolStripButton.Text = resources.GetString("this.ownsBookToolStripButton.Text");
            this.ownsBookToolStripButton.CheckedChanged += new System.EventHandler(this.ownsBookToolStripButton_CheckedChanged);
            // 
            // planToLevel
            // 
            this.planToLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planTo0Menu,
            this.planTo1Menu,
            this.planTo2Menu,
            this.planTo3Menu,
            this.planTo4Menu,
            this.planTo5Menu});
            this.planToLevel.Image = ((System.Drawing.Image)(resources.GetObject("planToLevel.Image")));
            this.planToLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.planToLevel.Name = "planToLevel";
            this.planToLevel.Size = new System.Drawing.Size(82, 22);
            this.planToLevel.Text = resources.GetString("this.planToLevel.Text");
            // 
            // planTo0Menu
            // 
            this.planTo0Menu.Name = "planTo0Menu";
            this.planTo0Menu.Size = new System.Drawing.Size(117, 22);
            this.planTo0Menu.Text = resources.GetString("this.planTo0Menu.Text");
            this.planTo0Menu.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // planTo1Menu
            // 
            this.planTo1Menu.Name = "planTo1Menu";
            this.planTo1Menu.Size = new System.Drawing.Size(117, 22);
            this.planTo1Menu.Text = resources.GetString("this.planTo1Menu.Text");
            this.planTo1Menu.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // planTo2Menu
            // 
            this.planTo2Menu.Name = "planTo2Menu";
            this.planTo2Menu.Size = new System.Drawing.Size(117, 22);
            this.planTo2Menu.Text = resources.GetString("this.planTo2Menu.Text");
            this.planTo2Menu.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // planTo3Menu
            // 
            this.planTo3Menu.Name = "planTo3Menu";
            this.planTo3Menu.Size = new System.Drawing.Size(117, 22);
            this.planTo3Menu.Text = resources.GetString("this.planTo3Menu.Text");
            this.planTo3Menu.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // planTo4Menu
            // 
            this.planTo4Menu.Name = "planTo4Menu";
            this.planTo4Menu.Size = new System.Drawing.Size(117, 22);
            this.planTo4Menu.Text = resources.GetString("this.planTo4Menu.Text");
            this.planTo4Menu.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // planTo5Menu
            // 
            this.planTo5Menu.Name = "planTo5Menu";
            this.planTo5Menu.Size = new System.Drawing.Size(117, 22);
            this.planTo5Menu.Text = resources.GetString("this.planTo5Menu.Text");
            this.planTo5Menu.Click += new System.EventHandler(this.planToMenu_Click);
            // 
            // pnlPlanControl
            // 
            this.pnlPlanControl.Controls.Add(this.flpSkillNameCost);
            this.pnlPlanControl.Controls.Add(this.lblSkillClass);
            this.pnlPlanControl.Controls.Add(this.descriptionTextBox);
            this.pnlPlanControl.Controls.Add(this.lblAttributes);
            this.pnlPlanControl.Controls.Add(this.lblLevel5Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel4Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel3Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel2Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel1Time);
            this.pnlPlanControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPlanControl.Location = new System.Drawing.Point(0, 0);
            this.pnlPlanControl.Name = "pnlPlanControl";
            this.pnlPlanControl.Size = new System.Drawing.Size(637, 106);
            this.pnlPlanControl.TabIndex = 4;
            // 
            // flpSkillNameCost
            // 
            this.flpSkillNameCost.AutoSize = true;
            this.flpSkillNameCost.Controls.Add(this.lblSkillName);
            this.flpSkillNameCost.Controls.Add(this.lblSkillCost);
            this.flpSkillNameCost.Location = new System.Drawing.Point(0, 19);
            this.flpSkillNameCost.Margin = new System.Windows.Forms.Padding(0);
            this.flpSkillNameCost.Name = "flpSkillNameCost";
            this.flpSkillNameCost.Size = new System.Drawing.Size(113, 13);
            this.flpSkillNameCost.TabIndex = 20;
            // 
            // lblSkillName
            // 
            this.lblSkillName.AutoSize = true;
            this.lblSkillName.Location = new System.Drawing.Point(3, 0);
            this.lblSkillName.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblSkillName.Name = "lblSkillName";
            this.lblSkillName.Size = new System.Drawing.Size(57, 13);
            this.lblSkillName.TabIndex = 0;
            this.lblSkillName.Text = resources.GetString("this.lblSkillName.Text");
            // 
            // lblSkillCost
            // 
            this.lblSkillCost.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSkillCost.AutoSize = true;
            this.lblSkillCost.Location = new System.Drawing.Point(60, 0);
            this.lblSkillCost.Margin = new System.Windows.Forms.Padding(0);
            this.lblSkillCost.Name = "lblSkillCost";
            this.lblSkillCost.Size = new System.Drawing.Size(50, 13);
            this.lblSkillCost.TabIndex = 19;
            this.lblSkillCost.Text = resources.GetString("this.lblSkillCost.Text");
            // 
            // lblSkillClass
            // 
            this.lblSkillClass.AutoSize = true;
            this.lblSkillClass.Location = new System.Drawing.Point(3, 4);
            this.lblSkillClass.Name = "lblSkillClass";
            this.lblSkillClass.Size = new System.Drawing.Size(71, 13);
            this.lblSkillClass.TabIndex = 18;
            this.lblSkillClass.Text = resources.GetString("this.lblSkillClass.Text");
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionTextBox.Location = new System.Drawing.Point(334, 19);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(300, 81);
            this.descriptionTextBox.TabIndex = 17;
            this.descriptionTextBox.Text = resources.GetString("this.descriptionTextBox.Text");
            // 
            // lblAttributes
            // 
            this.lblAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAttributes.Location = new System.Drawing.Point(296, 4);
            this.lblAttributes.Name = "lblAttributes";
            this.lblAttributes.Size = new System.Drawing.Size(338, 13);
            this.lblAttributes.TabIndex = 14;
            this.lblAttributes.Text = resources.GetString("this.lblAttributes.Text");
            this.lblAttributes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLevel5Time
            // 
            this.lblLevel5Time.AutoSize = true;
            this.lblLevel5Time.Location = new System.Drawing.Point(3, 87);
            this.lblLevel5Time.Name = "lblLevel5Time";
            this.lblLevel5Time.Size = new System.Drawing.Size(268, 13);
            this.lblLevel5Time.TabIndex = 5;
            this.lblLevel5Time.Text = resources.GetString("this.lblLevel5Time.Text");
            // 
            // lblLevel4Time
            // 
            this.lblLevel4Time.AutoSize = true;
            this.lblLevel4Time.Location = new System.Drawing.Point(3, 74);
            this.lblLevel4Time.Name = "lblLevel4Time";
            this.lblLevel4Time.Size = new System.Drawing.Size(259, 13);
            this.lblLevel4Time.TabIndex = 4;
            this.lblLevel4Time.Text = resources.GetString("this.lblLevel4Time.Text");
            // 
            // lblLevel3Time
            // 
            this.lblLevel3Time.AutoSize = true;
            this.lblLevel3Time.Location = new System.Drawing.Point(3, 61);
            this.lblLevel3Time.Name = "lblLevel3Time";
            this.lblLevel3Time.Size = new System.Drawing.Size(264, 13);
            this.lblLevel3Time.TabIndex = 3;
            this.lblLevel3Time.Text = resources.GetString("this.lblLevel3Time.Text");
            // 
            // lblLevel2Time
            // 
            this.lblLevel2Time.AutoSize = true;
            this.lblLevel2Time.Location = new System.Drawing.Point(3, 48);
            this.lblLevel2Time.Name = "lblLevel2Time";
            this.lblLevel2Time.Size = new System.Drawing.Size(231, 13);
            this.lblLevel2Time.TabIndex = 2;
            this.lblLevel2Time.Text = resources.GetString("this.lblLevel2Time.Text");
            // 
            // lblLevel1Time
            // 
            this.lblLevel1Time.AutoSize = true;
            this.lblLevel1Time.Location = new System.Drawing.Point(3, 35);
            this.lblLevel1Time.Name = "lblLevel1Time";
            this.lblLevel1Time.Size = new System.Drawing.Size(234, 13);
            this.lblLevel1Time.TabIndex = 1;
            this.lblLevel1Time.Text = resources.GetString("this.lblLevel1Time.Text");
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Location = new System.Drawing.Point(0, 0);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(218, 13);
            this.lblHelp.TabIndex = 30;
            this.lblHelp.Text = resources.GetString("this.lblHelp.Text");
            // 
            // SkillBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.verticalSplitContainer);
            this.Name = "SkillBrowserControl";
            this.Size = new System.Drawing.Size(840, 513);
            this.cmsSkillContext.ResumeLayout(false);
            this.verticalSplitContainer.Panel1.ResumeLayout(false);
            this.verticalSplitContainer.Panel2.ResumeLayout(false);
            this.verticalSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitContainer)).EndInit();
            this.verticalSplitContainer.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.pnlPlanControl.ResumeLayout(false);
            this.pnlPlanControl.PerformLayout();
            this.flpSkillNameCost.ResumeLayout(false);
            this.flpSkillNameCost.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private PersistentSplitContainer verticalSplitContainer;
        private EVEMon.SkillPlanner.SkillSelectControl skillSelectControl;
        private System.Windows.Forms.ContextMenuStrip cmsSkillContext;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo1;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo2;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo3;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo4;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo5;
        private System.Windows.Forms.Timer tmrTrainingSkillTick;
        private System.Windows.Forms.Panel pnlPlanControl;
        private System.Windows.Forms.Label lblSkillClass;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label lblAttributes;
        private System.Windows.Forms.Label lblLevel5Time;
        private System.Windows.Forms.Label lblLevel4Time;
        private System.Windows.Forms.Label lblLevel3Time;
        private System.Windows.Forms.Label lblLevel2Time;
        private System.Windows.Forms.Label lblLevel1Time;
        private System.Windows.Forms.Label lblSkillName;
        private SkillTreeDisplayControl skillTreeDisplay;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo0;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.ToolStripButton showSkillExplorerMenu;
        private System.Windows.Forms.ToolStripDropDownButton planToLevel;
        private System.Windows.Forms.ToolStripMenuItem planTo2Menu;
        private System.Windows.Forms.ToolStripMenuItem planTo0Menu;
        private System.Windows.Forms.ToolStripMenuItem planTo1Menu;
        private System.Windows.Forms.ToolStripMenuItem planTo3Menu;
        private System.Windows.Forms.ToolStripMenuItem planTo4Menu;
        private System.Windows.Forms.ToolStripMenuItem planTo5Menu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ownsBookToolStripButton;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblSkillCost;
        private System.Windows.Forms.FlowLayoutPanel flpSkillNameCost;

    }
}
