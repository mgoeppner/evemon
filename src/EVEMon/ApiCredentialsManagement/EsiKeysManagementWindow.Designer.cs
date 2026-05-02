using EVEMon.Common.Controls.MultiPanel;

namespace EVEMon.ApiCredentialsManagement
{
    partial class EsiKeysManagementWindow
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "CCP",
            "987654321",
            "John Doe",
            "123456",
            "Auto",
            "(none)"}, 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EsiKeysManagementWindow));
			this.esiKeysLabel = new System.Windows.Forms.Label();
			this.charactersLabel = new System.Windows.Forms.Label();
			this.esiKeyListLabel = new System.Windows.Forms.Label();
			this.charactersListLabel = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.charactersTabPage = new System.Windows.Forms.TabPage();
			this.charactersPagePanel = new System.Windows.Forms.Panel();
			this.charactersMultiPanel = new EVEMon.Common.Controls.MultiPanel.MultiPanel();
			this.charactersListPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.charactersListView = new System.Windows.Forms.ListView();
			this.columnMonitored = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnESIKeyID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnUri = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.noCharactersPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.charactersToolStrip = new System.Windows.Forms.ToolStrip();
			this.importCharacterMenu = new System.Windows.Forms.ToolStripButton();
			this.deleteCharacterMenu = new System.Windows.Forms.ToolStripButton();
			this.editUriMenu = new System.Windows.Forms.ToolStripButton();
			this.groupingMenu = new System.Windows.Forms.ToolStripButton();
			this.esiKeysTabPage = new System.Windows.Forms.TabPage();
			this.esiKeysPagePanel = new System.Windows.Forms.Panel();
			this.esiKeysMultiPanel = new EVEMon.Common.Controls.MultiPanel.MultiPanel();
			this.esiKeysListPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.esiKeysListBox = new EVEMon.ApiCredentialsManagement.EsiKeysListBox();
			this.noESIKeysPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
			this.esiKeysToolStrip = new System.Windows.Forms.ToolStrip();
			this.addESIKeyMenu = new System.Windows.Forms.ToolStripButton();
			this.deleteESIKeyMenu = new System.Windows.Forms.ToolStripButton();
			this.editESIKeyMenu = new System.Windows.Forms.ToolStripButton();
			this.tabControl.SuspendLayout();
			this.charactersTabPage.SuspendLayout();
			this.charactersPagePanel.SuspendLayout();
			this.charactersMultiPanel.SuspendLayout();
			this.charactersListPage.SuspendLayout();
			this.noCharactersPage.SuspendLayout();
			this.charactersToolStrip.SuspendLayout();
			this.esiKeysTabPage.SuspendLayout();
			this.esiKeysPagePanel.SuspendLayout();
			this.esiKeysMultiPanel.SuspendLayout();
			this.esiKeysListPage.SuspendLayout();
			this.noESIKeysPage.SuspendLayout();
			this.esiKeysToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// esiKeysLabel
			// 
			this.esiKeysLabel.AutoSize = true;
			this.esiKeysLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.esiKeysLabel.ForeColor = System.Drawing.SystemColors.Highlight;
			this.esiKeysLabel.Location = new System.Drawing.Point(3, 3);
			this.esiKeysLabel.Name = "esiKeysLabel";
			this.esiKeysLabel.Size = new System.Drawing.Size(50, 13);
			this.esiKeysLabel.TabIndex = 1;
			this.esiKeysLabel.Text = resources.GetString("this.esiKeysLabel.Text");
			// 
			// charactersLabel
			// 
			this.charactersLabel.AutoSize = true;
			this.charactersLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.charactersLabel.ForeColor = System.Drawing.SystemColors.Highlight;
			this.charactersLabel.Location = new System.Drawing.Point(3, 3);
			this.charactersLabel.Name = "charactersLabel";
			this.charactersLabel.Size = new System.Drawing.Size(58, 13);
			this.charactersLabel.TabIndex = 5;
			this.charactersLabel.Text = resources.GetString("this.charactersLabel.Text");
			// 
			// esiKeyListLabel
			// 
			this.esiKeyListLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.esiKeyListLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.esiKeyListLabel.Location = new System.Drawing.Point(0, 0);
			this.esiKeyListLabel.Name = "esiKeyListLabel";
			this.esiKeyListLabel.Size = new System.Drawing.Size(796, 318);
			this.esiKeyListLabel.TabIndex = 0;
			this.esiKeyListLabel.Text = resources.GetString("this.esiKeyListLabel.Text");
			this.esiKeyListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// charactersListLabel
			// 
			this.charactersListLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.charactersListLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.charactersListLabel.Location = new System.Drawing.Point(0, 0);
			this.charactersListLabel.Name = "charactersListLabel";
			this.charactersListLabel.Size = new System.Drawing.Size(796, 318);
			this.charactersListLabel.TabIndex = 10;
			this.charactersListLabel.Text = "First add your ESI key at ESI Keys section, characters will then appear in this l" +
    "ist.\r\nYou can also import XML character sheets from files or URLs.";
			this.charactersListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.CausesValidation = false;
			this.closeButton.Location = new System.Drawing.Point(732, 406);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(90, 23);
			this.closeButton.TabIndex = 9;
			this.closeButton.Text = resources.GetString("this.closeButton.Text");
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.charactersTabPage);
			this.tabControl.Controls.Add(this.esiKeysTabPage);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(810, 388);
			this.tabControl.TabIndex = 12;
			// 
			// charactersTabPage
			// 
			this.charactersTabPage.Controls.Add(this.charactersPagePanel);
			this.charactersTabPage.Controls.Add(this.charactersLabel);
			this.charactersTabPage.Location = new System.Drawing.Point(4, 22);
			this.charactersTabPage.Name = "charactersTabPage";
			this.charactersTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.charactersTabPage.Size = new System.Drawing.Size(802, 362);
			this.charactersTabPage.TabIndex = 1;
			this.charactersTabPage.Text = resources.GetString("this.charactersTabPage.Text");
			this.charactersTabPage.UseVisualStyleBackColor = true;
			// 
			// charactersPagePanel
			// 
			this.charactersPagePanel.Controls.Add(this.charactersMultiPanel);
			this.charactersPagePanel.Controls.Add(this.charactersToolStrip);
			this.charactersPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.charactersPagePanel.Location = new System.Drawing.Point(3, 16);
			this.charactersPagePanel.Name = "charactersPagePanel";
			this.charactersPagePanel.Size = new System.Drawing.Size(796, 343);
			this.charactersPagePanel.TabIndex = 16;
			// 
			// charactersMultiPanel
			// 
			this.charactersMultiPanel.Controls.Add(this.charactersListPage);
			this.charactersMultiPanel.Controls.Add(this.noCharactersPage);
			this.charactersMultiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.charactersMultiPanel.Location = new System.Drawing.Point(0, 25);
			this.charactersMultiPanel.Name = "charactersMultiPanel";
			this.charactersMultiPanel.SelectedPage = this.charactersListPage;
			this.charactersMultiPanel.Size = new System.Drawing.Size(796, 318);
			this.charactersMultiPanel.TabIndex = 15;
			// 
			// charactersListPage
			// 
			this.charactersListPage.BackColor = System.Drawing.SystemColors.Window;
			this.charactersListPage.Controls.Add(this.charactersListView);
			this.charactersListPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.charactersListPage.Location = new System.Drawing.Point(0, 0);
			this.charactersListPage.Name = "charactersListPage";
			this.charactersListPage.Size = new System.Drawing.Size(796, 318);
			this.charactersListPage.TabIndex = 0;
			this.charactersListPage.Text = resources.GetString("this.charactersListPage.Text");
			// 
			// charactersListView
			// 
			this.charactersListView.CheckBoxes = true;
			this.charactersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnMonitored,
            this.columnType,
            this.columnID,
            this.columnName,
            this.columnESIKeyID,
            this.columnStatus,
            this.columnUri});
			this.charactersListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.charactersListView.FullRowSelect = true;
			this.charactersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			listViewItem1.StateImageIndex = 0;
			this.charactersListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.charactersListView.Location = new System.Drawing.Point(0, 0);
			this.charactersListView.MultiSelect = false;
			this.charactersListView.Name = "charactersListView";
			this.charactersListView.Size = new System.Drawing.Size(796, 318);
			this.charactersListView.TabIndex = 12;
			this.charactersListView.UseCompatibleStateImageBehavior = false;
			this.charactersListView.View = System.Windows.Forms.View.Details;
			this.charactersListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.charactersListView_ColumnWidthChanging);
			this.charactersListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.charactersListView_ItemChecked);
			this.charactersListView.SelectedIndexChanged += new System.EventHandler(this.charactersListView_SelectedIndexChanged);
			this.charactersListView.DoubleClick += new System.EventHandler(this.charactersListView_DoubleClick);
			this.charactersListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.charactersListView_KeyDown);
			// 
			// columnMonitored
			// 
			this.columnMonitored.Text = "";
			this.columnMonitored.Width = 36;
			// 
			// columnType
			// 
			this.columnType.Text = resources.GetString("this.columnType.Text");
			this.columnType.Width = 49;
			// 
			// columnID
			// 
			this.columnID.Text = resources.GetString("this.columnID.Text");
			this.columnID.Width = 84;
			// 
			// columnName
			// 
			this.columnName.Text = resources.GetString("this.columnName.Text");
			this.columnName.Width = 117;
			// 
			// columnESIKeyID
			// 
			this.columnESIKeyID.Text = resources.GetString("this.columnESIKeyID.Text");
			this.columnESIKeyID.Width = 75;
			// 
			// columnStatus
			// 
			this.columnStatus.Text = resources.GetString("this.columnStatus.Text");
			// 
			// columnUri
			// 
			this.columnUri.Text = resources.GetString("this.columnUri.Text");
			this.columnUri.Width = 358;
			// 
			// noCharactersPage
			// 
			this.noCharactersPage.Controls.Add(this.charactersListLabel);
			this.noCharactersPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noCharactersPage.Location = new System.Drawing.Point(0, 0);
			this.noCharactersPage.Name = "noCharactersPage";
			this.noCharactersPage.Size = new System.Drawing.Size(796, 318);
			this.noCharactersPage.TabIndex = 1;
			this.noCharactersPage.Text = resources.GetString("this.noCharactersPage.Text");
			// 
			// charactersToolStrip
			// 
			this.charactersToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.charactersToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importCharacterMenu,
            this.deleteCharacterMenu,
            this.editUriMenu,
            this.groupingMenu});
			this.charactersToolStrip.Location = new System.Drawing.Point(0, 0);
			this.charactersToolStrip.Name = "charactersToolStrip";
			this.charactersToolStrip.Size = new System.Drawing.Size(796, 25);
			this.charactersToolStrip.TabIndex = 13;
			// 
			// importCharacterMenu
			// 
			this.importCharacterMenu.Image = ((System.Drawing.Image)(resources.GetObject("importCharacterMenu.Image")));
			this.importCharacterMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.importCharacterMenu.Name = "importCharacterMenu";
			this.importCharacterMenu.Size = new System.Drawing.Size(72, 22);
			this.importCharacterMenu.Text = resources.GetString("this.importCharacterMenu.Text");
			this.importCharacterMenu.Click += new System.EventHandler(this.importCharacterMenu_Click);
			// 
			// deleteCharacterMenu
			// 
			this.deleteCharacterMenu.Enabled = false;
			this.deleteCharacterMenu.Image = ((System.Drawing.Image)(resources.GetObject("deleteCharacterMenu.Image")));
			this.deleteCharacterMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteCharacterMenu.Name = "deleteCharacterMenu";
			this.deleteCharacterMenu.Size = new System.Drawing.Size(69, 22);
			this.deleteCharacterMenu.Text = resources.GetString("this.deleteCharacterMenu.Text");
			this.deleteCharacterMenu.Click += new System.EventHandler(this.deleteCharacterMenu_Click);
			// 
			// editUriMenu
			// 
			this.editUriMenu.Enabled = false;
			this.editUriMenu.Image = ((System.Drawing.Image)(resources.GetObject("editUriMenu.Image")));
			this.editUriMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.editUriMenu.Name = "editUriMenu";
			this.editUriMenu.Size = new System.Drawing.Size(74, 22);
			this.editUriMenu.Text = resources.GetString("this.editUriMenu.Text");
			this.editUriMenu.Click += new System.EventHandler(this.editUriButton_Click);
			// 
			// groupingMenu
			// 
			this.groupingMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.groupingMenu.Checked = true;
			this.groupingMenu.CheckOnClick = true;
			this.groupingMenu.CheckState = System.Windows.Forms.CheckState.Checked;
			this.groupingMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.groupingMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.groupingMenu.Name = "groupingMenu";
			this.groupingMenu.Size = new System.Drawing.Size(101, 22);
			this.groupingMenu.Text = resources.GetString("this.groupingMenu.Text");
			this.groupingMenu.Click += new System.EventHandler(this.groupingMenu_Click);
			// 
			// esiKeysTabPage
			// 
			this.esiKeysTabPage.Controls.Add(this.esiKeysPagePanel);
			this.esiKeysTabPage.Controls.Add(this.esiKeysLabel);
			this.esiKeysTabPage.Location = new System.Drawing.Point(4, 22);
			this.esiKeysTabPage.Name = "esiKeysTabPage";
			this.esiKeysTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.esiKeysTabPage.Size = new System.Drawing.Size(802, 362);
			this.esiKeysTabPage.TabIndex = 0;
			this.esiKeysTabPage.Text = resources.GetString("this.esiKeysTabPage.Text");
			this.esiKeysTabPage.UseVisualStyleBackColor = true;
			// 
			// esiKeysPagePanel
			// 
			this.esiKeysPagePanel.Controls.Add(this.esiKeysMultiPanel);
			this.esiKeysPagePanel.Controls.Add(this.esiKeysToolStrip);
			this.esiKeysPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.esiKeysPagePanel.Location = new System.Drawing.Point(3, 16);
			this.esiKeysPagePanel.Name = "esiKeysPagePanel";
			this.esiKeysPagePanel.Size = new System.Drawing.Size(796, 343);
			this.esiKeysPagePanel.TabIndex = 17;
			// 
			// esiKeysMultiPanel
			// 
			this.esiKeysMultiPanel.Controls.Add(this.esiKeysListPage);
			this.esiKeysMultiPanel.Controls.Add(this.noESIKeysPage);
			this.esiKeysMultiPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.esiKeysMultiPanel.Location = new System.Drawing.Point(0, 25);
			this.esiKeysMultiPanel.Name = "esiKeysMultiPanel";
			this.esiKeysMultiPanel.SelectedPage = this.noESIKeysPage;
			this.esiKeysMultiPanel.Size = new System.Drawing.Size(796, 318);
			this.esiKeysMultiPanel.TabIndex = 16;
			// 
			// esiKeysListPage
			// 
			this.esiKeysListPage.BackColor = System.Drawing.SystemColors.Window;
			this.esiKeysListPage.Controls.Add(this.esiKeysListBox);
			this.esiKeysListPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.esiKeysListPage.Location = new System.Drawing.Point(0, 0);
			this.esiKeysListPage.Name = "esiKeysListPage";
			this.esiKeysListPage.Size = new System.Drawing.Size(796, 318);
			this.esiKeysListPage.TabIndex = 0;
			this.esiKeysListPage.Text = "";
			// 
			// esiKeysListBox
			// 
			this.esiKeysListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.esiKeysListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.esiKeysListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.esiKeysListBox.FormattingEnabled = true;
			this.esiKeysListBox.ItemHeight = 46;
			this.esiKeysListBox.Location = new System.Drawing.Point(0, 0);
			this.esiKeysListBox.Name = "esiKeysListBox";
			this.esiKeysListBox.Size = new System.Drawing.Size(796, 318);
			this.esiKeysListBox.TabIndex = 0;
			this.esiKeysListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.apiKeysListBox_MouseClick);
			this.esiKeysListBox.SelectedIndexChanged += new System.EventHandler(this.apiKeysListBox_SelectedIndexChanged);
			this.esiKeysListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.esiKeysListBox_KeyDown);
			this.esiKeysListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.apiKeysListBox_MouseDoubleClick);
			// 
			// noESIKeysPage
			// 
			this.noESIKeysPage.Controls.Add(this.esiKeyListLabel);
			this.noESIKeysPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noESIKeysPage.Location = new System.Drawing.Point(0, 0);
			this.noESIKeysPage.Name = "noESIKeysPage";
			this.noESIKeysPage.Size = new System.Drawing.Size(796, 318);
			this.noESIKeysPage.TabIndex = 1;
			this.noESIKeysPage.Text = "";
			// 
			// esiKeysToolStrip
			// 
			this.esiKeysToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.esiKeysToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addESIKeyMenu,
            this.deleteESIKeyMenu,
            this.editESIKeyMenu});
			this.esiKeysToolStrip.Location = new System.Drawing.Point(0, 0);
			this.esiKeysToolStrip.Name = "esiKeysToolStrip";
			this.esiKeysToolStrip.Size = new System.Drawing.Size(796, 25);
			this.esiKeysToolStrip.TabIndex = 3;
			// 
			// addESIKeyMenu
			// 
			this.addESIKeyMenu.Image = ((System.Drawing.Image)(resources.GetObject("addESIKeyMenu.Image")));
			this.addESIKeyMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addESIKeyMenu.Name = "addESIKeyMenu";
			this.addESIKeyMenu.Size = new System.Drawing.Size(58, 22);
			this.addESIKeyMenu.Text = resources.GetString("this.addESIKeyMenu.Text");
			this.addESIKeyMenu.Click += new System.EventHandler(this.addESIKeyMenu_Click);
			// 
			// deleteESIKeyMenu
			// 
			this.deleteESIKeyMenu.Enabled = false;
			this.deleteESIKeyMenu.Image = ((System.Drawing.Image)(resources.GetObject("deleteESIKeyMenu.Image")));
			this.deleteESIKeyMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteESIKeyMenu.Name = "deleteESIKeyMenu";
			this.deleteESIKeyMenu.Size = new System.Drawing.Size(69, 22);
			this.deleteESIKeyMenu.Text = resources.GetString("this.deleteESIKeyMenu.Text");
			this.deleteESIKeyMenu.Click += new System.EventHandler(this.deleteESIKeyMenu_Click);
			// 
			// editESIKeyMenu
			// 
			this.editESIKeyMenu.Enabled = false;
			this.editESIKeyMenu.Image = ((System.Drawing.Image)(resources.GetObject("editESIKeyMenu.Image")));
			this.editESIKeyMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.editESIKeyMenu.Name = "editESIKeyMenu";
			this.editESIKeyMenu.Size = new System.Drawing.Size(56, 22);
			this.editESIKeyMenu.Text = resources.GetString("this.editESIKeyMenu.Text");
			this.editESIKeyMenu.Click += new System.EventHandler(this.editAPIKeyMenu_Click);
			// 
			// EsiKeysManagementWindow
			// 
			this.AcceptButton = this.closeButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(834, 441);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.closeButton);
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "EsiKeysManagementWindow";
			this.Text = resources.GetString("this.Text");
			this.tabControl.ResumeLayout(false);
			this.charactersTabPage.ResumeLayout(false);
			this.charactersTabPage.PerformLayout();
			this.charactersPagePanel.ResumeLayout(false);
			this.charactersPagePanel.PerformLayout();
			this.charactersMultiPanel.ResumeLayout(false);
			this.charactersListPage.ResumeLayout(false);
			this.noCharactersPage.ResumeLayout(false);
			this.charactersToolStrip.ResumeLayout(false);
			this.charactersToolStrip.PerformLayout();
			this.esiKeysTabPage.ResumeLayout(false);
			this.esiKeysTabPage.PerformLayout();
			this.esiKeysPagePanel.ResumeLayout(false);
			this.esiKeysPagePanel.PerformLayout();
			this.esiKeysMultiPanel.ResumeLayout(false);
			this.esiKeysListPage.ResumeLayout(false);
			this.noESIKeysPage.ResumeLayout(false);
			this.esiKeysToolStrip.ResumeLayout(false);
			this.esiKeysToolStrip.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage esiKeysTabPage;
        private System.Windows.Forms.TabPage charactersTabPage;
        private System.Windows.Forms.ListView charactersListView;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnESIKeyID;
        private System.Windows.Forms.ColumnHeader columnUri;
        private System.Windows.Forms.ColumnHeader columnMonitored;
        private MultiPanel esiKeysMultiPanel;
        private MultiPanelPage esiKeysListPage;
        private MultiPanelPage noESIKeysPage;
        private MultiPanel charactersMultiPanel;
        private MultiPanelPage charactersListPage;
        private MultiPanelPage noCharactersPage;
        private System.Windows.Forms.ToolStrip charactersToolStrip;
        private System.Windows.Forms.ToolStripButton importCharacterMenu;
        private System.Windows.Forms.ToolStripButton deleteCharacterMenu;
        private System.Windows.Forms.ToolStripButton editUriMenu;
        private System.Windows.Forms.ToolStripButton groupingMenu;
        private System.Windows.Forms.ToolStrip esiKeysToolStrip;
        private System.Windows.Forms.ToolStripButton addESIKeyMenu;
        private System.Windows.Forms.ToolStripButton deleteESIKeyMenu;
        private System.Windows.Forms.ToolStripButton editESIKeyMenu;
        private System.Windows.Forms.Panel charactersPagePanel;
        private System.Windows.Forms.Panel esiKeysPagePanel;
        private System.Windows.Forms.Label esiKeysLabel;
        private System.Windows.Forms.Label charactersLabel;
        private System.Windows.Forms.Label esiKeyListLabel;
        private System.Windows.Forms.Label charactersListLabel;
        private EsiKeysListBox esiKeysListBox;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnStatus;
    }
}