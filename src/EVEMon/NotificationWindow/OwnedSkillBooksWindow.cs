using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Models;

namespace EVEMon.NotificationWindow
{
    public partial class OwnedSkillBooksWindow : EVEMonForm
    {
        private readonly Character m_character;


        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="OwnedSkillBooksWindow"/> class from being created.
        /// </summary>
        private OwnedSkillBooksWindow()
        {
            InitializeComponent();

            RememberPositionKey = "OwnedSkillBooksWindow";

            lvOwnedSkillBooks.Visible = false;
            noOwnedSkillbooksLabel.Font = new Font(noOwnedSkillbooksLabel.Font.FontFamily, 11.25F, FontStyle.Bold);

            ListViewHelper.EnableDoubleBuffer(lvOwnedSkillBooks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnedSkillBooksWindow"/> class.
        /// </summary>
        /// <param name="character">The character.</param>
        public OwnedSkillBooksWindow(Character character)
            : this()
        {
            m_character = character;
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load, restores the window rectangle from the settings.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || this.IsDesignModeHosted())
                return;

            EveMonClient.CharacterAssetsUpdated += EveMonClient_CharacterAssetsUpdated;
            Disposed += OnDisposed;

            Text = string.Format(CultureConstants.DefaultCulture, Text, m_character.Name);

            UpdateList();
        }

        /// <summary>
        /// Called when [disposed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.CharacterAssetsUpdated -= EveMonClient_CharacterAssetsUpdated;
            Disposed -= OnDisposed;
        }

        #endregion


        #region Content Management

        /// <summary>
        /// Updates the list.
        /// </summary>
        internal void UpdateList()
        {
            int scrollBarPosition = lvOwnedSkillBooks.GetVerticalScrollBarPosition();

            // Store the selected item (if any) to restore it after the update
            int selectedItem = lvOwnedSkillBooks.SelectedItems.Count > 0
                ? lvOwnedSkillBooks.SelectedItems[0].Tag.GetHashCode()
                : 0;

            lvOwnedSkillBooks.BeginUpdate();
            try
            {
                IEnumerable<Skill> skills = m_character.Skills
                                                       .Where(skill => skill.IsOwned || skill.HasBookInAssets)
                                                       .OrderBy(x => x.Name);

                lvOwnedSkillBooks.Items.Clear();

                foreach (IGrouping<bool, Skill> group in skills.GroupBy(x => !x.IsKnown))
                {
                    ListViewGroup listGroup = group.Key
                                                  ? lvOwnedSkillBooks.Groups[0]
                                                  : lvOwnedSkillBooks.Groups[1];

                    // Add the items
                    lvOwnedSkillBooks.Items.AddRange(
                        group.Select(skill => new
                                                  {
                                                      skill,
                                                      item = new ListViewItem(skill.Name, listGroup)
                                                                 {
                                                                     UseItemStyleForSubItems = false,
                                                                     Tag = skill
                                                                 }
                                                  }).Select(x => CreateSubItems(x.skill, x.item)).ToArray());
                }

                // Restore the selected item (if any)
                if (selectedItem > 0)
                {
                    foreach (ListViewItem lvItem in lvOwnedSkillBooks.Items.Cast<ListViewItem>().Where(
                        lvItem => lvItem.Tag.GetHashCode() == selectedItem))
                    {
                        lvItem.Selected = true;
                    }
                }

                // Adjust the size of the columns
                AdjustColumns();

                // Display or hide the "no research points" label
                noOwnedSkillbooksLabel.Visible = lvOwnedSkillBooks.Items.Count == 0;
                lvOwnedSkillBooks.Visible = !noOwnedSkillbooksLabel.Visible;
            }
            finally
            {
                lvOwnedSkillBooks.EndUpdate();
                lvOwnedSkillBooks.SetVerticalScrollBarPosition(scrollBarPosition);
            }


        }

        /// <summary>
        /// Creates the list view sub items.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <param name="item">The item.</param>
        private ListViewItem CreateSubItems(Skill skill, ListViewItem item)
        {
            // Add enough subitems to match the number of columns
            while (item.SubItems.Count < lvOwnedSkillBooks.Columns.Count + 1)
            {
                item.SubItems.Add(string.Empty);
            }

            // Creates the subitems
            for (int i = 0; i < lvOwnedSkillBooks.Columns.Count; i++)
            {
                SetColumn(skill, item.SubItems[i], lvOwnedSkillBooks.Columns[i]);
            }

            return item;
        }

        /// <summary>
        /// Adjusts the columns.
        /// </summary>
        private void AdjustColumns()
        {
            foreach (ColumnHeader column in lvOwnedSkillBooks.Columns)
            {
                column.Width = -2;

                // Due to .NET design we need to prevent the last colummn to resize to the right end

                // Return if it's not the last column
                if (column.Index != lvOwnedSkillBooks.Columns.Count - 1)
                    continue;

                const int Pad = 4;

                // Calculate column header text width with padding
                int columnHeaderWidth = TextRenderer.MeasureText(column.Text, Font).Width + Pad * 2;

                // If there is an image assigned to the header, add its width with padding
                if (lvOwnedSkillBooks.SmallImageList != null && column.ImageIndex > -1)
                    columnHeaderWidth += lvOwnedSkillBooks.SmallImageList.ImageSize.Width + Pad;

                // Calculate the width of the header and the items of the column
                int columnMaxWidth = column.ListView.Items.Cast<ListViewItem>().Select(
                    item => TextRenderer.MeasureText(item.SubItems[column.Index].Text, Font).Width).Concat(
                        new[] { columnHeaderWidth }).Max() + Pad + 1;

                // Assign the width found
                column.Width = columnMaxWidth;
            }
        }

        /// <summary>
        /// Updates the listview sub-item.
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="item"></param>
        /// <param name="column"></param>
        private static void SetColumn(Skill skill, ListViewItem.ListViewSubItem item, ColumnHeader column)
        {
            switch (column.Index)
            {
                case 0:
                    item.Text = skill.Name;
                    break;
                case 1:
                    item.Text = skill.ArePrerequisitesMet ? "Yes" : "No";
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion


        #region Global Events

        /// <summary>
        /// Handles the CharacterAssetsUpdated event of the EveMonClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EVEMon.Common.CustomEventArgs.CharacterChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_CharacterAssetsUpdated(object sender, CharacterChangedEventArgs e)
        {
            if (e.Character != m_character)
                return;

            UpdateList();
        }

        #endregion


        #region Local Events

        /// <summary>
        /// Handles the ColumnWidthChanging event of the lvOwnedSkillBooks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ColumnWidthChangingEventArgs" /> instance containing the event data.</param>
        private void lvOwnedSkillBooks_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lvOwnedSkillBooks.Columns[e.ColumnIndex].Width;
        }

        #endregion
    }
}
