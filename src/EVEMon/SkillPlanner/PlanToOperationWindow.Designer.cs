using EVEMon.Common.Controls.MultiPanel;

namespace EVEMon.SkillPlanner
{
    partial class PlanToOperationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanToOperationWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.warningPicture = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.previousButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.rootMultiPanel = new EVEMon.Common.Controls.MultiPanel.MultiPanel();
            this.additionPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
            this.priorityGroup = new System.Windows.Forms.GroupBox();
            this.priorityLabel = new System.Windows.Forms.Label();
            this.lbExisting = new System.Windows.Forms.Label();
            this.priorityNumericBox = new System.Windows.Forms.NumericUpDown();
            this.additionsListBox = new System.Windows.Forms.ListBox();
            this.dependenciesSuppressionPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
            this.suppressionListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uselessPrereqsSuppressionPage = new EVEMon.Common.Controls.MultiPanel.MultiPanelPage();
            this.uselessPrereqsListBox = new System.Windows.Forms.ListBox();
            this.uselessPrereqsCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.warningPicture)).BeginInit();
            this.rootMultiPanel.SuspendLayout();
            this.additionPage.SuspendLayout();
            this.priorityGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priorityNumericBox)).BeginInit();
            this.dependenciesSuppressionPage.SuspendLayout();
            this.uselessPrereqsSuppressionPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("this.label1.Text");
            // 
            // warningPicture
            // 
            this.warningPicture.Image = ((System.Drawing.Image)(resources.GetObject("warningPicture.Image")));
            this.warningPicture.Location = new System.Drawing.Point(3, 3);
            this.warningPicture.Name = "warningPicture";
            this.warningPicture.Size = new System.Drawing.Size(32, 32);
            this.warningPicture.TabIndex = 0;
            this.warningPicture.TabStop = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = resources.GetString("this.label5.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(287, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = resources.GetString("this.label3.Text");
            // 
            // previousButton
            // 
            this.previousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previousButton.Location = new System.Drawing.Point(78, 368);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 23);
            this.previousButton.TabIndex = 2;
            this.previousButton.Text = resources.GetString("this.previousButton.Text");
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(241, 368);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = resources.GetString("this.cancelButton.Text");
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.Location = new System.Drawing.Point(160, 368);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = resources.GetString("this.nextButton.Text");
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // rootMultiPanel
            // 
            this.rootMultiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rootMultiPanel.Controls.Add(this.additionPage);
            this.rootMultiPanel.Controls.Add(this.dependenciesSuppressionPage);
            this.rootMultiPanel.Controls.Add(this.uselessPrereqsSuppressionPage);
            this.rootMultiPanel.Location = new System.Drawing.Point(12, 12);
            this.rootMultiPanel.Name = "rootMultiPanel";
            this.rootMultiPanel.SelectedPage = this.dependenciesSuppressionPage;
            this.rootMultiPanel.Size = new System.Drawing.Size(304, 350);
            this.rootMultiPanel.TabIndex = 0;
            // 
            // additionPage
            // 
            this.additionPage.Controls.Add(this.label5);
            this.additionPage.Controls.Add(this.priorityGroup);
            this.additionPage.Controls.Add(this.priorityNumericBox);
            this.additionPage.Controls.Add(this.label1);
            this.additionPage.Controls.Add(this.additionsListBox);
            this.additionPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.additionPage.Location = new System.Drawing.Point(0, 0);
            this.additionPage.Name = "additionPage";
            this.additionPage.Size = new System.Drawing.Size(304, 350);
            this.additionPage.TabIndex = 0;
            this.additionPage.Text = resources.GetString("this.additionPage.Text");
            // 
            // priorityGroup
            // 
            this.priorityGroup.Controls.Add(this.priorityLabel);
            this.priorityGroup.Controls.Add(this.lbExisting);
            this.priorityGroup.Controls.Add(this.label3);
            this.priorityGroup.Location = new System.Drawing.Point(7, 263);
            this.priorityGroup.Name = "priorityGroup";
            this.priorityGroup.Size = new System.Drawing.Size(294, 84);
            this.priorityGroup.TabIndex = 13;
            this.priorityGroup.TabStop = false;
            this.priorityGroup.Text = resources.GetString("this.priorityGroup.Text");
            // 
            // priorityLabel
            // 
            this.priorityLabel.AutoSize = true;
            this.priorityLabel.Location = new System.Drawing.Point(7, 57);
            this.priorityLabel.Name = "priorityLabel";
            this.priorityLabel.Size = new System.Drawing.Size(178, 13);
            this.priorityLabel.TabIndex = 10;
            this.priorityLabel.Text = resources.GetString("this.priorityLabel.Text");
            // 
            // lbExisting
            // 
            this.lbExisting.AutoSize = true;
            this.lbExisting.Location = new System.Drawing.Point(6, 30);
            this.lbExisting.Name = "lbExisting";
            this.lbExisting.Size = new System.Drawing.Size(251, 13);
            this.lbExisting.TabIndex = 9;
            this.lbExisting.Text = resources.GetString("this.lbExisting.Text");
            // 
            // priorityNumericBox
            // 
            this.priorityNumericBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.priorityNumericBox.Location = new System.Drawing.Point(103, 227);
            this.priorityNumericBox.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.priorityNumericBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.priorityNumericBox.Name = "priorityNumericBox";
            this.priorityNumericBox.Size = new System.Drawing.Size(47, 21);
            this.priorityNumericBox.TabIndex = 12;
            this.priorityNumericBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // additionsListBox
            // 
            this.additionsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.additionsListBox.FormattingEnabled = true;
            this.additionsListBox.Location = new System.Drawing.Point(4, 22);
            this.additionsListBox.Name = "additionsListBox";
            this.additionsListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.additionsListBox.Size = new System.Drawing.Size(297, 199);
            this.additionsListBox.TabIndex = 0;
            // 
            // dependenciesSuppressionPage
            // 
            this.dependenciesSuppressionPage.Controls.Add(this.suppressionListBox);
            this.dependenciesSuppressionPage.Controls.Add(this.label2);
            this.dependenciesSuppressionPage.Controls.Add(this.warningPicture);
            this.dependenciesSuppressionPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dependenciesSuppressionPage.Location = new System.Drawing.Point(0, 0);
            this.dependenciesSuppressionPage.Name = "dependenciesSuppressionPage";
            this.dependenciesSuppressionPage.Size = new System.Drawing.Size(304, 350);
            this.dependenciesSuppressionPage.TabIndex = 0;
            this.dependenciesSuppressionPage.Text = resources.GetString("this.dependenciesSuppressionPage.Text");
            // 
            // suppressionListBox
            // 
            this.suppressionListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suppressionListBox.FormattingEnabled = true;
            this.suppressionListBox.Location = new System.Drawing.Point(4, 41);
            this.suppressionListBox.Name = "suppressionListBox";
            this.suppressionListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.suppressionListBox.Size = new System.Drawing.Size(297, 290);
            this.suppressionListBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("this.label2.Text");
            // 
            // uselessPrereqsSuppressionPage
            // 
            this.uselessPrereqsSuppressionPage.Controls.Add(this.uselessPrereqsListBox);
            this.uselessPrereqsSuppressionPage.Controls.Add(this.uselessPrereqsCheckbox);
            this.uselessPrereqsSuppressionPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uselessPrereqsSuppressionPage.Location = new System.Drawing.Point(0, 0);
            this.uselessPrereqsSuppressionPage.Name = "uselessPrereqsSuppressionPage";
            this.uselessPrereqsSuppressionPage.Size = new System.Drawing.Size(304, 273);
            this.uselessPrereqsSuppressionPage.TabIndex = 1;
            this.uselessPrereqsSuppressionPage.Text = resources.GetString("this.uselessPrereqsSuppressionPage.Text");
            // 
            // uselessPrereqsListBox
            // 
            this.uselessPrereqsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uselessPrereqsListBox.FormattingEnabled = true;
            this.uselessPrereqsListBox.Location = new System.Drawing.Point(4, 28);
            this.uselessPrereqsListBox.Name = "uselessPrereqsListBox";
            this.uselessPrereqsListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.uselessPrereqsListBox.Size = new System.Drawing.Size(297, 225);
            this.uselessPrereqsListBox.TabIndex = 1;
            // 
            // uselessPrereqsCheckbox
            // 
            this.uselessPrereqsCheckbox.AutoSize = true;
            this.uselessPrereqsCheckbox.Location = new System.Drawing.Point(4, 4);
            this.uselessPrereqsCheckbox.Name = "uselessPrereqsCheckbox";
            this.uselessPrereqsCheckbox.Size = new System.Drawing.Size(273, 17);
            this.uselessPrereqsCheckbox.TabIndex = 0;
            this.uselessPrereqsCheckbox.Text = resources.GetString("this.uselessPrereqsCheckbox.Text");
            this.uselessPrereqsCheckbox.UseVisualStyleBackColor = true;
            // 
            // PlanToOperationForm
            // 
            this.AcceptButton = this.nextButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(328, 403);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.rootMultiPanel);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.cancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlanToOperationForm";
            this.Text = resources.GetString("this.Text");
            ((System.ComponentModel.ISupportInitialize)(this.warningPicture)).EndInit();
            this.rootMultiPanel.ResumeLayout(false);
            this.additionPage.ResumeLayout(false);
            this.additionPage.PerformLayout();
            this.priorityGroup.ResumeLayout(false);
            this.priorityGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priorityNumericBox)).EndInit();
            this.dependenciesSuppressionPage.ResumeLayout(false);
            this.dependenciesSuppressionPage.PerformLayout();
            this.uselessPrereqsSuppressionPage.ResumeLayout(false);
            this.uselessPrereqsSuppressionPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MultiPanel rootMultiPanel;
        private MultiPanelPage additionPage;
        private MultiPanelPage dependenciesSuppressionPage;
        private MultiPanelPage uselessPrereqsSuppressionPage;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.ListBox additionsListBox;
        private System.Windows.Forms.ListBox suppressionListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox uselessPrereqsListBox;
        private System.Windows.Forms.CheckBox uselessPrereqsCheckbox;
        private System.Windows.Forms.GroupBox priorityGroup;
        private System.Windows.Forms.Label priorityLabel;
        private System.Windows.Forms.Label lbExisting;
        private System.Windows.Forms.NumericUpDown priorityNumericBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox warningPicture;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}