using EVEMon.Controls;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterEveNotificationsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterEveNotificationsList));
            this.splitContainerNotifications = new System.Windows.Forms.SplitContainer();
            this.lvNotifications = new System.Windows.Forms.ListView();
            this.chSenderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSentDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.eveNotificationReadingPane = new EVEMon.Controls.ReadingPane();
            this.noEVENotificationsLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerNotifications)).BeginInit();
            this.splitContainerNotifications.Panel1.SuspendLayout();
            this.splitContainerNotifications.Panel2.SuspendLayout();
            this.splitContainerNotifications.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerNotifications
            // 
            this.splitContainerNotifications.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainerNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerNotifications.Location = new System.Drawing.Point(0, 0);
            this.splitContainerNotifications.Name = "splitContainerNotifications";
            this.splitContainerNotifications.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerNotifications.Panel1
            // 
            this.splitContainerNotifications.Panel1.Controls.Add(this.lvNotifications);
            // 
            // splitContainerNotifications.Panel2
            // 
            this.splitContainerNotifications.Panel2.Controls.Add(this.eveNotificationReadingPane);
            this.splitContainerNotifications.Size = new System.Drawing.Size(454, 434);
            this.splitContainerNotifications.SplitterDistance = 288;
            this.splitContainerNotifications.SplitterWidth = 6;
            this.splitContainerNotifications.TabIndex = 3;
            // 
            // lvNotifications
            // 
            this.lvNotifications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvNotifications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSenderName,
            this.chType,
            this.chSentDate});
            this.lvNotifications.ContextMenuStrip = this.contextMenu;
            this.lvNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNotifications.FullRowSelect = true;
            this.lvNotifications.HideSelection = false;
            this.lvNotifications.Location = new System.Drawing.Point(0, 0);
            this.lvNotifications.MultiSelect = false;
            this.lvNotifications.Name = "lvNotifications";
            this.lvNotifications.Size = new System.Drawing.Size(454, 288);
            this.lvNotifications.SmallImageList = this.ilIcons;
            this.lvNotifications.TabIndex = 0;
            this.lvNotifications.UseCompatibleStateImageBehavior = false;
            this.lvNotifications.View = System.Windows.Forms.View.Details;
            this.lvNotifications.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvNotifications_ColumnClick);
            this.lvNotifications.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvNotifications_ColumnReordered);
            this.lvNotifications.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvNotifications_ColumnWidthChanged);
            this.lvNotifications.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvNotifications_ItemSelectionChanged);
            this.lvNotifications.DoubleClick += new System.EventHandler(this.lvNotifications_DoubleClick);
            // 
            // chSenderName
            // 
            this.chSenderName.Text = resources.GetString("this.chSenderName.Text");
            // 
            // chType
            // 
            this.chType.Text = resources.GetString("this.chType.Text");
            this.chType.Width = 121;
            // 
            // chSentDate
            // 
            this.chSentDate.Text = resources.GetString("this.chSentDate.Text");
            this.chSentDate.Width = 90;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToCSVToolStripMenuItem});
            this.contextMenu.Name = "ShipPropertiesContextMenu";
            this.contextMenu.Size = new System.Drawing.Size(157, 48);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exportToCSVToolStripMenuItem.Text = resources.GetString("this.exportToCSVToolStripMenuItem.Text");
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "arrow_up.png");
            this.ilIcons.Images.SetKeyName(1, "arrow_down.png");
            this.ilIcons.Images.SetKeyName(2, "16x16Transparant.png");
            // 
            // eveNotificationReadingPane
            // 
            this.eveNotificationReadingPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eveNotificationReadingPane.Location = new System.Drawing.Point(0, 0);
            this.eveNotificationReadingPane.Name = "eveNotificationReadingPane";
            this.eveNotificationReadingPane.Size = new System.Drawing.Size(454, 140);
            this.eveNotificationReadingPane.TabIndex = 0;
            // 
            // noEVENotificationsLabel
            // 
            this.noEVENotificationsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noEVENotificationsLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.noEVENotificationsLabel.Location = new System.Drawing.Point(0, 0);
            this.noEVENotificationsLabel.Name = "noEVENotificationsLabel";
            this.noEVENotificationsLabel.Size = new System.Drawing.Size(454, 434);
            this.noEVENotificationsLabel.TabIndex = 4;
            this.noEVENotificationsLabel.Text = resources.GetString("this.noEVENotificationsLabel.Text");
            this.noEVENotificationsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // CharacterEveNotificationsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerNotifications);
            this.Controls.Add(this.noEVENotificationsLabel);
            this.Name = "CharacterEveNotificationsList";
            this.Size = new System.Drawing.Size(454, 434);
            this.splitContainerNotifications.Panel1.ResumeLayout(false);
            this.splitContainerNotifications.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerNotifications)).EndInit();
            this.splitContainerNotifications.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerNotifications;
        private System.Windows.Forms.ListView lvNotifications;
        private System.Windows.Forms.ColumnHeader chSenderName;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chSentDate;
        private ReadingPane eveNotificationReadingPane;
        private System.Windows.Forms.Label noEVENotificationsLabel;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
    }
}
