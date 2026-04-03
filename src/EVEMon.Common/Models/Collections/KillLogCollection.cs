using System.Collections;
using System.Collections.Generic;
using EVEMon.Common.Collections;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Serialization.Eve;
using EVEMon.Common.Service;
using EVEMon.Common.Serialization.Esi;
using EVEMon.Common.Constants;
using System;

namespace EVEMon.Common.Models.Collections
{
    public sealed class KillLogCollection : ReadonlyCollection<KillLog>, IEnumerable<KillLog>, IEnumerable
    {
        private readonly CCPCharacter m_ccpCharacter;
        private int m_killMailCounter;
        private readonly List<KillLog> m_pendingItems;
        private bool m_cacheLoaded;

        #region Constructor

        /// <summary>
        /// Internal constructor.
        /// </summary>
        /// <param name="ccpCharacter">The CCP character.</param>
        internal KillLogCollection(CCPCharacter ccpCharacter)
        {
            m_ccpCharacter = ccpCharacter;
            m_killMailCounter = 0;
            m_pendingItems = new List<KillLog>(32);
            m_cacheLoaded = false;
        }

        #endregion


        #region Lazy Cache Loading

        /// <summary>
        /// Ensures the kill log cache file has been loaded. Called lazily on first
        /// enumeration to avoid synchronous file I/O during character construction.
        /// </summary>
        private void EnsureCacheLoaded()
        {
            if (m_cacheLoaded)
                return;

            m_cacheLoaded = true;
            ImportFromCacheFile();
        }

        /// <summary>
        /// Gets the enumerator, ensuring the cache is loaded first.
        /// </summary>
        IEnumerator<KillLog> IEnumerable<KillLog>.GetEnumerator()
        {
            EnsureCacheLoaded();
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator, ensuring the cache is loaded first.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            EnsureCacheLoaded();
            return Items.GetEnumerator();
        }

        #endregion


        #region Importation

        /// <summary>
        /// Exports the kill logs to the cached file.
        /// </summary>
        public void ExportToCacheFile()
        {
            // Save the file to the cache
            string filename = m_ccpCharacter.Name + "-" + ESIAPICharacterMethods.KillLog;
            var exported = new SerializableAPIKillLog();
            foreach (KillLog killMail in Items)
                exported.Kills.Add(killMail.Export());
            LocalXmlCache.SaveAsync(filename, Util.SerializeToXmlDocument(exported)).
                ConfigureAwait(false);
            // Fire event to update the UI
            EveMonClient.OnCharacterKillLogUpdated(m_ccpCharacter);
        }

        /// <summary>
        /// Imports an enumeration of API objects.
        /// </summary>
        /// <param name="src">The enumeration of serializable kill log from the API.</param>
        internal void Import(IEnumerable<SerializableKillLogListItem> src)
        {
            Items.Clear();
            foreach (SerializableKillLogListItem srcKillLog in src)
                Items.Add(new KillLog(m_ccpCharacter, srcKillLog));
        }

        /// <summary>
        /// Imports an enumeration of ESI objects.
        /// </summary>
        /// <param name="kills">The enumeration of serializable kill data from ESI.</param>
        internal void Import(EsiAPIKillLog kills)
        {
            bool startRequest = false;
            lock (m_pendingItems)
            {
                // If no request currently running, start a new one
                if (m_killMailCounter == 0)
                {
                    m_killMailCounter = kills.Count;
                    m_pendingItems.Clear();
                    m_pendingItems.Capacity = m_killMailCounter;
                    startRequest = true;
                }
            }
            if (startRequest)
            {
                EveMonClient.Notifications.InvalidateAPIError();
                foreach (EsiKillLogListItem srcKillLog in kills)
                {
                    if (EsiErrors.IsErrorCountExceeded)
                        break;
                    // Query each individual mail; while the etag would be nice storing it in
                    // the legacy XML architecture is not really worth the trouble
                    string hash = srcKillLog.Hash;
                    EveMonClient.APIProviders.CurrentProvider.QueryEsi<EsiAPIKillMail>(
                        ESIAPIGenericMethods.KillMail, OnKillMailDownloaded, new ESIParams()
                        {
                            ParamOne = srcKillLog.KillID,
                            GetData = hash
                        }, hash);
                }
            }
        }

        private void OnKillMailDownloaded(EsiResult<EsiAPIKillMail> result, object hashValue)
        {
            var target = m_ccpCharacter;
            string hash = hashValue?.ToString() ?? EveMonConstants.UnknownText;
            // Synchronization is required here since multiple requests can finish at once
            lock (m_pendingItems)
            {
                // If character is still around and monitored
                if (target != null && target.Monitored)
                {
                    if (target.ShouldNotifyError(result, ESIAPICharacterMethods.KillLog))
                        EveMonClient.Notifications.NotifyKillMailError(result, hash);
                    if (!result.HasError && result.HasData)
                        // Add data inside synchronization
                        m_pendingItems.Add(new KillLog(target, result.Result.ToXMLItem()));
                }
                m_killMailCounter = Math.Max(0, m_killMailCounter - 1);
                if (m_killMailCounter == 0)
                {
                    // All kills fetched
                    Items.Clear();
                    Items.AddRange(m_pendingItems);
                    m_pendingItems.Clear();
                    ExportToCacheFile();
                }
            }
        }

        /// <summary>
        /// Imports the kill logs from a cached file.
        /// </summary>
        public void ImportFromCacheFile()
        {
            var result = LocalXmlCache.Load<SerializableAPIKillLog>(m_ccpCharacter.Name + "-" +
                ESIAPICharacterMethods.KillLog);
            if (result != null)
                Import(result.Kills);
        }

        #endregion

    }
}
