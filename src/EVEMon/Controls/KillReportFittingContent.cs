using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.SkillPlanner;

namespace EVEMon.Controls
{
    public partial class KillReportFittingContent : UserControl
    {
        #region Fields

        private const TextFormatFlags Format = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping | TextFormatFlags.NoPrefix;

        // KillReportFittingContent drawing - Region & text padding
        private const byte PadLeft = 6;
        private const byte PadRight = 7;

        // KillReportFittingContent drawing - Item
        private const byte FittingDetailHeight = 26;
        private const byte ItemImageSize = 24;

        private readonly Font m_fittingFont;
        private readonly Font m_fittingBoldFont;

        private KillLog m_killLog;
        private Item m_selectedItem;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KillReportFittingContent"/> class.
        /// </summary>
        public KillReportFittingContent()
        {
            InitializeComponent();

            FittingContentListBox.Visible = false;
            SaveFittingButton.Visible = false;
            ColorKeyGroupBox.Visible = false;

            m_fittingFont = new Font(Font.FontFamily, 8.25F);
            m_fittingBoldFont = new Font(Font.FontFamily, 8.25F, FontStyle.Bold);
            noItemsLabel.Font = new Font(noItemsLabel.Font.FontFamily, 11.25F, FontStyle.Bold);
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the kill log.
        /// </summary>
        /// <value>
        /// The kill log.
        /// </value>
        internal KillLog KillLog
        {
            get { return m_killLog; }
            set
            {
                m_killLog = value;

                if (!DesignMode || this.IsDesignModeHosted())
                    UpdateContent();
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

            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            EveMonClient.ItemPricesUpdated += EveMonClient_ItemPricesUpdated;
            Disposed += OnDisposed;
        }

        /// <summary>
        /// Called when disposed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            EveMonClient.ItemPricesUpdated -= EveMonClient_ItemPricesUpdated;
            Disposed -= OnDisposed;
        }

        #endregion


        #region Content Management

        /// <summary>
        /// Updates the content.
        /// </summary>
        private void UpdateContent()
        {
            // Update the standings list
            FittingContentListBox.BeginUpdate();
            try
            {
                IList<KillLogItem> items = m_killLog.Items.ToList();
                IEnumerable<IGrouping<KillLogFittingContentGroup, KillLogItem>> groups = items
                    .GroupBy(item => item.FittingContentGroup).OrderBy(x => x.Key);

                // Scroll through groups
                FittingContentListBox.Items.Clear();
                foreach (IGrouping<KillLogFittingContentGroup, KillLogItem> group in groups)
                {
                    FittingContentListBox.Items.Add(group.Key);

                    foreach (KillLogItem item in group)
                    {
                        // Add the item to the list
                        AddItem(item);

                        if (!item.Items.Any())
                            continue;

                        // Add items in a container to the list
                        foreach (KillLogItem itemInItem in item.Items)
                        {
                            AddItem(itemInItem);
                        }
                    }
                }

                // Display or hide the "no standings" label.
                noItemsLabel.Visible = !items.Any();
                FittingContentListBox.Visible = items.Any();


                // Invalidate display
                FittingContentListBox.Invalidate();
            }
            finally
            {
                FittingContentListBox.EndUpdate();
                ItemsCostLabel.Text = GetTotalCost();
            }
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        private void AddItem(KillLogItem item)
        {
            bool eventHandlerAdded = false;

            // Add if the item was destroyed
            if (item.QtyDestroyed > 0)
            {
                FittingContentListBox.Items.Add(item);
                item.KillLogItemImageUpdated += item_KillLogItemImageUpdated;
                eventHandlerAdded = true;
            }

            // Re-add if the item was also dropped
            if (item.QtyDropped <= 0)
                return;

            FittingContentListBox.Items.Add(item);
            if (!eventHandlerAdded)
                item.KillLogItemImageUpdated += item_KillLogItemImageUpdated;
        }

        /// <summary>
        /// Gets the total cost.
        /// </summary>
        /// <returns></returns>
        private string GetTotalCost()
        {
            double shipCost = Settings.MarketPricer.Pricer != null
                ? Settings.MarketPricer.Pricer.GetPriceByTypeID(m_killLog.Victim.ShipTypeID)
                : 0;
            bool unknownCost = m_killLog.Victim.ShipTypeID != DBConstants.CapsuleID && Math.Abs(shipCost) < double.Epsilon;
            double totalCost = shipCost;

            // Get the items cost
            double itemsCost;
            unknownCost |= GetItemsCost(m_killLog.Items, out itemsCost);
            totalCost += itemsCost;

            return unknownCost ? EveMonConstants.UnknownText : $" {totalCost:N2} ISK";
        }

        /// <summary>
        /// Gets the items cost.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="totalCost">The total cost.</param>
        /// <returns></returns>
        private static bool GetItemsCost(IEnumerable<KillLogItem> items, out double totalCost)
        {
            bool unknownCost = false;
            double itemCost = 0d;
            foreach (KillLogItem item in items)
            {
                double price = item.Price;
                unknownCost |= Math.Abs(price) < double.Epsilon;
                itemCost += price * (item.QtyDestroyed + item.QtyDropped);

                if (!item.Items.Any())
                    continue;

                unknownCost |= GetItemsCost(item.Items, out totalCost);
                itemCost += totalCost;
            }

            totalCost = itemCost;

            return unknownCost;
        }

        #endregion


        #region Drawing

        /// <summary>
        /// Handles the MeasureItem event of the FittingContentListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void FittingContentListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index >= 0)
                e.ItemHeight = FittingDetailHeight;
        }

        /// <summary>
        /// Handles the DrawItem event of the FittingContentListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void FittingContentListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= FittingContentListBox.Items.Count)
                return;

            object listItem = FittingContentListBox.Items[e.Index];
            KillLogItem item = listItem as KillLogItem;
            if (item != null)
            {
                // If item is the same with previous item then we previously have drawn a destroyed item
                // and now we need to draw a dropped one
                if (FittingContentListBox.Items[e.Index - 1] == item)
                    DrawItem(item, e, true);
                else
                {
                    // Draw a destroyed item or dropped one
                    if (item.QtyDestroyed > 0)
                        DrawItem(item, e);
                    else
                        DrawItem(item, e, true);
                }
            }
            else
                DrawItem((KillLogFittingContentGroup)listItem, e);
        }

        /// <summary>
        /// Draws the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        /// <param name="itemIsDropped">if set to <c>true</c> item is dropped.</param>
        private void DrawItem(KillLogItem item, DrawItemEventArgs e, bool itemIsDropped = false)
        {
            Graphics g = e.Graphics;

            // Draw background
            g.FillRectangle(itemIsDropped ? Brushes.Green : Brushes.LightGray, e.Bounds);

            int itemQty = itemIsDropped ? item.QtyDropped : item.QtyDestroyed;
            int inContainerPad = item.IsInContainer ? PadLeft * 2 : 0;

            // Texts size measure
            Size itemTextSize = TextRenderer.MeasureText(g, item.Name, m_fittingFont, Size.Empty, Format);
            Size itemQtyTextSize = TextRenderer.MeasureText(g, itemQty.ToNumericString(0), m_fittingFont);

            Rectangle itemTextRect = new Rectangle(e.Bounds.Left + inContainerPad + PadLeft * 2 + ItemImageSize,
                e.Bounds.Top + (e.Bounds.Height - itemTextSize.Height) / 2,
                itemTextSize.Width + PadRight, itemTextSize.Height);
            Rectangle itemQtyTextRect = new Rectangle(e.Bounds.Right - itemQtyTextSize.Width - PadRight,
                e.Bounds.Top + (e.Bounds.Height - itemTextSize.Height) / 2,
                itemQtyTextSize.Width + PadRight, itemQtyTextSize.Height);

            // Draw texts
            TextRenderer.DrawText(g, item.Name, m_fittingFont, itemTextRect, Color.Black);
            TextRenderer.DrawText(g, itemQty.ToNumericString(0), m_fittingFont, itemQtyTextRect, Color.Black);

            // Draw the image
            if (Settings.UI.SafeForWork)
                return;

            g.DrawImage(item.ItemImage, new Rectangle(e.Bounds.Left + inContainerPad + PadLeft * 2,
                e.Bounds.Top + (e.Bounds.Height - ItemImageSize) / 2,
                ItemImageSize, ItemImageSize));
        }

        /// <summary>
        /// Draws the list item for the given group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="e"></param>
        private void DrawItem(KillLogFittingContentGroup group, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            using (Brush brush = Settings.UI.SafeForWork
                ? new SolidBrush(Color.FromArgb(75, 75, 75))
                : (Brush)new LinearGradientBrush(new PointF(0F, 0F), new PointF(0F, FittingDetailHeight),
                    Color.FromArgb(75, 75, 75), Color.FromArgb(25, 25, 25)))
            {
                g.FillRectangle(brush, e.Bounds);
            }

            using (Pen pen = new Pen(Color.FromArgb(100, 100, 100)))
            {
                g.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right + 1, e.Bounds.Top);
            }

            Size fittingGroupTextSize = TextRenderer.MeasureText(g, group.GetDescription(), m_fittingBoldFont, Size.Empty, Format);
            Rectangle fittingGroupTextRect = new Rectangle(e.Bounds.Left + PadLeft / 3 + ItemImageSize,
                e.Bounds.Top +
                (e.Bounds.Height - fittingGroupTextSize.Height) / 2,
                fittingGroupTextSize.Width + PadRight,
                fittingGroupTextSize.Height);

            TextRenderer.DrawText(g, group.GetDescription(), m_fittingBoldFont, fittingGroupTextRect, Color.White);

            // Draw the group image
            if (Settings.UI.SafeForWork)
                return;

            Rectangle fittingGroupImageRect = new Rectangle(e.Bounds.Left + PadLeft / 3,
                e.Bounds.Top + (e.Bounds.Height - ItemImageSize) / 2,
                ItemImageSize, ItemImageSize);

            g.DrawImage(GetGroupImage(group), fittingGroupImageRect);
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Gets the group image.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        private Image GetGroupImage(KillLogFittingContentGroup group)
        {
            switch (group)
            {
                case KillLogFittingContentGroup.Cargo:
                case KillLogFittingContentGroup.Other:
                    return imageList.Images[1];
                case KillLogFittingContentGroup.HighSlot:
                    return imageList.Images[2];
                case KillLogFittingContentGroup.MediumSlot:
                    return imageList.Images[3];
                case KillLogFittingContentGroup.LowSlot:
                    return imageList.Images[4];
                case KillLogFittingContentGroup.RigSlot:
                    return imageList.Images[5];
                case KillLogFittingContentGroup.SubsystemSlot:
                    return imageList.Images[6];
                case KillLogFittingContentGroup.DroneBay:
                    return imageList.Images[0];
                case KillLogFittingContentGroup.Implant:
                case KillLogFittingContentGroup.Booster:
                    return imageList.Images[8];
                default:
                    return imageList.Images[0];
            }
        }

        #endregion


        #region Local Events

        /// <summary>
        /// Handles the MouseWheel event of the FittingContentListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void FittingContentListBox_MouseWheel(object sender, MouseEventArgs e)
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
                    if (FittingContentListBox.TopIndex - i >= 0)
                        item = FittingContentListBox.Items[FittingContentListBox.TopIndex - i];
                }
                    // Going down
                else
                {
                    // Compute the height of the items from current the topindex (included)
                    int height = 0;
                    for (int j = FittingContentListBox.TopIndex + i - 1; j < FittingContentListBox.Items.Count; j++)
                    {
                        height += FittingDetailHeight;
                    }

                    // Retrieve the next bottom item
                    if (height > FittingContentListBox.ClientSize.Height)
                        item = FittingContentListBox.Items[FittingContentListBox.TopIndex + i - 1];
                }

                // If found a new item as top or bottom
                if (item != null)
                    numberOfPixelsToMove[i - 1] = FittingDetailHeight * direction;
                else
                    lines -= direction;
            }

            // Scroll 
            if (lines != 0)
                FittingContentListBox.Invalidate();
        }

        /// <summary>
        /// Handles the MouseDown event of the FittingContentListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void FittingContentListBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Retrieve the item at the given point and quit if none
            int index = FittingContentListBox.IndexFromPoint(e.Location);
            if (index < 0 || index >= FittingContentListBox.Items.Count)
                return;

            KillLogItem killLogItem = FittingContentListBox.Items[index] as KillLogItem;

            // Beware, this last index may actually means a click in the whitespace at the bottom
            // Let's deal with this special case
            if (index == FittingContentListBox.Items.Count - 1)
            {
                Rectangle itemRect = FittingContentListBox.GetItemRectangle(index);
                if (!itemRect.Contains(e.Location))
                    killLogItem = null;
            }

            if (e.Button != MouseButtons.Right)
                return;

            // Right click reset the cursor
            FittingContentListBox.Cursor = Cursors.Default;

            // Set the selected item
            m_selectedItem = killLogItem?.Item;

            // Display the context menu
            contextMenuStrip.Show(FittingContentListBox, e.Location);
        }

        /// <summary>
        /// Handles the MouseMove event of the FittingContentListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void FittingContentListBox_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < FittingContentListBox.Items.Count; i++)
            {
                // Skip until we found the mouse location
                Rectangle rect = FittingContentListBox.GetItemRectangle(i);
                if (!rect.Contains(e.Location))
                    continue;

                object item = FittingContentListBox.Items[i];
                FittingContentListBox.Cursor = item is KillLogItem ? CustomCursors.ContextMenu : Cursors.Default;

                return;
            }

            // If we went so far, we're not over anything
            FittingContentListBox.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = m_selectedItem == null;

            if (e.Cancel || m_selectedItem == null)
                return;

            Ship ship = m_selectedItem as Ship;
            Blueprint blueprint = StaticBlueprints.GetBlueprintByID(m_selectedItem.ID);
            Skill skill = m_killLog.Character.Skills[m_selectedItem.ID];
            
            if (skill == Skill.UnknownSkill)
                skill = null;

            string text = ship != null ? "Ship" : blueprint != null ? "Blueprint" : skill != null ? "Skill" : "Item";

            showInBrowserMenuItem.Text = $"Show In {text} Browser...";

        }

        /// <summary>
        /// Handles the Click event of the showInBrowserMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void showInBrowserMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selectedItem == null)
                return;

            Ship ship = m_selectedItem as Ship;
            Blueprint blueprint = StaticBlueprints.GetBlueprintByID(m_selectedItem.ID);
            Skill skill = m_killLog.Character.Skills[m_selectedItem.ID];

            if (skill == Skill.UnknownSkill)
                skill = null;

            PlanWindow planWindow = PlanWindow.ShowPlanWindow(m_killLog.Character);

            if (ship != null)
                planWindow.ShowShipInBrowser(ship);
            else if (blueprint != null)
                planWindow.ShowBlueprintInBrowser(blueprint);
            else if (skill != null)
                planWindow.ShowSkillInBrowser(skill);
            else
                planWindow.ShowItemInBrowser(m_selectedItem);


        }

        /// <summary>
        /// Handles the KillLogItemImageUpdated event of the item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void item_KillLogItemImageUpdated(object sender, EventArgs e)
        {
            // Force to redraw
            FittingContentListBox.Invalidate();
        }

        /// <summary>
        /// Handles the Resize event of the FittingContentListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FittingContentListBox_Resize(object sender, EventArgs e)
        {
            FittingContentListBox.Invalidate();
        }

        /// <summary>
        /// Handles the Click event of the ToggleColorKeyPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToggleColorKeyPictureBox_Click(object sender, EventArgs e)
        {
            ColorKeyGroupBox.Visible = !ColorKeyGroupBox.Visible;
        }

        #endregion


        #region Global Events

        /// <summary>
        /// Handles the SettingsChanged event of the EveMonClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            // No need to do this if control is not visible
            if (!Visible)
                return;

            UpdateContent();
        }

        /// <summary>
        /// Occurs when the item prices get updated.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_ItemPricesUpdated(object sender, EventArgs e)
        {
            ItemsCostLabel.Text = GetTotalCost();
        }

        #endregion
    }
}
