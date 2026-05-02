using EVEMon.Common.Controls;

namespace EVEMon.SkillPlanner
{
    partial class PlanManagementWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanManagementWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ofdOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbPlanList = new EVEMon.Common.Controls.DraggableListView();
            this.PlanName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlanDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlanSkills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlanDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cmiRenameEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.NewPlanToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miImportPlanFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportPlanFromCharacter = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportExportPlanToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miExportPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportCharacterSkillsAsPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportCharAsPlanToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miRestorePlans = new System.Windows.Forms.ToolStripMenuItem();
            this.miSavePlans = new System.Windows.Forms.ToolStripMenuItem();
            this.mEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miRenameEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.ButtonsFlowLayoutPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("this.label1.Text");
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(84, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = resources.GetString("this.btnClose.Text");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ofdOpenDialog
            // 
            this.ofdOpenDialog.Filter = resources.GetString("this.ofdOpenDialog.Filter");
            this.ofdOpenDialog.Title = resources.GetString("this.ofdOpenDialog.Title");
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lbPlanList);
            this.panel1.Controls.Add(this.toolStrip);
            this.panel1.Location = new System.Drawing.Point(9, 22);
            this.panel1.MinimumSize = new System.Drawing.Size(305, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 327);
            this.panel1.TabIndex = 6;
            // 
            // lbPlanList
            // 
            this.lbPlanList.AllowDrop = true;
            this.lbPlanList.AllowRowReorder = true;
            this.lbPlanList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlanName,
            this.PlanDate,
            this.PlanSkills,
            this.PlanDescription});
            this.lbPlanList.ContextMenuStrip = this.contextMenu;
            this.lbPlanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlanList.FullRowSelect = true;
            this.lbPlanList.HideSelection = false;
            this.lbPlanList.Location = new System.Drawing.Point(0, 0);
            this.lbPlanList.Name = "lbPlanList";
            this.lbPlanList.Size = new System.Drawing.Size(606, 327);
            this.lbPlanList.TabIndex = 2;
            this.lbPlanList.UseCompatibleStateImageBehavior = false;
            this.lbPlanList.View = System.Windows.Forms.View.Details;
            this.lbPlanList.ListViewItemsDragged += new System.EventHandler<System.EventArgs>(this.lbPlanList_ListViewItemsDragged);
            this.lbPlanList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lbPlanList_ColumnClick);
            this.lbPlanList.SelectedIndexChanged += new System.EventHandler(this.lbPlanList_SelectedIndexChanged);
            this.lbPlanList.DoubleClick += new System.EventHandler(this.lbPlanList_DoubleClick);
            this.lbPlanList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbPlanList_MouseDown);
            this.lbPlanList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbPlanList_MouseMove);
            // 
            // PlanName
            // 
            this.PlanName.Text = resources.GetString("this.PlanName.Text");
            this.PlanName.Width = 176;
            // 
            // PlanDate
            // 
            this.PlanDate.Text = resources.GetString("this.PlanDate.Text");
            this.PlanDate.Width = 197;
            // 
            // PlanSkills
            // 
            this.PlanSkills.Text = resources.GetString("this.PlanSkills.Text");
            // 
            // PlanDescription
            // 
            this.PlanDescription.Text = resources.GetString("this.PlanDescription.Text");
            this.PlanDescription.Width = 170;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiOpen,
            this.cmiExport,
            this.toolStripSeparator,
            this.cmiRenameEdit,
            this.cmiDelete});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(127, 98);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // cmiOpen
            // 
            this.cmiOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmiOpen.Image")));
            this.cmiOpen.Name = "cmiOpen";
            this.cmiOpen.Size = new System.Drawing.Size(126, 22);
            this.cmiOpen.Text = resources.GetString("this.cmiOpen.Text");
            this.cmiOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cmiExport
            // 
            this.cmiExport.Image = ((System.Drawing.Image)(resources.GetObject("cmiExport.Image")));
            this.cmiExport.Name = "cmiExport";
            this.cmiExport.Size = new System.Drawing.Size(126, 22);
            this.cmiExport.Text = resources.GetString("this.cmiExport.Text");
            this.cmiExport.Click += new System.EventHandler(this.miExportPlan_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(123, 6);
            // 
            // cmiRenameEdit
            // 
            this.cmiRenameEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmiRenameEdit.Image")));
            this.cmiRenameEdit.Name = "cmiRenameEdit";
            this.cmiRenameEdit.Size = new System.Drawing.Size(126, 22);
            this.cmiRenameEdit.Text = resources.GetString("this.cmiRenameEdit.Text");
            this.cmiRenameEdit.Click += new System.EventHandler(this.miRenameEdit_Click);
            // 
            // cmiDelete
            // 
            this.cmiDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmiDelete.Image")));
            this.cmiDelete.Name = "cmiDelete";
            this.cmiDelete.Size = new System.Drawing.Size(126, 22);
            this.cmiDelete.Text = resources.GetString("this.cmiDelete.Text");
            this.cmiDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsbMoveUp,
            this.tsbMoveDown});
            this.toolStrip.Location = new System.Drawing.Point(606, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(41, 327);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = resources.GetString("this.toolStrip.Text");
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(38, 15);
            this.toolStripLabel1.Text = resources.GetString("this.toolStripLabel1.Text");
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Enabled = false;
            this.tsbMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveUp.Image")));
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(38, 20);
            this.tsbMoveUp.Text = resources.GetString("this.tsbMoveUp.Text");
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMoveDown.Enabled = false;
            this.tsbMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveDown.Image")));
            this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new System.Drawing.Size(38, 20);
            this.tsbMoveDown.Text = resources.GetString("this.tsbMoveDown.Text");
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.ButtonsFlowLayoutPanel, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(6, 6, 0, 12);
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(659, 401);
            this.tableLayoutPanel.TabIndex = 8;
            // 
            // ButtonsFlowLayoutPanel
            // 
            this.ButtonsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonsFlowLayoutPanel.AutoSize = true;
            this.ButtonsFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonsFlowLayoutPanel.Controls.Add(this.btnClose);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.btnOpen);
            this.ButtonsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.ButtonsFlowLayoutPanel.Location = new System.Drawing.Point(491, 360);
            this.ButtonsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0, 8, 6, 0);
            this.ButtonsFlowLayoutPanel.Name = "ButtonsFlowLayoutPanel";
            this.ButtonsFlowLayoutPanel.Size = new System.Drawing.Size(162, 29);
            this.ButtonsFlowLayoutPanel.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOpen.Enabled = false;
            this.ButtonsFlowLayoutPanel.SetFlowBreak(this.btnOpen, true);
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = resources.GetString("this.btnOpen.Text");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile,
            this.mEdit});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(659, 24);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = resources.GetString("this.menuStrip.Text");
            // 
            // mFile
            // 
            this.mFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewPlan,
            this.NewPlanToolStripSeparator,
            this.miImportPlanFromFile,
            this.miImportPlanFromCharacter,
            this.ImportExportPlanToolStripSeparator,
            this.miExportPlan,
            this.miExportCharacterSkillsAsPlan,
            this.ExportCharAsPlanToolStripSeparator,
            this.miRestorePlans,
            this.miSavePlans});
            this.mFile.Name = "mFile";
            this.mFile.Size = new System.Drawing.Size(37, 20);
            this.mFile.Text = resources.GetString("this.mFile.Text");
            this.mFile.DropDownOpening += new System.EventHandler(this.mFile_DropDownOpening);
            // 
            // miNewPlan
            // 
            this.miNewPlan.Image = ((System.Drawing.Image)(resources.GetObject("miNewPlan.Image")));
            this.miNewPlan.Name = "miNewPlan";
            this.miNewPlan.Size = new System.Drawing.Size(239, 22);
            this.miNewPlan.Text = resources.GetString("this.miNewPlan.Text");
            this.miNewPlan.Click += new System.EventHandler(this.miNewPlan_Click);
            // 
            // NewPlanToolStripSeparator
            // 
            this.NewPlanToolStripSeparator.Name = "NewPlanToolStripSeparator";
            this.NewPlanToolStripSeparator.Size = new System.Drawing.Size(236, 6);
            // 
            // miImportPlanFromFile
            // 
            this.miImportPlanFromFile.Image = ((System.Drawing.Image)(resources.GetObject("miImportPlanFromFile.Image")));
            this.miImportPlanFromFile.Name = "miImportPlanFromFile";
            this.miImportPlanFromFile.Size = new System.Drawing.Size(239, 22);
            this.miImportPlanFromFile.Text = resources.GetString("this.miImportPlanFromFile.Text");
            this.miImportPlanFromFile.Click += new System.EventHandler(this.miImportPlanFromFile_Click);
            // 
            // miImportPlanFromCharacter
            // 
            this.miImportPlanFromCharacter.Image = ((System.Drawing.Image)(resources.GetObject("miImportPlanFromCharacter.Image")));
            this.miImportPlanFromCharacter.Name = "miImportPlanFromCharacter";
            this.miImportPlanFromCharacter.Size = new System.Drawing.Size(239, 22);
            this.miImportPlanFromCharacter.Text = resources.GetString("this.miImportPlanFromCharacter.Text");
            this.miImportPlanFromCharacter.Click += new System.EventHandler(this.miImportPlanFromCharacter_Click);
            // 
            // ImportExportPlanToolStripSeparator
            // 
            this.ImportExportPlanToolStripSeparator.Name = "ImportExportPlanToolStripSeparator";
            this.ImportExportPlanToolStripSeparator.Size = new System.Drawing.Size(236, 6);
            // 
            // miExportPlan
            // 
            this.miExportPlan.Image = ((System.Drawing.Image)(resources.GetObject("miExportPlan.Image")));
            this.miExportPlan.Name = "miExportPlan";
            this.miExportPlan.Size = new System.Drawing.Size(239, 22);
            this.miExportPlan.Text = resources.GetString("this.miExportPlan.Text");
            this.miExportPlan.Click += new System.EventHandler(this.miExportPlan_Click);
            // 
            // miExportCharacterSkillsAsPlan
            // 
            this.miExportCharacterSkillsAsPlan.Image = ((System.Drawing.Image)(resources.GetObject("miExportCharacterSkillsAsPlan.Image")));
            this.miExportCharacterSkillsAsPlan.Name = "miExportCharacterSkillsAsPlan";
            this.miExportCharacterSkillsAsPlan.Size = new System.Drawing.Size(239, 22);
            this.miExportCharacterSkillsAsPlan.Text = resources.GetString("this.miExportCharacterSkillsAsPlan.Text");
            this.miExportCharacterSkillsAsPlan.Click += new System.EventHandler(this.miExportCharacterSkillsAsPlan_Click);
            // 
            // ExportCharAsPlanToolStripSeparator
            // 
            this.ExportCharAsPlanToolStripSeparator.Name = "ExportCharAsPlanToolStripSeparator";
            this.ExportCharAsPlanToolStripSeparator.Size = new System.Drawing.Size(236, 6);
            // 
            // miRestorePlans
            // 
            this.miRestorePlans.Image = ((System.Drawing.Image)(resources.GetObject("miRestorePlans.Image")));
            this.miRestorePlans.Name = "miRestorePlans";
            this.miRestorePlans.Size = new System.Drawing.Size(239, 22);
            this.miRestorePlans.Text = resources.GetString("this.miRestorePlans.Text");
            this.miRestorePlans.Click += new System.EventHandler(this.miRestorePlans_Click);
            // 
            // miSavePlans
            // 
            this.miSavePlans.Image = ((System.Drawing.Image)(resources.GetObject("miSavePlans.Image")));
            this.miSavePlans.Name = "miSavePlans";
            this.miSavePlans.Size = new System.Drawing.Size(239, 22);
            this.miSavePlans.Text = resources.GetString("this.miSavePlans.Text");
            this.miSavePlans.Click += new System.EventHandler(this.miSavePlans_Click);
            // 
            // mEdit
            // 
            this.mEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRenameEdit,
            this.miDelete});
            this.mEdit.Name = "mEdit";
            this.mEdit.Size = new System.Drawing.Size(39, 20);
            this.mEdit.Text = resources.GetString("this.mEdit.Text");
            this.mEdit.DropDownOpening += new System.EventHandler(this.mEdit_DropDownOpening);
            // 
            // miRenameEdit
            // 
            this.miRenameEdit.Image = ((System.Drawing.Image)(resources.GetObject("miRenameEdit.Image")));
            this.miRenameEdit.Name = "miRenameEdit";
            this.miRenameEdit.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miRenameEdit.Size = new System.Drawing.Size(145, 22);
            this.miRenameEdit.Text = resources.GetString("this.miRenameEdit.Text");
            this.miRenameEdit.Click += new System.EventHandler(this.miRenameEdit_Click);
            // 
            // miDelete
            // 
            this.miDelete.Image = ((System.Drawing.Image)(resources.GetObject("miDelete.Image")));
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miDelete.Size = new System.Drawing.Size(145, 22);
            this.miDelete.Text = resources.GetString("this.miDelete.Text");
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // PlanManagementWindow
            // 
            this.AcceptButton = this.btnOpen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 425);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlanManagementWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("this.Text");
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ButtonsFlowLayoutPanel.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog ofdOpenDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel ButtonsFlowLayoutPanel;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmiDelete;
        private System.Windows.Forms.ToolStripMenuItem cmiRenameEdit;
        private System.Windows.Forms.ToolStripMenuItem cmiOpen;
        private DraggableListView lbPlanList;
        private System.Windows.Forms.ColumnHeader PlanName;
        private System.Windows.Forms.ColumnHeader PlanDate;
        private System.Windows.Forms.ColumnHeader PlanSkills;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mEdit;
        private System.Windows.Forms.ToolStripMenuItem miImportPlanFromFile;
        private System.Windows.Forms.ToolStripMenuItem miImportPlanFromCharacter;
        private System.Windows.Forms.ToolStripMenuItem miRenameEdit;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miNewPlan;
        private System.Windows.Forms.ToolStripMenuItem cmiExport;
        private System.Windows.Forms.ColumnHeader PlanDescription;
        private System.Windows.Forms.ToolStripMenuItem miRestorePlans;
        private System.Windows.Forms.ToolStripSeparator ImportExportPlanToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem miSavePlans;
        private System.Windows.Forms.ToolStripMenuItem miExportPlan;
        private System.Windows.Forms.ToolStripSeparator NewPlanToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem miExportCharacterSkillsAsPlan;
        private System.Windows.Forms.ToolStripSeparator ExportCharAsPlanToolStripSeparator;
    }
}
