using EVEMon.Common.Enumerations;

namespace EVEMon.DetailsWindow
{
    sealed partial class ContractDetailsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractDetailsWindow));
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.BidsButton = new System.Windows.Forms.Button();
            this.DetailsPanel = new EVEMon.Common.Controls.NoFlickerPanel();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.ImageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.RoutePanel = new System.Windows.Forms.Panel();
            this.ItemImage = new EVEMon.Common.Controls.EveImage();
            this.CurrentToStartLinkLabel = new System.Windows.Forms.LinkLabel();
            this.CurrentToEndLinkLabel = new System.Windows.Forms.LinkLabel();
            this.StartToEndLinkLabel = new System.Windows.Forms.LinkLabel();
            this.RoutePanelParent = new System.Windows.Forms.Panel();
            this.ButtonPanel.SuspendLayout();
            this.RoutePanelParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.ButtonPanel.Controls.Add(this.BidsButton);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 398);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(464, 29);
            this.ButtonPanel.TabIndex = 5;
            this.ButtonPanel.Visible = false;
            // 
            // BidsButton
            // 
            this.BidsButton.Location = new System.Drawing.Point(185, 3);
            this.BidsButton.Name = "BidsButton";
            this.BidsButton.Size = new System.Drawing.Size(95, 23);
            this.BidsButton.TabIndex = 1;
            this.BidsButton.Text = resources.GetString("this.BidsButton.Text");
            this.BidsButton.UseVisualStyleBackColor = true;
            this.BidsButton.Click += new System.EventHandler(this.BidsButton_Click);
            // 
            // DetailsPanel
            // 
            this.DetailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsPanel.Location = new System.Drawing.Point(0, 0);
            this.DetailsPanel.Name = "DetailsPanel";
            this.DetailsPanel.Size = new System.Drawing.Size(464, 398);
            this.DetailsPanel.TabIndex = 6;
            this.DetailsPanel.Click += new System.EventHandler(this.DetailsPanel_Click);
            this.DetailsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DetailsPanel_Paint);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "itemExchange.png");
            this.ImageList.Images.SetKeyName(1, "courier.png");
            this.ImageList.Images.SetKeyName(2, "auction.png");
            // 
            // ImageListIcons
            // 
            this.ImageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListIcons.ImageStream")));
            this.ImageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListIcons.Images.SetKeyName(0, "arrow_up.png");
            this.ImageListIcons.Images.SetKeyName(1, "arrow_down.png");
            this.ImageListIcons.Images.SetKeyName(2, "16x16Transparant.png");
            // 
            // RoutePanel
            // 
            this.RoutePanel.Location = new System.Drawing.Point(0, 0);
            this.RoutePanel.Margin = new System.Windows.Forms.Padding(0);
            this.RoutePanel.Name = "RoutePanel";
            this.RoutePanel.Size = new System.Drawing.Size(10, 10);
            this.RoutePanel.TabIndex = 7;
            this.RoutePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.RoutePanel_Paint);
            // 
            // ItemImage
            // 
            this.ItemImage.ImageSize = EVEMon.Common.Enumerations.EveImageSize.x64;
            this.ItemImage.Location = new System.Drawing.Point(0, 0);
            this.ItemImage.Name = "ItemImage";
            this.ItemImage.PopUpEnabled = true;
            this.ItemImage.Size = new System.Drawing.Size(64, 64);
            this.ItemImage.SizeMode = EVEMon.Common.Enumerations.EveImageSizeMode.Normal;
            this.ItemImage.TabIndex = 8;
            this.ItemImage.Visible = false;
            this.ItemImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ItemImage_MouseClick);
            // 
            // CurrentToStartLinkLabel
            // 
            this.CurrentToStartLinkLabel.AutoSize = true;
            this.CurrentToStartLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 10);
            this.CurrentToStartLinkLabel.Location = new System.Drawing.Point(0, 0);
            this.CurrentToStartLinkLabel.Name = "CurrentToStartLinkLabel";
            this.CurrentToStartLinkLabel.Size = new System.Drawing.Size(59, 17);
            this.CurrentToStartLinkLabel.TabIndex = 9;
            this.CurrentToStartLinkLabel.TabStop = true;
            this.CurrentToStartLinkLabel.Text = resources.GetString("this.CurrentToStartLinkLabel.Text");
            this.CurrentToStartLinkLabel.UseCompatibleTextRendering = true;
            this.CurrentToStartLinkLabel.Visible = false;
            this.CurrentToStartLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CurrentToStartLinkLabel_LinkClicked);
            // 
            // CurrentToEndLinkLabel
            // 
            this.CurrentToEndLinkLabel.AutoSize = true;
            this.CurrentToEndLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 10);
            this.CurrentToEndLinkLabel.Location = new System.Drawing.Point(0, 0);
            this.CurrentToEndLinkLabel.Name = "CurrentToEndLinkLabel";
            this.CurrentToEndLinkLabel.Size = new System.Drawing.Size(59, 17);
            this.CurrentToEndLinkLabel.TabIndex = 10;
            this.CurrentToEndLinkLabel.TabStop = true;
            this.CurrentToEndLinkLabel.Text = resources.GetString("this.CurrentToEndLinkLabel.Text");
            this.CurrentToEndLinkLabel.UseCompatibleTextRendering = true;
            this.CurrentToEndLinkLabel.Visible = false;
            this.CurrentToEndLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CurrentToEndLinkLabel_LinkClicked);
            // 
            // StartToEndLinkLabel
            // 
            this.StartToEndLinkLabel.AutoSize = true;
            this.StartToEndLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 10);
            this.StartToEndLinkLabel.Location = new System.Drawing.Point(0, 0);
            this.StartToEndLinkLabel.Name = "StartToEndLinkLabel";
            this.StartToEndLinkLabel.Size = new System.Drawing.Size(59, 17);
            this.StartToEndLinkLabel.TabIndex = 11;
            this.StartToEndLinkLabel.TabStop = true;
            this.StartToEndLinkLabel.Text = resources.GetString("this.StartToEndLinkLabel.Text");
            this.StartToEndLinkLabel.UseCompatibleTextRendering = true;
            this.StartToEndLinkLabel.Visible = false;
            this.StartToEndLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StartToEndLinkLabel_LinkClicked);
            // 
            // RoutePanelParent
            // 
            this.RoutePanelParent.AutoScroll = true;
            this.RoutePanelParent.Controls.Add(this.RoutePanel);
            this.RoutePanelParent.Dock = System.Windows.Forms.DockStyle.Right;
            this.RoutePanelParent.Location = new System.Drawing.Point(464, 0);
            this.RoutePanelParent.Name = "RoutePanelParent";
            this.RoutePanelParent.Size = new System.Drawing.Size(0, 427);
            this.RoutePanelParent.TabIndex = 0;
            this.RoutePanelParent.Visible = false;
            // 
            // ContractDetailsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 427);
            this.Controls.Add(this.DetailsPanel);
            this.Controls.Add(this.ButtonPanel);
            this.Controls.Add(this.CurrentToStartLinkLabel);
            this.Controls.Add(this.CurrentToEndLinkLabel);
            this.Controls.Add(this.StartToEndLinkLabel);
            this.Controls.Add(this.ItemImage);
            this.Controls.Add(this.RoutePanelParent);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 1280);
            this.MinimumSize = new System.Drawing.Size(480, 465);
            this.Name = "ContractDetailsWindow";
            this.Text = resources.GetString("this.Text");
            this.ButtonPanel.ResumeLayout(false);
            this.RoutePanelParent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Button BidsButton;
        private Common.Controls.NoFlickerPanel DetailsPanel;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.ImageList ImageListIcons;
        private System.Windows.Forms.Panel RoutePanel;
        private Common.Controls.EveImage ItemImage;
        private System.Windows.Forms.LinkLabel CurrentToStartLinkLabel;
        private System.Windows.Forms.LinkLabel CurrentToEndLinkLabel;
        private System.Windows.Forms.LinkLabel StartToEndLinkLabel;
        private System.Windows.Forms.Panel RoutePanelParent;
    }
}