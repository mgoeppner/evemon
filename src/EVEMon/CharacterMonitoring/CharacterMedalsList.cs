using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.Properties;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterMedalsList : UserControl
    {
        #region Fields
        
        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix;

        // Medals drawing - Region & text padding
        private const int PadTop = 2;
        private const int PadLeft = 6;
        private const int PadRight = 7;

        // Medals drawing - Medals
        private const int MedalDetailHeight = 34;

        // Medals drawing - Medals groups
        private const int MedalGroupHeaderHeight = 21;
        private const int CollapserPadRight = 4;

        private readonly Font m_medalsFont;
        private readonly Font m_medalsBoldFont;
        private readonly List<string> m_collapsedGroups = new List<string>();
        private readonly Image m_medalImage = Resources.Medal32;
        
        private object m_lastTooltipItem;

        #endregion


        #region Constructor

        public CharacterMedalsList()
        {
            InitializeComponent();

            lbMedals.Visible = false;

            m_medalsFont = new Font(Font.FontFamily, 8.25F);
            m_medalsBoldFont = new Font(Font.FontFamily, 8.25F, FontStyle.Bold);
            noMedalsLabel.Font = new Font(noMedalsLabel.Font.FontFamily, 11.25F, FontStyle.Bold);
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the character associated with this monitor.
        /// </summary>
        internal CCPCharacter Character { get; set; }

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

            EveMonClient.CharacterMedalsUpdated += EveMonClient_CharacterMedalsUpdated;
            EveMonClient.CorporationMedalsUpdated += EveMonClient_CorporationMedalsUpdated;
            EveMonClient.EveIDToNameUpdated += EveMonClient_EveIDToNameUpdated;
            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.CharacterMedalsUpdated -= EveMonClient_CharacterMedalsUpdated;
            EveMonClient.CorporationMedalsUpdated -= EveMonClient_CorporationMedalsUpdated;
            EveMonClient.EveIDToNameUpdated -= EveMonClient_EveIDToNameUpdated;
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            Disposed -= OnDisposed;
        }

        /// <summary>
        /// When the control becomes visible again, we update the content.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
                UpdateContent();
        }

        #endregion


        #region Content Management

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateContent()
        {
            // Returns if not visible
            if (!Visible)
                return;

            // When no character, we just hide the list
            if (Character == null)
            {
                noMedalsLabel.Visible = true;
                lbMedals.Visible = false;
                return;
            }

            int scrollBarPosition = lbMedals.TopIndex;

            // Update the medals list
            lbMedals.BeginUpdate();
            try
            {
                // Get the medals rewarded and try assign missing title and description from corp medals
                // Also prevents multi rewarded medals from being iterated
                IList<Medal> medals = Character.CharacterMedals.Distinct(new MedalComparer())
                    .Where(medal => medal.TryAssignMissingTitleAndDescription()).ToList();

                IEnumerable<IGrouping<MedalGroup, Medal>> groups = medals.GroupBy(x => x.Group);

                // Scroll through groups
                lbMedals.Items.Clear();
                foreach (IGrouping<MedalGroup, Medal> group in groups)
                {
                    lbMedals.Items.Add(group.Key.GetDescription());

                    // Add items in the group when it's not collapsed
                    if (m_collapsedGroups.Contains(group.Key.GetDescription()))
                        continue;

                    // Prevents multi rewarded medals to be drawn
                    foreach (Medal medal in group)
                    {
                        lbMedals.Items.Add(medal);
                    }
                }

                // Display or hide the "no medals" label.
                noMedalsLabel.Visible = !medals.Any();
                lbMedals.Visible = medals.Any();

                // Invalidate display
                lbMedals.Invalidate();
            }
            finally
            {
                lbMedals.EndUpdate();
                lbMedals.TopIndex = scrollBarPosition;
            }
        }

        #endregion


        #region Drawing

        /// <summary>
        /// Handles the DrawItem event of the lbMedals control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbMedals_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbMedals.Items.Count)
                return;

            object item = lbMedals.Items[e.Index];
            Medal medal = item as Medal;
            if (medal != null)
                DrawItem(medal, e);
            else
                DrawItem((string)item, e);
        }

        /// <summary>
        /// Handles the MeasureItem event of the lbMedals control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void lbMedals_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.ItemHeight = GetItemHeight(lbMedals.Items[e.Index]);
        }

        /// <summary>
        /// Gets the item's height.
        /// </summary>
        /// <param name="item"></param>
        private int GetItemHeight(object item)
        {
            if (item is Medal)
                return Math.Max(m_medalsFont.Height * 2 + PadTop * 2, MedalDetailHeight);

            return MedalGroupHeaderHeight;
        }

        /// <summary>
        /// Draws the list item for the given medal
        /// </summary>
        /// <param name="medal"></param>
        /// <param name="e"></param>
        private void DrawItem(Medal medal, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            g.FillRectangle(e.Index % 2 == 0 ? Brushes.White : Brushes.LightGray, e.Bounds);

            // Texts
            string medalTitleText = medal.Title;
            string medalDescriptionText = medal.Description;
            string medalStatusText = medal.Status.ToTitleCase();
            string medalTimesAwardedText = $"Number of times awarded: {medal.TimesAwarded:N0}";

            // Measure texts
            Size medalTitleTextSize = TextRenderer.MeasureText(g, medalTitleText, m_medalsBoldFont, Size.Empty, Format);
            Size medalDescriptionTextSize = TextRenderer.MeasureText(g, medalDescriptionText, m_medalsFont, Size.Empty, Format);
            Size medalStatusTextSize = TextRenderer.MeasureText(g, medalStatusText, m_medalsBoldFont, Size.Empty, Format);
            Size medalTimesAwardedTextSize = TextRenderer.MeasureText(g, medalTimesAwardedText, m_medalsFont, Size.Empty, Format);

            // Draw texts
            TextRenderer.DrawText(g, medalTitleText, m_medalsBoldFont,
                                  new Rectangle(e.Bounds.Left + m_medalImage.Width + 4 + PadRight,
                                                e.Bounds.Top + PadTop,
                                                medalTitleTextSize.Width + PadLeft,
                                                medalTitleTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, medalTimesAwardedText, m_medalsFont,
                                  new Rectangle(e.Bounds.Left + m_medalImage.Width + 4 + PadRight * 3 + medalTitleTextSize.Width,
                                                e.Bounds.Top + PadTop,
                                                medalTimesAwardedTextSize.Width + PadLeft,
                                                medalTimesAwardedTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, medalStatusText, m_medalsBoldFont,
                                  new Rectangle(e.Bounds.Right - PadRight - medalStatusTextSize.Width,
                                                e.Bounds.Top + PadTop,
                                                medalStatusTextSize.Width + PadLeft,
                                                medalStatusTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, medalDescriptionText, m_medalsFont,
                                  new Rectangle(e.Bounds.Left + m_medalImage.Width + 4 + PadRight,
                                                e.Bounds.Top + PadTop + medalTitleTextSize.Height,
                                                medalDescriptionTextSize.Width + PadLeft,
                                                medalDescriptionTextSize.Height), Color.Black);

            // Draw images
            if (Settings.UI.SafeForWork)
                return;

            // Draw the medal image
            g.DrawImage(m_medalImage, new Rectangle(e.Bounds.Left + PadLeft / 2,
                                             MedalDetailHeight / 2 - m_medalImage.Height / 2 + e.Bounds.Top,
                                             m_medalImage.Width, m_medalImage.Height));
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
                                                                  m_medalsBoldFont, Size.Empty, Format);
            Rectangle standingGroupTextRect = new Rectangle(e.Bounds.Left + PadLeft,
                                                            e.Bounds.Top +
                                                            (e.Bounds.Height / 2 - standingGroupTextSize.Height / 2),
                                                            standingGroupTextSize.Width + PadRight,
                                                            standingGroupTextSize.Height);

            // Draws the text header
            TextRenderer.DrawText(g, group.ToUpper(CultureConstants.DefaultCulture), m_medalsBoldFont, standingGroupTextRect,
                                  Color.White, Color.Transparent, Format);

            // Draws the collapsing arrows
            bool isCollapsed = m_collapsedGroups.Contains(group);
            Image img = isCollapsed ? Resources.Expand : Resources.Collapse;

            g.DrawImageUnscaled(img, new Rectangle(e.Bounds.Right - img.Width - CollapserPadRight,
                                                   MedalGroupHeaderHeight / 2 - img.Height / 2 + e.Bounds.Top,
                                                   img.Width, img.Height));
        }

        /// <summary>
        /// Gets the preferred size from the preferred size of the list.
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize) => lbMedals.GetPreferredSize(proposedSize);

        #endregion


        #region Local events

        /// <summary>
        /// Handles the MouseWheel event of the lbMedals control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbMedals_MouseWheel(object sender, MouseEventArgs e)
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
                    if (lbMedals.TopIndex - i >= 0)
                        item = lbMedals.Items[lbMedals.TopIndex - i];
                }
                // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = lbMedals.TopIndex + i - 1; j < lbMedals.Items.Count; j++)
                    {
                        height += GetItemHeight(lbMedals.Items[j]);
                    }

                    // Retrieve the next bottom item
                    if (height > lbMedals.ClientSize.Height)
                        item = lbMedals.Items[lbMedals.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = GetItemHeight(item) * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                lbMedals.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the lbMedals control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbMedals_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbMedals.IndexFromPoint(e.Location);
            if (index < 0 || index >= lbMedals.Items.Count)
                return;

            Rectangle itemRect;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == lbMedals.Items.Count - 1)
            {
                itemRect = lbMedals.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    return;
            }

            // For a medals group, we have to handle the collapse/expand mechanism
            object item = lbMedals.Items[index];
            string standingsGroup = item as string;
            if (standingsGroup == null)
                return;

            // Left button : expand/collapse
            if (e.Button != MouseButtons.Right)
            {
                ToggleGroupExpandCollapse(standingsGroup);
                return;
            }

            // If right click on the button, still expand/collapse
            itemRect = lbMedals.GetItemRectangle(lbMedals.Items.IndexOf(item));
            Rectangle buttonRect = GetButtonRectangle(standingsGroup, itemRect);
            if (buttonRect.Contains(e.Location))
                ToggleGroupExpandCollapse(standingsGroup);
        }

        /// <summary>
        /// On mouse move, we show the tooltip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMedals_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lbMedals.Items.Count; i++)
            {
                // Skip until we found the mouse location
                Rectangle rect = lbMedals.GetItemRectangle(i);
                if (!rect.Contains(e.Location))
                    continue;

                Medal item = lbMedals.Items[i] as Medal;

                // Updates the tooltip
                if (item == null)
                    continue;

                DisplayTooltip(item);
                return;
            }

            // If we went so far, we're not over anything
            m_lastTooltipItem = null;
            ttToolTip.Active = false;
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Displays the tooltip for the given item (medal).
        /// </summary>
        /// <param name="item"></param>
        private void DisplayTooltip(Medal item)
        {
            if (ttToolTip.Active && m_lastTooltipItem != null && m_lastTooltipItem == item)
                return;

            m_lastTooltipItem = item;

            ttToolTip.Active = false;
            ttToolTip.SetToolTip(lbMedals, GetTooltipText(item));
            ttToolTip.Active = true;
        }

        /// <summary>
        /// Gets the tooltip text.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private static string GetTooltipText(Medal item)
        {
            StringBuilder toolTip = new StringBuilder();
            toolTip
                .Append($"Issuer: {item.Issuer}")
                .AppendLine()
                .Append($"Issued: {item.Issued.ToLocalTime()}")
                .AppendLine();

            if (item.Group == MedalGroup.OtherCorporation)
            {
                toolTip
                    .Append($"Corporation: {item.CorporationName}")
                    .AppendLine();
            }

            toolTip.Append($"Reason: {item.Reason.WordWrap(50)}");

            return toolTip.ToString();
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
                UpdateContent();
            }
            else
            {
                m_collapsedGroups.Add(group);
                UpdateContent();
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
                                       MedalGroupHeaderHeight / 2 - btnImage.Height / 2 + itemRect.Top);

            return new Rectangle(btnPoint, btnImage.Size);
        }

        #endregion


        #region Global events

        /// <summary>
        /// When the character medals update, we refresh the content.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CharacterMedalsUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != Character)
                return;

            UpdateContent();
        }

        /// <summary>
        /// When the corporation medals update, we refresh the content.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CorporationMedalsUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != Character)
                return;

            UpdateContent();
        }

        /// <summary>
        /// When the EveIDToName list updates, we refresh the content.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_EveIDToNameUpdated(object sender, EventArgs e)
        {
            UpdateContent();
        }

        /// <summary>
        /// When the settings change we update the content.
        /// </summary>
        /// <remarks>In case 'SafeForWork' gets enabled.</remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            UpdateContent();
        }

        #endregion
    }
}
