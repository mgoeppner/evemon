using System;
using System.Collections.Generic;
using System.Linq;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Extensions;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Serialization.Eve;
using EVEMon.Common.Service;
using EVEMon.Common.Serialization.Esi;
using EVEMon.Common.Net;

namespace EVEMon.Common.Models
{
    public sealed class EveMailMessage : IEveMessage
    {
        // Returned if the message somehow has no sender, since returning an empty list throws
        private static readonly string[] NO_SENDER = { "" };

        private readonly SerializableMailMessagesListItem m_source;
        private readonly CCPCharacter m_ccpCharacter;

        private ResponseParams m_bodyResponse;
        private bool m_queryPending;
        private string m_senderName;
        private IEnumerable<string> m_toMailingLists;
        private IEnumerable<string> m_toCharacters;
        private string m_toCorpOrAlliance;


        #region Constructor

        /// <summary>
        /// Constructor from the API.
        /// </summary>
        /// <param name="ccpCharacter"></param>
        /// <param name="src"></param>
        internal EveMailMessage(CCPCharacter ccpCharacter, SerializableMailMessagesListItem src)
        {
            if (ccpCharacter == null)
                throw new ArgumentNullException("ccpCharacter");

            m_ccpCharacter = ccpCharacter;
            m_source = src;
            m_bodyResponse = null;

            long senderID = src.SenderID;
            State = (senderID != ccpCharacter.CharacterID) ? EveMailState.Inbox :
                EveMailState.SentItem;
            MessageID = src.MessageID;
            SentDate = src.SentDate;
            Title = src.Title.HtmlDecode();

            // Was it sent from a mailing list?
            if (src.ToListID.Contains(senderID))
                m_senderName = ccpCharacter.EVEMailingLists.FirstOrDefault(x => x.ID ==
                    senderID)?.Name ?? EveMonConstants.UnknownText;
            else if (senderID == 0L)
                m_senderName = EveMonConstants.UnknownText;
            else
                m_senderName = EveIDToName.GetIDToName(senderID);
            m_toCharacters = GetIDsToNames(src.ToCharacterIDs);
            m_toMailingLists = GetMailingListIDsToNames(src.ToListID);
            m_toCorpOrAlliance = EveIDToName.GetIDToName(src.ToCorpOrAllianceID);

            EVEMailBody = new EveMailBody(0L, new EsiAPIMailBody() { Body = "" });
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the EVE mail state.
        /// </summary>
        /// <value>The state.</value>
        public EveMailState State { get; }

        /// <summary>
        /// Gets or sets the EVE mail message ID.
        /// </summary>
        /// <value>The message ID.</value>
        public long MessageID { get; }

        /// <summary>
        /// Gets or sets the EVE mail sender name.
        /// </summary>
        /// <value>The sender.</value>
        public string SenderName => m_senderName.IsEmptyOrUnknown() ? (m_senderName =
            EveIDToName.GetIDToName(m_source.SenderID)) : m_senderName;

        /// <summary>
        /// Gets or sets the sent date of the EVE mail.
        /// </summary>
        /// <value>The sent date.</value>
        public DateTime SentDate { get; }

        /// <summary>
        /// Gets or sets the EVE mail title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; }

        /// <summary>
        /// Gets or sets the EVE mail recipient (corp/alliance).
        /// 
        /// If it did not parse in the first place, m_toCorpOrAlliance != EveMonConstants.UnknownText,
        /// so this parse cannot fail
        /// </summary>
        public string ToCorpOrAlliance => m_toCorpOrAlliance.IsEmptyOrUnknown() ?
            (m_toCorpOrAlliance = EveIDToName.GetIDToName(m_source.ToCorpOrAllianceID)) :
            m_toCorpOrAlliance;

        /// <summary>
        /// Gets or sets the EVE mail recipient(s) (characters).
        /// </summary>
        public IEnumerable<string> ToCharacters => m_toCharacters.Contains(EveMonConstants.UnknownText)
            ? m_toCharacters = GetIDsToNames(m_source.ToCharacterIDs) : m_toCharacters;

        /// <summary>
        /// Gets or sets the EVE mail recipient (mailing lists).
        /// </summary>
        public IEnumerable<string> ToMailingLists => m_toMailingLists.Contains(EveMonConstants.UnknownText)
            ? m_toMailingLists = GetMailingListIDsToNames(m_source.ToListID) : m_toMailingLists;

        /// <summary>
        /// Gets the recipient.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Recipient => !string.IsNullOrEmpty(ToCharacters.FirstOrDefault())
            ? ToCharacters : (!string.IsNullOrEmpty(ToCorpOrAlliance) ? new List<string>
            {
                ToCorpOrAlliance
            } : (!string.IsNullOrEmpty(ToMailingLists.FirstOrDefault()) ? ToMailingLists :
            Enumerable.Empty<string>()));

        /// <summary>
        /// Gets or sets the EVE mail body.
        /// </summary>
        /// <value>The EVE mail body.</value>
        public EveMailBody EVEMailBody { get; private set; }

        /// <summary>
        /// Gets the EVE mail body text.
        /// </summary>
        /// <value>The text.</value>
        public string Text => EVEMailBody.BodyText.Normalize();

        #endregion


        #region Helper Methods

        /// <summary>
        /// Gets the names of the IDs.
        /// </summary>
        /// <param name="src">A list of IDs.</param>
        /// <returns>A list of names</returns>
        private IEnumerable<string> GetIDsToNames(ICollection<long> src)
        {
            // If there are no IDs to query return an empty list
            if (!src.Any())
                return NO_SENDER;
            
            return EveIDToName.GetIDsToNames(src).Select(x => x ?? EveMonConstants.UnknownText);
        }

        /// <summary>
        /// Gets the mailing list IDs to names.
        /// </summary>
        /// <param name="mailingListIDs">The mailing list IDs.</param>
        /// <returns></returns>
        private IEnumerable<string> GetMailingListIDsToNames(ICollection<long> mailingListIDs)
        {
            // If there are no IDs to query return a list with an empty entry
            if (!mailingListIDs.Any())
                return NO_SENDER;

            List<string> listOfNames = mailingListIDs.Select(listID => m_ccpCharacter.
                EVEMailingLists.FirstOrDefault(x => x.ID == listID)).Select(mailingList =>
                mailingList?.Name ?? EveMonConstants.UnknownText).ToList();
            // In case the list returned from the API is empty, add an "Unknown" entry
            if (!listOfNames.Any())
                listOfNames.Add(EveMonConstants.UnknownText);
            return listOfNames;
        }

        #endregion


        #region Querying

        /// <summary>
        /// Gets the EVE mail body.
        /// </summary>
        public void GetMailBody()
        {
            // Exit if we are already trying to download the mail message body text
            if (!m_queryPending && !EsiErrors.IsErrorCountExceeded)
            {
                var apiKey = m_ccpCharacter.Identity.FindAPIKeyWithAccess(
                    ESIAPICharacterMethods.MailBodies);
                string accessToken = apiKey?.GetAccessTokenForQuery();
                // Skip if no key or the token is not usable yet (e.g. refresh after resume).
                // GetMailBody is only invoked from CharacterEveMailMessagesList.
                // OnSelectionChanged, so the body for the current selection will stay
                // empty until the user re-selects this message after the refresh settles.
                // Acceptable trade-off vs firing a request that would 401 anyway.
                if (apiKey == null || string.IsNullOrEmpty(accessToken))
                    return;
                m_queryPending = true;
                EveMonClient.APIProviders.CurrentProvider.QueryEsi<EsiAPIMailBody>(
                    ESIAPICharacterMethods.MailBodies, OnEVEMailBodyDownloaded,
                    new ESIParams(m_bodyResponse, accessToken)
                    {
                        ParamOne = m_ccpCharacter.CharacterID,
                        ParamTwo = MessageID
                    }, MessageID);
            }
        }

        /// <summary>
        /// Processes the queried EVE mail message mail body.
        /// </summary>
        /// <param name="result">The result.</param>
        private void OnEVEMailBodyDownloaded(EsiResult<EsiAPIMailBody> result, object forMessage)
        {
            long messageID = (forMessage as long?) ?? 0L;
            m_queryPending = false;
            m_bodyResponse = result.Response;
            // Notify if an error occured
            if (m_ccpCharacter.ShouldNotifyError(result, ESIAPICharacterMethods.MailBodies))
                EveMonClient.Notifications.NotifyEVEMailBodiesError(m_ccpCharacter, result);
            if (result.HasData && !result.HasError && messageID != 0L && !string.IsNullOrEmpty(
                result.Result.Body))
            {
                EVEMailBody = new EveMailBody(messageID, result.Result);
                EveMonClient.OnCharacterEVEMailBodyDownloaded(m_ccpCharacter);
            }
        }

        #endregion
    }
}
