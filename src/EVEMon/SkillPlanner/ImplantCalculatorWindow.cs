using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// This controls allows the user to see how its plan's comutation times would change wth different implants.
    /// </summary>
    public partial class ImplantCalculatorWindow : EVEMonForm, IPlanOrderPluggable
    {
        private readonly PlanEditorControl m_planEditor;

        private Character m_character;
        private Plan m_plan;

        private bool m_init;


        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="ImplantCalculatorWindow"/> class from being created.
        /// Default constructor for designer.
        /// </summary>
        private ImplantCalculatorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplantCalculatorWindow"/> class.
        /// Constructor used in WindowsFactory.
        /// </summary>
        /// <param name="planEditor">The plan editor.</param>
        public ImplantCalculatorWindow(PlanEditorControl planEditor)
            : this()
        {
            m_planEditor = planEditor;
            Plan = planEditor.Plan;

            planEditor.ShowWithPluggable(this);
        }

        #endregion


        #region Inherited Events

        /// <summary>
        /// On load, update the controls states.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set the min and max values of the NumericUpDown controls
            // based on character attributes value
            foreach (NumericUpDown nud in AtrributesPanel.Controls.OfType<NumericUpDown>())
            {
                string tag = (nud.Tag as string) ?? string.Empty;
                int attribIndex;
                if (tag.TryParseInv(out attribIndex))
                {
                    var attrib = (EveAttribute)attribIndex;
                    var charAttribute = m_character[attrib];
                    long min = charAttribute.EffectiveValue - charAttribute.ImplantBonus;
                    nud.Minimum = min;
                    nud.Maximum = min + Math.Max(m_plan.ChosenImplantSet[attrib].Bonus,
                        EveConstants.MaxImplantPoints);
                }
            }

            EveMonClient.PlanNameChanged += EveMonClient_PlanNameChanged;

            m_init = true;
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


        #region Internal Properties

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        internal Plan Plan
        {
            get { return m_plan; }
            set
            {
                if (m_plan == value)
                    return;

                m_plan = value;
                m_character = (Character)m_plan.Character;

                // Used by WindowsFactory.GetByTag
                //Tag = m_plan;

                // Update the title
                UpdateTitle();

                // Update the content
                UpdateContentAsync().ConfigureAwait(true);
            }
        }

        #endregion


        #region Update Methods

        /// <summary>
        /// Update all the numeric boxes
        /// </summary>
        private async Task UpdateContentAsync()
        {
            gbAttributes.Text = $"Attributes of \"{m_plan.ChosenImplantSet.Name}\"";

            CharacterScratchpad characterScratchpad = m_plan.Character.After(m_plan.ChosenImplantSet);

            nudCharisma.Value = characterScratchpad.Charisma.EffectiveValue;
            nudWillpower.Value = characterScratchpad.Willpower.EffectiveValue;
            nudIntelligence.Value = characterScratchpad.Intelligence.EffectiveValue;
            nudPerception.Value = characterScratchpad.Perception.EffectiveValue;
            nudMemory.Value = characterScratchpad.Memory.EffectiveValue;

            // If the implant set isn't the active one we notify the user
            lblNotice.Visible = m_plan.ChosenImplantSet != m_character.ImplantSets.Current;

            //  Update all the times on the right pane
            await UpdateTimesAsync();
        }

        /// <summary>
        /// Updates the labels on the right of the numeric box.
        /// </summary>
        /// <param name="attrib"></param>
        /// <param name="myValue"></param>
        /// <param name="lblAdjust"></param>
        /// <param name="lblEffectiveAttribute"></param>
        private void UpdateAttributeLabels(EveAttribute attrib, int myValue, Control lblAdjust, Control lblEffectiveAttribute)
        {
            CharacterScratchpad characterScratchpad = m_plan.Character.After(m_plan.ChosenImplantSet);

            long baseAttr = characterScratchpad[attrib].EffectiveValue - characterScratchpad[attrib].ImplantBonus;
            long adjust = myValue - baseAttr;

            lblAdjust.ForeColor = adjust >= 0 ? SystemColors.ControlText : Color.Red;
            lblAdjust.Text = $"{(adjust >= 0 ? "+" : string.Empty)}{adjust}";
            lblEffectiveAttribute.Text = characterScratchpad[attrib].EffectiveValue.ToNumericString(0);
        }

        /// <summary>
        /// Update all the times on the right pane (base time, best time, etc).
        /// </summary>
        private async Task UpdateTimesAsync()
        {
            // Current (with implants)
            TimeSpan currentSpan = await UpdateTimesForCharacter(m_character.After(m_plan.ChosenImplantSet));

            // Current (without implants)
            ImplantSet noneImplantSet = m_character.ImplantSets.None;
            TimeSpan baseSpan = await UpdateTimesForCharacter(m_character.After(noneImplantSet));

            // This
            CharacterScratchpad scratchpad = CreateModifiedScratchpad(m_character.After(m_plan.ChosenImplantSet));
            TimeSpan thisSpan = await UpdateTimesForCharacter(scratchpad);

            lblCurrentSpan.Text = currentSpan.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);
            lblCurrentDate.Text = DateTime.Now.Add(currentSpan).ToString(CultureConstants.DefaultCulture);
            lblBaseSpan.Text = baseSpan.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);
            lblBaseDate.Text = DateTime.Now.Add(baseSpan).ToString(CultureConstants.DefaultCulture);
            lblThisSpan.Text = thisSpan.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);
            lblThisDate.Text = DateTime.Now.Add(thisSpan).ToString(CultureConstants.DefaultCulture);

            // Are the new attributes better than current (without implants) ?
            lblComparedToBase.ForeColor = thisSpan > baseSpan
                ? Color.Red
                : thisSpan < baseSpan ? Color.Green : SystemColors.ControlText;
            lblComparedToBase.Text = thisSpan > baseSpan
                ? $"{thisSpan.Subtract(baseSpan).ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} slower than current base"
                : thisSpan < baseSpan
                    ? $"{baseSpan.Subtract(thisSpan).ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} faster than current base"
                    : @"No time difference than current base";

            // Are the new attributes better than current (with implants) ?
            lblComparedToCurrent.ForeColor = thisSpan > currentSpan
                ? Color.DarkRed
                : thisSpan < currentSpan ? Color.DarkGreen : SystemColors.ControlText;
            lblComparedToCurrent.Text = thisSpan > currentSpan
                ? $"{thisSpan.Subtract(currentSpan).ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} slower than current"
                : thisSpan < currentSpan
                    ? $"{currentSpan.Subtract(thisSpan).ToDescriptiveText(DescriptiveTextOptions.IncludeCommas)} faster than current"
                    : @"No time difference than current base";

            // Update the plan's pluggable column
            m_planEditor?.ShowWithPluggable(this);
        }

        /// <summary>
        /// Update the times labels from the given character
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private Task<TimeSpan> UpdateTimesForCharacter(BaseCharacter character)
            => TaskHelper.RunCPUBoundTaskAsync(() => character.GetTrainingTimeToMultipleSkills(m_plan));

        #endregion


        #region Controls events

        /// <summary>
        /// When the intelligence numeric box changed, we update the attributes labels and the times on the right pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void nudIntelligence_ValueChanged(object sender, EventArgs e)
        {
            UpdateAttributeLabels(EveAttribute.Intelligence, (int)nudIntelligence.Value,
                lblAdjustIntelligence, lblEffectiveIntelligence);

            if (!m_init)
                return;

            //  Update all the times on the right pane
            await UpdateTimesAsync();
        }

        /// <summary>
        /// When the charisma numeric box changed, we update the attributes labels and the times on the right pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void nudCharisma_ValueChanged(object sender, EventArgs e)
        {
            UpdateAttributeLabels(EveAttribute.Charisma, (int)nudCharisma.Value,
                lblAdjustCharisma, lblEffectiveCharisma);

            if (!m_init)
                return;

            //  Update all the times on the right pane
            await UpdateTimesAsync();
        }

        /// <summary>
        /// When the perception numeric box changed, we update the attributes labels and the times on the right pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void nudPerception_ValueChanged(object sender, EventArgs e)
        {
            UpdateAttributeLabels(EveAttribute.Perception, (int)nudPerception.Value,
                lblAdjustPerception, lblEffectivePerception);

            if (!m_init)
                return;

            //  Update all the times on the right pane
            await UpdateTimesAsync();
        }

        /// <summary>
        /// When the memory numeric box changed, we update the attributes labels and the times on the right pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void nudMemory_ValueChanged(object sender, EventArgs e)
        {
            UpdateAttributeLabels(EveAttribute.Memory, (int)nudMemory.Value,
                lblAdjustMemory, lblEffectiveMemory);

            if (!m_init)
                return;

            //  Update all the times on the right pane
            await UpdateTimesAsync();
        }

        /// <summary>
        /// When the willpower numeric box changed, we update the attributes labels and the times on the right pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void nudWillpower_ValueChanged(object sender, EventArgs e)
        {
            UpdateAttributeLabels(EveAttribute.Willpower, (int)nudWillpower.Value,
                lblAdjustWillpower, lblEffectiveWillpower);

            if (!m_init)
                return;

            //  Update all the times on the right pane
            await UpdateTimesAsync();
        }

        /// <summary>
        /// Creates a scratchpad with the new implants values.
        /// </summary>
        /// <returns></returns>
        private CharacterScratchpad CreateModifiedScratchpad(BaseCharacter character)
        {
            // Creates a scratchpad with new implants
            CharacterScratchpad scratchpad = new CharacterScratchpad(character);

            scratchpad.Memory.ImplantBonus += (int)nudMemory.Value - character.Memory.EffectiveValue;
            scratchpad.Charisma.ImplantBonus += (int)nudCharisma.Value - character.Charisma.EffectiveValue;
            scratchpad.Intelligence.ImplantBonus += (int)nudIntelligence.Value - character.Intelligence.EffectiveValue;
            scratchpad.Perception.ImplantBonus += (int)nudPerception.Value - character.Perception.EffectiveValue;
            scratchpad.Willpower.ImplantBonus += (int)nudWillpower.Value - character.Willpower.EffectiveValue;

            return scratchpad;
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

            areRemappingPointsActive = true;

            CharacterScratchpad scratchpad = CreateModifiedScratchpad(m_character.After(m_plan.ChosenImplantSet));
            plan.UpdateStatistics(scratchpad, true, true);
            plan.UpdateOldTrainingTimes();
        }

        /// <summary>
        /// Updates the times when "choose implant set" changes.
        /// </summary>
        public Task UpdateOnImplantSetChange() => UpdateContentAsync();

        /// <summary>
        /// Updates the title.
        /// </summary>
        private void UpdateTitle()
        {
            Text = $"{m_character.Name} [{m_plan.Name}] - Implant Calculator";
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
    }
}
