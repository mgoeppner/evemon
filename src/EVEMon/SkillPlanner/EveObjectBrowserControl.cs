using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Collections;
using EVEMon.Common.Serialization.Datafiles;

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// Base class for EveObject browsers.
    /// Provides basic split container layout and item header including icon, name and category, 
    /// along with event handling for item selection and worksafeMode changes.
    /// </summary>
    /// <remarks>
    /// Should be an abstract class but Visual Studio Designer throws a wobbler when you
    /// try to design a class that inherits from an abstract class.
    /// </remarks>
    internal partial class EveObjectBrowserControl : UserControl
    {
        protected const int Pad = 3;

        private Plan m_plan;


        #region Initialization

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected EveObjectBrowserControl()
        {
            InitializeComponent();
            lblEveObjName.Font = new Font(lblEveObjName.Font.FontFamily, 11.25F, FontStyle.Bold);
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            SelectControl.SelectionChanged -= OnSelectionChanged;
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            EveMonClient.PlanChanged -= EveMonClient_PlanChanged;
            Disposed -= OnDisposed;
        }

        /// <summary>
        /// Occurs when the control is loaded.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

            ListViewHelper.EnableDoubleBuffer(PropertiesList);
            PropertiesList.ShowItemToolTips = true;

            lblEveObjName.Font = new Font(lblEveObjName.Font.FontFamily, 11.25F, FontStyle.Bold);

            // Watch for selection changes
            SelectControl.SelectionChanged += OnSelectionChanged;
            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            EveMonClient.PlanChanged += EveMonClient_PlanChanged;
            Disposed += OnDisposed;

            // Reposition the help text along side the treeview
            Control[] result = SelectControl.Controls.Find("lowerPanel", true);
            if (result.Length > 0)
                lblHelp.Location = new Point(lblHelp.Location.X, result[0].Location.Y);

            // Updates the controls visibility
            UpdateControlsVisibility();

            // Force a refresh
            UpdateContent();
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        internal Character Character
        {
            get { return SelectControl.Character; }
            set
            {
                if (SelectControl.Character == value)
                    return;

                SelectControl.Character = value;
            }
        }

        /// <summary>
        /// Gets or sets the current plan for this planner window.
        /// </summary>
        internal Plan Plan
        {
            get { return m_plan; }
            set
            {
                m_plan = value;
                SelectControl.Plan = value;
                OnSelectedPlanChanged();
            }
        }

        /// <summary>
        /// Gets or sets the currently selected object.
        /// </summary>
        internal Item SelectedObject
        {
            get { return SelectControl?.SelectedObject; }
            set
            {
                if (SelectControl == null)
                    return;

                SelectControl.SelectedObject = value;
            }
        }

        #endregion


        #region Protected Properties

        /// <summary>
        /// Gets or sets the select control.
        /// </summary>
        /// <value>The select control.</value>
        protected EveObjectSelectControl SelectControl { get; set; }

        /// <summary>
        /// Gets or sets the properties list.
        /// </summary>
        /// <value>The properties list.</value>
        protected ListView PropertiesList { get; set; }

        #endregion


        #region Events

        /// <summary>
        /// Updates whenever the selected plan changed.
        /// </summary>
        /// <remarks>This virtual method is implemented in classes that inherit from EveObjectBrowserControl.</remarks>
        protected virtual void OnSelectedPlanChanged()
        {
        }

        /// <summary>
        /// Updates whenever the plan changed.
        /// </summary>
        /// <remarks>This virtual method is implemented in classes that inherit from EveObjectBrowserControl.</remarks>
        protected virtual void OnPlanChanged()
        {
        }

        /// <summary>
        /// Updates the controls when the selection is changed.
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            UpdateContent();
        }

        /// <summary>
        /// Occurs when the settings changed.
        /// </summary>
        protected virtual void OnSettingsChanged()
        {
            UpdateControlsVisibility();
        }

        #endregion


        #region Event Handlers

        /// <summary>
        /// When the current plan changes (new skills, etc), we need to update some informations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_PlanChanged(object sender, PlanChangedEventArgs e)
        {
            if ((e.Plan != m_plan) || (e.Plan.Character != m_plan.Character))
                return;

            OnPlanChanged();
        }

        /// <summary>
        /// Occurs when the settings changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }

        /// <summary>
        /// Occurs when the selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged();
        }

        #endregion


        #region Selection management

        private void UpdateContent()
        {
            // Updates the header and the panels visibility.
            Item firstSelected = SelectControl.SelectedObject;
            if (firstSelected == null)
            {
                // Hide details and header
                pnlDetails.Visible = false;
                pnlBrowserHeader.Visible = false;

                // View help message
                lblHelp.Visible = true;

                // Done
                return;
            }

            // Hide help message
            lblHelp.Visible = false;

            // View details and header
            pnlBrowserHeader.Visible = true;
            pnlDetails.Visible = true;

            // Display details
            eoImage.EveItem = firstSelected;
            lblEveObjName.Text = firstSelected.Name;
            lblEveObjCategory.Text = firstSelected.CategoryPath;

            // Stop here if it's the blueprint tab
            if (SelectControl is BlueprintSelectControl)
                return;

            // Fill the list view
            UpdatePropertiesList();
        }

        /// <summary>
        /// Refresh the properties list.
        /// </summary>
        private void UpdatePropertiesList()
        {
            int scrollBarPosition = PropertiesList.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = PropertiesList.SelectedItems.Count > 0
                ? PropertiesList.SelectedItems[0].Tag.GetHashCode()
                : 0;

            PropertiesList.BeginUpdate();
            try
            {
                // Refresh columns
                PropertiesList.Columns.Clear();
                PropertiesList.Columns.Add("Attribute");
                foreach (Item obj in SelectControl.SelectedObjects)
                {
                    PropertiesList.Columns.Add(obj.Name);
                }

                // Prepare properties list
                IEnumerable<ListViewItem> items = AddPropertyGroups();

                // Fetch the new items to the list view
                PropertiesList.Items.Clear();
                PropertiesList.Items.AddRange(items.ToArray());

                if (PropertiesList.Items.Count > 0)
                    AdjustColumns();

                // Restore the selected item (if any)
                if (selectedItem <= 0)
                    return;

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
        /// Adds the property groups.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ListViewItem> AddPropertyGroups()
        {
            PropertiesList.Groups.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            foreach (EvePropertyCategory category in StaticProperties.AllCategories)
            {
                ListViewGroup group = new ListViewGroup(category.DisplayName);
                bool hasProps = false;

                foreach (EveProperty prop in category)
                {
                    // Skip the 'moon mining amount' property
                    if (prop.ID == DBConstants.MoonMiningAmountPropertyID)
                        continue;

                    // Checks whether we must display this property
                    bool visibleProperty = false;

                    // Some properties should be always visible (fitting, shields resists, etc)
                    if (this is ShipBrowserControl)
                        visibleProperty = prop.AlwaysVisibleForShips;

                    // Or we check whether any object has this property
                    if (!visibleProperty)
                        visibleProperty = SelectControl.SelectedObjects.Any(x => x.Properties[prop.ID].HasValue);

                    // Some properties should be hidden if they have the default value (sensor strenght, em damage, etc)
                    if (prop.HideIfDefault)
                    {
                        visibleProperty = SelectControl.SelectedObjects.Any(
                            x =>
                            {
                                EvePropertyValue? eveProperty = x.Properties[prop.ID];
                                return eveProperty != null && prop.DefaultValue != eveProperty.Value.Value;
                            });
                    }

                    // We hide the reprocessing skill here and make it visible in the "Reprocessing Info" section
                    if (prop.ID == DBConstants.ReprocessingSkillPropertyID)
                        visibleProperty = false;

                    // Jump to next property if not visible
                    if (!visibleProperty)
                        continue;

                    hasProps = true;

                    // Retrieve the data to put in the columns
                    AddPropertyValue(items, group, prop);
                }

                // Check if the objects belong to an item family that has fitting slot property 
                if (category.Name == DBConstants.GeneralCategoryName && SelectControl.SelectedObjects.Any(
                    x => (x.Family == ItemFamily.Item || x.Family == ItemFamily.Drone) &&
                         x.FittingSlot != ItemSlot.NoSlot && x.FittingSlot != ItemSlot.None))
                {
                    AddFittingSlotProperty(items, group);
                }

                // Add properties
                if (hasProps)
                    PropertiesList.Groups.Add(group);
            }

            // Add the reaction info
            AddReactionInfo(items);

            // Add the control tower fuel info
            AddControlTowerFuelInfo(items);

            // Add the reprocessing-refining info 
            AddReprocessingInfo(items);

            return items;
        }

        /// <summary>
        /// Adds the property value.
        /// </summary>
        /// <param name="items">The list of items.</param>
        /// <param name="group">The listGroup.</param>
        /// <param name="prop">The property.</param>
        private void AddPropertyValue(ICollection<ListViewItem> items, ListViewGroup group, EveProperty prop)
        {
            string[] labels = SelectControl.SelectedObjects.Select(prop.GetLabelOrDefault).ToArray();
            double[] values = SelectControl.SelectedObjects.Select(prop.GetNumericValue).ToArray();

            // Create the list view item
            ListViewItem item = new ListViewItem(group) { ToolTipText = prop.Description, Text = prop.Name, Tag = prop };
            items.Add(item);

            AddValueForSelectedObjects(prop, item, labels, values);
        }

        /// <summary>
        /// Adds the value for selected objects.
        /// </summary>
        /// <param name="prop">The evaluated EveProperty.</param>
        /// <param name="item">The list of items.</param>
        /// <param name="labels">The labels.</param>
        /// <param name="values">The values.</param>
        private void AddValueForSelectedObjects(EveProperty prop, ListViewItem item, IList<string> labels, IList<double> values)
        {
            double min = 0f;
            double max = 0f;
            bool allEqual = true;

            if (values.Any())
            {
                min = values.Min();
                max = values.Max();
                allEqual = values.All(x => Math.Abs(x - min) < float.Epsilon);
                if (prop != null && !prop.HigherIsBetter)
                {
                    double temp = min;
                    min = max;
                    max = temp;
                }
            }

            // Add the value for every selected item
            for (int index = 0; index < SelectControl.SelectedObjects.Count(); index++)
            {
                // Create the subitem and choose its forecolor
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, labels[index]);
                if (!allEqual)
                {
                    if (Math.Abs(values[index] - max) < float.Epsilon)
                        subItem.ForeColor = Color.DarkGreen;
                    else if (Math.Abs(values[index] - min) < float.Epsilon)
                        subItem.ForeColor = Color.DarkRed;

                    item.UseItemStyleForSubItems = false;
                }
                else if (SelectControl.SelectedObjects.Count() > 1)
                {
                    subItem.ForeColor = Color.DarkGray;
                    item.UseItemStyleForSubItems = false;
                }

                item.SubItems.Add(subItem);
            }
        }

        /// <summary>
        /// Adds the fitting slot property.
        /// </summary>
        /// <param name="items">The list of items.</param>
        /// <param name="group">The listGroup.</param>
        private void AddFittingSlotProperty(ICollection<ListViewItem> items, ListViewGroup group)
        {
            string[] labels = SelectControl.SelectedObjects.Select(x => x.FittingSlot.ToString()).ToArray();

            // Create the list view item
            ListViewItem item = new ListViewItem(group)
            { ToolTipText = "The slot that this item fits in", Text = @"Fitting Slot", Tag = Text };
            items.Add(item);

            // Add the value for every selected item
            AddValueForSelectedObjects(null, item, labels, new double[] { });
        }

        /// <summary>
        /// Adds the control tower fuel info.
        /// </summary>
        /// <param name="items">The items.</param>
        private void AddControlTowerFuelInfo(ICollection<ListViewItem> items)
        {
            if (SelectControl.SelectedObjects.All(x => !x.ControlTowerFuel.Any()))
                return;

            EveProperty prop = StaticProperties.GetPropertyByName(DBConstants.ConsumptionRatePropertyName);
            IList<SerializableControlTowerFuel> fuelMaterials = SelectControl.SelectedObjects.Where(
                x => x.ControlTowerFuel.Any()).SelectMany(x => x.ControlTowerFuel).ToList();

            foreach (string purpose in fuelMaterials.Select(x => x.Purpose).Distinct())
            {
                string groupName = $"Fuel Requirements - {purpose}";
                ListViewGroup group = new ListViewGroup(groupName);

                foreach (Item item in StaticItems.AllItems.OrderBy(x => x.ID))
                {
                    if (fuelMaterials.Where(x => x.Purpose == purpose).All(x => x.ID != item.ID))
                        continue;

                    IEnumerable<Material> materials = GetMaterials(fuelMaterials, item);

                    AddListViewItem(prop, items, group, item, materials);
                }

                PropertiesList.Groups.Add(group);
            }
        }

        /// <summary>
        /// Gets the materials.
        /// </summary>
        /// <param name="fuelMaterials">The fuel materials.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private IEnumerable<Material> GetMaterials(IEnumerable<SerializableControlTowerFuel> fuelMaterials, Item item)
        {
            // Create the list of materials we need to scroll through
            List<Material> materials = new List<Material>();
            foreach (Item obj in SelectControl.SelectedObjects)
            {
                // Compensate for missing entries
                if (!obj.ControlTowerFuel.Any())
                {
                    materials.Add(null);
                    continue;
                }

                materials.Add(obj.ControlTowerFuel.Where(
                    x => x.ID == item.ID && fuelMaterials.Any(y => y == x)).Select(
                        x => new ControlTowerFuel(x)).FirstOrDefault());
            }
            return materials;
        }

        /// <summary>
        /// Adds the reaction info.
        /// </summary>
        /// <param name="items">The items.</param>
        private void AddReactionInfo(ICollection<ListViewItem> items)
        {
            if (SelectControl.SelectedObjects.All(x => !x.ReactionMaterial.Any()))
                return;

            EveProperty prop = StaticProperties.GetPropertyByID(DBConstants.ConsumptionQuantityPropertyID);
            IList<SerializableReactionInfo> reactionMaterials = SelectControl.SelectedObjects.Where(
                x => x.ReactionMaterial.Any()).SelectMany(x => x.ReactionMaterial).ToList();

            // Add resources info
            ListViewGroup resourcesGroup = new ListViewGroup("Resources");
            IList<SerializableReactionInfo> resources = reactionMaterials.Where(x => x.IsInput).ToList();
            AddItemsAndSubItems(prop, items, resourcesGroup, resources);

            // Add products info
            ListViewGroup productsGroup = new ListViewGroup("Products");
            IList<SerializableReactionInfo> products = reactionMaterials.Where(x => !x.IsInput).ToList();
            AddItemsAndSubItems(prop, items, productsGroup, products);
        }

        /// <summary>
        /// Adds the items and sub items.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="items">The items.</param>
        /// <param name="resourcesGroup">The resources group.</param>
        /// <param name="reactionMaterials">The reaction materials.</param>
        private void AddItemsAndSubItems(EveProperty prop, ICollection<ListViewItem> items, ListViewGroup resourcesGroup,
            IList<SerializableReactionInfo> reactionMaterials)
        {
            foreach (Item item in StaticItems.AllItems.OrderBy(x => x.ID))
            {
                if (reactionMaterials.All(x => x.ID != item.ID))
                    continue;

                // Create the list of materials we need to scroll through
                List<Material> materials = new List<Material>();
                foreach (Item obj in SelectControl.SelectedObjects)
                {
                    // Compensate for missing entries
                    if (!obj.ReactionMaterial.Any())
                    {
                        materials.Add(null);
                        continue;
                    }

                    materials.Add(obj.ReactionMaterial.Where(
                        x => x.ID == item.ID && reactionMaterials.Any(y => y == x)).Select(
                            x => new ReactionMaterial(x)).FirstOrDefault());
                }

                AddListViewItem(prop, items, resourcesGroup, item, materials);
            }

            PropertiesList.Groups.Add(resourcesGroup);
        }

        /// <summary>
        /// Adds the reprocessing info.
        /// </summary>
        /// <param name="items">The list of items.</param>
        private void AddReprocessingInfo(ICollection<ListViewItem> items)
        {
            if (SelectControl.SelectedObjects.All(x => x.ReprocessingMaterials == null))
                return;

            string groupName = "Reprocessing - Refining Info";

            if (SelectControl.SelectedObjects.Where(x => x.ReprocessingSkill != null)
                .All(x => x.ReprocessingSkill.ID != DBConstants.ScrapMetalProcessingSkillID))
            {
                groupName = "Refining Info";
            }

            if (SelectControl.SelectedObjects.Where(x => x.ReprocessingSkill != null)
                .All(x => x.ReprocessingSkill.ID == DBConstants.ScrapMetalProcessingSkillID))
            {
                groupName = "Reprocessing Info";
            }

            ListViewGroup group = new ListViewGroup(groupName);

            // Add the reprocessing skill
            AddReprocessingSkill(group, items);

            IList<Material> reprocessingMaterials = SelectControl.SelectedObjects.Where(
                x => x.ReprocessingMaterials != null).SelectMany(x => x.ReprocessingMaterials).ToList();

            AddItemsAndSubItems(items, group, reprocessingMaterials);
        }

        /// <summary>
        /// Adds the items and sub items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="group">The group.</param>
        /// <param name="reprocessingMaterials">The reprocessing materials.</param>
        private void AddItemsAndSubItems(ICollection<ListViewItem> items, ListViewGroup group,
            IList<Material> reprocessingMaterials)
        {
            foreach (Item item in StaticItems.AllItems.OrderBy(x => x.ID))
            {
                if (reprocessingMaterials.All(x => x.Item != item))
                    continue;

                // Create the list of reprocessing materials we need to scroll through
                List<Material> materials = new List<Material>();
                foreach (Item obj in SelectControl.SelectedObjects)
                {
                    // Compensate for missing entries
                    if (obj.ReprocessingMaterials == null)
                    {
                        materials.Add(null);
                        continue;
                    }

                    materials.Add(obj.ReprocessingMaterials.FirstOrDefault(
                        x => x.Item == item && reprocessingMaterials.Any(y => y == x)));
                }

                AddListViewItem(null, items, group, item, materials);
            }

            PropertiesList.Groups.Add(group);
        }

        /// <summary>
        /// Adds the list view item.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="items">The items.</param>
        /// <param name="group">The group.</param>
        /// <param name="item">The item.</param>
        /// <param name="materials">The materials.</param>
        private void AddListViewItem(EveProperty prop, ICollection<ListViewItem> items, ListViewGroup group, Item item,
            IEnumerable<Material> materials)
        {
            // Create the list of labels and values
            List<string> labels = new List<string>();
            List<double> values = new List<double>();
            foreach (Material material in materials)
            {
                // Add default labels and values for non existing materials
                if (material == null)
                {
                    labels.Add("0 ");
                    values.Add(0f);
                    continue;
                }

                labels.Add($"{material.Quantity:N0} {prop?.Unit}");
                values.Add(material.Quantity);
            }

            // Create the list view item
            ListViewItem lvItem = new ListViewItem(group)
            {
                ToolTipText = item.Description,
                Text = item.Name,
                Tag = item
            };
            items.Add(lvItem);

            AddValueForSelectedObjects(prop, lvItem, labels, values);
        }

        /// <summary>
        /// Adds the reprocessing skill.
        /// </summary>
        /// <param name="group">The listGroup.</param>
        /// <param name="items">The list of items.</param>
        private void AddReprocessingSkill(ListViewGroup group, ICollection<ListViewItem> items)
        {
            // Create the list of labels
            List<string> labels = new List<string>();
            foreach (Item obj in SelectControl.SelectedObjects)
            {
                // Add a placeholder if no materials
                if (obj.ReprocessingMaterials == null)
                {
                    labels.Add("None");
                    continue;
                }

                string skillName = obj.ReprocessingSkill?.Name ?? EveMonConstants.UnknownText;
                labels.Add(skillName);
            }

            // Create the list view item
            EveProperty property = StaticProperties.GetPropertyByID(DBConstants.ReprocessingSkillPropertyID);
            ListViewItem item = new ListViewItem(group);
            if (property != null)
            {
                item.ToolTipText = property.Description;
                item.Text = property.Name;

                StaticSkill skill = SelectControl.SelectedObjects.Select(obj => obj.ReprocessingSkill).FirstOrDefault();
                if (skill != null && SelectControl.SelectedObjects.All(obj => obj.ReprocessingSkill == skill))
                    item.Tag = Character?.Skills[skill.ID] ?? SkillCollection.Skills.FirstOrDefault(x => x.ID == skill.ID);
                else
                    item.Tag = property.ID;
            }

            items.Add(item);

            // Add the value for every selected item
            AddValueForSelectedObjects(null, item, labels, new double[] { });
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        protected void AdjustColumns()
        {
            foreach (ColumnHeader column in PropertiesList.Columns)
            {
                column.Width = -2;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column
                if (column.Index != PropertiesList.Columns.Count - 1)
                    continue;

                const int ColumnPad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + ColumnPad * 2;

                // Calculate the width of the header and the items of the column
                int columnMaxWidth = PropertiesList.Columns[column.Index].ListView.Items.Cast<ListViewItem>().Select(
                    item => TextRenderer.MeasureText(item.SubItems[column.Index].Text, Font).Width).Concat(
                        new[] { columnHeaderWidth }).Max() + ColumnPad + 1;

                // Assign the width found
                column.Width = columnMaxWidth;
            }
        }

        #endregion


        #region Helper Methods
        
        /// <summary>
        /// Updates the controls visibility.
        /// </summary>
        private void UpdateControlsVisibility()
        {
            scDetailsRight.Panel2Collapsed = Plan == null;

            if (Settings.UI.SafeForWork)
            {
                eoImage.ImageSize = EveImageSize.x0;
                eoImage.Visible = false;
                lblEveObjCategory.Location = new Point(Pad, lblEveObjCategory.Location.Y);
                lblEveObjName.Location = new Point(Pad, lblEveObjName.Location.Y);
            }
            else
            {
                eoImage.ImageSize = EveImageSize.x64;
                eoImage.Visible = true;
                if (SelectControl.SelectedObject != null)
                    eoImage.EveItem = SelectControl.SelectedObject;

                lblEveObjCategory.Location = new Point(eoImage.Width + Pad * 2, lblEveObjCategory.Location.Y);
                lblEveObjName.Location = new Point(eoImage.Width + Pad * 2, lblEveObjName.Location.Y);
            }
        }

        #endregion
    }
}
