using EVEMon.Common.Constants;
using EVEMon.Common.Helpers;
using EVEMon.Common.Threading;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EVEMon.Common.Service
{
    /// <summary>
    /// A simple web server that is used to receive callback information from SSO using
    /// HttpListener.
    /// 
    /// SSO was really meant for web apps so this is the best we can do...
    /// </summary>
    public class SSOWebServerHttpListener : ISSOWebServer
    {
        // A random port would be nice, but the API requires a fixed callback URL
        public const int PORT = 4916;
        // Used for initializing the responses properly
        private static readonly object RESPONSE_LOCK = new object();

        // Encoded responses for client requests
        private static byte[] responseOK = null;
        private static byte[] response404 = null;

        // Initializes the text responses sent to the client
        private static void InitResponses()
        {
            lock (RESPONSE_LOCK)
            {
                if (response404 == null || responseOK == null)
                {
                    response404 = Encoding.UTF8.GetBytes(Properties.Resources.CallbackFail);
                    responseOK = Encoding.UTF8.GetBytes(Properties.Resources.CallbackOK);
                }
            }
        }

        // The TCP listener used to receive requests
        // We expect few requests, so we can get away with a single thread
        private readonly HttpListener listener;

        public SSOWebServerHttpListener()
        {
            if (!HttpListener.IsSupported)
                throw new InvalidOperationException("HTTP listener not supported");
            listener = new HttpListener();
            // Calculate prefix, must end with slash according to HttpListener documentation
            string prefix = string.Format(NetworkConstants.SSORedirect, PORT);
            if (!prefix.EndsWith("/"))
                prefix += "/";
            listener.Prefixes.Add(prefix);
            // Where would the exception go otherwise?
            listener.IgnoreWriteExceptions = true;
            InitResponses();
        }

        /// <summary>
        /// Asynchronously waits for an auth code in the background.
        /// </summary>
        /// <param name="state">The SSO state used.</param>
        /// <param name="callback">The callback which will be invoked when the code is
        /// received, reception fails, or the server is stopped.</param>
        public void BeginWaitForCode(string state, Action<Task<string>> callback)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException("state");
            WaitForCodeAsync(state).ContinueWith((result) => Dispatcher.Invoke(() => callback?.Invoke(result)));
        }

        public void Dispose()
        {
            listener.Stop();
            listener.Close();
        }

        /// <summary>
        /// Responds to the client which requests the specified URL.
        /// </summary>
        /// <param name="state">The SSO state used.</param>
        /// <param name="output">The response where the output will be sent.</param>
        /// <param name="queryParams">The arguments from the query.</param>
        /// <returns></returns>
        private async Task<string> SendReponseAsync(string state, HttpListenerResponse output,
            NameValueCollection queryParams)
        {
            string code = "";
            byte[] response;
            HttpStatusCode responseCode;
            // Check for matching state in response
            var stateParams = queryParams.GetValues("state");
            if (stateParams != null && stateParams.Length == 1 && stateParams[0] == state)
            {
                var codeParams = queryParams.GetValues("code");
                // Take the first one, only should be one
                if (codeParams != null && codeParams.Length > 0)
                    code = codeParams[0];
            }
            // Choose the right response
            if (string.IsNullOrEmpty(code))
            {
                response = response404;
                responseCode = HttpStatusCode.NotFound;
            }
            else
            {
                response = responseOK;
                responseCode = HttpStatusCode.OK;
            }
            // Send the response
            using (var stream = output.OutputStream)
            {
                int len = response.Length;
                // HTTP response code
                output.StatusCode = (int)responseCode;
                // Supply the length
                output.ContentLength64 = len;
                // Supply content type and encoding
                output.ContentType = "text/html";
                output.ContentEncoding = Encoding.UTF8;
                await stream.WriteAsync(response, 0, len);
                await stream.FlushAsync();
            }
            return code;
        }

        /// <summary>
        /// Starts the web server.
        /// </summary>
        public void Start()
        {
            try
            {
                listener.Start();
            }
            catch (HttpListenerException e)
            {
                ExceptionHandler.LogException(e, true);
                throw new IOException("Error when starting server", e);
            }
        }

        /// <summary>
        /// Stops the web server.
        /// </summary>
        public void Stop()
        {
            try
            {
                listener.Stop();
            }
            catch (HttpListenerException e)
            {
                ExceptionHandler.LogException(e, true);
            }
        }

        /// <summary>
        /// Waits for the auth code asynchronously; the reported state must match the argument.
        /// </summary>
        /// <param name="state">The SSO state.</param>
        /// <returns>The token received, or null if none was received.</returns>
        public async Task<string> WaitForCodeAsync(string state)
        {
            // Blank states are bad
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException("state");
            string code = string.Empty;
            try
            {
                do
                {
                    // Accept client
                    var context = await listener.GetContextAsync().ConfigureAwait(false);
                    using (var output = context.Response)
                    {
                        // Check for state in the URL
                        string query = context.Request.Url.Query;
                        if (query == null)
                            query = "";
                        var queryParams = HttpUtility.ParseQueryString(query);
                        code = await SendReponseAsync(state, output, queryParams);
                    }
                } while (string.IsNullOrEmpty(code));
            }
            catch (ObjectDisposedException)
            {
                // Happens normally while shutting down
            }
            catch (HttpListenerException e)
            {
                throw new IOException("Error when waiting for auth code", e);
            }
            return code;
        }
    }
}
