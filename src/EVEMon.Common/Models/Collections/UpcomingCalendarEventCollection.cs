using EVEMon.Common.Collections;
using EVEMon.Common.Serialization.Esi;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Serialization.Eve;
using System;

namespace EVEMon.Common.Models.Collections
{
    public sealed class UpcomingCalendarEventCollection : ReadonlyCollection<UpcomingCalendarEvent>
    {
        private readonly CCPCharacter m_character;
        private readonly object m_counterLock;
        private int m_eventCounter;

        #region Constructor

        /// <summary>
        /// Internal constructor.
        /// </summary>
        /// <param name="character">The character.</param>
        internal UpcomingCalendarEventCollection(CCPCharacter character)
        {
            m_character = character;
            m_eventCounter = 0;
            m_counterLock = new object();
        }

        #endregion


        #region Importation

        /// <summary>
        /// Imports an enumeration of API objects.
        /// </summary>
        /// <param name="events">The serializable calendar events from the API.</param>
        internal void Import(EsiAPICalendarEvents events)
        {
            if (m_eventCounter == 0)
            {
                ESIKey apiKey = m_character.Identity.FindAPIKeyWithAccess(ESIAPICharacterMethods.
                    UpcomingCalendarEventDetails);
                string accessToken = apiKey?.GetAccessTokenForQuery();
                // Detail requests are character-scoped authenticated calls. If a token
                // refresh is in flight, leave the counter at zero so the next calendar
                // monitor refresh can retry instead of wedging this collection.
                if (apiKey == null || string.IsNullOrEmpty(accessToken) ||
                    EsiErrors.IsErrorCountExceeded)
                    return;

                Items.Clear();
                EveMonClient.Notifications.InvalidateAPIError();
                lock (m_counterLock)
                {
                    m_eventCounter = 0;
                }
                // Import the events from the API; note that a request must be made for details
                // for every event!
                foreach (EsiAPICalendarEvent srcEvent in events)
                {
                    long id = srcEvent.EventID;
                    if (EsiErrors.IsErrorCountExceeded)
                        break;
                    lock (m_counterLock)
                    {
                        m_eventCounter++;
                    }
                    // Query each individual event; maintaining etags/expiration for all of
                    // them is not really worth it
                    EveMonClient.APIProviders.CurrentProvider.QueryEsi<EsiAPICalendarEvent>(
                        ESIAPICharacterMethods.UpcomingCalendarEventDetails,
                        OnCalendarEventDownloaded, new ESIParams(null, accessToken)
                        {
                            ParamOne = m_character.CharacterID,
                            ParamTwo = id
                        }, id);
                }
            }
        }

        private void OnCalendarEventDownloaded(EsiResult<EsiAPICalendarEvent> result, object forID)
        {
            var target = m_character;
            long id = (forID as long?) ?? 0L;

            // If character is still around and monitored
            if (target != null && target.Monitored)
            {
                if (target.ShouldNotifyError(result, ESIAPICharacterMethods.
                        UpcomingCalendarEventDetails))
                    EveMonClient.Notifications.NotifyCharacterUpcomingCalendarEventDetailsError(
                        m_character, result);
                if (!result.HasError && result.HasData)
                    Items.Add(new UpcomingCalendarEvent(target, result.Result));
            }
            // Synchronization is required here since multiple requests can finish at once
            lock (m_counterLock)
            {
                m_eventCounter = Math.Max(0, m_eventCounter - 1);
            }
        }

        #endregion

    }
}
