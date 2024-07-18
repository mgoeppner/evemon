using EVEMon.Common.Extensions;
using EVEMon.XmlGenerator.StaticData;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EVEMon.XmlGenerator.Extensions
{
    /// <summary>
    /// Series of extension methods to cleanup and format strings for use in the data files.
    /// </summary>
    public static class StringCleaning
    {
        /// <summary>
        /// Cleans up and normalizes a string by passing it through the following filters.
        /// </summary>
        /// <param name="input"><c>string</c> to be cleaned.</param>
        /// <returns>cleaned <c>string</c></returns>
        public static string Clean(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string output = input.TrimWhitespace();
            output = output.ReplaceTabs();
            output = output.ReplaceHtmlLineBreaks();
            output = output.CleanXmlTags();
            output = output.CollapseSpaces();
            output = output.Normalize();

            return output;
        }

        /// <summary>
        /// Colapses any sequence of spaces into a single space.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string CollapseSpaces(this string input)
            => Regex.Replace(input, @"[ ]{2,}", m => @" ", RegexOptions.Compiled);

        /// <summary>
        /// Replaces the HTML line breaks.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ReplaceHtmlLineBreaks(this string input)
            => Regex.Replace(input, @"<br+?>|<br\s?/+?>", m => Environment.NewLine,
                    RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Removes any text between opposing angle brackets (i.e. XML or HTML tags).
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // Remove markup
        private static string CleanXmlTags(this string input)
            => Regex.Replace(input, "<.+?>",
                m => string.Empty, RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Switches any occurance of a tab with a single space.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // Replace tab characters with spaces
        private static string ReplaceTabs(this string input) => input.Replace('\t', ' ');

        /// <summary>
        /// Trims whitespace from the beginning and end of a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string TrimWhitespace(this string input)
        {
            // Fix space before a dot (yes, there are those in descriptions)
            string output = input.Replace(" .", ".");

            // Remove whitespace from the beginning and end of a string
            return output.Trim();
        }

        /// <summary>
        /// Formats a properties value in a human friendly manner.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">property</exception>
        public static string FormatPropertyValue(this DgmTypeAttributes property)
        {
            property.ThrowIfNull(nameof(property));

            // Is it actually an integer stored as a float?
            if (property.ValueFloat.HasValue &&
                Math.Abs(Math.Truncate(property.ValueFloat.Value) - property.ValueFloat.Value) < float.Epsilon)
                return Convert.ToInt64(property.ValueFloat.Value).ToString(CultureInfo.InvariantCulture);

            return property.ValueInt64.HasValue ? property.ValueInt64.ToString() : property.ValueFloat.ToString();
        }

        /// <summary>
        /// Formats a decimal in a human friendly manner.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // Is it actually an integer stored as a double?
        public static string FormatDecimal(this decimal input)
            => Math.Truncate(input) == input
                ? Convert.ToInt64(input).ToString(CultureInfo.InvariantCulture)
                : input.ToString(CultureInfo.InvariantCulture);
    }
}