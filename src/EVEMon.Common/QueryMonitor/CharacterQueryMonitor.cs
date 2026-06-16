using System;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Serialization.Eve;

namespace EVEMon.Common.QueryMonitor
{
    /// <summary>
    /// Represents a monitor for all queries related to characters.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    public sealed class CharacterQueryMonitor<T> : CCPQueryMonitorBase<T> where T : class
    {
        /// <summary>
        /// Handles the response from an ESI query.
        /// </summary>
        /// <param name="character">The character to monitor.</param>
        /// <param name="method">The method to use.</param>
        /// <param name="ifNoData">Whether to invoke the success callback on a "No Data"
        /// response.</param>
        /// <param name="result">The result of the query.</param>
        /// <param name="onSuccess">An action to call on success.</param>
        /// <param name="onFailure">The callback to use upon failure.</param>
        private static void HandleQuery(CCPCharacter character, Enum method, bool ifNoData,
            EsiResult<T> result, Action<T> onSuccess, NotifyErrorCallback onFailure)
        {
            // Character may have been set to not be monitored
            if (character.Monitored)
            {
                bool hasData = result.HasData;
                if (character.ShouldNotifyError(result, method))
                    onFailure.Invoke(character, result);
                if (!result.HasError && (ifNoData || hasData))
                    onSuccess.Invoke(hasData ? result.Result : null);
            }
            foreach (var monitor in character.QueryMonitors)
                if (monitor.Method.HasParent(method))
                    character.QueryMonitors.Query(monitor.Method);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="character">The character to monitor.</param>
        /// <param name="method">The method to use.</param>
        /// <param name="onSuccess">An action to call on success.</param>
        /// <param name="onFailure">The callback to use upon failure.</param>
        /// <param name="ifNoData">If true, the success callback is invoked even if the
        /// response returned "Not Modified" or "No Data"; if false (default), no action is
        /// called if the response is empty</param>
        internal CharacterQueryMonitor(CCPCharacter character, Enum method, Action<T>
            onSuccess, NotifyErrorCallback onFailure, bool ifNoData = false) : base(character,
            method, (result) => {
                HandleQuery(character, method, ifNoData, result, onSuccess, onFailure);
            })
        {
        }

        /// <summary>
        /// Retrieves the parameters required for the ESI request.
        /// </summary>
        /// <returns>The ESI request parameters.</returns>
        internal override ESIParams GetESIParams()
        {
            return new ESIParams(LastResult?.Response, m_apiKey.GetAccessTokenForQuery())
            {
                ParamOne = m_character.CharacterID
            };
        }
        
        /// <summary>
        /// Gets a value indicating whether this monitor has access to data.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this monitor has access; otherwise, <c>false</c>.
        /// </value>
        public override bool HasAccess
        {
            get
            {
                m_apiKey = m_character.Identity.FindAPIKeyWithAccess((ESIAPICharacterMethods)
                    Method);
                return m_apiKey != null;
            }
        }
    }
}
