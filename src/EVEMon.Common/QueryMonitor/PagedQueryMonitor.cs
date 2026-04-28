using EVEMon.Common.Extensions;
using EVEMon.Common.Models;
using System.Collections.Generic;

namespace EVEMon.Common.QueryMonitor
{
    /// <summary>
    /// Represents a monitor for all queries related to characters and their corporations,
    /// supporting paged requests.
    /// </summary>
    /// <typeparam name="T">The outer container type.</typeparam>
    /// <typeparam name="U">The inner result type.</typeparam>
    public sealed class PagedQueryMonitor<T, U> : QueryMonitor<T> where T : List<U>
        where U : class
    {
        private readonly CCPQueryMonitorBase<T> wrapped;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wrapped">The query monitor to wrap.</param>
        internal PagedQueryMonitor(CCPQueryMonitorBase<T> wrapped) : base(wrapped.Method,
            wrapped.Callback)
        {
            this.wrapped = wrapped;
        }

        /// <summary>
        /// Gets the required API key information are known.
        /// </summary>
        /// <returns>False if an API key was required and not found.</returns>
        internal override bool HasESIKey => wrapped.HasESIKey;

        /// <summary>
        /// Gets a value indicating whether this monitor has access to data.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this monitor has access; otherwise, <c>false</c>.
        /// </value>
        public override bool HasAccess => wrapped.HasAccess;

        /// <summary>
        /// Forwards the wrapped monitor's gate so paged queries also wait while an access
        /// token refresh is in flight. Without this, paged endpoints (assets, contracts,
        /// wallet, etc.) would bypass the refresh gate and fire requests with a null token.
        /// </summary>
        internal override bool CanQueryNow => wrapped.CanQueryNow;

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

            provider.QueryPagedEsi<T, U>(Method, callback, wrapped.GetESIParams());
        }
    }
}
