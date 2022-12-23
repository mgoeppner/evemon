using System;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Interfaces;

namespace EVEMon.Common.Models
{
    /// <summary>
    /// Represents an attribute for a character scratchpad
    /// </summary>
    public sealed class CharacterAttribute : ICharacterAttribute
    {
        private readonly EveAttribute m_attrib;
        private readonly Character m_character;

        /// <summary>
        /// Constructor from a character attribute.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="attrib"></param>
        internal CharacterAttribute(Character character, EveAttribute attrib)
        {
            Base = EveConstants.CharacterBaseAttributePoints;
            m_attrib = attrib;
            m_character = character;
        }

        /// <summary>
        /// Gets the base attribute.
        /// </summary>
        public long Base { get; internal set; }

        /// <summary>
        /// Gets the bonus granted by the implant.
        /// </summary>
        public long ImplantBonus => m_character.CurrentImplants[m_attrib].Bonus;

        /// <summary>
        /// Gets the bonus applied from cerebral accelerators
        /// </summary>
        public long BoosterBonus { get; internal set; }

        /// <summary>
        /// Gets the effective attribute value.
        /// </summary>
        public long EffectiveValue => Base + ImplantBonus + BoosterBonus;

        /// <summary>
        /// Gets a string representation with the provided format. The following parameters are accepted :
        /// <list type="bullet"><item>%n for name (lower case)</item><item>%N for name (CamelCase)</item><item>%B for attribute base value</item><item>%b for base bonus</item><item>%i for implant bonus</item><item>%r for remapping points</item><item>%e for effective value</item><item>%x for booster bonus</item></list>
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">format</exception>
        public string ToString(string format)
        {
            format.ThrowIfNull(nameof(format));

            format = format.Replace("%n", m_attrib.ToString().ToLower(CultureConstants.DefaultCulture));
            format = format.Replace("%N", m_attrib.ToString());
            format = format.Replace("%B", EveConstants.CharacterBaseAttributePoints.ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%b", Base.ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%i", ImplantBonus.ToString(CultureConstants.DefaultCulture));
            format = format.Replace("%r",
                (Base - EveConstants.CharacterBaseAttributePoints).ToString(CultureConstants.DefaultCulture));
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
