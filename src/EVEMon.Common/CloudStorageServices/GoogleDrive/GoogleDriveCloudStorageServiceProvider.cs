using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EVEMon.Common.Constants;
using EVEMon.Common.Exceptions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Serialization;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using File = System.IO.File;
using GoogleFile = Google.Apis.Drive.v3.Data.File;

namespace EVEMon.Common.CloudStorageServices.GoogleDrive
{
    public sealed class GoogleDriveCloudStorageServiceProvider : CloudStorageServiceProvider
    {
        private SerializableAPIResult<SerializableAPICredentials> m_result =
                new SerializableAPIResult<SerializableAPICredentials>();

        private static UserCredential s_credential;
        private string m_fileId;

        private const string Spaces = "appDataFolder";
        private const string ContentType = "application/xml";
        private const string CredentialsDirectory = @".googledrive";
        private const string UserId = @"user";


        #region Properties

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name => "Google Drive";

        /// <summary>
        /// Gets the authentication steps.
        /// </summary>
        /// <value>
        /// The authentication steps.
        /// </value>
        public override AuthenticationSteps AuthSteps => AuthenticationSteps.One;

        /// <summary>
        /// Gets a value indicating whether this <see cref="CloudStorageServiceProvider" /> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        protected override bool Enabled => true;

        /// <summary>
        /// Gets a value indicating whether the provider API credentials are stored.
        /// </summary>
        /// <value>
        /// <c>true</c> if the provider API credentials are stored; otherwise, <c>false</c>.
        /// </value>
        public override bool HasCredentialsStored
        {
            get
            {
                try
                {
                    return !string.IsNullOrWhiteSpace(GetCredentialsPath(checkAuth: true));
                }
                catch (APIException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        protected override ApplicationSettingsBase Settings => GoogleDriveCloudStorageServiceSettings.Default;

        /// <summary>
        /// Gets the refferal link.
        /// </summary>
        /// <value>
        /// The refferal link.
        /// </value>
        public override Uri RefferalLink => new Uri("https://accounts.google.com/SignUp");

        /// <summary>
        /// Gets the logo.
        /// </summary>
        /// <value>
        /// The logo.
        /// </value>
        public override Image Logo => CloudStorageServiceResources.GoogleDriveLogo;

        #endregion


        #region Implementation Methods

        /// <summary>
        /// Asynchronous checks the provider authentication with credentials is valid.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override Task<SerializableAPIResult<SerializableAPICredentials>>
            CheckProviderAuthWithCredentialsIsValidAsync(uint userId, string apiKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously requests the provider an authentication code.
        /// </summary>
        protected override async Task<SerializableAPIResult<SerializableAPICredentials>> RequestProviderAuthCodeAsync()
        {
            m_result = new SerializableAPIResult<SerializableAPICredentials>();

            try
            {
                await GetCredentialsAsync().ConfigureAwait(false);
                await CheckAuthCodeAsync(string.Empty).ConfigureAwait(false);
            }
            catch (GoogleApiException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };
            }
            catch (APIException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorCode = exc.ErrorCode, ErrorMessage = exc.Message };
            }
            catch (Exception exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return m_result;
        }

        /// <summary>
        /// Asynchronously checks the provider authentication code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override async Task<SerializableAPIResult<SerializableAPICredentials>> CheckProviderAuthCodeAsync(
            string code) 
            => await CheckAuthenticationAsync().ConfigureAwait(false);

        /// <summary>
        /// Asynchronously checks the authentication.
        /// </summary>
        protected override async Task<SerializableAPIResult<SerializableAPICredentials>> CheckAuthenticationAsync()
        {
            if (m_result == null)
                m_result = new SerializableAPIResult<SerializableAPICredentials>();

            try
            {
                if (!HasCredentialsStored)
                    return m_result;

                if (s_credential == null)
                    await GetCredentialsAsync().ConfigureAwait(false);

                using (DriveService client = GetClient())
                {
                    AboutResource.GetRequest request = client.About.Get();
                    request.Fields = "user";

                    await request.ExecuteAsync().ConfigureAwait(false);
                }
            }
            catch (GoogleApiException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };

                if (HasCredentialsStored)
                    await ResetSettingsAsync().ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return m_result;
        }

        /// <summary>
        /// Asynchronously revokes the authorization.
        /// </summary>
        /// <returns></returns>
        protected override async Task<SerializableAPIResult<SerializableAPICredentials>> RevokeAuthorizationAsync()
        {
            m_result = new SerializableAPIResult<SerializableAPICredentials>();

            try
            {
                Task<bool> revokeTokenAsync = s_credential?.RevokeTokenAsync(CancellationToken.None);
                bool success = revokeTokenAsync != null && await revokeTokenAsync.ConfigureAwait(false);

                if (!success)
                {
                    m_result.Error = new SerializableAPIError { ErrorMessage = "Unable to revoke authorization" };
                }
            }
            catch (GoogleApiException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };
            }
            catch (Exception exc)
            {
                m_result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return m_result;
        }

        /// <summary>
        /// Asynchronously uploads the file.
        /// </summary>
        /// <returns></returns>
        protected override async Task<SerializableAPIResult<CloudStorageServiceAPIFile>> UploadFileAsync()
        {
            SerializableAPIResult<CloudStorageServiceAPIFile> result = new SerializableAPIResult<CloudStorageServiceAPIFile>();

            try
            {
                m_fileId = m_fileId ?? await GetFileIdAsync().ConfigureAwait(false);

                byte[] content = Util.GZipCompress(SettingsFileContentByteArray).ToArray();

                using (DriveService client = GetClient())
                using (Stream stream = Util.GetMemoryStream(content))
                {
                    ResumableUpload<GoogleFile, GoogleFile> request;
                    GoogleFile fileMetadata = new GoogleFile { Name = SettingsFileNameWithoutExtension };
                    if (string.IsNullOrWhiteSpace(m_fileId))
                    {
                        //Upload
                        fileMetadata.Parents = new List<string> { Spaces };
                        request = client.Files.Create(fileMetadata, stream, ContentType);
                        ((FilesResource.CreateMediaUpload)request).Fields = "id, name";
                    }
                    else
                    {
                        //Update
                        request = client.Files.Update(fileMetadata, m_fileId, stream, ContentType);
                        ((FilesResource.UpdateMediaUpload)request).AddParents = Spaces;
                        ((FilesResource.UpdateMediaUpload)request).Fields = "id, name";
                    }

                    // Do the actual upload
                    IUploadProgress response = await request.UploadAsync().ConfigureAwait(false);
                    m_fileId = request.ResponseBody?.Id;

                    // Chceck response for exception
                    if (response.Exception != null)
                    {
                        result.Error = new SerializableAPIError { ErrorMessage = response.Exception.Message };
                    }
                }
            }
            catch (GoogleApiException exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                IsAuthenticated = false;
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };
            }
            catch (Exception exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return result;
        }

        /// <summary>
        /// Asynchronously downloads the file.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        protected override async Task<SerializableAPIResult<CloudStorageServiceAPIFile>> DownloadFileAsync()
        {
            SerializableAPIResult<CloudStorageServiceAPIFile> result = new SerializableAPIResult<CloudStorageServiceAPIFile>();

            try
            {
                m_fileId = m_fileId ?? await GetFileIdAsync().ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(m_fileId))
                    throw new FileNotFoundException();

                using (DriveService client = GetClient())
                using (Stream stream = new MemoryStream())
                {
                    FilesResource.GetRequest request = client.Files.Get(m_fileId);
                    request.Fields = "id, name";

                    IDownloadProgress response = await request.DownloadAsync(stream).ConfigureAwait(false);

                    if (response.Exception == null)
                        return await GetMappedAPIFileAsync(result, stream);

                    result.Error = new SerializableAPIError { ErrorMessage = response.Exception.Message };
                }
            }
            catch (GoogleApiException exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.Message };
            }
            catch (TokenResponseException exc)
            {
                IsAuthenticated = false;
                result.Error = new SerializableAPIError { ErrorMessage = exc.Error.ErrorDescription ?? exc.Error.Error };
            }
            catch (Exception exc)
            {
                result.Error = new SerializableAPIError { ErrorMessage = exc.Message };
            }

            return result;
        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Asynchronously gets the credentials.
        /// </summary>
        /// <returns></returns>
        private static async Task GetCredentialsAsync()
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = Util.Decrypt(GoogleDriveCloudStorageServiceSettings.Default.AppKey,
                    CultureConstants.InvariantCulture.NativeName),
                ClientSecret = Util.Decrypt(GoogleDriveCloudStorageServiceSettings.Default.AppSecret,
                    CultureConstants.InvariantCulture.NativeName)
            };

            EveMonClient.Trace("GoogleDrive.GetCredentialsAsync - Authorizing", printMethod: false);

            s_credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets,
                new[] { DriveService.Scope.DriveAppdata }, UserId, CancellationToken.None,
                new FileDataStore(GetCredentialsPath(), true),
                new ShellExecuteCodeReceiver()).ConfigureAwait(false);

            EveMonClient.Trace("GoogleDrive.GetCredentialsAsync - Authorized", printMethod: false);
        }

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
        /// Gets the client.
        /// </summary>
        /// <returns></returns>
        private static DriveService GetClient()
        {
            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = s_credential,
                ApplicationName = EveMonClient.FileVersionInfo.ProductName,
            };

            return new DriveService(initializer);
        }

        /// <summary>
        /// Gets the file identifier.
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetFileIdAsync()
        {
            FileList result;
            using (DriveService client = GetClient())
            {
                FilesResource.ListRequest listRequest = client.Files.List();
                listRequest.Spaces = Spaces;
                listRequest.Fields = "files(id, name)";

                result = await listRequest.ExecuteAsync().ConfigureAwait(false);
            }

            return result.Files.SingleOrDefault(file => file.Name == SettingsFileNameWithoutExtension)?.Id;
        }

        #endregion
    }
}
