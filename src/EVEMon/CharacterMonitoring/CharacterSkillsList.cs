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
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Properties;
using EVEMon.SkillPlanner;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterSkillsList : UserControl
    {
        #region Fields

        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
        private const byte SkillsSummaryTextWidth = 75;
        private const byte SkillGroupTotalSPTextWidth = 100;

        // Skills drawing - Region & text padding
        private const byte PadTop = 2;
        private const byte PadLeft = 6;
        private const byte PadRight = 7;

        // Skills drawing - Boxes
        private const byte BoxWidth = 57;
        private const byte BoxHeight = 14;
        private const byte LowerBoxHeight = 8;
        private const byte BoxHPad = 6;
        private const byte BoxVPad = 2;
        private const byte SkillDetailHeight = 31;

        // Skills drawing - Skills groups
        private const byte SkillHeaderHeight = 21;
        private const byte CollapserPadRight = 4;

        // Skills drawing - Font & brushes
        private readonly Font m_skillsFont;
        private readonly Font m_boldSkillsFont;

        private object m_lastTooltipItem;
        private BlinkAction m_blinkAction;

        private int m_maxGroupNameWidth;
        private Skill m_selectedSkill;

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CharacterSkillsList()
        {
            InitializeComponent();

            lbSkills.Visible = false;

            m_skillsFont = new Font(Font.FontFamily, 8.25F);
            m_boldSkillsFont = new Font(Font.FontFamily, 8.25F, FontStyle.Bold);
            noSkillsLabel.Font = new Font(noSkillsLabel.Font.FontFamily, 11.25F, FontStyle.Bold);
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the character associated with this monitor.
        /// </summary>
        internal Character Character { get; set; }

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

            EveMonClient.CharacterUpdated += EveMonClient_CharacterUpdated;
            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            EveMonClient.TimerTick += EveMonClient_TimerTick;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.CharacterUpdated -= EveMonClient_CharacterUpdated;
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            EveMonClient.TimerTick -= EveMonClient_TimerTick;
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
        /// Updates all the content.
        /// </summary>
        /// <remarks>
        /// Another high-complexity method for us to look at.
        /// </remarks>
        private void UpdateContent()
        {
            // Returns if not visible
            if (!Visible)
                return;

            // When no character, we just hide the list
            if (Character == null)
            {
                noSkillsLabel.Visible = true;
                lbSkills.Visible = false;
                return;
            }

            int scrollBarPosition = lbSkills.TopIndex;

            // Update the skills list
            lbSkills.BeginUpdate();
            try
            {
                IList<Skill> skills = GetCharacterSkills().ToList();
                IOrderedEnumerable<IGrouping<SkillGroup, Skill>> groups =
                    skills.GroupBy(x => x.Group).OrderBy(x => x.Key.Name);

                m_maxGroupNameWidth = groups.Select(
                    group => TextRenderer.MeasureText(@group.Key.Name, m_boldSkillsFont, Size.Empty, Format)).Select(
                        groupNameSize => groupNameSize.Width).Concat(new[] { 0 }).Max();

                // Scroll through groups
                lbSkills.Items.Clear();
                foreach (IGrouping<SkillGroup, Skill> group in groups)
                {
                    lbSkills.Items.Add(group.Key);

                    // Add items in the group when it's not collapsed
                    if (Character.UISettings.CollapsedGroups.Contains(group.Key.Name))
                        continue;

                    foreach (Skill skill in group.OrderBy(x => x.Name))
                    {
                        lbSkills.Items.Add(skill);
                    }
                }

                // Display or hide the "no skills" label.
                noSkillsLabel.Visible = !skills.Any();
                lbSkills.Visible = skills.Any();

                // Invalidate display
                lbSkills.Invalidate();
            }
            finally
            {
                lbSkills.EndUpdate();
                lbSkills.TopIndex = scrollBarPosition;
            }
        }

        /// <summary>
        /// Gets a characters skills, filtered by MainWindow settings
        /// </summary>
        /// <returns>
        /// IEnumerable of <see cref="Skill"/> Skill.
        /// </returns>
        private IEnumerable<Skill> GetCharacterSkills()
        {
            if (Settings.UI.MainWindow.ShowPrereqMetSkills)
                return Character.Skills.Where(x => x.IsKnown || (x.ArePrerequisitesMet && x.IsPublic));

            if (Settings.UI.MainWindow.ShowNonPublicSkills)
                return Character.Skills;

            return Settings.UI.MainWindow.ShowAllPublicSkills
                       ? Character.Skills.Where(x => x.IsKnown || x.IsPublic)
                       : Character.Skills.Where(x => x.IsKnown);
        }

        #endregion


        #region Drawing

        /// <summary>
        /// Handles the DrawItem event of the lbSkills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbSkills_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbSkills.Items.Count)
                return;

            object item = lbSkills.Items[e.Index];
            SkillGroup skillGroup = item as SkillGroup;
            if (skillGroup != null)
                DrawItem(skillGroup, e);
            else
                DrawItem((Skill)item, e);
        }

        /// <summary>
        /// Handles the MeasureItem event of the lbSkills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void lbSkills_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.ItemHeight = GetItemHeight(lbSkills.Items[e.Index]);
        }

        /// <summary>
        /// Gets the item's height.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private int GetItemHeight(object item)
        {
            if (item is SkillGroup)
                return SkillHeaderHeight;

            return Math.Max(m_skillsFont.Height * 2 + PadTop * 2, SkillDetailHeight);
        }

        /// <summary>
        /// Draws the list item for the given skill
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="e"></param>
        private void DrawItem(Skill skill, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            DrawBackground(skill, e);

            // Measure texts
            long skillPointsToNextLevel = skill.StaticData.GetPointsRequiredForLevel(Math.Min(skill.Level + 1, 5));

            string rankText = $" (Rank {skill.Rank})";
            string spText = $"SP: {skill.SkillPoints:N0}/{skillPointsToNextLevel:N0}";
            string levelText = $"Level {skill.Level}";
            string pctText = $"{Math.Floor(skill.PercentCompleted)}% Done";

            Size skillNameSize = TextRenderer.MeasureText(g, skill.Name, m_boldSkillsFont, Size.Empty, Format);
            Size rankTextSize = TextRenderer.MeasureText(g, rankText, m_skillsFont, Size.Empty, Format);
            Size levelTextSize = TextRenderer.MeasureText(g, levelText, m_skillsFont, Size.Empty, Format);
            Size spTextSize = TextRenderer.MeasureText(g, spText, m_skillsFont, Size.Empty, Format);
            Size pctTextSize = TextRenderer.MeasureText(g, pctText, m_skillsFont, Size.Empty, Format);


            // Draw texts
            Color highlightColor = Color.Black;
            if (!skill.IsKnown)
                highlightColor = Color.Red;

            if (!skill.IsPublic)
                highlightColor = Color.DarkRed;

            if (skill.ArePrerequisitesMet && skill.IsPublic && !skill.IsKnown)
                highlightColor = Color.SlateGray;

            if (Settings.UI.MainWindow.HighlightPartialSkills && skill.IsPartiallyTrained && !skill.IsTraining)
                highlightColor = Color.Green;

            if (Settings.UI.MainWindow.HighlightQueuedSkills && skill.IsQueued && !skill.IsTraining)
                highlightColor = Color.RoyalBlue;

            TextRenderer.DrawText(g, skill.Name, m_boldSkillsFont,
                                  new Rectangle(
                                      e.Bounds.Left + PadLeft,
                                      e.Bounds.Top + PadTop,
                                      skillNameSize.Width + PadLeft,
                                      skillNameSize.Height), highlightColor);

            TextRenderer.DrawText(g, rankText, m_skillsFont,
                                  new Rectangle(
                                      e.Bounds.Left + PadLeft + skillNameSize.Width,
                                      e.Bounds.Top + PadTop,
                                      rankTextSize.Width + PadLeft,
                                      rankTextSize.Height), highlightColor);

            TextRenderer.DrawText(g, spText, m_skillsFont,
                                  new Rectangle(
                                      e.Bounds.Left + PadLeft,
                                      e.Bounds.Top + PadTop + skillNameSize.Height,
                                      spTextSize.Width + PadLeft,
                                      spTextSize.Height), highlightColor);

            // Boxes
            DrawBoxes(skill, e);

            // Draw progression bar
            DrawProgressionBar(skill, e);


            // Draw level and percent texts
            TextRenderer.DrawText(g, levelText, m_skillsFont,
                                  new Rectangle(
                                      e.Bounds.Right - BoxWidth - PadRight - BoxHPad - levelTextSize.Width,
                                      e.Bounds.Top + PadTop, levelTextSize.Width + PadRight,
                                      levelTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, pctText, m_skillsFont,
                                  new Rectangle(
                                      e.Bounds.Right - BoxWidth - PadRight - BoxHPad - pctTextSize.Width,
                                      e.Bounds.Top + PadTop + levelTextSize.Height,
                                      pctTextSize.Width + PadRight, pctTextSize.Height), Color.Black);
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private static void DrawBackground(Skill skill, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            if (skill.IsTraining)
            {
                // In training
                g.FillRectangle(Brushes.LightSteelBlue, e.Bounds);
            }
            else if (e.Index % 2 == 0)
            {
                // Not in training - odd
                g.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                // Not in training - even
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }
        }

        /// <summary>
        /// Draws the progression bar.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private static void DrawProgressionBar(Skill skill, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawRectangle(Pens.Black, new Rectangle(e.Bounds.Right - BoxWidth - PadRight,
                                                      e.Bounds.Top + PadTop + BoxHeight + BoxVPad,
                                                      BoxWidth, LowerBoxHeight));

            Rectangle pctBarRect = new Rectangle(e.Bounds.Right - BoxWidth - PadRight + 2,
                                                 e.Bounds.Top + PadTop + BoxHeight + BoxVPad + 2,
                                                 BoxWidth - 3, LowerBoxHeight - 3);

            g.FillRectangle(Brushes.DarkGray, pctBarRect);
            int fillWidth = (int)(pctBarRect.Width * skill.FractionCompleted);
            if (fillWidth <= 0)
                return;

            Rectangle fillRect = new Rectangle(pctBarRect.X, pctBarRect.Y, fillWidth, pctBarRect.Height);
            g.FillRectangle(Brushes.Black, fillRect);
        }

        /// <summary>
        /// Draws the boxes.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void DrawBoxes(Skill skill, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            bool isAlpha = skill.Character.EffectiveCharacterStatus.IsAlpha();

            g.DrawRectangle(Pens.Black, new Rectangle(e.Bounds.Right - BoxWidth - PadRight,
                e.Bounds.Top + PadTop, BoxWidth, BoxHeight));

            const int levelBoxWidth = (BoxWidth - 4 - 3) / 5, levelBoxHeight = BoxHeight - 3;
            int square = Math.Min(levelBoxWidth, levelBoxHeight);
            for (int level = 1; level <= 5; level++)
            {
                Brush fillBrush = Brushes.DarkGray;
                int alphaPad = 0, w, h;
                if (isAlpha && level > skill.StaticData.AlphaLimit)
                {
                    // Draw smaller gold square to indicate this level cannot be trained
                    fillBrush = Brushes.DarkGoldenrod;
                    alphaPad = 2;
                }
                if (level <= skill.Level)
                {
                    // Trained
                    if (alphaPad > 0)
                        // And inactive
                        alphaPad = 0;
                    else
                        fillBrush = Brushes.Black;
                }

                // If the box is denoting an untrainable level, make it square
                if (alphaPad > 0)
                    w = h = square - 2 * alphaPad;
                else
                {
                    w = levelBoxWidth;
                    h = levelBoxHeight;
                }

                // Determine proper spacing to center it in the frame
                var brect = new Rectangle(e.Bounds.Right - BoxWidth - PadRight + 2 +
                    (levelBoxWidth + 1) * (level - 1) + (levelBoxWidth - w) / 2,
                    e.Bounds.Top + PadTop + 2 + (levelBoxHeight - h) / 2, w, h);

                // Color indicator for a queued level or one which is training now
                var ccpCharacter = Character as CCPCharacter;
                if (ccpCharacter != null)
                {
                    var skillQueue = ccpCharacter.SkillQueue;
                    // X || (!X && Y) == X || Y
                    if (skillQueue.Any(qskill => skill == qskill.Skill && level == qskill.
                        Level && (!skill.IsTraining || level > skill.Level + 1)))
                    {
                        fillBrush = Brushes.RoyalBlue;
                    }
                }

                // Blink indicator of skill levels which are in training
                if (skill.IsTraining && level == skill.Level + 1)
                {
                    if (m_blinkAction == BlinkAction.Blink)
                        fillBrush = Brushes.RoyalBlue;
                    m_blinkAction = (m_blinkAction == BlinkAction.Reset) ? BlinkAction.Blink :
                        BlinkAction.Stop;
                }

                g.FillRectangle(fillBrush, brect);
            }
        }

        /// <summary>
        /// Draws the list item for the given skill group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void DrawItem(SkillGroup group, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draws the background
            using (Brush brush = Settings.UI.SafeForWork
                                     ? new SolidBrush(Color.FromArgb(75, 75, 75))
                                     : (Brush)new LinearGradientBrush(new PointF(0F, 0F), new PointF(0F, SkillHeaderHeight),
                                                                      Color.FromArgb(75, 75, 75), Color.FromArgb(25, 25, 25)))
            {
                g.FillRectangle(brush, e.Bounds);
            }

            using (Pen pen = new Pen(Color.FromArgb(100, 100, 100)))
            {
                g.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right + 1, e.Bounds.Top);
            }

            // Measure Texts
            string skillInTrainingSuffix = string.Empty;
            string skillsInQueueSuffix = string.Empty;
            bool hasTrainingSkill = group.Any(x => x.IsTraining);
            bool hasQueuedSkill = group.Any(x => x.IsQueued && !x.IsTraining);
            if (hasTrainingSkill)
                skillInTrainingSuffix = "( 1 in training )";
            if (hasQueuedSkill)
            {
                skillsInQueueSuffix = $"( {group.Count(x => x.IsQueued && !x.IsTraining)} in queue )";
            }

            string skillsSummaryText = $"{group.Count(x => x.IsKnown)} of {group.Count(x => x.IsPublic)} skills";

            string skillsTotalSPText = $"{group.TotalSP:N0} Points";

            Rectangle skillGroupNameTextRect = new Rectangle(e.Bounds.Left + PadLeft,
                                                             e.Bounds.Top + e.Bounds.Height / 2 - lbSkills.ItemHeight / 2,
                                                             m_maxGroupNameWidth + PadLeft / 2,
                                                             lbSkills.ItemHeight);

            int skillsSummaryTextWidth = (int)(SkillsSummaryTextWidth * (g.DpiX / EveMonConstants.DefaultDpi));
            Rectangle skillsSummaryTextRect = new Rectangle(
                skillGroupNameTextRect.X + m_maxGroupNameWidth + PadLeft / 2, skillGroupNameTextRect.Y,
                skillsSummaryTextWidth + PadLeft / 2, lbSkills.ItemHeight);

            int skillGroupTotalSPTextWidth = (int)(SkillGroupTotalSPTextWidth * (g.DpiX / EveMonConstants.DefaultDpi));
            Rectangle skillsTotalSPTextRect = new Rectangle(
                skillsSummaryTextRect.X + skillsSummaryTextWidth + PadLeft / 2, skillGroupNameTextRect.Y,
                skillGroupTotalSPTextWidth + PadLeft / 2, lbSkills.ItemHeight);

            Size skillInTrainingSuffixSize = TextRenderer.MeasureText(g, skillInTrainingSuffix, m_skillsFont, Size.Empty);
            Rectangle skillInTrainingSuffixRect = new Rectangle(
                skillsTotalSPTextRect.X + skillGroupTotalSPTextWidth + PadLeft / 2, skillGroupNameTextRect.Y,
                skillInTrainingSuffixSize.Width, lbSkills.ItemHeight);

            Size skillQueueTextSize = TextRenderer.MeasureText(g, skillsInQueueSuffix, m_skillsFont, Size.Empty);
            Rectangle skillQueueRect = new Rectangle(
                skillInTrainingSuffixRect.X + skillInTrainingSuffixSize.Width, skillInTrainingSuffixRect.Y,
                skillQueueTextSize.Width, lbSkills.ItemHeight);

            // Draw the header
            TextRenderer.DrawText(g, group.Name, m_boldSkillsFont, skillGroupNameTextRect, Color.White, Format);
            TextRenderer.DrawText(g, skillsSummaryText, m_skillsFont, skillsSummaryTextRect, Color.White,
                                  Format | TextFormatFlags.Right);
            TextRenderer.DrawText(g, skillsTotalSPText, m_skillsFont, skillsTotalSPTextRect, Color.White,
                                  Format | TextFormatFlags.Right);
            TextRenderer.DrawText(g, skillInTrainingSuffix, m_skillsFont, skillInTrainingSuffixRect, Color.White,
                                  Format | TextFormatFlags.Right);
            TextRenderer.DrawText(g, skillsInQueueSuffix, m_skillsFont, skillQueueRect, Settings.UI.SafeForWork
                ? Color.White
                : Color.Yellow,
                                  Format | TextFormatFlags.Right);

            // Draws the collapsing arrows
            bool isCollapsed = Character.UISettings.CollapsedGroups.Contains(group.Name);
            Image image = isCollapsed ? Resources.Expand : Resources.Collapse;

            g.DrawImageUnscaled(image, new Point(e.Bounds.Right - image.Width - CollapserPadRight,
                                                 SkillHeaderHeight / 2 - image.Height / 2 + e.Bounds.Top));
        }

        /// <summary>
        /// Gets the preferred size from the preferred size of the list.
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize) => lbSkills.GetPreferredSize(proposedSize);

        #endregion


        #region Toggle / expand

        /// <summary>
        /// Toggles all the skill groups to collapse or open.
        /// </summary>
        public void ToggleAll()
        {
            // When at least one group collapsed, expand all
            if (Character.UISettings.CollapsedGroups.Count != 0)
                Character.UISettings.CollapsedGroups.Clear();
                // When none collapsed, collapse all
            else
            {
                foreach (SkillGroup group in Character.SkillGroups)
                {
                    Character.UISettings.CollapsedGroups.Add(group.Name);
                }
            }

            // Update the list
            UpdateContent();
        }

        /// <summary>
        /// Toggles the expansion or collapsing of a single group
        /// </summary>
        /// <param name="group">The group to expand or collapse.</param>
        private void ToggleGroupExpandCollapse(SkillGroup group)
        {
            if (Character.UISettings.CollapsedGroups.Contains(group.Name))
                ExpandSkillGroup(group);
            else
                CollapseSkillGroup(group);
        }

        /// <summary>
        /// Expand a skill group
        /// </summary>
        /// <param name="group">Skill group in lbSkills</param>
        private void ExpandSkillGroup(SkillGroup group)
        {
            Character.UISettings.CollapsedGroups.Remove(group.Name);
            UpdateContent();
        }

        /// <summary>
        /// Collapse a skill group
        /// </summary>
        /// <param name="group">Skill group in lbSkills</param>
        private void CollapseSkillGroup(SkillGroup group)
        {
            Character.UISettings.CollapsedGroups.Add(group.Name);
            UpdateContent();
        }

        #endregion


        #region Local events

        /// <summary>
        /// Handles the MouseWheel event of the lbSkills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbSkills_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!lbSkills.VerticalScrollBarVisible())
                return;
            if (e.Delta == 0)
                return;

            // Update the drawing based upon the mouse wheel scrolling
            int numberOfItemLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / Math.Abs(e.Delta);
            int lines = numberOfItemLinesToMove;
            if (lines == 0)
                return;

            // Quit if at the top and requesting an up movement
            if (lines > 0 && lbSkills.GetVerticalScrollBarPosition() == 0)
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
                    if (lbSkills.TopIndex - i >= 0)
                        item = lbSkills.Items[lbSkills.TopIndex - i];
                }
                    // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = lbSkills.TopIndex + i - 1; j < lbSkills.Items.Count; j++)
                    {
                        height += GetItemHeight(lbSkills.Items[j]);
                    }

                    // Retrieve the next bottom item
                    if (height > lbSkills.ClientSize.Height)
                        item = lbSkills.Items[lbSkills.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = GetItemHeight(item) * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                lbSkills.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the lbSkills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbSkills_MouseDown(object sender, MouseEventArgs e)
        {
            // Retrieve the item at the given point and quit if none
            int index = lbSkills.IndexFromPoint(e.Location);
            if (index < 0 || index >= lbSkills.Items.Count)
                return;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == lbSkills.Items.Count - 1)
            {
                Rectangle itemRect = lbSkills.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    return;
            }

            // For a skill group, we have to handle the collapse/expand mechanism and the tooltip
            object item = lbSkills.Items[index];
            SkillGroup skillGroup = item as SkillGroup;
            if (skillGroup != null)
            {
                // Left button : expand/collapse
                if (e.Button != MouseButtons.Right)
                {
                    ToggleGroupExpandCollapse(skillGroup);
                    return;
                }

                // If right click on the button, still expand/collapse
                Rectangle itemRect = lbSkills.GetItemRectangle(lbSkills.Items.IndexOf(item));
                Rectangle buttonRect = GetButtonRectangle(skillGroup, itemRect);
                if (buttonRect.Contains(e.Location))
                {
                    ToggleGroupExpandCollapse(skillGroup);
                    return;
                }
                return;
            }

            // Right click for skills below lv5 : we display a context menu to plan higher levels
            Skill skill = (Skill)item;
            if (e.Button == MouseButtons.Right)
            {
                lbSkills.Cursor = Cursors.Default;

                // Set the selected skill
                m_selectedSkill = skill;

                // Display the context menu
                contextMenuStrip.Show(lbSkills, e.Location);
            }
        }

        /// <summary>
        /// On mouse move, we show the tooltip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSkills_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lbSkills.Items.Count; i++)
            {
                // Skip until we found the mouse location
                Rectangle rect = lbSkills.GetItemRectangle(i);
                if (!rect.Contains(e.Location))
                    continue;

                object item = lbSkills.Items[i];
                lbSkills.Cursor = item is Skill ? CustomCursors.ContextMenu : Cursors.Default;

                // Updates the tooltip
                DisplayTooltip(item);
                return;
            }

            // If we went so far, we're not over anything
            m_lastTooltipItem = null;
            ttToolTip.Active = false;
            lbSkills.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = !Character.Skills.Any();

            if (e.Cancel)
                return;

            BuildContextMenu(m_selectedSkill);

            showInSkillExplorerMenuItem.Visible = m_selectedSkill != null;
                showInMenuSeparator.Visible =
                    tsmiAddSkill.Visible = m_selectedSkill != null && m_selectedSkill.Level < 5;
        }

        /// <summary>
        /// Builds the context menu for the selected skill.
        /// </summary>
        /// <param name="skill">The skill.</param>
        private void BuildContextMenu(Skill skill)
        {
            tsmiAddSkill.DropDownItems.Clear();

            if (skill == null || skill.Level == 5)
            {
                tsmiAddSkill.Text = string.Empty;
                return;
            }
            
            // Reset the menu
            tsmiAddSkill.Text = $"Add {skill.Name}";

            // Build the level options
            for (long level = skill.Level + 1; level <= 5; level++)
            {
                ToolStripMenuItem tempMenuLevel = null;
                try
                {
                    tempMenuLevel = new ToolStripMenuItem($"Level {Skill.GetRomanFromInt(level)} to");

                    Character.Plans.AddTo(tempMenuLevel.DropDownItems,
                        (menuPlanItem, plan) =>
                        {
                            menuPlanItem.Click += menuPlanItem_Click;
                            menuPlanItem.Tag = new KeyValuePair<Plan, SkillLevel>(plan, new SkillLevel(skill, level));
                        });

                    ToolStripMenuItem menuLevel = tempMenuLevel;
                    tempMenuLevel = null;

                    tsmiAddSkill.DropDownItems.Add(menuLevel);
                }
                finally
                {
                    tempMenuLevel?.Dispose();
                }
            }
        }

        /// <summary>
        /// Displays the tooltip for the given item (skill or skillgroup).
        /// </summary>
        /// <param name="item"></param>
        private void DisplayTooltip(object item)
        {
            if (ttToolTip.Active && m_lastTooltipItem != null && m_lastTooltipItem == item)
                return;

            m_lastTooltipItem = item;

            ttToolTip.Active = false;
            SkillGroup skillGroup = item as SkillGroup;
            ttToolTip.SetToolTip(lbSkills, skillGroup != null ? GetTooltip(skillGroup) : GetTooltip((Skill)item));
            ttToolTip.Active = true;
        }

        /// <summary>
        /// Gets the tooltip text for the given skill
        /// </summary>
        /// <param name="skill"></param>
        private static string GetTooltip(Skill skill)
        {
            long sp = skill.SkillPoints;
            long nextLevel = Math.Min(5, skill.Level + 1);
            long nextLevelSP = skill.StaticData.GetPointsRequiredForLevel(nextLevel);
            long pointsLeft = skill.GetLeftPointsRequiredToLevel(nextLevel);
            string remainingTimeText = skill.GetLeftTrainingTimeToLevel(nextLevel)
                .ToDescriptiveText(DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.UppercaseText);

            if (sp < skill.StaticData.GetPointsRequiredForLevel(1))
            {
                // Training hasn't got past level 1 yet
                StringBuilder untrainedToolTip = new StringBuilder();
                untrainedToolTip
                    .Append($"Not yet trained to Level I ({Math.Floor(skill.PercentCompleted)}%)")
                    .AppendLine()
                    .Append($"Next level I: {pointsLeft:N0} skill points remaining")
                    .AppendLine()
                    .Append($"Training time remaining: {remainingTimeText}")
                    .AppendLine();

                AddSkillBoilerPlate(untrainedToolTip, skill);

                return untrainedToolTip.ToString();
            }

            // So, it's a left click on a skill, we display the tool tip
            // Partially trained skill ?
            if (skill.IsPartiallyTrained)
            {
                StringBuilder partiallyTrainedToolTip = new StringBuilder();
                partiallyTrainedToolTip
                    .Append($"Partially Completed ({Math.Floor(skill.PercentCompleted)}%)")
                    .AppendLine()
                    .Append($"Training to level {Skill.GetRomanFromInt(nextLevel)}: {pointsLeft:N0} skill points remaining")
                    .AppendLine()
                    .Append($"Training time remaining: {remainingTimeText}")
                    .AppendLine();

                AddSkillBoilerPlate(partiallyTrainedToolTip, skill);

                return partiallyTrainedToolTip.ToString();
            }

            // We've completed all the skill points for the current level
            if (!skill.IsPartiallyTrained)
            {
                if (skill.Level != 5)
                {
                    StringBuilder levelCompleteToolTip = new StringBuilder();
                    levelCompleteToolTip
                        .Append($"Completed Level {Skill.GetRomanFromInt(skill.Level)}: {sp:N0}/{nextLevelSP:N0}")
                        .AppendLine()
                        .Append($"Next level {Skill.GetRomanFromInt(nextLevel)}: {pointsLeft:N0} skill points required")
                        .AppendLine()
                        .Append($"Training Time: {remainingTimeText}")
                        .AppendLine();

                    AddSkillBoilerPlate(levelCompleteToolTip, skill);

                    return levelCompleteToolTip.ToString();
                }

                // Lv 5 completed
                StringBuilder lv5ToolTip = new StringBuilder();
                lv5ToolTip
                    .Append($"Level V Complete: {sp:N0}/{nextLevelSP:N0}")
                    .AppendLine()
                    .AppendLine("No further training required");

                AddSkillBoilerPlate(lv5ToolTip, skill);

                return lv5ToolTip.ToString();
            }

            // Error in calculating SkillPoints
            StringBuilder calculationErrorToolTip = new StringBuilder();
            calculationErrorToolTip
                .AppendLine("Partially Trained (Could not cacluate all skill details)")
                .Append($"Next level {nextLevel}: {pointsLeft:N0} skill points remaining")
                .AppendLine()
                .Append($"Training time remaining: {remainingTimeText}")
                .AppendLine();

            AddSkillBoilerPlate(calculationErrorToolTip, skill);

            return calculationErrorToolTip.ToString();
        }

        /// <summary>
        /// Adds the skill boiler plate.
        /// </summary>
        /// <param name="toolTip">The tool tip.</param>
        /// <param name="skill">The skill.</param>
        private static void AddSkillBoilerPlate(StringBuilder toolTip, Skill skill)
        {
            toolTip
                .AppendLine()
                .AppendLine(skill.Description.WordWrap(100))
                .Append($"Primary: {skill.PrimaryAttribute}, ")
                .Append($"Secondary: {skill.SecondaryAttribute} ")
                .Append($"({skill.SkillPointsPerHour:N0} SP/hour)");
        }

        /// <summary>
        /// Gets the tooltip content for the given skill group
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        private static string GetTooltip(SkillGroup group)
        {
            // Maximas are computed on public skills only
            long totalValidSP = group.Where(x => x.IsPublic).Sum(x => x.SkillPoints);
            long maxSP = group.Where(x => x.IsPublic).Sum(x => x.StaticData.GetPointsRequiredForLevel(5));
            int maxKnown = group.Count(x => x.IsPublic);

            // Current achievements are computed on every skill, including non-public
            long totalSP = group.Sum(x => x.SkillPoints);
            int known = group.Count(x => x.IsKnown);

            // The group has been completed !
            if (totalValidSP >= maxSP)
            {
                return $"Skill Group completed: {totalSP:N0}/{maxSP:N0} (100%){Environment.NewLine}" +
                       $"Skills: {known}/{maxKnown} (100%)";
            }

            // If the group is not completed yet
            double percentDonePoints = 1.0 * Math.Min(totalSP, maxSP) / maxSP;
            double percentDoneSkills = 1.0 * Math.Min(known, maxKnown) / maxKnown;

            return $"Points Completed: {totalSP:N0} of {maxSP:N0} ({percentDonePoints:P1}){Environment.NewLine}" +
                   $"Skills Known: {known} of {maxKnown} ({percentDoneSkills:P0})";
        }

        /// <summary>
        /// Gets the rectangle for the collapse/expand button.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="itemRect">The item rect.</param>
        /// <returns></returns>
        private Rectangle GetButtonRectangle(SkillGroup group, Rectangle itemRect)
        {
            // Checks whether this group is collapsed
            bool isCollapsed = Character.UISettings.CollapsedGroups.Contains(group.Name);

            // Get the image for this state
            Image btnImage = isCollapsed ? Resources.Expand : Resources.Collapse;

            // Compute the top left point
            Point btnPoint = new Point(itemRect.Right - btnImage.Width - CollapserPadRight,
                                       SkillHeaderHeight / 2 - btnImage.Height / 2 + itemRect.Top);

            return new Rectangle(btnPoint, btnImage.Size);
        }

        /// <summary>
        /// Handler for a plan item click on the plan's context menu.
        /// Add a skill to the plan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void menuPlanItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem planItem = (ToolStripMenuItem)sender;
            KeyValuePair<Plan, SkillLevel> tag = (KeyValuePair<Plan, SkillLevel>)planItem.Tag;

            IPlanOperation operation = tag.Key.TryPlanTo(tag.Value.Skill, tag.Value.Level);
            if (operation == null)
                return;

            PlanWindow planWindow = PlanWindow.ShowPlanWindow(plan: operation.Plan);
            if (planWindow == null)
                return;

            PlanHelper.SelectPerform(new PlanToOperationWindow(operation), planWindow, operation);
        }

        /// <summary>
        /// Shows the selected skill in Skill Browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showInSkillBrowserMenuItem_Click(object sender, EventArgs e)
        {
            // Open the skill browser
            PlanWindow.ShowPlanWindow(Character).ShowSkillInBrowser(m_selectedSkill);
        }

        /// <summary>
        /// Shows the selected skill in Skill Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showInSkillExplorerMenuItem_Click(object sender, EventArgs e)
        {
            // Open the skill explorer
            SkillExplorerWindow.ShowSkillExplorerWindow(Character).ShowSkillInExplorer(m_selectedSkill);
        }

        #endregion


        #region Global events

        /// <summary>
        /// Handles the TimerTick event of the EveMonClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_TimerTick(object sender, EventArgs e)
        {
            if (!Character.IsTraining || !Visible)
                return;

            if (m_blinkAction == BlinkAction.Stop)
                m_blinkAction = BlinkAction.Reset;

            // Redraw only the rows showing live training data (blinking level box,
            // skill points); repainting the whole list every tick makes it flicker
            for (int i = 0; i < lbSkills.Items.Count; i++)
            {
                object item = lbSkills.Items[i];
                Skill skill = item as Skill;
                SkillGroup group = item as SkillGroup;
                if ((skill != null && skill.IsTraining) ||
                    (group != null && group.Any(x => x.IsTraining)))
                {
                    // GetItemRectangle returns an empty rectangle for scrolled-out rows,
                    // and Invalidate treats an empty rectangle as "invalidate everything"
                    Rectangle itemRect = lbSkills.GetItemRectangle(i);
                    if (!itemRect.IsEmpty)
                        lbSkills.Invalidate(itemRect);
                }
            }
        }

        /// <summary>
        /// When the character changed, we refresh the content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != Character)
                return;

            UpdateContent();
        }

        /// <summary>
        /// When the settings changed, we refresh the content (show all public skills, non-public skills, etc)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            UpdateContent();
        }

        #endregion
    }
}
