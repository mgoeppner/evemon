using EVEMon.Common.Enumerations;

namespace EVEMon.SkillPlanner
{
    partial class ShipBrowserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipBrowserControl));
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.lvShipProperties = new System.Windows.Forms.ListView();
            this.chAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ShipAttributeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipSelectControl = new EVEMon.SkillPlanner.ShipSelectControl();
            this.gbRequiredSkills = new System.Windows.Forms.GroupBox();
            this.requiredSkillsControl = new EVEMon.SkillPlanner.RequiredSkillsControl();
            this.tbCntrlShipInformation = new System.Windows.Forms.TabControl();
            this.tbPgShipDetails = new System.Windows.Forms.TabPage();
            this.tbPgShipMastery = new System.Windows.Forms.TabPage();
            this.pnlMastery = new System.Windows.Forms.Panel();
            this.masteryTreeDisplayControl = new EVEMon.SkillPlanner.MasteryTreeDisplayControl();
            this.tlStrpPlanTo = new System.Windows.Forms.ToolStrip();
            this.tslbTextForEligibility = new System.Windows.Forms.ToolStripLabel();
            this.tslbEligible = new System.Windows.Forms.ToolStripLabel();
            this.tsPlanToMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsPlanToLevelOne = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelThree = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelFour = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPlanToLevelFive = new System.Windows.Forms.ToolStripMenuItem();
            this.splitButtonLoadouts = new EVEMon.Common.Controls.SplitButton();
            this.LoadoutsProviderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblViewLoadouts = new System.Windows.Forms.Label();
            this.showInMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.showInSkillBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.showInItemBrowser = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scObjectBrowser)).BeginInit();
            this.scObjectBrowser.Panel1.SuspendLayout();
            this.scObjectBrowser.Panel2.SuspendLayout();
            this.scObjectBrowser.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlBrowserHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDetailsRight)).BeginInit();
            this.scDetailsRight.Panel1.SuspendLayout();
            this.scDetailsRight.Panel2.SuspendLayout();
            this.scDetailsRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDetails)).BeginInit();
            this.scDetails.Panel1.SuspendLayout();
            this.scDetails.Panel2.SuspendLayout();
            this.scDetails.SuspendLayout();
            this.gbDescription.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.ShipAttributeContextMenu.SuspendLayout();
            this.gbRequiredSkills.SuspendLayout();
            this.tbCntrlShipInformation.SuspendLayout();
            this.tbPgShipDetails.SuspendLayout();
            this.tbPgShipMastery.SuspendLayout();
            this.pnlMastery.SuspendLayout();
            this.tlStrpPlanTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // scObjectBrowser
            // 
            // 
            // scObjectBrowser.Panel1
            // 
            this.scObjectBrowser.Panel1.Controls.Add(this.shipSelectControl);
            this.scObjectBrowser.Size = new System.Drawing.Size(765, 512);
            // 
            // pnlDetails
            // 
            this.pnlDetails.Size = new System.Drawing.Size(597, 442);
            // 
            // pnlBrowserHeader
            // 
            this.pnlBrowserHeader.Controls.Add(this.lblViewLoadouts);
            this.pnlBrowserHeader.Controls.Add(this.splitButtonLoadouts);
            this.pnlBrowserHeader.Size = new System.Drawing.Size(597, 70);
            this.pnlBrowserHeader.Controls.SetChildIndex(this.lblEveObjName, 0);
            this.pnlBrowserHeader.Controls.SetChildIndex(this.lblEveObjCategory, 0);
            this.pnlBrowserHeader.Controls.SetChildIndex(this.eoImage, 0);
            this.pnlBrowserHeader.Controls.SetChildIndex(this.splitButtonLoadouts, 0);
            this.pnlBrowserHeader.Controls.SetChildIndex(this.lblViewLoadouts, 0);
            // 
            // scDetailsRight
            // 
            // 
            // scDetailsRight.Panel1
            // 
            this.scDetailsRight.Panel1.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            // 
            // scDetailsRight.Panel2
            // 
            this.scDetailsRight.Panel2.Controls.Add(this.gbRequiredSkills);
            this.scDetailsRight.Panel2MinSize = 108;
            this.scDetailsRight.Size = new System.Drawing.Size(240, 442);
            this.scDetailsRight.SplitterDistance = 330;
            // 
            // scDetails
            // 
            // 
            // scDetails.Panel1
            // 
            this.scDetails.Panel1.Controls.Add(this.tbCntrlShipInformation);
            this.scDetails.Size = new System.Drawing.Size(597, 442);
            this.scDetails.SplitterDistance = 353;
            // 
            // gbDescription
            // 
            this.gbDescription.Location = new System.Drawing.Point(0, 14);
            this.gbDescription.Size = new System.Drawing.Size(240, 316);
            // 
            // tbDescription
            // 
            this.tbDescription.Size = new System.Drawing.Size(234, 297);
            // 
            // gbAttributes
            // 
            this.gbAttributes.Controls.Add(this.lvShipProperties);
            this.gbAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAttributes.Location = new System.Drawing.Point(3, 3);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Size = new System.Drawing.Size(339, 410);
            this.gbAttributes.TabIndex = 4;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = resources.GetString("this.gbAttributes.Text");
            // 
            // lvShipProperties
            // 
            this.lvShipProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAttribute,
            this.chValue});
            this.lvShipProperties.ContextMenuStrip = this.ShipAttributeContextMenu;
            this.lvShipProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvShipProperties.FullRowSelect = true;
            this.lvShipProperties.HideSelection = false;
            this.lvShipProperties.Location = new System.Drawing.Point(3, 16);
            this.lvShipProperties.Name = "lvShipProperties";
            this.lvShipProperties.Size = new System.Drawing.Size(333, 391);
            this.lvShipProperties.TabIndex = 3;
            this.lvShipProperties.UseCompatibleStateImageBehavior = false;
            this.lvShipProperties.View = System.Windows.Forms.View.Details;
            this.lvShipProperties.DoubleClick += new System.EventHandler(this.propertiesList_DoubleClick);
            // 
            // chAttribute
            // 
            this.chAttribute.Text = resources.GetString("this.chAttribute.Text");
            this.chAttribute.Width = 120;
            // 
            // chValue
            // 
            this.chValue.Text = resources.GetString("this.chValue.Text");
            this.chValue.Width = 120;
            // 
            // ShipAttributeContextMenu
            // 
            this.ShipAttributeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInSkillBrowser,
            this.showInItemBrowser,
            this.showInMenuSeparator,
            this.exportToCSVToolStripMenuItem});
            this.ShipAttributeContextMenu.Name = "ShipPropertiesContextMenu";
            this.ShipAttributeContextMenu.Size = new System.Drawing.Size(189, 76);
            this.ShipAttributeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ItemAttributeContextMenu_Opening);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exportToCSVToolStripMenuItem.Text = resources.GetString("this.exportToCSVToolStripMenuItem.Text");
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
            // 
            // shipSelectControl
            // 
            this.shipSelectControl.AutoSize = true;
            this.shipSelectControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shipSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shipSelectControl.Location = new System.Drawing.Point(0, 0);
            this.shipSelectControl.Margin = new System.Windows.Forms.Padding(2);
            this.shipSelectControl.Name = "shipSelectControl";
            this.shipSelectControl.Size = new System.Drawing.Size(163, 512);
            this.shipSelectControl.TabIndex = 0;
            // 
            // gbRequiredSkills
            // 
            this.gbRequiredSkills.Controls.Add(this.requiredSkillsControl);
            this.gbRequiredSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRequiredSkills.Location = new System.Drawing.Point(0, 0);
            this.gbRequiredSkills.Name = "gbRequiredSkills";
            this.gbRequiredSkills.Size = new System.Drawing.Size(240, 108);
            this.gbRequiredSkills.TabIndex = 0;
            this.gbRequiredSkills.TabStop = false;
            this.gbRequiredSkills.Text = resources.GetString("this.gbRequiredSkills.Text");
            // 
            // requiredSkillsControl
            // 
            this.requiredSkillsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requiredSkillsControl.Location = new System.Drawing.Point(3, 16);
            this.requiredSkillsControl.MinimumSize = new System.Drawing.Size(187, 0);
            this.requiredSkillsControl.Name = "requiredSkillsControl";
            this.requiredSkillsControl.Size = new System.Drawing.Size(234, 89);
            this.requiredSkillsControl.TabIndex = 0;
            // 
            // tbCntrlShipInformation
            // 
            this.tbCntrlShipInformation.Controls.Add(this.tbPgShipDetails);
            this.tbCntrlShipInformation.Controls.Add(this.tbPgShipMastery);
            this.tbCntrlShipInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCntrlShipInformation.Location = new System.Drawing.Point(0, 0);
            this.tbCntrlShipInformation.Name = "tbCntrlShipInformation";
            this.tbCntrlShipInformation.SelectedIndex = 0;
            this.tbCntrlShipInformation.Size = new System.Drawing.Size(353, 442);
            this.tbCntrlShipInformation.TabIndex = 1;
            // 
            // tbPgShipDetails
            // 
            this.tbPgShipDetails.Controls.Add(this.gbAttributes);
            this.tbPgShipDetails.Location = new System.Drawing.Point(4, 22);
            this.tbPgShipDetails.Name = "tbPgShipDetails";
            this.tbPgShipDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgShipDetails.Size = new System.Drawing.Size(345, 416);
            this.tbPgShipDetails.TabIndex = 0;
            this.tbPgShipDetails.Text = resources.GetString("this.tbPgShipDetails.Text");
            this.tbPgShipDetails.UseVisualStyleBackColor = true;
            // 
            // tbPgShipMastery
            // 
            this.tbPgShipMastery.Controls.Add(this.pnlMastery);
            this.tbPgShipMastery.Location = new System.Drawing.Point(4, 22);
            this.tbPgShipMastery.Name = "tbPgShipMastery";
            this.tbPgShipMastery.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgShipMastery.Size = new System.Drawing.Size(230, 317);
            this.tbPgShipMastery.TabIndex = 1;
            this.tbPgShipMastery.Text = resources.GetString("this.tbPgShipMastery.Text");
            this.tbPgShipMastery.UseVisualStyleBackColor = true;
            // 
            // pnlMastery
            // 
            this.pnlMastery.Controls.Add(this.masteryTreeDisplayControl);
            this.pnlMastery.Controls.Add(this.tlStrpPlanTo);
            this.pnlMastery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMastery.Location = new System.Drawing.Point(3, 3);
            this.pnlMastery.Name = "pnlMastery";
            this.pnlMastery.Size = new System.Drawing.Size(224, 311);
            this.pnlMastery.TabIndex = 2;
            // 
            // masteryTreeDisplayControl
            // 
            this.masteryTreeDisplayControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masteryTreeDisplayControl.Location = new System.Drawing.Point(0, 25);
            this.masteryTreeDisplayControl.Name = "masteryTreeDisplayControl";
            this.masteryTreeDisplayControl.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.masteryTreeDisplayControl.Size = new System.Drawing.Size(224, 286);
            this.masteryTreeDisplayControl.TabIndex = 2;
            // 
            // tlStrpPlanTo
            // 
            this.tlStrpPlanTo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlStrpPlanTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbTextForEligibility,
            this.tslbEligible,
            this.tsPlanToMenu});
            this.tlStrpPlanTo.Location = new System.Drawing.Point(0, 0);
            this.tlStrpPlanTo.Name = "tlStrpPlanTo";
            this.tlStrpPlanTo.Size = new System.Drawing.Size(224, 25);
            this.tlStrpPlanTo.TabIndex = 1;
            this.tlStrpPlanTo.Text = resources.GetString("this.tlStrpPlanTo.Text");
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
            // tsPlanToMenu
            // 
            this.tsPlanToMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPlanToLevelOne,
            this.tsPlanToLevelTwo,
            this.tsPlanToLevelThree,
            this.tsPlanToLevelFour,
            this.tsPlanToLevelFive});
            this.tsPlanToMenu.Image = ((System.Drawing.Image)(resources.GetObject("tsPlanToMenu.Image")));
            this.tsPlanToMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPlanToMenu.Name = "tsPlanToMenu";
            this.tsPlanToMenu.Size = new System.Drawing.Size(84, 22);
            this.tsPlanToMenu.Text = resources.GetString("this.tsPlanToMenu.Text");
            // 
            // tsPlanToLevelOne
            // 
            this.tsPlanToLevelOne.Name = "tsPlanToLevelOne";
            this.tsPlanToLevelOne.Size = new System.Drawing.Size(114, 22);
            this.tsPlanToLevelOne.Text = resources.GetString("this.tsPlanToLevelOne.Text");
            this.tsPlanToLevelOne.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelTwo
            // 
            this.tsPlanToLevelTwo.Name = "tsPlanToLevelTwo";
            this.tsPlanToLevelTwo.Size = new System.Drawing.Size(114, 22);
            this.tsPlanToLevelTwo.Text = resources.GetString("this.tsPlanToLevelTwo.Text");
            this.tsPlanToLevelTwo.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelThree
            // 
            this.tsPlanToLevelThree.Name = "tsPlanToLevelThree";
            this.tsPlanToLevelThree.Size = new System.Drawing.Size(114, 22);
            this.tsPlanToLevelThree.Text = resources.GetString("this.tsPlanToLevelThree.Text");
            this.tsPlanToLevelThree.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelFour
            // 
            this.tsPlanToLevelFour.Name = "tsPlanToLevelFour";
            this.tsPlanToLevelFour.Size = new System.Drawing.Size(114, 22);
            this.tsPlanToLevelFour.Text = resources.GetString("this.tsPlanToLevelFour.Text");
            this.tsPlanToLevelFour.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // tsPlanToLevelFive
            // 
            this.tsPlanToLevelFive.Name = "tsPlanToLevelFive";
            this.tsPlanToLevelFive.Size = new System.Drawing.Size(114, 22);
            this.tsPlanToLevelFive.Text = resources.GetString("this.tsPlanToLevelFive.Text");
            this.tsPlanToLevelFive.Click += new System.EventHandler(this.tsPlanToLevel_Click);
            // 
            // splitButtonLoadouts
            // 
            this.splitButtonLoadouts.AutoSize = true;
            this.splitButtonLoadouts.ContextMenuStrip = this.LoadoutsProviderContextMenuStrip;
            this.splitButtonLoadouts.Location = new System.Drawing.Point(170, 44);
            this.splitButtonLoadouts.Name = "splitButtonLoadouts";
            this.splitButtonLoadouts.Size = new System.Drawing.Size(97, 23);
            this.splitButtonLoadouts.TabIndex = 12;
            this.splitButtonLoadouts.Text = resources.GetString("this.splitButtonLoadouts.Text");
            this.splitButtonLoadouts.UseVisualStyleBackColor = true;
            this.splitButtonLoadouts.Click += new System.EventHandler(this.splitButtonLoadouts_Click);
            // 
            // LoadoutsProviderContextMenuStrip
            // 
            this.LoadoutsProviderContextMenuStrip.Name = "LoadoutsProviderContextMenuStrip";
            this.LoadoutsProviderContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // lblViewLoadouts
            // 
            this.lblViewLoadouts.AutoSize = true;
            this.lblViewLoadouts.Location = new System.Drawing.Point(70, 49);
            this.lblViewLoadouts.Name = "lblViewLoadouts";
            this.lblViewLoadouts.Size = new System.Drawing.Size(99, 13);
            this.lblViewLoadouts.TabIndex = 13;
            this.lblViewLoadouts.Text = resources.GetString("this.lblViewLoadouts.Text");
            // 
            // showInMenuSeparator
            // 
            this.showInMenuSeparator.Name = "showInMenuSeparator";
            this.showInMenuSeparator.Size = new System.Drawing.Size(185, 6);
            // 
            // showInSkillBrowser
            // 
            this.showInSkillBrowser.Name = "showInSkillBrowser";
            this.showInSkillBrowser.Size = new System.Drawing.Size(188, 22);
            this.showInSkillBrowser.Text = resources.GetString("this.showInSkillBrowser.Text");
            this.showInSkillBrowser.Click += new System.EventHandler(this.propertiesList_DoubleClick);
            // 
            // showInItemBrowser
            // 
            this.showInItemBrowser.Name = "showInItemBrowser";
            this.showInItemBrowser.Size = new System.Drawing.Size(188, 22);
            this.showInItemBrowser.Text = resources.GetString("this.showInItemBrowser.Text");
            this.showInItemBrowser.Click += new System.EventHandler(this.propertiesList_DoubleClick);
            // 
            // ShipBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ShipBrowserControl";
            this.Size = new System.Drawing.Size(765, 512);
            this.Controls.SetChildIndex(this.scObjectBrowser, 0);
            this.scObjectBrowser.Panel1.ResumeLayout(false);
            this.scObjectBrowser.Panel1.PerformLayout();
            this.scObjectBrowser.Panel2.ResumeLayout(false);
            this.scObjectBrowser.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scObjectBrowser)).EndInit();
            this.scObjectBrowser.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlBrowserHeader.ResumeLayout(false);
            this.pnlBrowserHeader.PerformLayout();
            this.scDetailsRight.Panel1.ResumeLayout(false);
            this.scDetailsRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDetailsRight)).EndInit();
            this.scDetailsRight.ResumeLayout(false);
            this.scDetails.Panel1.ResumeLayout(false);
            this.scDetails.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDetails)).EndInit();
            this.scDetails.ResumeLayout(false);
            this.gbDescription.ResumeLayout(false);
            this.gbAttributes.ResumeLayout(false);
            this.ShipAttributeContextMenu.ResumeLayout(false);
            this.gbRequiredSkills.ResumeLayout(false);
            this.tbCntrlShipInformation.ResumeLayout(false);
            this.tbPgShipDetails.ResumeLayout(false);
            this.tbPgShipMastery.ResumeLayout(false);
            this.pnlMastery.ResumeLayout(false);
            this.pnlMastery.PerformLayout();
            this.tlStrpPlanTo.ResumeLayout(false);
            this.tlStrpPlanTo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvShipProperties;
        private System.Windows.Forms.ColumnHeader chAttribute;
        private System.Windows.Forms.ColumnHeader chValue;
        private ShipSelectControl shipSelectControl;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.ContextMenuStrip ShipAttributeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbRequiredSkills;
        private RequiredSkillsControl requiredSkillsControl;
        private System.Windows.Forms.TabControl tbCntrlShipInformation;
        private System.Windows.Forms.TabPage tbPgShipDetails;
        private System.Windows.Forms.TabPage tbPgShipMastery;
        private System.Windows.Forms.ToolStrip tlStrpPlanTo;
        private System.Windows.Forms.ToolStripDropDownButton tsPlanToMenu;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelOne;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelTwo;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelThree;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelFour;
        private System.Windows.Forms.ToolStripMenuItem tsPlanToLevelFive;
        private System.Windows.Forms.Panel pnlMastery;
        private MasteryTreeDisplayControl masteryTreeDisplayControl;
        private System.Windows.Forms.ToolStripLabel tslbTextForEligibility;
        private System.Windows.Forms.ToolStripLabel tslbEligible;
        private Common.Controls.SplitButton splitButtonLoadouts;
        private System.Windows.Forms.Label lblViewLoadouts;
        private System.Windows.Forms.ContextMenuStrip LoadoutsProviderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showInSkillBrowser;
        private System.Windows.Forms.ToolStripMenuItem showInItemBrowser;
        private System.Windows.Forms.ToolStripSeparator showInMenuSeparator;
    }
}
