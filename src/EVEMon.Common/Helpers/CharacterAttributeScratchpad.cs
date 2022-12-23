using System;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Interfaces;

namespace EVEMon.Common.Helpers
{
    /// <summary>
    /// Represents an attribute for a character scratchpad
    /// </summary>
    public sealed class CharacterAttributeScratchpad : ICharacterAttribute
    {
        private readonly EveAttribute m_attrib;

        private long m_base;
        private long m_implantBonus;
        private long m_boosterBonus;

        /// <summary>
        /// Constructor from a character attribute.
        /// </summary>
        /// <param name="attrib"></param>
        internal CharacterAttributeScratchpad(EveAttribute attrib)
        {
            m_attrib = attrib;
        }

        /// <summary>
        /// Resets the attribute with the given source
        /// </summary>
        /// <param name="baseAttribute"></param>
        /// <param name="implantBonus"></param>
        internal void Reset(long baseAttribute, long implantBonus, long boosterBonus)
        {
            m_base = baseAttribute;
            m_implantBonus = implantBonus;
            m_boosterBonus = boosterBonus;
            UpdateEffectiveAttribute();
        }

        /// <summary>
        /// Resets the attribute with the given source
        /// </summary>
        /// <param name="src"></param>
        internal void Reset(ICharacterAttribute src)
        {
            m_base = src.Base;
            m_implantBonus = src.ImplantBonus;
            m_boosterBonus = src.BoosterBonus;
            UpdateEffectiveAttribute();
        }

        /// <summary>
        /// Updates the effective attribute
        /// </summary>
        internal void UpdateEffectiveAttribute()
        {
            EffectiveValue = m_base + m_implantBonus + m_boosterBonus;
        }

        /// <summary>
        /// Gets or sets the base attribute.
        /// </summary>
        public long Base
        {
            get { return m_base; }
            set
            {
                m_base = value;
                UpdateEffectiveAttribute();
            }
        }

        /// <summary>
        /// Gets or sets the bonus granted by the implant
        /// </summary>
        public long ImplantBonus
        {
            get { return m_implantBonus; }
            set
            {
                m_implantBonus = value;
                UpdateEffectiveAttribute();
            }
        }

        /// <summary>
        /// Gets or sets the bonus applied from cerebral accelerators
        /// </summary>
        public long BoosterBonus
        {
            get { return m_boosterBonus; }
            set
            {
                m_boosterBonus = value;
                UpdateEffectiveAttribute();
            }
        }

        /// <summary>
        /// Gets the effective attribute value.
        /// </summary>
        public long EffectiveValue { get; private set; }

        /// <summary>
        /// Gets a string representation with the provided format. The following parameters are accepted :
        /// <list type="bullet">
        /// <item>%n for name (lower case)</item>
        /// <item>%N for name (CamelCase)</item>
        /// <item>%B for attribute base value</item>
        /// <item>%b for base bonus</item>
        /// <item>%i for implant bonus</item>
        /// <item>%r for remapping points</item>
        /// <item>%e for effective value</item>
        /// <item>%x for booster bonus</item>
        /// </list>
        /// </summary>
        /// <returns>The formatted string.</returns>
        /// <exception cref="System.ArgumentNullException">format</exception>
        public string ToString(string format)
        {
            format.ThrowIfNull(nameof(format));

            format = format.Replace("%n", m_attrib.ToString().ToLower(CultureConstants.DefaultCulture));
            format = format.Replace("%N", m_attrib.ToString());
            format = format.Replace("%B", EveConstants.CharacterBaseAttributePoints.ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%b", m_base.ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%i", ImplantBonus.ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%r", (m_base - EveConstants.CharacterBaseAttributePoints).ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%e", EffectiveValue.ToString("0", CultureConstants.DefaultCulture));
            format = format.Replace("%x", BoosterBonus.ToString(CultureConstants.DefaultCulture));
            return format;
        }

        /// <summary>
        /// Gets a string representation with the following format : "<c>Intelligence : 15</c>"
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{m_attrib} : {EffectiveValue}";
    }
}
