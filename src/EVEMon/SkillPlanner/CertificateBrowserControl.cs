using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;

namespace EVEMon.SkillPlanner
{
    public partial class CertificateBrowserControl : UserControl
    {
        private CertificateClass m_selectedCertificate;
        private Plan m_plan;
        private const int HPad = 40;

        /// <summary>
        /// Constructor
        /// </summary>
        public CertificateBrowserControl()
        {
            InitializeComponent();

            rightSplitContainer.RememberDistanceKey = "CertificateBrowser_Right";
            leftSplitContainer.RememberDistanceKey = "CertificateBrowser_Left";
        }


        #region Inherited Events

        /// <summary>
        /// On load.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

            lblName.Font = new Font(lblName.Font, FontStyle.Bold);

            certSelectControl.SelectionChanged += certSelectControl_SelectionChanged;

            EveMonClient.PlanChanged += EveMonClient_PlanChanged;
            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            Disposed += OnDisposed;

            // Reposition the help text along side the treeview
            Control[] result = certSelectControl.Controls.Find("pnlResults", true);
            if (result.Length > 0)
                lblHelp.Location = new Point(lblHelp.Location.X, result[0].Location.Y);

            // Update the controls visibility
            UpdateControlVisibility();
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            certSelectControl.SelectionChanged -= certSelectControl_SelectionChanged;

            EveMonClient.PlanChanged -= EveMonClient_PlanChanged;
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            Disposed -= OnDisposed;
        }

        #endregion


        #region Internal Properties

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        internal Character Character
        {
            get { return certSelectControl.Character; }
            set
            {
                if (value == null || certSelectControl.Character == value)
                    return;

                certSelectControl.Character = value;
            }
        }

        /// <summary>
        /// Gets or sets the current plan
        /// </summary>
        internal Plan Plan
        {
            get { return m_plan; }
            set
            {
                if (m_plan == value)
                    return;

                m_plan = value;

                certSelectControl.Plan = m_plan;
                certDisplayControl.Plan = m_plan;

                UpdateEligibility();
            }
        }

        /// <summary>
        /// Gets or sets the selected certificate class.
        /// </summary>
        /// <value>
        /// The selected certificate class.
        /// </value>
        internal CertificateClass SelectedCertificateClass
        {
            get { return m_selectedCertificate; }
            set
            {
                if (m_selectedCertificate == value)
                    return;

                m_selectedCertificate = value;
                certDisplayControl.CertificateClass = value;
                certSelectControl.SelectedCertificateClass = value;
                UpdateContent();
            }
        }

        #endregion


        #region Content Update

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateContent()
        {
            // When no certificate class is selected, we just hide the right panel.
            if (m_selectedCertificate == null)
            {
                // View help message
                lblHelp.Visible = true;

                panelRight.Visible = false;
                return;
            }

            // Hide help message
            lblHelp.Visible = false;

            // Updates controls visibility
            panelRight.Visible = true;

            lblName.Text = m_selectedCertificate.Name;
            lblCategory.Text = m_selectedCertificate.Category.Name;
            textboxDescription.Text = m_selectedCertificate.Certificate.Description;

            // Training time per certificate level
            for (int i = 1; i <= 5; i++)
            {
                UpdateLevelLabel(panelHeader.Controls.OfType<Label>()
                    .First(label => label.Name == $"lblLevel{i}Time"), i);
            }

            // Only read the recommendations from one level, because they are all the same
            PersistentSplitContainer rSplCont = rightSplitContainer;
            List<Control> newItems = new List<Control>();
            SortedList<string, Item> ships = new SortedList<string, Item>();
            foreach (Item ship in m_selectedCertificate.Certificate.Recommendations)
            {
                ships.Add(ship.Name, ship);
            }

            Label tempLabel = null;
            try
            {
                tempLabel = new Label();
                tempLabel.Font = new Font(tempLabel.Font, FontStyle.Bold);
                tempLabel.AutoSize = true;
                tempLabel.Dock = DockStyle.Top;
                tempLabel.Text = @"Recommended For";
                tempLabel.Padding = new Padding(5);

                Label tsl = tempLabel;
                tempLabel = null;

                newItems.Add(tsl);

                Size tslTextSize = TextRenderer.MeasureText(tsl.Text, Font);
                int panelMinSize = rSplCont.Panel2MinSize;
                rSplCont.Panel2MinSize = panelMinSize > tslTextSize.Width + HPad
                    ? panelMinSize
                    : tslTextSize.Width + HPad;
                rSplCont.SplitterDistance = rSplCont.Width - rSplCont.Panel2MinSize;
            }
            finally
            {
                tempLabel?.Dispose();
            }

            foreach (LinkLabel linkLabel in ships.Values
                .Select(ship =>
                {
                    LinkLabel linkLabel;
                    LinkLabel tempLinkLabel = null;
                    try
                    {
                        tempLinkLabel = new LinkLabel();
                        tempLinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                        tempLinkLabel.Padding = new Padding(16, 0, 0, 0);
                        tempLinkLabel.Dock = DockStyle.Top;
                        tempLinkLabel.Text = ship.Name;
                        tempLinkLabel.Tag = ship;

                        linkLabel = tempLinkLabel;
                        tempLinkLabel = null;
                    }
                    finally
                    {
                        tempLinkLabel?.Dispose();
                    }

                    return linkLabel;
                }))
            {
                linkLabel.MouseClick += recommendations_MenuItem;
                newItems.Add(linkLabel);
            }

            // Updates the recommendations for this certificate
            UpdateRecommendations(newItems, rSplCont);

            // Update the menus and such
            UpdateEligibility();

            // Update the certificates tree display
            certDisplayControl.CertificateClass = m_selectedCertificate;
        }

        /// <summary>
        /// Updates the provided label with the training time to the given level.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="level">The level.</param>
        private void UpdateLevelLabel(Control label, int level)
        {
            label.Visible = m_selectedCertificate?.Character != null;

            if (m_selectedCertificate?.Character == null)
                return;

            StringBuilder sb = new StringBuilder();

            // "Level III: "
            sb.Append($"Level {Skill.GetRomanFromInt(level)}: ");

            CertificateLevel certificateLevel = m_selectedCertificate.Certificate.GetCertificateLevel(level);

            // Is it already trained ?
            if (certificateLevel.IsTrained)
            {
                label.Text = sb.Append("Already trained").ToString();
                return;
            }

            // Training time left for level
            sb.Append(certificateLevel.GetTrainingTime.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas));

            label.Text = sb.ToString();
        }

        /// <summary>
        /// Updates the recommendations.
        /// </summary>
        /// <param name="newItems">The new items list.</param>
        /// <param name="rSplCont">The right splitter container.</param>
        private void UpdateRecommendations(List<Control> newItems, SplitContainer rSplCont)
        {
            rightSplitContainer.Panel2.Controls.Clear();

            if (newItems.Count != 0)
            {
                newItems.Reverse();
                rSplCont.Panel2.Controls.AddRange(newItems.ToArray());
            }
            else
            {
                Label tempLabel = null;
                try
                {
                    tempLabel = new Label();
                    tempLabel.Text = @"No Recommendations";
                    tempLabel.Enabled = false;
                    tempLabel.Dock = DockStyle.Fill;
                    tempLabel.TextAlign = ContentAlignment.MiddleCenter;

                    Label tsl = tempLabel;
                    tempLabel = null;

                    rSplCont.Panel2.Controls.Add(tsl);

                    Size tslTextSize = TextRenderer.MeasureText(tsl.Text, Font);
                    rSplCont.Panel2MinSize = tslTextSize.Width + HPad;
                    rSplCont.SplitterDistance = rSplCont.Width - rSplCont.Panel2MinSize;
                }
                finally
                {
                    tempLabel?.Dispose();
                }
            }
        }

        /// <summary>
        /// Updates eligibility label and planning menus.
        /// </summary>
        private void UpdateEligibility()
        {
            foreach (ToolStripItem control in tspControl.Items)
            {
                control.Visible = m_plan != null;
            }

            // Not visible
            if (m_selectedCertificate == null || m_plan == null)
                return;

            // First we search the highest eligible certificate level after this plan
            IList<CertificateLevel> eligibleCertLevel = m_selectedCertificate.Certificate.AllLevel
                .TakeWhile(cert => m_plan.WillGrantEligibilityFor(cert)).ToList();

            CertificateLevel lastEligibleCertLevel = null;
            if (!eligibleCertLevel.Any())
                tslbEligible.Text = @"(none)";
            else
            {
                lastEligibleCertLevel = eligibleCertLevel.Last();
                tslbEligible.Text = lastEligibleCertLevel.ToString();

                tslbEligible.Text += m_selectedCertificate.HighestTrainedLevel == null
                    ? " (improved from \"none\")"
                    : (int)lastEligibleCertLevel.Level > (int)m_selectedCertificate.HighestTrainedLevel.Level
                        ? $" (improved from \"{m_selectedCertificate.HighestTrainedLevel}\")"
                        : @" (no change)";
            }

            planToLevel.Enabled = false;

            // "Plan to N" menus
            for (int i = 1; i <= 5; i++)
            {
                planToLevel.Enabled |= UpdatePlanningMenuStatus(planToLevel.DropDownItems[i - 1],
                    m_selectedCertificate.Certificate.GetCertificateLevel(i), lastEligibleCertLevel);
            }
        }

        /// <summary>
        /// Updates a "plan to" menu.
        /// </summary>
        /// <param name="menu">The menu to update</param>
        /// <param name="certLevel">The level represent by this menu</param>
        /// <param name="lastEligibleCertLevel">The highest eligible certificate after this plan</param>
        private bool UpdatePlanningMenuStatus(ToolStripItem menu, CertificateLevel certLevel, CertificateLevel lastEligibleCertLevel)
        {
            menu.Enabled = certLevel != null && (lastEligibleCertLevel == null || certLevel.Level > lastEligibleCertLevel.Level);

            if (menu.Enabled)
                menu.Tag = m_plan.TryPlanTo(certLevel);

            return menu.Enabled;
        }

        #endregion


        #region Control Events

        /// <summary>
        /// When the user select a new certificate class, we update everything.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void certSelectControl_SelectionChanged(object sender, EventArgs e)
        {
            SelectedCertificateClass = certSelectControl.SelectedCertificateClass;
        }

        /// <summary>
        /// Handler for the ship-links generated for the recommendations.
        /// </summary>
        private void recommendations_MenuItem(object sender, EventArgs e)
        {
            Item ship = ((Control)sender)?.Tag as Item;

            // Open the ship browser
            PlanWindow.ShowPlanWindow(certSelectControl.Character, m_plan).ShowShipInBrowser(ship);
        }

        #endregion


        #region Golbal Event Handlers

        /// <summary>
        /// When the current plan changes (new skills, etc), we need to update some informations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_PlanChanged(object sender, PlanChangedEventArgs e)
        {
            if (e.Plan == m_plan)
                UpdateEligibility();
        }

        /// <summary>
        /// When the settings changes, we need to update.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            UpdateControlVisibility();
        }

        #endregion


        #region Context menu

        /// <summary>
        /// Plan to Level N.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tsPlanToLevel_Click(object sender, EventArgs e)
        {
            IPlanOperation operation = ((ToolStripMenuItem)sender).Tag as IPlanOperation;
            if (operation == null)
                return;

            PlanWindow planWindow = ParentForm as PlanWindow;
            if (planWindow == null)
                return;

            PlanHelper.SelectPerform(new PlanToOperationWindow(operation), planWindow, operation);
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Updates the control visibility.
        /// </summary>
        private void UpdateControlVisibility()
        {
            if (Settings.UI.SafeForWork)
            {
                pictureBox1.Visible = false;
                planToLevel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }
            else
            {
                pictureBox1.Visible = true;
                planToLevel.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
        }
        
        #endregion

    }
}