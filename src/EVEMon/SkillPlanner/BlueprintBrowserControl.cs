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
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Models;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.SkillPlanner
{
    internal partial class BlueprintBrowserControl : EveObjectBrowserControl
    {
        private double m_materialFacilityMultiplier;
        private bool m_hasManufacturing;
        private bool m_hasCopying;
        private bool m_hasResearchingMaterialEfficiency;
        private bool m_hasResearchingTimeEfficiency;
        private bool m_hasInvention;
        private bool m_hasReactions;

        private Blueprint m_blueprint;
        private BlueprintActivity m_activity;
        private readonly Point m_gbManufOriginalLocation;
        private readonly Point m_gbResearchingOriginalLocation;
        private readonly Point m_gbInventionOriginalLocation;
        private readonly Point m_gbReactionsOriginalLocation;

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlueprintBrowserControl()
        {
            InitializeComponent();

            scObjectBrowser.RememberDistanceKey = "BlueprintBrowser_Left";
            SelectControl = blueprintSelectControl;
            PropertiesList = lvManufacturing;

            PropertiesList.MouseDown += PropertiesList_MouseDown;
            PropertiesList.MouseMove += PropertiesList_MouseMove;

            m_gbManufOriginalLocation = gbManufacturing.Location;
            m_gbResearchingOriginalLocation = gbResearching.Location;
            m_gbInventionOriginalLocation = gbInvention.Location;
            m_gbReactionsOriginalLocation = gbReactions.Location;
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// Occurs when the control is loaded.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // Call the base method
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

            lblNoItemManufacturing.Font = new Font(lblNoItemManufacturing.Font.FontFamily, 11.25F, FontStyle.Bold);
            lblNoItemCopy.Font = new Font(lblNoItemCopy.Font.FontFamily, 11.25F, FontStyle.Bold);
            lblNoItemME.Font = new Font(lblNoItemME.Font.FontFamily, 11.25F, FontStyle.Bold);
            lblNoItemTE.Font = new Font(lblNoItemTE.Font.FontFamily, 11.25F, FontStyle.Bold);
            lblNoItemInvention.Font = new Font(lblNoItemInvention.Font.FontFamily, 11.25F, FontStyle.Bold);

            lblHelp.Text = Properties.Resources.MessageBlueprintSelect;
            gbDescription.Text = @"Attributes";
            pnlAttributes.AutoScroll = true;

            // Update ImplantSet Modifier
            UpdateImplantSetModifier();
        }

        /// <summary>
        /// Updates the controls when the selection is changed.
        /// </summary>
        protected override void OnSelectionChanged()
        {
            // Call the base method
            base.OnSelectionChanged();

            if (SelectedObject == null)
                return;

            m_blueprint = SelectedObject as Blueprint;

            // Update Tabs
            UpdateTabs();

            // Get the acitvity
            m_activity = GetActivity();

            // Update Required Skills
            requiredSkillsControl.Object = SelectedObject;
            requiredSkillsControl.Activity = m_activity;

            // Update Facility Modifier
            UpdateFacilityModifier();
        }

        /// <summary>
        /// Updates whenever the selected plan changed.
        /// </summary>
        protected override void OnSelectedPlanChanged()
        {
            base.OnSelectedPlanChanged();

            if (Plan == null)
            {
                gbRequiredSkills.Hide();
                return;
            }

            requiredSkillsControl.Plan = Plan;

            // We recalculate the right panels minimum size
            int reqSkillControlMinWidth = requiredSkillsControl.MinimumSize.Width;
            int reqSkillPanelMinWidth = scDetails.Panel2MinSize;
            scDetails.Panel2MinSize = reqSkillPanelMinWidth > reqSkillControlMinWidth ?
                reqSkillPanelMinWidth : reqSkillControlMinWidth;
        }

        /// <summary>
        /// Updates the implant modifier list when the settings changed.
        /// </summary>
        protected override void OnSettingsChanged()
        {
            base.OnSettingsChanged();
            UpdateImplantSetModifier();
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Replaces the user set search string with another.
        /// </summary>
        /// <param name="text">The text to replace it with.</param>
        internal void SetSearchText(string text)
        {
            blueprintSelectControl.SearchText = text;
        }

        /// <summary>
        /// Create the necessary tabs.
        /// </summary>
        private void UpdateTabs()
        {
            // Hide the tab control if the selected tab is not the first
            // (avoids tab creation be visible to user)
            if (tabControl.SelectedIndex != 0)
                tabControl.Hide();

            tabControl.SuspendLayout();
            try
            {
                RefreshTabs();
            }
            finally
            {
                tabControl.ResumeLayout(true);
                tabControl.Show();
            }
        }

        /// <summary>
        /// Refreshes the tabs.
        /// </summary>
        private void RefreshTabs()
        {
            // Determine the blueprints' activities  
            SetActivities();

            // Store the visible selector control for later use
            Control visibleSelector;
            if (blueprintSelectControl.tvItems.Visible)
                visibleSelector = blueprintSelectControl.tvItems;
            else
                visibleSelector = blueprintSelectControl.lbSearchList;

            // Store the selected tab index for later use
            int storedTabIndex = tabControl.SelectedIndex;

            tabControl.TabPages.Clear();

            // Add the appropriate tabs
            if (m_hasManufacturing)
                tabControl.TabPages.Add(tpManufacturing);

            if (m_hasResearchingMaterialEfficiency)
                tabControl.TabPages.Add(tpResearchME);

            if (m_hasResearchingTimeEfficiency)
                tabControl.TabPages.Add(tpResearchTE);

            if (m_hasCopying)
                tabControl.TabPages.Add(tpCopying);

            if (m_hasInvention)
                tabControl.TabPages.Add(tpInvention);

            if (m_hasReactions)
                tabControl.TabPages.Add(tpReactions);

            // Restore the index of the previous selected tab,
            // if the index doesn't exist it smartly selects
            // the first one by its own
            tabControl.SelectedIndex = storedTabIndex;

            // Return focus to selector
            visibleSelector.Focus();
        }

        /// <summary>
        /// Sets the activities.
        /// </summary>
        private void SetActivities()
        {
            bool manufacturing = false, copying = false, me = false, te = false, invention =
                false, reactions = false;
            // Search prerequisites for activities
            foreach (var prereq in m_blueprint.Prerequisites)
                switch (prereq.Activity)
                {
                    case BlueprintActivity.Manufacturing:
                        manufacturing = true;
                        break;
                    case BlueprintActivity.Copying:
                        copying = true;
                        break;
                    case BlueprintActivity.ResearchingMaterialEfficiency:
                        me = true;
                        break;
                    case BlueprintActivity.ResearchingTimeEfficiency:
                        te = true;
                        break;
                    case BlueprintActivity.Invention:
                        invention = true;
                        break;
                    case BlueprintActivity.SimpleReactions:
                        reactions = true;
                        break;
                    case BlueprintActivity.Reactions:
                        reactions = true;
                        break;
                    default:
                        break;
                }
            // Search materials for activities
            foreach (var prereq in m_blueprint.MaterialRequirements)
                switch (prereq.Activity)
                {
                    case BlueprintActivity.Manufacturing:
                        manufacturing = true;
                        break;
                    case BlueprintActivity.Copying:
                        copying = true;
                        break;
                    case BlueprintActivity.ResearchingMaterialEfficiency:
                        me = true;
                        break;
                    case BlueprintActivity.ResearchingTimeEfficiency:
                        te = true;
                        break;
                    case BlueprintActivity.Invention:
                        invention = true;
                        break;
                    case BlueprintActivity.SimpleReactions:
                        reactions = true;
                        break;
                    case BlueprintActivity.Reactions:
                        reactions = true;
                        break;
                    default:
                        break;
                }

            m_hasManufacturing = manufacturing && m_blueprint.ProductionTime > 0d;
            m_hasCopying = copying && m_blueprint.ResearchCopyTime > 0d;
            m_hasResearchingMaterialEfficiency = me && m_blueprint.ResearchMaterialTime > 0d;
            m_hasResearchingTimeEfficiency = te && m_blueprint.ResearchProductivityTime > 0d;
            m_hasInvention = invention && m_blueprint.ResearchInventionTime > 0d;
            m_hasReactions = reactions && m_blueprint.ReactionTime > 0d;
        }

        /// <summary>
        /// Update the attributes info.
        /// </summary>
        private void UpdateAttributes()
        {
            // Produce item
            lblItem.ForeColor = Color.Blue;
            if ((m_blueprint.ProducesItem?.ID ?? 0) != 0)
            {
                var item = m_blueprint.ProducesItem;
                lblItem.Text = item.Name;
                lblItem.Tag = item;
            }
            else if (m_blueprint.ReactionOutcome?.Item != null)
            {
                var item = m_blueprint.ReactionOutcome.Item;
                lblItem.Text = item.Name;
                lblItem.Tag = item;
            }

            // Invented blueprints
            InventBlueprintListBox.Items.Clear();
            foreach (KeyValuePair<Blueprint, decimal> item in m_blueprint.InventBlueprints)
            {
                InventBlueprintListBox.Items.Add(item.Key);
            }

            // Success probability
            lblSuccessProbability.Visible = lblProbability.Visible = m_blueprint.InventBlueprints.Any();
            if (lblProbability.Visible)
            {
                decimal baseProbability = m_blueprint.InventBlueprints.Max(x => x.Value);
                lblProbability.Text = $"{baseProbability:P1} (You: {baseProbability * GetProbabilityModifier():P1})";
            }

            // Runs per copy
            lblRunsPerCopy.Text = m_blueprint.RunsPerCopy.ToString(CultureConstants.DefaultCulture);

            // Manufacturing base time
            double activityTime = (int)(m_blueprint.ProductionTime * GetTimeEfficiencyModifier(
                BlueprintActivity.Manufacturing)) * GetFacilityManufacturingAndMaterialMultiplier();
            lblProductionBaseTime.Text = BaseActivityTime(activityTime);

            // Reaction base time
            double reactionTime = (int)(m_blueprint.ReactionTime * GetTimeEfficiencyModifier(
                BlueprintActivity.Reactions)) * GetFacilityManufacturingAndMaterialMultiplier();
            lblReactionBaseTime.Text = BaseActivityTime(reactionTime);

            // Manufacturing character time
            activityTime *= GetImplantMultiplier(DBConstants.ManufacturingModifyingImplantIDs);
            lblProductionCharTime.Text = CharacterActivityTime(activityTime, DBConstants.
                IndustrySkillID, DBConstants.AdvancedIndustrySkillID);

            // Reactions character time
            reactionTime *= GetImplantMultiplier(DBConstants.ManufacturingModifyingImplantIDs);
            lblReactionCharTime.Text = CharacterActivityTime(activityTime, DBConstants.
                ReactionsSkillID);

            // Researching material efficiency base time
            activityTime = (int)(m_blueprint.ResearchMaterialTime * GetTimeEfficiencyModifier(
                BlueprintActivity.ResearchingMaterialEfficiency)) * GetFacilityResearchTimeMultiplier(
                BlueprintActivity.ResearchingMaterialEfficiency);
            lblResearchMEBaseTime.Text = BaseActivityTime(activityTime);

            // Researching material efficiency character time
            activityTime *= GetImplantMultiplier(DBConstants.ResearchMaterialEfficiencyTimeModifyingImplantIDs);
            lblResearchMECharTime.Text = CharacterActivityTime(activityTime, DBConstants.
                MetallurgySkillID, DBConstants.AdvancedIndustrySkillID);

            // Researching copy base time
            activityTime = (int)(m_blueprint.ResearchCopyTime * GetTimeEfficiencyModifier(
                BlueprintActivity.Copying)) * GetFacilityResearchTimeMultiplier(BlueprintActivity.Copying);
            lblResearchCopyBaseTime.Text = BaseActivityTime(activityTime);

            // Researching copy character time
            activityTime *= GetImplantMultiplier(DBConstants.ResearchCopyTimeModifyingImplantIDs);
            lblResearchCopyCharTime.Text = CharacterActivityTime(activityTime, DBConstants.
                ScienceSkillID, DBConstants.AdvancedIndustrySkillID);

            // Researching time efficiency base time
            activityTime = (int)(m_blueprint.ResearchProductivityTime *
                                 GetTimeEfficiencyModifier(BlueprintActivity.ResearchingTimeEfficiency)) *
                           GetFacilityResearchTimeMultiplier(BlueprintActivity.ResearchingTimeEfficiency);
            lblResearchTEBaseTime.Text = BaseActivityTime(activityTime);

            // Researching time efficiency character time
            activityTime *= GetImplantMultiplier(DBConstants.ResearchTimeEfficiencyTimeModifyingImplantIDs);
            lblResearchTECharTime.Text = CharacterActivityTime(activityTime, DBConstants.
                ResearchSkillID, DBConstants.AdvancedIndustrySkillID);

            if (m_hasReactions)
            {
                gbReactions.Visible = true;
                gbManufacturing.Visible = gbResearching.Visible = gbInvention.Visible = false;
                gbReactions.Location = m_gbManufOriginalLocation;
            }
            else
            {
                gbReactions.Visible = false;
                gbManufacturing.Visible = m_hasManufacturing;
                gbResearching.Location = gbManufacturing.Visible ? m_gbResearchingOriginalLocation :
                    m_gbManufOriginalLocation;
                gbResearching.Visible = m_hasCopying || m_hasResearchingMaterialEfficiency || m_hasResearchingTimeEfficiency;
                gbInvention.Location = gbResearching.Visible ? m_gbInventionOriginalLocation : gbResearching.Location;
                gbInvention.Visible = !gbResearching.Visible || m_hasInvention;
                gbReactions.Location = m_gbReactionsOriginalLocation;
            }

            if (!gbInvention.Visible)
                return;

            // Invention time base time
            activityTime = m_blueprint.ResearchInventionTime * GetFacilityResearchTimeMultiplier(
                BlueprintActivity.Invention);
            lblInventionBaseTime.Text = BaseActivityTime(activityTime);

            // Invention character time
            activityTime *= 1d;
            lblInventionCharTime.Text = CharacterActivityTime(activityTime, 0, DBConstants.
                AdvancedIndustrySkillID);
        }

        /// <summary>
        /// Update the required materials list.
        /// </summary>
        private void UpdateRequiredMaterialsList()
        {
            int scrollBarPosition = PropertiesList.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = (PropertiesList.SelectedItems.Count > 0) ? PropertiesList.
                SelectedItems[0].Tag.GetHashCode() : 0;

            PropertiesList.BeginUpdate();
            try
            {
                // Clear everything
                PropertiesList.Items.Clear();
                PropertiesList.Groups.Clear();
                PropertiesList.Columns.Clear();

                // Create the columns
                PropertiesList.Columns.Add("item", "Item");
                PropertiesList.Columns.Add("qBase", "Quantity (Base)");

                if (Character != null)
                    PropertiesList.Columns.Add("quant", "Quantity (You)");

                IEnumerable<ListViewItem> items = AddGroups();

                // Add the items
                PropertiesList.Items.AddRange(items.OrderBy(x => x.Text).ToArray());

                // Show/Hide the "no item required" label and autoresize the columns 
                PropertiesList.Visible = PropertiesList.Items.Count > 0;

                if (PropertiesList.Items.Count > 0)
                    AdjustColumns();

                if (selectedItem <= 0)
                    return;

                // Restore the selected item (if any)
                foreach (ListViewItem lvItem in PropertiesList.Items.Cast<ListViewItem>().Where(
                    lvItem => lvItem.Tag.GetHashCode() == selectedItem))
                {
                    lvItem.Selected = true;
                }
            }
            finally
            {
                PropertiesList.EndUpdate();
                PropertiesList.SetVerticalScrollBarPosition(scrollBarPosition);
            }
        }

        /// <summary>
        /// Adds the groups.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ListViewItem> AddGroups()
        {
            var items = new List<ListViewItem>();
            double materiaEffModifier = 1d - (double)nudME.Value / 100;

            foreach (var marketGroup in StaticItems.AllGroups)
            {
                // Create the groups
                var group = new ListViewGroup(marketGroup.CategoryPath);
                bool hasItem = false;
                foreach (var material in m_blueprint.MaterialRequirements)
                    if (material.Activity == m_activity && marketGroup.Items.Any(y => y.ID == material.ID))
                    {
                        hasItem = true;

                        // Create the item
                        var item = new ListViewItem(group)
                        {
                            Tag = StaticItems.GetItemByID(material.ID),
                            Text = material.Name
                        };

                        // Add the item to the list
                        items.Add(item);

                        // Calculate the base material quantity
                        long baseMaterialQuantity = material.Quantity;
                        // Calculate the actual material quantity
                        long actualMaterialQuantity = (long)Math.Ceiling(material.Quantity *
                            materiaEffModifier * m_materialFacilityMultiplier);

                        // Add the base quantity for every item
                        var subItemBase = new ListViewItem.ListViewSubItem(item,
                            baseMaterialQuantity.ToString("N0"));
                        item.SubItems.Add(subItemBase);
                        // Add the quantity needed according to the character's skills for every item
                        var subItem = new ListViewItem.ListViewSubItem(item,
                            actualMaterialQuantity.ToString("N0"));
                        item.SubItems.Add(subItem);
                    }

                // Add the group that has an item
                if (hasItem)
                    PropertiesList.Groups.Add(group);
            }

            return items;
        }

        /// <summary>
        /// Update the facility modifier list. 
        /// </summary>
        private void UpdateFacilityModifier()
        {
            cbFacility.Items.Clear();

            cbFacility.Items.Add("NPC Station");

            Item producedItem = m_blueprint.ProducesItem;

            // This should not happen but be prepared if something changes in CCP DB
            if (producedItem == null)
            {
                cbFacility.SelectedIndex = 0;
                return;
            }

#if false
            switch (m_activity)
            {
                case BlueprintActivity.Manufacturing:
                    if (producedItem.MarketGroup.BelongsIn(DBConstants.DronesMarketGroupID)
                        && !producedItem.MarketGroup.BelongsIn(DBConstants.SmallToLargeShipsMarketGroupIDs))
                    {
                        cbFacility.Items.Add("Drone Assembly Array");
                    }

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.AmmosAndChargesMarketGroupID) ||
                        producedItem.MarketGroup.BelongsIn(DBConstants.FuelBlocksMarketGroupID))
                    {
                        cbFacility.Items.Add("Ammunition Assembly Array");
                    }

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.ShipEquipmentsMarketGroupID) ||
                        producedItem.MarketGroup.BelongsIn(DBConstants.ShipModificationsMarketGroupID))
                    {
                        cbFacility.Items.Add("Equipment Assembly Array");
                        cbFacility.Items.Add("Rapid Equipment Assembly Array");
                    }

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.ComponentsMarketGroupID))
                        cbFacility.Items.Add("Component Assembly Array");

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.StrategicComponentsMarketGroupIDs))
                        cbFacility.Items.Add("Subsystem Assembly Array");

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.SmallToLargeShipsMarketGroupIDs))
                        cbFacility.Items.Add("(Ship Size) Ship Assembly Array");

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.CapitalShipsMarketGroupIDs))
                        cbFacility.Items.Add("Capital Assembly Array");

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.AdvancedSmallToLargeShipsMarketGroupIDs))
                        cbFacility.Items.Add("Advanced (Ship Size) Ship Assembly Array");

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.SupercapitalShipsMarketGroupIDs))
                        cbFacility.Items.Add("Supercapital Assembly Array");

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.StandardCapitalShipComponentsMarketGroupID) ||
                        producedItem.MarketGroup.BelongsIn(DBConstants.AdvancedCapitalComponentsMarketGroupID))
                    {
                        cbFacility.Items.Add("Thukker Component Assembly Array");
                    }

                    if (producedItem.MarketGroup.BelongsIn(DBConstants.BoostersMarketGroupID))
                        cbFacility.Items.Add("Drug Laboratory");

                    break;
                case BlueprintActivity.Copying:
                case BlueprintActivity.Invention:
                    cbFacility.Items.AddRange(new object[]
                    {
                        "Design Laboratory"
                    });
                    break;
                case BlueprintActivity.ReverseEngineering:
                    cbFacility.Items.AddRange(new object[]
                    {
                        "Experimental Laboratory"
                    });
                    break;
                case BlueprintActivity.ResearchingMaterialEfficiency:
                case BlueprintActivity.ResearchingTimeEfficiency:
                    cbFacility.Items.AddRange(new object[]
                    {
                        "Research Laboratory",
                        "Hyasyoda Research Laboratory"
                    });
                    break;
            }
#endif
            cbFacility.Items.AddRange(new object[]
            {
                "Citadel",
                "Raitaru",
                "Azbel",
                "Sotiyo"
            });

            BlueprintBrowserSettings settings;

            // Skill Planner
            if (Plan != null)
                settings = Settings.UI.BlueprintBrowser;
            // Character associated Data Browser
            else if (Character != null)
                settings = Settings.UI.BlueprintCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.BlueprintDataBrowser;

            // Update the selected index
            if (m_activity == BlueprintActivity.Manufacturing)
            {
                cbFacility.SelectedIndex = (settings.ProductionFacilityIndex < cbFacility.
                    Items.Count) ? settings.ProductionFacilityIndex : 0;
                return;
            }

            cbFacility.SelectedIndex = (settings.ResearchFacilityIndex < cbFacility.Items.
                Count) ? settings.ResearchFacilityIndex : 0;
        }

        /// <summary>
        /// Update the implant set modifier list. 
        /// </summary>
        private void UpdateImplantSetModifier()
        {
            cbImplantSet.Visible = Character != null;

            if (Character == null)
                return;

            cbImplantSet.Items.Clear();
            foreach (ImplantSet set in Character.ImplantSets)
            {
                cbImplantSet.Items.Add(set);
            }

            int comboBoxArrowWidth = 16 * (int)Math.Truncate(Graphics.FromHwnd(Handle).DpiX / EveMonConstants.DefaultDpi);
            int maxWidth = Math.Min(Character.ImplantSets.Max(set =>
                TextRenderer.MeasureText(set.Name, cbImplantSet.Font).Width) + comboBoxArrowWidth,
                (int)(cbImplantSet.Font.Size * EveMonConstants.ImplantSetNameMaxLength));

            cbImplantSet.Size = new Size(Math.Max(maxWidth, cbImplantSet.Size.Width), cbImplantSet.Size.Height);

            BlueprintBrowserSettings settings;

            // Skill Planner
            if (Plan != null)
                settings = Settings.UI.BlueprintBrowser;
            // Character associated Data Browser
            else if (Character != null)
                settings = Settings.UI.BlueprintCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.BlueprintDataBrowser;

            // Update the selected index
            cbImplantSet.SelectedIndex = settings.ImplantSetIndex < cbImplantSet.Items.Count ?
                settings.ImplantSetIndex : 0;
        }

        /// <summary>
        /// Calculate the base activity time.
        /// </summary>
        /// <param name="activityTime"></param>
        /// <returns></returns>
        private static string BaseActivityTime(double activityTime)
        {
            if (double.IsNaN(activityTime))
                return TimeSpanToText(TimeSpan.FromSeconds(0d), false);

            TimeSpan time = TimeSpan.FromSeconds(Math.Ceiling(activityTime));
            return TimeSpanToText(time, time.Seconds != 0);
        }

        /// <summary>
        /// Calculate the character's activity time.
        /// </summary>
        /// <param name="activityTime">The activity time.</param>
        /// <param name="skillID">The skill ID.</param>
        /// <param name="advSkillID">The advanced skill ID.</param>
        /// <returns></returns>
        private string CharacterActivityTime(double activityTime, int skillID, int advSkillID = 0)
        {
            if (Character == null)
                return string.Empty;

            long advancedSkillLevel = 0L;
            const double AdvancedIndustrySkillBonusFactor = 0.03d;
            double skillBonusModifier = 0d;

            if (skillID != 0)
            {
                double skillBonusFactor;
                switch (skillID)
                {
                    case DBConstants.IndustrySkillID:
                    case DBConstants.ReactionsSkillID:
                        skillBonusFactor = 0.04d;
                        break;
                    default:
                        skillBonusFactor = 0.05d;
                        break;
                }

                skillBonusModifier = skillBonusFactor * Character.LastConfirmedSkillLevel(
                    skillID);
            }
            if (advSkillID != 0)
                advancedSkillLevel = Character.LastConfirmedSkillLevel(advSkillID);

            double activityTimeModifier = (1.0d - skillBonusModifier) *
                (1.0d - AdvancedIndustrySkillBonusFactor * advancedSkillLevel);

            var time = TimeSpan.FromSeconds(Math.Ceiling(activityTime * activityTimeModifier));
            return $"{TimeSpanToText(time, time.Seconds != 0)} (You)";
        }

        /// <summary>
        /// Transpose the timespan to text.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="includeSeconds"></param>
        /// <returns></returns>
        private static string TimeSpanToText(TimeSpan time, bool includeSeconds)
            => time.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas, includeSeconds);

        /// <summary>
        /// Gets the probability modifier.
        /// </summary>
        /// <returns></returns>
        private decimal GetProbabilityModifier()
        {
            if (Character == null)
                return 1M;

            const decimal BonusFactor = 0.05M;
            decimal skillLevel = m_blueprint.Prerequisites.Where(x => (x.Activity ==
                BlueprintActivity.Invention || x.Activity == BlueprintActivity.
                ReverseEngineering) && x.Skill != null).Max(x => Character.
                LastConfirmedSkillLevel(x.Skill.ID));

            return 1M + BonusFactor * skillLevel;
        }

        /// <summary>
        /// Gets the time efficiency modifier.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        private double GetTimeEfficiencyModifier(BlueprintActivity activity)
        {
            switch (activity)
            {
                case BlueprintActivity.Manufacturing:
                    return 1d - (double)nudTE.Value / 100;
                case BlueprintActivity.ResearchingMaterialEfficiency:
                    switch ((int)nudME.Value)
                    {
                        case 0:
                            return 1d;
                        case 1:
                            return (250 - 105) / 105d;
                        case 2:
                            return (595d - 250d) / 105d;
                        case 3:
                            return (1414 - 595) / 105d;
                        case 4:
                            return (3360 - 1414) / 105d;
                        case 5:
                            return (8000 - 3360) / 105d;
                        case 6:
                            return (19000 - 8000) / 105d;
                        case 7:
                            return (45255 - 19000) / 105d;
                        case 8:
                            return (107700 - 45255) / 105d;
                        case 9:
                        case 10:
                            return (256000 - 107700) / 105d;
                    }
                    break;
            }

            if (activity != BlueprintActivity.ResearchingTimeEfficiency)
                return 1d;

            switch ((int)nudTE.Value)
            {
                case 0:
                    return 1d;
                case 2:
                    return (250 - 105) / 105d;
                case 4:
                    return (595d - 250d) / 105d;
                case 6:
                    return (1414 - 595) / 105d;
                case 8:
                    return (3360 - 1414) / 105d;
                case 10:
                    return (8000 - 3360) / 105d;
                case 12:
                    return (19000 - 8000) / 105d;
                case 14:
                    return (45255 - 19000) / 105d;
                case 16:
                    return (107700 - 45255) / 105d;
                case 18:
                case 20:
                    return (256000 - 107700) / 105d;
            }

            return 1d;
        }

        /// <summary>
        /// Gets the activity from the selected tab.
        /// </summary>
        /// <returns></returns>
        private BlueprintActivity GetActivity()
        {
            // Unsubscribe mouse handlers for the old properties list
            PropertiesList.MouseDown -= PropertiesList_MouseDown;
            PropertiesList.MouseMove -= PropertiesList_MouseMove;

            BlueprintActivity activity = BlueprintActivity.None;

            if (tabControl.SelectedTab == null)
                return activity;

            switch (tabControl.SelectedTab.Text)
            {
                case "Manufacturing":
                    PropertiesList = lvManufacturing;
                    activity = BlueprintActivity.Manufacturing;
                    break;
                case "Copying":
                    PropertiesList = lvCopying;
                    activity = BlueprintActivity.Copying;
                    break;
                case "Researching Material Efficiency":
                    PropertiesList = lvResearchME;
                    activity = BlueprintActivity.ResearchingMaterialEfficiency;
                    break;
                case "Researching Time Efficiency":
                    PropertiesList = lvResearchTE;
                    activity = BlueprintActivity.ResearchingTimeEfficiency;
                    break;
                case "Invention":
                    PropertiesList = lvInvention;
                    activity = BlueprintActivity.Invention;
                    break;
                case "Reactions":
                    PropertiesList = lvReactions;
                    activity = BlueprintActivity.Reactions;
                    break;
                case "Simple Reactions":
                    PropertiesList = lvReactions;
                    activity = BlueprintActivity.SimpleReactions;
                    break;
                default:
                    throw new NotImplementedException();
            }

            // Re-subscribe mouse handlers for the new properties list
            PropertiesList.MouseDown += PropertiesList_MouseDown;
            PropertiesList.MouseMove += PropertiesList_MouseMove;

            return activity;
        }

        /// <summary>
        /// Show the item in its appropriate browser.
        /// </summary>
        /// <param name="item"></param>
        private void ShowInBrowser(Item item)
        {
            PlanWindow planWindow = ParentForm as PlanWindow;

            if (item is Ship)
                planWindow?.ShowShipInBrowser(item);
            else
                planWindow?.ShowItemInBrowser(item);
        }

        /// <summary>
        /// Gets the manufacturing time and material multiplier of a facility.
        /// </summary>
        /// <returns></returns>
        private double GetFacilityManufacturingAndMaterialMultiplier()
        {
            string text = cbFacility.Text;
            m_materialFacilityMultiplier = 1.0d;

            if (m_activity != BlueprintActivity.Manufacturing)
                return 1.0d;

            if (text.StartsWith("Rapid", StringComparison.Ordinal))
            {
                m_materialFacilityMultiplier = 1.05d;
                return 0.65d;
            }

            if (text.StartsWith("Thukker Component", StringComparison.Ordinal))
            {
                m_materialFacilityMultiplier = 0.85d;
                return 0.75d;
            }

            if (text.StartsWith("Subsystem", StringComparison.Ordinal) ||
                text.StartsWith("Supercapital", StringComparison.Ordinal) ||
                text.StartsWith("Drug", StringComparison.Ordinal) ||
                text.StartsWith("NPC", StringComparison.Ordinal))
            {
                return 1.0d;
            }

            m_materialFacilityMultiplier = 0.98d;
            return 0.75d;
        }

        /// <summary>
        /// Gets the research time multiplier of a facility by their activity.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        private double GetFacilityResearchTimeMultiplier(BlueprintActivity activity)
        {
            string text = cbFacility.Text;

            if (text.StartsWith("Design", StringComparison.Ordinal))
            {
                switch (activity)
                {
                    case BlueprintActivity.Invention:
                        return 0.5d;
                    case BlueprintActivity.Copying:
                        return 0.6d;
                    default:
                        return 1;
                }
            }

            if (!text.Contains("Research Laboratory"))
                return 1.0d;

            switch (activity)
            {
                case BlueprintActivity.ResearchingMaterialEfficiency:
                case BlueprintActivity.ResearchingTimeEfficiency:
                    return text.StartsWith("Hyasyoda", StringComparison.Ordinal) ? 0.65d : 0.70d;
            }

            return 1.0d;
        }

        /// <summary>
        /// Gets the multiplier of an implant.
        /// </summary>
        /// <returns></returns>
        private double GetImplantMultiplier(ICollection<int> implantIDs)
        {
            ImplantSet implantSet = (ImplantSet)cbImplantSet.Tag;

            Implant implant = implantSet?.FirstOrDefault(x => implantIDs.Contains(x.ID));

            if (implant == null)
                return 1.0d;

            double bonus = implant.Properties
                .FirstOrDefault(x => DBConstants.IndustryModifyingPropertyIDs.IndexOf(x.Property.ID) != -1)
                .Int64Value;
            double multiplier = 1.0d + bonus / 100;

            return multiplier;
        }

        #endregion


        #region Event Handlers

        /// <summary>
        /// Occurs when a tab is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Updates the required skills control according to the seleted tab</remarks>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == -1)
                return;

            m_activity = GetActivity();
            requiredSkillsControl.Activity = m_activity;
            UpdateFacilityModifier();
        }

        /// <summary>
        /// Occurs when the value of the control changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudME_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown control = sender as NumericUpDown;

            if (control == null)
                return;

            UpdateAttributes();
            UpdateRequiredMaterialsList();
        }

        /// <summary>
        /// Occurs when the value of the control changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudTE_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown control = sender as NumericUpDown;

            if (control == null)
                return;

            UpdateAttributes();
        }

        /// <summary>
        /// Occurs on facility selection change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlueprintBrowserSettings settings;

            // Skill Planner
            if (Plan != null)
                settings = Settings.UI.BlueprintBrowser;
            // Character associated Data Browser
            else if (Character != null)
                settings = Settings.UI.BlueprintCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.BlueprintDataBrowser;

            if (m_activity == BlueprintActivity.Manufacturing)
                settings.ProductionFacilityIndex = cbFacility.SelectedIndex;
            else
                settings.ResearchFacilityIndex = cbFacility.SelectedIndex;

            UpdateAttributes();
            UpdateRequiredMaterialsList();
        }

        /// <summary>
        /// Occurs on implant set selection change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbImplantSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlueprintBrowserSettings settings;

            // Skill Planner
            if (Plan != null)
                settings = Settings.UI.BlueprintBrowser;
            // Character associated Data Browser
            else if (Character != null)
                settings = Settings.UI.BlueprintCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.BlueprintDataBrowser;

            settings.ImplantSetIndex = cbImplantSet.SelectedIndex;
            cbImplantSet.Tag = cbImplantSet.SelectedItem;

            if (m_blueprint == null)
                return;

            UpdateAttributes();
            UpdateRequiredMaterialsList();
        }

        /// <summary>
        /// Occurs when clicking on the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblItem_Click(object sender, EventArgs e)
        {
            Item item = (Item)lblItem.Tag;

            if (item == null)
                return;

            ShowInBrowser(item);

            lblItem.ForeColor = Color.Purple;
        }

        /// <summary>
        /// Occurs when clicking on a list box item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InventBlueprintListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Blueprint blueprint = (Blueprint)InventBlueprintListBox.SelectedItem;

            if (blueprint == null)
                return;

            SelectedObject = blueprint;
            InventBlueprintListBox.ClearSelected();
        }

        /// <summary>
        /// Occurs when double clicking on a list view item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesList_DoubleClick(object sender, EventArgs e)
        {
            Item item = (Item)PropertiesList.FocusedItem?.Tag;

            if (item == null)
                return;

            ShowInBrowser(item);
        }

        /// <summary>
        /// Handles the Opening event of the BlueprintAttributeContextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void BlueprintAttributeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            Item item = (Item)PropertiesList.FocusedItem?.Tag;

            showInMenuItem.Visible = showInMenuSeparator.Visible = item != null;
            showInMenuItem.Text = item is Ship ? "Show In Ship Browser" : "Show In Item Browser";
        }

        /// <summary>
        /// Exports activity info to CSV format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewExporter.CreateCSV(PropertiesList);
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
        /// Handles the MouseMove event of the InventBlueprintListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void InventBlueprintListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            InventBlueprintListBox.Cursor = InventBlueprintListBox.Items.Count > 0
                ? Cursors.Hand
                : Cursors.Default;
        }

        #endregion
    }
}
