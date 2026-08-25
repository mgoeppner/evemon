using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEMon.Common;
using EVEMon.Common.Controls;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Threading;

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// Allows to wiew and change attribute remappings.
    /// </summary>
    public partial class AttributesOptimizerWindow : EVEMonForm, IPlanOrderPluggable
    {

        #region Fields

        private readonly Dictionary<AttributesOptimizerControl, RemappingResult>
            m_remappingDictionary = new Dictionary<AttributesOptimizerControl, RemappingResult>();

        private readonly PlanEditorControl m_planEditor;
        private readonly BaseCharacter m_baseCharacter;
        private readonly Character m_character;
        private readonly AttributeOptimizationStrategy m_strategy;
        private readonly BasePlan m_plan;

        private string m_description;

        private CharacterScratchpad m_statisticsScratchpad;
        private bool m_areRemappingPointsActive;

        // Variables for manual edition of a plan
        private RemappingPoint m_manuallyEditedRemappingPoint;
        private RemappingResult m_remapping;

        #endregion


        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="AttributesOptimizerWindow"/> class from being created.
        /// Default constructor for designer.
        /// </summary>
        private AttributesOptimizerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributesOptimizerWindow"/> class.
        /// Constructor used in WindowsFactory.
        /// </summary>
        /// <param name="planEditorControl">The plan editor control.</param>
        /// <param name="strategy">Optimization strategy</param>
        /// <exception cref="System.ArgumentNullException">planEditorControl</exception>
        public AttributesOptimizerWindow(PlanEditorControl planEditorControl, AttributeOptimizationStrategy strategy)
            : this()
        {
            planEditorControl.ThrowIfNull(nameof(planEditorControl));

            m_planEditor = planEditorControl;
            m_plan = planEditorControl.Plan;
            m_character = (Character)m_plan.Character;
            m_baseCharacter = m_character.After(m_plan.ChosenImplantSet);
            m_strategy = strategy;

            // Update title and description
            UpdateTitle();
        }

        /// <summary>
        /// Constructor for use in code when the user wants to manually edit a remapping point.
        /// </summary>
        /// <param name="planEditorControl">The plan editor control.</param>
        /// <param name="point">The point.</param>
        public AttributesOptimizerWindow(PlanEditorControl planEditorControl, RemappingPoint point)
            : this(planEditorControl, AttributeOptimizationStrategy.ManualRemappingPointEdition)
        {
            m_manuallyEditedRemappingPoint = point;
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load, restores the window rectangle from the settings.
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            throbber.State = ThrobberState.Rotating;

            EveMonClient.PlanNameChanged += EveMonClient_PlanNameChanged;

            await Task.Run(() => Run());
        }
        
        /// <summary>
        /// On closing, we unsubscribe the global events to help the GC.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            EveMonClient.PlanNameChanged -= EveMonClient_PlanNameChanged;
        }

        #endregion


        #region Update Methods

        /// <summary>
        /// Updates the title.
        /// </summary>
        private void UpdateTitle()
        {
            Text = @"Attributes Optimizer";

            switch (m_strategy)
            {
                case AttributeOptimizationStrategy.RemappingPoints:
                    m_description = $"Based on {m_plan.Name}; using the remapping points you defined.";
                    Text += $" ({m_plan.Name}, remapping points)";
                    break;
                case AttributeOptimizationStrategy.OneYearPlan:
                    m_description = $"Based on {m_plan.Name}; best attributes for the first year.";
                    Text += $" ({m_plan.Name}, first year)";
                    break;
                case AttributeOptimizationStrategy.Character:
                    m_description = $"Based on {m_character.Name}" +
                                    $"{(m_character.Name.EndsWith("s", StringComparison.CurrentCulture) ? "'" : "'s")} skills";
                    Text += $" ({m_character.Name})";
                    break;
                case AttributeOptimizationStrategy.ManualRemappingPointEdition:
                    m_description = "Manual editing of a remapping point";
                    Text += $" ({m_plan.Name}, remapping point edit)";
                    break;
            }

            IEnumerable<Label> labelDescriptions = tabControl.TabPages.Cast<TabPage>()
                .Where(tabPage => tabPage != tabSummary)
                .SelectMany(tabPage => tabPage.Controls.OfType<AttributesOptimizerControl>())
                .Concat(Controls.OfType<AttributesOptimizerControl>())
                .Select(optimizerControl => optimizerControl.Controls.OfType<Label>()
                    .FirstOrDefault(label => label.Name == "labelDescription"))
                .Where(labelDescription => labelDescription != null);

            // Update the description in controls
            foreach (Label labelDescription in labelDescriptions)
            {
                labelDescription.Text = m_description;
            }
        }

        /// <summary>
        /// Starts optimization.
        /// </summary>
        /// <param name="update">if set to <c>true</c> [update].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Run(bool update = false)
        {
            // Compute best scratchpad
            RemappingResult remapping = null;
            ICollection<RemappingResult> remappingList = null;

            switch (m_strategy)
            {
                case AttributeOptimizationStrategy.ManualRemappingPointEdition:
                    m_areRemappingPointsActive = true;
                    if (update)
                    {
                        remapping = m_remapping;
                        m_manuallyEditedRemappingPoint = remapping.Point.Clone();
                    }
                    else
                    {
                        remapping = AttributesOptimizer.GetResultsFromRemappingPoints(m_plan).Single(
                            x => x.Point == m_manuallyEditedRemappingPoint);
                        m_manuallyEditedRemappingPoint = m_manuallyEditedRemappingPoint.Clone();
                        m_remapping = remapping;
                    }
                    remapping.Optimize(TimeSpan.MaxValue);
                    break;
                case AttributeOptimizationStrategy.Character:
                    m_areRemappingPointsActive = false;
                    remapping = AttributesOptimizer.OptimizeFromCharacter(m_character, m_plan);
                    break;
                case AttributeOptimizationStrategy.OneYearPlan:
                    m_areRemappingPointsActive = false;
                    remapping = AttributesOptimizer.OptimizeFromFirstYearOfPlan(m_plan);
                    break;
                case AttributeOptimizationStrategy.RemappingPoints:
                    m_areRemappingPointsActive = true;
                    remappingList = AttributesOptimizer.OptimizeFromPlanAndRemappingPoints(m_plan);
                    break;
                default:
                    throw new NotImplementedException();
            }

            // Update the controls for every attribute
            Dispatcher.Invoke(() => UpdateForm(remapping, remappingList));
        }

        /// <summary>
        /// Updates controls on the form.
        /// </summary>
        /// <param name="remapping">An <see cref="RemappingResult"/> object</param>
        /// <param name="remappingList">List of remappings</param>
        private void UpdateForm(RemappingResult remapping, ICollection<RemappingResult> remappingList)
        {
            // Update the attributes
            if (remapping != null)
            {
                m_statisticsScratchpad = remapping.BestScratchpad.Clone();
                UpdateForRemapping(remapping);
            }
            else
                UpdateForRemappingList(remappingList);

            // Update the plan order's column
            if ((remapping != null) || (remappingList.Count != 0))
                m_planEditor.ShowWithPluggable(this);

            // Hide the throbber and the waiting message
            panelWait.Hide();
        }

        /// <summary>
        /// Updates the UI once the computation has been done (for whole plan or character from birth)
        /// </summary>
        /// <param name="remapping"></param>
        private void UpdateForRemapping(RemappingResult remapping)
        {          
            // Create control
            AttributesOptimizerControl ctrl = CreateAttributesOptimizationControl(remapping);
            Controls.Add(ctrl);

            IList<AttributesOptimizerControl> optControls = Controls.OfType<AttributesOptimizerControl>().ToList();

            if (optControls.Count == 1)
                return;

            Controls.RemoveAt(Controls.IndexOf(optControls.First()));
        }

        /// <summary>
        /// Updates the UI once the computations with remapping points strategy have been done.
        /// </summary>
        /// <param name="remappingList">The remapping list.</param>
        private void UpdateForRemappingList(ICollection<RemappingResult> remappingList)
        {
            // Display "no result" or "summary"
            if (remappingList.Count == 0)
            {
                panelNoResult.Show();
                return;
            }

            tabControl.Controls.Clear();

            // Adds a tab page for the summary
            tabControl.Controls.Add(tabSummary);

            // Updates the summary informations
            UpdateSummaryInformation(remappingList);

            // Adds a tab page for every remapping
            int index = 1;
            foreach (RemappingResult remap in remappingList)
            {
                AddTabPage(remap, "#" + index++);
            }

            tabControl.Show();
            tabSummary.Focus();
        }

        /// <summary>
        /// Updates information in summary page.
        /// </summary>
        /// <param name="remappingList">List of remappings</param>
        private void UpdateSummaryInformation(ICollection<RemappingResult> remappingList)
        {
            TimeSpan baseDuration = m_plan.GetTotalTime(m_character.After(m_plan.ChosenImplantSet), false);
            lvPoints.Items.Clear();

            // Add global informations
            ListViewGroup globalGroup = new ListViewGroup("Global informations");
            lvPoints.Groups.Add(globalGroup);

            TimeSpan savedTime = remappingList.Aggregate(TimeSpan.Zero,
                                                         (current, remap) =>
                                                         current.Add(remap.BaseDuration.Subtract(remap.BestDuration)));

            lvPoints.Items.Add(new ListViewItem(
                $"Current time : {baseDuration.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)}", globalGroup));

            if (savedTime != TimeSpan.Zero)
            {
                lvPoints.Items.Add(
                    new ListViewItem(
                        $"Optimized time : {baseDuration.Subtract(savedTime).ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)}",
                        globalGroup));

                if (savedTime < TimeSpan.Zero)
                {
                    ListViewItem savedTimeItem = lvPoints.Items.Add(
                        new ListViewItem(
                            $"{(-savedTime).ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} slower than current.",
                                         globalGroup));
                    savedTimeItem.ForeColor = Color.DarkRed;
                }
                else
                {
                    ListViewItem savedTimeItem = lvPoints.Items.Add(
                        new ListViewItem(
                            $"{savedTime.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} better than current.",
                                         globalGroup));
                    savedTimeItem.ForeColor = Color.DarkGreen;
                }
            }
            else
                lvPoints.Items.Add(new ListViewItem("Your attributes are already optimal.", globalGroup));

            // Notify plan updated
            ListViewItem lvi = new ListViewItem("Your plan has been updated.", globalGroup)
                                   { Font = new Font(lvPoints.Font, FontStyle.Bold) };
            lvPoints.Items.Add(lvi);

            // Add pages and summary informations
            TimeSpan lastRemap = TimeSpan.Zero;
            foreach (RemappingResult remap in remappingList)
            {
                AddSummaryForRemapping(remap, ref lastRemap);
            }

            columnHeader.Width = lvPoints.ClientSize.Width;
        }

        /// <summary>
        /// Adds summary information for given remapping.
        /// </summary>
        /// <param name="remap">Remapping object</param>
        /// <param name="lastRemap">Time of previous remapping</param>
        private void AddSummaryForRemapping(RemappingResult remap, ref TimeSpan lastRemap)
        {
            // Create the group
            string text = $"{remap} at {remap.StartTime.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)}";
            ListViewGroup group = new ListViewGroup(text);
            lvPoints.Groups.Add(group);

            // Check there are at least one year between each remap
            TimeSpan timeSinceLastRemap = remap.StartTime.Subtract(lastRemap);
            if (timeSinceLastRemap < TimeSpan.FromDays(365) && remap.StartTime != TimeSpan.Zero)
            {
                ListViewItem item = lvPoints.Items.Add(
                    new ListViewItem(
                        $"The previous remap point was only {timeSinceLastRemap.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} ago.",
                        group));
                item.ForeColor = Color.DarkRed;
            }

            lastRemap = remap.StartTime;

            // Add five items, one for each attribute
            AddItemForAttribute(remap, group, EveAttribute.Intelligence);
            AddItemForAttribute(remap, group, EveAttribute.Perception);
            AddItemForAttribute(remap, group, EveAttribute.Charisma);
            AddItemForAttribute(remap, group, EveAttribute.Willpower);
            AddItemForAttribute(remap, group, EveAttribute.Memory);
        }

        /// <summary>
        /// Adds the list item for the given attribute.
        /// </summary>
        /// <param name="remap"></param>
        /// <param name="group"></param>
        /// <param name="attrib"></param>
        private void AddItemForAttribute(RemappingResult remap, ListViewGroup group,
                                         EveAttribute attrib)
        {
            // Add the list view item for this attribute
            string itemText = RemappingPoint.GetStringForAttribute(attrib, remap.BaseScratchpad, remap.BestScratchpad);
            lvPoints.Items.Add(new ListViewItem(itemText, group));
        }

        /// <summary>
        /// Adds the tab page for the given remapping
        /// </summary>
        /// <param name="remapping">The remapping.</param>
        /// <param name="tabName">Name of the tab.</param>
        private void AddTabPage(RemappingResult remapping, string tabName)
        {
            AttributesOptimizerControl ctl = CreateAttributesOptimizationControl(remapping);

            if (ctl == null)
                return;

            m_remappingDictionary[ctl] = remapping;

            TabPage tempPage = null;
            try
            {
                tempPage = new TabPage(tabName);
                tempPage.Controls.Add(ctl);

                TabPage page = tempPage;
                tempPage = null;

                tabControl.TabPages.Add(page);
            }
            finally
            {
                tempPage?.Dispose();
            }
        }

        /// <summary>
        /// Creates a <see cref="AttributesOptimizerControl"/> for a given remapping.
        /// </summary>
        /// <param name="remapping">The remapping object to represents.</param>
        /// <returns>The created control.</returns>
        private AttributesOptimizerControl CreateAttributesOptimizationControl(RemappingResult remapping)
        {
            AttributesOptimizerControl control;
            AttributesOptimizerControl ctl = null;
            try
            {
                ctl = new AttributesOptimizerControl(m_character, m_plan, remapping, m_description);
                ctl.AttributeChanged += AttributesOptimizationControl_AttributeChanged;

                // For a manually edited point, we initialize the control with the attributes from the current remapping point
                if (m_strategy == AttributeOptimizationStrategy.ManualRemappingPointEdition &&
                    m_manuallyEditedRemappingPoint.Status == RemappingPointStatus.UpToDate)
                    ctl.UpdateValuesFrom(m_manuallyEditedRemappingPoint);

                control = ctl;
                ctl = null;
            }
            finally
            {
                ctl?.Dispose();
            }

            return control;
        }

        #endregion


        #region Loacal Events

        /// <summary>
        /// Recalculating plan and summary page after change of a <see cref="AttributesOptimizerControl"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AttributeChangedEventArgs"/> instance containing the event data.</param>
        private void AttributesOptimizationControl_AttributeChanged(object sender, AttributeChangedEventArgs e)
        {
            AttributesOptimizerControl control = (AttributesOptimizerControl)sender;

            if (m_strategy == AttributeOptimizationStrategy.RemappingPoints)
            {
                m_remappingDictionary[control] = e.Remapping;
                UpdateSummaryInformation(m_remappingDictionary.Values);
            }

            m_statisticsScratchpad = e.Remapping.BestScratchpad.Clone();
            m_remapping = e.Remapping;

            // Update the plan order's column
            m_planEditor.ShowWithPluggable(this);
        }

        #endregion


        #region Global Events

        /// <summary>
        /// Occurs when a plan name changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PlanChangedEventArgs"/> instance containing the event data.</param>
        private void EveMonClient_PlanNameChanged(object sender, PlanChangedEventArgs e)
        {
            if (m_plan != e.Plan)
                return;

            UpdateTitle();
        }

        #endregion


        #region IPlanOrderPluggable Members

        /// <summary>
        /// Updates the statistics for the plan editor.
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="areRemappingPointsActive"></param>
        /// <exception cref="System.ArgumentNullException">plan</exception>
        public void UpdateStatistics(BasePlan plan, out bool areRemappingPointsActive)
        {
            plan.ThrowIfNull(nameof(plan));

            areRemappingPointsActive = m_areRemappingPointsActive;

            if (m_areRemappingPointsActive)
                plan.UpdateStatistics(new CharacterScratchpad(m_baseCharacter.After(plan.ChosenImplantSet)), true, true);
            else
                plan.UpdateStatistics(m_statisticsScratchpad.Clone(), false, true);

            plan.UpdateOldTrainingTimes(new CharacterScratchpad(m_baseCharacter.After(plan.ChosenImplantSet)), false,
                                        true);
        }

        /// <summary>
        /// Updates the times when "choose implant set" changes.
        /// </summary>
        public Task UpdateOnImplantSetChange()
        {
            panelWait.Show();

            return Task.Run(() => Run(update: true));
        }

        #endregion
    }
}