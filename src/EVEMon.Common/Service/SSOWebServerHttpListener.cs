﻿using EVEMon.Common.Constants;
using EVEMon.Common.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
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

            listener.MapGet("/callback", HandleCallback);
        }

        private async void HandleCallback(HttpContext context, [FromQuery] string code, [FromQuery] string state)
        {
            if (codeCompletionSource != null)
            {
                codeCompletionSource.SetResult((code, state));
            }

            await SendReponseAsync(context, code, state);
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
            WaitForCodeAsync(state).ContinueWith((result) => Dispatcher.Invoke(() =>
                callback?.Invoke(result)));
        }

        public async void Dispose()
        {
            try
            {
                await listener.StopAsync();
            }
            catch
            {
                // Ignore
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
        public async void Start()
        {
            await listener.RunAsync();
        }

        /// <summary>
        /// Stops the web server.
        /// </summary>
        public async void Stop()
        {
            await listener.StopAsync();
        }

        /// <summary>
        /// Waits for the auth code asynchronously; the reported state must match the argument.
        /// </summary>
        /// <param name="expectedState">The SSO state.</param>
        /// <returns>The token received, or null if none was received.</returns>
        public async Task<string> WaitForCodeAsync(string expectedState)
        {
            codeCompletionSource = new();

            var (code, state) = await codeCompletionSource.Task;

            if (expectedState == state)
            {
                return code; 
            }

            return null;
        }
    }
}
