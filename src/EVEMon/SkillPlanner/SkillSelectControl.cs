using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Constants;
using EVEMon.Common.Controls;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Resources.Skill_Select;
using EVEMon.Common.Models.Collections;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.SkillPlanner
{
    public partial class SkillSelectControl : UserControl
    {
        public event EventHandler<EventArgs> SelectedSkillChanged;

        private bool m_allExpanded;
        private Character m_character;
        private Skill m_selectedSkill;
        private Plan m_plan;
        private bool m_init;
        private bool m_blockSelectionReentrancy;


        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SkillSelectControl()
        {
            InitializeComponent();
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
        /// Gets or sets the plan.
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
        /// Gets the selected skill.
        /// </summary>
        internal Skill SelectedSkill
        {
            get { return m_selectedSkill; }
            set
            {
                if (m_selectedSkill == value)
                    return;

                m_selectedSkill = value;

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

        /// <summary>
        /// Gets or sets true whether the control is hosted in the skill browser.
        /// When true, the "Show in skill browser" context menus won't be displayed.
        /// </summary>
        [Category("Behavior"),
         Description("When true, the \"Show in Skill Browser\" context menus won't be displayed."),
         DefaultValue(false)]
        public bool HostedInSkillBrowser { get; set; }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load, restore settings and update the content
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // Call the base method
            base.OnLoad(e);

            // Return on design mode
            if (DesignMode || this.IsDesignModeHosted())
                return;

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

            // Create the attributes combinations and add them to the combo box
            cbFilterByAttributes.Items.AddRange(GetAttributesCombinations().ToArray());

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
            cbSkillFilter.Items.Clear();
            cbSkillFilter.Items.AddRange(EnumExtensions.GetDescriptions<SkillFilter>()
                .Where(description => !string.IsNullOrWhiteSpace((string)description))
                .ToArray());

            // Skill Planner
            if (m_plan != null)
                return;

            // Data Browser (associated character)
            if (m_character != null)
            {
                cbSkillFilter.Items.Remove(SkillFilter.NotPlanned.GetDescription());
                cbSkillFilter.Items.Remove(SkillFilter.NotPlannedButTrainable.GetDescription());
                cbSkillFilter.Items.Remove(SkillFilter.Planned.GetDescription());

                return;
            }

            // Data Browser (non-associated character)
            const int Index = (int)SkillFilter.ByAttributes + 1;
            while (cbSkillFilter.Items.Count > Index)
            {
                cbSkillFilter.Items.RemoveAt(Index);
            }
            cbSkillFilter.Items.Add(SkillFilter.AlphaFriendly.GetDescription());
        }

        /// <summary>
        /// Initializes the sort control.
        /// </summary>
        private void InitializeSortControl()
        {
            cbSorting.Items.Clear();
            cbSorting.Items.AddRange(EnumExtensions.GetDescriptions<SkillSort>()
                .Where(description => !string.IsNullOrWhiteSpace((string)description))
                .ToArray());

            // Skill Planner or Data Browser (associated character)
            if ((m_plan != null) || (m_character != null))
                return;

            cbSorting.Items.Remove(SkillSort.TimeToNextLevel.GetDescription());
            cbSorting.Items.Remove(SkillSort.TimeToLevel5.GetDescription());
            cbSorting.Items.Remove(SkillSort.SPPerHour.GetDescription());
        }

        /// <summary>
        /// Initiliazes the selected indexes.
        /// </summary>
        private void InitiliazeSelectedIndexes()
        {
            if (Settings.UI.UseStoredSearchFilters)
            {
                SkillBrowserSettings settings;

                // Skill Planner
                if (m_plan != null)
                    settings = Settings.UI.SkillBrowser;
                // Character associated Data Browser
                else if (m_character != null)
                    settings = Settings.UI.SkillCharacterDataBrowser;
                // Data Browser
                else
                    settings = Settings.UI.SkillDataBrowser;

                cbShowNonPublic.Checked = settings.ShowNonPublicSkills;

                cbSkillFilter.SelectedItem =
                    cbSkillFilter.Items.Contains(settings.Filter.GetDescription())
                        ? settings.Filter.GetDescription()
                        : SkillFilter.All.GetDescription();

                cbFilterByAttributes.SelectedIndex = settings.FilterByAttributesIndex;
                cbSorting.SelectedItem =
                    cbSorting.Items.Contains(settings.Sort.GetDescription())
                        ? settings.Sort.GetDescription()
                        : SkillSort.None.GetDescription();

                tbSearchText.Text = settings.TextSearch;
                lbSearchTextHint.Visible = string.IsNullOrEmpty(tbSearchText.Text);

                return;
            }

            cbShowNonPublic.Checked = false;
            cbSkillFilter.SelectedItem = SkillFilter.All.GetDescription();
            cbFilterByAttributes.SelectedIndex = 0;
            cbSorting.SelectedItem = SkillSort.None.GetDescription();
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
            Disposed -= OnDisposed;
            EveMonClient.SettingsChanged -= EveMonClient_SettingsChanged;
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
        /// Called whenever the selection changes,
        /// fires the approriate event.
        /// </summary>
        private void OnSelectionChanged()
        {
            if (HostedInSkillBrowser)
            {
                SelectedSkillChanged?.ThreadSafeInvoke(this, new EventArgs());
                return;
            }

            // Set the selected skill in plan editor's skill selector
            PlanWindow planWindow = ParentForm as PlanWindow;
            planWindow?.SetSkillBrowserSkillSelectorSelectedSkill(m_selectedSkill);
        }

        /// <summary>
        /// Updates the control visibility.
        /// </summary>
        private void UpdateControlVisibility()
        {
            pbSearchImage.Visible = !Settings.UI.SafeForWork;
            UpdateContent();
        }

        /// <summary>
        /// Updates the item from the provided selection.
        /// </summary>
        /// <param name="selection">The selection</param>
        private void UpdateSelection(object selection)
        {
            if (!m_blockSelectionReentrancy)
                SelectedSkill = selection as Skill;
        }

        /// <summary>
        /// Updates the search text hint visibility.
        /// </summary>
        private void UpdateSearchTextHintVisibility()
        {
            lbSearchTextHint.Visible = !tbSearchText.Focused && string.IsNullOrEmpty(tbSearchText.Text);
        }

        /// <summary>
        /// Gets the attributes combinations.
        /// </summary>
        /// <returns></returns>
        /// <remarks> 
        /// This complex LINQ expression ensures the automatic catch of all present attributes combinations 
        /// and the ones that CCP may introduce in the future
        /// </remarks>
        private static IEnumerable<object> GetAttributesCombinations()
            => EnumExtensions.GetValues<EveAttribute>()
                .Where(attribute => attribute != EveAttribute.None)
                .OrderBy(attribute => attribute)
                .SelectMany(primaryAttribute => StaticSkills.AllSkills
                    .Where(staticSkill => staticSkill.PrimaryAttribute == primaryAttribute)
                    .Select(staticSkill => staticSkill.SecondaryAttribute)
                    .Distinct()
                    .OrderBy(secondaryAttribute => secondaryAttribute)
                    .Select(secondaryAttribute => $"{primaryAttribute} - {secondaryAttribute}"));

        #endregion


        #region Icon set

        /// <summary>
        /// Gets the icon set for the given index, using the given list for missing icons.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private ImageList GetIconSet(int index)
        {
            string groupname = string.Empty;

            if (index > 0 && index < IconSettings.Default.Properties.Count)
            {
                SettingsProperty settingsProperty = IconSettings.Default.Properties["Group" + index];
                if (settingsProperty != null)
                    groupname = settingsProperty.DefaultValue.ToString();
            }

            string groupDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}Resources\\Skill_Select\\Group";
            string defaultResourcesPath = $"{groupDirectory}0\\Default.resx";
            string groupResourcesPath = $"{groupDirectory}{index}\\{groupname}.resx";

            if (!File.Exists(defaultResourcesPath) ||
                (!string.IsNullOrEmpty(groupname) && !File.Exists(groupResourcesPath)))
            {
                groupname = string.Empty;
            }

            return string.IsNullOrEmpty(groupname) ? ilSkillIcons : GetIconSet(defaultResourcesPath, groupResourcesPath);
        }

        /// <summary>
        /// Gets the icon set for the given index, using the given list for missing icons.
        /// </summary>
        /// <param name="defaultResourcesPath">The default resources path.</param>
        /// <param name="groupResourcesPath">The group resources path.</param>
        /// <returns></returns>
        private static ImageList GetIconSet(string defaultResourcesPath, string groupResourcesPath)
        {
            ImageList imageList;
            ImageList tempImageList = null;
            try
            {
                tempImageList = new ImageList();
                IDictionaryEnumerator basicx;
                IResourceReader defaultGroupReader = null;
                tempImageList.ColorDepth = ColorDepth.Depth32Bit;

                try
                {
                    defaultGroupReader = new ResourceReader(defaultResourcesPath);

                    basicx = defaultGroupReader.GetEnumerator();

                    while (basicx.MoveNext())
                    {
                        tempImageList.Images.Add(basicx.Key.ToString(), (Icon)basicx.Value);
                    }
                }
                finally
                {
                    defaultGroupReader?.Close();
                }

                IResourceReader groupReader = null;
                try
                {
                    groupReader = new ResourceReader(groupResourcesPath);

                    basicx = groupReader.GetEnumerator();

                    while (basicx.MoveNext())
                    {
                        if (tempImageList.Images.ContainsKey(basicx.Key.ToString()))
                            tempImageList.Images.RemoveByKey(basicx.Key.ToString());

                        tempImageList.Images.Add(basicx.Key.ToString(), (Icon)basicx.Value);
                    }
                }
                finally
                {
                    groupReader?.Close();
                }

                imageList = tempImageList;
                tempImageList = null;
            }
            finally
            {
                tempImageList?.Dispose();
            }

            return imageList;
        }

        #endregion


        #region Content creation and update

        /// <summary>
        /// Updates the skills list content.
        /// </summary>
        internal void UpdateContent()
        {
            IList<Skill> skills = GetFilteredData().ToList();

            tvItems.Hide();
            lbSearchList.Hide();
            lvSortedSkillList.Hide();
            lbNoMatches.Hide();

            // Nothing to display ?
            if (!skills.Any())
            {
                lbNoMatches.Show();
                UpdateSelection(null);
                return;
            }

            // Is it sorted ?
            if (cbSorting.SelectedIndex > 0)
            {
                lvSortedSkillList.Show();
                UpdateListView(skills);
                return;
            }

            // Not sorted but there is a text filter
            if (!string.IsNullOrEmpty(tbSearchText.Text))
            {
                lbSearchList.Show();
                UpdateListBox(skills);
                return;
            }

            // Regular display, the tree
            tvItems.Show();
            UpdateTree(skills);
        }

        /// <summary>
        /// Gets the filtered skills.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Skill> GetFilteredData()
        {
            IEnumerable<Skill> skills = m_character?.Skills ?? SkillCollection.Skills;

            // Non-public skills
            if (!cbShowNonPublic.Checked)
                skills = skills.Where(skill => skill.IsPublic);

            // Filter
            Func<Skill, bool> predicate = GetFilter();
            skills = skills.Where(predicate);

            // Text search
            if (!string.IsNullOrEmpty(tbSearchText.Text))
            {
                skills = skills
                    .Where(skill => skill.Name.Contains(tbSearchText.Text, ignoreCase: true)
                                    || skill.Description.Contains(tbSearchText.Text, ignoreCase: true));
            }

            // When sorting by "time to...", remove lv5 skills
            if ((string)cbSorting.SelectedItem == SkillSort.TimeToLevel5.GetDescription() || 
                (string)cbSorting.SelectedItem == SkillSort.TimeToNextLevel.GetDescription())
            {
                skills = skills.Where(skill => skill.Level < 5);
            }

            return skills;
        }

        /// <summary>
        /// Gets the skill predicate for the selected combo filter index.
        /// </summary>
        /// <returns></returns>
        private Func<Skill, bool> GetFilter()
        {
            var skillFilter = (SkillFilter)(EnumExtensions.GetValueFromDescription<
                SkillFilter>((string)cbSkillFilter.SelectedItem) ?? SkillFilter.All);

            lblFilterBy.Enabled = cbFilterByAttributes.Enabled = (skillFilter ==
                SkillFilter.ByAttributes);

            EveAttribute primary = EveAttribute.None;
            EveAttribute secondary = EveAttribute.None;
            if (cbFilterByAttributes.Enabled)
            {
                string[] attributes = cbFilterByAttributes.SelectedItem.ToString().Split('-');
                Enum.TryParse(attributes.First().Trim(), out primary);
                Enum.TryParse(attributes.Last().Trim(), out secondary);
            }

            switch (skillFilter)
            {
                default:
                    return x => true;
                case SkillFilter.ByAttributes:
                    return x => x.PrimaryAttribute == primary && x.SecondaryAttribute == secondary;
                case SkillFilter.Known:
                    return x => x.IsKnown;
                case SkillFilter.AlphaFriendly:
                    return x => x.StaticData.AlphaFriendly;
                case SkillFilter.Unknown:
                    return x => !x.IsKnown;
                case SkillFilter.UnknownAndNotOwned:
                    return x => !x.IsKnown && !x.IsOwned;
                case SkillFilter.UnknownButOwned:
                    return x => !x.IsKnown && x.IsOwned;
                case SkillFilter.UnknownButTrainable:
                    return x => !x.IsKnown && x.ArePrerequisitesMet;
                case SkillFilter.UnknownAndNotTrainable:
                    return x => !x.IsKnown && !x.ArePrerequisitesMet;
                case SkillFilter.Planned:
                    return x => m_plan.IsPlanned(x);
                case SkillFilter.Lv1Ready:
                    return x => x.Level == 0 && x.ArePrerequisitesMet && !x.IsPartiallyTrained;
                case SkillFilter.Trainable:
                    return x => x.ArePrerequisitesMet && x.Level < 5;
                case SkillFilter.PartiallyTrained:
                    return x => x.IsPartiallyTrained;
                case SkillFilter.NotPlanned:
                    return x => !(m_plan.IsPlanned(x) || x.Level == 5);
                case SkillFilter.NotPlannedButTrainable:
                    return x => !m_plan.IsPlanned(x) && x.ArePrerequisitesMet && x.Level < 5;
                case SkillFilter.NoLv5:
                    return x => x.Level < 5;
            }
        }

        /// <summary>
        /// Updates the skills tree.
        /// </summary>
        private void UpdateTree(IEnumerable<Skill> skills)
        {
            // Store the selected node (if any) to restore it after the update
            int selectedItemHash = tvItems.SelectedNode?.Tag?.GetHashCode() ?? 0;

            // Update the image list choice
            int iconGroupIndex = Settings.UI.SkillBrowser.IconsGroupIndex;
            if (iconGroupIndex == 0)
                iconGroupIndex = 1;

            // Special case when displaying the planner as EVE data browser
            tvItems.ImageList = m_character == null && !Settings.UI.SafeForWork
                ? GetIconSet(0)
                : GetIconSet(iconGroupIndex);

            // Rebuild the nodes
            int numberOfItems = 0;
            tvItems.BeginUpdate();
            try
            {
                tvItems.Nodes.Clear();

                numberOfItems = AddNodes(skills, numberOfItems);

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
        /// Adds the nodes.
        /// </summary>
        /// <param name="skills">The skills.</param>
        /// <param name="numberOfItems">The number of items.</param>
        /// <returns></returns>
        private int AddNodes(IEnumerable<Skill> skills, int numberOfItems)
        {
            string key = Settings.UI.SafeForWork
                ? "book_old"
                : m_character == null
                    ? "Skills"
                    : "book";

            int groupImageIndex =  tvItems.ImageList.Images.IndexOfKey(key);

            // When we display the skill browser as EVE data browser
            int dataBrowserSkillImageIndex = Settings.UI.SafeForWork
                ? groupImageIndex
                : tvItems.ImageList.Images.IndexOfKey("Skill");

            foreach (IGrouping<SkillGroup, Skill> group in skills.GroupBy(x => x.Group).OrderBy(x => x.Key.Name))
            {
                TreeNode groupNode = new TreeNode
                {
                    Text = group.Key.Name,
                    ImageIndex = groupImageIndex,
                    SelectedImageIndex = groupImageIndex,
                    Tag = group.Key
                };

                // Add nodes for skills in this group
                foreach (Skill skill in group)
                {
                    int imageIndex;
                    if (m_character != null)
                    {
                        // Choose image index
                        if (skill.Level != 0)
                            imageIndex = tvItems.ImageList.Images.IndexOfKey("lvl" + skill.Level);
                        else if (skill.IsKnown)
                            imageIndex = tvItems.ImageList.Images.IndexOfKey("lvl0");
                        else if (skill.IsOwned)
                            imageIndex = tvItems.ImageList.Images.IndexOfKey("book");
                        else if (skill.ArePrerequisitesMet)
                            imageIndex = tvItems.ImageList.Images.IndexOfKey("PrereqsMet");
                        else
                            imageIndex = tvItems.ImageList.Images.IndexOfKey("PrereqsNOTMet");
                    }
                    else
                        imageIndex = dataBrowserSkillImageIndex;
                    
                    // Create node and adds it
                    TreeNode node = new TreeNode
                    {
                        Text = $"{skill.Name} ({skill.Rank})",
                        ImageIndex = imageIndex,
                        SelectedImageIndex = imageIndex,
                        Tag = skill
                    };

                    // We color some nodes
                    if (!skill.IsPublic && Settings.UI.SkillBrowser.ShowNonPublicSkills)
                        node.ForeColor = Color.DarkRed;

                    if (skill.IsPartiallyTrained && Settings.UI.PlanWindow.HighlightPartialSkills)
                        node.ForeColor = Color.Green;

                    if (skill.IsQueued && !skill.IsTraining && Settings.UI.PlanWindow.HighlightQueuedSkills)
                        node.ForeColor = Color.RoyalBlue;

                    if (skill.IsTraining)
                    {
                        node.BackColor = Color.LightSteelBlue;
                        node.ForeColor = Color.Black;
                    }

                    groupNode.Nodes.Add(node);
                    numberOfItems++;
                }

                // Add group when not empty
                tvItems.Nodes.Add(groupNode);
            }
            return numberOfItems;
        }

        /// <summary>
        /// Updates the list box displayed when there is a text filter and no sort criteria.
        /// </summary>
        /// <param name="skills"></param>
        private void UpdateListBox(IEnumerable<Skill> skills)
        {
            // Store the selected node (if any) to restore it after the update
            int selectedItemHash = tvItems.SelectedNode?.Tag?.GetHashCode() ?? 0;

            lbSearchList.BeginUpdate();
            try
            {
                lbSearchList.Items.Clear();
                foreach (Skill skill in skills)
                {
                    lbSearchList.Items.Add(skill);

                    // Restore the selected node (if any)
                    if (selectedItemHash > 0 && skill.GetHashCode() == selectedItemHash)
                        lbSearchList.SelectedItem = skill;
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
        /// <param name="skills"></param>
        private void UpdateListView(IList<Skill> skills)
        {
            // Store the selected node (if any) to restore it after the update
            int selectedItemHash = tvItems.SelectedNode?.Tag?.GetHashCode() ?? 0;

            // Retrieve the data to fetch into the list
            IEnumerable<string> labels = null;
            string column = GetSortedListData(ref skills, ref labels);
            if (labels == null)
                return;

            // Update the listview
            lvSortedSkillList.BeginUpdate();
            try
            {
                lvSortedSkillList.Items.Clear();

                using (IEnumerator<string> labelsEnumerator = labels.GetEnumerator())
                {
                    foreach (Skill skill in skills)
                    {
                        // Retrieves the label for the second column (sort key)
                        labelsEnumerator.MoveNext();
                        string label = labelsEnumerator.Current;

                        // Creates the item and adds it
                        ListViewItem lvi = new ListViewItem(skill.Name);
                        lvi.SubItems.Add(label);
                        lvi.Tag = skill;

                        lvSortedSkillList.Items.Add(lvi);

                        // Restore the selected node (if any)
                        if (selectedItemHash > 0 && skill.GetHashCode() == selectedItemHash)
                            lvi.Selected = true;
                    }
                }

                // Auto adjust column widths
                chSortKey.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                chName.Width = lvSortedSkillList.ClientSize.Width - chSortKey.Width;
                chSortKey.Text = column;
            }
            finally
            {
                lvSortedSkillList.EndUpdate();
            }
        }

        /// <summary>
        /// Gets the data for the sorted list view.
        /// </summary>
        /// <param name="skills"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        private string GetSortedListData(ref IList<Skill> skills, ref IEnumerable<string> labels)
        {
            SkillSort skillSort =
                (SkillSort)
                    (EnumExtensions.GetValueFromDescription<SkillSort>((string)cbSorting.SelectedItem) ??
                     SkillSort.None);

            switch (skillSort)
            {
                // Sort by name, default, occurs on initialization
                default:
                    return string.Empty;

                // Time to next level
                case SkillSort.TimeToNextLevel:
                    IEnumerable<TimeSpan> times = skills
                        .Select(x => m_character.GetTrainingTimeToMultipleSkills(x.Prerequisites)
                            .Add(x.GetLeftTrainingTimeToNextLevel));

                    TimeSpan[] timesArray = times.ToArray();
                    Skill[] skillsArray = skills.ToArray();
                    Array.Sort(timesArray, skillsArray);

                    string[] labelsArray = new string[skillsArray.Length];
                    for (int i = 0; i < labelsArray.Length; i++)
                    {
                        TimeSpan time = timesArray[i];
                        labelsArray[i] = time == TimeSpan.Zero
                            ? "-"
                            : $"{Skill.GetRomanFromInt(skillsArray[i].Level + 1)}: " +
                              $"{time.ToDescriptiveText(DescriptiveTextOptions.None)}";
                    }

                    skills = skillsArray;
                    labels = labelsArray;

                    return "Time";

                // Time to level 5
                case SkillSort.TimeToLevel5:
                    times = skills.
                        Select(x => m_character.GetTrainingTimeToMultipleSkills(x.Prerequisites)
                        .Add(x.GetLeftTrainingTimeToLevel(5)));

                    timesArray = times.ToArray();
                    skillsArray = skills.ToArray();
                    Array.Sort(timesArray, skillsArray);

                    labelsArray = new string[skillsArray.Length];
                    for (int i = 0; i < labelsArray.Length; i++)
                    {
                        TimeSpan time = timesArray[i];
                        labelsArray[i] = time == TimeSpan.Zero
                            ? "-"
                            : time.ToDescriptiveText(DescriptiveTextOptions.None);
                    }

                    skills = skillsArray;
                    labels = labelsArray;

                    return "Time to Max Level";

                // Skill rank
                case SkillSort.Rank:
                    skills = skills.OrderBy(x => x.Rank).ToList();
                    labels = skills.Select(x => x.Rank.ToString(CultureConstants.DefaultCulture));
                    return "Rank";

                // Skill SP/hour
                case SkillSort.SPPerHour:
                    skills = skills.OrderByDescending(x => x.SkillPointsPerHour).ToList();
                    labels = skills.Select(x => x.SkillPointsPerHour.ToString(CultureConstants.DefaultCulture));
                    return "SP/hour";
            }
        }

        #endregion


        #region Control Events

        /// <summary>
        /// When the user click the text search hint, he actually wants to select the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSearchTextHint_Click(object sender, EventArgs e)
        {
            tbSearchText.Focus();
        }

        /// <summary>
        /// When the user begins to write in the text box, we don't want to bother him with the text search hint.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_Enter(object sender, EventArgs e)
        {
            lbSearchTextHint.Visible = false;
        }

        /// <summary>
        /// When the user leaves the text box, we display the text search hint if the box is empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_Leave(object sender, EventArgs e)
        {
            UpdateSearchTextHintVisibility();
        }

        /// <summary>
        /// When the search text box changes, we update the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();

            SkillBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.SkillBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.SkillCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.SkillDataBrowser;

            settings.TextSearch = tbSearchText.Text;
        }

        /// <summary>
        /// When the selection of the listbox changes, updates the control's selection and fires the event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSearchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelection(lbSearchList.SelectedItem);
        }

        /// <summary>
        /// When the selection of the tree changes, updates the control's selection and fires the event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvSkillList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateSelection(e.Node?.Tag);
        }

        /// <summary>
        /// When the selection of the listview changes, updates the control's selection and fires the event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvSortedSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelection(lvSortedSkillList.SelectedItems.Cast<ListViewItem>().FirstOrDefault()?.Tag);
        }

        /// <summary>
        /// When the "show non public" checkbox is checked, we update settings and the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShowNonPublic_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();

            SkillBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.SkillBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.SkillCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.SkillDataBrowser;

            settings.ShowNonPublicSkills = cbShowNonPublic.Checked;
        }

        /// <summary>
        /// When the sorting combo box changes, we update settings and the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();
            lvSortedSkillList.Focus();

            SkillBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.SkillBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.SkillCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.SkillDataBrowser;

            settings.Sort =
                    (SkillSort)
                        (EnumExtensions.GetValueFromDescription<SkillSort>((string)cbSorting.SelectedItem) ??
                         SkillSort.None);
        }

        /// <summary>
        /// When the filter by attributes combo box changes, we update settings and the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFilterByAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();

            SkillBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.SkillBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.SkillBrowser;
            // Data Browser
            else
                settings = Settings.UI.SkillDataBrowser;

            settings.FilterByAttributesIndex = cbFilterByAttributes.SelectedIndex;
        }

        /// <summary>
        /// When the filter combo box changes, we update the settings and the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSkillFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_init)
                return;

            UpdateContent();

            SkillBrowserSettings settings;

            // Skill Planner
            if (m_plan != null)
                settings = Settings.UI.SkillBrowser;
            // Character associated Data Browser
            else if (m_character != null)
                settings = Settings.UI.SkillCharacterDataBrowser;
            // Data Browser
            else
                settings = Settings.UI.SkillDataBrowser;

            settings.Filter =
                (SkillFilter)
                    (EnumExtensions.GetValueFromDescription<SkillFilter>((string)cbSkillFilter.SelectedItem) ??
                     SkillFilter.All);
        }

        /// <summary>
        /// When the user clicks the search text box, we select the whole text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // (Ctrl + A) has KeyChar value 1
            if (e.KeyChar != (char)Keys.LButton)
                return;

            tbSearchText.SelectAll();
            e.Handled = true;
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

            lbSearchList.Cursor = lbSearchList.IndexFromPoint(e.Location) > -1
                ? CustomCursors.ContextMenu
                : Cursors.Default;
        }

        /// <summary>
        /// When the mouse gets pressed, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void lvSortedSkillList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            lvSortedSkillList.Cursor = Cursors.Default;
        }

        /// <summary>
        /// When the mouse moves over the list, we change the cursor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void lvSortedSkillList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            lvSortedSkillList.Cursor = lvSortedSkillList.GetItemAt(e.X, e.Y) != null
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
        /// When the user begins dragging a skill from the treeview, we start a drag'n drop operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvItems_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = tvItems.SelectedNode;

            if (node?.Nodes.Count != 0)
                return;

            Skill skill = node.Tag as Skill;
            if (skill == null || m_plan == null || m_plan.GetPlannedLevel(skill) == 5 || skill.Level == 5)
                return;

            DoDragDrop(node, DragDropEffects.Move);
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


        #region Context menu

        /// <summary>
        /// When the tree's context menu opens, we update the submenus' statuses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmSkills_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip contextMenu = sender as ContextMenuStrip;

            e.Cancel = contextMenu?.SourceControl == null || (!contextMenu.SourceControl.Visible && m_selectedSkill == null);

            if (e.Cancel || contextMenu?.SourceControl == null)
                return;

            contextMenu.SourceControl.Cursor = Cursors.Default;

            Skill skill = null;
            TreeNode node = tvItems.SelectedNode;
            if (node != null)
                skill = node.Tag as Skill;

            if (m_selectedSkill == null && skill != null)
                node = null;

            cmiPlanToLevel.Visible = planToMenuSkillSeparator.Visible = m_plan != null && m_selectedSkill != null;

            // "Show in skill browser/explorer"
            showInSkillExplorerMenu.Visible = m_selectedSkill != null;
            showInSkillBrowserMenu.Visible = m_selectedSkill != null && !HostedInSkillBrowser;

            // "Collapse" and "Expand" menus
            cmiCollapseSelected.Visible = m_selectedSkill == null && node != null && node.IsExpanded;
            cmiExpandSelected.Visible = m_selectedSkill == null && node != null && !node.IsExpanded;

            cmiExpandSelected.Text = m_selectedSkill == null && node != null && !node.IsExpanded
                ? $"Expand \"{node.Text}\""
                : string.Empty;
            cmiCollapseSelected.Text = m_selectedSkill == null && node != null && node.IsExpanded
                ? $"Collapse \"{node.Text}\""
                : string.Empty;

            expandCollapseSeparator.Visible = node != null;

            // "Expand All" and "Collapse All" menus
            cmiCollapseAll.Enabled = cmiCollapseAll.Visible = m_allExpanded;
            cmiExpandAll.Enabled = cmiExpandAll.Visible = !cmiCollapseAll.Enabled;

            // "Plan to N" menus
            if (m_selectedSkill == null || m_plan == null)
                return;

            cmiPlanToLevel.Enabled = false;
            for (int i = 0; i <= 5; i++)
            {
                cmiPlanToLevel.Enabled |= m_plan.UpdatesRegularPlanToMenu(cmiPlanToLevel.DropDownItems[i], m_selectedSkill, i);
            }
        }

        /// <summary>
        /// When the list's context menu opens, we update the menus status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmListSkills_Opening(object sender, CancelEventArgs e)
        {
            cmiLvPlanTo.Visible = planToMenuSkillListSeparator.Visible = m_plan != null && m_selectedSkill != null;

            // "Show in skill browser"
            showInSkillBrowserListMenu.Visible = !HostedInSkillBrowser;

            // "Plan to N" menus
            if (m_selectedSkill == null || m_plan == null)
                return;

            for (int i = 0; i <= 5; i++)
            {
                m_plan.UpdatesRegularPlanToMenu(cmiLvPlanTo.DropDownItems[i], m_selectedSkill, i);
            }
        }

        /// <summary>
        /// Listview, listbox, and tree view contexts menu > Plan to > Plan to N.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planToLevelMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem levelItem = (ToolStripMenuItem)sender;
            IPlanOperation operation = levelItem.Tag as IPlanOperation;
            if (operation == null)
                return;

            PlanWindow planWindow = ParentForm as PlanWindow;
            if (planWindow == null)
                return;

            PlanHelper.SelectPerform(new PlanToOperationWindow(operation), planWindow, operation);
        }

        /// <summary>
        /// Treeview's context menu > Collapse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCollapseSelected_Click(object sender, EventArgs e)
        {
            tvItems.SelectedNode.Collapse();
        }

        /// <summary>
        /// Treeview's context menu > Expand.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiExpandSelected_Click(object sender, EventArgs e)
        {
            tvItems.SelectedNode.Expand();
        }

        /// <summary>
        /// Treeview's context menu > Expand all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiExpandAll_Click(object sender, EventArgs e)
        {
            tvItems.ExpandAll();
            m_allExpanded = true;
        }

        /// <summary>
        /// Treeview's context menu > Collapse all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCollapseAll_Click(object sender, EventArgs e)
        {
            tvItems.CollapseAll();
            m_allExpanded = false;
        }

        /// <summary>
        /// Context menu > Show In Skill Browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showInSkillBrowserMenu_Click(object sender, EventArgs e)
        {
            // Open the skill browser
            PlanWindow.ShowPlanWindow(m_character, m_plan).ShowSkillInBrowser(m_selectedSkill);
        }

        /// <summary>
        /// Context menu > Show In Skill Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showInSkillExplorerMenu_Click(object sender, EventArgs e)
        {
            // Open the skill explorer
            SkillExplorerWindow.ShowSkillExplorerWindow(m_character, m_plan).ShowSkillInExplorer(m_selectedSkill);
        }

        #endregion
    }
}
