using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace EVEMon.Common
{
    /// <summary>
    /// Provides supported UI language metadata and culture setup.
    /// </summary>
    public static class LocalizationHelper
    {
        public const string DefaultCultureName = "en-US";
        public const string ChineseCultureName = "zh-CN";

        private static readonly IReadOnlyList<SupportedLanguage> s_supportedLanguages =
            new List<SupportedLanguage>
            {
                new SupportedLanguage(DefaultCultureName, "English"),
                new SupportedLanguage(ChineseCultureName, "简体中文")
            };

        /// <summary>
        /// Gets the supported UI languages.
        /// </summary>
        public static IReadOnlyList<SupportedLanguage> SupportedLanguages => s_supportedLanguages;

        /// <summary>
        /// Normalizes a culture name to one supported by EVEMon.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        /// <returns>A supported culture name.</returns>
        public static string NormalizeCultureName(string cultureName)
        {
            if (string.IsNullOrWhiteSpace(cultureName))
                return DefaultCultureName;

            return s_supportedLanguages.Any(language =>
                string.Equals(language.CultureName, cultureName, StringComparison.OrdinalIgnoreCase))
                ? cultureName
                : DefaultCultureName;
        }

        /// <summary>
        /// Gets the culture information for a supported language.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        /// <returns>The culture information.</returns>
        public static CultureInfo GetCultureInfo(string cultureName)
            => CultureInfo.GetCultureInfo(NormalizeCultureName(cultureName));

        /// <summary>
        /// Applies the selected culture to the current and future UI threads.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        public static void ApplyCulture(string cultureName)
        {
            CultureInfo cultureInfo = GetCultureInfo(cultureName);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        /// <summary>
        /// Gets the display string for a supported language.
        /// </summary>
        /// <param name="cultureName">The culture name.</param>
        /// <returns>The display string.</returns>
        public static string GetDisplayName(string cultureName)
            => s_supportedLanguages.First(language =>
                string.Equals(language.CultureName, NormalizeCultureName(cultureName), StringComparison.OrdinalIgnoreCase))
                .DisplayName;
    }

    /// <summary>
    /// Describes a supported EVEMon UI language.
    /// </summary>
    public sealed class SupportedLanguage
    {
        public SupportedLanguage(string cultureName, string displayName)
        {
            CultureName = cultureName;
            DisplayName = displayName;
        }

        public string CultureName { get; }

        public string DisplayName { get; }

        public override string ToString() => DisplayName;
    }
}
