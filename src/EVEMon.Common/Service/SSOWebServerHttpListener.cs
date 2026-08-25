using EVEMon.Common.Constants;
using EVEMon.Common.Helpers;
using EVEMon.Common.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        private WebApplication listener;

        // Encoded responses for client requests
        private static byte[] responseOK = null;
        private static byte[] response404 = null;


        private TaskCompletionSource<(string, string)> codeCompletionSource;

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

        public SSOWebServerHttpListener()
        {
            // Calculate prefix, must end with slash according to HttpListener documentation
            string prefix = string.Format(NetworkConstants.SSORedirect, PORT);
            if (!prefix.EndsWith("/"))
                prefix += "/";

            InitResponses();

            var builder = WebApplication.CreateBuilder();
            builder.WebHost.ConfigureKestrel(kestrel =>
            {
                kestrel.ListenLocalhost(PORT);
            });

            listener = builder.Build();

            listener.MapGet("/callback", HandleCallbackAsync);
        }

        private Task HandleCallbackAsync(HttpContext context, [FromQuery] string code, [FromQuery] string state)
        {
            if (codeCompletionSource != null)
            {
                codeCompletionSource.TrySetResult((code, state));
            }

            return SendReponseAsync(context, code, state);
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
            _ = WaitForCodeAsync(state).ContinueWith((result) => Dispatcher.Invoke(() =>
                callback?.Invoke(result)), TaskScheduler.Default);
        }

        public void Dispose()
        {
            var host = Interlocked.Exchange(ref listener, null);
            if (host == null)
                return;

            // Unblock anyone awaiting the auth code. Without this the never-completed
            // TCS pins the BeginWaitForCode continuation, and through it the disposed
            // window; UpdateTokens already handles IsCanceled
            codeCompletionSource?.TrySetCanceled();

            // Shut down in the background; Dispose is called on the UI thread when the
            // owning form closes, and blocking it on Kestrel shutdown deadlocks
            _ = DisposeServerAsync(host);
        }

        private static async Task DisposeServerAsync(WebApplication host)
        {
            try
            {
                await host.StopAsync();
                await host.DisposeAsync();
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, true);
            }
        }

        /// <summary>
        /// Responds to the client which requests the specified URL.
        /// </summary>
        /// <param name="state">The SSO state used.</param>
        /// <param name="context">The response where the output will be sent.</param>
        /// <returns></returns>
        private async Task SendReponseAsync(HttpContext context, string code, string state)
        {
            byte[] response;
            HttpStatusCode responseCode;

            // Choose the right response
            if (string.IsNullOrEmpty(state) || string.IsNullOrEmpty(code))
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
            using (var stream = context.Response.BodyWriter.AsStream())
            {
                int len = response.Length;
                // HTTP response code
                context.Response.StatusCode = (int)responseCode;

                // Supply content type and encoding
                context.Response.ContentType = "text/html";
                await stream.WriteAsync(response, 0, len);
                await stream.FlushAsync();
            }
        }

        /// <summary>
        /// Starts the web server.
        /// </summary>
        public Task StartAsync()
        {
            var host = listener ?? throw new ObjectDisposedException(nameof(SSOWebServerHttpListener));
            return host.StartAsync();
        }

        /// <summary>
        /// Stops the web server.
        /// </summary>
        public Task StopAsync()
        {
            var host = listener ?? throw new ObjectDisposedException(nameof(SSOWebServerHttpListener));
            return host.StopAsync();
        }

        /// <summary>
        /// Waits for the auth code asynchronously; the reported state must match the argument.
        /// </summary>
        /// <param name="expectedState">The SSO state.</param>
        /// <returns>The token received, or null if none was received.</returns>
        public async Task<string> WaitForCodeAsync(string expectedState)
        {
            // Run continuations asynchronously so completing the TCS from Kestrel's
            // request thread never runs UI-bound continuations inline on it
            codeCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

            var (code, state) = await codeCompletionSource.Task;

            if (expectedState == state)
            {
                return code; 
            }

            return null;
        }
    }
}
