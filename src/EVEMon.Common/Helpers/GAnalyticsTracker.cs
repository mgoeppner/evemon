using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common.Constants;
using EVEMon.Common.Extensions;
using EVEMon.Common.Net;
using EVEMon.Common.Threading;

namespace EVEMon.Common.Helpers
{
    public static class GAnalyticsTracker
    {
        private static readonly GampParameters s_parameters;

        private const string DailyStartText = "Daily-Start";

        /// <summary>
        /// Initializes the <see cref="GAnalyticsTracker"/> class.
        /// </summary>
        static GAnalyticsTracker()
        {
            string userAgentId = Util.Decrypt("+PzrIVxVhL3PBJcRYN2tXg==", CultureConstants.InvariantCulture.NativeName);
            string clientId = EveMonClient.IsDebugBuild ? "2" : "1";

            s_parameters = new GampParameters
            {
                ProtocolVersion = "1",
                TrackerId = $"{userAgentId}-{clientId}",
                AnonymizeIp = true,
                ClientId = Util.CreateSHA1SumFromMacAddress(),
                ApplicationName = EveMonClient.FileVersionInfo.ProductName,
                ApplicationVersion = EveMonClient.FileVersionInfo.FileVersion,
                ScreenResolution = $"{Screen.PrimaryScreen.WorkingArea.Width}x{Screen.PrimaryScreen.WorkingArea.Height}",
                UserAgent = HttpWebClientServiceState.UserAgent.ToString()
            };
        }

        /// <summary>
        /// Tracks the starting of the application asynchronously.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        /// <remarks>Default action is 'Start'</remarks>
        public static void TrackStart(Type type, string action = null)
        {
            if (string.IsNullOrWhiteSpace(action))
                action = SessionStatus.Start.ToString();

            if (action != SessionStatus.Start.ToString() && action != DailyStartText)
            {
                throw new ArgumentException($"Only actions '{SessionStatus.Start}' and '{DailyStartText}' are allowed.");
            }

            TrackEventAsync(type, "ApplicationLifecycle", action);
        }

        /// <summary>
        /// Tracks the ending of the application.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void TrackEnd(Type type)
        {
            TrackEvent(type, "ApplicationLifecycle", SessionStatus.End.ToString());
        }

        /// <summary>
        /// Tracks the event.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="category">The category.</param>
        /// <param name="action">The action.</param>
        private static void TrackEvent(Type type, string category, string action)
        {
            InitEvent(type, category, action);
            if (NetworkMonitor.IsNetworkAvailable)
            {
                var result = HttpWebClientService.DownloadImage(new Uri(NetworkConstants.
                    GoogleAnalyticsUrl), new RequestParams(BuildQueryString()));
                if (EveMonClient.IsDebugBuild)
                {
                    // Trace the error (if any) and event category
                    EveMonClient.Trace($"({category} - {action})");
                    if (result.Error != null)
                        EveMonClient.Trace($"{result.Error.Message}");
                }
            }
            else
            {
                // Reschedule later
                Dispatcher.Schedule(TimeSpan.FromMinutes(1), () => TrackEvent(type, category,
                    action));
                if (EveMonClient.IsDebugBuild)
                    EveMonClient.Trace($"in {TimeSpan.FromMinutes(1)}");
            }
        }

        /// <summary>
        /// Tracks the event asynchronously.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="category">The category.</param>
        /// <param name="action">The action.</param>
        private static void TrackEventAsync(Type type, string category, string action)
        {
            InitEvent(type, category, action);

            // Sent notification
            if (NetworkMonitor.IsNetworkAvailable)
            {
                HttpWebClientService.DownloadImageAsync(new Uri(NetworkConstants.GoogleAnalyticsUrl),
                    new RequestParams(BuildQueryString())).ContinueWith(task =>
                {
                    if (EveMonClient.IsDebugBuild)
                    {
                        EveMonClient.Trace($"GAnalyticsTracker.TrackEventAsync - ({category} - {action})",
                            printMethod: false);
                        if (task.Result.Error != null)
                            EveMonClient.Trace($"GAnalyticsTracker.TrackEventAsync - {task.Result.Error.Message}",
                                printMethod: false);
                        else
                            EveMonClient.Trace($"GAnalyticsTracker.TrackEventAsync - in {TimeSpan.FromDays(1)}",
                                printMethod: false);
                    }
                    Dispatcher.Schedule(TimeSpan.FromDays(1), () => TrackStart(type, DailyStartText));
                }, EveMonClient.CurrentSynchronizationContext);
            }
            else
            {
                // Reschedule later
                Dispatcher.Schedule(TimeSpan.FromMinutes(1), () => TrackEventAsync(type,
                    category, action));
                if (EveMonClient.IsDebugBuild)
                    EveMonClient.Trace($"GAnalyticsTracker.TrackEventAsync - in {TimeSpan.FromMinutes(1)}",
                        printMethod: false);
            }
        }

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="category">The category.</param>
        /// <param name="action">The action.</param>
        private static void InitEvent(Type type, string category, string action)
        {
            s_parameters.HitType = GaHitType.Event;
            s_parameters.ScreenName = type.Name;
            s_parameters.EventCategory = category;
            s_parameters.EventAction = action;

            SessionStatus status;
            s_parameters.SessionControl = Enum.TryParse(action, true, out status)
                ? status.ToString().ToLowerInvariant()
                : null;
        }

        /// <summary>
        /// Builds the query string.
        /// </summary>
        /// <returns></returns>
        private static string BuildQueryString()
        {
            StringBuilder sb = new StringBuilder();
            IDictionary<string, string> parameters = GetParametersAsDict();

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                sb.Append($"{parameter.Key}={Uri.EscapeDataString(parameter.Value)}");

                if (parameters.Keys.Last() != parameter.Key)
                    sb.Append("&");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the parameters as dictionary.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private static IDictionary<string, string> GetParametersAsDict()
        {
            IDictionary<string, string> parametersDict = new Dictionary<string, string>();

            foreach (PropertyInfo prop in s_parameters
                .GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                GampParameterAttribute attribute =
                    Attribute.GetCustomAttribute(prop, typeof(GampParameterAttribute), true) as GampParameterAttribute;

                if (attribute == null)
                    continue;

                object value = prop.GetValue(s_parameters, null);

                if (value == null)
                {
                    if (attribute.IsRequired)
                        prop.ThrowIfNull(prop.Name, $"{prop.Name} is a required parameter");

                    continue;
                }

                if (prop.PropertyType.IsEnum)
                    value = value.ToString().ToLowerInvariant();
                else if (prop.PropertyType == typeof(bool))
                    value = Convert.ToInt32(value);

                parametersDict.Add(attribute.Token, value.ToString());
            }

            return parametersDict;
        }


        #region Private Helper Classes

        private enum GaHitType
        {
            Event,
        }

        private enum SessionStatus
        {
            Start,
            End
        }

        private class GampParameters
        {
            /// <summary>
            /// Gets or sets the protocol version.
            /// </summary>
            /// <value>
            /// The protocol version.
            /// </value>
            [GampParameter("v", true)]
            internal string ProtocolVersion { get; set; }

            /// <summary>
            /// Gets or sets the tracker identifier.
            /// </summary>
            /// <value>
            /// The tracker identifier.
            /// </value>
            [GampParameter("tid", true)]
            internal string TrackerId { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [anonymize ip].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [anonymize ip]; otherwise, <c>false</c>.
            /// </value>
            [GampParameter("aip")]
            internal bool AnonymizeIp { get; set; }

            /// <summary>
            /// Gets or sets the client identifier.
            /// </summary>
            /// <value>
            /// The client identifier.
            /// </value>
            [GampParameter("cid", true)]
            internal string ClientId { get; set; }

            /// <summary>
            /// Gets or sets the type of the hit.
            /// </summary>
            /// <value>
            /// The type of the hit.
            /// </value>
            [GampParameter("t", true)]
            internal GaHitType HitType { get; set; }

            /// <summary>
            /// Gets or sets the name of the screen.
            /// </summary>
            /// <value>
            /// The name of the screen.
            /// </value>
            [GampParameter("cd", true)]
            internal string ScreenName { get; set; }

            /// <summary>
            /// Gets or sets the name of the application.
            /// </summary>
            /// <value>
            /// The name of the application.
            /// </value>
            [GampParameter("an", true)]
            internal string ApplicationName { get; set; }

            /// <summary>
            /// Gets or sets the control of the session.
            /// </summary>
            /// <value>
            /// The name of the screen.
            /// </value>
            [GampParameter("sc")]
            internal string SessionControl { get; set; }

            /// <summary>
            /// Gets or sets the resolution of the screen.
            /// </summary>
            /// <value>
            /// The name of the screen.
            /// </value>
            [GampParameter("sr")]
            internal string ScreenResolution { get; set; }

            /// <summary>
            /// Gets or sets the application version.
            /// </summary>
            /// <value>
            /// The application version.
            /// </value>
            [GampParameter("av")]
            internal string ApplicationVersion { get; set; }

            /// <summary>
            /// Gets or sets the event category.
            /// </summary>
            /// <value>
            /// The event category.
            /// </value>
            [GampParameter("ec")]
            internal string EventCategory { get; set; }

            /// <summary>
            /// Gets or sets the event action.
            /// </summary>
            /// <value>
            /// The event action.
            /// </value>
            [GampParameter("ea")]
            internal string EventAction { get; set; }

            /// <summary>
            /// Gets or sets the user agent info.
            /// </summary>
            /// <value>
            /// The name of the screen.
            /// </value>
            [GampParameter("ua")]
            internal string UserAgent { get; set; }

            /// <summary>
            /// Gets or sets the user language.
            /// </summary>
            /// <value>
            /// The name of the screen.
            /// </value>
            [GampParameter("ul")]
            internal string UserLanguage => Encoding.Default.BodyName;

            /// <summary>
            /// Gets or sets the document encoding.
            /// </summary>
            /// <value>
            /// The name of the screen.
            /// </value>
            [GampParameter("de")]
            internal string DocumentEncoding => CultureInfo.CurrentUICulture.Name;
        }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum)]
        private class GampParameterAttribute : Attribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GampParameterAttribute"/> class.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="isRequried">if set to <c>true</c> [is requried].</param>
            internal GampParameterAttribute(string token, bool isRequried = false)
            {
                Token = token;
                IsRequired = isRequried;
            }

            /// <summary>
            /// Gets the token.
            /// </summary>
            /// <value>
            /// The token.
            /// </value>
            internal string Token { get; }

            /// <summary>
            /// Gets a value indicating whether this instance is required.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is required; otherwise, <c>false</c>.
            /// </value>
            internal bool IsRequired { get; }
        }

        #endregion
    }
}
