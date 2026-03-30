using System;
using System.Linq;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Serialization.Eve;

namespace EVEMon.Common.QueryMonitor
{
    /// <summary>
    /// Represents a monitor for all queries related to characters and their corporations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CCPQueryMonitorBase<T> : QueryMonitor<T> where T : class
    {
        protected readonly CCPCharacter m_character;
        protected ESIKey m_apiKey;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="character">The character to monitor.</param>
        /// <param name="method">The method to use.</param>
        /// <param name="onSuccess">The callback to use on success or failure.</param>
        internal CCPQueryMonitorBase(CCPCharacter character, Enum method,
            Action<EsiResult<T>> callback) : base(method, callback)
        {
            m_character = character;
        }

        /// <summary>
        /// Retrieves the parameters required for the ESI request.
        /// </summary>
        /// <returns>The ESI request parameters.</returns>
        internal abstract ESIParams GetESIParams();

        /// <summary>
        /// Gets the required API key information are known.
        /// </summary>
        /// <returns>False if an API key was required and not found.</returns>
        internal override bool HasESIKey => m_character.Identity.ESIKeys.Any();

        /// <summary>
        /// Gets whether the bound ESI key currently has a usable access token.
        /// </summary>
        protected override bool CanQueryNow => m_apiKey?.HasUsableAccessToken ?? false;

        /// <summary>
        /// Performs the query to the provider, passing the required arguments.
        /// </summary>
        /// <param name="provider">The API provider to use.</param>
        /// <param name="callback">The callback invoked on the UI thread after a result has
        /// been queried.</param>
        /// <exception cref="System.ArgumentNullException">provider</exception>
        protected override void QueryAsyncCore(APIProvider provider, APIProvider.
            ESIRequestCallback<T> callback)
        {
            provider.ThrowIfNull(nameof(provider));

            provider.QueryEsi(Method, callback, GetESIParams());
        }
    }
}
