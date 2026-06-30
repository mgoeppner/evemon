using System;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Models;

namespace EVEMon.Common.QueryMonitor
{
    /// <summary>
    /// Represents a monitor for all queries related to corporations.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    public sealed class CorporationQueryMonitor<T> : CCPQueryMonitorBase<T> where T : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="character">The character to monitor.</param>
        /// <param name="method">The method to use.</param>
        /// <param name="onSuccess">An action to call on success.</param>
        /// <param name="onFailure">The callback to use upon failure.</param>
        internal CorporationQueryMonitor(CCPCharacter character, Enum method, Action<T>
            onSuccess, NotifyErrorCallback onFailure) : base(character, method, (result) =>
            {
                if (character.Monitored)
                {
                    // "No corp role(s)" = 403
                    if (result.HasError)
                    {
                        // Do not invoke onFailure on corp roles error since we cannot actually
                        // determine whether the key had the roles until we try
                        if (result.ErrorCode != 403 && character.ShouldNotifyError(result,
                                method))
                            onFailure.Invoke(character, result);
                    }
                    else if (result.HasData)
                        onSuccess.Invoke(result.Result);
                }
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
                ParamOne = m_character.CorporationID
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
                m_apiKey = m_character.Identity.FindAPIKeyWithAccess((ESIAPICorporationMethods)
                    Method);
                return !m_character.IsInNPCCorporation && m_apiKey != null;
            }
        }
    }
}
