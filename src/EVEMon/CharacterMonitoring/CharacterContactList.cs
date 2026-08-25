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
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Properties;

namespace EVEMon.CharacterMonitoring
{
    internal sealed partial class CharacterContactList : UserControl
    {
        #region Fields

        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix;

        // Contacts drawing - Region & text padding
        private const int PadTop = 2;
        private const int PadLeft = 6;
        private const int PadRight = 7;

        // Contacts drawing - Contacts
        private const int ContactDetailHeight = 34;

        // Contacts drawing - Contacts groups
        private const int ContactGroupHeaderHeight = 21;
        private const int CollapserPadRight = 4;

        private readonly Font m_contactsFont;
        private readonly Font m_contactsBoldFont;
        private readonly List<string> m_collapsedGroups = new List<string>();

        #endregion


        #region Constructor

        public CharacterContactList()
        {
            InitializeComponent();

            lbContacts.Hide();

            m_contactsFont = new Font(Font.FontFamily, 8.25F);
            m_contactsBoldFont = new Font(Font.FontFamily, 8.25F, FontStyle.Bold);
            noContactsLabel.Font = new Font(noContactsLabel.Font.FontFamily, 11.25F, FontStyle.Bold);
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the character associated with this monitor.
        /// </summary>
        internal CCPCharacter Character { get; set; }

        /// <summary>
        /// Gets or sets the standing status to show contacts for.
        /// </summary>
        internal StandingStatus ShowContactsWithStandings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show all contacts.
        /// </summary>
        internal bool ShowAllContacts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show contacts in watch list.
        /// </summary>
        internal bool ShowContactsInWatchList { get; set; }

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

            EveMonClient.CharacterContactsUpdated += EveMonClient_CharacterContactsUpdated;
            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            EveMonClient.EveIDToNameUpdated += EveMonClient_EveIDToNameUpdated;
            Disposed += OnDisposed;

            ShowAllContacts = true;
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.CharacterContactsUpdated -= EveMonClient_CharacterContactsUpdated;
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
        internal void UpdateContent()
        {
            // Returns if not visible
            if (!Visible)
                return;

            // When no character, we just hide the list
            if (Character == null)
            {
                noContactsLabel.Visible = true;
                lbContacts.Visible = false;
                return;
            }

            int scrollBarPosition = lbContacts.TopIndex;

            // Update the standings list
            lbContacts.BeginUpdate();
            try
            {
                IList<Contact> contacts = Character.Contacts.ToList();

                if (!ShowAllContacts && !ShowContactsInWatchList)
                    contacts = contacts.Where(contact => Standing.Status(contact.Standing) == ShowContactsWithStandings).ToList();

                if (ShowContactsInWatchList)
                    contacts = contacts.Where(contact => contact.IsInWatchlist).ToList();

                IEnumerable<IGrouping<ContactGroup, Contact>> groups = contacts.GroupBy(x => x.Group).OrderBy(x => (int)x.Key);

                // Scroll through groups
                lbContacts.Items.Clear();
                foreach (IGrouping<ContactGroup, Contact> group in groups)
                {
                    string groupHeaderText = $"{@group.Key.GetDescription()} ({@group.Count()})";
                    lbContacts.Items.Add(groupHeaderText);

                    // Add items in the group when it's not collapsed
                    if (m_collapsedGroups.Contains(groupHeaderText))
                        continue;

                    foreach (Contact contact in group.OrderBy(contact => contact.Name))
                    {
                        contact.ContactImageUpdated += contact_ContactImageUpdated;
                        lbContacts.Items.Add(contact);
                    }
                }

                // Display or hide the "no contacts" label.
                noContactsLabel.Visible = !contacts.Any();
                lbContacts.Visible = contacts.Any();

                // Invalidate display
                lbContacts.Invalidate();
            }
            finally
            {
                lbContacts.EndUpdate();
                lbContacts.TopIndex = scrollBarPosition;
            }
        }

        #endregion


        #region Drawing

        /// <summary>
        /// Handles the DrawItem event of the lbContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbContacts_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbContacts.Items.Count)
                return;

            object item = lbContacts.Items[e.Index];
            Contact contact = item as Contact;
            if (contact != null)
                DrawItem(contact, e);
            else
                DrawItem((string)item, e);
        }

        /// <summary>
        /// Handles the MeasureItem event of the lbContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void lbContacts_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.ItemHeight = GetItemHeight(lbContacts.Items[e.Index]);
        }

        /// <summary>
        /// Gets the item's height.
        /// </summary>
        /// <param name="item"></param>
        private int GetItemHeight(object item)
        {
            if (item is Contact)
                return Math.Max(m_contactsFont.Height * 2 + PadTop * 2, ContactDetailHeight);

            return ContactGroupHeaderHeight;
        }

        /// <summary>
        /// Draws the list item for the given standing
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="e"></param>
        private void DrawItem(Contact contact, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            // Draw background
            g.FillRectangle(e.Index % 2 == 0 ? Brushes.White : Brushes.LightGray, e.Bounds);
            // Measure and draw contact name
            Size contactTextSize = TextRenderer.MeasureText(g, contact.Name,
                m_contactsBoldFont, Size.Empty, Format);
            TextRenderer.DrawText(g, contact.Name, m_contactsBoldFont, new Rectangle(
                e.Bounds.Left + contact.EntityImage.Width + 4, e.Bounds.Top + (contact.Group ==
                ContactGroup.Agent ? PadTop : (e.Bounds.Height - contactTextSize.Height) / 2),
                contactTextSize.Width + PadLeft, contactTextSize.Height), Color.Black);

            // Draw text for agents
            if (contact.Group == ContactGroup.Agent)
            {
                Agent agent = StaticGeography.GetAgentByName(contact.Name);
                if (agent != null)
                {
                    Station agentStation = agent.Station;
                    string agentLocationText = agentStation != null ? agentStation.Name :
                        agent.Station.Name;
                    // Determine the agent level and division
                    string agentLevelText = (agent.AgentType != AgentType.BasicAgent &&
                        agent.AgentType != AgentType.ResearchAgent) ? agent.AgentType.
                        GetDescription() : $"Level {Skill.GetRomanFromInt(agent.Level)}";
                    string agentLevelDivisionText = $"( {agentLevelText} - {agent.Division} )";
                    // Calculate text size
                    Size agentLocationTextSize = TextRenderer.MeasureText(g, agentLocationText,
                        m_contactsFont, Size.Empty, Format);
                    Size agentLevelDivisionTextSize = TextRenderer.MeasureText(g,
                        agentLevelDivisionText, m_contactsFont, Size.Empty, Format);
                    // Draw agent level and division text
                    TextRenderer.DrawText(g, agentLevelDivisionText, m_contactsFont,
                        new Rectangle(e.Bounds.Left + contact.EntityImage.Width + 4 +
                            contactTextSize.Width + PadRight, e.Bounds.Top + PadTop,
                            agentLevelDivisionTextSize.Width + PadLeft,
                            agentLevelDivisionTextSize.Height), Color.Black, Format);

                    // Draw agent location
                    TextRenderer.DrawText(g, agentLocationText, m_contactsFont, new Rectangle(
                        e.Bounds.Left + contact.EntityImage.Width + 4, e.Bounds.Top + PadTop +
                        agentLevelDivisionTextSize.Height, agentLocationTextSize.Width +
                        PadLeft, agentLocationTextSize.Height), Color.Black);
                }
            }
            else if (Settings.UI.SafeForWork)
            {
                string contactStandingStatusText = $"({Standing.Status(contact.Standing)})";
                // Measure and draw standing text
                Size contactStandingStatusTextSize = TextRenderer.MeasureText(g,
                    contactStandingStatusText, m_contactsFont, Size.Empty, Format);
                TextRenderer.DrawText(g, contactStandingStatusText, m_contactsFont,
                    new Rectangle(e.Bounds.Left + contact.EntityImage.Width + 4 +
                    contactTextSize.Width + PadRight, e.Bounds.Top + (e.Bounds.Height -
                    contactStandingStatusTextSize.Height) / 2, contactStandingStatusTextSize.
                    Width + PadLeft, contactStandingStatusTextSize.Height), Color.Black);
                // Draw watchlist text
                if (contact.IsInWatchlist)
                {
                    const string ContactInWatchListText = " - Watching";
                    Size contactInWatchListTextSize = TextRenderer.MeasureText(g,
                        ContactInWatchListText, m_contactsFont, Size.Empty, Format);
                    TextRenderer.DrawText(g, ContactInWatchListText, m_contactsFont,
                        new Rectangle(e.Bounds.Left + contact.EntityImage.Width + 4 +
                            contactTextSize.Width + contactStandingStatusTextSize.Width +
                            PadRight, e.Bounds.Top + (e.Bounds.Height -
                            contactStandingStatusTextSize.Height) / 2,
                            contactInWatchListTextSize.Width + PadLeft,
                            contactInWatchListTextSize.Height), Color.Black);
                }
            }
            else
            {
                // Draw standing image
                Image standingImage = Standing.GetStandingImage((int)contact.Standing);
                g.DrawImage(standingImage, new Rectangle(e.Bounds.Left + contact.EntityImage.
                    Width + 4 + contactTextSize.Width + PadRight * 2, e.Bounds.Top + (e.Bounds.
                    Height - standingImage.Size.Height) / 2, standingImage.Width,
                    standingImage.Height));
                // Draw watchlist image
                if (contact.IsInWatchlist)
                    g.DrawImage(Resources.Watch, new Rectangle(e.Bounds.Left + contact.
                        EntityImage.Width + 4 + contactTextSize.Width + standingImage.Width +
                        PadRight * 3, e.Bounds.Top + (e.Bounds.Height - Resources.Watch.
                        Height) / 2, Resources.Watch.Width, Resources.Watch.Height));
            }

            // Draw images
            if (!Settings.UI.SafeForWork)
                g.DrawImage(contact.EntityImage, new Rectangle(e.Bounds.Left + PadLeft / 2,
                    ContactDetailHeight / 2 - contact.EntityImage.Height / 2 + e.Bounds.Top,
                    contact.EntityImage.Width, contact.EntityImage.Height));
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

            // Setting character spacing
            NativeMethods.SetTextCharacterSpacing(g, 4);

            // Measure texts
            Size standingGroupTextSize = TextRenderer.MeasureText(g, group.ToUpper(CultureConstants.DefaultCulture),
                                                                  m_contactsBoldFont, Size.Empty, Format);
            Rectangle standingGroupTextRect = new Rectangle(e.Bounds.Left + PadLeft,
                                                            e.Bounds.Top +
                                                            (e.Bounds.Height / 2 - standingGroupTextSize.Height / 2),
                                                            standingGroupTextSize.Width + PadRight,
                                                            standingGroupTextSize.Height);

            // Draws the text header
            TextRenderer.DrawText(g, group.ToUpper(CultureConstants.DefaultCulture), m_contactsBoldFont, standingGroupTextRect,
                                  Color.White, Color.Transparent, Format);

            // Draws the collapsing arrows
            bool isCollapsed = m_collapsedGroups.Contains(group);
            Image img = isCollapsed ? Resources.Expand : Resources.Collapse;

            g.DrawImageUnscaled(img, new Rectangle(e.Bounds.Right - img.Width - CollapserPadRight,
                                                   ContactGroupHeaderHeight / 2 - img.Height / 2 + e.Bounds.Top,
                                                   img.Width, img.Height));
        }

        /// <summary>
        /// Gets the preferred size from the preferred size of the list.
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize) => lbContacts.GetPreferredSize(proposedSize);

        #endregion


        #region Local events

        /// <summary>
        /// Handles the ContactImageUpdated event of the contact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void contact_ContactImageUpdated(object sender, EventArgs e)
        {
            // Force to redraw
            lbContacts.Invalidate();
        }

        /// <summary>
        /// Handles the MouseWheel event of the lbContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbContacts_MouseWheel(object sender, MouseEventArgs e)
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
                    if (lbContacts.TopIndex - i >= 0)
                        item = lbContacts.Items[lbContacts.TopIndex - i];
                }
                    // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = lbContacts.TopIndex + i - 1; j < lbContacts.Items.Count; j++)
                    {
                        height += GetItemHeight(lbContacts.Items[j]);
                    }

                    // Retrieve the next bottom item
                    if (height > lbContacts.ClientSize.Height)
                        item = lbContacts.Items[lbContacts.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = GetItemHeight(item) * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                lbContacts.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the lbContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbContacts_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbContacts.IndexFromPoint(e.Location);
            if (index < 0 || index >= lbContacts.Items.Count)
                return;

            Rectangle itemRect;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == lbContacts.Items.Count - 1)
            {
                itemRect = lbContacts.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    return;
            }

            // For a standings group, we have to handle the collapse/expand mechanism
            object item = lbContacts.Items[index];
            string contactsGroup = item as string;
            if (contactsGroup == null)
                return;

            // Left button : expand/collapse
            if (e.Button != MouseButtons.Right)
            {
                ToggleGroupExpandCollapse(contactsGroup);
                return;
            }

            // If right click on the button, still expand/collapse
            itemRect = lbContacts.GetItemRectangle(lbContacts.Items.IndexOf(item));
            Rectangle buttonRect = GetButtonRectangle(contactsGroup, itemRect);
            if (buttonRect.Contains(e.Location))
                ToggleGroupExpandCollapse(contactsGroup);
        }

        #endregion


        #region Helper Methods

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
                                       ContactGroupHeaderHeight / 2 - btnImage.Height / 2 + itemRect.Top);

            return new Rectangle(btnPoint, btnImage.Size);
        }

        #endregion


        #region Global events

        /// <summary>
        /// When the character standings update, we refresh the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterContactsUpdated(object sender, CharacterChangedEventArgs e)
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
        /// When ID to namae is updated we update the content.
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
