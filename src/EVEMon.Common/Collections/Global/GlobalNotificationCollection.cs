using EVEMon.Common.Enumerations;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Notifications;
using EVEMon.Common.Serialization;
using EVEMon.Common.Serialization.Esi;
using EVEMon.Common.Serialization.Eve;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EVEMon.Common.Collections.Global
{
    /// <summary>
    /// The collection used by <see cref="EveMonClient.Notifications"/>
    /// </summary>
    public sealed class GlobalNotificationCollection : ReadonlyCollection<NotificationEventArgs>
    {
        /// <summary>
        /// Constructor, used by <see cref="EveMonClient"/> only.
        /// </summary>
        internal GlobalNotificationCollection()
        {
        }

        /// <summary>
        /// Protected default constructor with an initial capacity.
        /// </summary>
        internal GlobalNotificationCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        /// Adds a notification to this collection.
        /// </summary>
        /// <param name="notification"></param>
        /// <exception cref="System.ArgumentNullException">notification</exception>
        public void Notify(NotificationEventArgs notification)
        {
            notification.ThrowIfNull(nameof(notification));

            switch (notification.Behaviour)
            {
            case NotificationBehaviour.Cohabitate:
                Items.Add(notification);
                break;

            case NotificationBehaviour.Overwrite:
                // Replace the previous notifications with the same invalidation key
                InvalidateCore(notification.InvalidationKey);
                Items.Add(notification);
                break;

            case NotificationBehaviour.Merge:
                // Merge the notifications with the same key
                // Snapshot Items to prevent IndexOutOfRangeException if the notification
                // chain (via ThreadSafeInvoke) re-enters and modifies Items concurrently
                long key = notification.InvalidationKey;
                foreach (NotificationEventArgs other in Items.ToArray().Where(x => x.InvalidationKey == key))
                {
                    notification.Append(other);
                }

                // Replace the previous notifications with the same invalidation key
                InvalidateCore(key);
                Items.Add(notification);
                break;
            }

            EveMonClient.OnNotificationSent(notification);
        }

        /// <summary>
        /// Invalidates the notifications with the given key and notify an event.
        /// </summary>
        /// <param name="e"></param>
        public void Invalidate(NotificationInvalidationEventArgs e)
        {
            if (InvalidateCore(e.Key))
                EveMonClient.OnNotificationInvalidated(e);
        }

        /// <summary>
        /// Invalidates the notifications with the given key.
        /// </summary>
        /// <param name="key"></param>
        private bool InvalidateCore(long key)
        {
            int index = 0;
            bool foundAny = false;

            // Removes all the notifications with the given key
            while (index < Items.Count)
            {
                if (Items[index].InvalidationKey != key)
                    index++;
                else
                {
                    Items.RemoveAt(index);
                    foundAny = true;
                }
            }

            // Did we remove anything
            return foundAny;
        }


        #region API Server error

        /// <summary>
        /// Invalidates the notification for an API server querying error.
        /// </summary>
        internal void InvalidateAPIError()
        {
            Invalidate(new NotificationInvalidationEventArgs(null, NotificationCategory.
                QueryingError));
        }

        /// <summary>
        /// Notifies an SSO sign-in error.
        /// </summary>
        internal void NotifySSOError()
        {
            var notification = new NotificationEventArgs(null, NotificationCategory.
                QueryingError)
            {
                Description = Properties.Resources.ErrorSSO,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an SSO character list error.
        /// </summary>
        /// <param name="key">The ESI key which failed to load.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterListError(ESIKey key, JsonResult<EsiAPITokenInfo> result)
        {
            string charName = string.Format("token {0:D}", key.ID);
            // Attempt to match the ESI key to a character name
            var id = EveMonClient.CharacterIdentities.FirstOrDefault((charID) => charID.
                ESIKeys.Contains(key));
            if (id != null)
                charName = id.CharacterName;
            var notification = new NotificationEventArgs(null, NotificationCategory.
                QueryingError)
            {
                Description = string.Format(Properties.Resources.ErrorCharacterList, charName),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a citadel querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyCitadelQueryError(EsiResult<EsiAPIStructure> result)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = Properties.Resources.ErrorStructure,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a kill mail query error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyKillMailError(EsiResult<EsiAPIKillMail> result, string hash)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = string.Format(Properties.Resources.ErrorKillMail, hash),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a station querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyStationQueryError(EsiResult<EsiAPIStation> result)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = Properties.Resources.ErrorStation,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an EVE factional warfare stats querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyEveFactionalWarfareStatsError(EsiResult<EsiAPIEveFactionalWarfareStats> result)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = Properties.Resources.ErrorEVEFacWarStat,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an EVE factional war list querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyEveFactionWarsError(EsiResult<EsiAPIEveFactionWars> result)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = Properties.Resources.ErrorEVEFacWarList,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character Id to name querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterNameError(EsiResult<EsiAPICharacterNames> result)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = Properties.Resources.ErrorIDToName,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a planet querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyPlanetInfoError(EsiResult<EsiAPIPlanet> result)
        {
            var notification = new APIErrorNotificationEventArgs(null, result)
            {
                Description = Properties.Resources.ErrorPlanets,
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        #endregion


        #region Server status API error

        /// <summary>
        /// Notifies a server status querying error.
        /// </summary>
        /// <param name="result">The result.</param>
        internal void NotifyServerStatusError(EsiResult<EsiAPIServerStatus> result)
        {
            var notification =
                new APIErrorNotificationEventArgs(null, result)
                {
                    Description = Properties.Resources.ErrorStatus,
                    Behaviour = NotificationBehaviour.Overwrite,
                    Priority = NotificationPriority.Error
                };
            Notify(notification);
        }

        #endregion

#if false

        /// <summary>
        /// Invalidates the notification for an account status error.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        internal void InvalidateAccountStatusError(ESIKey apiKey)
        {
            Invalidate(new NotificationInvalidationEventArgs(apiKey, NotificationCategory.QueryingError));
        }

        /// <summary>
        /// Notifies an account status querying error.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="result">The result.</param>
        internal void NotifyAccountStatusError(ESIKey apiKey, EsiResult<EsiAPIAccountStatus> result)
        {
            var notification =
                new APIErrorNotificationEventArgs(apiKey, result)
                {
                    Description = string.Format(Properties.Resources.ErrorAccountStatus, apiKey),
                    Behaviour = NotificationBehaviour.Overwrite,
                    Priority = NotificationPriority.Error
                };
            Notify(notification);
        }

#endif


        #region Character API errors

        /// <summary>
        /// Invalidates the notification for a character's API error.
        /// </summary>
        /// <param name="character">The character.</param>
        internal void InvalidateCharacterAPIError(CCPCharacter character)
        {
            Invalidate(new NotificationInvalidationEventArgs(character, NotificationCategory.
                QueryingError));
        }

        /// <summary>
        /// Notifies a character sheet querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterSheetError(CCPCharacter character,
            EsiResult<EsiAPICharacterSheet> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorCharacterSheet,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character account balance querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterBalanceError(CCPCharacter character,
            EsiResult<string> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorAccountBalance,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character location querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterLocationError(CCPCharacter character,
            EsiResult<EsiAPILocation> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorLocation,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character clones querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterClonesError(CCPCharacter character,
            EsiResult<EsiAPIClones> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorJumpClones,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character jump fatigue querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterFatigueError(CCPCharacter character,
            EsiResult<EsiAPIJumpFatigue> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorJumpFatigue,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character attribute querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterAttributesError(CCPCharacter character,
            EsiResult<EsiAPIAttributes> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorAttributes,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character ship querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterShipError(CCPCharacter character,
            EsiResult<EsiAPIShip> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorShip, character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character implants querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterImplantsError(CCPCharacter character,
            EsiResult<List<int>> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorImplants,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a skill querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterSkillsError(CCPCharacter character,
            EsiResult<EsiAPISkills> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorSkills, character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an employment history querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterEmploymentError(CCPCharacter character,
            EsiResult<EsiAPIEmploymentHistory> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorEmploymentHistory,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a skill queue querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifySkillQueueError(CCPCharacter character,
            EsiResult<EsiAPISkillQueue> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorSkillQueue,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a standings querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterStandingsError(CCPCharacter character,
            EsiResult<EsiAPIStandings> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorStandings,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a factional warfare stats querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterFactionalWarfareStatsError(CCPCharacter character,
            EsiResult<EsiAPIFactionalWarfareStats> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorFacWarStat,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an assets querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterAssetsError(CCPCharacter character,
            EsiResult<EsiAPIAssetList> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorAssets, character?.
                    Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies the character market orders querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterMarketOrdersError(CCPCharacter character,
            EsiResult<EsiAPIMarketOrders> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorMarketOrders,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a corporation market orders querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCorporationMarketOrdersError(CCPCharacter character,
            EsiResult<EsiAPIMarketOrders> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorMarketOrders,
                    character?.CorporationName),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies the character contracts querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterContractsError(CCPCharacter character,
            EsiResult<EsiAPIContracts> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorContracts,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies the corporation contracts querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCorporationContractsError(CCPCharacter character,
            EsiResult<EsiAPIContracts> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorContracts,
                    character?.CorporationName),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a contract items querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyContractItemsError(CCPCharacter character,
            EsiResult<EsiAPIContractItems> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorContractItems,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a contract bids querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyContractBidsError(CCPCharacter character,
            EsiResult<EsiAPIContractBids> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorContractBids,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a wallet journal querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterWalletJournalError(CCPCharacter character,
            EsiResult<EsiAPIWalletJournal> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorWalletJournal,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a wallet transactions querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterWalletTransactionsError(CCPCharacter character,
            EsiResult<EsiAPIWalletTransactions> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorWalletTransactions,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a character industry jobs querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterIndustryJobsError(CCPCharacter character,
            EsiResult<EsiAPIIndustryJobs> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorIndustryJobs,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a corporation industry jobs querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCorporationIndustryJobsError(CCPCharacter character,
            EsiResult<EsiAPIIndustryJobs> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorIndustryJobs,
                    character?.CorporationName),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a research querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterResearchPointsError(CCPCharacter character,
            EsiResult<EsiAPIResearchPoints> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorResearchPoints,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a mail messages query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyEVEMailMessagesError(CCPCharacter character,
            EsiResult<EsiAPIMailMessages> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorEVEMail,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a mail body query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyEVEMailBodiesError(CCPCharacter character,
            EsiResult<EsiAPIMailBody> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorEVEMailBody,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a mailing lists query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyMailingListsError(CCPCharacter character,
            EsiResult<EsiAPIMailingLists> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorEVEMailLists,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a notifications query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyEVENotificationsError(CCPCharacter character,
            EsiResult<EsiAPINotifications> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorNotifications,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a contact list query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterContactsError(CCPCharacter character,
            EsiResult<EsiAPIContactsList> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorContacts, character?.
                    Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a medals query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterMedalsError(CCPCharacter character,
            EsiResult<EsiAPIMedals> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorMedals, character?.
                    Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a corporation medals querying error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCorporationMedalsError(CCPCharacter character,
            EsiResult<EsiAPIMedals> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorMedals, character?.
                    CorporationName),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an upcoming calendar event details query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterUpcomingCalendarEventDetailsError(CCPCharacter character,
            EsiResult<EsiAPICalendarEvent> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorCalendarDetails,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies an upcoming calendar events query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterUpcomingCalendarEventsError(CCPCharacter character,
            EsiResult<EsiAPICalendarEvents> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorCalendarEvents,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a calendar event body query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterUpcomingCalendarEventInfoError(CCPCharacter character,
            EsiResult<EsiAPICalendarEvent> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorCalendarDetails,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a calendar event attendees query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterCalendarEventAttendeesError(CCPCharacter character,
            EsiResult<EsiAPICalendarEventAttendees> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorCalendarAttendees,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a kill log query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterKillLogError(CCPCharacter character,
            EsiResult<EsiAPIKillLog> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorKillLog, character?.
                    Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a planetary colonies query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterPlanetaryColoniesError(CCPCharacter character,
            EsiResult<EsiAPIPlanetaryColoniesList> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorPlanets, character?.
                    Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a planetary layout query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterPlanetaryLayoutError(CCPCharacter character,
            EsiResult<EsiAPIPlanetaryColony> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorPlanetLayout,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        /// <summary>
        /// Notifies a loyalty query error.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="result">The result.</param>
        internal void NotifyCharacterLoyaltyPointsError(CCPCharacter character,
            EsiResult<EsiAPILoyality> result)
        {
            var notification = new APIErrorNotificationEventArgs(character, result)
            {
                Description = string.Format(Properties.Resources.ErrorLoyalty,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Error
            };
            Notify(notification);
        }

        #endregion


        #region Account expiration

        /// <summary>
        /// Invalidates the notification for an account expiration.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        internal void InvalidateAccountExpiration(ESIKey apiKey)
        {
            Invalidate(new NotificationInvalidationEventArgs(apiKey, NotificationCategory.
                AccountExpiration));
        }

        /// <summary>
        /// Notifies an account is to expire within a week.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="expireDate">The expire date.</param>
        /// <param name="priority">The priority.</param>
        internal void NotifyAccountExpiration(ESIKey apiKey, DateTime expireDate,
            NotificationPriority priority)
        {
            var notification = new NotificationEventArgs(apiKey, NotificationCategory.
                AccountExpiration)
            {
                Description = string.Format(Properties.Resources.MessageExpiration,
                    expireDate.ToRemainingTimeShortDescription(DateTimeKind.Utc), apiKey),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = priority
            };
            Notify(notification);
        }

        #endregion


        #region Insufficient balance

        /// <summary>
        /// Invalidates the notification for an insufficient balance.
        /// </summary>
        /// <param name="character">The character.</param>
        internal void InvalidateInsufficientBalance(CCPCharacter character)
        {
            Invalidate(new NotificationInvalidationEventArgs(character, NotificationCategory.
                InsufficientBalance));
        }

        /// <summary>
        /// Notifies an account has an insufficient balance.
        /// </summary>
        /// <param name="character">The character.</param>
        internal void NotifyInsufficientBalance(CCPCharacter character)
        {
            var notification = new NotificationEventArgs(character, NotificationCategory.
                InsufficientBalance)
            {
                Description = string.Format(Properties.Resources.MessageMarginTrading,
                    character?.Name),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Warning
            };
            Notify(notification);
        }

        #endregion


        #region Skill completion

        /// <summary>
        /// Notifies a character finished training a skill.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="skillsCompleted">The completed skills.</param>
        internal void NotifySkillCompletion(CCPCharacter character,
            IEnumerable<QueuedSkill> skillsCompleted)
        {
            var notification = new SkillCompletionNotificationEventArgs(character,
                skillsCompleted)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion


        #region Skill queue less than threshold

        /// <summary>
        /// Invalidates the notification for skill queue availability.
        /// </summary>
        /// <param name="character">The character.</param>
        internal void InvalidateSkillQueueThreshold(CCPCharacter character)
        {
            Invalidate(new NotificationInvalidationEventArgs(character, NotificationCategory.
                SkillQueueRoomAvailable));
        }

        /// <summary>
        /// Notify when we have room to queue more skills.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="threshold">The number of days to which the warning is set.</param>
        internal void NotifySkillQueueThreshold(CCPCharacter character, int threshold)
        {
            string text, name = character?.Name;
            if (threshold == 1)
                text = "a";
            else
                text = threshold.ToString();
            var notification = new NotificationEventArgs(character, NotificationCategory.
                SkillQueueRoomAvailable)
            {
                Description = string.Format(Properties.Resources.MessageLessThanDay,
                    character?.Name, text, threshold.S()),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Warning
            };
            Notify(notification);
        }

        #endregion


        #region Server status

        /// <summary>
        /// Invalidates the notification for the server status.
        /// </summary>
        internal void InvalidateServerStatusChange()
        {
            Invalidate(new NotificationInvalidationEventArgs(null, NotificationCategory.
                ServerStatusChange));
        }

        /// <summary>
        /// Notifies about the server status.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="status">The status.</param>
        internal void NotifyServerStatusChanged(string serverName, ServerStatus status)
        {
            string text = string.Empty;
            switch (status)
            {
            case ServerStatus.Offline:
                text = $"{serverName} is offline.";
                break;
            case ServerStatus.Online:
                text = $"{serverName} is online.";
                break;
            case ServerStatus.CheckDisabled:
            case ServerStatus.Unknown:
                break;
            default:
                throw new NotImplementedException();
            }

            if (!string.IsNullOrEmpty(text))
            {
                var notification = new NotificationEventArgs(null, NotificationCategory.
                    ServerStatusChange)
                {
                    Description = text,
                    Behaviour = NotificationBehaviour.Overwrite,
                    Priority = NotificationPriority.Information
                };
                Notify(notification);
            }
        }

        #endregion


        #region Market orders expiration

        /// <summary>
        /// Notify some character market orders have been expired or fulfilled.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="expiredOrders">The expired orders.</param>
        internal void NotifyCharacterMarketOrdersEnded(Character character,
            IEnumerable<MarketOrder> expiredOrders)
        {
            var notification = new MarketOrdersNotificationEventArgs(character, expiredOrders)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        /// <summary>
        /// Notify some corporation market orders have been expired or fulfilled.
        /// </summary>
        /// <param name="corporation">The corporation.</param>
        /// <param name="expiredOrders">The expired orders.</param>
        internal void NotifyCorporationMarketOrdersEnded(Corporation corporation,
            IEnumerable<MarketOrder> expiredOrders)
        {
            var notification = new MarketOrdersNotificationEventArgs(corporation, expiredOrders)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion


        #region Contracts expiration

        /// <summary>
        /// Notify some character contracts have been expired or fulfilled.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="endedContracts">The ended contracts.</param>
        public void NotifyCharacterContractsEnded(Character character,
            IEnumerable<Contract> endedContracts)
        {
            var notification = new ContractsNotificationEventArgs(character, endedContracts)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        /// <summary>
        /// Notify some corporation contracts have been expired or fulfilled.
        /// </summary>
        /// <param name="corporation">The corporation.</param>
        /// <param name="endedContracts">The ended contracts.</param>
        public void NotifyCorporationContractsEnded(Corporation corporation,
            IEnumerable<Contract> endedContracts)
        {
            var notification = new ContractsNotificationEventArgs(corporation, endedContracts)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion


        #region Contracts assigned

        /// <summary>
        /// Invalidates the notification for assigned contracts.
        /// </summary>
        /// <param name="character">The character.</param>
        internal void InvalidateCharacterContractsAssigned(CCPCharacter character)
        {
            Invalidate(new NotificationInvalidationEventArgs(character, NotificationCategory.
                ContractsAssigned));
        }

        /// <summary>
        /// Notifies for character assigned contracts.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="assignedContracts">The assigned contracts.</param>
        public void NotifyCharacterContractsAssigned(Character character, int assignedContracts)
        {
            var notification = new NotificationEventArgs(character, NotificationCategory.
                ContractsAssigned)
            {
                Description = string.Format(Properties.Resources.MessageNewContracts,
                    assignedContracts, assignedContracts.S()),
                Behaviour = NotificationBehaviour.Overwrite,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion


        #region Industry jobs completion

        /// <summary>
        /// Notify some character industry jobs have ended.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="jobsCompleted">The completed jobs.</param>
        internal void NotifyCharacterIndustryJobCompletion(Character character,
            IEnumerable<IndustryJob> jobsCompleted)
        {
            var notification = new IndustryJobsNotificationEventArgs(character, jobsCompleted)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        /// <summary>
        /// Notify some corporation industry jobs have ended.
        /// </summary>
        /// <param name="corporation">The corporation.</param>
        /// <param name="jobsCompleted">The completed jobs.</param>
        internal void NotifyCorporationIndustryJobCompletion(Corporation corporation,
            IEnumerable<IndustryJob> jobsCompleted)
        {
            var notification = new IndustryJobsNotificationEventArgs(corporation, jobsCompleted)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion


        #region Planetary Pins expiration

        /// <summary>
        /// Invalidates the notification for completed planetary pins.
        /// </summary>
        /// <param name="character">The character.</param>
        internal void InvalidateCharacterPlanetaryPinCompleted(CCPCharacter character)
        {
            Invalidate(new NotificationInvalidationEventArgs(character, NotificationCategory.
                PlanetaryPinsCompleted));
        }

        /// <summary>
        /// Notify some character planetary pins have ended.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="pinsCompleted">The completed pins.</param>
        internal void NotifyCharacterPlanetaryPinCompleted(Character character,
            IEnumerable<PlanetaryPin> pinsCompleted)
        {
            var notification = new PlanetaryPinsNotificationEventArgs(character, pinsCompleted)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }


        #endregion


        #region New EVE mail message

        /// <summary>
        /// Notify new EVE mail message is available.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="newMessages">The new messages.</param>
        internal void NotifyNewEVEMailMessages(Character character, int newMessages)
        {
            var notification = new EveMailMessageNotificationEventArgs(character, newMessages)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion


        #region New EVE notification

        /// <summary>
        /// Notify new EVE notification is available.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="newNotifications">The new notifications.</param>
        internal void NotifyNewEVENotifications(Character character, int newNotifications)
        {
            var notification = new EveNotificationEventArgs(character, newNotifications)
            {
                Behaviour = NotificationBehaviour.Merge,
                Priority = NotificationPriority.Information
            };
            Notify(notification);
        }

        #endregion

    }
}
