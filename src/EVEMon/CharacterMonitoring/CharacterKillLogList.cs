using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.Properties;
using EVEMon.Common.SettingsObjects;
using EVEMon.DetailsWindow;
using EVEMon.SkillPlanner;

namespace EVEMon.CharacterMonitoring
{
    /// <summary>
    /// Displays a list of kill logs.
    /// </summary>
    internal sealed partial class CharacterKillLogList : UserControl, IListView
    {
        #region Fields


        #region ListBox Fields

        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix;

        // KillLog drawing - Region & text padding
        private const int PadTop = 2;
        private const int PadLeft = 6;
        private const int PadRight = 7;

        // KillLog drawing - Kill
        private const int KillDetailHeight = 34;
        private const string CopyKillInfoText = "Copy Kill Information to Clipboard";

        // KillLog drawing - KillLog groups
        private const int KillGroupHeaderHeight = 21;
        private const int CollapserPadRight = 4;

        private readonly Font m_killFont;
        private readonly Font m_killBoldFont;
        private readonly List<string> m_collapsedGroups = new List<string>();

        private int m_copyPositionFromRight;
        private Size m_copyKillInfoTextSize;

        #endregion


        #region ListView Fields

        private ColumnHeader m_sortCriteria;
        private KillLog m_selectedKillLog;

        private string m_textFilter = string.Empty;
        private bool m_sortAscending;

        #endregion


        #endregion


        #region Constructor

        public CharacterKillLogList()
        {
            InitializeComponent();

            lbKillLog.Hide();
            lvKillLog.Hide();

            ListViewHelper.EnableDoubleBuffer(lvKillLog);

            m_sortCriteria = lvKillLog.Columns[0];

            m_killFont = new Font(Font.FontFamily, 6.25F);
            m_killBoldFont = new Font(Font.FontFamily, 6.25F, FontStyle.Bold);
            noKillLogLabel.Font = new Font(noKillLogLabel.Font.FontFamily, 11.25F, FontStyle.Bold);

            lvKillLog.ColumnClick += lvKillLog_ColumnClick;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the character associated with this monitor.
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
                if (!DesignMode && !this.IsDesignModeHosted())
                    UpdateColumns();
            }
        }

        /// <summary>
        /// Gets or sets the grouping of a listview.
        /// </summary>
        /// <value></value>
        /// <remarks>Not used anywhere; Only to implement IListView</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Enum Grouping { get; set; }

        /// <summary>
        /// Gets or sets the settings used for columns.
        /// </summary>
        /// <remarks>Not used anywhere; Only to implement IListView</remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<IColumnSettings> Columns { get; set; }

        #endregion


        #region Inherited events

        /// <summary>
        /// On load subscribe the events.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || this.IsDesignModeHosted())
                return;

            EveMonClient.CharacterKillLogUpdated += EveMonClient_CharacterKillLogUpdated;
            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            EveMonClient.EveIDToNameUpdated += EveMonClient_EveIDToNameUpdated;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.CharacterKillLogUpdated -= EveMonClient_CharacterKillLogUpdated;
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            EveMonClient.EveIDToNameUpdated -= EveMonClient_EveIDToNameUpdated;
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

            lbKillLog.Visible = false;
            lvKillLog.Visible = false;
            UpdateKillLogView();
        }

        #endregion


        #region Content Management

        /// <summary>
        /// Updates the kill log view.
        /// </summary>
        internal void UpdateKillLogView()
        {
            if (Settings.UI.MainWindow.CombatLog.ShowCondensedLogs)
                UpdateColumns();
            else
                UpdateListContent();
        }


        #region ListBox Update Methods

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateListContent()
        {
            // Returns if not visible
            if (!Visible)
                return;

            // When no character, we just hide the list
            if (Character == null)
            {
                noKillLogLabel.Show();
                lbKillLog.Hide();
                lvKillLog.Hide();
                return;
            }

            int scrollBarPosition = lbKillLog.TopIndex;

            // Update the kills list
            lbKillLog.BeginUpdate();
            try
            {
                var kills = new List<KillLog>(Character.KillLog);
                kills.Sort();
                IEnumerable<IGrouping<KillGroup, KillLog>> groups = kills.GroupBy(x => x.Group).OrderBy(x => (int)x.Key);

                // Scroll through groups
                lbKillLog.Items.Clear();
                foreach (IGrouping<KillGroup, KillLog> group in groups)
                {
                    string groupHeaderText = $"{@group.Key} ({@group.Count()})";

                    lbKillLog.Items.Add(groupHeaderText);

                    // Add items in the group when it's not collapsed
                    if (m_collapsedGroups.Contains(groupHeaderText))
                        continue;

                    foreach (KillLog kill in group)
                    {
                        kill.KillLogVictimShipImageUpdated += kill_KillLogVictimShipImageUpdated;
                        kill.UpdateCharacterNames();
                        lbKillLog.Items.Add(kill);
                    }
                }

                // Display or hide the "no kills" label.
                noKillLogLabel.Visible = !kills.Any();
                lbKillLog.Visible = kills.Any();
                lvKillLog.Hide();

                // Invalidate display
                lbKillLog.Invalidate();
            }
            finally
            {
                lbKillLog.EndUpdate();
                lbKillLog.TopIndex = scrollBarPosition;
            }
        }

        #endregion


        #region ListView Update Methods

        /// <summary>
        /// Autoresizes the columns.
        /// </summary>
        public void AutoResizeColumns()
        {
            AdjustColumns();
        }

        /// <summary>
        /// Updates the columns.
        /// </summary>
        private void UpdateColumns()
        {
            // Returns if not visible
            if (!Visible)
                return;

            // We update the content
            UpdateListViewContent();
        }

        /// <summary>
        /// Updates the content of the listview.
        /// </summary>
        private void UpdateListViewContent()
        {
            // Returns if not visible
            if (!Visible)
                return;

            int scrollBarPosition = lvKillLog.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = lvKillLog.SelectedItems.Count > 0 ?
                lvKillLog.SelectedItems[0].Tag.GetHashCode() : 0;

            lvKillLog.BeginUpdate();
            try
            {
                IEnumerable<KillLog> killLog = Character.KillLog.Where(x => IsTextMatching(x, m_textFilter));

                UpdateSort();

                lvKillLog.Items.Clear();
                lvKillLog.Groups.Clear();

                foreach (IGrouping<KillGroup, KillLog> group in killLog.GroupBy(x => x.Group).OrderBy(x => x.Key))
                {
                    string groupText = $"{group.Key} ({group.Count()})";
                    ListViewGroup listGroup = new ListViewGroup(groupText);
                    lvKillLog.Groups.Add(listGroup);

                    // Add the items
                    lvKillLog.Items.AddRange(group.Select(kill => new
                    {
                        kill,
                        item = new ListViewItem(kill.KillTime.ToLocalTime().ToString(CultureConstants.DefaultCulture), listGroup)
                        {
                            UseItemStyleForSubItems = false,
                            Tag = kill
                        }
                    }).Select(x => CreateSubItems(x.kill, x.item)).ToArray());
                }

                // Restore the selected item (if any)
                if (selectedItem > 0)
                {
                    foreach (ListViewItem lvItem in lvKillLog.Items.Cast<ListViewItem>().Where(
                        lvItem => lvItem.Tag.GetHashCode() == selectedItem))
                    {
                        lvItem.Selected = true;
                    }
                }

                // Adjust the size of the columns
                AdjustColumns();

                // Display or hide the "no research points" label
                noKillLogLabel.Visible = lvKillLog.Items.Count == 0;
                lvKillLog.Visible = !noKillLogLabel.Visible;
                lbKillLog.Visible = false;
            }
            finally
            {
                lvKillLog.EndUpdate();
                lvKillLog.SetVerticalScrollBarPosition(scrollBarPosition);
            }
        }

        /// <summary>
        /// Creates the list view sub items.
        /// </summary>
        /// <param name="kill">The kill report.</param>
        /// <param name="item">The item.</param>
        private ListViewItem CreateSubItems(KillLog kill, ListViewItem item)
        {
            kill.UpdateCharacterNames();

            // Add enough subitems to match the number of columns
            while (item.SubItems.Count < lvKillLog.Columns.Count + 1)
            {
                item.SubItems.Add(string.Empty);
            }

            // Creates the subitems
            for (int i = 0; i < lvKillLog.Columns.Count; i++)
            {
                SetColumn(kill, item.SubItems[i], lvKillLog.Columns[i]);
            }

            return item;
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        private void AdjustColumns()
        {
            foreach (ColumnHeader column in lvKillLog.Columns)
            {
                column.Width = -2;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column
                if (column.Index != lvKillLog.Columns.Count - 1)
                    continue;

                const int Pad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + Pad * 2;

                // If there is an image assigned to the header, add its width with padding
                if (lvKillLog.SmallImageList != null && column.ImageIndex > -1)
                    columnHeaderWidth += lvKillLog.SmallImageList.ImageSize.Width + Pad;

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
            lvKillLog.ListViewItemSorter = new ListViewItemComparerByTag<KillLog>(
                new KillLogComparer(m_sortCriteria, m_sortAscending));

            UpdateSortVisualFeedback();
        }

        /// <summary>
        /// Updates the sort feedback (the arrow on the header).
        /// </summary>
        private void UpdateSortVisualFeedback()
        {
            foreach (ColumnHeader columnHeader in lvKillLog.Columns)
            {
                if (m_sortCriteria == columnHeader)
                    columnHeader.ImageIndex = m_sortAscending ? 0 : 1;
                else
                    columnHeader.ImageIndex = 2;
            }
        }

        /// <summary>
        /// Updates the listview sub-item.
        /// </summary>
        /// <param name="kill"></param>
        /// <param name="item"></param>
        /// <param name="column"></param>
        private static void SetColumn(KillLog kill, ListViewItem.ListViewSubItem item, ColumnHeader column)
        {
            switch (column.Index)
            {
                case 0:
                    item.Text = $"{kill.KillTime.ToLocalTime()}";
                    break;
                case 1:
                    item.Text = kill.Victim.ShipTypeName;
                    break;
                case 2:
                    item.Text = kill.Victim.Name;
                    break;
                case 3:
                    item.Text = kill.Victim.CorporationName;
                    break;
                case 4:
                    item.Text = kill.Victim.AllianceName;
                    break;
                case 5:
                    item.Text = kill.Victim.FactionName;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion


        #endregion


        #region Drawing

        /// <summary>
        /// Handles the DrawItem event of the lbKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbKillLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbKillLog.Items.Count)
                return;

            object item = lbKillLog.Items[e.Index];
            KillLog kill = item as KillLog;
            if (kill != null)
                DrawItem(kill, e);
            else
                DrawItem((string)item, e);
        }

        /// <summary>
        /// Handles the MeasureItem event of the lbKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void lbKillLog_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.ItemHeight = GetItemHeight(lbKillLog.Items[e.Index]);
        }

        /// <summary>
        /// Gets the item's height.
        /// </summary>
        /// <param name="item"></param>
        private int GetItemHeight(object item)
        {
            if (item is KillLog)
                return Math.Max(m_killFont.Height * 2 + PadTop * 2, KillDetailHeight);

            return KillGroupHeaderHeight;
        }

        /// <summary>
        /// Draws the list item for the given kill
        /// </summary>
        /// <param name="killLog"></param>
        /// <param name="e"></param>
        private void DrawItem(KillLog killLog, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            g.FillRectangle(e.Index % 2 == 0 ? Brushes.White : Brushes.LightGray, e.Bounds);

            // Draw text for a kill
            if (killLog.Group == KillGroup.Kills)
                DrawKillText(killLog, e);

            // Draw text for a loss
            if (killLog.Group == KillGroup.Losses)
                DrawLossText(killLog, e);

            // If 'Safe for work' draw 'copy' text
            if (Settings.UI.SafeForWork)
            {
                m_copyKillInfoTextSize = TextRenderer.MeasureText(g, CopyKillInfoText, m_killFont, Size.Empty, Format);
                m_copyPositionFromRight = m_copyKillInfoTextSize.Width + PadRight;
                TextRenderer.DrawText(g, CopyKillInfoText, m_killFont,
                    new Rectangle(e.Bounds.Right - m_copyPositionFromRight,
                        e.Bounds.Top + PadTop,
                        m_copyKillInfoTextSize.Width + PadLeft,
                        m_copyKillInfoTextSize.Height), Color.Black);

            }

            // Draw images
            if (Settings.UI.SafeForWork)
                return;

            // Draw the kill image
            g.DrawImage(killLog.VictimShipImage,
                new Rectangle(e.Bounds.Left + PadLeft / 2,
                    KillDetailHeight / 2 - killLog.VictimShipImage.Height / 2 + e.Bounds.Top,
                    killLog.VictimShipImage.Width, killLog.VictimShipImage.Height));

            // Draw the copy image
            m_copyPositionFromRight = 24;
            g.DrawImage(Resources.Copy, new Rectangle(e.Bounds.Right - m_copyPositionFromRight,
                e.Bounds.Top + PadTop,
                Resources.Copy.Width, Resources.Copy.Height));
        }

        /// <summary>
        /// Draws the text for a kill.
        /// </summary>
        /// <param name="killLog">The kill log.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void DrawKillText(KillLog killLog, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Texts
            string victimNameText = killLog.Victim.Name;
            string killTimeSinceText = killLog.TimeSinceKill
                .ToDescriptiveText(DescriptiveTextOptions.IncludeCommas |
                                   DescriptiveTextOptions.SpaceText |
                                   DescriptiveTextOptions.FullText);
            string killTimeText = $"({killTimeSinceText} ago)";
            string victimNameCorpAndAllianceName = GetText(killLog.Victim.CorporationName, killLog.Victim.AllianceName);
            string whatAndWhereInfo = $"{killLog.Victim.ShipTypeName}, " +
                                      $"{killLog.SolarSystem?.Name}, " +
                                      $"{killLog.SolarSystem?.Constellation?.Region?.Name}, " +
                                      $"{killLog.SolarSystem?.SecurityLevel:N1}";

            // Measure texts
            Size victimNameTextSize = TextRenderer.MeasureText(g, victimNameText, m_killBoldFont, Size.Empty, Format);
            Size killTimeTextSize = TextRenderer.MeasureText(g, killTimeText, m_killFont, Size.Empty, Format);
            Size victimNameCorpAndAllianceNameSize = TextRenderer.MeasureText(g, victimNameCorpAndAllianceName, m_killFont,
                Size.Empty, Format);
            Size whatAndWhereInfoSize = TextRenderer.MeasureText(g, whatAndWhereInfo, m_killFont, Size.Empty, Format);

            // Draw texts
            TextRenderer.DrawText(g, victimNameText, m_killBoldFont,
                new Rectangle(e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight,
                    e.Bounds.Top,
                    victimNameTextSize.Width + PadLeft,
                    victimNameTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, killTimeText, m_killFont,
                new Rectangle(
                    e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight * 3 + victimNameTextSize.Width,
                    e.Bounds.Top,
                    killTimeTextSize.Width + PadLeft,
                    killTimeTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, victimNameCorpAndAllianceName, m_killFont,
                new Rectangle(e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight,
                    e.Bounds.Top + victimNameTextSize.Height,
                    victimNameCorpAndAllianceNameSize.Width + PadLeft,
                    victimNameCorpAndAllianceNameSize.Height), Color.Black);

            TextRenderer.DrawText(g, whatAndWhereInfo, m_killFont,
                new Rectangle(e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight,
                    e.Bounds.Top + victimNameTextSize.Height +
                    victimNameCorpAndAllianceNameSize.Height,
                    whatAndWhereInfoSize.Width + PadLeft,
                    whatAndWhereInfoSize.Height), Color.Black);
        }

        /// <summary>
        /// Draws the text for a loss.
        /// </summary>
        /// <param name="killLog">The kill log.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void DrawLossText(KillLog killLog, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Texts
            string killTimeSinceText = killLog.TimeSinceKill
                .ToDescriptiveText(DescriptiveTextOptions.IncludeCommas |
                                   DescriptiveTextOptions.SpaceText |
                                   DescriptiveTextOptions.FullText);
            string killTimeText = $"({killTimeSinceText} ago)";
            string finalBlowAttackerCorpAndAllianceName = GetText(killLog.FinalBlowAttacker.CorporationName,
                killLog.FinalBlowAttacker.AllianceName);
            string finalBlowAttackerShipAndModuleName = GetText(killLog.FinalBlowAttacker.ShipTypeName,
                killLog.FinalBlowAttacker.WeaponTypeName);

            // Measure texts
            Size killShipNameTextSize = TextRenderer.MeasureText(g, killLog.Victim.ShipTypeName, m_killBoldFont, Size.Empty,
                Format);
            Size killTimeTextSize = TextRenderer.MeasureText(g, killTimeText, m_killFont, Size.Empty, Format);
            Size finalBlowAttackerCorpAndAllianceNameSize = TextRenderer.MeasureText(g, finalBlowAttackerCorpAndAllianceName,
                m_killFont, Size.Empty, Format);
            Size finalBlowAttackerShipAndModuleNameSize = TextRenderer.MeasureText(g, finalBlowAttackerShipAndModuleName,
                m_killFont, Size.Empty, Format);

            // Draw texts
            TextRenderer.DrawText(g, killLog.Victim.ShipTypeName, m_killBoldFont,
                new Rectangle(e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight,
                    e.Bounds.Top,
                    killShipNameTextSize.Width + PadLeft,
                    killShipNameTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, killTimeText, m_killFont,
                new Rectangle(
                    e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight * 3 + killShipNameTextSize.Width,
                    e.Bounds.Top,
                    killTimeTextSize.Width + PadLeft,
                    killTimeTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, finalBlowAttackerCorpAndAllianceName, m_killFont,
                new Rectangle(e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight,
                    e.Bounds.Top + killShipNameTextSize.Height,
                    finalBlowAttackerCorpAndAllianceNameSize.Width + PadLeft,
                    finalBlowAttackerCorpAndAllianceNameSize.Height), Color.Black);

            TextRenderer.DrawText(g, finalBlowAttackerShipAndModuleName, m_killFont,
                new Rectangle(e.Bounds.Left + killLog.VictimShipImage.Width + 4 + PadRight,
                    e.Bounds.Top + killShipNameTextSize.Height +
                    finalBlowAttackerCorpAndAllianceNameSize.Height,
                    finalBlowAttackerShipAndModuleNameSize.Width + PadLeft,
                    finalBlowAttackerShipAndModuleNameSize.Height), Color.Black);
        }

        /// <summary>
        /// Draws the list item for the given group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="e"></param>
        private void DrawItem(string group, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draws the background
            using (LinearGradientBrush lgb = new LinearGradientBrush(new PointF(0F, 0F), new PointF(0F, 21F),
                Color.FromArgb(75, 75, 75), Color.FromArgb(25, 25, 25)))
            {
                g.FillRectangle(lgb, e.Bounds);
            }

            using (Pen p = new Pen(Color.FromArgb(100, 100, 100)))
            {
                g.DrawLine(p, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right + 1, e.Bounds.Top);
            }

            // Measure texts
            Size standingGroupTextSize = TextRenderer.MeasureText(g, group.ToUpper(CultureConstants.DefaultCulture),
                m_killBoldFont, Size.Empty, Format);
            Rectangle standingGroupTextRect = new Rectangle(e.Bounds.Left + PadLeft,
                e.Bounds.Top +
                (e.Bounds.Height / 2 - standingGroupTextSize.Height / 2),
                standingGroupTextSize.Width + PadRight,
                standingGroupTextSize.Height);

            // Draws the text header
            TextRenderer.DrawText(g, group.ToUpper(CultureConstants.DefaultCulture), m_killBoldFont, standingGroupTextRect,
                Color.White, Color.Transparent, Format);

            // Draws the collapsing arrows
            bool isCollapsed = m_collapsedGroups.Contains(group);
            Image img = isCollapsed ? Resources.Expand : Resources.Collapse;

            g.DrawImageUnscaled(img, new Rectangle(e.Bounds.Right - img.Width - CollapserPadRight,
                KillGroupHeaderHeight / 2 - img.Height / 2 + e.Bounds.Top,
                img.Width, img.Height));
        }

        /// <summary>
        /// Gets the preferred size from the preferred size of the list.
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize) => lbKillLog.GetPreferredSize(proposedSize);

        #endregion


        #region Local events

        /// <summary>
        /// Handles the StandingImageUpdated event of the standing control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void kill_KillLogVictimShipImageUpdated(object sender, EventArgs e)
        {
            // Force to redraw
            lbKillLog.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the lbKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbKillLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowKillDetails();
        }

        /// <summary>
        /// Handles the MouseWheel event of the lbKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbKillLog_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 0)
                return;

            // Update the drawing based upon the mouse wheel scrolling
            int numberOfItemLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / Math.Abs(e.Delta);
            int lines = numberOfItemLinesToMove;
            if (lines == 0)
                return;

            // Compute the number of lines to move
            int direction = lines / Math.Abs(lines);
            int[] numberOfPixelsToMove = new int[lines * direction];
            for (int i = 1; i <= Math.Abs(lines); i++)
            {
                object item = null;

                // Going up
                if (direction == Math.Abs(direction))
                {
                    // Retrieve the next top item
                    if (lbKillLog.TopIndex - i >= 0)
                        item = lbKillLog.Items[lbKillLog.TopIndex - i];
                }
                // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = lbKillLog.TopIndex + i - 1; j < lbKillLog.Items.Count; j++)
                    {
                        height += GetItemHeight(lbKillLog.Items[j]);
                    }

                    // Retrieve the next bottom item
                    if (height > lbKillLog.ClientSize.Height)
                        item = lbKillLog.Items[lbKillLog.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = GetItemHeight(item) * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                lbKillLog.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the lbKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbKillLog_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbKillLog.IndexFromPoint(e.Location);
            if (index < 0 || index >= lbKillLog.Items.Count)
                return;

            Rectangle itemRect;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == lbKillLog.Items.Count - 1)
            {
                itemRect = lbKillLog.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    return;
            }

            object item = lbKillLog.Items[index];
            string killsGroup = item as string;

            if (killsGroup != null)
            {
                // Left or Middle button : expand/collapse
                if (e.Button != MouseButtons.Right)
                {
                    ToggleGroupExpandCollapse(killsGroup);
                    return;
                }

                // If right click on the button, still expand/collapse
                itemRect = lbKillLog.GetItemRectangle(lbKillLog.Items.IndexOf(item));
                Rectangle buttonRect = GetButtonRectangle(killsGroup, itemRect);
                if (!buttonRect.Contains(e.Location))
                    return;

                ToggleGroupExpandCollapse(killsGroup);
                return;
            }

            // Right click we display a context menu
            if (e.Button == MouseButtons.Right)
            {
                // Display the context menu
                contextMenuStrip.Show(lbKillLog, e.Location);
                return;
            }

            // Did the user clicked on the "copy kill info" image ?
            itemRect = lbKillLog.GetItemRectangle(index);
            Rectangle copyKillInfoRect = GetCopyKillInfoRect(itemRect);
            if (copyKillInfoRect.Contains(e.Location))
                KillLogExporter.CopyKillInfoToClipboard(m_selectedKillLog);
        }

        /// <summary>
        /// Handles the MouseMove event of the lbKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbKillLog_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lbKillLog.Items.Count; i++)
            {
                // Skip until we find an item
                Rectangle rect = lbKillLog.GetItemRectangle(i);
                if (!rect.Contains(e.Location))
                    continue;

                // Skip if we are over the "copy kill info" image 
                rect = GetCopyKillInfoRect(lbKillLog.GetItemRectangle(i));
                if (rect.Contains(e.Location))
                {
                    lbKillLog.Cursor = Cursors.Default;
                    DisplayTooltip();
                    return;
                }

                toolTip.Active = false;

                object item = lbKillLog.Items[i];
                m_selectedKillLog = item as KillLog;

                lbKillLog.Cursor = m_selectedKillLog != null ? CustomCursors.ContextMenu : Cursors.Default;

                return;
            }

            toolTip.Active = false;
            lbKillLog.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lvKillLog_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            lvKillLog.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When user moves over the list we display a cursor indicating there is a context menu available.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lvKillLog_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            m_selectedKillLog = lvKillLog.GetItemAt(e.X, e.Y)?.Tag as KillLog;

            lvKillLog.Cursor = m_selectedKillLog != null ? CustomCursors.ContextMenu : Cursors.Default;
        }

        /// <summary>
        /// When the user clicks a column header, we update the sorting.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvKillLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnHeader column = lvKillLog.Columns[e.Column];
            if (m_sortCriteria == column)
                m_sortAscending = !m_sortAscending;
            else
            {
                m_sortCriteria = column;
                m_sortAscending = true;
            }

            // Updates the item sorter
            UpdateSort();
        }

        /// <summary>
        /// Handles the DoubleClick event of the lvKillLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lvKillLog_DoubleClick(object sender, EventArgs e)
        {
            ShowKillDetails();
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = m_selectedKillLog == null;
        }

        /// <summary>
        /// Handles the Click event of the showDetailsMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void showDetailsMenuItem_Click(object sender, EventArgs e)
        {
            ShowKillDetails();
        }

        /// <summary>
        /// Handles the Click event of the showInShipBrowserMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void showInShipBrowserMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selectedKillLog == null)
                return;

            Ship ship = StaticItems.GetItemByID(m_selectedKillLog.Victim.ShipTypeID) as Ship;

            if (ship == null)
                return;

            PlanWindow.ShowPlanWindow(Character).ShowShipInBrowser(ship);
        }

        /// <summary>
        /// Handles the Click event of the copyKillInfoMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void copyKillInfoMenuItem_Click(object sender, EventArgs e)
        {
            KillLogExporter.CopyKillInfoToClipboard(m_selectedKillLog);
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
        private static bool IsTextMatching(KillLog x, string text) => string.IsNullOrEmpty(text) ||
               x.Victim.ShipTypeName.ToUpperInvariant().Contains(text, ignoreCase: true) ||
               x.Victim.Name.ToUpperInvariant().Contains(text, ignoreCase: true) ||
               x.Victim.CorporationName.ToUpperInvariant().Contains(text, ignoreCase: true) ||
               x.Victim.AllianceName.ToUpperInvariant().Contains(text, ignoreCase: true) ||
               x.Victim.FactionName.ToUpperInvariant().Contains(text, ignoreCase: true);

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="text1">The text1.</param>
        /// <param name="text2">The text2.</param>
        /// <returns></returns>
        private static string GetText(string text1, string text2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(text1);

            if (!text2.IsEmptyOrUnknown())
                sb.Append($" / {text2}");

            return sb.ToString();
        }

        /// <summary>
        /// Toggles the expansion or collapsing of a single group
        /// </summary>
        /// <param name="group">The group to expand or collapse.</param>
        private void ToggleGroupExpandCollapse(string group)
        {
            if (m_collapsedGroups.Contains(group))
            {
                m_collapsedGroups.Remove(group);
                UpdateListContent();
            }
            else
            {
                m_collapsedGroups.Add(group);
                UpdateListContent();
            }
        }

        /// <summary>
        /// Gets the rectangle for the collapse/expand button.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="itemRect">The item rect.</param>
        /// <returns></returns>
        private Rectangle GetButtonRectangle(string group, Rectangle itemRect)
        {
            // Checks whether this group is collapsed
            bool isCollapsed = m_collapsedGroups.Contains(group);

            // Get the image for this state
            Image btnImage = isCollapsed ? Resources.Expand : Resources.Collapse;

            // Compute the top left point
            Point btnPoint = new Point(itemRect.Right - btnImage.Width - CollapserPadRight,
                KillGroupHeaderHeight / 2 - btnImage.Height / 2 + itemRect.Top);

            return new Rectangle(btnPoint, btnImage.Size);
        }

        /// <summary>
        /// Shows the kill details.
        /// </summary>
        private void ShowKillDetails()
        {
            if (m_selectedKillLog == null)
                return;

            WindowsFactory.ShowByTag<KillReportWindow, KillLog>(m_selectedKillLog);
        }

        /// <summary>
        /// Displays the tooltip.
        /// </summary>
        private void DisplayTooltip()
        {
            if (toolTip.Active || Settings.UI.SafeForWork)
                return;

            toolTip.Active = false;
            toolTip.SetToolTip(lbKillLog, CopyKillInfoText);
            toolTip.Active = true;
        }

        /// <summary>
        /// Gets the rectangle of the "copy" icon for the listbox item at the given index.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Rectangle GetCopyKillInfoRect(Rectangle rect)
        {
            Bitmap icon = Resources.Copy;
            Size copyKillInfoSize = Settings.UI.SafeForWork
                ? m_copyKillInfoTextSize
                : icon.Size;
            Rectangle copyKillInfoRect = new Rectangle(rect.Right - m_copyPositionFromRight, rect.Top + PadTop,
                copyKillInfoSize.Width, copyKillInfoSize.Height);
            copyKillInfoRect.Inflate(2, 8);
            return copyKillInfoRect;
        }

        #endregion


        #region Global events

        /// <summary>
        /// When the character kill log update, we refresh the content.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CharacterKillLogUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != Character)
                return;

            UpdateKillLogView();
        }

        /// <summary>
        /// When the ID to name conversion updates, we refresh the content.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_EveIDToNameUpdated(object sender, EventArgs e)
        {
            UpdateKillLogView();
        }

        /// <summary>
        /// When the settings change we update the content.
        /// </summary>
        /// <remarks>In case 'SafeForWork' gets enabled.</remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            UpdateKillLogView();
        }

        #endregion
    }
}
