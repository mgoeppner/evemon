using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EVEMon.Common.Constants;
using EVEMon.Common.CustomEventArgs;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Helpers;
using EVEMon.Common.Models;

namespace EVEMon.SkillPlanner
{
    /// <summary>
    /// Control that shows attribute remapping and allows to adjust it.
    /// </summary>
    public partial class AttributesOptimizerControl : UserControl
    {
        private readonly Character m_character;
        private readonly BasePlan m_plan;
        private readonly RemappingResult m_remapping;
        private readonly string m_description;

        /// <summary>
        /// Occurs when attributes changes. 
        /// </summary>
        [Category("Behavior")]
        public event EventHandler<AttributeChangedEventArgs> AttributeChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="AttributesOptimizerControl"/>.
        /// </summary>
        /// <param name="character">Character information</param>
        /// <param name="plan">Skill plan</param>
        /// <param name="remapping">Optimized remapping</param>
        /// <param name="description"></param>
        public AttributesOptimizerControl(Character character, BasePlan plan,
                                             RemappingResult remapping, string description)
        {
            InitializeComponent();
            lbMEM.Font = FontFactory.GetFont("Tahoma");
            lbWIL.Font = FontFactory.GetFont("Tahoma");
            lbCHA.Font = FontFactory.GetFont("Tahoma");
            lbPER.Font = FontFactory.GetFont("Tahoma");
            lbINT.Font = FontFactory.GetFont("Tahoma");

            m_character = character;
            m_plan = plan;
            m_remapping = remapping;
            m_description = description;

            UpdateControls(m_character, m_plan, m_remapping, m_description);
        }

        /// <summary>
        /// On load, use the <see cref="FontFactory"/> to retrieve fonts.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lblUnassignedAttributePoints.Font = FontFactory.GetFont("Tahoma", 8.25F);
            lbWarning.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
            lblMemory.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
            lblWillpower.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
            lblCharisma.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
            lblPerception.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
            lblIntelligence.Font = FontFactory.GetFont("Microsoft Sans Serif", 8.25F);
        }

        /// <summary>
        /// Updates bars and labels with given attributes from remapping.
        /// </summary>
        /// <param name="character">Character information</param>
        /// <param name="plan">Skill plan</param>
        /// <param name="remapping">Remapping with attributes and training time</param>
        /// <param name="description"></param>
        private void UpdateControls(Character character, BasePlan plan, RemappingResult remapping,
                                    string description)
        {
            UpdateAttributeControls(remapping, EveAttribute.Perception, lbPER, pbPERRemappable, pbPERImplants);
            UpdateAttributeControls(remapping, EveAttribute.Willpower, lbWIL, pbWILRemappable, pbWILImplants);
            UpdateAttributeControls(remapping, EveAttribute.Memory, lbMEM, pbMEMRemappable, pbMEMImplants);
            UpdateAttributeControls(remapping, EveAttribute.Intelligence, lbINT, pbINTRemappable, pbINTImplants);
            UpdateAttributeControls(remapping, EveAttribute.Charisma, lbCHA, pbCHARemappable, pbCHAImplants);

            lblBooster.Text = String.Format("Active Booster: +{0}", character.Intelligence.BoosterBonus);

            // Update the description label
            labelDescription.Text = description;

            // Update the current time control
            lbCurrentTime.Text = remapping.BaseDuration.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);

            // Update the optimized time control
            lbOptimizedTime.Text = remapping.BestDuration.ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);

            // Update the time benefit control
            string bestDurationTimeText = remapping.BaseDuration.Subtract(remapping.BestDuration)
                .ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);
            string worseDurationTimeText = remapping.BestDuration.Subtract(remapping.BaseDuration)
                .ToDescriptiveText(DescriptiveTextOptions.IncludeCommas);

            lbGain.ForeColor = remapping.BestDuration < remapping.BaseDuration
                ? Color.DarkGreen
                : remapping.BaseDuration < remapping.BestDuration
                    ? Color.DarkRed
                    : Color.Black;
            lbGain.Text = remapping.BestDuration < remapping.BaseDuration
                ? $"{bestDurationTimeText} better than current"
                : remapping.BaseDuration < remapping.BestDuration
                    ? $"{worseDurationTimeText} slower than current"
                    : "Same as current";

            // A plan may not have a years worth of skills in it,
            // only fair to warn the user
            lbWarning.Visible = remapping.BestDuration < TimeSpan.FromDays(365);

            // Spare points
            long sparePoints = EveConstants.SpareAttributePointsOnRemap;
            for (int i = 0; i < 5; i++)
            {
                sparePoints -= remapping.BestScratchpad[(EveAttribute)i].Base - EveConstants.CharacterBaseAttributePoints;
            }
            pbUnassigned.Value = sparePoints;

            // If the implant set isn't the active one we notify the user
            lblNotice.Visible = plan.ChosenImplantSet != character.ImplantSets.Current;
        }

        /// <summary>
        /// Updates bars and labels for specified attribute.
        /// </summary>
        /// <param name="remapping">Attribute remapping</param>
        /// <param name="attrib">Attribute that will be used to update controls</param>
        /// <param name="label">Label control</param>
        /// <param name="pbRemappable">Attribute bar for remappable value</param>
        /// <param name="pbImplants">Attribute bar for implants</param>
        private static void UpdateAttributeControls(RemappingResult remapping,
                                             EveAttribute attrib,
                                             Control label,
                                             AttributeBarControl pbRemappable,
                                             AttributeBarControl pbImplants)
        {
            // Compute base and effective attributes
            long effectiveAttribute = remapping.BestScratchpad[attrib].EffectiveValue;
            long oldBaseAttribute = remapping.BaseScratchpad[attrib].Base;
            long remappableAttribute = remapping.BestScratchpad[attrib].Base;
            long implantsBonus = remapping.BestScratchpad[attrib].ImplantBonus;

            // Update the label
            label.Text = $"{effectiveAttribute} (new : {remappableAttribute} ; old : {oldBaseAttribute})";

            // Update the bars
            pbRemappable.Value = remappableAttribute - EveConstants.CharacterBaseAttributePoints;
            pbImplants.Value = implantsBonus;
        }

        /// <summary>
        /// Calculates new remapping from values of controls.
        /// </summary>
        private void Recalculate()
        {
            CharacterScratchpad scratchpad = m_remapping.BaseScratchpad.Clone();
            scratchpad.Memory.Base = pbMEMRemappable.Value + EveConstants.CharacterBaseAttributePoints;
            scratchpad.Charisma.Base = pbCHARemappable.Value + EveConstants.CharacterBaseAttributePoints;
            scratchpad.Willpower.Base = pbWILRemappable.Value + EveConstants.CharacterBaseAttributePoints;
            scratchpad.Perception.Base = pbPERRemappable.Value + EveConstants.CharacterBaseAttributePoints;
            scratchpad.Intelligence.Base = pbINTRemappable.Value + EveConstants.CharacterBaseAttributePoints;

            // Get remapping for provided attributes
            RemappingResult manualRemapping = new RemappingResult(m_remapping,
                                                                  scratchpad);
            manualRemapping.Update();
            UpdateControls(m_character, m_plan, manualRemapping, m_description);

            // Notify the changes
            AttributeChanged?.ThreadSafeInvoke(this, new AttributeChangedEventArgs(manualRemapping));
        }


        #region Events

        /// <summary>
        /// Change of any attribute must be adjusted if there is no enough free points in the pool.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AttributeValueChangingEventArgs"/> instance containing the event data.</param>
        private void pbRemappable_ValueChanging(object sender, AttributeValueChangingEventArgs e)
        {
            AttributeBarControl control = (AttributeBarControl)sender;

            // Adjust delta if there is no enough free points
            if (pbUnassigned.Value < e.Value)
                control.DeltaValue = pbUnassigned.Value;

            // Add/remove points from pool
            pbUnassigned.Value -= control.DeltaValue;
        }

        /// <summary>
        /// Recalculate the time after change of an attribute.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AttributeValueChangedEventArgs"/> instance containing the event data.</param>
        private void pb_ValueChanged(object sender, AttributeValueChangedEventArgs e)
        {
            Recalculate();
        }

        /// <summary>
        /// Correct highlight if selected cell is inaccessable.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AttributeHighlightingEventArgs"/> instance containing the event data.</param>
        private void pbRemappable_Highlighting(object sender, AttributeHighlightingEventArgs e)
        {
            AttributeBarControl control = (AttributeBarControl)sender;
            
            // Adjust possible highlight using free points in pool
            if (e.Value - control.Value > pbUnassigned.Value)
                control.HighlightedValue = control.Value + pbUnassigned.Value;
        }

        /// <summary>
        /// Reset to original optimized remapping.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOptimize_Click(object sender, EventArgs e)
        {
            // Updates the remapping point with the optimized remapping
            m_remapping.Update();

            // Set all labels and bars to calculated optimized remap
            UpdateControls(m_character, m_plan, m_remapping, m_description);

            // Fires the event
            AttributeChanged?.ThreadSafeInvoke(this, new AttributeChangedEventArgs(m_remapping));
        }

        /// <summary>
        /// Reset to remapping with current attributes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCurrent_Click(object sender, EventArgs e)
        {
            // Make unoptimized remap
            RemappingResult zeroRemapping = new RemappingResult(m_remapping, m_remapping.BaseScratchpad.Clone());
            zeroRemapping.Update();

            // Update the controls
            UpdateControls(m_character, m_plan, zeroRemapping, m_description);

            // Fires the event
            AttributeChanged?.ThreadSafeInvoke(this, new AttributeChangedEventArgs(zeroRemapping));
        }

        /// <summary>
        /// One of +/- buttons was pressed.
        /// Check is it possible to change requested attribute and do it if we can.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void attributeButton_Click(object sender, EventArgs e)
        {
            AttributeButtonControl button = sender as AttributeButtonControl;
            if (button?.AttributeBar == null)
                return;

            // Adjust delta
            long deltaValue = button.ValueChange;
            if (pbUnassigned.Value < deltaValue)
                deltaValue = pbUnassigned.Value;

            if (deltaValue < 0 && button.AttributeBar.Value <= button.AttributeBar.BaseValue)
                return;

            if (button.AttributeBar.Value + deltaValue < button.AttributeBar.BaseValue)
                deltaValue = Math.Max(button.AttributeBar.Value - button.AttributeBar.BaseValue, deltaValue);

            if (button.AttributeBar.Value + deltaValue > button.AttributeBar.MaxPoints)
                deltaValue = button.AttributeBar.MaxPoints - button.AttributeBar.Value;

            if (deltaValue == 0)
                return;

            button.AttributeBar.Value += deltaValue;
            pbUnassigned.Value -= deltaValue;
            Recalculate();
        }

        /// <summary>
        /// Updates the controls with the values from the current remapping point.
        /// </summary>
        /// <param name="point"></param>
        /// <exception cref="System.ArgumentNullException">point</exception>
        public void UpdateValuesFrom(RemappingPoint point)
        {
            point.ThrowIfNull(nameof(point));

            // Creates a scratchpad with the base values from the provided point.
            CharacterScratchpad scratchpad = new CharacterScratchpad(m_character.After(m_plan.ChosenImplantSet));
            for (int i = 0; i < 5; i++)
            {
                scratchpad[(EveAttribute)i].Base = point[(EveAttribute)i];
            }

            RemappingResult remapping = new RemappingResult(m_remapping, scratchpad);
            remapping.Update();

            // Update the controls
            UpdateControls(m_character, m_plan, remapping, m_description);

            // Fires the event
            AttributeChanged?.ThreadSafeInvoke(this, new AttributeChangedEventArgs(remapping));
        }

        #endregion
    }
}
