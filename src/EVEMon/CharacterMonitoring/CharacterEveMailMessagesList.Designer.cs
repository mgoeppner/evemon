using EVEMon.Controls;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterEveMailMessagesList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterEveMailMessagesList));
			this.ilIcons = new System.Windows.Forms.ImageList(this.components);
			this.noEVEMailMessagesLabel = new System.Windows.Forms.Label();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mailReadLocal = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainerMailMessages = new System.Windows.Forms.SplitContainer();
			this.lvMailMessages = new System.Windows.Forms.ListView();
			this.chSenderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chSentDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chToCharacterIDs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chToCorpOrAlliance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chToListID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.eveMailReadingPane = new EVEMon.Controls.ReadingPane();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.contextMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMailMessages)).BeginInit();
			this.splitContainerMailMessages.Panel1.SuspendLayout();
			this.splitContainerMailMessages.Panel2.SuspendLayout();
			this.splitContainerMailMessages.SuspendLayout();
			this.SuspendLayout();
			// 
			// ilIcons
			// 
			this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
			this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.ilIcons.Images.SetKeyName(0, "arrow_up.png");
			this.ilIcons.Images.SetKeyName(1, "arrow_down.png");
			this.ilIcons.Images.SetKeyName(2, "16x16Transparant.png");
			// 
			// noEVEMailMessagesLabel
			// 
			this.noEVEMailMessagesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.noEVEMailMessagesLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.noEVEMailMessagesLabel.Location = new System.Drawing.Point(0, 0);
			this.noEVEMailMessagesLabel.Name = "noEVEMailMessagesLabel";
			this.noEVEMailMessagesLabel.Size = new System.Drawing.Size(454, 434);
			this.noEVEMailMessagesLabel.TabIndex = 1;
			this.noEVEMailMessagesLabel.Text = resources.GetString("this.noEVEMailMessagesLabel.Text");
			this.noEVEMailMessagesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mailReadLocal,
            this.toolStripSeparator,
            this.exportToCSVToolStripMenuItem});
			this.contextMenu.Name = "mailListContextMenu";
			this.contextMenu.Size = new System.Drawing.Size(157, 76);
			this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
			// 
			// mailReadLocal
			// 
			this.mailReadLocal.Name = "mailReadLocal";
			this.mailReadLocal.Size = new System.Drawing.Size(156, 22);
			this.mailReadLocal.Text = resources.GetString("this.mailReadLocal.Text");
			this.mailReadLocal.Click += new System.EventHandler(this.mailReadLocal_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(153, 6);
			// 
			// exportToCSVToolStripMenuItem
			// 
			this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
			this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.exportToCSVToolStripMenuItem.Text = resources.GetString("this.exportToCSVToolStripMenuItem.Text");
			this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
			// 
			// splitContainerMailMessages
			// 
			this.splitContainerMailMessages.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitContainerMailMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMailMessages.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMailMessages.Name = "splitContainerMailMessages";
			this.splitContainerMailMessages.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerMailMessages.Panel1
			// 
			this.splitContainerMailMessages.Panel1.Controls.Add(this.lvMailMessages);
			// 
			// splitContainerMailMessages.Panel2
			// 
			this.splitContainerMailMessages.Panel2.Controls.Add(this.eveMailReadingPane);
			this.splitContainerMailMessages.Size = new System.Drawing.Size(454, 434);
			this.splitContainerMailMessages.SplitterDistance = 288;
			this.splitContainerMailMessages.SplitterWidth = 6;
			this.splitContainerMailMessages.TabIndex = 2;
			// 
			// lvMailMessages
			// 
			this.lvMailMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvMailMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSenderName,
            this.chTitle,
            this.chSentDate,
            this.chToCharacterIDs,
            this.chToCorpOrAlliance,
            this.chToListID});
			this.lvMailMessages.ContextMenuStrip = this.contextMenu;
			this.lvMailMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMailMessages.FullRowSelect = true;
			this.lvMailMessages.HideSelection = false;
			this.lvMailMessages.Location = new System.Drawing.Point(0, 0);
			this.lvMailMessages.MultiSelect = false;
			this.lvMailMessages.Name = "lvMailMessages";
			this.lvMailMessages.Size = new System.Drawing.Size(454, 288);
			this.lvMailMessages.SmallImageList = this.ilIcons;
			this.lvMailMessages.TabIndex = 0;
			this.lvMailMessages.UseCompatibleStateImageBehavior = false;
			this.lvMailMessages.View = System.Windows.Forms.View.Details;
			this.lvMailMessages.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMailMessages_ColumnClick);
			this.lvMailMessages.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvMailMessages_ColumnReordered);
			this.lvMailMessages.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvMailMessages_ColumnWidthChanged);
			this.lvMailMessages.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvMailMessages_ItemSelectionChanged);
			this.lvMailMessages.DoubleClick += new System.EventHandler(this.lvMailMessages_DoubleClick);
			// 
			// chSenderName
			// 
			this.chSenderName.Text = resources.GetString("this.chSenderName.Text");
			// 
			// chTitle
			// 
			this.chTitle.Text = resources.GetString("this.chTitle.Text");
			this.chTitle.Width = 121;
			// 
			// chSentDate
			// 
			this.chSentDate.Text = resources.GetString("this.chSentDate.Text");
			this.chSentDate.Width = 90;
			// 
			// chToCharacterIDs
			// 
			this.chToCharacterIDs.Text = resources.GetString("this.chToCharacterIDs.Text");
			// 
			// chToCorpOrAlliance
			// 
			this.chToCorpOrAlliance.Text = resources.GetString("this.chToCorpOrAlliance.Text");
			// 
			// chToListID
			// 
			this.chToListID.Text = resources.GetString("this.chToListID.Text");
			// 
			// eveMailReadingPane
			// 
			this.eveMailReadingPane.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eveMailReadingPane.Location = new System.Drawing.Point(0, 0);
			this.eveMailReadingPane.Name = "eveMailReadingPane";
			this.eveMailReadingPane.Size = new System.Drawing.Size(454, 140);
			this.eveMailReadingPane.TabIndex = 0;
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// CharacterEveMailMessagesList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerMailMessages);
			this.Controls.Add(this.noEVEMailMessagesLabel);
			this.Name = "CharacterEveMailMessagesList";
			this.Size = new System.Drawing.Size(454, 434);
			this.contextMenu.ResumeLayout(false);
			this.splitContainerMailMessages.Panel1.ResumeLayout(false);
			this.splitContainerMailMessages.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMailMessages)).EndInit();
			this.splitContainerMailMessages.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label noEVEMailMessagesLabel;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ColumnHeader chSenderName;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chSentDate;
        private System.Windows.Forms.ColumnHeader chToCorpOrAlliance;
        private System.Windows.Forms.ColumnHeader chToCharacterIDs;
        private System.Windows.Forms.ColumnHeader chToListID;
        private System.Windows.Forms.SplitContainer splitContainerMailMessages;
        private System.Windows.Forms.ListView lvMailMessages;
        private System.Windows.Forms.Timer timer;
        private ReadingPane eveMailReadingPane;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem mailReadLocal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
    }
}
