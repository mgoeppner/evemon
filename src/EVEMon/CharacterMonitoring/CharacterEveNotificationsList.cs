using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Collections;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.Models.Extended;
using EVEMon.Common.Notifications;
using EVEMon.Common.Serialization.Eve;
using EVEMon.Common.SettingsObjects;
using EVEMon.DetailsWindow;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterEveNotificationsList : UserControl, IListView
    {
        #region Fields

        private readonly List<EveNotificationColumnSettings> m_columns = new List<EveNotificationColumnSettings>();
        private readonly List<EveNotification> m_list = new List<EveNotification>();

        private EVENotificationsGrouping m_grouping;
        private EveNotificationColumn m_sortCriteria;
        private ReadingPanePositioning m_panePosition;

        private string m_textFilter = string.Empty;
        private bool m_sortAscending;
        private bool m_columnsChanged;
        private bool m_isUpdatingColumns;
        private bool m_init;

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CharacterEveNotificationsList()
        {
            InitializeComponent();

            eveNotificationReadingPane.HidePane();
            splitContainerNotifications.Hide();
            lvNotifications.Hide();
            lvNotifications.AllowColumnReorder = true;
            lvNotifications.Columns.Clear();

            noEVENotificationsLabel.Font = new Font(noEVENotificationsLabel.Font.FontFamily, 11.25F, FontStyle.Bold);

            ListViewHelper.EnableDoubleBuffer(lvNotifications);

            lvNotifications.MouseDown += listView_MouseDown;
            lvNotifications.MouseMove += listView_MouseMove;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the character associated with this monitor.
        /// </summary>
        internal CCPCharacter Character { get; set; }

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
        /// Gets or sets the grouping mode.
        /// </summary>
        [Browsable(false)]
        public Enum Grouping
        {
            get { return m_grouping; }
            set
            {
                m_grouping = (EVENotificationsGrouping)value;
                if (m_init)
                    UpdateColumns();
            }
        }

        /// <summary>
        /// Gets or sets the pane position.
        /// </summary>
        internal ReadingPanePositioning PanePosition
        {
            get { return m_panePosition; }
            set
            {
                m_panePosition = value;
                UpdatePanePosition();
            }
        }

        /// <summary>
        /// Gets or sets the enumeration of EVE mail messages to display.
        /// </summary>
        private IEnumerable<EveNotification> EVENotifications
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
        /// Gets or sets the settings used for columns.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IEnumerable<IColumnSettings> Columns
        {
            get
            {
                // Add the visible columns; matching the display order
                List<EveNotificationColumnSettings> newColumns = new List<EveNotificationColumnSettings>();
                foreach (ColumnHeader header in lvNotifications.Columns.Cast<ColumnHeader>().OrderBy(x => x.DisplayIndex))
                {
                    EveNotificationColumnSettings columnSetting =
                        m_columns.First(x => x.Column == (EveNotificationColumn)header.Tag);
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
                    m_columns.AddRange(value.Cast<EveNotificationColumnSettings>());

                if (m_init)
                    UpdateColumns();
            }
        }

        #endregion


        # region Inherited Events

        /// <summary>
        /// On load subscribe the events.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || this.IsDesignModeHosted())
                return;

            EveMonClient.TimerTick += EveMonClient_TimerTick;
            EveMonClient.CharacterEVENotificationsUpdated += EveMonClient_CharacterEVENotificationsUpdated;
            EveMonClient.EveIDToNameUpdated += EveMonClient_EveIDToNameUpdated;
            EveMonClient.NotificationRefTypesUpdated += EveMonClient_NotificationRefTypesUpdated;
            EveNotificationTextParser.NotificationTextParserUpdated += EveNotificationTextParser_NotificationTextParserUpdated;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.TimerTick -= EveMonClient_TimerTick;
            EveMonClient.CharacterEVENotificationsUpdated -= EveMonClient_CharacterEVENotificationsUpdated;
            EveMonClient.EveIDToNameUpdated -= EveMonClient_EveIDToNameUpdated;
            EveMonClient.NotificationRefTypesUpdated -= EveMonClient_NotificationRefTypesUpdated;
            EveNotificationTextParser.NotificationTextParserUpdated -= EveNotificationTextParser_NotificationTextParserUpdated;
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

            lvNotifications.Visible = false;
            eveNotificationReadingPane.HidePane();

            EVENotifications = Character?.EVENotifications;
            Columns = Settings.UI.MainWindow.EVENotifications.Columns;
            Grouping = Character?.UISettings.EVENotificationsGroupBy;
            PanePosition = Settings.UI.MainWindow.EVENotifications.ReadingPanePosition;
            TextFilter = string.Empty;

            UpdateColumns();
            m_init = true;

            UpdateListVisibility();
        }

        # endregion


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
        private void UpdateColumns()
        {
            // Returns if not visible
            if (!Visible)
                return;

            lvNotifications.BeginUpdate();
            m_isUpdatingColumns = true;

            try
            {
                lvNotifications.Columns.Clear();
                lvNotifications.Groups.Clear();
                lvNotifications.Items.Clear();

                foreach (EveNotificationColumnSettings column in m_columns.Where(x => x.Visible))
                {
                    ColumnHeader header = lvNotifications.Columns.Add(column.Column.GetHeader(), column.Width);
                    header.Tag = column.Column;
                }

                // We update the content
                UpdateContent();
            }
            finally
            {
                lvNotifications.EndUpdate();
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

            int scrollBarPosition = lvNotifications.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = lvNotifications.SelectedItems.Count > 0 ? lvNotifications.
                SelectedItems[0].Tag.GetHashCode() : 0;

            lvNotifications.BeginUpdate();
            splitContainerNotifications.Visible = false;
            try
            {
                IEnumerable<EveNotification> eveNotifications = m_list .Where(x => x.SentDate
                    != DateTime.MinValue).Where(x => IsTextMatching(x, m_textFilter));

                UpdateSort();

                UpdateContentByGroup(eveNotifications);

                // Restore the selected item (if any)
                if (selectedItem > 0)
                {
                    foreach (ListViewItem lvItem in lvNotifications.Items.Cast<ListViewItem>()
                        .Where(lvItem => lvItem.Tag.GetHashCode() == selectedItem))
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
                lvNotifications.EndUpdate();
                lvNotifications.SetVerticalScrollBarPosition(scrollBarPosition);
            }
        }

        /// <summary>
        /// Updates the list visibility.
        /// </summary>
        private void UpdateListVisibility()
        {
            // Display or hide the "no EVE mail messages" label
            if (!m_init)
                return;

            noEVENotificationsLabel.Visible = lvNotifications.Items.Count == 0;
            lvNotifications.Visible = splitContainerNotifications.Visible = !noEVENotificationsLabel.Visible;
        }

        /// <summary>
        /// Updates the content by group.
        /// </summary>
        /// <param name="eveNotifications">The eve notifications.</param>
        private void UpdateContentByGroup(IEnumerable<EveNotification> eveNotifications)
        {
            switch (m_grouping)
            {
                case EVENotificationsGrouping.Type:
                    IOrderedEnumerable<IGrouping<string, EveNotification>> groups0 =
                        eveNotifications.GroupBy(x => x.TypeName).OrderBy(x => x.Key);
                    UpdateContent(groups0);
                    break;
                case EVENotificationsGrouping.TypeDesc:
                    IOrderedEnumerable<IGrouping<string, EveNotification>> groups1 =
                        eveNotifications.GroupBy(x => x.TypeName).OrderByDescending(x => x.Key);
                    UpdateContent(groups1);
                    break;
                case EVENotificationsGrouping.SentDate:
                    IOrderedEnumerable<IGrouping<DateTime, EveNotification>> groups2 =
                        eveNotifications.GroupBy(x => x.SentDate.ToLocalTime().Date).OrderBy(x => x.Key);
                    UpdateContent(groups2);
                    break;
                case EVENotificationsGrouping.SentDateDesc:
                    IOrderedEnumerable<IGrouping<DateTime, EveNotification>> groups3 =
                        eveNotifications.GroupBy(x => x.SentDate.ToLocalTime().Date).OrderByDescending(x => x.Key);
                    UpdateContent(groups3);
                    break;
                case EVENotificationsGrouping.Sender:
                    IOrderedEnumerable<IGrouping<string, EveNotification>> groups4 =
                        eveNotifications.GroupBy(x => x.SenderName).OrderBy(x => x.Key);
                    UpdateContent(groups4);
                    break;
                case EVENotificationsGrouping.SenderDesc:
                    IOrderedEnumerable<IGrouping<string, EveNotification>> groups5 =
                        eveNotifications.GroupBy(x => x.SenderName).OrderByDescending(x => x.Key);
                    UpdateContent(groups5);
                    break;
            }
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="groups"></param>
        private void UpdateContent<TKey>(IEnumerable<IGrouping<TKey, EveNotification>> groups)
        {
            lvNotifications.Items.Clear();
            lvNotifications.Groups.Clear();

            // Add the groups
            foreach (IGrouping<TKey, EveNotification> group in groups)
            {
                string groupText;
                if (group.Key is EveMailState)
                    groupText = ((EveMailState)(object)group.Key).GetHeader();
                else if (group.Key is DateTime)
                    groupText = ((DateTime)(object)group.Key).ToShortDateString();
                else
                    groupText = group.Key.ToString();

                ListViewGroup listGroup = new ListViewGroup(groupText);
                lvNotifications.Groups.Add(listGroup);

                // Add the items in every group
                lvNotifications.Items.AddRange(
                    group.Select(eveNotification => new
                    {
                        eveNotification,
                        item = new ListViewItem(eveNotification.SenderName, listGroup)
                        {
                            UseItemStyleForSubItems = false,
                            Tag = eveNotification
                        }
                    }).Select(x => CreateSubItems(x.eveNotification, x.item)).ToArray());
            }
        }

        /// <summary>
        /// Creates the list view sub items.
        /// </summary>
        /// <param name="eveNotification">The notification.</param>
        /// <param name="item">The item.</param>
        private ListViewItem CreateSubItems(EveNotification eveNotification, ListViewItem item)
        {
            // Add enough subitems to match the number of columns
            while (item.SubItems.Count < lvNotifications.Columns.Count + 1)
            {
                item.SubItems.Add(string.Empty);
            }

            // Creates the subitems
            for (int i = 0; i < lvNotifications.Columns.Count; i++)
            {
                SetColumn(eveNotification, item.SubItems[i], (EveNotificationColumn)lvNotifications.Columns[i].Tag);
            }

            return item;
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        private void AdjustColumns()
        {
            foreach (ColumnHeader column in lvNotifications.Columns)
            {
                if (m_columns[column.Index].Width == -1)
                    m_columns[column.Index].Width = -2;

                column.Width = m_columns[column.Index].Width;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column and not set to auto-resize
                if (column.Index != lvNotifications.Columns.Count - 1 || m_columns[column.Index].Width != -2)
                    continue;

                const int Pad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + Pad * 2;

                // If there is an image assigned to the header, add its width with padding
                if (lvNotifications.SmallImageList != null && column.ImageIndex > -1)
                    columnHeaderWidth += lvNotifications.SmallImageList.ImageSize.Width + Pad;

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
            lvNotifications.ListViewItemSorter = new ListViewItemComparerByTag<EveNotification>(
                new EveNotificationComparer(m_sortCriteria, m_sortAscending));

            UpdateSortVisualFeedback();
        }

        /// <summary>
        /// Updates the sort feedback (the arrow on the header).
        /// </summary>
        private void UpdateSortVisualFeedback()
        {
            foreach (ColumnHeader columnHeader in lvNotifications.Columns.Cast<ColumnHeader>())
            {
                EveNotificationColumn column = (EveNotificationColumn)columnHeader.Tag;
                if (m_sortCriteria == column)
                    columnHeader.ImageIndex = m_sortAscending ? 0 : 1;
                else
                    columnHeader.ImageIndex = 2;
            }
        }

        /// <summary>
        /// Updates the listview sub-item.
        /// </summary>
        /// <param name="eveNotification"></param>
        /// <param name="item"></param>
        /// <param name="column"></param>
        private static void SetColumn(EveNotification eveNotification, ListViewItem.ListViewSubItem item,
            EveNotificationColumn column)
        {
            switch (column)
            {
                case EveNotificationColumn.SenderName:
                    item.Text = eveNotification.SenderName;
                    break;
                case EveNotificationColumn.Type:
                    item.Text = eveNotification.Title;
                    break;
                case EveNotificationColumn.SentDate:
                    DateTime sentDateTime = eveNotification.SentDate.ToLocalTime();
                    item.Text = $"{sentDateTime:ddd} {sentDateTime:G}";
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Updates the pane position.
        /// </summary>
        private void UpdatePanePosition()
        {
            switch (PanePosition)
            {
                case ReadingPanePositioning.Off:
                    splitContainerNotifications.Panel2Collapsed = true;
                    break;
                case ReadingPanePositioning.Bottom:
                    splitContainerNotifications.Orientation = Orientation.Horizontal;
                    splitContainerNotifications.Panel2Collapsed = false;
                    break;
                case ReadingPanePositioning.Right:
                    splitContainerNotifications.Orientation = Orientation.Vertical;
                    splitContainerNotifications.Panel2Collapsed = false;
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
        private static bool IsTextMatching(EveNotification x, string text) => string.IsNullOrEmpty(text)
       || x.SenderName.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.Title.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.Text.ToUpperInvariant().Contains(text, ignoreCase: true);

        /// <summary>
        /// Called when selection changed.
        /// </summary>
        private void OnSelectionChanged()
        {
            if (lvNotifications.SelectedItems.Count == 0)
            {
                eveNotificationReadingPane.HidePane();
                return;
            }

            EveNotification selectedObject = lvNotifications.SelectedItems[0].Tag as EveNotification;
            if (selectedObject == null)
            {
                eveNotificationReadingPane.HidePane();
                return;
            }
            
            eveNotificationReadingPane.SelectedObject = selectedObject;
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
            ListViewExporter.CreateCSV(lvNotifications);
        }

        /// <summary>
        /// When the selection update timer ticks, we process the changes caused by a selection change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            OnSelectionChanged();
        }

        /// <summary>
        /// When the user selects another item, we do not immediately process the change but rather delay it through a timer.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ListViewItemSelectionChangedEventArgs"/> instance containing the event data.</param>
        private void lvNotifications_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (timer.Enabled)
                return;

            timer.Start();
        }

        /// <summary>
        /// Opens a window form to display the EVE notification.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lvNotifications_DoubleClick(object sender, EventArgs e)
        {
            var items = lvNotifications.SelectedItems;
            if (items.Count > 0)
            {
                var item = items[0];
                var notification = (EveNotification)item.Tag;

                // Quit if we haven't downloaded the notification text yet
                if (notification.EVENotificationText != null)
                    // Show or bring to front if a window with the same EVE notification already exists
                    WindowsFactory.ShowByTag<EveMessageWindow, EveNotification>(notification);
            }
        }

        /// <summary>
        /// On column reorder we update the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvNotifications_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user manually resizes a column, we make sure to update the column preferences.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvNotifications_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (m_isUpdatingColumns || m_columns.Count <= e.ColumnIndex)
                return;

            if (m_columns[e.ColumnIndex].Width == lvNotifications.Columns[e.ColumnIndex].Width)
                return;

            m_columns[e.ColumnIndex].Width = lvNotifications.Columns[e.ColumnIndex].Width;
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user clicks a column header, we update the sorting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvNotifications_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var column = (EveNotificationColumn)lvNotifications.Columns[e.Column].Tag;
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
            if (e.Button != MouseButtons.Right)
                return;

            lvNotifications.Cursor = Cursors.Default;
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

            lvNotifications.Cursor = CustomCursors.ContextMenu;
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
            if (!Visible || !m_columnsChanged)
                return;

            Settings.UI.MainWindow.EVENotifications.Columns.Clear();
            Settings.UI.MainWindow.EVENotifications.Columns.AddRange(Columns.Cast<EveNotificationColumnSettings>());

            // Recreate the columns
            Columns = Settings.UI.MainWindow.EVENotifications.Columns;
            m_columnsChanged = false;
        }

        /// <summary>
        /// When the notifications change update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterEVENotificationsUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (Character == null || e.Character != Character)
                return;

            EVENotifications = Character.EVENotifications;
            UpdateColumns();
        }
        
        /// <summary>
        /// When the EveIDToName list updates, update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_EveIDToNameUpdated(object sender, EventArgs e)
        {
            UpdateColumns();
        }

        /// <summary>
        /// When the NotificationRefTypes list updates, update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_NotificationRefTypesUpdated(object sender, EventArgs e)
        {
            UpdateColumns();
        }
        
        /// <summary>
        /// When the notification text parser updates, update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveNotificationTextParser_NotificationTextParserUpdated(object sender, EventArgs e)
        {
            UpdateColumns();
        }

        #endregion
    }
}
