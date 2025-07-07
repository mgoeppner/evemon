using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using EVEMon.Common.Collections;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Extensions;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Models;
using EVEMon.Common.Models.Comparers;
using EVEMon.Common.Serialization.Exportation;
using EVEMon.Common.Serialization.Settings;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.Common.Helpers
{
    public static class PlanIOHelper
    {
        /// <summary>
        /// Exports the plan under a text format.
        /// </summary>
        /// <param name="planToExport">The plan to export.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="exportActions">The export actions.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// planToExport
        /// or
        /// settings
        /// </exception>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="System.ArgumentNullException">planToExport or settings</exception>
        public static string ExportAsText(Plan planToExport, PlanExportSettings settings,
                                          Action<StringBuilder, PlanEntry, PlanExportSettings> exportActions = null)
        {
            planToExport.ThrowIfNull(nameof(planToExport));

            settings.ThrowIfNull(nameof(settings));

            PlanScratchpad plan = new PlanScratchpad(planToExport.Character, planToExport);
            plan.Sort(planToExport.SortingPreferences);
            plan.UpdateStatistics();

            StringBuilder builder = new StringBuilder();
            Character character = (Character)plan.Character;

            // Initialize constants
            string lineFeed = Environment.NewLine;
            string boldStart = string.Empty;
            string boldEnd = string.Empty;

            switch (settings.Markup)
            {
                case MarkupType.Forum:
                    boldStart = "[b]";
                    boldEnd = "[/b]";
                    break;
                case MarkupType.Html:
                    lineFeed = $"<br />{Environment.NewLine}";
                    boldStart = "<b>";
                    boldEnd = "</b>";
                    break;
                case MarkupType.Undefined:
                case MarkupType.None:
                    break;
                default:
                    throw new NotImplementedException();
            }

            // Header
            if (settings.IncludeHeader)
            {
                builder.Append(boldStart)
                    .Append($"{(settings.ShoppingList ? "Shopping list " : "Skill plan ")} for {character.Name}")
                    .Append(boldEnd)
                    .Append(lineFeed)
                    .Append(lineFeed);
            }

            // Scroll through entries
            int index = 0;
            DateTime endTime = DateTime.Now;
            foreach (PlanEntry entry in plan)
            {
                // Skip is we're only build a shopping list
                bool shoppingListCandidate = !(entry.CharacterSkill.IsKnown || entry.Level != 1 || entry.CharacterSkill.IsOwned);
                if (settings.ShoppingList && !shoppingListCandidate)
                    continue;

                // Remapping point
                if (!settings.ShoppingList && (entry.Remapping != null) && settings.RemappingPoints)
                {
                    builder
                        .Append($"***{entry.Remapping}***")
                        .Append(lineFeed);
                }

                // Entry's index
                index++;
                if (settings.EntryNumber)
                    builder.Append($"{index}. ");

                // Name
                builder.Append(boldStart);
                AddName(settings, entry, builder);
                builder.Append(boldEnd);

                // Training time
                AddTrainingTime(settings, shoppingListCandidate, entry, builder);

                exportActions?.Invoke(builder, entry, settings);

                builder.Append(lineFeed);

                // End time
                endTime = entry.EndTime;
            }

            // Footer
            AddFooter(settings, boldEnd, index, endTime, builder, lineFeed, plan, boldStart);

            // Returns the text representation.
            return builder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        /// <summary>
        /// Adds the name.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="entry">The entry.</param>
        /// <param name="builder">The builder.</param>
        private static void AddName(PlanExportSettings settings, ISkillLevel entry, StringBuilder builder)
        {
            if (settings.Markup == MarkupType.Html)
            {
                builder.Append("<a href=\"\" onclick=\"CCPEVE.show" +
                               $"{(!settings.ShoppingList ? "Info" : "MarketDetails")}({entry.Skill.ID})\">");
            }
            builder.Append(entry.Skill.Name);

            if (settings.Markup == MarkupType.Html)
                builder.Append("</a>");

            if (!settings.ShoppingList)
                builder.Append($" {Skill.GetRomanFromInt(entry.Level)}");

        }

        /// <summary>
        /// Adds the training time.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="shoppingListCandidate">if set to <c>true</c> [shopping list candidate].</param>
        /// <param name="entry">The entry.</param>
        /// <param name="builder">The builder.</param>
        private static void AddTrainingTime(PlanExportSettings settings, bool shoppingListCandidate, PlanEntry entry, StringBuilder builder)
        {
            if (!settings.EntryTrainingTimes && !settings.EntryStartDate && !settings.EntryFinishDate &&
                (!settings.EntryCost || !shoppingListCandidate))
                return;

            const DescriptiveTextOptions TimeFormat = DescriptiveTextOptions.FullText
                                                      | DescriptiveTextOptions.IncludeCommas
                                                      | DescriptiveTextOptions.SpaceText;

            builder.Append(" (");
            bool needComma = false;

            // Training time
            if (settings.EntryTrainingTimes)
            {
                needComma = true;
                builder.Append(entry.TrainingTime.ToDescriptiveText(TimeFormat));
            }

            // Training start date
            if (settings.EntryStartDate)
            {
                if (needComma)
                    builder.Append("; ");

                needComma = true;

                builder.Append($"Start: {entry.StartTime.ToUniversalTime().DateTimeToTimeString()} UTC");
            }

            // Training end date
            if (settings.EntryFinishDate)
            {
                if (needComma)
                    builder.Append("; ");

                needComma = true;

                builder.Append($"Finish: {entry.EndTime.ToUniversalTime().DateTimeToTimeString()} UTC");
            }

            // Skill cost
            if (settings.EntryCost && shoppingListCandidate)
            {
                if (needComma)
                    builder.Append("; ");

                builder.Append(FormattableString.Invariant($"Cost: {entry.Skill.Cost:N0} ISK"));
            }

            builder.Append(')');
        }

        /// <summary>
        /// Adds the footer.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="boldEnd">The bold end.</param>
        /// <param name="index">The index.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="builder">The builder.</param>
        /// <param name="lineFeed">The line feed.</param>
        /// <param name="plan">The plan.</param>
        /// <param name="boldStart">The bold start.</param>
        private static void AddFooter(PlanExportSettings settings, string boldEnd, int index, DateTime endTime, StringBuilder builder,
                                      string lineFeed, BasePlan plan, string boldStart)
        {
            if (!settings.FooterCount && !settings.FooterTotalTime && !settings.FooterDate && !settings.FooterCost)
                return;

            builder.AppendLine(lineFeed);
            bool needComma = false;

            // Skills count
            if (settings.FooterCount)
            {
                builder
                    .Append($"{boldStart}{plan.GetUniqueSkillsCount()}{boldEnd} " +
                            $"unique skill{(plan.GetUniqueSkillsCount() == 1 ? string.Empty : "s")}, ")
                    .Append($"{boldStart}{index}{boldEnd} skill level{(index == 1 ? string.Empty : "s")}");

                needComma = true;
            }

            // Plan's training duration
            if (settings.FooterTotalTime)
            {
                const DescriptiveTextOptions TimeFormat =
                    DescriptiveTextOptions.FullText | DescriptiveTextOptions.IncludeCommas | DescriptiveTextOptions.SpaceText;

                if (needComma)
                    builder.Append("; ");

                needComma = true;

                builder.Append($"Total time: {boldStart}{plan.TotalTrainingTime.ToDescriptiveText(TimeFormat)}{boldEnd}");
            }

            // End training date
            if (settings.FooterDate)
            {
                if (needComma)
                    builder.Append("; ");

                needComma = true;

                builder.Append($"Completion: {boldStart}{endTime.ToUniversalTime().DateTimeToTimeString()}{boldEnd} UTC");
            }

            // Total books cost
            if (settings.FooterCost)
            {
                if (needComma)
                    builder.Append("; ");

                string formattedIsk = FormattableString.Invariant($"{plan.NotKnownSkillBooksCost:N0}");
                builder.Append($"Cost: {boldStart}{formattedIsk}{boldEnd} ISK");
            }

            // Warning about skill costs
            builder.Append(lineFeed);
            if (settings.FooterCost || settings.EntryCost)
                builder.Append("N.B. Skill costs are based on CCP's database and are indicative only");
        }

        /// <summary>
        /// Exports the plan under an XML format.
        /// </summary>
        /// <param name="plan">The plan.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">plan</exception>
        public static string ExportAsXML(Plan plan)
        {
            plan.ThrowIfNull(nameof(plan));

            // Generates a settings plan and transforms it to an output plan
            SerializablePlan serial = plan.Export();
            OutputPlan output = new OutputPlan { Name = serial.Name, Owner = serial.Owner, Revision = Settings.Revision };
            output.Entries.AddRange(serial.Entries);

            // Serializes to XML document and gets a string representation
            XmlDocument doc = (XmlDocument)Util.SerializeToXmlDocument(output);
            return Util.GetXmlStringRepresentation(doc);
        }

        /// <summary>
        /// Exports the plan under an XML format.
        /// </summary>
        /// <param name="plans">The plans.</param>
        /// <returns></returns>
        public static string ExportAsXML(IEnumerable<Plan> plans)
        {
            OutputPlans output = new OutputPlans { Revision = Settings.Revision };
            output.Plans.AddRange(plans.Select(plan => plan.Export()));

            // Serializes to XML document and gets a string representation
            XmlDocument doc = (XmlDocument)Util.SerializeToXmlDocument(output);
            return Util.GetXmlStringRepresentation(doc);
        }

        /// <summary>
        /// Imports a <see cref="SerializablePlan" /> from the given filename. Works with old and new formats.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">filename</exception>
        public static SerializablePlan ImportFromXML(string filename)
        {
            filename.ThrowIfNull(nameof(filename));

            int revision = -1;
            SerializablePlan result = null;
            try
            {
                // Is the format compressed ? 
                if (filename.EndsWith(".emp", StringComparison.OrdinalIgnoreCase))
                {
                    string tempFile = Util.UncompressToTempFile(filename);
                    try
                    {
                        return ImportFromXML(tempFile);
                    }
                    finally
                    {
                        FileHelper.DeleteFile(tempFile);
                    }
                }

                // Reads the revision number from the file
                revision = Util.GetRevisionNumber(filename);

                // Old format
                result = revision == 0
                             ? (SerializablePlan)UIHelper.ShowNoSupportMessage()
                             : Util.DeserializeXmlFromFile<OutputPlan>(filename);
            }
            catch (UnauthorizedAccessException exc)
            {
                MessageBox.Show(@"Couldn't read the given file, access was denied. Maybe the directory was under synchronization.");
                ExceptionHandler.LogException(exc, true);
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(@"The file seems to be corrupted, wrong gzip format.");
                ExceptionHandler.LogException(exc, true);
            }

            if (result == null && revision > 0)
                MessageBox.Show(@"There was a problem with the format of the document.");

            return result;
        }

        /// <summary>
        /// Imports plans from the given filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">filename</exception>
        public static IEnumerable<SerializablePlan> ImportPlansFromXML(string filename)
        {
            filename.ThrowIfNull(nameof(filename));

            OutputPlans result = null;
            try
            {
                // Is the format compressed ? 
                if (filename.EndsWith(".epb", StringComparison.OrdinalIgnoreCase))
                {
                    string tempFile = Util.UncompressToTempFile(filename);
                    try
                    {
                        return ImportPlansFromXML(tempFile);
                    }
                    finally
                    {
                        FileHelper.DeleteFile(tempFile);
                    }
                }

                // Reads the revision number from the file
                int revision = Util.GetRevisionNumber(filename);

                if (revision != 0)
                    result = Util.DeserializeXmlFromFile<OutputPlans>(filename);
            }
            catch (UnauthorizedAccessException exc)
            {
                MessageBox.Show(@"Couldn't read the given file, access was denied. Maybe the directory was under synchronization.");
                ExceptionHandler.LogException(exc, true);
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(@"The file seems to be corrupted, wrong gzip format.");
                ExceptionHandler.LogException(exc, true);
            }

            if (result != null)
                return result.Plans;

            MessageBox.Show(@"There was a problem with the format of the document.");
            return null;
        }

        /// <summary>
        /// Creates a plan from a character skill queue.
        /// </summary>
        /// <param name="newPlan">The new plan.</param>
        /// <param name="character">The character.</param>
        public static bool CreatePlanFromCharacterSkillQueue(Plan newPlan, Character character)
        {
            CCPCharacter ccpCharacter = character as CCPCharacter;

            if (ccpCharacter == null)
                return false;

            if (!ccpCharacter.SkillQueue.Any())
            {
                MessageBox.Show(@"There are no skills in the characters' queue.",
                    @"Plan Creation Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (ccpCharacter.Plans.Any(x => x.Name == newPlan.Name))
            {
                MessageBox.Show(@"There is already a plan with the same name in the characters' Plans.",
                    @"Plan Creation Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Add skill queue in plan
            foreach (QueuedSkill qSkill in ccpCharacter.SkillQueue)
            {
                newPlan.PlanTo(qSkill.Skill, qSkill.Level);
            }

            // Check if there is already a plan with the same skills
            if (ccpCharacter.Plans.Any(plan => !newPlan.Except(plan, new PlanEntryComparer()).Any()))
            {
                if (MessageBox.Show(@"There is already a plan with the same skills in the characters' Plans.

Would you like to proceed?",
                        @"Plan Creation Duplicate",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }

            // Add plan and save
            ccpCharacter.Plans.Insert(0, newPlan);

            return true;
        }
    }
}
