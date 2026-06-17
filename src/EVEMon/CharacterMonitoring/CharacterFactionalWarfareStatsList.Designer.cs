namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterFactionalWarfareStatsList
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Kills", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Victory Points", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Yesterday");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Last Week");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Total");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Yesterday");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Last Week");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Total");
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("General", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Kills", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Victory Points", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Pilots");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Systems Controlled");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Yesterday");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Last Week");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("Total");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("Yesterday");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("Last Week");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Total");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterFactionalWarfareStatsList));
            this.noFactionalWarfareLabel = new System.Windows.Forms.Label();
            this.notEnlistedLabel = new System.Windows.Forms.Label();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.ListSplitContainer = new EVEMon.Common.Controls.PersistentSplitContainer();
            this.lvPersonal = new System.Windows.Forms.ListView();
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chYou = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAll = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMilitia = new System.Windows.Forms.ListView();
            this.chMilitia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmarr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCaldari = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGallente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMinmatar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeaderTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FightingPartiesPanel = new System.Windows.Forms.Panel();
            this.PlusPictureBox = new System.Windows.Forms.PictureBox();
            this.AgainstPictureBox = new System.Windows.Forms.PictureBox();
            this.AllyAgainstFactionPictureBox = new System.Windows.Forms.PictureBox();
            this.PrimeAgainstFactionPictureBox = new System.Windows.Forms.PictureBox();
            this.EnlistedFactionPictureBox = new System.Windows.Forms.PictureBox();
            this.FactionPictureBox = new System.Windows.Forms.PictureBox();
            this.CorporationPictureBox = new System.Windows.Forms.PictureBox();
            this.RankLabel = new System.Windows.Forms.Label();
            this.TimeServedLabel = new System.Windows.Forms.Label();
            this.FactionLabel = new System.Windows.Forms.Label();
            this.CorporationLabel = new System.Windows.Forms.Label();
            this.ListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListSplitContainer)).BeginInit();
            this.ListSplitContainer.Panel1.SuspendLayout();
            this.ListSplitContainer.Panel2.SuspendLayout();
            this.ListSplitContainer.SuspendLayout();
            this.HeaderTableLayoutPanel.SuspendLayout();
            this.FightingPartiesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlusPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgainstPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllyAgainstFactionPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrimeAgainstFactionPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnlistedFactionPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FactionPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CorporationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // noFactionalWarfareLabel
            // 
            this.noFactionalWarfareLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.noFactionalWarfareLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noFactionalWarfareLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noFactionalWarfareLabel.Location = new System.Drawing.Point(0, 0);
            this.noFactionalWarfareLabel.Name = "noFactionalWarfareLabel";
            this.noFactionalWarfareLabel.Size = new System.Drawing.Size(409, 607);
            this.noFactionalWarfareLabel.TabIndex = 3;
            this.noFactionalWarfareLabel.Text = resources.GetString("this.noFactionalWarfareLabel.Text");
            this.noFactionalWarfareLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // notEnlistedLabel
            // 
            this.notEnlistedLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.notEnlistedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notEnlistedLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.notEnlistedLabel.Location = new System.Drawing.Point(0, 0);
            this.notEnlistedLabel.Name = "notEnlistedLabel";
            this.notEnlistedLabel.Size = new System.Drawing.Size(409, 607);
            this.notEnlistedLabel.TabIndex = 4;
            this.notEnlistedLabel.Text = resources.GetString("this.notEnlistedLabel.Text");
            this.notEnlistedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.ListSplitContainer);
            this.ListPanel.Controls.Add(this.HeaderTableLayoutPanel);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(0, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(409, 607);
            this.ListPanel.TabIndex = 6;
            // 
            // ListSplitContainer
            // 
            this.ListSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListSplitContainer.IsSplitterFixed = true;
            this.ListSplitContainer.Location = new System.Drawing.Point(0, 70);
            this.ListSplitContainer.Name = "ListSplitContainer";
            this.ListSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ListSplitContainer.Panel1
            // 
            this.ListSplitContainer.Panel1.Controls.Add(this.lvPersonal);
            // 
            // ListSplitContainer.Panel2
            // 
            this.ListSplitContainer.Panel2.Controls.Add(this.lvMilitia);
            this.ListSplitContainer.RememberDistanceKey = null;
            this.ListSplitContainer.Size = new System.Drawing.Size(409, 537);
            this.ListSplitContainer.SplitterDistance = 232;
            this.ListSplitContainer.SplitterWidth = 1;
            this.ListSplitContainer.TabIndex = 9;
            // 
            // lvPersonal
            // 
            this.lvPersonal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvPersonal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDescription,
            this.chYou,
            this.chAll});
            this.lvPersonal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPersonal.FullRowSelect = true;
            listViewGroup1.Header = "Kills";
            listViewGroup1.Name = "Kills";
            listViewGroup2.Header = "Victory Points";
            listViewGroup2.Name = "Victory Points";
            this.lvPersonal.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lvPersonal.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPersonal.HideSelection = false;
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup1;
            listViewItem4.Group = listViewGroup2;
            listViewItem5.Group = listViewGroup2;
            listViewItem6.Group = listViewGroup2;
            this.lvPersonal.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lvPersonal.Location = new System.Drawing.Point(0, 0);
            this.lvPersonal.MultiSelect = false;
            this.lvPersonal.Name = "lvPersonal";
            this.lvPersonal.Size = new System.Drawing.Size(409, 232);
            this.lvPersonal.TabIndex = 6;
            this.lvPersonal.UseCompatibleStateImageBehavior = false;
            this.lvPersonal.View = System.Windows.Forms.View.Details;
            this.lvPersonal.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvPersonal_ColumnWidthChanging);
            // 
            // chDescription
            // 
            this.chDescription.Text = resources.GetString("this.chDescription.Text");
            this.chDescription.Width = 101;
            // 
            // chYou
            // 
            this.chYou.Text = resources.GetString("this.chYou.Text");
            this.chYou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chAll
            // 
            this.chAll.Text = resources.GetString("this.chAll.Text");
            this.chAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lvMilitia
            // 
            this.lvMilitia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvMilitia.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMilitia,
            this.chAmarr,
            this.chCaldari,
            this.chGallente,
            this.chMinmatar});
            this.lvMilitia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMilitia.FullRowSelect = true;
            listViewGroup3.Header = "General";
            listViewGroup3.Name = "General";
            listViewGroup4.Header = "Kills";
            listViewGroup4.Name = "Kills";
            listViewGroup5.Header = "Victory Points";
            listViewGroup5.Name = "Victory Points";
            this.lvMilitia.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4,
            listViewGroup5});
            this.lvMilitia.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMilitia.HideSelection = false;
            listViewItem7.Group = listViewGroup3;
            listViewItem8.Group = listViewGroup3;
            listViewItem9.Group = listViewGroup4;
            listViewItem10.Group = listViewGroup4;
            listViewItem11.Group = listViewGroup4;
            listViewItem12.Group = listViewGroup5;
            listViewItem13.Group = listViewGroup5;
            listViewItem14.Group = listViewGroup5;
            this.lvMilitia.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14});
            this.lvMilitia.Location = new System.Drawing.Point(0, 0);
            this.lvMilitia.MultiSelect = false;
            this.lvMilitia.Name = "lvMilitia";
            this.lvMilitia.Size = new System.Drawing.Size(409, 304);
            this.lvMilitia.TabIndex = 7;
            this.lvMilitia.UseCompatibleStateImageBehavior = false;
            this.lvMilitia.View = System.Windows.Forms.View.Details;
            this.lvMilitia.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvMilitia_ColumnWidthChanging);
            // 
            // chMilitia
            // 
            this.chMilitia.Text = resources.GetString("this.chMilitia.Text");
            this.chMilitia.Width = 101;
            // 
            // chAmarr
            // 
            this.chAmarr.Text = resources.GetString("this.chAmarr.Text");
            this.chAmarr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chCaldari
            // 
            this.chCaldari.Text = resources.GetString("this.chCaldari.Text");
            this.chCaldari.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chGallente
            // 
            this.chGallente.Text = resources.GetString("this.chGallente.Text");
            this.chGallente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chMinmatar
            // 
            this.chMinmatar.Text = resources.GetString("this.chMinmatar.Text");
            this.chMinmatar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // HeaderTableLayoutPanel
            // 
            this.HeaderTableLayoutPanel.AutoSize = true;
            this.HeaderTableLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.HeaderTableLayoutPanel.ColumnCount = 4;
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.HeaderTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.HeaderTableLayoutPanel.Controls.Add(this.FightingPartiesPanel, 1, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.FactionPictureBox, 0, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.CorporationPictureBox, 3, 0);
            this.HeaderTableLayoutPanel.Controls.Add(this.RankLabel, 1, 2);
            this.HeaderTableLayoutPanel.Controls.Add(this.TimeServedLabel, 2, 2);
            this.HeaderTableLayoutPanel.Controls.Add(this.FactionLabel, 1, 1);
            this.HeaderTableLayoutPanel.Controls.Add(this.CorporationLabel, 2, 1);
            this.HeaderTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderTableLayoutPanel.Name = "HeaderTableLayoutPanel";
            this.HeaderTableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.HeaderTableLayoutPanel.RowCount = 3;
            this.HeaderTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.HeaderTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.HeaderTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.HeaderTableLayoutPanel.Size = new System.Drawing.Size(409, 70);
            this.HeaderTableLayoutPanel.TabIndex = 8;
            // 
            // FightingPartiesPanel
            // 
            this.HeaderTableLayoutPanel.SetColumnSpan(this.FightingPartiesPanel, 2);
            this.FightingPartiesPanel.Controls.Add(this.PlusPictureBox);
            this.FightingPartiesPanel.Controls.Add(this.AgainstPictureBox);
            this.FightingPartiesPanel.Controls.Add(this.AllyAgainstFactionPictureBox);
            this.FightingPartiesPanel.Controls.Add(this.PrimeAgainstFactionPictureBox);
            this.FightingPartiesPanel.Controls.Add(this.EnlistedFactionPictureBox);
            this.FightingPartiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FightingPartiesPanel.Location = new System.Drawing.Point(64, 3);
            this.FightingPartiesPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FightingPartiesPanel.Name = "FightingPartiesPanel";
            this.FightingPartiesPanel.Size = new System.Drawing.Size(280, 35);
            this.FightingPartiesPanel.TabIndex = 7;
            // 
            // PlusPictureBox
            // 
            this.PlusPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PlusPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PlusPictureBox.Image")));
            this.PlusPictureBox.Location = new System.Drawing.Point(156, 2);
            this.PlusPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.PlusPictureBox.Name = "PlusPictureBox";
            this.PlusPictureBox.Size = new System.Drawing.Size(32, 32);
            this.PlusPictureBox.TabIndex = 6;
            this.PlusPictureBox.TabStop = false;
            // 
            // AgainstPictureBox
            // 
            this.AgainstPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.AgainstPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("AgainstPictureBox.Image")));
            this.AgainstPictureBox.Location = new System.Drawing.Point(92, 2);
            this.AgainstPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.AgainstPictureBox.Name = "AgainstPictureBox";
            this.AgainstPictureBox.Size = new System.Drawing.Size(32, 32);
            this.AgainstPictureBox.TabIndex = 5;
            this.AgainstPictureBox.TabStop = false;
            // 
            // SecondAgainstFactionPictureBox
            // 
            this.AllyAgainstFactionPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.AllyAgainstFactionPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SecondAgainstFactionPictureBox.Image")));
            this.AllyAgainstFactionPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("SecondAgainstFactionPictureBox.InitialImage")));
            this.AllyAgainstFactionPictureBox.Location = new System.Drawing.Point(188, 2);
            this.AllyAgainstFactionPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.AllyAgainstFactionPictureBox.Name = "AllyAgainstFactionPictureBox";
            this.AllyAgainstFactionPictureBox.Size = new System.Drawing.Size(32, 32);
            this.AllyAgainstFactionPictureBox.TabIndex = 4;
            this.AllyAgainstFactionPictureBox.TabStop = false;
            // 
            // FirstAgainstFactionPictureBox
            // 
            this.PrimeAgainstFactionPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PrimeAgainstFactionPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("FirstAgainstFactionPictureBox.Image")));
            this.PrimeAgainstFactionPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("FirstAgainstFactionPictureBox.InitialImage")));
            this.PrimeAgainstFactionPictureBox.Location = new System.Drawing.Point(124, 2);
            this.PrimeAgainstFactionPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.PrimeAgainstFactionPictureBox.Name = "PrimeAgainstFactionPictureBox";
            this.PrimeAgainstFactionPictureBox.Size = new System.Drawing.Size(32, 32);
            this.PrimeAgainstFactionPictureBox.TabIndex = 1;
            this.PrimeAgainstFactionPictureBox.TabStop = false;
            // 
            // EnlistedFactionPictureBox
            // 
            this.EnlistedFactionPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.EnlistedFactionPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("EnlistedFactionPictureBox.Image")));
            this.EnlistedFactionPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("EnlistedFactionPictureBox.InitialImage")));
            this.EnlistedFactionPictureBox.Location = new System.Drawing.Point(60, 2);
            this.EnlistedFactionPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.EnlistedFactionPictureBox.Name = "EnlistedFactionPictureBox";
            this.EnlistedFactionPictureBox.Size = new System.Drawing.Size(32, 32);
            this.EnlistedFactionPictureBox.TabIndex = 0;
            this.EnlistedFactionPictureBox.TabStop = false;
            // 
            // FactionPictureBox
            // 
            this.FactionPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("FactionPictureBox.Image")));
            this.FactionPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("FactionPictureBox.InitialImage")));
            this.FactionPictureBox.Location = new System.Drawing.Point(0, 3);
            this.FactionPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.FactionPictureBox.Name = "FactionPictureBox";
            this.HeaderTableLayoutPanel.SetRowSpan(this.FactionPictureBox, 3);
            this.FactionPictureBox.Size = new System.Drawing.Size(64, 64);
            this.FactionPictureBox.TabIndex = 8;
            this.FactionPictureBox.TabStop = false;
            // 
            // CorporationPictureBox
            // 
            this.CorporationPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CorporationPictureBox.Image")));
            this.CorporationPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("CorporationPictureBox.InitialImage")));
            this.CorporationPictureBox.Location = new System.Drawing.Point(344, 3);
            this.CorporationPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.CorporationPictureBox.Name = "CorporationPictureBox";
            this.HeaderTableLayoutPanel.SetRowSpan(this.CorporationPictureBox, 3);
            this.CorporationPictureBox.Size = new System.Drawing.Size(64, 64);
            this.CorporationPictureBox.TabIndex = 9;
            this.CorporationPictureBox.TabStop = false;
            // 
            // RankLabel
            // 
            this.RankLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RankLabel.AutoSize = true;
            this.RankLabel.Location = new System.Drawing.Point(165, 54);
            this.RankLabel.Name = "RankLabel";
            this.RankLabel.Size = new System.Drawing.Size(36, 13);
            this.RankLabel.TabIndex = 3;
            this.RankLabel.Text = resources.GetString("this.RankLabel.Text");
            this.RankLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeServedLabel
            // 
            this.TimeServedLabel.AutoSize = true;
            this.TimeServedLabel.Location = new System.Drawing.Point(207, 54);
            this.TimeServedLabel.Name = "TimeServedLabel";
            this.TimeServedLabel.Size = new System.Drawing.Size(68, 13);
            this.TimeServedLabel.TabIndex = 4;
            this.TimeServedLabel.Text = resources.GetString("this.TimeServedLabel.Text");
            this.TimeServedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FactionLabel
            // 
            this.FactionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FactionLabel.AutoSize = true;
            this.FactionLabel.Location = new System.Drawing.Point(156, 41);
            this.FactionLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.FactionLabel.Name = "FactionLabel";
            this.FactionLabel.Size = new System.Drawing.Size(45, 13);
            this.FactionLabel.TabIndex = 10;
            this.FactionLabel.Text = resources.GetString("this.FactionLabel.Text");
            this.FactionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CorporationLabel
            // 
            this.CorporationLabel.AutoSize = true;
            this.CorporationLabel.Location = new System.Drawing.Point(207, 41);
            this.CorporationLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.CorporationLabel.Name = "CorporationLabel";
            this.CorporationLabel.Size = new System.Drawing.Size(64, 13);
            this.CorporationLabel.TabIndex = 11;
            this.CorporationLabel.Text = resources.GetString("this.CorporationLabel.Text");
            // 
            // CharacterFactionalWarfareStatsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.noFactionalWarfareLabel);
            this.Controls.Add(this.notEnlistedLabel);
            this.Name = "CharacterFactionalWarfareStatsList";
            this.Size = new System.Drawing.Size(409, 607);
            this.ListPanel.ResumeLayout(false);
            this.ListPanel.PerformLayout();
            this.ListSplitContainer.Panel1.ResumeLayout(false);
            this.ListSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListSplitContainer)).EndInit();
            this.ListSplitContainer.ResumeLayout(false);
            this.HeaderTableLayoutPanel.ResumeLayout(false);
            this.HeaderTableLayoutPanel.PerformLayout();
            this.FightingPartiesPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PlusPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgainstPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllyAgainstFactionPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrimeAgainstFactionPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnlistedFactionPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FactionPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CorporationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noFactionalWarfareLabel;
        private System.Windows.Forms.Label notEnlistedLabel;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.ListView lvPersonal;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ColumnHeader chYou;
        private System.Windows.Forms.ColumnHeader chAll;
        private System.Windows.Forms.ListView lvMilitia;
        private System.Windows.Forms.ColumnHeader chMilitia;
        private System.Windows.Forms.ColumnHeader chAmarr;
        private System.Windows.Forms.ColumnHeader chCaldari;
        private System.Windows.Forms.ColumnHeader chGallente;
        private System.Windows.Forms.ColumnHeader chMinmatar;
        private System.Windows.Forms.TableLayoutPanel HeaderTableLayoutPanel;
        private System.Windows.Forms.Label RankLabel;
        private Common.Controls.PersistentSplitContainer ListSplitContainer;
        private System.Windows.Forms.Label TimeServedLabel;
        private System.Windows.Forms.Panel FightingPartiesPanel;
        private System.Windows.Forms.PictureBox AllyAgainstFactionPictureBox;
        private System.Windows.Forms.PictureBox PrimeAgainstFactionPictureBox;
        private System.Windows.Forms.PictureBox EnlistedFactionPictureBox;
        private System.Windows.Forms.PictureBox PlusPictureBox;
        private System.Windows.Forms.PictureBox AgainstPictureBox;
        private System.Windows.Forms.PictureBox FactionPictureBox;
        private System.Windows.Forms.PictureBox CorporationPictureBox;
        private System.Windows.Forms.Label FactionLabel;
        private System.Windows.Forms.Label CorporationLabel;
    }
}
