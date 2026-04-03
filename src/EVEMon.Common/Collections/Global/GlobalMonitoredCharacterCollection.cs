using System;
using System.Collections.Generic;
using System.Linq;
using EVEMon.Common.Attributes;
using EVEMon.Common.Models;
using EVEMon.Common.SettingsObjects;

namespace EVEMon.Common.Collections.Global
{
    /// <summary>
    /// Represents the characters list
    /// </summary>
    [EnforceUIThreadAffinity]
    public sealed class GlobalMonitoredCharacterCollection : ReadonlyCollection<Character>
    {
        /// <summary>
        /// Update the order from the given list.
        /// </summary>
        /// <param name="order"></param>
        public void Update(IEnumerable<Character> order)
        {
            Items.Clear();
            Items.AddRange(order);

            // Notify the change
            EveMonClient.OnMonitoredCharactersChanged();
        }

        /// <summary>
        /// Moves the given character to the target index.
        /// </summary>
        /// <remarks>
        /// When the item is located before the target index, it is decremented. 
        /// That way we ensures the item is actually inserted before the item that originally was at <c>targetindex</c>.
        /// </remarks>
        /// <param name="item"></param>
        /// <param name="targetIndex"></param>
        public void MoveTo(Character item, int targetIndex)
        {
            int oldIndex = Items.IndexOf(item);
            if (oldIndex == -1)
                throw new InvalidOperationException("The item was not found in the collection.");

            if (oldIndex < targetIndex)
                targetIndex--;
            Items.RemoveAt(oldIndex);
            Items.Insert(targetIndex, item);

            EveMonClient.OnMonitoredCharactersChanged();
        }

        /// <summary>
        /// When the <see cref="Character.Monitored"/> property changed, this collection is updated.
        /// </summary>
        /// <param name="character">The character for which the property changed.</param>
        /// <param name="value"></param>
        internal void OnCharacterMonitoringChanged(Character character, bool value)
        {
            if (value)
            {
                if (Items.Contains(character))
                    return;

                Items.Add(character);
                EveMonClient.OnMonitoredCharactersChanged();
                return;
            }

            if (!Items.Contains(character))
                return;

            Items.Remove(character);
            EveMonClient.OnMonitoredCharactersChanged();
        }

        /// <summary>
        /// Imports the given characters.
        /// </summary>
        /// <param name="monitoredCharacters"></param>
        internal void Import(ICollection<MonitoredCharacterSettings> monitoredCharacters)
        {
            Items.Clear();

            if (!monitoredCharacters.Any())
            {
                EveMonClient.OnMonitoredCharactersChanged();
                return;
            }

            foreach (MonitoredCharacterSettings characterSettings in monitoredCharacters)
            {
                Character character = EveMonClient.Characters[characterSettings.CharacterGuid.ToString()];
                if (character == null)
                    continue;

                Items.Add(character);
                character.Monitored = true;
                character.UISettings = characterSettings.Settings;
            }

            // Notify once after all characters have been imported, instead of
            // per-character which caused N redundant LayoutTabPages + Settings.Save calls
            EveMonClient.OnMonitoredCharactersChanged();
        }

        /// <summary>
        /// Updates the settings from <see cref="Settings"/>. Adds and removes group as needed.
        /// </summary>
        internal IEnumerable<MonitoredCharacterSettings> Export()
            => Items.Select(character => new MonitoredCharacterSettings(character));
    }
}
