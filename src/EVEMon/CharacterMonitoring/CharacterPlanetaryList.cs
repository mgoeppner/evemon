using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.SettingsObjects;
using EVEMon.SkillPlanner;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterPlanetaryList : UserControl, IListView
    {
        #region Fields

        private readonly List<PlanetaryColumnSettings> m_columns = new List<PlanetaryColumnSettings>();
        private readonly List<PlanetaryPin> m_list = new List<PlanetaryPin>();

        private Timer m_refreshTimer;
        private PlanetaryGrouping m_grouping;
        private PlanetaryColumn m_sortCriteria;

        private string m_textFilter = string.Empty;
        private bool m_sortAscending = true;
        private bool m_columnsChanged;
        private bool m_isUpdatingColumns;
        private bool m_init;

        private int m_columnTTCDisplayIndex;

        #endregion


        #region Constructor

        public CharacterPlanetaryList()
        {
            InitializeComponent();

            lvPlanetary.Hide();
            lvPlanetary.AllowColumnReorder = true;
            lvPlanetary.Columns.Clear();

            noPlanetaryColoniesLabel.Font = new Font(noPlanetaryColoniesLabel.Font.FontFamily, 11.25F, FontStyle.Bold);

            ListViewHelper.EnableDoubleBuffer(lvPlanetary);

            lvPlanetary.ColumnClick += listView_ColumnClick;
            lvPlanetary.ColumnWidthChanged += listView_ColumnWidthChanged;
            lvPlanetary.ColumnReordered += listView_ColumnReordered;
            lvPlanetary.MouseDown += listView_MouseDown;
            lvPlanetary.MouseMove += listView_MouseMove;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the character associated with this monitor.
        /// </summary>
        internal CCPCharacter Character { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="lvPlanetary"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        internal bool Visibility
        {
            get { return lvPlanetary.Visible; }
            set { lvPlanetary.Visible = value; }
        }

        /// <summary>
        /// Gets or sets the text filter.
        /// </summary>
        [Browsable(false)]
        public string TextFilter
        {
            get { return m_textFilter; }
            set
            {
                m_textFilter = value;
                if (m_init)
                    UpdateColumns();
            }
        }

        /// <summary>
        /// Gets or sets the enumeration of planetary colonies to display.
        /// </summary>
        internal IEnumerable<PlanetaryPin> PlanetaryPins
        {
            get { return m_list; }
            set
            {
                m_list.Clear();
                if (value == null)
                    return;

                m_list.AddRange(value);
            }
        }

        /// <summary>
        /// Gets or sets the grouping mode.
        /// </summary>
        [Browsable(false)]
        public Enum Grouping
        {
            get { return m_grouping; }
            set
            {
                m_grouping = (PlanetaryGrouping)value;
                if (m_init)
                    UpdateColumns();
            }
        }

        /// <summary>
        /// Gets or sets the settings used for columns.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IEnumerable<IColumnSettings> Columns
        {
            get
            {
                // Add the visible columns; matching the display order
                List<PlanetaryColumnSettings> newColumns = new List<PlanetaryColumnSettings>();
                foreach (ColumnHeader header in lvPlanetary.Columns.Cast<ColumnHeader>().OrderBy(x => x.DisplayIndex))
                {
                    PlanetaryColumnSettings columnSetting = m_columns.First(x => x.Column == (PlanetaryColumn)header.Tag);
                    if (columnSetting.Width > -1)
                        columnSetting.Width = header.Width;

                    newColumns.Add(columnSetting);
                }

                // Then add the other columns
                newColumns.AddRange(m_columns.Where(x => !x.Visible));

                return newColumns;
            }
            set
            {
                m_columns.Clear();
                if (value != null)
                    m_columns.AddRange(value.Cast<PlanetaryColumnSettings>());

                // Whenever the columns changes, we need to
                // reset the dipslay index of the TTC column
                m_columnTTCDisplayIndex = -1;

                if (m_init)
                    UpdateColumns();
            }
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load subscribe the events.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || this.IsDesignModeHosted())
                return;

            m_refreshTimer = new Timer();

            m_refreshTimer.Tick += refresh_TimerTick;
            m_refreshTimer.Interval = 1000;

            EveMonClient.TimerTick += EveMonClient_TimerTick;
            EveMonClient.CharacterPlanetaryColoniesUpdated += EveMonClient_CharacterPlanetaryColoniesUpdated;
            EveMonClient.CharacterPlanetaryLayoutUpdated += EveMonClient_CharacterPlanetaryLayoutUpdated;
            EveMonClient.CharacterPlaneteryPinsCompleted += EveMonClient_CharacterPlaneteryPinsCompleted;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            m_refreshTimer.Dispose();

            EveMonClient.TimerTick -= EveMonClient_TimerTick;
            EveMonClient.CharacterPlanetaryColoniesUpdated -= EveMonClient_CharacterPlanetaryColoniesUpdated;
            EveMonClient.CharacterPlanetaryLayoutUpdated -= EveMonClient_CharacterPlanetaryLayoutUpdated;
            EveMonClient.CharacterPlaneteryPinsCompleted -= EveMonClient_CharacterPlaneteryPinsCompleted;
            Disposed -= OnDisposed;
        }

        /// <summary>
        /// When the control becomes visible again, we update the content.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (DesignMode || this.IsDesignModeHosted() || Character == null || !Visible)
                return;

            // Prevents the properties to call UpdateColumns() till we set all properties
            m_init = false;

            lvPlanetary.Visible = false;

            PlanetaryPins = Character?.PlanetaryColonies.SelectMany(x => x.Pins);
            Columns = Settings.UI.MainWindow.Planetary.Columns;
            Grouping = Character?.UISettings.PlanetaryGroupBy;
            TextFilter = string.Empty;

            UpdateColumns();

            m_init = true;

            UpdateListVisibility();
        }

        #endregion


        #region Update Methods

        /// <summary>
        /// Autoresizes the columns.
        /// </summary>
        public void AutoResizeColumns()
        {
            m_columns.ForEach(column =>
            {
                if (column.Visible)
                    column.Width = -2;
            });

            UpdateColumns();
        }

        /// <summary>
        /// Updates the columns.
        /// </summary>
        internal void UpdateColumns()
        {
            // Returns if not visible
            if (!Visible)
                return;

            lvPlanetary.BeginUpdate();
            m_isUpdatingColumns = true;

            try
            {
                lvPlanetary.Columns.Clear();
                lvPlanetary.Groups.Clear();
                lvPlanetary.Items.Clear();

                foreach (PlanetaryColumnSettings column in m_columns.Where(x => x.Visible))
                {
                    ColumnHeader header = lvPlanetary.Columns.Add(column.Column.GetHeader(), column.Width);
                    header.Tag = column.Column;

                    switch (column.Column)
                    {
                        case PlanetaryColumn.Quantity:
                        case PlanetaryColumn.Volume:
                        case PlanetaryColumn.QuantityPerCycle:
                            header.TextAlign = HorizontalAlignment.Right;
                            break;
                        case PlanetaryColumn.CycleTime:
                            header.TextAlign = HorizontalAlignment.Center;
                            break;
                    }
                }

                // We update the content
                UpdateContent();
            }
            finally
            {
                lvPlanetary.EndUpdate();
                m_isUpdatingColumns = false;
            }
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        private void UpdateContent()
        {
            // Returns if not visible
            if (!Visible)
                return;

            int scrollBarPosition = lvPlanetary.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = lvPlanetary.SelectedItems.Count > 0
                ? lvPlanetary.SelectedItems[0].Tag.GetHashCode()
                : 0;

            lvPlanetary.BeginUpdate();
            try
            {
                string text = m_textFilter.ToUpperInvariant();
                IEnumerable<PlanetaryPin> pins = m_list.Where(x => IsTextMatching(x, text));

                if (Settings.UI.MainWindow.Planetary.ShowEcuOnly)
                    pins = pins.Where(pin => DBConstants.EcuTypeIDs.Any(id => id == pin.TypeID));

                UpdateSort();

                UpdateContentByGroup(pins);

                // Restore the selected item (if any)
                if (selectedItem > 0)
                {
                    foreach (ListViewItem lvItem in lvPlanetary.Items.Cast<ListViewItem>().Where(
                        lvItem => lvItem.Tag.GetHashCode() == selectedItem))
                    {
                        lvItem.Selected = true;
                    }
                }

                // Adjust the size of the columns
                AdjustColumns();

                UpdateListVisibility();
            }
            finally
            {
                lvPlanetary.EndUpdate();
                lvPlanetary.SetVerticalScrollBarPosition(scrollBarPosition);
            }
        }

        /// <summary>
        /// Updates the content by group.
        /// </summary>
        /// <param name="pins">The pins.</param>
        private void UpdateContentByGroup(IEnumerable<PlanetaryPin> pins)
        {
            switch (m_grouping)
            {
                case PlanetaryGrouping.None:
                    UpdateNoGroupContent(pins);
                    break;
                case PlanetaryGrouping.SolarSystem:
                    IOrderedEnumerable<IGrouping<string, PlanetaryPin>> groups0 =
                        pins.GroupBy(x => x.Colony.SolarSystem.Name).OrderBy(x => x.Key);
                    UpdateContent(groups0);
                    break;
                case PlanetaryGrouping.SolarSystemDesc:
                    IOrderedEnumerable<IGrouping<string, PlanetaryPin>> groups1 =
                        pins.GroupBy(x => x.Colony.SolarSystem.Name).OrderByDescending(x => x.Key);
                    UpdateContent(groups1);
                    break;
                case PlanetaryGrouping.PlanetType:
                    IOrderedEnumerable<IGrouping<string, PlanetaryPin>> groups2 =
                        pins.GroupBy(x => x.Colony.PlanetTypeName).OrderBy(x => x.Key);
                    UpdateContent(groups2);
                    break;
                case PlanetaryGrouping.PlanetTypeDesc:
                    IOrderedEnumerable<IGrouping<string, PlanetaryPin>> groups3 =
                        pins.GroupBy(x => x.Colony.PlanetTypeName).OrderByDescending(x => x.Key);
                    UpdateContent(groups3);
                    break;
                case PlanetaryGrouping.Colony:
                    IOrderedEnumerable<IGrouping<PlanetaryColony, PlanetaryPin>> groups4 =
                        pins.GroupBy(x => x.Colony).OrderBy(x => x.Key.PlanetID);
                    UpdateContent(groups4);
                    break;
                case PlanetaryGrouping.ColonyDesc:
                    IOrderedEnumerable<IGrouping<PlanetaryColony, PlanetaryPin>> groups5 =
                        pins.GroupBy(x => x.Colony).OrderByDescending(x => x.Key.PlanetID);
                    UpdateContent(groups5);
                    break;
                case PlanetaryGrouping.EndDate:
                    IOrderedEnumerable<IGrouping<DateTime, PlanetaryPin>> groups6 =
                        pins.GroupBy(x => x.ExpiryTime.ToLocalTime().Date).OrderBy(x => x.Key);
                    UpdateContent(groups6);
                    break;
                case PlanetaryGrouping.EndDateDesc:
                    IOrderedEnumerable<IGrouping<DateTime, PlanetaryPin>> groups7 =
                        pins.GroupBy(x => x.ExpiryTime.ToLocalTime().Date).OrderByDescending(x => x.Key);
                    UpdateContent(groups7);
                    break;
                case PlanetaryGrouping.GroupName:
                    IOrderedEnumerable<IGrouping<string, PlanetaryPin>> groups8 =
                        pins.GroupBy(x => x.GroupName).OrderBy(x => x.Key);
                    UpdateContent(groups8);
                    break;
                case PlanetaryGrouping.GroupNameDesc:
                    IOrderedEnumerable<IGrouping<string, PlanetaryPin>> groups9 =
                        pins.GroupBy(x => x.GroupName).OrderByDescending(x => x.Key);
                    UpdateContent(groups9);
                    break;
            }
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        private void UpdateNoGroupContent(IEnumerable<PlanetaryPin> pins)
        {
            lvPlanetary.Items.Clear();
            lvPlanetary.Groups.Clear();

            // Add the items
            lvPlanetary.Items.AddRange(pins.Select(
                pin => new
                {
                    pin,
                    item = new ListViewItem(pin.TypeName)
                    {
                        UseItemStyleForSubItems = false,
                        Tag = pin
                    }
                }).Select(x => CreateSubItems(x.pin, x.item)).ToArray());
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="groups"></param>
        private void UpdateContent<TKey>(IEnumerable<IGrouping<TKey, PlanetaryPin>> groups)
        {
            lvPlanetary.Items.Clear();
            lvPlanetary.Groups.Clear();

            // Add the groups
            foreach (IGrouping<TKey, PlanetaryPin> group in groups)
            {
                string groupText;
                if (group.Key is DateTime)
                    groupText = ((DateTime)(object)group.Key).ToShortDateString();
                else
                {
                    PlanetaryColony colony = group.Key as PlanetaryColony;
                    if (colony != null)
                    {
                        groupText = $"{colony.SolarSystem.Name} > {colony.PlanetName} [{colony.PlanetTypeName}] " +
                                    $"(Installations: {colony.NumberOfPins}, " +
                                    $"Level: {colony.UpgradeLevel}, " +
                                    $"Updated: {colony.LastUpdate.ToLocalTime()})";
                    }
                    else
                        groupText = group.Key.ToString();
                }

                ListViewGroup listGroup = new ListViewGroup(groupText);
                lvPlanetary.Groups.Add(listGroup);

                // Add the items in every group
                lvPlanetary.Items.AddRange(
                    group.Select(pin => new
                    {
                        pin,
                        item = new ListViewItem(pin.TypeName, listGroup)
                        {
                            UseItemStyleForSubItems = false,
                            Tag = pin
                        }

                    }).Select(x => CreateSubItems(x.pin, x.item)).ToArray());
            }
        }

        /// <summary>
        /// Creates the list view sub items.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private ListViewItem CreateSubItems(PlanetaryPin pin, ListViewItem item)
        {
            // Add enough subitems to match the number of columns
            while (item.SubItems.Count < lvPlanetary.Columns.Count + 1)
            {
                item.SubItems.Add(string.Empty);
            }

            // Creates the subitems
            for (int i = 0; i < lvPlanetary.Columns.Count; i++)
            {
                SetColumn(pin, item.SubItems[i], (PlanetaryColumn)lvPlanetary.Columns[i].Tag);
            }

            return item;
        }

        /// <summary>
        /// Updates the list visibility.
        /// </summary>
        private void UpdateListVisibility()
        {
            // Display or hide the "no planetary colonies" label
            if (!m_init)
                return;

            noPlanetaryColoniesLabel.Visible = lvPlanetary.Items.Count == 0;
            lvPlanetary.Visible = !noPlanetaryColoniesLabel.Visible;
            m_refreshTimer.Enabled = lvPlanetary.Visible;
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        private void AdjustColumns()
        {
            foreach (ColumnHeader column in lvPlanetary.Columns)
            {
                if (m_columns[column.Index].Width == -1)
                    m_columns[column.Index].Width = -2;

                column.Width = m_columns[column.Index].Width;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column and not set to auto-resize
                if (column.Index != lvPlanetary.Columns.Count - 1 || m_columns[column.Index].Width != -2)
                    continue;

                const int Pad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + Pad * 2;

                // If there is an image assigned to the header, add its width with padding
                if (lvPlanetary.SmallImageList != null && column.ImageIndex > -1)
                    columnHeaderWidth += lvPlanetary.SmallImageList.ImageSize.Width + Pad;

                // Calculate the width of the header and the items of the column
                int columnMaxWidth = column.ListView.Items.Cast<ListViewItem>().Select(
                    item => TextRenderer.MeasureText(item.SubItems[column.Index].Text, Font).Width).Concat(
                        new[] { columnHeaderWidth }).Max() + Pad + 1;

                // Assign the width found
                column.Width = columnMaxWidth;
            }
        }

        /// <summary>
        /// Updates the item sorter.
        /// </summary>
        private void UpdateSort()
        {
            lvPlanetary.ListViewItemSorter = new ListViewItemComparerByTag<PlanetaryPin>(
                new PlanetaryPinComparer(m_sortCriteria, m_sortAscending));

            UpdateSortVisualFeedback();
        }

        /// <summary>
        /// Updates the sort feedback (the arrow on the header).
        /// </summary>
        private void UpdateSortVisualFeedback()
        {
            foreach (ColumnHeader columnHeader in lvPlanetary.Columns.Cast<ColumnHeader>())
            {
                PlanetaryColumn column = (PlanetaryColumn)columnHeader.Tag;
                if (m_sortCriteria == column)
                    columnHeader.ImageIndex = m_sortAscending ? 0 : 1;
                else
                    columnHeader.ImageIndex = 2;
            }
        }

        /// <summary>
        /// Updates the listview sub-item.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <param name="item">The item.</param>
        /// <param name="column">The column.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private static void SetColumn(PlanetaryPin pin, ListViewItem.ListViewSubItem item, PlanetaryColumn column)
        {
            switch (column)
            {
                case PlanetaryColumn.State:
                    item.Text = pin.State != PlanetaryPinState.None ? pin.State.GetDescription() :
                        string.Empty;
                    item.ForeColor = GetStateColor(pin);
                    break;
                case PlanetaryColumn.TTC:
                    item.Text = pin.TTC;
                    break;
                case PlanetaryColumn.TypeName:
                    item.Text = pin.TypeName;
                    break;
                case PlanetaryColumn.ContentTypeName:
                    item.Text = pin.ContentTypeName;
                    break;
                case PlanetaryColumn.InstallTime:
                    item.Text = pin.InstallTime == DateTime.MinValue ? string.Empty : $"{pin.InstallTime.ToLocalTime()}";
                    break;
                case PlanetaryColumn.EndTime:
                    item.Text = pin.ExpiryTime == DateTime.MinValue ? string.Empty : $"{pin.ExpiryTime.ToLocalTime()}";
                    break;
                case PlanetaryColumn.PlanetName:
                    item.Text = pin.Colony.PlanetName;
                    break;
                case PlanetaryColumn.PlanetTypeName:
                    item.Text = pin.Colony.PlanetTypeName;
                    break;
                case PlanetaryColumn.SolarSystem:
                    item.Text = pin.Colony.SolarSystem.Name;
                    item.ForeColor = pin.Colony.SolarSystem.SecurityLevelColor;
                    break;
                case PlanetaryColumn.Location:
                    item.Text = pin.Colony.FullLocation;
                    break;
                case PlanetaryColumn.Region:
                    item.Text = pin.Colony.SolarSystem.Constellation.Region.Name;
                    break;
                case PlanetaryColumn.Quantity:
                    item.Text = pin.ContentQuantity.ToNumericString(2);
                    break;
                case PlanetaryColumn.QuantityPerCycle:
                    item.Text = pin.QuantityPerCycle.ToNumericString(2);
                    break;
                case PlanetaryColumn.CycleTime:
                    item.Text = $"{pin.CycleTime}";
                    break;
                case PlanetaryColumn.Volume:
                    item.Text = pin.ContentVolume.ToNumericString(2);
                    break;
                case PlanetaryColumn.LinkedTo:
                    item.Text = string.Join(", ", pin.LinkedTo.Select(x=> x.TypeName).Distinct());
                    break;
                case PlanetaryColumn.RoutedTo:
                    item.Text = string.Join(", ", pin.RoutedTo.Select(x => x.TypeName).Distinct());
                    break;
                case PlanetaryColumn.GroupName:
                    item.Text = pin.GroupName;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Checks the given text matches the item.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is text matching] [the specified x]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsTextMatching(PlanetaryPin x, string text) => string.IsNullOrEmpty(text)
       || x.Colony.PlanetName.ToUpperInvariant().Contains(text)
       || x.Colony.PlanetTypeName.ToUpperInvariant().Contains(text)
       || x.Colony.PlanetTypeName.ToUpperInvariant().Contains(text)
       || x.Colony.SolarSystem.Name.ToUpperInvariant().Contains(text)
       || x.Colony.SolarSystem.Constellation.Name.ToUpperInvariant().Contains(text)
       || x.Colony.SolarSystem.Constellation.Region.Name.ToUpperInvariant().Contains(text)
       || x.TypeName.ToUpperInvariant().Contains(text)
       || x.ContentTypeName.ToUpperInvariant().Contains(text);

        /// <summary>
        /// Updates the time to completion.
        /// </summary>
        private void UpdateTimeToCompletion()
        {
            const int Pad = 4;
            int columnTTCIndex = m_columns.IndexOf(m_columns.FirstOrDefault(x => x.Column == PlanetaryColumn.TTC));

            foreach (ListViewItem listViewItem in lvPlanetary.Items.Cast<ListViewItem>())
            {
                PlanetaryPin pin = (PlanetaryPin)listViewItem.Tag;
                if (pin.State != PlanetaryPinState.Extracting)
                    continue;

                // Update the time to completion
                if (columnTTCIndex != -1 && m_columns[columnTTCIndex].Visible)
                {
                    if (m_columnTTCDisplayIndex == -1)
                        m_columnTTCDisplayIndex = lvPlanetary.Columns[columnTTCIndex].DisplayIndex;

                    listViewItem.SubItems[m_columnTTCDisplayIndex].Text = pin.TTC;

                    // Using AutoResizeColumn when TTC is the first column
                    // results to a nasty visual bug due to ListViewItem.ImageIndex placeholder
                    if (m_columnTTCDisplayIndex == 0)
                    {
                        // Calculate column header text width with padding
                        int columnHeaderWidth =
                            TextRenderer.MeasureText(lvPlanetary.Columns[m_columnTTCDisplayIndex].Text, Font).Width + Pad * 2;

                        // If there is an image assigned to the header, add its width with padding
                        if (ilIcons.ImageSize.Width > 0)
                            columnHeaderWidth += ilIcons.ImageSize.Width + Pad;

                        int columnWidth = lvPlanetary.Items.Cast<ListViewItem>().Select(
                            item => TextRenderer.MeasureText(item.SubItems[m_columnTTCDisplayIndex].Text, Font).Width).Concat(
                                new[] { columnHeaderWidth }).Max() + Pad + 2;
                        lvPlanetary.Columns[m_columnTTCDisplayIndex].Width = columnWidth;
                    }
                    else
                        lvPlanetary.AutoResizeColumn(m_columnTTCDisplayIndex, ColumnHeaderAutoResizeStyle.HeaderSize);
                }

                if (pin.TTC.Length != 0)
                    continue;

                // Pin is idle
                pin.State = PlanetaryPinState.Idle;
                UpdateContent();
            }
        }

        /// <summary>
        /// Gets the color of the state.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <returns></returns>
        private static Color GetStateColor(PlanetaryPin pin)
        {
            switch (pin.State)
            {
                case PlanetaryPinState.Extracting:
                    return Color.Green;
                case PlanetaryPinState.Producing:
                    return Color.Orange;
                case PlanetaryPinState.Idle:
                    return Color.DarkRed;
                default:
                    return SystemColors.GrayText;
            }
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
            ListViewExporter.CreateCSV(lvPlanetary);
        }

        /// <summary>
        /// Handles the Tick event of the m_timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void refresh_TimerTick(object sender, EventArgs e)
        {
            UpdateTimeToCompletion();
        }

        /// <summary>
        /// On column reorder we update the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user manually resizes a column, we make sure to update the column preferences.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (m_isUpdatingColumns || m_columns.Count <= e.ColumnIndex)
                return;

            if (m_columns[e.ColumnIndex].Width == lvPlanetary.Columns[e.ColumnIndex].Width)
                return;

            m_columns[e.ColumnIndex].Width = lvPlanetary.Columns[e.ColumnIndex].Width;
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user clicks a column header, we update the sorting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var column = (PlanetaryColumn)lvPlanetary.Columns[e.Column].Tag;
            if (m_sortCriteria == column)
                m_sortAscending = !m_sortAscending;
            else
            {
                m_sortCriteria = column;
                m_sortAscending = true;
            }

            m_isUpdatingColumns = true;

            // Updates the item sorter
            UpdateSort();

            m_isUpdatingColumns = false;
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            lvPlanetary.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void listView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            lvPlanetary.Cursor = CustomCursors.ContextMenu;
        }

        /// <summary>
        /// Handles the Opening event of the contextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            bool visible = lvPlanetary.SelectedItems.Count != 0;
            showInBrowserMenuItem.Visible = showInBrowserMenuSeparator.Visible = visible;
            if (visible)
            {
                var pin = lvPlanetary.SelectedItems[0]?.Tag as PlanetaryPin;
                showCommodityInBrowserMenuItem.Visible = pin != null && pin.ContentTypeID != 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the showInBrowserMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void showInBrowserMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripItem;
            var items = lvPlanetary.SelectedItems;

            if (menuItem != null && items.Count > 0)
            {
                var pin = items[0]?.Tag as PlanetaryPin;

                // showInstallationInBrowserMenuItem
                if (menuItem == showInstallationInBrowserMenuItem)
                {
                    if (pin?.TypeID == null)
                        return;

                    Item installation = StaticItems.GetItemByID(pin.TypeID);

                    if (installation != null)
                        PlanWindow.ShowPlanWindow(Character).ShowItemInBrowser(installation);

                    return;
                }

                // showCommodityInBrowserMenuItem
                if (menuItem == showCommodityInBrowserMenuItem)
                {
                    if (pin?.ContentTypeID == null)
                        return;

                    Item commmodity = StaticItems.GetItemByID(pin.ContentTypeID);

                    if (commmodity != null)
                        PlanWindow.ShowPlanWindow(Character).ShowItemInBrowser(commmodity);

                    return;
                }


                if (pin?.Colony?.PlanetTypeID == null)
                    return;

                Item planet = StaticItems.GetItemByID(pin.Colony.PlanetTypeID);

                if (planet != null)
                    PlanWindow.ShowPlanWindow(Character).ShowItemInBrowser(planet);
            }
        }

        # endregion


        #region Global Events

        /// <summary>
        /// On timer tick, we update the column settings if any changes have been made to them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_TimerTick(object sender, EventArgs e)
        {
            if (!Visible)
            {
                if (m_refreshTimer.Enabled)
                    m_refreshTimer.Stop();

                return;
            }

            // Find how many jobs are active and not ready
            int activePins = lvPlanetary.Items.Cast<ListViewItem>().Select(
                item => (PlanetaryPin)item.Tag).Count(pin => pin.State == PlanetaryPinState.Extracting);

            // We use time dilation according to the ammount of active pins that are active,
            // due to excess CPU usage for computing the 'time to completion' when there are too many pins
            m_refreshTimer.Interval = 900 + 100 * activePins;

            if (!m_columnsChanged)
                return;

            Settings.UI.MainWindow.Planetary.Columns.Clear();
            Settings.UI.MainWindow.Planetary.Columns.AddRange(Columns.Cast<PlanetaryColumnSettings>());

            // Recreate the columns
            Columns = Settings.UI.MainWindow.Planetary.Columns;
            m_columnsChanged = false;
        }

        /// <summary>
        /// When the planetary colonies change update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterPlanetaryColoniesUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (Character == null || e.Character != Character)
                return;

            UpdateColumns();
        }

        /// <summary>
        /// When the planetary pins change update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterPlanetaryLayoutUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (Character == null || e.Character != Character)
                return;

            PlanetaryPins = Character.PlanetaryColonies.SelectMany(x => x.Pins);
            UpdateColumns();
        }
        
        /// <summary>
        /// Handles the PlanetaryPinsCompleted event of the EveMonClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PlanetaryPinsEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CharacterPlaneteryPinsCompleted(object sender, PlanetaryPinsEventArgs e)
        {
            UpdateContent();
        }

        #endregion
    }
}
