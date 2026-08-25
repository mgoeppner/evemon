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
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterWalletJournalList : UserControl, IListView
    {
        #region Fields

        private readonly List<WalletJournalColumnSettings> m_columns = new List<WalletJournalColumnSettings>();
        private readonly List<WalletJournal> m_list = new List<WalletJournal>();

        private WalletJournalGrouping m_grouping;
        private WalletJournalColumn m_sortCriteria;

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
        public CharacterWalletJournalList()
        {
            InitializeComponent();

            lvWalletJournal.Hide();
            lvWalletJournal.AllowColumnReorder = true;
            lvWalletJournal.Columns.Clear();

            noWalletJournalLabel.Font = new Font(noWalletJournalLabel.Font.FontFamily, 11.25F, FontStyle.Bold);

            ListViewHelper.EnableDoubleBuffer(lvWalletJournal);

            lvWalletJournal.ColumnClick += listView_ColumnClick;
            lvWalletJournal.ColumnWidthChanged += listView_ColumnWidthChanged;
            lvWalletJournal.ColumnReordered += listView_ColumnReordered;
            lvWalletJournal.MouseDown += listView_MouseDown;
            lvWalletJournal.MouseMove += listView_MouseMove;
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
        /// Gets or sets the enumeration of research points to display.
        /// </summary>
        private IEnumerable<WalletJournal> WalletJournal
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
        /// Gets or sets the grouping of a listview.
        /// </summary>
        /// <value></value>
        [Browsable(false)]
        public Enum Grouping
        {
            get { return m_grouping; }
            set
            {
                m_grouping = (WalletJournalGrouping)value;
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
                List<WalletJournalColumnSettings> newColumns = new List<WalletJournalColumnSettings>();
                foreach (ColumnHeader header in lvWalletJournal.Columns.Cast<ColumnHeader>().OrderBy(x => x.DisplayIndex))
                {
                    WalletJournalColumnSettings columnSetting =
                        m_columns.First(x => x.Column == (WalletJournalColumn)header.Tag);
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
                    m_columns.AddRange(value.Cast<WalletJournalColumnSettings>());

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
            EveMonClient.RefTypesUpdated += EveMonClient_RefTypesUpdated;
            EveMonClient.EveIDToNameUpdated += EveMonClient_EveIDToNameUpdated;
            EveMonClient.CharacterWalletJournalUpdated += EveMonClient_CharacterWalletJournalUpdated;
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
            EveMonClient.RefTypesUpdated -= EveMonClient_RefTypesUpdated;
            EveMonClient.EveIDToNameUpdated -= EveMonClient_EveIDToNameUpdated;
            EveMonClient.CharacterWalletJournalUpdated -= EveMonClient_CharacterWalletJournalUpdated;
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

            lvWalletJournal.Visible = false;

            WalletJournal = Character?.WalletJournal;
            Columns = Settings.UI.MainWindow.WalletJournal.Columns;
            Grouping = Character?.UISettings.WalletJournalGroupBy;
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
            m_columns.ForEach(column => {
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

            lvWalletJournal.BeginUpdate();
            m_isUpdatingColumns = true;

            try
            {
                lvWalletJournal.Columns.Clear();
                lvWalletJournal.Groups.Clear();
                lvWalletJournal.Items.Clear();

                foreach (WalletJournalColumnSettings column in m_columns.Where(x => x.Visible))
                {
                    ColumnHeader header = lvWalletJournal.Columns.Add(column.Column.GetHeader(), column.Width);
                    header.Tag = column.Column;

                    switch (column.Column)
                    {
                        case WalletJournalColumn.Amount:
                        case WalletJournalColumn.Balance:
                        case WalletJournalColumn.TaxAmount:
                            header.TextAlign = HorizontalAlignment.Right;
                            break;
                    }
                }

                // We update the content
                UpdateContent();
            }
            finally
            {
                lvWalletJournal.EndUpdate();
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

            int scrollBarPosition = lvWalletJournal.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = lvWalletJournal.SelectedItems.Count > 0
                ? lvWalletJournal.SelectedItems[0].Tag.GetHashCode() : 0;

            lvWalletJournal.BeginUpdate();
            try
            {
                IEnumerable<WalletJournal> walletJournalTransactions = m_list.Where(x => IsTextMatching(x, m_textFilter));

                UpdateSort();

                UpdateContentByGroup(walletJournalTransactions);

                // Restore the selected item (if any)
                if (selectedItem > 0)
                {
                    foreach (ListViewItem lvItem in lvWalletJournal.Items.Cast<ListViewItem>().Where(
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
                lvWalletJournal.EndUpdate();
                lvWalletJournal.SetVerticalScrollBarPosition(scrollBarPosition);
            }
        }

        /// <summary>
        /// Updates the list visibility.
        /// </summary>
        private void UpdateListVisibility()
        {
            // Display or hide the "no wallet Journal" label
            if (!m_init)
                return;

            noWalletJournalLabel.Visible = lvWalletJournal.Items.Count == 0;
            lvWalletJournal.Visible = !noWalletJournalLabel.Visible;
        }

        /// <summary>
        /// Updates the content by group.
        /// </summary>
        /// <param name="walletJournalTransactions">The wallet journal transactions.</param>
        private void UpdateContentByGroup(IEnumerable<WalletJournal> walletJournalTransactions)
        {
            switch (m_grouping)
            {
                case WalletJournalGrouping.None:
                    UpdateNoGroupContent(walletJournalTransactions);
                    break;
                case WalletJournalGrouping.Date:
                    IOrderedEnumerable<IGrouping<DateTime, WalletJournal>> groups1 =
                        walletJournalTransactions.GroupBy(x => x.Date.ToLocalTime().Date).OrderBy(x => x.Key);
                    UpdateContent(groups1);
                    break;
                case WalletJournalGrouping.DateDesc:
                    IOrderedEnumerable<IGrouping<DateTime, WalletJournal>> groups2 =
                        walletJournalTransactions.GroupBy(x => x.Date.ToLocalTime().Date).OrderByDescending(x => x.Key);
                    UpdateContent(groups2);
                    break;
                case WalletJournalGrouping.Type:
                    IOrderedEnumerable<IGrouping<string, WalletJournal>> groups3 =
                        walletJournalTransactions.GroupBy(x => x.Type).OrderBy(x => x.Key);
                    UpdateContent(groups3);
                    break;
                case WalletJournalGrouping.TypeDesc:
                    IOrderedEnumerable<IGrouping<string, WalletJournal>> groups4 =
                        walletJournalTransactions.GroupBy(x => x.Type).OrderByDescending(x => x.Key);
                    UpdateContent(groups4);
                    break;
                case WalletJournalGrouping.Issuer:
                    IOrderedEnumerable<IGrouping<string, WalletJournal>> groups5 =
                        walletJournalTransactions.GroupBy(x => x.Issuer).OrderBy(x => x.Key);
                    UpdateContent(groups5);
                    break;
                case WalletJournalGrouping.IssuerDesc:
                    IOrderedEnumerable<IGrouping<string, WalletJournal>> groups6 =
                        walletJournalTransactions.GroupBy(x => x.Issuer).OrderByDescending(x => x.Key);
                    UpdateContent(groups6);
                    break;
                case WalletJournalGrouping.Recipient:
                    IOrderedEnumerable<IGrouping<string, WalletJournal>> groups7 =
                        walletJournalTransactions.GroupBy(x => x.Recipient).OrderBy(x => x.Key);
                    UpdateContent(groups7);
                    break;
                case WalletJournalGrouping.RecipientDesc:
                    IOrderedEnumerable<IGrouping<string, WalletJournal>> groups8 =
                        walletJournalTransactions.GroupBy(x => x.Recipient).OrderByDescending(x => x.Key);
                    UpdateContent(groups8);
                    break;
            }
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        private void UpdateNoGroupContent(IEnumerable<WalletJournal> walletJournalTransactions)
        {
            lvWalletJournal.Items.Clear();
            lvWalletJournal.Groups.Clear();

            // Add the items
            lvWalletJournal.Items.AddRange(walletJournalTransactions.Select(walletJournal => new
            {
                walletJournal,
                item = new ListViewItem($"{walletJournal.Date.ToLocalTime()}")
                {
                    UseItemStyleForSubItems = false,
                    Tag = walletJournal
                }
            }).Select(x => CreateSubItems(x.walletJournal, x.item)).ToArray());
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="groups"></param>
        private void UpdateContent<TKey>(IEnumerable<IGrouping<TKey, WalletJournal>> groups)
        {
            lvWalletJournal.Items.Clear();
            lvWalletJournal.Groups.Clear();

            // Add the groups
            foreach (IGrouping<TKey, WalletJournal> group in groups)
            {
                string groupText;
                if (group.Key is DateTime)
                    groupText = ((DateTime)(object)group.Key).ToShortDateString();
                else
                    groupText = group.Key.ToString();

                ListViewGroup listGroup = new ListViewGroup(groupText);
                lvWalletJournal.Groups.Add(listGroup);

                // Add the items in every group
                lvWalletJournal.Items.AddRange(group.Select(walletJournal => new
                {
                    walletJournal,
                    item = new ListViewItem($"{walletJournal.Date.ToLocalTime()}", listGroup)
                    {
                        UseItemStyleForSubItems = false,
                        Tag = walletJournal
                    }
                }).Select(x => CreateSubItems(x.walletJournal, x.item)).ToArray());
            }
        }

        /// <summary>
        /// Creates the list view sub items.
        /// </summary>
        /// <param name="walletJournal">The WalletJournal.</param>
        /// <param name="item">The item.</param>
        private ListViewItem CreateSubItems(WalletJournal walletJournal, ListViewItem item)
        {
            // Add enough subitems to match the number of columns
            while (item.SubItems.Count < lvWalletJournal.Columns.Count + 1)
            {
                item.SubItems.Add(string.Empty);
            }

            // Creates the subitems
            for (int i = 0; i < lvWalletJournal.Columns.Count; i++)
            {
                SetColumn(walletJournal, item.SubItems[i], (WalletJournalColumn)lvWalletJournal.Columns[i].Tag);
            }

            return item;
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        private void AdjustColumns()
        {
            foreach (ColumnHeader column in lvWalletJournal.Columns)
            {
                if (m_columns[column.Index].Width == -1)
                    m_columns[column.Index].Width = -2;

                column.Width = m_columns[column.Index].Width;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column and not set to auto-resize
                if (column.Index != lvWalletJournal.Columns.Count - 1 || m_columns[column.Index].Width != -2)
                    continue;

                const int Pad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + Pad * 2;

                // If there is an image assigned to the header, add its width with padding
                if (lvWalletJournal.SmallImageList != null && column.ImageIndex > -1)
                    columnHeaderWidth += lvWalletJournal.SmallImageList.ImageSize.Width + Pad;

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
            lvWalletJournal.ListViewItemSorter = new ListViewItemComparerByTag<WalletJournal>(
                new WalletJournalComparer(m_sortCriteria, m_sortAscending));

            UpdateSortVisualFeedback();
        }

        /// <summary>
        /// Updates the sort feedback (the arrow on the header).
        /// </summary>
        private void UpdateSortVisualFeedback()
        {
            foreach (ColumnHeader columnHeader in lvWalletJournal.Columns.Cast<ColumnHeader>())
            {
                WalletJournalColumn column = (WalletJournalColumn)columnHeader.Tag;
                if (m_sortCriteria == column)
                    columnHeader.ImageIndex = m_sortAscending ? 0 : 1;
                else
                    columnHeader.ImageIndex = 2;
            }
        }

        /// <summary>
        /// Updates the listview sub-item.
        /// </summary>
        /// <param name="walletJournal"></param>
        /// <param name="item"></param>
        /// <param name="column"></param>
        private static void SetColumn(WalletJournal walletJournal, ListViewItem.ListViewSubItem item,
                                      WalletJournalColumn column)
        {
            bool numberFormat = Settings.UI.MainWindow.WalletJournal.NumberAbsFormat;

            switch (column)
            {
                case WalletJournalColumn.Date:
                    item.Text = $"{walletJournal.Date.ToLocalTime():G}";
                    break;
                case WalletJournalColumn.Type:
                    item.Text = walletJournal.Type;
                    break;
                case WalletJournalColumn.Amount:
                    item.Text = numberFormat
                        ? FormatHelper.Format(walletJournal.Amount, AbbreviationFormat.AbbreviationSymbols)
                        : walletJournal.Amount.ToNumericString(2);
                    item.ForeColor = walletJournal.Amount < 0 ? Color.DarkRed : Color.DarkGreen;
                    break;
                case WalletJournalColumn.Balance:
                    item.Text = numberFormat
                        ? FormatHelper.Format(walletJournal.Balance, AbbreviationFormat.AbbreviationSymbols)
                        : walletJournal.Balance.ToNumericString(2);
                    break;
                case WalletJournalColumn.Reason:
                    item.Text = walletJournal.Reason;
                    break;
                case WalletJournalColumn.Issuer:
                    item.Text = walletJournal.Issuer;
                    break;
                case WalletJournalColumn.Recipient:
                    item.Text = walletJournal.Recipient;
                    break;
                case WalletJournalColumn.TaxReceiver:
                    item.Text = walletJournal.TaxReceiver;
                    break;
                case WalletJournalColumn.TaxAmount:
                    item.Text = numberFormat
                        ? FormatHelper.Format(walletJournal.TaxAmount, AbbreviationFormat.AbbreviationSymbols)
                        : walletJournal.TaxAmount.ToNumericString(2);
                    break;
                case WalletJournalColumn.ID:
                    item.Text = walletJournal.ID.ToString(CultureConstants.DefaultCulture);
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
        private static bool IsTextMatching(WalletJournal x, string text) => string.IsNullOrEmpty(text)
       || x.Type.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.Reason.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.Issuer.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.Recipient.ToUpperInvariant().Contains(text, ignoreCase: true)
       || x.TaxReceiver.ToUpperInvariant().Contains(text, ignoreCase: true);

        #endregion


        #region Local Event Handlers

        /// <summary>
        /// Exports item info to CSV format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewExporter.CreateCSV(lvWalletJournal);
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

            if (m_columns[e.ColumnIndex].Width == lvWalletJournal.Columns[e.ColumnIndex].Width)
                return;

            m_columns[e.ColumnIndex].Width = lvWalletJournal.Columns[e.ColumnIndex].Width;
            m_columnsChanged = true;
        }

        /// <summary>
        /// When the user clicks a column header, we update the sorting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            WalletJournalColumn column = (WalletJournalColumn)lvWalletJournal.Columns[e.Column].Tag;
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

            lvWalletJournal.Cursor = Cursors.Default;
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

            lvWalletJournal.Cursor = CustomCursors.ContextMenu;
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

            Settings.UI.MainWindow.WalletJournal.Columns.Clear();
            Settings.UI.MainWindow.WalletJournal.Columns.AddRange(Columns.Cast<WalletJournalColumnSettings>());

            // Recreate the columns
            Columns = Settings.UI.MainWindow.WalletJournal.Columns;
            m_columnsChanged = false;
        }

        /// <summary>
        /// When the RefTypes list updates, update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_RefTypesUpdated(object sender, EventArgs e)
        {
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
        /// When the wallet Journal change update the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterWalletJournalUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (Character == null || e.Character != Character)
                return;

            WalletJournal = Character.WalletJournal;
            UpdateColumns();
        }

        #endregion
    }
}
