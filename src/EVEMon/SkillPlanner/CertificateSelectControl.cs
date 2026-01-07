using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Controls;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// Represents a control to select certificates
    /// </summary>
    public partial class CertificateSelectControl : UserControl
    {
        // Blank image list for 'Safe for work' setting
        private readonly ImageList m_emptyImageList = new ImageList();

        private CertificateClass m_selectedCertificateClass;
        private Character m_character;
        private Plan m_plan;
        private Font m_iconsFont;
        private bool m_blockSelectionReentrancy;
        private bool m_allExpanded;
        private bool m_init;

        public event EventHandler<EventArgs> SelectionChanged;


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public CertificateSelectControl()
        {
            InitializeComponent();

            // Fix for grey background on transparent icons
            // Reset ImageStream to remove any designer-imposed 8-bit limitations
            ilCertIcons.ImageStream = null;
            ilCertIcons.ColorDepth = ColorDepth.Depth32Bit;
            ilCertIcons.ImageSize = new Size(24, 24);
            ilCertIcons.TransparentColor = Color.Transparent;

            // Reload images from embedded resources to ensure quality
            ReloadImageListFromResources();

            tbSearchText.KeyPress += tbSearchText_KeyPress;
            tbSearchText.Enter += tbSearchText_Enter;
            tbSearchText.Leave += tbSearchText_Leave;
            lvSortedList.SelectedIndexChanged += lvSortedList_SelectedIndexChanged;
            tvItems.AfterSelect += tvItems_AfterSelect;
            cmListCerts.Opening += cmListCerts_Opening;
        }

        #endregion


        #region Internal Properties

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        internal Character Character
        {
            get { return m_character; }
            set
            {
                if (value == null || m_character == value)
                    return;

                m_character = value;
            }
        }

        /// <summary>
        /// Gets or sets the current plan.
        /// </summary>
        internal Plan Plan
        {
            get { return m_plan; }
            set
            {
                if (m_plan == value)
                    return;

                // Should we be transforming a Data Browser to a Skill Planner?
                bool transformToPlanner = (value != null) && (m_plan == null) && (m_character != null);

                if (value == null)
                    return;

                m_plan = value;
                m_character = (Character)m_plan.Character;

                // Transform a Data Browser to a Skill Planner
                if (!transformToPlanner)
                    return;

                InitializeFiltersControls();
                UpdateContent();
            }
        }

        /// <summary>
        /// Gets or sets the selected certificate class.
        /// </summary>
        internal CertificateClass SelectedCertificateClass
        {
            get { return m_selectedCertificateClass; }
            set
            {
                if (m_selectedCertificateClass == value)
                    return;

                m_selectedCertificateClass = value;

                if (value == null)
                {
                    OnSelectionChanged();
                    return;
                }

                // Updates the selection for the three controls
                m_blockSelectionReentrancy = true;
                try
                {
                    tvItems.SelectNodeWithTag(value);
                }
                finally
                {
                    m_blockSelectionReentrancy = false;
                }

                // Fires event for subscribers
                OnSelectionChanged();
            }
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load, read settings and update the content.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

            m_emptyImageList.ImageSize = new Size(24, 24);
            m_emptyImageList.Images.Add(new Bitmap(24, 24));

            m_iconsFont = FontFactory.GetFont("Tahoma", 8.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Initialize the filters controls
            InitializeFiltersControls();

            EveMonClient.SettingsChanged += EveMonClient_SettingsChanged;
            Disposed += OnDisposed;

            // Updates the controls
            UpdateControlVisibility();
        }

        /// <summary>
        /// Initializes the filters controls.
        /// </summary>
        private void InitializeFiltersControls()
        {
            m_init = false;

            InitializeFilterControl();
            InitializeSortControl();

            InitiliazeSelectedIndexes();

            m_init = true;
        }

        /// <summary>
        /// Initializes the filter control.
        /// </summary>
        private void InitializeFilterControl()
        {
            cbFilter.Items.Clear();
            cbFilter.Items.AddRange(EnumExtensions.GetDescriptions<CertificateFilter>()
                .Where(description => !string.IsNullOrWhiteSpace((string)description))
                .ToArray());

            // Skill Planner or Data Browser (associated character)
            if ((m_plan != null) || (m_character != null))
                return;

            // Data Browser (associated character)
            if (m_character != null)
                return;

            // Data Browser (non-associated character)
            const int Index = (int)CertificateFilter.All + 1;
            while (cbFilter.Items.Count > Index)
            {
                cbFilter.Items.RemoveAt(Index);
            }
        }

        /// <summary>
        /// Initializes the sort control.
        /// </summary>
        private void InitializeSortControl()
        {
            cbSorting.Items.Clear();
            cbSorting.Items.AddRange(EnumExtensions.GetDescriptions<CertificateSort>()
                .Where(description => !string.IsNullOrWhiteSpace((string)description))
                .ToArray());

            // Skill Planner or Data Browser (associated character)
            if ((m_plan != null) || (m_character != null))
                return;

            cbSorting.Items.Remove(CertificateSort.TimeToNextLevel.GetDescription());
            cbSorting.Items.Remove(CertificateSort.TimeToMaxLevel.GetDescription());
        }

        /// <summary>
        /// Initiliazes the selected indexes.
        /// </summary>
        private void InitiliazeSelectedIndexes()
        {
            if (Settings.UI.UseStoredSearchFilters)
            {
                CertificateBrowserSettings settings;

                // Skill Planner
                if (m_plan != null)
                    settings = Settings.UI.CertificateBrowser;
                // Character associated Data Browser
                else if (m_character != null)
                    settings = Settings.UI.CertificateCharacterDataBrowser;
                // Data Browser
                else
                    settings = Settings.UI.CertificateDataBrowser;

                cbFilter.SelectedItem =
                    cbFilter.Items.Contains(settings.Filter.GetDescription())
                        ? settings.Filter.GetDescription()
                        : CertificateFilter.All.GetDescription();
                cbSorting.SelectedItem =
                    cbSorting.Items.Contains(settings.Sort.GetDescription())
                        ? settings.Sort.GetDescription()
                        : CertificateSort.None.GetDescription();

                tbSearchText.Text = settings.TextSearch;
                lbSearchTextHint.Visible = string.IsNullOrEmpty(tbSearchText.Text);

                return;
            }

            cbFilter.SelectedItem = CertificateFilter.All.GetDescription();
            cbSorting.SelectedItem = CertificateSort.None.GetDescription();
        }

        /// <summary>
        /// Occurs when the control visibility changed.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (!Visible)
                return;

            UpdateSearchTextHintVisibility();
        }

        /// <summary>
        /// Unsubscribe events on disposing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
            Disposed -= OnDisposed;
        }

        /// <summary>
        /// When the settings are changed, update the display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveMonClient_SettingsChanged(object sender, EventArgs e)
        {
            UpdateControlVisibility();
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Updates the control visibility.
        /// </summary>
        private void UpdateControlVisibility()
        {
            pbSearchImage.Visible = !Settings.UI.SafeForWork;
            UpdateContent();
        }

        /// <summary>
        /// Updates the search text hint visibility.
        /// </summary>
        private void UpdateSearchTextHintVisibility()
        {
            lbSearchTextHint.Visible = !tbSearchText.Focused && string.IsNullOrEmpty(tbSearchText.Text);
        }

        #endregion


        #region Control Events

        /// <summary>
        /// When the combo filter changes, we need to refresh the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();

            CertificateBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.CertificateBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.CertificateCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.CertificateDataBrowser;

            settings.Filter =
                (CertificateFilter)
                    (EnumExtensions.GetValueFromDescription<CertificateFilter>((string)cbFilter.SelectedItem) ??
                     CertificateFilter.All);
        }

        /// <summary>
        /// When the sort filter changes, we need to refresh the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();
            lvSortedList.Focus();

            CertificateBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.CertificateBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.CertificateCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.CertificateDataBrowser;

            settings.Sort =
                    (CertificateSort)
                        (EnumExtensions.GetValueFromDescription<CertificateSort>((string)cbSorting.SelectedItem) ??
                         CertificateSort.None);
        }

        /// <summary>
        /// When the "Search Text" label changes, we focus the textbox behind.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSearchTextHint_Click(object sender, EventArgs e)
        {
            tbSearchText.Focus();
        }

        /// <summary>
        /// When the user enters the search textbox, we hide the "search text" hint...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearchText_Enter(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = false;
        }

        /// <summary>
        /// When the user leaves the search textbox, we display the "search text" hint...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearchText_Leave(object sender, EventArgs e)
        {
            UpdateSearchTextHintVisibility();
        }

        /// <summary>
        /// When the search text box changes, we update the settings with this new filter and we update the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearchText_TextChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();

            CertificateBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.CertificateBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.CertificateCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.CertificateDataBrowser;

            settings.TextSearch = tbSearchText.Text;
        }

        /// <summary>
        /// When the "left button" key is pressed, we select the whole text. (???)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // (Ctrl + A) has KeyChar value 1
            if (e.KeyChar != (char)Keys.LButton)
                return;

            tbSearchText.SelectAll();
            e.Handled = true;
        }

        /// <summary>
        /// When the results listbox's selection is changed, we update the selected index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSearchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelection(lbSearchList.SelectedItem);
        }

        /// <summary>
        /// Changes the selection when you right click on a search.
        /// </summary>
        /// <param name="sender">is lbSearchList</param>
        /// <param name="e"></param>
        private void lbSearchList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            lbSearchList.SelectedIndex = lbSearchList.IndexFromPoint(e.Location);
            lbSearchList.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lbSearchList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            lbSearchList.Cursor = m_plan != null && lbSearchList.IndexFromPoint(e.Location) > -1
                ? CustomCursors.ContextMenu
                : Cursors.Default;
        }

        /// <summary>
        /// When the sorted listview' selection is changed, we update the selected index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSortedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelection(lvSortedList.SelectedItems.Cast<ListViewItem>().FirstOrDefault()?.Tag);
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lvSortedList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            lvSortedList.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lvSortedList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            lvSortedList.Cursor = m_plan != null && lvSortedList.GetItemAt(e.X, e.Y) != null
                ? CustomCursors.ContextMenu
                : Cursors.Default;
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void tvItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            tvItems.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void tvItems_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            tvItems.Cursor = CustomCursors.ContextMenu;
        }

        /// <summary>
        /// When the treeview's selection is changed, we update the selected index.
        /// Also used to force node selection on a right click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateSelection(e.Node?.Tag);
        }

        /// <summary>
        /// Handles the MouseUp event of the pbSearchTextDel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void pbSearchTextDel_MouseUp(object sender, MouseEventArgs e)
        {
            tbSearchText.Clear();
            UpdateSearchTextHintVisibility();
        }

        #endregion


        #region Display update

        /// <summary>
        /// Updates the display.
        /// </summary>
        private void UpdateContent()
        {
            IList<CertificateClass> classes = GetFilteredData().ToList();

            tvItems.Hide();
            lbSearchList.Hide();
            lvSortedList.Hide();
            lbNoMatches.Hide();

            // Nothing to display ?
            if (!classes.Any())
            {
                lbNoMatches.Show();
                UpdateSelection(null);
                return;
            }

            // Is it sorted ?
            if (cbSorting.SelectedIndex != 0)
            {
                lvSortedList.Show();
                UpdateListView(classes);
                return;
            }

            // Not sorted but there is a text filter
            if (!string.IsNullOrEmpty(tbSearchText.Text))
            {
                lbSearchList.Show();
                UpdateListBox(classes);
                return;
            }

            // Regular display, the tree 
            tvItems.Show();
            UpdateTree(classes);
        }

        /// <summary>
        /// Updates the certificates tree.
        /// </summary>
        /// <param name="classes">The list of certificate classes to show</param>
        private void UpdateTree(IList<CertificateClass> classes)
        {
            // Store the selected node (if any) to restore it after the update
            int selectedItemHash = tvItems.SelectedNode?.Tag?.GetHashCode() ?? 0;
            
            // Fill the tree
            int numberOfItems = 0;
            tvItems.BeginUpdate();
            try
            {
                tvItems.ImageList = Settings.UI.SafeForWork ? m_emptyImageList : ilCertIcons;

                // Clear the existing nodes
                tvItems.Nodes.Clear();

                // Creates the nodes representing the categories
                foreach (IGrouping<CertificateGroup, CertificateClass> category in classes.GroupBy(x => x.Category).OrderBy(x => x.Key.Name))
                {
                    int imageIndex = tvItems.ImageList.Images.IndexOfKey("Certificate");

                    TreeNode categoryNode = new TreeNode
                    {
                        Text = category.Key.Name,
                        ImageIndex = imageIndex,
                        SelectedImageIndex = imageIndex,
                        Tag = category
                    };

                    foreach (TreeNode node in category
                        .Select(certClass =>
                            new
                            {
                                certClass,
                                index = m_character == null ? imageIndex : GetCertImageIndex(certClass.Certificate)
                            })
                        .Select(childNode =>
                            new TreeNode
                            {
                                Text = childNode.certClass.Name,
                                ImageIndex = childNode.index,
                                SelectedImageIndex = childNode.index,
                                Tag = childNode.certClass
                            }))
                    {
                        categoryNode.Nodes.Add(node);
                        numberOfItems++;
                    }

                    tvItems.Nodes.Add(categoryNode);
                }

                TreeNode selectedNode = null;

                // Restore the selected node (if any)
                if (selectedItemHash > 0)
                {
                    foreach (TreeNode node in tvItems.GetAllNodes()
                        .Where(node => node.Tag.GetHashCode() == selectedItemHash))
                    {
                        tvItems.SelectNodeWithTag(node.Tag);
                        selectedNode = node;
                    }
                }

                if (selectedNode != null)
                    return;

                // Reset if the node doesn't exist anymore
                tvItems.SelectNodeWithTag(null);
                UpdateSelection(null);
            }
            finally
            {
                tvItems.EndUpdate();
                m_allExpanded = false;

                // If the filtered set is small enough to fit all nodes on screen, call expandAll()
                if (numberOfItems < tvItems.DisplayRectangle.Height / tvItems.ItemHeight)
                {
                    tvItems.ExpandAll();
                    m_allExpanded = true;
                }
            }
        }

        /// <summary>
        /// Updates the list box displayed when there is a text filter and no sort criteria.
        /// </summary>
        /// <param name="classes">The list of certificates to show</param>
        private void UpdateListBox(IList<CertificateClass> classes)
        {
            // Store the selected node (if any) to restore it after the update
            int selectedItemHash = tvItems.SelectedNode?.Tag?.GetHashCode() ?? 0;

            lbSearchList.BeginUpdate();
            try
            {
                lbSearchList.Items.Clear();

                foreach (CertificateClass certClass in classes)
                {
                    lbSearchList.Items.Add(certClass);

                    // Restore the selected node (if any)
                    if (selectedItemHash > 0 && certClass.GetHashCode() == selectedItemHash)
                        lbSearchList.SelectedItem = certClass;
                }
            }
            finally
            {
                lbSearchList.EndUpdate();
            }
        }

        /// <summary>
        /// Updates the listview displayed when there is a sort criteria.
        /// </summary>
        /// <param name="classes">The list of certificates to show</param>
        private void UpdateListView(IList<CertificateClass> classes)
        {
            // Store the selected node (if any) to restore it after the update
            int selectedItemHash = tvItems.SelectedNode?.Tag?.GetHashCode() ?? 0;

            // Retrieve the data to fetch into the list
            IEnumerable<string> labels = null;
            string column = GetSortedListData(ref classes, ref labels);
            if (labels == null)
                return;

            lvSortedList.BeginUpdate();
            try
            {
                lvSortedList.Items.Clear();

                using (IEnumerator<string> labelsEnumerator = labels.GetEnumerator())
                {
                    foreach (CertificateClass certClass in classes)
                    {
                        // Retrieves the label for the second column (sort key)
                        labelsEnumerator.MoveNext();
                        string label = labelsEnumerator.Current;

                        // Add the item
                        ListViewItem lvi = new ListViewItem(certClass.Name);
                        lvi.SubItems.Add(new ListViewItem.ListViewSubItem(lvi, label));
                        lvi.Tag = certClass;

                        lvSortedList.Items.Add(lvi);

                       // Restore the selected node (if any)
                        if (selectedItemHash > 0 && certClass.GetHashCode() == selectedItemHash)
                            lvi.Selected = true;
                    }
                }

                // Auto adjust column widths
                chSortKey.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                chName.Width = Math.Max(0, Math.Max(lvSortedList.ClientSize.Width / 2,
                                                    lvSortedList.ClientSize.Width - (chSortKey.Width + 16)));
                chSortKey.Text = column;
            }
            finally
            {
                lvSortedList.EndUpdate();
            }
        }

        /// <summary>
        /// Gets the filtered list of data.
        /// </summary>
        /// <returns>The fitlerd list of the data</returns>
        private IEnumerable<CertificateClass> GetFilteredData()
        {
            IEnumerable<CertificateClass> classes = m_character?.CertificateClasses ??
                                                    CertificateClassCollection.CertificateClasses;

            // Apply the selected filter 
            Func<CertificateClass, bool> predicate = GetFilter();
            classes = classes.Where(predicate);

            // Text search
            if (!string.IsNullOrEmpty(tbSearchText.Text))
            {
                classes = classes.Where(x => x.Name.Contains(tbSearchText.Text, ignoreCase: true));
            }

            // When sorting by "time to...", filter completed items
            if ((string)cbSorting.SelectedItem == CertificateSort.TimeToMaxLevel.GetDescription() ||
                (string)cbSorting.SelectedItem == CertificateSort.TimeToNextLevel.GetDescription())
            {
                classes = classes.Where(x => !x.IsCompleted);
            }

            return classes;
        }

        /// <summary>
        /// Gets the items' filter.
        /// </summary>
        private Func<CertificateClass, bool> GetFilter()
        {
            CertificateFilter certificateFilter =
                (CertificateFilter)
                    (EnumExtensions.GetValueFromDescription<CertificateFilter>((string)cbFilter.SelectedItem) ??
                     CertificateFilter.All);

            // Update the base filter from the combo box
            switch (certificateFilter)
            {
                default:
                    return x => true;
                case CertificateFilter.HideMaxLevel:
                    return x => !x.IsCompleted;
                case CertificateFilter.NextLevelTrainable:
                    return x => x.IsFurtherTrainable;
                case CertificateFilter.NextLevelUntrainable:
                    return x => !x.IsFurtherTrainable & !x.IsCompleted;
                case CertificateFilter.Completed:
                    return x => x.IsCompleted;
            }
        }

        /// <summary>
        /// Gets the data for the sorted list view.
        /// </summary>
        /// <param name="classes">The certificate classes which are shown</param>
        /// <param name="labels">The labeltexts to show</param>
        /// <returns>The column header text</returns>
        private string GetSortedListData(ref IList<CertificateClass> classes, ref IEnumerable<string> labels)
        {
            CertificateSort certificateSort =
                (CertificateSort)
                    (EnumExtensions.GetValueFromDescription<CertificateSort>((string)cbSorting.SelectedItem) ??
                     CertificateSort.None);

            IEnumerable<TimeSpan> times;
            string columnHeader;

            switch (certificateSort)
            {
                // Sort by name, default, occurs on initialization
                default:
                    return string.Empty;
                    // Sort by time to next level
                case CertificateSort.TimeToNextLevel:
                    {
                        times = classes.Select(GetTimeToNextLevel);
                        columnHeader = "Time";
                        break;
                    }
                    // Sort by time to max level
                case CertificateSort.TimeToMaxLevel:
                    {
                        times = classes.Select(GetTimeToMaxLevel);
                        columnHeader = "Time to Max Level";
                        break;
                    }
            }

            CertificateClass[] classesArray = classes.ToArray();
            TimeSpan[] timesArray = times.ToArray();
            Array.Sort(timesArray, classesArray);
            classes = classesArray;
            labels = timesArray.Select(x => x.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas));
            return columnHeader;
        }

        /// <summary>
        /// Gets the time to next grade.
        /// </summary>
        /// <param name="certificateClass">The certificate class.</param>
        /// <returns>The time required to finish the next grade</returns>
        private static TimeSpan GetTimeToNextLevel(CertificateClass certificateClass)
        {
            CertificateLevel lowestTrinedLevel = certificateClass.Certificate.LowestUntrainedLevel;
            return lowestTrinedLevel?.GetTrainingTime ?? TimeSpan.Zero;
        }

        /// <summary>
        /// Gets the time to elite grade.
        /// </summary>
        /// <param name="certificateClass">The certificate class.</param>
        /// <returns>The time required to finish the final grade</returns>
        private static TimeSpan GetTimeToMaxLevel(CertificateClass certificateClass)
        {
            CertificateLevel levelFive = certificateClass.Certificate.GetCertificateLevel(5);
            return levelFive.IsTrained
                ? TimeSpan.Zero
                : levelFive.GetTrainingTime;
        }

        /// <summary>
        /// Gets the image's index for the provided certificate class,
        /// lazily creating the images when they're needed.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <returns>The index of the image appropriate for the certificate</returns>
        private int GetCertImageIndex(Certificate certificate)
        {
            if (!Settings.UI.SafeForWork)
            {
                // Untrained icons at indices 0-5, trained icons at indices 6-11
                // If no level trained, show untrained level 0 icon
                // If a level is trained, show trained icon for that level
                return certificate.HighestTrainedLevel == null
                    ? 0
                    : (int)certificate.HighestTrainedLevel.Level + 6;
            }

            // Prepares data
            char[] chars = new char[5];
            bool[] trained = new bool[5];

            foreach (int level in certificate.AllLevel
                .Select(certLevel =>
                    new
                    {
                        certLevel,
                        level = (int)certLevel.Level
                    })
                .Where(cert => cert.certLevel.IsTrained)
                .Select(cert => cert.level))
            {
                trained[level - 1] = true;
                chars[level - 1] = Convert.ToChar(level);
            }

            // Create key and retrieves its index, then returns if it already exists
            string key = string.Concat(chars);
            int index = tvItems.ImageList.Images.IndexOfKey(key);
            if (index != -1)
                return index;

            // Create the image if it does not exist yet
            const int ImageSize = 32;
            const int MaxLetterWidth = 6;

            Bitmap bmp;
            using(Bitmap tempBitmap = new Bitmap(ImageSize, ImageSize, PixelFormat.Format32bppArgb))
            {
                bmp = (Bitmap)tempBitmap.Clone();
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                string[] letters = new string[5];
                float[] xPositions = new float[5];
                float x = 0.0f,
                    height = 0.0f;

                int i = 0;
                // Scroll through letters and measure them
                foreach (var certLevel in certificate.AllLevel)
                {
                    letters[i] = ((int)certLevel.Level).ToString();
                    SizeF size = g.MeasureString(letters[i], m_iconsFont, MaxLetterWidth, StringFormat.GenericTypographic);
                    height = Math.Max(height, size.Height);
                    xPositions[i] = x;
                    x += size.Width + 1.0f;
                    i++;
                }

                // Y offset
                float y = Math.Max(0.0f, (ImageSize - height) * 0.5f);

                // Draw the letters
                g.Clear(Color.White);
                using (SolidBrush grantedBrush = new SolidBrush(Color.Blue))
                using (SolidBrush nonGrantedBrush = new SolidBrush(Color.Gray))
                {
                    for (int j = 0; j < certificate.AllLevel.Count(); j++)
                    {
                        // Special color for trained, gray for the other ones
                        bool isTrained = trained[j];
                        SolidBrush brush = isTrained ? grantedBrush : nonGrantedBrush;
                        g.DrawString(letters[j], m_iconsFont, brush, xPositions[j], y);
                    }
                }
            }
            // Insert image and return its index
            tvItems.ImageList.Images.Add(key, bmp);
            return tvItems.ImageList.Images.IndexOfKey(key);
        }

        #endregion


        #region Selection Helper Methods

        /// <summary>
        /// Called whenever the selection changes,
        /// fires the approriate event.
        /// </summary>
        private void OnSelectionChanged()
        {
            SelectionChanged?.ThreadSafeInvoke(this, new EventArgs());
        }

        /// <summary>
        /// Updates the item from the provided selection.
        /// </summary>
        /// <param name="selection">The selection</param>
        private void UpdateSelection(object selection)
        {
            if (!m_blockSelectionReentrancy)
                SelectedCertificateClass = selection as CertificateClass;
        }

        #endregion


        #region Context menus

        /// <summary>
        /// Context > Plan To > Level N
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void planToLevelMenuItem_Click(object sender, EventArgs e)
        {
            IPlanOperation operation = ((ToolStripMenuItem)sender).Tag as IPlanOperation;
            if (operation == null)
                return;

            PlanWindow planWindow = ParentForm as PlanWindow;
            if (planWindow == null)
                return;

            PlanHelper.SelectPerform(new PlanToOperationWindow(operation), planWindow, operation);
        }

        /// <summary>
        /// When the tree's context menu opens,
        /// we update the submenus' statuses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmListCerts_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip contextMenu = sender as ContextMenuStrip;

            e.Cancel = contextMenu?.SourceControl == null ||
                       (!contextMenu.SourceControl.Visible && m_selectedCertificateClass == null) ||
                       (!tvItems.Visible && m_plan == null);

            if (e.Cancel || contextMenu?.SourceControl == null)
                return;

            contextMenu.SourceControl.Cursor = Cursors.Default;

            CertificateClass certificateClass = null;
            TreeNode node = tvItems.SelectedNode;
            if (node != null)
                certificateClass = node.Tag as CertificateClass;

            if (m_selectedCertificateClass == null && certificateClass != null)
                node = null;

            cmiLvPlanToLevel.Visible = m_plan != null && m_selectedCertificateClass?.Certificate != null;
            planToSeparator.Visible = m_plan != null && m_selectedCertificateClass?.Certificate != null && node != null && tvItems.Visible;

            // "Expand" and "Collapse" selected menu
            cmiExpandSelected.Visible = m_selectedCertificateClass?.Certificate == null && node != null && !node.IsExpanded;
            cmiCollapseSelected.Visible = m_selectedCertificateClass?.Certificate == null && node != null && node.IsExpanded;

            cmiExpandSelected.Text = m_selectedCertificateClass?.Certificate == null && node != null && !node.IsExpanded
                ? $"Expand \"{node.Text}\""
                : string.Empty;
            cmiCollapseSelected.Text = m_selectedCertificateClass?.Certificate == null && node != null && node.IsExpanded
                ? $"Collapse \"{node.Text}\""
                : string.Empty;

            expandCollapseSeparator.Visible = m_selectedCertificateClass?.Certificate == null && node != null;

            // "Expand All" and "Collapse All" menu
            cmiCollapseAll.Enabled = cmiCollapseAll.Visible = tvItems.Visible && m_allExpanded;
            cmiExpandAll.Enabled = cmiExpandAll.Visible = tvItems.Visible && !cmiCollapseAll.Enabled;

            if (m_selectedCertificateClass?.Certificate == null || m_plan == null)
                return;

            cmiLvPlanToLevel.Enabled = m_selectedCertificateClass?.Certificate != null &&
                !m_plan.WillGrantEligibilityFor(m_selectedCertificateClass.Certificate.GetCertificateLevel(5));

            cmiLvPlanToLevel.Text =
                $"Plan {(m_selectedCertificateClass == null ? string.Empty : $"\"{m_selectedCertificateClass?.Name}\" ")}to...";
            
            // "Plan to N" menus
            for (int i = 1; i <= 5; i++)
            {
                SetAdditionMenuStatus(cmiLvPlanToLevel.DropDownItems[i - 1],
                    m_selectedCertificateClass.Certificate.GetCertificateLevel(i));
            }
        }

        /// <summary>
        /// Sets the visible status of the context menu submenu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="certLevel">The cert level.</param>
        private void SetAdditionMenuStatus(ToolStripItem menu, CertificateLevel certLevel)
        {
            menu.Visible = certLevel != null;

            if (certLevel == null)
                return;

            menu.Enabled = !m_plan.WillGrantEligibilityFor(certLevel);

            if (menu.Enabled)
                menu.Tag = m_plan.TryPlanTo(certLevel);
        }

        /// <summary>
        /// Treeview's context menu > Expand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiExpandSelected_Click(object sender, EventArgs e)
        {
            tvItems.SelectedNode.Expand();
        }

        /// <summary>
        /// Treeview's context menu > Collapse 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCollapseSelected_Click(object sender, EventArgs e)
        {
            tvItems.SelectedNode.Collapse();
        }

        /// <summary>
        /// Treeview's context menu > Expand All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiExpandAll_Click(object sender, EventArgs e)
        {
            tvItems.ExpandAll();
            m_allExpanded = true;
        }

        /// <summary>
        /// Treeview's context menu > Collapse All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCollapseAll_Click(object sender, EventArgs e)
        {
            tvItems.CollapseAll();
            m_allExpanded = false;
        }

        #endregion


        #region Image Loading

        /// <summary>
        /// Reloads the certificate icons from embedded resources to ensure 32-bit color depth.
        /// Loads untrained icons at indices 0-5, trained icons at indices 6-11, Certificate icon at index 12.
        /// </summary>
        private void ReloadImageListFromResources()
        {
            ilCertIcons.Images.Clear();
            var asm = typeof(EVEMon.Common.Properties.Resources).Assembly;

            // Load untrained icons (indices 0-5)
            for (int i = 0; i <= 5; i++)
            {
                string resourceName = $"EVEMon.Common.Resources.Images.level{i}_24.png";
                using (var stream = asm.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            ilCertIcons.Images.Add($"level{i}", new Bitmap(img));
                        }
                    }
                }
            }

            // Load trained icons (indices 6-11) - _1 suffix
            for (int i = 0; i <= 5; i++)
            {
                string resourceName = $"EVEMon.Common.Resources.Images.level{i}_24_1.png";
                using (var stream = asm.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            ilCertIcons.Images.Add($"level{i}_trained", new Bitmap(img));
                        }
                    }
                }
            }

            // Load the Certificate icon (used for category nodes)
            // Try with hyphen first (original filename), then underscore as fallback
            string certResourceName = "EVEMon.Common.Resources.Images.Certificate-24.png";
            var certStream = asm.GetManifestResourceStream(certResourceName);
            if (certStream == null)
            {
                certResourceName = "EVEMon.Common.Resources.Images.Certificate_24.png";
                certStream = asm.GetManifestResourceStream(certResourceName);
            }

            if (certStream != null)
            {
                using (certStream)
                using (var img = Image.FromStream(certStream))
                {
                    ilCertIcons.Images.Add("Certificate", new Bitmap(img));
                }
            }
        }

        #endregion
    }
}