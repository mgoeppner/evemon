using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Properties;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterStandingsList : UserControl
    {
        #region Fields

        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix;

        // Standings drawing - Region & text padding
        private const int PadTop = 2;
        private const int PadLeft = 6;
        private const int PadRight = 7;

        // Standings drawing - Standings
        private const int StandingDetailHeight = 34;

        // Standings drawing - Standings groups
        private const int StandingGroupHeaderHeight = 21;
        private const int CollapserPadRight = 4;

        private readonly Font m_standingsFont;
        private readonly Font m_standingsBoldFont;
        private readonly List<string> m_collapsedGroups = new List<string>();

        #endregion


        #region Constructor

        public CharacterStandingsList()
        {
            InitializeComponent();

            lbStandings.Visible = false;

            m_standingsFont = new Font(Font.FontFamily, 8.25F);
            m_standingsBoldFont = new Font(Font.FontFamily, 8.25F, FontStyle.Bold);
            noStandingsLabel.Font = new Font(noStandingsLabel.Font.FontFamily, 11.25F, FontStyle.Bold);
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

            EveMonClient.CharacterStandingsUpdated += EveMonClient_CharacterStandingsUpdated;
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
            EveMonClient.CharacterStandingsUpdated -= EveMonClient_CharacterStandingsUpdated;
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
                noStandingsLabel.Visible = true;
                lbStandings.Visible = false;
                return;
            }

            int scrollBarPosition = lbStandings.TopIndex;

            // Update the standings list
            lbStandings.BeginUpdate();
            try
            {
                IEnumerable<Standing> standings = Character.Standings;
                IEnumerable<IGrouping<StandingGroup, Standing>> groups = standings.GroupBy(
                    x => x.Group).Reverse();

                // Scroll through groups
                lbStandings.Items.Clear();
                foreach (IGrouping<StandingGroup, Standing> group in groups)
                {
                    lbStandings.Items.Add(group.Key.GetDescription());

                    // Add items in the group when it's not collapsed
                    if (m_collapsedGroups.Contains(group.Key.GetDescription()))
                        continue;

                    foreach (Standing standing in group.OrderByDescending(x => x.EffectiveStanding))
                    {
                        standing.StandingImageUpdated += standing_StandingImageUpdated;
                        lbStandings.Items.Add(standing);
                    }
                }

                // Display or hide the "no standings" label.
                noStandingsLabel.Visible = !standings.Any();
                lbStandings.Visible = standings.Any();

                // Invalidate display
                lbStandings.Invalidate();
            }
            finally
            {
                lbStandings.EndUpdate();
                lbStandings.TopIndex = scrollBarPosition;
            }
        }

        #endregion


        #region Drawing

        /// <summary>
        /// Handles the DrawItem event of the lbStandings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbStandings_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbStandings.Items.Count)
                return;

            object item = lbStandings.Items[e.Index];
            Standing standing = item as Standing;
            if (standing != null)
                DrawItem(standing, e);
            else
                DrawItem((string)item, e);
        }

        /// <summary>
        /// Handles the MeasureItem event of the lbStandings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void lbStandings_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.ItemHeight = GetItemHeight(lbStandings.Items[e.Index]);
        }

        /// <summary>
        /// Gets the item's height.
        /// </summary>
        /// <param name="item"></param>
        private int GetItemHeight(object item)
        {
            if (item is Standing)
                return Math.Max(m_standingsFont.Height * 2 + PadTop * 2, StandingDetailHeight);

            return StandingGroupHeaderHeight;
        }

        /// <summary>
        /// Draws the list item for the given standing
        /// </summary>
        /// <param name="standing"></param>
        /// <param name="e"></param>
        private void DrawItem(Standing standing, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            g.FillRectangle(e.Index % 2 == 0 ? Brushes.White : Brushes.LightGray, e.Bounds);

            Skill diplomacySkill = Character.Skills[DBConstants.DiplomacySkillID];
            Skill connectionsSkill = Character.Skills[DBConstants.ConnectionsSkillID];
            var diplomacySkillLevel = new SkillLevel(diplomacySkill, diplomacySkill.
                LastConfirmedLvl);
            var connectionsSkillLevel = new SkillLevel(connectionsSkill, connectionsSkill.
                LastConfirmedLvl);

            // Texts
            string standingText = $"{standing.EntityName}  {standing.EffectiveStanding:N2}";
            string standingStatusText = $"({Standing.Status(standing.EffectiveStanding)})";
            string standingsDetailsText = $"{(standing.StandingValue < 0 ? diplomacySkillLevel : connectionsSkillLevel)} " +
                                          $"raises your effective standing from {standing.StandingValue:N2}";

            // Measure texts
            Size standingTextSize = TextRenderer.MeasureText(g, standingText, m_standingsBoldFont, Size.Empty, Format);
            Size standingStatusTextSize = TextRenderer.MeasureText(g, standingStatusText, m_standingsBoldFont, Size.Empty, Format);
            Size standingsDetailsTextSize = TextRenderer.MeasureText(g, standingsDetailsText, m_standingsFont, Size.Empty, Format);

            bool standingsDiffer = Math.Abs(standing.EffectiveStanding - standing.StandingValue) > double.Epsilon;

            // Draw texts
            TextRenderer.DrawText(g, standingText, m_standingsBoldFont,
                                  new Rectangle(
                                      e.Bounds.Left + standing.EntityImage.Width + 4,
                                      e.Bounds.Top + (standingsDiffer
                                                          ? PadTop
                                                          : (e.Bounds.Height - standingTextSize.Height) / 2),
                                      standingTextSize.Width + PadLeft,
                                      standingTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, standingStatusText, m_standingsBoldFont,
                                  new Rectangle(
                                      e.Bounds.Left + standing.EntityImage.Width + 4 + standingTextSize.Width + PadRight,
                                      e.Bounds.Top + (standingsDiffer
                                                          ? PadTop
                                                          : (e.Bounds.Height - standingStatusTextSize.Height) / 2),
                                      standingStatusTextSize.Width + PadLeft,
                                      standingStatusTextSize.Height), GetStatusColor(Standing.Status(standing.EffectiveStanding)));

            if (standingsDiffer)
            {
                TextRenderer.DrawText(g, standingsDetailsText, m_standingsFont,
                                      new Rectangle(
                                          e.Bounds.Left + standing.EntityImage.Width + 4,
                                          e.Bounds.Top + PadTop + standingTextSize.Height,
                                          standingsDetailsTextSize.Width + PadLeft,
                                          standingsDetailsTextSize.Height), Color.Black);
            }

            // Draw the entity image
            if (Settings.UI.SafeForWork)
                return;

            g.DrawImage(standing.EntityImage,
                        new Rectangle(e.Bounds.Left + PadLeft / 2,
                                      StandingDetailHeight / 2 - standing.EntityImage.Height / 2 + e.Bounds.Top,
                                      standing.EntityImage.Width, standing.EntityImage.Height));
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
                                                                  m_standingsBoldFont, Size.Empty, Format);
            Rectangle standingGroupTextRect = new Rectangle(e.Bounds.Left + PadLeft,
                                                            e.Bounds.Top +
                                                            (e.Bounds.Height / 2 - standingGroupTextSize.Height / 2),
                                                            standingGroupTextSize.Width + PadRight,
                                                            standingGroupTextSize.Height);

            // Draws the text header
            TextRenderer.DrawText(g, group.ToUpper(CultureConstants.DefaultCulture), m_standingsBoldFont, standingGroupTextRect,
                                  Color.White, Color.Transparent, Format);

            // Draws the collapsing arrows
            bool isCollapsed = m_collapsedGroups.Contains(group);
            Image img = isCollapsed ? Resources.Expand : Resources.Collapse;

            g.DrawImageUnscaled(img, new Rectangle(e.Bounds.Right - img.Width - CollapserPadRight,
                                                   StandingGroupHeaderHeight / 2 - img.Height / 2 + e.Bounds.Top,
                                                   img.Width, img.Height));
        }

        /// <summary>
        /// Gets the preferred size from the preferred size of the list.
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize) => lbStandings.GetPreferredSize(proposedSize);

        #endregion


        #region Local events

        /// <summary>
        /// Handles the StandingImageUpdated event of the standing control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void standing_StandingImageUpdated(object sender, EventArgs e)
        {
            // Force to redraw
            lbStandings.Invalidate();
        }

        /// <summary>
        /// Handles the MouseWheel event of the lbStandings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbStandings_MouseWheel(object sender, MouseEventArgs e)
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
                    if (lbStandings.TopIndex - i >= 0)
                        item = lbStandings.Items[lbStandings.TopIndex - i];
                }
                    // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = lbStandings.TopIndex + i - 1; j < lbStandings.Items.Count; j++)
                    {
                        height += GetItemHeight(lbStandings.Items[j]);
                    }

                    // Retrieve the next bottom item
                    if (height > lbStandings.ClientSize.Height)
                        item = lbStandings.Items[lbStandings.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = GetItemHeight(item) * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                lbStandings.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the lbStandings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbStandings_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbStandings.IndexFromPoint(e.Location);
            if (index < 0 || index >= lbStandings.Items.Count)
                return;

            Rectangle itemRect;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == lbStandings.Items.Count - 1)
            {
                itemRect = lbStandings.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    return;
            }

            // For a standings group, we have to handle the collapse/expand mechanism
            object item = lbStandings.Items[index];
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
            itemRect = lbStandings.GetItemRectangle(lbStandings.Items.IndexOf(item));
            Rectangle buttonRect = GetButtonRectangle(standingsGroup, itemRect);
            if (buttonRect.Contains(e.Location))
                ToggleGroupExpandCollapse(standingsGroup);
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Gets the color of a standing status.
        /// </summary>
        /// <param name="status">The standing status.</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <returns></returns>
        private static Color GetStatusColor(StandingStatus status)
        {
            if (Settings.UI.SafeForWork)
                return Color.Black;

            switch (status)
            {
                case StandingStatus.Neutral:
                    return Color.FromArgb(255, 178, 178, 178);
                case StandingStatus.Terrible:
                    return Color.FromArgb(255, 191, 0, 0);
                case StandingStatus.Bad:
                    return Color.FromArgb(255, 255, 89, 0);
                case StandingStatus.Good:
                    return Color.FromArgb(255, 51, 127, 255);
                case StandingStatus.Excellent:
                    return Color.FromArgb(255, 0, 38, 153);
                default:
                    throw new NotImplementedException();
            }
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
                                       StandingGroupHeaderHeight / 2 - btnImage.Height / 2 + itemRect.Top);

            return new Rectangle(btnPoint, btnImage.Size);
        }

        #endregion


        #region Global events

        /// <summary>
        /// When the character standings update, we refresh the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterStandingsUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != Character)
                return;

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

        /// <summary>
        /// When the EVE ID to name changes we update the content.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_EveIDToNameUpdated(object sender, EventArgs e)
        {
            UpdateContent();
        }

        #endregion
    }
}
