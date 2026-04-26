using System;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using EVEMon.Common.Serialization.Eve;

namespace EVEMon.Common.QueryMonitor
{
    public sealed class ESIKeyQueryMonitor<T> : QueryMonitor<T> where T : class
    {
        private readonly ESIKey m_esiKey;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="method"></param>
        /// <param name="onUpdated"></param>
        internal ESIKeyQueryMonitor(ESIKey apiKey, Enum method, Action<EsiResult<T>> onUpdated)
            : base(method, onUpdated)
        {
            m_esiKey = apiKey;
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
                if (Method is ESIAPIGenericMethods)
                    return true;

                ulong method = (ulong)(ESIAPICharacterMethods)Method;
                return method == (m_esiKey.AccessMask & method);
            }
        }

        /// <summary>
        /// Gets whether the bound ESI key currently has a usable access token.
        /// </summary>
        internal override bool CanQueryNow => m_esiKey?.HasUsableAccessToken ?? false;

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

            provider.QueryEsi(Method, callback, new ESIParams(LastResult?.Response, m_esiKey.
                GetAccessTokenForQuery()));
        }
    }
}
