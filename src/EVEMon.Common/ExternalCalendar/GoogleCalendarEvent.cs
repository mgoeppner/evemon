using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EVEMon.Common.CloudStorageServices.GoogleDrive;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations.UISettings;
using EVEMon.Common.Exceptions;
using EVEMon.Common.Extensions;
using EVEMon.Common.Serialization;
using EVEMon.Common.SettingsObjects;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace EVEMon.Common.ExternalCalendar
{
    /// <summary>
    /// Class for handling Google calendar.
    /// </summary>
    public sealed class GoogleCalendarEvent : CalendarEvent
    {
        #region Fields

        /// <summary>
        /// The maximum of minutes google accepts.
        /// </summary>
        private const int MaxGoogleMinutes = 40320;

        /// <summary>
        /// The Google credential
        /// </summary>
        private static UserCredential s_credential;

        /// <summary>
        /// The calendar identifier
        /// </summary>
        private static string s_calendarId;

        /// <summary>
        /// Determines if the calendar is the primary one
        /// </summary>
        private static bool s_isPrimaryCalendar;

        /// <summary>
        /// The credentials directory
        /// </summary>
        private const string CredentialsDirectory = @".googlecalendar";

        /// <summary>
        /// The user identifier
        /// </summary>
        private const string UserId = @"user";

        #endregion


        #region Public Static Properties

        /// <summary>
        /// Gets the google reminder methods.
        /// </summary>
        /// <value>The reminder methods.</value>
        public static IEnumerable<object> ReminderMethods
            => Enum.GetValues(typeof(GoogleCalendarReminder))
                .Cast<Enum>()
                .Select(item => item.GetDescription());

        #endregion


        #region Internal Properties

        /// <summary>
        /// Gets or sets Reminder Method.
        /// </summary>
        internal GoogleCalendarReminder ReminderMethod { private get; set; }

        #endregion


        #region Internal Methods

        /// <summary>
        /// Add a new event or Update the appropriate event in the calendar.
        /// </summary>
        /// <param name="eventExists">if set to <c>true</c> the event exists.</param>
        /// <param name="queuePosition">The queue position.</param>
        /// <param name="lastSkillInQueue">if set to <c>true</c> skill is the last in queue.</param>
        internal override async Task AddOrUpdateEventAsync(bool eventExists, int queuePosition, bool lastSkillInQueue)
        {
            Event eventItem = eventExists
                ? (Event)Events[0]
                : new Event();

            // Set the title and content of the entry
            eventItem.Summary = Subject;
            eventItem.Start = new EventDateTime { DateTimeDateTimeOffset = StartDate };
            eventItem.End = new EventDateTime { DateTimeDateTimeOffset = EndDate };
            eventItem.Reminders = new Event.RemindersData();

            if (AlternateReminder)
            {
                EarlyReminder = new DateTime(StartDate.Year,
                    StartDate.Month,
                    StartDate.Day,
                    EarlyReminder.Hour,
                    EarlyReminder.Minute,
                    EarlyReminder.Second);

                LateReminder = new DateTime(StartDate.Year,
                    StartDate.Month,
                    StartDate.Day,
                    LateReminder.Hour,
                    LateReminder.Minute,
                    LateReminder.Second);

                // Subtract the reminder time from the event time
                DateTime dateTimeAlternateReminder = WorkOutAlternateReminders();
                TimeSpan timeSpan = eventItem.Start.DateTimeDateTimeOffset.GetValueOrDefault().Subtract(dateTimeAlternateReminder);
                Minutes = Math.Abs(timeSpan.Hours * 60 + timeSpan.Minutes);

                SetGoogleReminder(eventItem);
            }
            else if (ItemReminder)
                SetGoogleReminder(eventItem);
            else
                eventItem.Reminders.UseDefault = false;

            using (CalendarService client = await GetClient())
            {
                if (eventExists)
                {
                    // Update the event
                    await client.Events.Update(eventItem, await GetCalendarId(), eventItem.Id).ExecuteAsync();
                }
                else
                {
                    // Send the request and receive the response
                    await client.Events.Insert(eventItem, await GetCalendarId()).ExecuteAsync();
                }
            }
        }

        /// <summary>
        /// Get the relevant Google event.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if an event is found, <c>false</c> otherwise.
        /// </returns>
        internal override bool GetEvent()
        {
            if (Events.Count < 1)
                return false;

            Event eventItem = (Event)Events[0];
            if (!eventItem.Start.DateTimeDateTimeOffset.HasValue || !eventItem.End.DateTimeDateTimeOffset.HasValue)
                return false;

            StartDate = eventItem.Start.DateTimeDateTimeOffset.Value.DateTime;
            EndDate = eventItem.End.DateTimeDateTimeOffset.Value.DateTime;
            Subject = eventItem.Summary;

            if (eventItem.Reminders?.Overrides != null)
            {
                ItemReminder = true;
                eventItem.Reminders.UseDefault = true;

                if (!eventItem.Reminders.Overrides.Any())
                    return true;

                eventItem.Reminders.UseDefault = false;
                GoogleCalendarReminder reminderMethod;
                Enum.TryParse(eventItem.Reminders.Overrides.First().Method, out reminderMethod);
                ReminderMethod = reminderMethod;
                Minutes = eventItem.Reminders.Overrides.First().Minutes.GetValueOrDefault();
            }
            else
            {
                ItemReminder = false;
                Minutes = 10;
            }

            return true;
        }

        /// <summary>
        /// Read the Google events.
        /// </summary>
        internal override async Task ReadEventsAsync()
        {
            Events.Clear();

            using (CalendarService client = await GetClient())
            {
                EventsResource.ListRequest request = client.Events.List(await GetCalendarId());
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                request.SingleEvents = true;
                request.ShowDeleted = false;
                request.TimeMinDateTimeOffset = StartDate;
                request.TimeMaxDateTimeOffset = EndDate;
                request.Q = Subject;

                Events events = await request.ExecuteAsync();

                foreach (Event @event in events.Items
                    .Where(entry => entry.Summary == Subject))
                {
                    Events.Add(@event);
                }
            }
        }

        /// <summary>
        /// Delete the relevant event.
        /// </summary>
        /// <param name="eventIndex">The event index.</param>
        internal override async Task DeleteEventAsync(int eventIndex)
        {
            Event eventItem = (Event)Events[eventIndex];

            using (CalendarService client = await GetClient())
            {
                await client.Events.Delete(await GetCalendarId(), eventItem.Id).ExecuteAsync();
            }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Requests the authentication.
        /// </summary>
        /// <param name="checkAuth">if set to <c>true</c> [check authentication].</param>
        /// <returns></returns>
        public static async Task<SerializableAPIResult<SerializableAPICredentials>> RequestAuth(bool checkAuth = false)
            => await LogOn(checkAuth);

        /// <summary>
        /// Revokes the authentication.
        /// </summary>
        /// <returns></returns>
        public static async Task<SerializableAPIResult<SerializableAPICredentials>> RevokeAuth() 
            => await LogOut();

        #endregion


        #region Private Methods

        /// <summary>
        /// Gets the credentials path.
        /// </summary>
        /// <param name="checkAuth">if set to <c>true</c> [check authentication].</param>
        /// <returns></returns>
        /// <exception cref="EVEMon.Common.Exceptions.APIException"></exception>
        private static string GetCredentialsPath(bool checkAuth = false)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);

            string certPath = Directory.GetParent(configuration.FilePath).Parent?.Parent?.FullName;

            bool fileExists = false;
            if (!string.IsNullOrWhiteSpace(certPath))
            {
                certPath = Path.Combine(certPath, CredentialsDirectory);
                string filePath = Path.Combine(certPath, $"{typeof(TokenResponse).FullName}-{UserId}");
                fileExists = File.Exists(filePath);
            }

            if (!checkAuth || fileExists)
                return certPath;

            SerializableAPIError error = new SerializableAPIError
            {
                ErrorCode = @"Authentication required",
                ErrorMessage = "Authentication required.\n\n" +
                               "Go to External Calendar options to request authentication.\n" +
                               "(i.e. Tools > Options... > Scheduler > External Calendar)"
            };

            throw new APIException(error);
        }

        /// <summary>
        /// Gets the calendar identifier.
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetCalendarId()
        {
            if (s_calendarId != null)
            {
                if (string.IsNullOrWhiteSpace(Settings.Calendar.GoogleCalendarName) && s_isPrimaryCalendar)
                    return s_calendarId;

                if (s_calendarId == Settings.Calendar.GoogleCalendarName)
                    return s_calendarId;
            }

            s_isPrimaryCalendar = string.IsNullOrWhiteSpace(Settings.Calendar.GoogleCalendarName);
            s_calendarId = string.IsNullOrWhiteSpace(Settings.Calendar.GoogleCalendarName)
                               ? "primary"
                               : Settings.Calendar.GoogleCalendarName;

            using (CalendarService client = await GetClient())
            {
                Calendar response = await client.Calendars.Get(s_calendarId).ExecuteAsync();
                s_calendarId = response.Id;
            }

            return s_calendarId;
        }

        /// <summary>
        /// Determines whether credentials are stored.
        /// </summary>
        /// <returns></returns>
        public static bool HasCredentialsStored()
        {
            try
            {
                GetCredentialsPath(true);
            }
            catch (APIException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logon to Google.
        /// </summary>
        /// <param name="checkAuth">if set to <c>true</c> [check authentication].</param>
        /// <returns></returns>
        private static async Task<SerializableAPIResult<SerializableAPICredentials>> LogOn(bool checkAuth = false)
        {
            SerializableAPIResult<SerializableAPICredentials> result =
                new SerializableAPIResult<SerializableAPICredentials>();

            if (checkAuth && !HasCredentialsStored())
                return result;

            var clientSecrets = new ClientSecrets
            {
                ClientId = Util.Decrypt(GoogleDriveCloudStorageServiceSettings.Default.AppKey,
                    CultureConstants.InvariantCulture.NativeName),
                ClientSecret = Util.Decrypt(GoogleDriveCloudStorageServiceSettings.Default.AppSecret,
                    CultureConstants.InvariantCulture.NativeName)
            };

            try
            {
                s_credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets,
                    new[] { CalendarService.Scope.Calendar }, UserId, CancellationToken.None,
                    new FileDataStore(GetCredentialsPath(checkAuth), true));

                if (checkAuth)
                {
                    using (CalendarService client = await GetClient())
                        await client.Settings.List().ExecuteAsync();
                }
            }
            catch (GoogleApiException exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };
            }
            catch (APIException exc)
            {
                result.Error = new SerializableAPIError { ErrorCode = exc.ErrorCode, ErrorMessage = exc.Message };
            }
            catch (Exception exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return result;
        }

        /// <summary>
        /// Logout from Google.
        /// </summary>
        private static async Task<SerializableAPIResult<SerializableAPICredentials>> LogOut()
        {
            SerializableAPIResult<SerializableAPICredentials> result =
                new SerializableAPIResult<SerializableAPICredentials>();

            try
            {
                Task<bool> revokeTokenAsync = s_credential?.RevokeTokenAsync(CancellationToken.None);
                bool success = revokeTokenAsync != null && await revokeTokenAsync;

                if (!success)
                {
                    result.Error = new SerializableAPIError { ErrorMessage = "Unable to revoke authorization" };
                }
            }
            catch (GoogleApiException exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };
            }
            catch (Exception exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return result;
        }

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns></returns>
        private static async Task<CalendarService> GetClient()
        {
            if ((s_credential == null) || !HasCredentialsStored())
            {
                GetCredentialsPath(true);
                await LogOn(true);
            }

            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = s_credential,
                ApplicationName = EveMonClient.FileVersionInfo.ProductName,
            };

            return new CalendarService(initializer);
        }

        /// <summary>
        /// Sets the goolge reminder.
        /// </summary>
        /// <param name="eventItem">The event item.</param>
        private void SetGoogleReminder(Event eventItem)
        {
            eventItem.Reminders.UseDefault = false;
            eventItem.Reminders.Overrides = eventItem.Reminders.Overrides ?? new List<EventReminder>();

            EventReminder reminder = eventItem.Reminders.Overrides.FirstOrDefault() ?? new EventReminder();
            reminder.Minutes = Math.Min(Minutes, MaxGoogleMinutes);
            reminder.Method = ReminderMethod.ToString().ToLowerInvariant();

            eventItem.Reminders.Overrides.Clear();
            eventItem.Reminders.Overrides.Add(reminder);
        }

        #endregion

    }
}
