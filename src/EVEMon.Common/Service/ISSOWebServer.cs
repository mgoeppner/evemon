using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVEMon.Common.Service
{
    /// <summary>
    /// The parent of both implementations for SSOWebServer.
    /// 
    /// SSO was really meant for web apps so this is the best we can do...
    /// </summary>
    interface ISSOWebServer : IDisposable
    {
        /// <summary>
        /// Asynchronously waits for an auth code in the background.
        /// </summary>
        /// <param name="state">The SSO state used.</param>
        /// <param name="callback">The callback which will be invoked when the code is
        /// received, reception fails, or the server is stopped.</param>
        void BeginWaitForCode(string state, Action<Task<string>> callback);

        /// <summary>
        /// Starts the web server.
        /// </summary>
        Task StartAsync();

        /// <summary>
        /// Stops the web server.
        /// </summary>
        Task StopAsync();

        /// <summary>
        /// Waits for the auth code asynchronously; the reported state must match the argument.
        /// </summary>
        /// <param name="state">The SSO state.</param>
        /// <returns>The token received, or null if none was received.</returns>
        Task<string> WaitForCodeAsync(string state);
    }
}
