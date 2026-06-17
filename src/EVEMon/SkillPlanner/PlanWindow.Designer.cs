namespace EVEMon.SkillPlanner
{
    partial class PlanWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanWindow));
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.ObsoleteEntriesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SkillsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CostStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SkillPointsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.upperToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsddbPlans = new System.Windows.Forms.ToolStripDropDownButton();
            this.newPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPlanFromSkillQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiExportPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAfterPlanCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDeletePlan = new System.Windows.Forms.ToolStripButton();
            this.tsbPrintPlan = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImplantCalculator = new System.Windows.Forms.ToolStripButton();
            this.attributesOptimizerStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadoutImport = new System.Windows.Forms.ToolStripButton();
            this.tsbClipboardImport = new System.Windows.Forms.ToolStripButton();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpPlanEditor = new System.Windows.Forms.TabPage();
            this.planEditor = new EVEMon.SkillPlanner.PlanEditorControl();
            this.tpSkillBrowser = new System.Windows.Forms.TabPage();
            this.skillBrowser = new EVEMon.SkillPlanner.SkillBrowserControl();
            this.tpCertificateBrowser = new System.Windows.Forms.TabPage();
            this.certBrowser = new EVEMon.SkillPlanner.CertificateBrowserControl();
            this.tpShipBrowser = new System.Windows.Forms.TabPage();
            this.shipBrowser = new EVEMon.SkillPlanner.ShipBrowserControl();
            this.tpItemBrowser = new System.Windows.Forms.TabPage();
            this.itemBrowser = new EVEMon.SkillPlanner.ItemBrowserControl();
            this.tpBlueprintBrowser = new System.Windows.Forms.TabPage();
            this.blueprintBrowser = new EVEMon.SkillPlanner.BlueprintBrowserControl();
            this.ilTabIcons = new System.Windows.Forms.ImageList(this.components);
            this.MainStatusStrip.SuspendLayout();
            this.upperToolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpPlanEditor.SuspendLayout();
            this.tpSkillBrowser.SuspendLayout();
            this.tpCertificateBrowser.SuspendLayout();
            this.tpShipBrowser.SuspendLayout();
            this.tpItemBrowser.SuspendLayout();
            this.tpBlueprintBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ObsoleteEntriesStatusLabel,
            this.SkillsStatusLabel,
            this.TimeStatusLabel,
            this.CostStatusLabel,
            this.SkillPointsStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 540);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.ShowItemToolTips = true;
            this.MainStatusStrip.Size = new System.Drawing.Size(943, 22);
            this.MainStatusStrip.TabIndex = 1;
            // 
            // ObsoleteEntriesStatusLabel
            // 
            this.ObsoleteEntriesStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("ObsoleteEntriesStatusLabel.Image")));
            this.ObsoleteEntriesStatusLabel.IsLink = true;
            this.ObsoleteEntriesStatusLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.ObsoleteEntriesStatusLabel.Name = "ObsoleteEntriesStatusLabel";
            this.ObsoleteEntriesStatusLabel.Size = new System.Drawing.Size(117, 17);
            this.ObsoleteEntriesStatusLabel.Text = resources.GetString("this.ObsoleteEntriesStatusLabel.Text");
            this.ObsoleteEntriesStatusLabel.Visible = false;
            this.ObsoleteEntriesStatusLabel.Click += new System.EventHandler(this.obsoleteEntriesToolStripStatusLabel_Click);
            // 
            // SkillsStatusLabel
            // 
            this.SkillsStatusLabel.AutoToolTip = true;
            this.SkillsStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("SkillsStatusLabel.Image")));
            this.SkillsStatusLabel.Name = "SkillsStatusLabel";
            this.SkillsStatusLabel.Size = new System.Drawing.Size(104, 17);
            this.SkillsStatusLabel.Text = resources.GetString("this.SkillsStatusLabel.Text");
            this.SkillsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimeStatusLabel
            // 
            this.TimeStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("TimeStatusLabel.Image")));
            this.TimeStatusLabel.Name = "TimeStatusLabel";
            this.TimeStatusLabel.Size = new System.Drawing.Size(116, 17);
            this.TimeStatusLabel.Text = resources.GetString("this.TimeStatusLabel.Text");
            this.TimeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CostStatusLabel
            // 
            this.CostStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("CostStatusLabel.Image")));
            this.CostStatusLabel.Name = "CostStatusLabel";
            this.CostStatusLabel.Size = new System.Drawing.Size(98, 17);
            this.CostStatusLabel.Text = resources.GetString("this.CostStatusLabel.Text");
            this.CostStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SkillPointsStatusLabel
            // 
            this.SkillPointsStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("SkillPointsStatusLabel.Image")));
            this.SkillPointsStatusLabel.Name = "SkillPointsStatusLabel";
            this.SkillPointsStatusLabel.Size = new System.Drawing.Size(95, 17);
            this.SkillPointsStatusLabel.Text = resources.GetString("this.SkillPointsStatusLabel.Text");
            this.SkillPointsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // upperToolStrip
            // 
            this.upperToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.upperToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbPlans,
            this.tsddbSave,
            this.tsbDeletePlan,
            this.tsbPrintPlan,
            this.tsbCopyToClipboard,
            this.toolStripSeparator1,
            this.tsbImplantCalculator,
            this.attributesOptimizerStripButton,
            this.toolStripSeparator2,
            this.tsbLoadoutImport,
            this.tsbClipboardImport});
            this.upperToolStrip.Location = new System.Drawing.Point(0, 0);
            this.upperToolStrip.Name = "upperToolStrip";
            this.upperToolStrip.Size = new System.Drawing.Size(943, 25);
            this.upperToolStrip.TabIndex = 3;
            this.upperToolStrip.Text = resources.GetString("this.upperToolStrip.Text");
            // 
            // tsddbPlans
            // 
            this.tsddbPlans.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlanToolStripMenuItem,
            this.createPlanFromSkillQueueToolStripMenuItem,
            this.toolStripSeparator3});
            this.tsddbPlans.Image = ((System.Drawing.Image)(resources.GetObject("tsddbPlans.Image")));
            this.tsddbPlans.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsddbPlans.Name = "tsddbPlans";
            this.tsddbPlans.Size = new System.Drawing.Size(93, 22);
            this.tsddbPlans.Text = resources.GetString("this.tsddbPlans.Text");
            this.tsddbPlans.DropDownOpening += new System.EventHandler(this.tsddbPlans_DropDownOpening);
            this.tsddbPlans.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbPlans_DropDownItemClicked);
            // 
            // newPlanToolStripMenuItem
            // 
            this.newPlanToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newPlanToolStripMenuItem.Image")));
            this.newPlanToolStripMenuItem.Name = "newPlanToolStripMenuItem";
            this.newPlanToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.newPlanToolStripMenuItem.Text = resources.GetString("this.newPlanToolStripMenuItem.Text");
            this.newPlanToolStripMenuItem.Click += new System.EventHandler(this.newPlanToolStripMenuItem_Click);
            // 
            // createPlanFromSkillQueueToolStripMenuItem
            // 
            this.createPlanFromSkillQueueToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("createPlanFromSkillQueueToolStripMenuItem.Image")));
            this.createPlanFromSkillQueueToolStripMenuItem.Name = "createPlanFromSkillQueueToolStripMenuItem";
            this.createPlanFromSkillQueueToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.createPlanFromSkillQueueToolStripMenuItem.Text = resources.GetString("this.createPlanFromSkillQueueToolStripMenuItem.Text");
            this.createPlanFromSkillQueueToolStripMenuItem.Click += new System.EventHandler(this.createPlanFromSkillQueueToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // tsddbSave
            // 
            this.tsddbSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportPlan,
            this.tsmiAfterPlanCharacter});
            this.tsddbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsddbSave.Image")));
            this.tsddbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbSave.Name = "tsddbSave";
            this.tsddbSave.Size = new System.Drawing.Size(69, 22);
            this.tsddbSave.Text = resources.GetString("this.tsddbSave.Text");
            // 
            // tsmiExportPlan
            // 
            this.tsmiExportPlan.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExportPlan.Image")));
            this.tsmiExportPlan.Name = "tsmiExportPlan";
            this.tsmiExportPlan.Size = new System.Drawing.Size(189, 22);
            this.tsmiExportPlan.Text = resources.GetString("this.tsmiExportPlan.Text");
            this.tsmiExportPlan.Click += new System.EventHandler(this.tsmiExportPlan_Click);
            // 
            // tsmiAfterPlanCharacter
            // 
            this.tsmiAfterPlanCharacter.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAfterPlanCharacter.Image")));
            this.tsmiAfterPlanCharacter.Name = "tsmiAfterPlanCharacter";
            this.tsmiAfterPlanCharacter.Size = new System.Drawing.Size(189, 22);
            this.tsmiAfterPlanCharacter.Text = resources.GetString("this.tsmiAfterPlanCharacter.Text");
            this.tsmiAfterPlanCharacter.Click += new System.EventHandler(this.tsmiAfterPlanCharacter_Click);
            // 
            // tsbDeletePlan
            // 
            this.tsbDeletePlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeletePlan.Image")));
            this.tsbDeletePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeletePlan.Name = "tsbDeletePlan";
            this.tsbDeletePlan.Size = new System.Drawing.Size(69, 22);
            this.tsbDeletePlan.Text = resources.GetString("this.tsbDeletePlan.Text");
            this.tsbDeletePlan.Click += new System.EventHandler(this.tsbDeletePlan_Click);
            // 
            // tsbPrintPlan
            // 
            this.tsbPrintPlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrintPlan.Image")));
            this.tsbPrintPlan.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbPrintPlan.Name = "tsbPrintPlan";
            this.tsbPrintPlan.Size = new System.Drawing.Size(61, 22);
            this.tsbPrintPlan.Text = resources.GetString("this.tsbPrintPlan.Text");
            this.tsbPrintPlan.ToolTipText = resources.GetString("this.tsbPrintPlan.ToolTipText");
            this.tsbPrintPlan.Click += new System.EventHandler(this.tsbPrintPlan_Click);
            // 
            // tsbCopyToClipboard
            // 
            this.tsbCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyToClipboard.Image")));
            this.tsbCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyToClipboard.Name = "tsbCopyToClipboard";
            this.tsbCopyToClipboard.Size = new System.Drawing.Size(133, 22);
            this.tsbCopyToClipboard.Text = resources.GetString("this.tsbCopyToClipboard.Text");
            this.tsbCopyToClipboard.Click += new System.EventHandler(this.tsbCopyToClipboard_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImplantCalculator
            // 
            this.tsbImplantCalculator.Image = ((System.Drawing.Image)(resources.GetObject("tsbImplantCalculator.Image")));
            this.tsbImplantCalculator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImplantCalculator.Name = "tsbImplantCalculator";
            this.tsbImplantCalculator.Size = new System.Drawing.Size(103, 22);
            this.tsbImplantCalculator.Text = resources.GetString("this.tsbImplantCalculator.Text");
            this.tsbImplantCalculator.Click += new System.EventHandler(this.tsbImplantCalculator_Click);
            // 
            // attributesOptimizerStripButton
            // 
            this.attributesOptimizerStripButton.Image = ((System.Drawing.Image)(resources.GetObject("attributesOptimizerStripButton.Image")));
            this.attributesOptimizerStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.attributesOptimizerStripButton.Name = "attributesOptimizerStripButton";
            this.attributesOptimizerStripButton.Size = new System.Drawing.Size(143, 22);
            this.attributesOptimizerStripButton.Text = resources.GetString("this.attributesOptimizerStripButton.Text");
            this.attributesOptimizerStripButton.Click += new System.EventHandler(this.tsbAttributesOptimizer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLoadoutImport
            // 
            this.tsbLoadoutImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadoutImport.Image")));
            this.tsbLoadoutImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadoutImport.Name = "tsbLoadoutImport";
            this.tsbLoadoutImport.Size = new System.Drawing.Size(119, 22);
            this.tsbLoadoutImport.Text = resources.GetString("this.tsbLoadoutImport.Text");
            this.tsbLoadoutImport.Click += new System.EventHandler(this.tsbLoadoutImport_Click);
            // 
            // tsbClipboardImport
            // 
            this.tsbClipboardImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbClipboardImport.Image")));
            this.tsbClipboardImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardImport.Name = "tsbClipboardImport";
            this.tsbClipboardImport.Size = new System.Drawing.Size(118, 22);
            this.tsbClipboardImport.Text = resources.GetString("this.tsbClipboardImport.Text");
            this.tsbClipboardImport.ToolTipText = resources.GetString("this.tsbClipboardImport.ToolTipText");
            this.tsbClipboardImport.Click += new System.EventHandler(this.tsbClipboardImport_Click);
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 5000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.ReshowDelay = 100;
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "EVEMon Plan Format (*.emp)|*.emp|XML  Format (*.xml)|*.xml|Text Format (*.txt)|*." +
    "txt";
            this.sfdSave.Title = resources.GetString("this.sfdSave.Title");
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpPlanEditor);
            this.tabControl.Controls.Add(this.tpSkillBrowser);
            this.tabControl.Controls.Add(this.tpCertificateBrowser);
            this.tabControl.Controls.Add(this.tpShipBrowser);
            this.tabControl.Controls.Add(this.tpItemBrowser);
            this.tabControl.Controls.Add(this.tpBlueprintBrowser);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.ilTabIcons;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(943, 515);
            this.tabControl.TabIndex = 4;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tpPlanEditor
            // 
            this.tpPlanEditor.Controls.Add(this.planEditor);
            this.tpPlanEditor.ImageIndex = 0;
            this.tpPlanEditor.Location = new System.Drawing.Point(4, 31);
            this.tpPlanEditor.Margin = new System.Windows.Forms.Padding(0);
            this.tpPlanEditor.Name = "tpPlanEditor";
            this.tpPlanEditor.Size = new System.Drawing.Size(935, 480);
            this.tpPlanEditor.TabIndex = 1;
            this.tpPlanEditor.Text = resources.GetString("this.tpPlanEditor.Text");
            this.tpPlanEditor.UseVisualStyleBackColor = true;
            // 
            // planEditor
            // 
            this.planEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planEditor.Location = new System.Drawing.Point(0, 0);
            this.planEditor.Name = "planEditor";
            this.planEditor.Size = new System.Drawing.Size(935, 480);
            this.planEditor.TabIndex = 2;
            this.planEditor.TabIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tpSkillBrowser
            // 
            this.tpSkillBrowser.Controls.Add(this.skillBrowser);
            this.tpSkillBrowser.ImageIndex = 1;
            this.tpSkillBrowser.Location = new System.Drawing.Point(4, 31);
            this.tpSkillBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.tpSkillBrowser.Name = "tpSkillBrowser";
            this.tpSkillBrowser.Size = new System.Drawing.Size(935, 480);
            this.tpSkillBrowser.TabIndex = 0;
            this.tpSkillBrowser.Text = resources.GetString("this.tpSkillBrowser.Text");
            this.tpSkillBrowser.UseVisualStyleBackColor = true;
            // 
            // skillBrowser
            // 
            this.skillBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillBrowser.Location = new System.Drawing.Point(0, 0);
            this.skillBrowser.Name = "skillBrowser";
            this.skillBrowser.Size = new System.Drawing.Size(935, 480);
            this.skillBrowser.TabIndex = 0;
            // 
            // tpCertificateBrowser
            // 
            this.tpCertificateBrowser.Controls.Add(this.certBrowser);
            this.tpCertificateBrowser.ImageIndex = 5;
            this.tpCertificateBrowser.Location = new System.Drawing.Point(4, 31);
            this.tpCertificateBrowser.Name = "tpCertificateBrowser";
            this.tpCertificateBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tpCertificateBrowser.Size = new System.Drawing.Size(935, 480);
            this.tpCertificateBrowser.TabIndex = 6;
            this.tpCertificateBrowser.Text = resources.GetString("this.tpCertificateBrowser.Text");
            this.tpCertificateBrowser.UseVisualStyleBackColor = true;
            // 
            // certBrowser
            // 
            this.certBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.certBrowser.Location = new System.Drawing.Point(3, 3);
            this.certBrowser.Name = "certBrowser";
            this.certBrowser.Size = new System.Drawing.Size(929, 474);
            this.certBrowser.TabIndex = 0;
            // 
            // tpShipBrowser
            // 
            this.tpShipBrowser.Controls.Add(this.shipBrowser);
            this.tpShipBrowser.ImageIndex = 2;
            this.tpShipBrowser.Location = new System.Drawing.Point(4, 31);
            this.tpShipBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.tpShipBrowser.Name = "tpShipBrowser";
            this.tpShipBrowser.Size = new System.Drawing.Size(935, 480);
            this.tpShipBrowser.TabIndex = 2;
            this.tpShipBrowser.Text = resources.GetString("this.tpShipBrowser.Text");
            this.tpShipBrowser.UseVisualStyleBackColor = true;
            // 
            // shipBrowser
            // 
            this.shipBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shipBrowser.Location = new System.Drawing.Point(0, 0);
            this.shipBrowser.Name = "shipBrowser";
            this.shipBrowser.Size = new System.Drawing.Size(935, 480);
            this.shipBrowser.TabIndex = 0;
            // 
            // tpItemBrowser
            // 
            this.tpItemBrowser.Controls.Add(this.itemBrowser);
            this.tpItemBrowser.ImageIndex = 3;
            this.tpItemBrowser.Location = new System.Drawing.Point(4, 31);
            this.tpItemBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.tpItemBrowser.Name = "tpItemBrowser";
            this.tpItemBrowser.Size = new System.Drawing.Size(935, 480);
            this.tpItemBrowser.TabIndex = 3;
            this.tpItemBrowser.Text = resources.GetString("this.tpItemBrowser.Text");
            this.tpItemBrowser.UseVisualStyleBackColor = true;
            // 
            // itemBrowser
            // 
            this.itemBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemBrowser.Location = new System.Drawing.Point(0, 0);
            this.itemBrowser.Name = "itemBrowser";
            this.itemBrowser.Size = new System.Drawing.Size(935, 480);
            this.itemBrowser.TabIndex = 0;
            // 
            // tpBlueprintBrowser
            // 
            this.tpBlueprintBrowser.Controls.Add(this.blueprintBrowser);
            this.tpBlueprintBrowser.ImageIndex = 4;
            this.tpBlueprintBrowser.Location = new System.Drawing.Point(4, 31);
            this.tpBlueprintBrowser.Name = "tpBlueprintBrowser";
            this.tpBlueprintBrowser.Size = new System.Drawing.Size(935, 480);
            this.tpBlueprintBrowser.TabIndex = 5;
            this.tpBlueprintBrowser.Text = resources.GetString("this.tpBlueprintBrowser.Text");
            this.tpBlueprintBrowser.UseVisualStyleBackColor = true;
            // 
            // blueprintBrowser
            // 
            this.blueprintBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintBrowser.Location = new System.Drawing.Point(0, 0);
            this.blueprintBrowser.Name = "blueprintBrowser";
            this.blueprintBrowser.Size = new System.Drawing.Size(935, 480);
            this.blueprintBrowser.TabIndex = 0;
            // 
            // ilTabIcons
            // 
            this.ilTabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTabIcons.ImageStream")));
            this.ilTabIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTabIcons.Images.SetKeyName(0, "Plan");
            this.ilTabIcons.Images.SetKeyName(1, "Skill");
            this.ilTabIcons.Images.SetKeyName(2, "Ships");
            this.ilTabIcons.Images.SetKeyName(3, "Items");
            this.ilTabIcons.Images.SetKeyName(4, "Blueprint");
            this.ilTabIcons.Images.SetKeyName(5, "Certificate");
            // 
            // PlanWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 562);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.upperToolStrip);
            this.Controls.Add(this.MainStatusStrip);
            this.MinimumSize = new System.Drawing.Size(780, 350);
            this.Name = "PlanWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("this.Text");
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.upperToolStrip.ResumeLayout(false);
            this.upperToolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tpPlanEditor.ResumeLayout(false);
            this.tpSkillBrowser.ResumeLayout(false);
            this.tpCertificateBrowser.ResumeLayout(false);
            this.tpShipBrowser.ResumeLayout(false);
            this.tpItemBrowser.ResumeLayout(false);
            this.tpBlueprintBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel SkillsStatusLabel;
        private System.Windows.Forms.ToolStrip upperToolStrip;
        private System.Windows.Forms.ToolStripButton tsbDeletePlan;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.ToolStripButton tsbCopyToClipboard;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbImplantCalculator;
        private System.Windows.Forms.TabPage tpItemBrowser;
        private ItemBrowserControl itemBrowser;
        private System.Windows.Forms.TabPage tpPlanEditor;
        private PlanEditorControl planEditor;
        private System.Windows.Forms.TabPage tpShipBrowser;
        private ShipBrowserControl shipBrowser;
        private System.Windows.Forms.TabPage tpSkillBrowser;
        private SkillBrowserControl skillBrowser;
        private System.Windows.Forms.ToolStripDropDownButton tsddbPlans;
        private System.Windows.Forms.ToolStripButton tsbPrintPlan;
        private System.Windows.Forms.ToolStripDropDownButton tsddbSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiAfterPlanCharacter;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportPlan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton attributesOptimizerStripButton;
        private System.Windows.Forms.ImageList ilTabIcons;
        private System.Windows.Forms.ToolStripButton tsbLoadoutImport;
        private System.Windows.Forms.ToolStripStatusLabel ObsoleteEntriesStatusLabel;
        private System.Windows.Forms.TabPage tpBlueprintBrowser;
        private BlueprintBrowserControl blueprintBrowser;
        private System.Windows.Forms.ToolStripStatusLabel CostStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel TimeStatusLabel;
        private System.Windows.Forms.TabPage tpCertificateBrowser;
        private CertificateBrowserControl certBrowser;
        private System.Windows.Forms.ToolStripStatusLabel SkillPointsStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem newPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPlanFromSkillQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbClipboardImport;
    }
}
