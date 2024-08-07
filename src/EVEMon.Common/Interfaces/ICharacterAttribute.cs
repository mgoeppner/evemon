﻿using System;

namespace EVEMon.Common.Interfaces
{
    public interface ICharacterAttribute
    {
        long Base { get; }
        long ImplantBonus { get; }

        long BoosterBonus { get; }

        long EffectiveValue { get; }

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
        /// </list>
        /// </summary>
        /// <returns>The formatted string.</returns>
        string ToString(string format);
    }
}
