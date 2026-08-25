using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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
    internal sealed partial class CharacterLoyaltyList : UserControl
    {
        #region Fields

        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix;

        // Standings drawing - Region & text padding
        private const int PadTop = 2;
        private const int PadLeft = 6;
        private const int PadRight = 7;

        // Loyalty drawing - Loyalty
        private const int LoyaltyDetailHeight = 34;

        private readonly Font m_loyaltyFont;
        private readonly Font m_loyaltyBoldFont;

        #endregion


        #region Constructor

        public CharacterLoyaltyList()
        {
            InitializeComponent();

            lbLoyalty.Visible = false;

            m_loyaltyFont = new Font(Font.FontFamily, 8.25F);
            m_loyaltyBoldFont = new Font(Font.FontFamily, 8.25F, FontStyle.Bold);
            noLoyaltyLabel.Font = new Font(noLoyaltyLabel.Font.FontFamily, 11.25F, FontStyle.Bold);
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

            EveMonClient.CharacterLoyaltyPointsUpdated += EveMonClient_CharacterLoyaltyUpdated;
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
            EveMonClient.CharacterLoyaltyPointsUpdated -= EveMonClient_CharacterLoyaltyUpdated;
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
                noLoyaltyLabel.Visible = true;
                lbLoyalty.Visible = false;
                return;
            }

            int scrollBarPosition = lbLoyalty.TopIndex;

            // Update the standings list
            lbLoyalty.BeginUpdate();
            try
            {
                var loyaltyList = new List<Loyalty>(Character.LoyaltyPoints);
                loyaltyList.Sort();

                // Scroll through groups
                lbLoyalty.Items.Clear();

                foreach (var loyalty in loyaltyList)
                {
                    loyalty.LoyaltyCorpImageUpdated += loyalty_CorpImageUpdated;
                    lbLoyalty.Items.Add(loyalty);
                }

                // Display or hide the "no standings" label.
                noLoyaltyLabel.Visible = !loyaltyList.Any();
                lbLoyalty.Visible = loyaltyList.Any();

                // Invalidate display
                lbLoyalty.Invalidate();
            }
            finally
            {
                lbLoyalty.EndUpdate();
                lbLoyalty.TopIndex = scrollBarPosition;
            }
        }

        #endregion


        #region Drawing

        /// <summary>
        /// Handles the DrawItem event of the lbLoyalty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbLoyalty_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbLoyalty.Items.Count)
                return;

            Loyalty loyalty = lbLoyalty.Items[e.Index] as Loyalty;
            DrawItem(loyalty, e);
        }

        /// <summary>
        /// Handles the MeasureItem event of the lbLoyalty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void lbLoyalty_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.ItemHeight = GetItemHeight(lbLoyalty.Items[e.Index]);
        }

        /// <summary>
        /// Gets the item's height.
        /// </summary>
        /// <param name="item"></param>
        private int GetItemHeight(object item)
        {
            return Math.Max(m_loyaltyFont.Height * 2 + PadTop * 2, LoyaltyDetailHeight);
        }

        /// <summary>
        /// Draws the list item for the given loyalty point balance
        /// </summary>
        /// <param name="loyalty"></param>
        /// <param name="e"></param>
        private void DrawItem(Loyalty loyalty, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            g.FillRectangle(e.Index % 2 == 0 ? Brushes.White : Brushes.LightGray, e.Bounds);

            // Texts - corp on first row, points on last row
            string corp = loyalty.CorporationName;
            string loyaltyText = $"{loyalty.LoyaltyPoints}";
            string pointText = loyalty.LoyaltyPoints == 1 ? "point" : "points";

            // Measure texts
            Size corpTextSize = TextRenderer.MeasureText(g, corp, m_loyaltyBoldFont, Size.Empty, Format);
            Size loyaltyTextSize = TextRenderer.MeasureText(g, loyaltyText, m_loyaltyBoldFont, Size.Empty, Format);
            Size pointTextSize = TextRenderer.MeasureText(g, pointText, m_loyaltyFont, Size.Empty, Format);

            // Draw texts
            TextRenderer.DrawText(g, corp, m_loyaltyBoldFont, new Rectangle(
                e.Bounds.Left + PadLeft * 7, e.Bounds.Top + PadTop,
                corpTextSize.Width + PadLeft,
                corpTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, loyaltyText, m_loyaltyBoldFont, new Rectangle(
                e.Bounds.Left + PadLeft * 7, e.Bounds.Top + PadTop + corpTextSize.Height,
                loyaltyTextSize.Width + PadLeft,
                loyaltyTextSize.Height), Color.Black);

            TextRenderer.DrawText(g, pointText, m_loyaltyFont, new Rectangle(
                e.Bounds.Left + PadLeft * 8 + loyaltyTextSize.Width,
                e.Bounds.Top + PadTop + corpTextSize.Height,
                pointTextSize.Width + PadLeft, pointTextSize.Height), Color.Black);

            // Draw the corporation image
            if (Settings.UI.SafeForWork)
                return;

            g.DrawImage(loyalty.CorporationImage, new Rectangle(e.Bounds.Left + PadLeft / 2,
                LoyaltyDetailHeight / 2 - loyalty.CorporationImage.Height / 2 + e.Bounds.Top,
                loyalty.CorporationImage.Width, loyalty.CorporationImage.Height));
        }

        /// <summary>
        /// Gets the preferred size from the preferred size of the list.
        /// </summary>
        /// <param name="proposedSize"></param>
        /// <returns></returns>
        public override Size GetPreferredSize(Size proposedSize) => lbLoyalty.GetPreferredSize(proposedSize);

        #endregion


        #region Local events

        /// <summary>
        /// When the image updates, we redraw the list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void loyalty_CorpImageUpdated(object sender, EventArgs e)
        {
            // Force to redraw
            lbLoyalty.Invalidate();
        }

        /// <summary>
        /// Handles the MouseWheel event of the lbLoyalty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbLoyalty_MouseWheel(object sender, MouseEventArgs e)
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
                    if (lbLoyalty.TopIndex - i >= 0)
                        item = lbLoyalty.Items[lbLoyalty.TopIndex - i];
                }
                // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = lbLoyalty.TopIndex + i - 1; j < lbLoyalty.Items.Count; j++)
                    {
                        height += GetItemHeight(lbLoyalty.Items[j]);
                    }

                    // Retrieve the next bottom item
                    if (height > lbLoyalty.ClientSize.Height)
                        item = lbLoyalty.Items[lbLoyalty.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = GetItemHeight(item) * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                lbLoyalty.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the lbLoyalty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbLoyalty_MouseDown(object sender, MouseEventArgs e)
        {
            int index = lbLoyalty.IndexFromPoint(e.Location);
            if (index < 0 || index >= lbLoyalty.Items.Count)
                return;

            Rectangle itemRect;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == lbLoyalty.Items.Count - 1)
            {
                itemRect = lbLoyalty.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    return;
            }
        }

        #endregion


        #region Global events

        /// <summary>
        /// When the character loyalty point balances update, we refresh the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_CharacterLoyaltyUpdated(object sender, CharacterChangedEventArgs e)
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
