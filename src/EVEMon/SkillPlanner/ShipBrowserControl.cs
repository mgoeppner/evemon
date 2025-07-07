using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.Data;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Loadouts;
using EVEMon.Common.Models;

namespace EVEMon.SkillPlanner
{
    internal partial class ShipBrowserControl : EveObjectBrowserControl
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShipBrowserControl()
        {
            InitializeComponent();
            scObjectBrowser.RememberDistanceKey = "ShipsBrowser_Left";
            SelectControl = shipSelectControl;
            PropertiesList = lvShipProperties;

            PropertiesList.MouseDown += PropertiesList_MouseDown;
            PropertiesList.MouseMove += PropertiesList_MouseMove;
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// Occurs when the settings changed.
        /// </summary>
        protected override void OnSettingsChanged()
        {
            base.OnSettingsChanged();

            UpdateControlVisibility();
        }

        /// <summary>
        /// Occurs when the conrol loads.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

            if (Character != null && LoadoutsProvider.Providers.Any())
            {
                ToolStripItem[] toolStripItems = LoadoutsProvider.Providers
                    .Select(provider => new ToolStripMenuItem(provider.Name))
                    .ToArray<ToolStripItem>();

                LoadoutsProviderContextMenuStrip.Items.AddRange(toolStripItems);

                foreach (ToolStripMenuItem toolStripItem in LoadoutsProviderContextMenuStrip.Items)
                {
                    toolStripItem.Click += toolStripItem_Click;
                }

                splitButtonLoadouts.Text = Settings.LoadoutsProvider.Provider?.Name;
            }

            lblViewLoadouts.Visible = splitButtonLoadouts.Visible = Character != null && LoadoutsProvider.Providers.Any();

            UpdateControlVisibility();
        }

        /// <summary>
        /// Updates the controls when the selection is changed.
        /// </summary>
        protected override void OnSelectionChanged()
        {
            base.OnSelectionChanged();
            if (SelectedObject == null)
                return;

            // Description
            tbDescription.Text = SelectedObject.Description;

            // Required Skills
            requiredSkillsControl.Object = SelectedObject;

            // Update the Mastery tab
            masteryTreeDisplayControl.MasteryShip = Character?.MasteryShips.GetMasteryShipByID(SelectedObject.ID);

            ShipLoadoutSelectWindow loadoutSelect = WindowsFactory.GetByTag<ShipLoadoutSelectWindow, Character>(Character);
            if (loadoutSelect != null)
                loadoutSelect.Ship = shipSelectControl.SelectedObject;

            // Update the eligibity controls
            UpdateEligibility();
        }

        /// <summary>
        /// Updates whenever the selected plan changed.
        /// </summary>
        protected override void OnSelectedPlanChanged()
        {
            base.OnSelectedPlanChanged();

            requiredSkillsControl.Plan = Plan;
            masteryTreeDisplayControl.Plan = Plan;

            // We recalculate the right panels minimum size
            int reqSkillControlMinWidth = requiredSkillsControl.MinimumSize.Width;
            int reqSkillPanelMinWidth = scDetails.Panel2MinSize;
            scDetails.Panel2MinSize = reqSkillPanelMinWidth > reqSkillControlMinWidth
                ? reqSkillPanelMinWidth
                : reqSkillControlMinWidth;

            UpdateEligibility();
        }

        /// <summary>
        /// Updates whenever the plan changed.
        /// </summary>
        protected override void OnPlanChanged()
        {
            UpdateEligibility();
        }

        #endregion


        #region Local Event Handlers

        /// <summary>
        /// Exports item info to CSV format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewExporter.CreateCSV(PropertiesList, true);
        }

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

        /// <summary>
        /// Occurs when clicking a menu item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem control = sender as ToolStripMenuItem;

            if (control == null)
                return;

            splitButtonLoadouts.Text = control.Text;
            Settings.LoadoutsProvider.ProviderName = control.Text;
        }

        /// <summary>
        /// Occurs when clicking the button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void splitButtonLoadouts_Click(object sender, EventArgs e)
        {
            if (Character == null)
                return;

            if (Settings.LoadoutsProvider.Provider == null)
                return;

            ShipLoadoutSelectWindow loadoutWindow;
            if (Plan != null)
            {
                loadoutWindow = WindowsFactory.GetByTag<ShipLoadoutSelectWindow, Character>(Character);

                if (loadoutWindow == null)
                {
                    loadoutWindow = WindowsFactory.ShowByTag<ShipLoadoutSelectWindow, Plan>(Plan);
                    WindowsFactory.ChangeTag<ShipLoadoutSelectWindow, Plan, Character>(Plan, Character);
                }
                else
                    loadoutWindow = WindowsFactory.ShowByTag<ShipLoadoutSelectWindow, Character>(Character);
            }
            else
                loadoutWindow = WindowsFactory.ShowByTag<ShipLoadoutSelectWindow, Character>(Character);

            loadoutWindow.Ship = SelectedObject;
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void PropertiesList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            PropertiesList.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void PropertiesList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            PropertiesList.Cursor = CustomCursors.ContextMenu;
        }

        /// <summary>
        /// Occurs when double clicking on a list view item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesList_DoubleClick(object sender, EventArgs e)
        {
            // Is it a skill?
            Skill skill = PropertiesList.FocusedItem?.Tag as Skill;

            if (skill != null)
            {
                PlanWindow.ShowPlanWindow(SelectControl.Character, Plan)?.ShowSkillInBrowser(skill);
                return;
            }

            // Is it an item?
            Item item = PropertiesList.FocusedItem?.Tag as Item;

            if (item != null)
                PlanWindow.ShowPlanWindow(SelectControl.Character, Plan)?.ShowItemInBrowser(item);
        }

        /// <summary>
        /// Handles the Opening event of the ItemAttributeContextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ItemAttributeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            // Is it a skill?
            Skill skill = PropertiesList.FocusedItem?.Tag as Skill;
            showInSkillBrowser.Visible = skill != null;

            // Is it an item?
            Item item = PropertiesList.FocusedItem?.Tag as Item;
            showInItemBrowser.Visible = item != null;

            showInMenuSeparator.Visible = skill != null || item != null;
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Replaces the user set search string with another.
        /// </summary>
        /// <param name="text">The text to replace it with.</param>
        internal void SetSearchText(string text)
        {
            shipSelectControl.SearchText = text;
        }

        /// <summary>
        /// Updates the contol visibility.
        /// </summary>
        private void UpdateControlVisibility()
        {
            if (Character == null)
                tbCntrlShipInformation.TabPages.Remove(tbPgShipMastery);

            lblViewLoadouts.Location = Settings.UI.SafeForWork
                ? new Point(Pad, lblViewLoadouts.Location.Y)
                : new Point(eoImage.Width + Pad * 2, lblViewLoadouts.Location.Y);

            splitButtonLoadouts.Location = Settings.UI.SafeForWork
                ? new Point(Pad + lblViewLoadouts.Width, splitButtonLoadouts.Location.Y)
                : new Point(eoImage.Width + lblViewLoadouts.Width + Pad * 2, splitButtonLoadouts.Location.Y);
        }

        /// <summary>
        /// Updates eligibility label and planning menus.
        /// </summary>
        private void UpdateEligibility()
        {
            foreach (ToolStripItem control in tlStrpPlanTo.Items)
            {
                control.Visible = Plan != null;
            }

            // Not visible
            if (SelectedObject == null || Plan == null)
                return;

            MasteryShip masteryShip = masteryTreeDisplayControl.MasteryShip;

            if (masteryShip == null)
                return;

            // First we search the highest eligible mastery level after this plan
            IEnumerable<Mastery> eligibleMasteryLevel =
                masteryShip.TakeWhile(masteryLevel => Plan.WillGrantEligibilityFor(masteryLevel)).ToList();

            Mastery lastEligibleMasteryLevel = null;
            if (!eligibleMasteryLevel.Any())
            {
                tslbEligible.Text = @"(none)";
            }
            else
            {
                lastEligibleMasteryLevel = eligibleMasteryLevel.Last();
                tslbEligible.Text = lastEligibleMasteryLevel.ToString();

                if (masteryShip.HighestTrainedLevel == null)
                {
                    tslbEligible.Text += @" (improved from ""none"")";
                }
                else if (lastEligibleMasteryLevel.Level > masteryShip.HighestTrainedLevel.Level)
                {
                    tslbEligible.Text += string.Format(CultureConstants.DefaultCulture, " (improved from \"{0}\")",
                        masteryShip.HighestTrainedLevel);
                }
                else
                {
                    tslbEligible.Text += @" (no change)";
                }
            }

            // "Plan to N" menus
            for (int i = 1; i <= 5; i++)
            {
                UpdatePlanningMenuStatus(tsPlanToMenu.DropDownItems[i - 1], masteryShip.GetLevel(i), lastEligibleMasteryLevel);
            }
        }

        /// <summary>
        /// Updates a "plan to" menu.
        /// </summary>
        /// <param name="menu">The menu to update</param>
        /// <param name="masteryLevel">The level represent by this menu</param>
        /// <param name="lastEligibleMasteryLevel">The highest eligible mastery after this plan</param>
        private void UpdatePlanningMenuStatus(ToolStripItem menu, Mastery masteryLevel, Mastery lastEligibleMasteryLevel)
        {
            menu.Visible = masteryLevel != null;
            menu.Enabled = masteryLevel != null &&
                           (lastEligibleMasteryLevel == null || masteryLevel.Level > lastEligibleMasteryLevel.Level);

            if (menu.Enabled)
                menu.Tag = Plan.TryPlanTo(masteryLevel);
        }

        #endregion
    }
}
