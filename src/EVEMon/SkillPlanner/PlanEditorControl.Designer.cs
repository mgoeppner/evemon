using EVEMon.Common.Controls;

namespace EVEMon.SkillPlanner
{
    sealed partial class PlanEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanEditorControl));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miChangeNote = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangePriority = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTo0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTo1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTo2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTo3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTo4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeTo5 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miRemoveFromPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToTopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miShowInSkillBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowInSkillExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.showInMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miMarkOwned = new System.Windows.Forms.ToolStripMenuItem();
            this.markOwnedMenuSeaprator = new System.Windows.Forms.ToolStripSeparator();
            this.miCopyTo = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyToNewPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopySelectedToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miPlanGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tmrAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.tsPlan = new System.Windows.Forms.ToolStrip();
            this.tslMove = new System.Windows.Forms.ToolStripLabel();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslSort = new System.Windows.Forms.ToolStripLabel();
            this.tsSortPriorities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbToggleSkills = new System.Windows.Forms.ToolStripButton();
            this.tsbToggleRemapping = new System.Windows.Forms.ToolStripButton();
            this.tssColorKey = new System.Windows.Forms.ToolStripSeparator();
            this.tsbColorKey = new System.Windows.Forms.ToolStripButton();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.tmrSelect = new System.Windows.Forms.Timer(this.components);
            this.pFooter = new System.Windows.Forms.Panel();
            this.gbColorKey = new System.Windows.Forms.GroupBox();
            this.flpColorKey = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTrainable = new System.Windows.Forms.Label();
            this.lblNonPublic = new System.Windows.Forms.Label();
            this.lblPrereqNotMet = new System.Windows.Forms.Label();
            this.lblDepended = new System.Windows.Forms.Label();
            this.lblQueued = new System.Windows.Forms.Label();
            this.lblPrereqMetNotKnown = new System.Windows.Forms.Label();
            this.lblDowntime = new System.Windows.Forms.Label();
            this.lblCurrentlyTraining = new System.Windows.Forms.Label();
            this.lblPartiallyTrained = new System.Windows.Forms.Label();
            this.lblOmegaOnly = new System.Windows.Forms.Label();
            this.pscPlan = new EVEMon.Common.Controls.PersistentSplitContainer();
            this.lvSkills = new EVEMon.Common.Controls.DraggableListView();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.implantSetterPanel = new System.Windows.Forms.Panel();
            this.lblChooseImplantSet = new System.Windows.Forms.Label();
            this.cbChooseImplantSet = new System.Windows.Forms.ComboBox();
            this.tsPreferences = new System.Windows.Forms.ToolStrip();
            this.preferencesMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.columnSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSizeColumnsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skillSelectControl = new EVEMon.SkillPlanner.SkillSelectControl();
            this.contextMenu.SuspendLayout();
            this.tsPlan.SuspendLayout();
            this.pFooter.SuspendLayout();
            this.gbColorKey.SuspendLayout();
            this.flpColorKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pscPlan)).BeginInit();
            this.pscPlan.Panel1.SuspendLayout();
            this.pscPlan.Panel2.SuspendLayout();
            this.pscPlan.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.implantSetterPanel.SuspendLayout();
            this.tsPreferences.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miChangeNote,
            this.miChangePriority,
            this.miChangeLevel,
            this.changeMenuSeparator,
            this.miRemoveFromPlan,
            this.MoveToTopMenuItem,
            this.planMenuSeparator,
            this.miShowInSkillBrowser,
            this.miShowInSkillExplorer,
            this.showInMenuSeparator,
            this.miMarkOwned,
            this.markOwnedMenuSeaprator,
            this.miCopyTo,
            this.copyMenuSeparator,
            this.miPlanGroups});
            this.contextMenu.Name = "cmsContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(217, 254);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // miChangeNote
            // 
            this.miChangeNote.Name = "miChangeNote";
            this.miChangeNote.Size = new System.Drawing.Size(216, 22);
            this.miChangeNote.Text = resources.GetString("this.miChangeNote.Text");
            this.miChangeNote.Click += new System.EventHandler(this.miChangeNote_Click);
            // 
            // miChangePriority
            // 
            this.miChangePriority.Name = "miChangePriority";
            this.miChangePriority.Size = new System.Drawing.Size(216, 22);
            this.miChangePriority.Text = resources.GetString("this.miChangePriority.Text");
            this.miChangePriority.Click += new System.EventHandler(this.miChangePriority_Click);
            // 
            // miChangeLevel
            // 
            this.miChangeLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miChangeTo0,
            this.miChangeTo1,
            this.miChangeTo2,
            this.miChangeTo3,
            this.miChangeTo4,
            this.miChangeTo5});
            this.miChangeLevel.Image = ((System.Drawing.Image)(resources.GetObject("miChangeLevel.Image")));
            this.miChangeLevel.Name = "miChangeLevel";
            this.miChangeLevel.Size = new System.Drawing.Size(216, 22);
            this.miChangeLevel.Text = resources.GetString("this.miChangeLevel.Text");
            // 
            // miChangeTo0
            // 
            this.miChangeTo0.Name = "miChangeTo0";
            this.miChangeTo0.Size = new System.Drawing.Size(117, 22);
            this.miChangeTo0.Tag = "0";
            this.miChangeTo0.Text = resources.GetString("this.miChangeTo0.Text");
            this.miChangeTo0.Click += new System.EventHandler(this.miChangeToLevel_Click);
            // 
            // miChangeTo1
            // 
            this.miChangeTo1.Name = "miChangeTo1";
            this.miChangeTo1.Size = new System.Drawing.Size(117, 22);
            this.miChangeTo1.Tag = "1";
            this.miChangeTo1.Text = resources.GetString("this.miChangeTo1.Text");
            this.miChangeTo1.Click += new System.EventHandler(this.miChangeToLevel_Click);
            // 
            // miChangeTo2
            // 
            this.miChangeTo2.Name = "miChangeTo2";
            this.miChangeTo2.Size = new System.Drawing.Size(117, 22);
            this.miChangeTo2.Tag = "2";
            this.miChangeTo2.Text = resources.GetString("this.miChangeTo2.Text");
            this.miChangeTo2.Click += new System.EventHandler(this.miChangeToLevel_Click);
            // 
            // miChangeTo3
            // 
            this.miChangeTo3.Name = "miChangeTo3";
            this.miChangeTo3.Size = new System.Drawing.Size(117, 22);
            this.miChangeTo3.Tag = "3";
            this.miChangeTo3.Text = resources.GetString("this.miChangeTo3.Text");
            this.miChangeTo3.Click += new System.EventHandler(this.miChangeToLevel_Click);
            // 
            // miChangeTo4
            // 
            this.miChangeTo4.Name = "miChangeTo4";
            this.miChangeTo4.Size = new System.Drawing.Size(117, 22);
            this.miChangeTo4.Tag = "4";
            this.miChangeTo4.Text = resources.GetString("this.miChangeTo4.Text");
            this.miChangeTo4.Click += new System.EventHandler(this.miChangeToLevel_Click);
            // 
            // miChangeTo5
            // 
            this.miChangeTo5.Name = "miChangeTo5";
            this.miChangeTo5.Size = new System.Drawing.Size(117, 22);
            this.miChangeTo5.Tag = "5";
            this.miChangeTo5.Text = resources.GetString("this.miChangeTo5.Text");
            this.miChangeTo5.Click += new System.EventHandler(this.miChangeToLevel_Click);
            // 
            // changeMenuSeparator
            // 
            this.changeMenuSeparator.Name = "changeMenuSeparator";
            this.changeMenuSeparator.Size = new System.Drawing.Size(213, 6);
            // 
            // miRemoveFromPlan
            // 
            this.miRemoveFromPlan.Name = "miRemoveFromPlan";
            this.miRemoveFromPlan.Size = new System.Drawing.Size(216, 22);
            this.miRemoveFromPlan.Text = resources.GetString("this.miRemoveFromPlan.Text");
            this.miRemoveFromPlan.Click += new System.EventHandler(this.miRemoveFromPlan_Click);
            // 
            // MoveToTopMenuItem
            // 
            this.MoveToTopMenuItem.Name = "MoveToTopMenuItem";
            this.MoveToTopMenuItem.Size = new System.Drawing.Size(216, 22);
            this.MoveToTopMenuItem.Text = resources.GetString("this.MoveToTopMenuItem.Text");
            this.MoveToTopMenuItem.Click += new System.EventHandler(this.MoveToTopMenuItem_Click);
            // 
            // planMenuSeparator
            // 
            this.planMenuSeparator.Name = "planMenuSeparator";
            this.planMenuSeparator.Size = new System.Drawing.Size(213, 6);
            // 
            // miShowInSkillBrowser
            // 
            this.miShowInSkillBrowser.Name = "miShowInSkillBrowser";
            this.miShowInSkillBrowser.Size = new System.Drawing.Size(216, 22);
            this.miShowInSkillBrowser.Text = resources.GetString("this.miShowInSkillBrowser.Text");
            this.miShowInSkillBrowser.Click += new System.EventHandler(this.miShowInSkillBrowser_Click);
            // 
            // miShowInSkillExplorer
            // 
            this.miShowInSkillExplorer.Image = ((System.Drawing.Image)(resources.GetObject("miShowInSkillExplorer.Image")));
            this.miShowInSkillExplorer.Name = "miShowInSkillExplorer";
            this.miShowInSkillExplorer.Size = new System.Drawing.Size(216, 22);
            this.miShowInSkillExplorer.Text = resources.GetString("this.miShowInSkillExplorer.Text");
            this.miShowInSkillExplorer.Click += new System.EventHandler(this.miShowInSkillExplorer_Click);
            // 
            // showInMenuSeparator
            // 
            this.showInMenuSeparator.Name = "showInMenuSeparator";
            this.showInMenuSeparator.Size = new System.Drawing.Size(213, 6);
            // 
            // miMarkOwned
            // 
            this.miMarkOwned.Name = "miMarkOwned";
            this.miMarkOwned.Size = new System.Drawing.Size(216, 22);
            this.miMarkOwned.Text = resources.GetString("this.miMarkOwned.Text");
            this.miMarkOwned.Click += new System.EventHandler(this.miMarkOwned_Click);
            // 
            // markOwnedMenuSeaprator
            // 
            this.markOwnedMenuSeaprator.Name = "markOwnedMenuSeaprator";
            this.markOwnedMenuSeaprator.Size = new System.Drawing.Size(213, 6);
            // 
            // miCopyTo
            // 
            this.miCopyTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCopyToNewPlan,
            this.miCopySelectedToClipboard});
            this.miCopyTo.Name = "miCopyTo";
            this.miCopyTo.Size = new System.Drawing.Size(216, 22);
            this.miCopyTo.Text = resources.GetString("this.miCopyTo.Text");
            // 
            // miCopyToNewPlan
            // 
            this.miCopyToNewPlan.Name = "miCopyToNewPlan";
            this.miCopyToNewPlan.Size = new System.Drawing.Size(152, 22);
            this.miCopyToNewPlan.Text = resources.GetString("this.miCopyToNewPlan.Text");
            this.miCopyToNewPlan.Click += new System.EventHandler(this.miCopyToNewPlan_Click);
            // 
            // miCopySelectedToClipboard
            // 
            this.miCopySelectedToClipboard.Name = "miCopySelectedToClipboard";
            this.miCopySelectedToClipboard.Size = new System.Drawing.Size(152, 22);
            this.miCopySelectedToClipboard.Text = resources.GetString("this.miCopySelectedToClipboard.Text");
            this.miCopySelectedToClipboard.Click += new System.EventHandler(this.miCopySelectedToClipboard_Click);
            // 
            // copyMenuSeparator
            // 
            this.copyMenuSeparator.Name = "copyMenuSeparator";
            this.copyMenuSeparator.Size = new System.Drawing.Size(213, 6);
            // 
            // miPlanGroups
            // 
            this.miPlanGroups.Name = "miPlanGroups";
            this.miPlanGroups.Size = new System.Drawing.Size(216, 22);
            this.miPlanGroups.Text = resources.GetString("this.miPlanGroups.Text");
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "EVEMon Plan Format (*.emp)|*.emp|XML Format (*.xml)|*.xml|Text Format (*.txt)|*.t" +
    "xt";
            this.sfdSave.Title = resources.GetString("this.sfdSave.Title");
            // 
            // tmrAutoRefresh
            // 
            this.tmrAutoRefresh.Interval = 30000;
            this.tmrAutoRefresh.Tick += new System.EventHandler(this.tmrAutoRefresh_Tick);
            // 
            // tsPlan
            // 
            this.tsPlan.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsPlan.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsPlan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslMove,
            this.tsbMoveUp,
            this.tsbMoveDown,
            this.toolStripSeparator1,
            this.tslSort,
            this.tsSortPriorities,
            this.toolStripSeparator4,
            this.tsbToggleSkills,
            this.tsbToggleRemapping,
            this.tssColorKey,
            this.tsbColorKey});
            this.tsPlan.Location = new System.Drawing.Point(0, 0);
            this.tsPlan.Name = "tsPlan";
            this.tsPlan.Size = new System.Drawing.Size(41, 558);
            this.tsPlan.TabIndex = 0;
            this.tsPlan.Text = resources.GetString("this.tsPlan.Text");
            // 
            // tslMove
            // 
            this.tslMove.Name = "tslMove";
            this.tslMove.Size = new System.Drawing.Size(38, 15);
            this.tslMove.Text = resources.GetString("this.tslMove.Text");
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(38, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // tslSort
            // 
            this.tslSort.Name = "tslSort";
            this.tslSort.Size = new System.Drawing.Size(38, 15);
            this.tslSort.Text = resources.GetString("this.tslSort.Text");
            // 
            // tsSortPriorities
            // 
            this.tsSortPriorities.CheckOnClick = true;
            this.tsSortPriorities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSortPriorities.Image = ((System.Drawing.Image)(resources.GetObject("tsSortPriorities.Image")));
            this.tsSortPriorities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSortPriorities.Name = "tsSortPriorities";
            this.tsSortPriorities.Size = new System.Drawing.Size(38, 20);
            this.tsSortPriorities.Text = resources.GetString("this.tsSortPriorities.Text");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(38, 6);
            // 
            // tsbToggleSkills
            // 
            this.tsbToggleSkills.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToggleSkills.Image = ((System.Drawing.Image)(resources.GetObject("tsbToggleSkills.Image")));
            this.tsbToggleSkills.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToggleSkills.Name = "tsbToggleSkills";
            this.tsbToggleSkills.Size = new System.Drawing.Size(38, 20);
            this.tsbToggleSkills.Text = resources.GetString("this.tsbToggleSkills.Text");
            this.tsbToggleSkills.ToolTipText = resources.GetString("this.tsbToggleSkills.ToolTipText");
            this.tsbToggleSkills.Click += new System.EventHandler(this.toggleSkillsPanelButton_Click);
            // 
            // tsbToggleRemapping
            // 
            this.tsbToggleRemapping.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToggleRemapping.Image = ((System.Drawing.Image)(resources.GetObject("tsbToggleRemapping.Image")));
            this.tsbToggleRemapping.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToggleRemapping.Name = "tsbToggleRemapping";
            this.tsbToggleRemapping.Size = new System.Drawing.Size(38, 20);
            this.tsbToggleRemapping.Text = resources.GetString("this.tsbToggleRemapping.Text");
            this.tsbToggleRemapping.ToolTipText = resources.GetString("this.tsbToggleRemapping.ToolTipText");
            this.tsbToggleRemapping.Click += new System.EventHandler(this.tsbToggleRemapping_Click);
            // 
            // tssColorKey
            // 
            this.tssColorKey.Name = "tssColorKey";
            this.tssColorKey.Size = new System.Drawing.Size(38, 6);
            // 
            // tsbColorKey
            // 
            this.tsbColorKey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbColorKey.Image = ((System.Drawing.Image)(resources.GetObject("tsbColorKey.Image")));
            this.tsbColorKey.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbColorKey.Name = "tsbColorKey";
            this.tsbColorKey.Size = new System.Drawing.Size(38, 20);
            this.tsbColorKey.Text = resources.GetString("this.tsbColorKey.Text");
            this.tsbColorKey.ToolTipText = resources.GetString("this.tsbColorKey.ToolTipText");
            this.tsbColorKey.Click += new System.EventHandler(this.tsbColorKey_Click);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "RedArrow.png");
            this.ilIcons.Images.SetKeyName(1, "BlueArrow.png");
            this.ilIcons.Images.SetKeyName(2, "GreenArrow.png");
            this.ilIcons.Images.SetKeyName(3, "shape_align_middle.png");
            this.ilIcons.Images.SetKeyName(4, "arrow_up.png");
            this.ilIcons.Images.SetKeyName(5, "arrow_down.png");
            this.ilIcons.Images.SetKeyName(6, "16x16Transparant.png");
            // 
            // tmrSelect
            // 
            this.tmrSelect.Tick += new System.EventHandler(this.tmrSelect_Tick);
            // 
            // pFooter
            // 
            this.pFooter.Controls.Add(this.gbColorKey);
            this.pFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pFooter.Location = new System.Drawing.Point(41, 520);
            this.pFooter.Name = "pFooter";
            this.pFooter.Size = new System.Drawing.Size(719, 38);
            this.pFooter.TabIndex = 14;
            this.pFooter.Visible = false;
            // 
            // gbColorKey
            // 
            this.gbColorKey.Controls.Add(this.flpColorKey);
            this.gbColorKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbColorKey.Location = new System.Drawing.Point(0, 0);
            this.gbColorKey.Name = "gbColorKey";
            this.gbColorKey.Size = new System.Drawing.Size(719, 38);
            this.gbColorKey.TabIndex = 0;
            this.gbColorKey.TabStop = false;
            this.gbColorKey.Text = resources.GetString("this.gbColorKey.Text");
            // 
            // flpColorKey
            // 
            this.flpColorKey.AutoSize = true;
            this.flpColorKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpColorKey.Controls.Add(this.lblTrainable);
            this.flpColorKey.Controls.Add(this.lblNonPublic);
            this.flpColorKey.Controls.Add(this.lblPrereqNotMet);
            this.flpColorKey.Controls.Add(this.lblDepended);
            this.flpColorKey.Controls.Add(this.lblQueued);
            this.flpColorKey.Controls.Add(this.lblPrereqMetNotKnown);
            this.flpColorKey.Controls.Add(this.lblDowntime);
            this.flpColorKey.Controls.Add(this.lblCurrentlyTraining);
            this.flpColorKey.Controls.Add(this.lblPartiallyTrained);
            this.flpColorKey.Controls.Add(this.lblOmegaOnly);
            this.flpColorKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpColorKey.Location = new System.Drawing.Point(3, 16);
            this.flpColorKey.Name = "flpColorKey";
            this.flpColorKey.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.flpColorKey.Size = new System.Drawing.Size(713, 19);
            this.flpColorKey.TabIndex = 29;
            // 
            // lblTrainable
            // 
            this.lblTrainable.AutoSize = true;
            this.lblTrainable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrainable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTrainable.Location = new System.Drawing.Point(5, 0);
            this.lblTrainable.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblTrainable.Name = "lblTrainable";
            this.lblTrainable.Size = new System.Drawing.Size(53, 15);
            this.lblTrainable.TabIndex = 37;
            this.lblTrainable.Text = resources.GetString("this.lblTrainable.Text");
            this.lblTrainable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNonPublic
            // 
            this.lblNonPublic.AutoSize = true;
            this.lblNonPublic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNonPublic.ForeColor = System.Drawing.Color.DarkRed;
            this.lblNonPublic.Location = new System.Drawing.Point(62, 0);
            this.lblNonPublic.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblNonPublic.Name = "lblNonPublic";
            this.lblNonPublic.Size = new System.Drawing.Size(61, 15);
            this.lblNonPublic.TabIndex = 35;
            this.lblNonPublic.Text = resources.GetString("this.lblNonPublic.Text");
            this.lblNonPublic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrereqNotMet
            // 
            this.lblPrereqNotMet.AutoSize = true;
            this.lblPrereqNotMet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrereqNotMet.ForeColor = System.Drawing.Color.Red;
            this.lblPrereqNotMet.Location = new System.Drawing.Point(127, 0);
            this.lblPrereqNotMet.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblPrereqNotMet.Name = "lblPrereqNotMet";
            this.lblPrereqNotMet.Size = new System.Drawing.Size(81, 15);
            this.lblPrereqNotMet.TabIndex = 29;
            this.lblPrereqNotMet.Text = resources.GetString("this.lblPrereqNotMet.Text");
            this.lblPrereqNotMet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDepended
            // 
            this.lblDepended.AutoSize = true;
            this.lblDepended.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDepended.ForeColor = System.Drawing.Color.Gray;
            this.lblDepended.Location = new System.Drawing.Point(212, 0);
            this.lblDepended.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblDepended.Name = "lblDepended";
            this.lblDepended.Size = new System.Drawing.Size(59, 15);
            this.lblDepended.TabIndex = 30;
            this.lblDepended.Text = resources.GetString("this.lblDepended.Text");
            this.lblDepended.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQueued
            // 
            this.lblQueued.AutoSize = true;
            this.lblQueued.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQueued.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblQueued.Location = new System.Drawing.Point(275, 0);
            this.lblQueued.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblQueued.Name = "lblQueued";
            this.lblQueued.Size = new System.Drawing.Size(47, 15);
            this.lblQueued.TabIndex = 32;
            this.lblQueued.Text = resources.GetString("this.lblQueued.Text");
            this.lblQueued.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrereqMetNotKnown
            // 
            this.lblPrereqMetNotKnown.AutoSize = true;
            this.lblPrereqMetNotKnown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrereqMetNotKnown.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblPrereqMetNotKnown.Location = new System.Drawing.Point(326, 0);
            this.lblPrereqMetNotKnown.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblPrereqMetNotKnown.Name = "lblPrereqMetNotKnown";
            this.lblPrereqMetNotKnown.Size = new System.Drawing.Size(123, 15);
            this.lblPrereqMetNotKnown.TabIndex = 36;
            this.lblPrereqMetNotKnown.Text = resources.GetString("this.lblPrereqMetNotKnown.Text");
            this.lblPrereqMetNotKnown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDowntime
            // 
            this.lblDowntime.AutoSize = true;
            this.lblDowntime.BackColor = System.Drawing.Color.DarkGray;
            this.lblDowntime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDowntime.ForeColor = System.Drawing.Color.Red;
            this.lblDowntime.Location = new System.Drawing.Point(453, 0);
            this.lblDowntime.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblDowntime.Name = "lblDowntime";
            this.lblDowntime.Size = new System.Drawing.Size(56, 15);
            this.lblDowntime.TabIndex = 33;
            this.lblDowntime.Text = resources.GetString("this.lblDowntime.Text");
            this.lblDowntime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentlyTraining
            // 
            this.lblCurrentlyTraining.AutoSize = true;
            this.lblCurrentlyTraining.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblCurrentlyTraining.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentlyTraining.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentlyTraining.Location = new System.Drawing.Point(513, 0);
            this.lblCurrentlyTraining.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblCurrentlyTraining.Name = "lblCurrentlyTraining";
            this.lblCurrentlyTraining.Size = new System.Drawing.Size(91, 15);
            this.lblCurrentlyTraining.TabIndex = 34;
            this.lblCurrentlyTraining.Text = resources.GetString("this.lblCurrentlyTraining.Text");
            this.lblCurrentlyTraining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPartiallyTrained
            // 
            this.lblPartiallyTrained.AutoSize = true;
            this.lblPartiallyTrained.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPartiallyTrained.ForeColor = System.Drawing.Color.Green;
            this.lblPartiallyTrained.Location = new System.Drawing.Point(608, 0);
            this.lblPartiallyTrained.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblPartiallyTrained.Name = "lblPartiallyTrained";
            this.lblPartiallyTrained.Size = new System.Drawing.Size(84, 15);
            this.lblPartiallyTrained.TabIndex = 31;
            this.lblPartiallyTrained.Text = resources.GetString("this.lblPartiallyTrained.Text");
            this.lblPartiallyTrained.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOmegaOnly
            // 
            this.lblOmegaOnly.AutoSize = true;
            this.lblOmegaOnly.BackColor = System.Drawing.Color.Gold;
            this.lblOmegaOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOmegaOnly.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOmegaOnly.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.lblOmegaOnly.Name = "lblOmegaOnly";
            this.lblOmegaOnly.TabIndex = 38;
            this.lblOmegaOnly.Text = resources.GetString("this.lblOmegaOnly.Text");
            this.lblOmegaOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pscPlan
            // 
            this.pscPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pscPlan.Location = new System.Drawing.Point(41, 0);
            this.pscPlan.Name = "pscPlan";
            // 
            // pscPlan.Panel1
            // 
            this.pscPlan.Panel1.Controls.Add(this.lvSkills);
            this.pscPlan.Panel1.Controls.Add(this.tlpHeader);
            // 
            // pscPlan.Panel2
            // 
            this.pscPlan.Panel2.Controls.Add(this.skillSelectControl);
            this.pscPlan.RememberDistanceKey = null;
            this.pscPlan.Size = new System.Drawing.Size(719, 520);
            this.pscPlan.SplitterDistance = 519;
            this.pscPlan.TabIndex = 13;
            // 
            // lvSkills
            // 
            this.lvSkills.AllowColumnReorder = true;
            this.lvSkills.AllowDrop = true;
            this.lvSkills.AllowRowReorder = true;
            this.lvSkills.ContextMenuStrip = this.contextMenu;
            this.lvSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSkills.FullRowSelect = true;
            this.lvSkills.HideSelection = false;
            this.lvSkills.Location = new System.Drawing.Point(0, 33);
            this.lvSkills.Name = "lvSkills";
            this.lvSkills.Size = new System.Drawing.Size(519, 487);
            this.lvSkills.SmallImageList = this.ilIcons;
            this.lvSkills.TabIndex = 0;
            this.lvSkills.UseCompatibleStateImageBehavior = false;
            this.lvSkills.View = System.Windows.Forms.View.Details;
            this.lvSkills.ListViewItemsDragged += new System.EventHandler<System.EventArgs>(this.lvSkills_ListViewItemsDragged);
            this.lvSkills.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvSkills_ColumnReordered);
            this.lvSkills.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.lvSkills_ItemHover);
            this.lvSkills.SelectedIndexChanged += new System.EventHandler(this.lvSkills_SelectedIndexChanged);
            this.lvSkills.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvSkills_DragDrop);
            this.lvSkills.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSkills_DragEnter);
            this.lvSkills.DragOver += new System.Windows.Forms.DragEventHandler(this.lvSkills_DragOver);
            this.lvSkills.DragLeave += new System.EventHandler(this.lvSkills_DragLeave);
            this.lvSkills.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSkills_KeyDown);
            this.lvSkills.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSkills_MouseDoubleClick);
            this.lvSkills.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvSkills_MouseDown);
            this.lvSkills.MouseLeave += new System.EventHandler(this.lvSkills_MouseLeave);
            this.lvSkills.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvSkills_MouseMove);
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpHeader.Controls.Add(this.implantSetterPanel, 0, 0);
            this.tlpHeader.Controls.Add(this.tsPreferences, 1, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(519, 33);
            this.tlpHeader.TabIndex = 4;
            // 
            // implantSetterPanel
            // 
            this.implantSetterPanel.AutoSize = true;
            this.implantSetterPanel.Controls.Add(this.lblChooseImplantSet);
            this.implantSetterPanel.Controls.Add(this.cbChooseImplantSet);
            this.implantSetterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.implantSetterPanel.Location = new System.Drawing.Point(0, 0);
            this.implantSetterPanel.Margin = new System.Windows.Forms.Padding(0);
            this.implantSetterPanel.Name = "implantSetterPanel";
            this.implantSetterPanel.Size = new System.Drawing.Size(486, 33);
            this.implantSetterPanel.TabIndex = 1;
            // 
            // lblChooseImplantSet
            // 
            this.lblChooseImplantSet.AutoSize = true;
            this.lblChooseImplantSet.Location = new System.Drawing.Point(3, 10);
            this.lblChooseImplantSet.Name = "lblChooseImplantSet";
            this.lblChooseImplantSet.Size = new System.Drawing.Size(102, 13);
            this.lblChooseImplantSet.TabIndex = 1;
            this.lblChooseImplantSet.Text = resources.GetString("this.lblChooseImplantSet.Text");
            // 
            // cbChooseImplantSet
            // 
            this.cbChooseImplantSet.DisplayMember = "Name";
            this.cbChooseImplantSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChooseImplantSet.FormattingEnabled = true;
            this.cbChooseImplantSet.Location = new System.Drawing.Point(111, 6);
            this.cbChooseImplantSet.Name = "cbChooseImplantSet";
            this.cbChooseImplantSet.Size = new System.Drawing.Size(170, 21);
            this.cbChooseImplantSet.TabIndex = 2;
            this.cbChooseImplantSet.SelectedIndexChanged += new System.EventHandler(this.cbChooseImplantSet_SelectedIndexChanged);
            this.cbChooseImplantSet.DropDownClosed += new System.EventHandler(this.cbChooseImplantSet_DropDownClosed);
            // 
            // tsPreferences
            // 
            this.tsPreferences.CanOverflow = false;
            this.tsPreferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsPreferences.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsPreferences.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesMenu});
            this.tsPreferences.Location = new System.Drawing.Point(486, 0);
            this.tsPreferences.Name = "tsPreferences";
            this.tsPreferences.Size = new System.Drawing.Size(33, 33);
            this.tsPreferences.TabIndex = 3;
            this.tsPreferences.Text = resources.GetString("this.tsPreferences.Text");
            // 
            // preferencesMenu
            // 
            this.preferencesMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.preferencesMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.preferencesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columnSettingsMenuItem,
            this.autoSizeColumnsMenuItem});
            this.preferencesMenu.Image = ((System.Drawing.Image)(resources.GetObject("preferencesMenu.Image")));
            this.preferencesMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.preferencesMenu.Name = "preferencesMenu";
            this.preferencesMenu.Size = new System.Drawing.Size(29, 30);
            this.preferencesMenu.Text = resources.GetString("this.preferencesMenu.Text");
            this.preferencesMenu.ToolTipText = resources.GetString("this.preferencesMenu.ToolTipText");
            // 
            // columnSettingsMenuItem
            // 
            this.columnSettingsMenuItem.Name = "columnSettingsMenuItem";
            this.columnSettingsMenuItem.Size = new System.Drawing.Size(176, 22);
            this.columnSettingsMenuItem.Text = resources.GetString("this.columnSettingsMenuItem.Text");
            this.columnSettingsMenuItem.Click += new System.EventHandler(this.columnSettingsMenuItem_Click);
            // 
            // autoSizeColumnsMenuItem
            // 
            this.autoSizeColumnsMenuItem.Name = "autoSizeColumnsMenuItem";
            this.autoSizeColumnsMenuItem.Size = new System.Drawing.Size(176, 22);
            this.autoSizeColumnsMenuItem.Text = resources.GetString("this.autoSizeColumnsMenuItem.Text");
            this.autoSizeColumnsMenuItem.Click += new System.EventHandler(this.autoSizeColumnsMenuItem_Click);
            // 
            // skillSelectControl
            // 
            this.skillSelectControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.skillSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillSelectControl.Location = new System.Drawing.Point(0, 0);
            this.skillSelectControl.Margin = new System.Windows.Forms.Padding(2);
            this.skillSelectControl.Name = "skillSelectControl";
            this.skillSelectControl.Size = new System.Drawing.Size(196, 520);
            this.skillSelectControl.TabIndex = 12;
            // 
            // PlanEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pscPlan);
            this.Controls.Add(this.pFooter);
            this.Controls.Add(this.tsPlan);
            this.Name = "PlanEditorControl";
            this.Size = new System.Drawing.Size(760, 558);
            this.contextMenu.ResumeLayout(false);
            this.tsPlan.ResumeLayout(false);
            this.tsPlan.PerformLayout();
            this.pFooter.ResumeLayout(false);
            this.gbColorKey.ResumeLayout(false);
            this.gbColorKey.PerformLayout();
            this.flpColorKey.ResumeLayout(false);
            this.flpColorKey.PerformLayout();
            this.pscPlan.Panel1.ResumeLayout(false);
            this.pscPlan.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pscPlan)).EndInit();
            this.pscPlan.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.implantSetterPanel.ResumeLayout(false);
            this.implantSetterPanel.PerformLayout();
            this.tsPreferences.ResumeLayout(false);
            this.tsPreferences.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DraggableListView lvSkills;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miRemoveFromPlan;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.Timer tmrAutoRefresh;
        private System.Windows.Forms.ToolStrip tsPlan;
        private System.Windows.Forms.ToolStripLabel tslMove;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel implantSetterPanel;
        private System.Windows.Forms.ToolStripMenuItem miChangeNote;
        private System.Windows.Forms.ToolStripSeparator changeMenuSeparator;
        private System.Windows.Forms.ToolStripLabel tslSort;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ToolStripMenuItem miShowInSkillBrowser;
        private System.Windows.Forms.ToolStripSeparator planMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem miPlanGroups;
        private System.Windows.Forms.ToolStripMenuItem miCopyTo;
        private System.Windows.Forms.ToolStripMenuItem miChangePriority;
        private System.Windows.Forms.ToolStripMenuItem miMarkOwned;
        private System.Windows.Forms.ToolStripMenuItem miShowInSkillExplorer;
        private System.Windows.Forms.ToolStripSeparator copyMenuSeparator;
        private System.Windows.Forms.ToolStripMenuItem miChangeLevel;
        private System.Windows.Forms.ToolStripMenuItem miChangeTo0;
        private System.Windows.Forms.ToolStripMenuItem miChangeTo1;
        private System.Windows.Forms.ToolStripMenuItem miChangeTo2;
        private System.Windows.Forms.ToolStripMenuItem miChangeTo3;
        private System.Windows.Forms.ToolStripMenuItem miChangeTo4;
        private System.Windows.Forms.ToolStripMenuItem miChangeTo5;
        private PersistentSplitContainer pscPlan;
        private System.Windows.Forms.ToolStripButton tsbToggleSkills;
        private System.Windows.Forms.Timer tmrSelect;
        private System.Windows.Forms.ToolStripButton tsbToggleRemapping;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsSortPriorities;
        private System.Windows.Forms.ToolStripSeparator tssColorKey;
        private System.Windows.Forms.ToolStripButton tsbColorKey;
        private System.Windows.Forms.Panel pFooter;
        private System.Windows.Forms.GroupBox gbColorKey;
        private System.Windows.Forms.FlowLayoutPanel flpColorKey;
        private System.Windows.Forms.Label lblTrainable;
        private System.Windows.Forms.Label lblNonPublic;
        private System.Windows.Forms.Label lblPrereqNotMet;
        private System.Windows.Forms.Label lblDepended;
        private System.Windows.Forms.Label lblQueued;
        private System.Windows.Forms.Label lblPrereqMetNotKnown;
        private System.Windows.Forms.Label lblDowntime;
        private System.Windows.Forms.Label lblCurrentlyTraining;
        private System.Windows.Forms.Label lblPartiallyTrained;
        private System.Windows.Forms.Label lblOmegaOnly;
        private System.Windows.Forms.ComboBox cbChooseImplantSet;
        private System.Windows.Forms.Label lblChooseImplantSet;
        private System.Windows.Forms.ToolStripMenuItem MoveToTopMenuItem;
        private System.Windows.Forms.ToolStrip tsPreferences;
        private System.Windows.Forms.ToolStripDropDownButton preferencesMenu;
        private System.Windows.Forms.ToolStripMenuItem columnSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoSizeColumnsMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private SkillSelectControl skillSelectControl;
        private System.Windows.Forms.ToolStripSeparator showInMenuSeparator;
        private System.Windows.Forms.ToolStripSeparator markOwnedMenuSeaprator;
        private System.Windows.Forms.ToolStripMenuItem miCopyToNewPlan;
        private System.Windows.Forms.ToolStripMenuItem miCopySelectedToClipboard;
    }
}
