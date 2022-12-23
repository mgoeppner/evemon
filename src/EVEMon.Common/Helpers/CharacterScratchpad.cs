using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EVEMon.Common.Collections;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Factories;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;

namespace EVEMon.Common.Helpers
{
    /// <summary>
    /// Represents a character class designed for computations and temporary modifications (new implants, remaps, etc)
    /// </summary>
    public sealed class CharacterScratchpad : BaseCharacter
    {
        private readonly CharacterAttributeScratchpad[] m_attributes = new CharacterAttributeScratchpad[5];
        private readonly long[] m_skillLevels;
        private readonly long[] m_skillSP;

        private readonly BaseCharacter m_character;
        private long m_skillPoints;

        /// <summary>
        /// Constructor from a character.
        /// </summary>
        /// <param name="character"></param>
        public CharacterScratchpad(BaseCharacter character)
        {
            TrainedSkills = new Collection<StaticSkillLevel>();
            TrainingTime = TimeSpan.Zero;
            m_character = character;
            m_skillSP = new long[StaticSkills.ArrayIndicesCount];
            m_skillLevels = new long[StaticSkills.ArrayIndicesCount];
            m_cloneStateSetting = character.AccountStatusSettings;

            for (int i = 0; i < m_attributes.Length; i++)
            {
                m_attributes[i] = new CharacterAttributeScratchpad((EveAttribute)i);
            }

            Reset();
        }

        /// <summary>
        /// Gets Alpha/Omega status for this character.
        /// </summary>
        public override AccountStatus CharacterStatus
        {
            get
            {
                // Note that EffectiveCharacterStatus will always return CharacterStatus,
                // because the AccountStatusMode will always be "Auto"
                if (m_character != null)
                {
                    return m_character.CharacterStatus;
                }
                return base.CharacterStatus;
            }
            protected set
            {
                base.CharacterStatus = value;
            }
        }

        #region Attributes

        /// <summary>
        /// Gets the intelligence of the character.
        /// </summary> 
        public new CharacterAttributeScratchpad Intelligence => m_attributes[(int)EveAttribute.Intelligence];

        /// <summary>
        /// Gets the charisma of the character.
        /// </summary> 
        public new CharacterAttributeScratchpad Charisma => m_attributes[(int)EveAttribute.Charisma];

        /// <summary>
        /// Gets the perception of the character.
        /// </summary> 
        public new CharacterAttributeScratchpad Perception => m_attributes[(int)EveAttribute.Perception];

        /// <summary>
        /// Gets the memory of the character.
        /// </summary> 
        public new CharacterAttributeScratchpad Memory => m_attributes[(int)EveAttribute.Memory];

        /// <summary>
        /// Gets the willpower of the character.
        /// </summary> 
        public new CharacterAttributeScratchpad Willpower => m_attributes[(int)EveAttribute.Willpower];

        /// <summary>
        /// Gets the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute to retrieve</param>
        /// <returns></returns>
        public new CharacterAttributeScratchpad this[EveAttribute attribute] => m_attributes[(int)attribute];

        /// <summary>
        /// Performs the given remapping
        /// </summary>
        /// <param name="point"></param>
        /// <exception cref="System.ArgumentNullException">point</exception>
        public void Remap(RemappingPoint point)
        {
            point.ThrowIfNull(nameof(point));

            for (int i = 0; i < m_attributes.Length; i++)
            {
                EveAttribute attrib = (EveAttribute)i;
                m_attributes[i].Base = point[attrib];
            }
        }

        /// <summary>
        /// Remove all the implants
        /// </summary>
        public void ClearImplants()
        {
            foreach (CharacterAttributeScratchpad attribute in m_attributes)
            {
                attribute.ImplantBonus = 0;
            }
        }

        #endregion


        #region Overriden methods

        /// <summary>
        /// Gets the total skill points.
        /// </summary>
        /// <returns></returns>
        protected override long TotalSkillPoints => m_skillPoints;

        /// <summary>
        /// Gets the current level of the given skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">skill</exception>
        public override long GetSkillLevel(StaticSkill skill)
        {
            skill.ThrowIfNull(nameof(skill));

            return m_skillLevels[skill.ArrayIndex];
        }

        /// <summary>
        /// Gets the current level of the given skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">skill</exception>
        public override long GetSkillPoints(StaticSkill skill)
        {
            skill.ThrowIfNull(nameof(skill));

            return m_skillSP[skill.ArrayIndex];
        }

        /// <summary>
        /// Gets the requested attribute.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        protected override ICharacterAttribute GetAttribute(EveAttribute attribute) => m_attributes[(int)attribute];

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        internal override void Dispose()
        {
            m_character.Dispose();
        }

        #endregion


        #region Training and skill levels updates

        /// <summary>
        /// Gets or sets the total training time. Note the training time is always zero when you create a scratchpad from a character.
        /// </summary>
        public TimeSpan TrainingTime { get; private set; }

        /// <summary>
        /// Gets the list of skills trained so far (by the <see cref="Train&lt;T&gt;"/> or <see cref="SetSkillLevel"/> methods).
        /// </summary>
        public Collection<StaticSkillLevel> TrainedSkills { get; }

        /// <summary>
        /// Clears the training time and trained skills only. 
        /// Does not remove the benefits from those skills (use <see cref="Reset()"/> for that purpose).
        /// </summary>
        public void ClearTraining()
        {
            TrainingTime = TimeSpan.Zero;
            TrainedSkills.Clear();
        }

        /// <summary>
        /// Performs the given training.
        /// Rely on <see cref="SetSkillLevel"/> but only applied when the given level is greater than the current one.
        /// </summary>
        /// <param name="trainings"></param>
        /// <exception cref="System.ArgumentNullException">trainings</exception>
        public void Train<T>(IEnumerable<T> trainings)
            where T : ISkillLevel
        {
            trainings.ThrowIfNull(nameof(trainings));

            foreach (T item in trainings)
            {
                Train(item.Skill, item.Level);
            }
        }

        /// <summary>
        /// Performs the given training, also apply remapping points.
        /// Rely on <see cref="SetSkillLevel"/> but only applied when the given level is greater than the current one.
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="applyRemappingPoints"></param>
        /// <exception cref="System.ArgumentNullException">entries</exception>
        public void TrainEntries(IEnumerable<PlanEntry> entries, bool applyRemappingPoints)
        {
            entries.ThrowIfNull(nameof(entries));

            foreach (PlanEntry entry in entries)
            {
                if (entry.Remapping != null && entry.Remapping.Status == RemappingPointStatus.UpToDate &&
                    applyRemappingPoints)
                    Remap(entry.Remapping);

                Train(entry.Skill, entry.Level);
            }
        }

        /// <summary>
        /// Performs the given training.
        /// Same as <see cref="SetSkillLevel"/> but only applied when the given level is greater than the current one.
        /// </summary>
        /// <param name="training"></param>
        /// <exception cref="System.ArgumentNullException">training</exception>
        public void Train(ISkillLevel training)
        {
            training.ThrowIfNull(nameof(training));

            Train(training.Skill, training.Level);
        }

        /// <summary>
        /// Performs the given training. Same as <see cref="SetSkillLevel"/> but only applied when the given level is greater than the current one.
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="level"></param>
        public void Train(StaticSkill skill, long level)
        {
            SetSkillLevel(skill, level, LearningOptions.UpgradeOnly);
        }

        /// <summary>
        /// Changes the level of the provided skill, updating the results.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <param name="level">The level.</param>
        /// <param name="options">The options.</param>
        private void SetSkillLevel(StaticSkill skill, long level, LearningOptions options = LearningOptions.None)
        {
            int index = skill.ArrayIndex;

            if (index > m_skillLevels.Length)
                return;

            // May quit for if this level is alread equal (or greater, depending on the options)
            if ((options & LearningOptions.UpgradeOnly) != LearningOptions.None)
            {
                if (m_skillLevels[index] >= level)
                    return;
            }
            else
            {
                if (m_skillLevels[index] == level)
                    return;
            }

            // Update prerequisites
            if ((options & LearningOptions.IgnorePrereqs) == LearningOptions.None)
            {
                // Deal with recursive prereqs (like Polaris)
                foreach (StaticSkillLevel prereq in skill.Prerequisites.Where(prereq => prereq.Skill != skill))
                {
                    // Set the prereq's level
                    SetSkillLevel(prereq.Skill, prereq.Level, options | LearningOptions.UpgradeOnly);
                }
            }

            // Update training time
            if ((options & LearningOptions.IgnoreTraining) == LearningOptions.None)
            {
                TrainingTime += GetTrainingTime(skill, level);
                TrainedSkills.Add(new StaticSkillLevel(skill, level));
            }

            // Update skillpoints
            if ((options & LearningOptions.FreezeSP) == LearningOptions.None)
                UpdateSP(skill, level);

            // Updates the skill level
            m_skillLevels[index] = level;
        }

        /// <summary>
        /// Updates the total SP count for this character
        /// </summary>
        /// <param name="staticSkill"></param>
        /// <param name="level"></param>
        private void UpdateSP(StaticSkill staticSkill, long level)
        {
            long targetSP = staticSkill.GetPointsRequiredForLevel(level);
            long difference = targetSP - m_skillSP[staticSkill.ArrayIndex];

            m_skillSP[staticSkill.ArrayIndex] = targetSP;
            m_skillPoints += difference;
        }

        #endregion


        #region Cloning, reseting, temporary changes

        /// <summary>
        /// Clear all the skills.
        /// </summary>
        public void ClearSkills()
        {
            for (int i = 0; i < m_skillSP.Length; i++)
            {
                m_skillLevels[i] = 0;
            }

            for (int i = 0; i < m_skillLevels.Length; i++)
            {
                m_skillLevels[i] = 0;
            }

            m_skillPoints = 0;
            TrainingTime = TimeSpan.Zero;

            foreach (CharacterAttributeScratchpad attribute in m_attributes)
            {
                attribute.UpdateEffectiveAttribute();
            }
        }

        /// <summary>
        /// Returns a clone of this scratchpad.
        /// </summary>
        /// <returns></returns>
        public CharacterScratchpad Clone() => new CharacterScratchpad(this);

        /// <summary>
        /// Resets the scratchpad from the <see cref="BaseCharacter"/> it was built upon.
        /// </summary>
        public void Reset()
        {
            CharacterScratchpad character = m_character as CharacterScratchpad;
            if (character != null)
                Reset(character);
            else
                ResetFromCharacter();
        }

        /// <summary>
        /// Resets this scratchpad using the provided scratchpad.
        /// </summary>
        /// <param name="scratchpad"></param>
        private void Reset(CharacterScratchpad scratchpad)
        {
            m_skillPoints = scratchpad.m_skillPoints;
            TrainingTime = scratchpad.TrainingTime;

            TrainedSkills.Clear();
            TrainedSkills.AddRange(scratchpad.TrainedSkills);

            for (int i = 0; i < m_attributes.Length; i++)
            {
                m_attributes[i].Reset(scratchpad.m_attributes[i]);
            }

            scratchpad.m_skillSP.CopyTo(m_skillSP, 0);
            scratchpad.m_skillLevels.CopyTo(m_skillLevels, 0);
        }

        /// <summary>
        /// Resets the scratchpad from the <see cref="BaseCharacter"/> it was built upon.
        /// </summary>
        private void ResetFromCharacter()
        {
            TrainingTime = TimeSpan.Zero;
            TrainedSkills.Clear();

            // Initialize attributes-related stuff
            for (int i = 0; i < m_attributes.Length; i++)
            {
                ICharacterAttribute attrib = m_character[(EveAttribute)i];
                m_attributes[i].Reset(attrib.Base, attrib.ImplantBonus, attrib.BoosterBonus);
            }

            // Initialize skills
            m_skillPoints = 0;
            foreach (StaticSkill skill in StaticSkills.AllSkills)
            {
                long sp = m_character.GetSkillPoints(skill);
                long level = m_character.GetSkillLevel(skill);

                m_skillPoints += sp;
                m_skillSP[skill.ArrayIndex] = sp;
                m_skillLevels[skill.ArrayIndex] = level;
            }
        }

        /// <summary>
        /// Allow to store the state of this scratchpad to restore it later.
        /// The method returns an <see cref="IDisposable"/> object which, once disposed, will restore the state.
        /// </summary>
        /// <remarks>Use it in a <c>using</c> block to enforce an automatic restoration of the object even when exceptions are throw.</remarks>
        /// <returns>A disposable object which, once disposed, will restore the state of the </returns>
        public IDisposable BeginTemporaryChanges()
        {
            CharacterScratchpad clone = Clone();
            return new DisposableWithCallback(() => Reset(clone));
        }

        #endregion
    }
}
