using System;
using System.Collections.Generic;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;

namespace EVEMon.Common.Models
{
    public abstract class BaseCharacter
    {
        private const long SKILL_INJECTOR_MULTIPLIER = 100000;
        private const long SKILL_INJECTOR_SMALL_DIVIDER = 5;

        /// <summary>
        /// Overrides the automatically determined clone state if set to Alpha or Omega.
        /// </summary>
        protected AccountStatusMode m_cloneStateSetting;

        public BaseCharacter()
        {
            CharacterStatus = AccountStatus.Unknown;
            m_cloneStateSetting = AccountStatusMode.Auto;
        }

        #region Abstract methods and properties

        protected abstract long TotalSkillPoints { get; }
        protected abstract ICharacterAttribute GetAttribute(EveAttribute attribute);

        internal abstract void Dispose();

        public abstract long GetSkillLevel(StaticSkill skill);
        public abstract long GetSkillPoints(StaticSkill skill);

        #endregion


        #region Account status

        /// <summary>
        /// Gets Alpha/Omega status for this character.
        /// </summary>
        public virtual AccountStatus CharacterStatus { get; protected set; }

        /// <summary>
        /// Retrieves whether this character is effectively Alpha, Omega, or unknown. If auto
        /// status is set (the default), EVEMon tries to determine this value from the known
        /// character information. Otherwise, the user override is used.
        /// </summary>
        public AccountStatus EffectiveCharacterStatus
        {
            get
            {
                AccountStatus cloneState;
                switch (AccountStatusSettings)
                {
                case AccountStatusMode.Alpha:
                    cloneState = AccountStatus.Alpha;
                    break;
                case AccountStatusMode.Omega:
                    cloneState = AccountStatus.Omega;
                    break;
                case AccountStatusMode.Auto:
                default:
                    cloneState = CharacterStatus;
                    break;
                }
                return cloneState;
            }
        }

        /// <summary>
        /// The method used to determine the character's clone state (or the override).
        /// </summary>
        public virtual AccountStatusMode AccountStatusSettings {
            get
            {
                return m_cloneStateSetting;
            }
            set
            {
                m_cloneStateSetting = value;
            }
        }

        #endregion


        #region Computation methods

        /// <summary>
        /// Gets the total skill points for this character.
        /// </summary>
        public long SkillPoints => TotalSkillPoints;

        /// <summary>
        /// Computes the SP per hour for the given skill, without factoring in the newbies bonus.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns>SP earned per hour.</returns>
        /// <exception cref="System.ArgumentNullException">skill</exception>
        public virtual float GetBaseSPPerHour(StaticSkill skill)
        {
            return GetOmegaSPPerHour(skill) * EffectiveCharacterStatus.GetTrainingRate();
        }

        /// <summary>
        /// Computes the SP per hour for the given skill, without factoring in the newbies bonus abd without boosters.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns>SP earned per hour.</returns>
        /// <exception cref="System.ArgumentNullException">skill</exception>
        public virtual float GetBaseSPPerHourWithoutBoosters(StaticSkill skill)
        {
            return GetOmegaSPPerHourWithoutBoosters(skill) * EffectiveCharacterStatus.GetTrainingRate();
        }

        /// <summary>
        /// Computes the SP per hour for the given skill for an Omega clone, without factoring
        /// in the newbies bonus.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns>SP earned per hour.</returns>
        /// <exception cref="System.ArgumentNullException">skill</exception>
        protected float GetOmegaSPPerHour(StaticSkill skill)
        {
            skill.ThrowIfNull(nameof(skill));

            if (skill.PrimaryAttribute == EveAttribute.None || skill.SecondaryAttribute == EveAttribute.None)
                return 1800.0f;

            float primAttr = GetAttribute(skill.PrimaryAttribute).EffectiveValue;
            float secondaryAttr = GetAttribute(skill.SecondaryAttribute).EffectiveValue;
            return primAttr * 60.0f + secondaryAttr * 30.0f;
        }

        /// <summary>
        /// Computes the SP per hour for the given skill for an Omega clone, without factoring
        /// in the newbies bonus and without boosters.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns>SP earned per hour.</returns>
        /// <exception cref="System.ArgumentNullException">skill</exception>
        protected float GetOmegaSPPerHourWithoutBoosters(StaticSkill skill)
        {
            skill.ThrowIfNull(nameof(skill));

            if (skill.PrimaryAttribute == EveAttribute.None || skill.SecondaryAttribute == EveAttribute.None)
                return 0.0f;

            var p = GetAttribute(skill.PrimaryAttribute);
            var s = GetAttribute(skill.SecondaryAttribute);

            float primAttr = p.EffectiveValue - s.BoosterBonus;
            float secondaryAttr = s.EffectiveValue - s.BoosterBonus;
            return primAttr * 60.0f + secondaryAttr * 30.0f;
        }

        /// <summary>
        /// Gets a character scratchpad representing this character after the provided skill levels trainings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trainings"></param>
        /// <returns></returns>
        public CharacterScratchpad After<T>(IEnumerable<T> trainings)
            where T : ISkillLevel
        {
            CharacterScratchpad scratchpad = new CharacterScratchpad(this);
            scratchpad.Train(trainings);
            return scratchpad;
        }

        /// <summary>
        /// Gets a character scratchpad representing this character after a switch to the provided implant set.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">set</exception>
        public CharacterScratchpad After(ImplantSet set)
        {
            set.ThrowIfNull(nameof(set));

            CharacterScratchpad scratchpad = new CharacterScratchpad(this);
            for (int i = 0; i < 5; i++)
            {
                EveAttribute attribute = (EveAttribute)i;
                scratchpad[attribute].ImplantBonus = set[attribute].Bonus;
            }
            return scratchpad;
        }

        /// <summary>
        /// Gets the time span for a specific number of skill points.
        /// </summary>
        /// <param name="points">The points to calculate points.</param>
        /// <param name="skill">The skill to train.</param>
        /// <returns></returns>
        public TimeSpan GetTimeSpanForPoints(StaticSkill skill, long points) 
            => GetTrainingTime(points, GetBaseSPPerHour(skill));

        /// <summary>
        /// Gets the time span for a specific number of skill points without boosters.
        /// </summary>
        /// <param name="points">The points to calculate points.</param>
        /// <param name="skill">The skill to train.</param>
        /// <returns></returns>
        public TimeSpan GetTimeSpanForPointsWithoutBoosters(StaticSkill skill, long level)
            => GetTrainingTimeWithoutBoosters(skill, level);

        /// <summary>
        /// Gets the required skill injectors for the specified skill points.
        /// </summary>
        /// <param name="skillPoints">The skill points.</param>
        /// <returns></returns>
        public SkillInjectorsRequired GetRequiredSkillInjectorsForSkillPoints(long skillPoints)
        {
            long remainingSkillPoints = skillPoints, targetSP = SkillPoints + skillPoints;
            int injectorsLarge = 0, injectorsSmall = 0;

            while (remainingSkillPoints > 0)
            {
                long projectedSkillPoints = targetSP - remainingSkillPoints;
                long nextInjector = GetSkillPointsGainedFromLargeInjector(projectedSkillPoints);
                // Can we use smalls instead?
                if (remainingSkillPoints < nextInjector)
                {
                    long nextSmallInjector = nextInjector / SKILL_INJECTOR_SMALL_DIVIDER;
                    remainingSkillPoints -= nextSmallInjector;
                    injectorsSmall++;
                }
                else
                {
                    remainingSkillPoints -= nextInjector;
                    injectorsLarge++;
                }
            }

            return new SkillInjectorsRequired(injectorsLarge, injectorsSmall);
        }

        /// <summary>
        /// Gets the skill points gained from a large injector.
        /// </summary>
        /// <param name="skillPoints">The start skill points.</param>
        /// <returns></returns>
        private static long GetSkillPointsGainedFromLargeInjector(long skillPoints)
        {
            double sp = skillPoints / 1000000d;
            if (sp < 5.0)
                return 5 * SKILL_INJECTOR_MULTIPLIER;
            if (sp < 50.0)
                return 4 * SKILL_INJECTOR_MULTIPLIER;
            if (sp < 80.0)
                return 3 * SKILL_INJECTOR_MULTIPLIER;
            return (long)(1.5 * SKILL_INJECTOR_MULTIPLIER);
        }
        
        #endregion


        #region GetSPToTrain

        /// <summary>
        /// Computes the number of SP to train.
        /// </summary>
        /// <param name="skillLevel">The skill level.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">skillLevel</exception>
        public long GetSPToTrain(ISkillLevel skillLevel)
        {
            skillLevel.ThrowIfNull(nameof(skillLevel));

            return GetSPToTrain(skillLevel.Skill, skillLevel.Level);
        }

        /// <summary>
        /// Computes the number of SP to train.
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="level"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        private long GetSPToTrain(StaticSkill skill, long level, TrainingOrigin origin = TrainingOrigin.FromCurrent)
        {
            if (level == 0)
                return 0;
            long sp = skill.GetPointsRequiredForLevel(level);

            // Deals with the origin
            long result;
            switch (origin)
            {
                // Include current SP
                case TrainingOrigin.FromCurrent:
                    result = sp - GetSkillPoints(skill);
                    break;

                // This level only (previous are known)
                case TrainingOrigin.FromPreviousLevel:
                    result = sp - skill.GetPointsRequiredForLevel(level - 1);
                    break;

                case TrainingOrigin.FromPreviousLevelOrCurrent:
                    result = sp - Math.Max(GetSkillPoints(skill), skill.GetPointsRequiredForLevel(level - 1));
                    break;

                // Include nothing
                default:
                    result = sp;
                    break;
            }

            // Returns result
            return result < 0 ? 0 : result;
        }

        #endregion


        #region GetTrainingTime & GetTrainingTimeToMultipleSkills

        /// <summary>
        /// Computes the training time for the given skill.
        /// </summary>
        /// <param name="skillLevel">The skill level.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">skillLevel</exception>
        public TimeSpan GetTrainingTime(ISkillLevel skillLevel)
        {
            skillLevel.ThrowIfNull(nameof(skillLevel));

            return GetTrainingTime(skillLevel.Skill, skillLevel.Level);
        }

        /// <summary>
        /// Computes the training time for the given skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="level"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public TimeSpan GetTrainingTime(StaticSkill skill, long level, TrainingOrigin origin = TrainingOrigin.FromCurrent)
        {
            float spPerHour = GetBaseSPPerHour(skill);
            long sp = GetSPToTrain(skill, level, origin);
            return GetTrainingTime(sp, spPerHour);
        }

        /// <summary>
        /// Computes the training time for the given skill without boosters.
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="level"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public TimeSpan GetTrainingTimeWithoutBoosters(StaticSkill skill, long level, TrainingOrigin origin = TrainingOrigin.FromCurrent)
        {
            float spPerHour = GetBaseSPPerHourWithoutBoosters(skill);
            long sp = GetSPToTrain(skill, level, origin);
            return GetTrainingTime(sp, spPerHour);
        }

        /// <summary>
        /// Gets the time to train the given SP at the provided speed.
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="spPerHour"></param>
        /// <returns></returns>
        internal static TimeSpan GetTrainingTime(long sp, float spPerHour)
            => Math.Abs(spPerHour) < float.Epsilon ? TimeSpan.FromDays(999.0) : TimeSpan.FromHours(sp / spPerHour);

        /// <summary>
        /// Gets the time require to train the given skills and their prerequisites.
        /// </summary>
        /// <param name="trainings">A sequence of pairs of skills and the target levels.</param>
        /// <returns></returns>
        public TimeSpan GetTrainingTimeToMultipleSkills(IEnumerable<ISkillLevel> trainings)
            => After(trainings).TrainingTime;

        #endregion


        #region ICharacter non-abstract explicit members

        /// <summary>
        /// Gets the <see cref="ICharacterAttribute"/> with the specified attribute.
        /// </summary>
        /// <value></value>
        public ICharacterAttribute this[EveAttribute attribute] => GetAttribute(attribute);

        /// <summary>
        /// Gets the intelligence.
        /// </summary>
        /// <value>The intelligence.</value>
        public ICharacterAttribute Intelligence => GetAttribute(EveAttribute.Intelligence);

        /// <summary>
        /// Gets the perception.
        /// </summary>
        /// <value>The perception.</value>
        public ICharacterAttribute Perception => GetAttribute(EveAttribute.Perception);

        /// <summary>
        /// Gets the willpower.
        /// </summary>
        /// <value>The willpower.</value>
        public ICharacterAttribute Willpower => GetAttribute(EveAttribute.Willpower);

        /// <summary>
        /// Gets the charisma.
        /// </summary>
        /// <value>The charisma.</value>
        public ICharacterAttribute Charisma => GetAttribute(EveAttribute.Charisma);

        /// <summary>
        /// Gets the memory.
        /// </summary>
        /// <value>The memory.</value>
        public ICharacterAttribute Memory => GetAttribute(EveAttribute.Memory);

        #endregion

        /// <summary>
        /// Reports how many skill injectors are required to receive the expected SP.
        /// </summary>
        public struct SkillInjectorsRequired
        {
            public int Large { get; }

            public int Small { get; }

            public int Total
            {
                get
                {
                    return Large + Small;
                }
            }

            internal SkillInjectorsRequired(int large, int small)
            {
                Large = large;
                Small = small;
            }

            public override string ToString()
            {
                string display;
                if (Large > 0)
                {
                    if (Small == 1 && Large == 1)
                        display = "1 Large and 1 Small Skill Injector";
                    else if (Small > 0)
                        // 2 Large and 2 Small Skill Injectors
                        display = string.Format("{0:D} Large and {1:D} Small Skill Injectors",
                            Large, Small);
                    else
                        // 1 Large Skill Injector(s)
                        display = string.Format("{0:D} Large Skill Injector{1}", Large,
                            Large.S());
                }
                else
                    // 3 Small Skill Injector(s)
                    display = string.Format("{0:D} Small Skill Injector{1}", Small, Small.S());
                return display;
            }
        }
    }
}
