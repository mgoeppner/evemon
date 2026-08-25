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
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.Notifications;
using EVEMon.Common.Serialization.Eve;
using EVEMon.Common.SettingsObjects;
using EVEMon.DetailsWindow;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterEveMailMessagesList : UserControl, IListView
    {
        #region Fields

        private readonly List<EveMailMessageColumnSettings> m_columns = new List<EveMailMessageColumnSettings>();
        private readonly List<EveMailMessage> m_list = new List<EveMailMessage>();

        private EVEMailMessagesGrouping m_grouping;
        private EveMailMessageColumn m_sortCriteria;
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
        public CharacterEveMailMessagesList()
        {
            InitializeComponent();

            eveMailReadingPane.HidePane();
            splitContainerMailMessages.Hide();
            lvMailMessages.Hide();
            lvMailMessages.AllowColumnReorder = true;
            lvMailMessages.Columns.Clear();
            
            mailReadLocal.Font = new Font(mailReadLocal.Font, FontStyle.Bold);
            noEVEMailMessagesLabel.Font = new Font(noEVEMailMessagesLabel.Font.FontFamily, 11.25F, FontStyle.Bold);

            ListViewHelper.EnableDoubleBuffer(lvMailMessages);

            lvMailMessages.MouseDown += listView_MouseDown;
            lvMailMessages.MouseMove += listView_MouseMove;
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
                m_grouping = (EVEMailMessagesGrouping)value;
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
        private IEnumerable<EveMailMessage> EVEMailMessages
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
                List<EveMailMessageColumnSettings> newColumns = new List<EveMailMessageColumnSettings>();
                foreach (ColumnHeader header in lvMailMessages.Columns.Cast<ColumnHeader>().OrderBy(x => x.DisplayIndex))
                {
                    EveMailMessageColumnSettings columnSetting =
                        m_columns.First(x => x.Column == (EveMailMessageColumn)header.Tag);
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
                    m_columns.AddRange(value.Cast<EveMailMessageColumnSettings>());

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
            EveMonClient.CharacterEVEMailMessagesUpdated += EveMonClient_CharacterEVEMailMessagesUpdated;
            EveMonClient.CharacterEVEMailingListsUpdated += EveMonClient_CharacterEVEMailingListsUpdated;
            EveMonClient.CharacterEVEMailBodyDownloaded += EveMonClient_CharacterEVEMailBodyDownloaded;
            EveMonClient.EveIDToNameUpdated += EveMonClient_EveIDToNameUpdated;
            EveMonClient.NotificationSent += EveMonClient_NotificationSent;
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
            EveMonClient.CharacterEVEMailMessagesUpdated -= EveMonClient_CharacterEVEMailMessagesUpdated;
            EveMonClient.CharacterEVEMailingListsUpdated -= EveMonClient_CharacterEVEMailingListsUpdated;
            EveMonClient.CharacterEVEMailBodyDownloaded -= EveMonClient_CharacterEVEMailBodyDownloaded;
            EveMonClient.EveIDToNameUpdated -= EveMonClient_EveIDToNameUpdated;
            EveMonClient.NotificationSent -= EveMonClient_NotificationSent;
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

            lvMailMessages.Visible = false;
            eveMailReadingPane.HidePane();

            EVEMailMessages = Character?.EVEMailMessages;
            Columns = Settings.UI.MainWindow.EVEMailMessages.Columns;
            Grouping = Character?.UISettings.EVEMailMessagesGroupBy;
            TextFilter = string.Empty;
            PanePosition = Settings.UI.MainWindow.EVEMailMessages.ReadingPanePosition;

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

            lvMailMessages.BeginUpdate();
            m_isUpdatingColumns = true;

            try
            {
                lvMailMessages.Columns.Clear();
                lvMailMessages.Groups.Clear();
                lvMailMessages.Items.Clear();

                foreach (EveMailMessageColumnSettings column in m_columns.Where(x => x.Visible))
                {
                    ColumnHeader header = lvMailMessages.Columns.Add(column.Column.GetHeader(), column.Width);
                    header.Tag = column.Column;
                }

                // We update the content
                UpdateContent();
            }
            finally
            {
                lvMailMessages.EndUpdate();
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

            int scrollBarPosition = lvMailMessages.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = lvMailMessages.SelectedItems.Count > 0
                ? lvMailMessages.SelectedItems[0].Tag.GetHashCode()
                : 0;

            lvMailMessages.BeginUpdate();
            try
            {
                IEnumerable<EveMailMessage> eveMailMessages = m_list
                    .Where(x => x.SentDate != DateTime.MinValue).Where(x => IsTextMatching(x, m_textFilter));

                UpdateSort();

                UpdateContentByGroup(eveMailMessages);

                // Restore the selected item (if any)
                if (selectedItem > 0)
                {
                    foreach (ListViewItem lvItem in lvMailMessages.Items.Cast<ListViewItem>().Where(
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
                lvMailMessages.EndUpdate();
                lvMailMessages.SetVerticalScrollBarPosition(scrollBarPosition);
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

            noEVEMailMessagesLabel.Visible = lvMailMessages.Items.Count == 0;
            lvMailMessages.Visible = splitContainerMailMessages.Visible = !noEVEMailMessagesLabel.Visible;
        }

        /// <summary>
        /// Updates the content by group.
        /// </summary>
        /// <param name="eveMailMessages">The eve mail messages.</param>
        private void UpdateContentByGroup(IEnumerable<EveMailMessage> eveMailMessages)
        {
            switch (m_grouping)
            {
                case EVEMailMessagesGrouping.State:
                    IOrderedEnumerable<IGrouping<EveMailState, EveMailMessage>> groups0 =
                        eveMailMessages.GroupBy(x => x.State).OrderBy(x => (int)x.Key);
                    UpdateContent(groups0);
                    break;
                case EVEMailMessagesGrouping.StateDesc:
                    IOrderedEnumerable<IGrouping<EveMailState, EveMailMessage>> groups1 =
                        eveMailMessages.GroupBy(x => x.State).OrderByDescending(x => (int)x.Key);
                    UpdateContent(groups1);
                    break;
                case EVEMailMessagesGrouping.SentDate:
                    IOrderedEnumerable<IGrouping<DateTime, EveMailMessage>> groups2 =
                        eveMailMessages.GroupBy(x => x.SentDate.ToLocalTime().Date).OrderBy(x => x.Key);
                    UpdateContent(groups2);
                    break;
                case EVEMailMessagesGrouping.SentDateDesc:
                    IOrderedEnumerable<IGrouping<DateTime, EveMailMessage>> groups3 =
                        eveMailMessages.GroupBy(x => x.SentDate.ToLocalTime().Date).OrderByDescending(x => x.Key);
                    UpdateContent(groups3);
                    break;
                case EVEMailMessagesGrouping.Sender:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups4 =
                        eveMailMessages.GroupBy(x => x.SenderName).OrderBy(x => x.Key);
                    UpdateContent(groups4);
                    break;
                case EVEMailMessagesGrouping.SenderDesc:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups5 =
                        eveMailMessages.GroupBy(x => x.SenderName).OrderByDescending(x => x.Key);
                    UpdateContent(groups5);
                    break;
                case EVEMailMessagesGrouping.Subject:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups6 =
                        eveMailMessages.GroupBy(x => x.Title).OrderBy(x => x.Key);
                    UpdateContent(groups6);
                    break;
                case EVEMailMessagesGrouping.SubjectDesc:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups7 =
                        eveMailMessages.GroupBy(x => x.Title).OrderByDescending(x => x.Key);
                    UpdateContent(groups7);
                    break;
                case EVEMailMessagesGrouping.Recipient:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups8 =
                        eveMailMessages.GroupBy(x => x.ToCharacters.First()).OrderBy(x => x.Key);
                    UpdateContent(groups8);
                    break;
                case EVEMailMessagesGrouping.RecipientDesc:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups9 =
                        eveMailMessages.GroupBy(x => x.ToCharacters.First()).OrderByDescending(x => x.Key);
                    UpdateContent(groups9);
                    break;
                case EVEMailMessagesGrouping.CorpOrAlliance:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups10 =
                        eveMailMessages.GroupBy(x => x.ToCorpOrAlliance).OrderBy(x => x.Key);
                    UpdateContent(groups10);
                    break;
                case EVEMailMessagesGrouping.CorpOrAllianceDesc:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups11 =
                        eveMailMessages.GroupBy(x => x.ToCorpOrAlliance).OrderByDescending(x => x.Key);
                    UpdateContent(groups11);
                    break;
                case EVEMailMessagesGrouping.MailingList:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups12 =
                        eveMailMessages.GroupBy(x => x.ToMailingLists.First()).OrderBy(x => x.Key);
                    UpdateContent(groups12);
                    break;
                case EVEMailMessagesGrouping.MailingListDesc:
                    IOrderedEnumerable<IGrouping<string, EveMailMessage>> groups13 =
                        eveMailMessages.GroupBy(x => x.ToMailingLists.First()).OrderByDescending(x => x.Key);
                    UpdateContent(groups13);
                    break;
            }
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="groups"></param>
        private void UpdateContent<TKey>(IEnumerable<IGrouping<TKey, EveMailMessage>> groups)
        {
            lvMailMessages.Items.Clear();
            lvMailMessages.Groups.Clear();

            // Add the groups
            foreach (IGrouping<TKey, EveMailMessage> group in groups)
            {
                string groupText;
                if (group.Key is EveMailState)
                    groupText = ((EveMailState)(object)group.Key).GetHeader();
                else if (group.Key is DateTime)
                    groupText = ((DateTime)(object)group.Key).ToShortDateString();
                else
                    groupText = group.Key.ToString();

                ListViewGroup listGroup = new ListViewGroup(groupText);
                lvMailMessages.Groups.Add(listGroup);

                // Add the items in every group
                lvMailMessages.Items.AddRange(group.Select(eveMailMessage => new
                {
                    eveMailMessage,
                    item = new ListViewItem(eveMailMessage.SenderName, listGroup)
                    {
                        UseItemStyleForSubItems = false,
                        Tag = eveMailMessage
                    }
                }).Select(x => CreateSubItems(x.eveMailMessage, x.item)).ToArray());
            }
        }

        /// <summary>
        /// Creates the list view sub items.
        /// </summary>
        /// <param name="eveMailMessage">The mail message.</param>
        /// <param name="item">The item.</param>
        private ListViewItem CreateSubItems(EveMailMessage eveMailMessage, ListViewItem item)
        {
            // Add enough subitems to match the number of columns
            while (item.SubItems.Count < lvMailMessages.Columns.Count + 1)
            {
                item.SubItems.Add(string.Empty);
            }

            // Creates the subitems
            for (int i = 0; i < lvMailMessages.Columns.Count; i++)
            {
                SetColumn(eveMailMessage, item.SubItems[i], (EveMailMessageColumn)lvMailMessages.Columns[i].Tag);
            }

            return item;
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        private void AdjustColumns()
        {
            foreach (ColumnHeader column in lvMailMessages.Columns)
            {
                if (m_columns[column.Index].Width == -1)
                    m_columns[column.Index].Width = -2;

                column.Width = m_columns[column.Index].Width;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column and not set to auto-resize
                if (column.Index != lvMailMessages.Columns.Count - 1 || m_columns[column.Index].Width != -2)
                    continue;

                const int Pad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + Pad * 2;

                // If there is an image assigned to the header, add its width with padding
                if (lvMailMessages.SmallImageList != null && column.ImageIndex > -1)
                    columnHeaderWidth += lvMailMessages.SmallImageList.ImageSize.Width + Pad;

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
            lvMailMessages.ListViewItemSorter = new ListViewItemComparerByTag<EveMailMessage>(
                new EveMailMessageComparer(m_sortCriteria, m_sortAscending));

            UpdateSortVisualFeedback();
        }

        /// <summary>
        /// Updates the sort feedback (the arrow on the header).
        /// </summary>
        private void UpdateSortVisualFeedback()
        {
            foreach (ColumnHeader columnHeader in lvMailMessages.Columns.Cast<ColumnHeader>())
            {
                EveMailMessageColumn column = (EveMailMessageColumn)columnHeader.Tag;
                if (m_sortCriteria == column)
                    columnHeader.ImageIndex = m_sortAscending ? 0 : 1;
                else
                    columnHeader.ImageIndex = 2;
            }
        }

        /// <summary>
        /// Updates the listview sub-item.
        /// </summary>
        /// <param name="eveMailMessage"></param>
        /// <param name="item"></param>
        /// <param name="column"></param>
        private static void SetColumn(EveMailMessage eveMailMessage, ListViewItem.ListViewSubItem item, EveMailMessageColumn column)
        {
            switch (column)
            {
                case EveMailMessageColumn.SenderName:
                    item.Text = eveMailMessage.SenderName;
                    break;
                case EveMailMessageColumn.Title:
                    item.Text = eveMailMessage.Title;
                    break;
                case EveMailMessageColumn.SentDate:
                    DateTime sentDateTime = eveMailMessage.SentDate.ToLocalTime();
                    item.Text = $"{sentDateTime:ddd} {sentDateTime:G}";
                    break;
                case EveMailMessageColumn.ToCharacters:
                    item.Text = string.Join(", ", eveMailMessage.ToCharacters);
                    break;
                case EveMailMessageColumn.ToCorpOrAlliance:
                    item.Text = eveMailMessage.ToCorpOrAlliance;
                    break;
                case EveMailMessageColumn.ToMailingList:
                    item.Text = string.Join(", ", eveMailMessage.ToMailingLists);
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
                    splitContainerMailMessages.Panel2Collapsed = true;
                    break;
                case ReadingPanePositioning.Bottom:
                    splitContainerMailMessages.Orientation = Orientation.Horizontal;
                    splitContainerMailMessages.Panel2Collapsed = false;
                    break;
                case ReadingPanePositioning.Right:
                    splitContainerMailMessages.Orientation = Orientation.Vertical;
                    splitContainerMailMessages.Panel2Collapsed = false;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Tries to open the selected mail in the EVEMon Mail Window.
        /// </summary>
        private void ReadMailLocal()
        {
            ListViewItem item = lvMailMessages.SelectedItems[0];
            EveMailMessage message = (EveMailMessage)item.Tag;

            // Show or bring to front if a window with the same EVE mail message already exists
            WindowsFactory.ShowByTag<EveMessageWindow, EveMailMessage>(message);
        }
        
        /// <summary>
        /// Checks the given text matches the item.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        /// 	<c>true</c> if [is text matching] [the specified x]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsTextMatching(EveMailMessage x, string text) => string.IsNullOrEmpty(text)
       || x.SenderName.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.Title.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.ToCorpOrAlliance.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.ToCharacters.Any(y => y.ToUpperInvariant().Contains(text, ignoreCase: true))
       || x.EVEMailBody.BodyText.ToUpperInvariant().Contains(text, ignoreCase: true);

        /// <summary>
        /// Called when selection changed.
        /// </summary>
        private void OnSelectionChanged()
        {
            if (lvMailMessages.SelectedItems.Count == 0)
            {
                eveMailReadingPane.HidePane();
                return;
            }

            EveMailMessage selectedObject = lvMailMessages.SelectedItems[0].Tag as EveMailMessage;
            if (selectedObject == null)
            {
                eveMailReadingPane.HidePane();
                return;
            }

            // If we haven't done it yet, download the mail body
            if (selectedObject.EVEMailBody.MessageID == 0)
            {
                selectedObject.GetMailBody();
                return;
            }

            eveMailReadingPane.SelectedObject = selectedObject;
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
            ListViewExporter.CreateCSV(lvMailMessages);
        }

        /// <summary>
        /// Shows the context menu only when a message is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            mailReadLocal.Enabled = lvMailMessages.SelectedItems.Count != 0;
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
        private void lvMailMessages_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (timer.Enabled)
                return;

            timer.Start();
        }

        /// <summary>
        /// Opens a window form to display the EVE mail body.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lvMailMessages_DoubleClick(object sender, EventArgs e)
        {
            ReadMailLocal();
        }

        /// <summary>
        /// On column reorder we update the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvMailMessages_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user manually resizes a column, we make sure to update the column preferences.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvMailMessages_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (m_isUpdatingColumns || m_columns.Count <= e.ColumnIndex)
                return;

            if (m_columns[e.ColumnIndex].Width == lvMailMessages.Columns[e.ColumnIndex].Width)
                return;

            m_columns[e.ColumnIndex].Width = lvMailMessages.Columns[e.ColumnIndex].Width;
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user clicks a column header, we update the sorting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvMailMessages_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            EveMailMessageColumn column = (EveMailMessageColumn)lvMailMessages.Columns[e.Column].Tag;
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

            lvMailMessages.Cursor = Cursors.Default;
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

            lvMailMessages.Cursor = CustomCursors.ContextMenu;
        }

        /// <summary>
        /// Picking "Read" in the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mailReadLocal_Click(object sender, EventArgs e)
        {
            ReadMailLocal();
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

            Settings.UI.MainWindow.EVEMailMessages.Columns.Clear();
            Settings.UI.MainWindow.EVEMailMessages.Columns.AddRange(Columns.Cast<EveMailMessageColumnSettings>());

            // Recreate the columns
            Columns = Settings.UI.MainWindow.EVEMailMessages.Columns;
            m_columnsChanged = false;
        }

        /// <summary>
        /// When the mail messages change update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterEVEMailMessagesUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (Character == null || e.Character != Character)
                return;

            EVEMailMessages = Character.EVEMailMessages;
            UpdateColumns();
        }

        /// <summary>
        /// When the mailing lists change update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterEVEMailingListsUpdated(object sender, CharacterChangedEventArgs e)
        {
            UpdateColumns();
        }

        /// <summary>
        /// When the mail message body gets downloaded update the reading pane.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CharacterEVEMailBodyDownloaded(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != Character)
                return;

            OnSelectionChanged();
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
        /// Handles the NotificationSent event of the EveMonClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EVEMon.Common.Notifications.NotificationEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_NotificationSent(object sender, NotificationEventArgs e)
        {
            if (Character == null)
                return;

            APIErrorNotificationEventArgs notification = e as APIErrorNotificationEventArgs;

            CCPAPIResult<SerializableAPIMailBodies> notificationResult =
                notification?.Result as CCPAPIResult<SerializableAPIMailBodies>;

            if (notificationResult == null)
                return;

            // In case there was an error, hide the pane
            if (notification.Result.HasError)
                eveMailReadingPane.HidePane();
        }

        #endregion
    }
}
