using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using EVEMon.Common.Constants;
using EVEMon.Common.Extensions;

namespace EVEMon.Common.Net
{
    /// <summary>
    /// Exception class for all exceptions thrown by HttpWebService requests.
    /// </summary>
    [Serializable]
    public sealed class HttpWebClientServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClientServiceException"/> class.
        /// </summary>
        public HttpWebClientServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClientServiceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HttpWebClientServiceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClientServiceException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public HttpWebClientServiceException(string message, Exception ex)
            : base(message, ex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClientServiceException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" />
        /// that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />
        /// that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or
        /// <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        private HttpWebClientServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClientServiceException"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="url">The URL.</param>
        /// <param name="message">The message.</param>
        private HttpWebClientServiceException(HttpWebClientServiceExceptionStatus status, Uri url, string message)
            : base(message)
        {
            Status = status;
            Url = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClientServiceException"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="url">The URL.</param>
        /// <param name="message">The message.</param>
        private HttpWebClientServiceException(HttpWebClientServiceExceptionStatus status, Exception ex, Uri url, string message)
            : base(message, ex)
        {
            Status = status;
            Url = url;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public HttpWebClientServiceExceptionStatus Status { get; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        public string HostName => Url.Host;

        /// <summary>
        /// Factory method to create an HttpWebServiceException resulting from a WebException.
        /// Various different HttpWebServiceExceptionStatus types are applied, with appropriate messages, depending on the
        /// nature of the WebException.
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <param name="ex">The Exception that was thrown</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Only WebException and HttpRequestException allowed</exception>
        public static HttpWebClientServiceException HttpWebClientException(Uri url, Exception ex, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            var webException = ex as WebException;
            var httpRequestException = ex as HttpRequestException;

            if (webException == null && httpRequestException == null)
                throw new InvalidOperationException("Only WebException and HttpRequestException allowed");

            HttpWebClientServiceExceptionStatus status;
            string msg = webException != null
                ? ParseWebRequestException(webException, url, GetProxyHost(url), out status)
                : ParseHttpRequestException(httpRequestException, httpStatusCode, url, GetProxyHost(url), out status);

            return new HttpWebClientServiceException(status, ex, url, msg)
            {
                StatusCode = httpStatusCode
            };
        }

        /// <summary>
        /// Gets the proxy host.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private static string GetProxyHost(Uri url)
            => HttpWebClientServiceState.Proxy.Enabled
                ? HttpWebClientServiceState.Proxy.Host
                : url.Host;

        /// <summary>
        /// Parses a web exception to get an error message and a <see cref="HttpWebClientServiceExceptionStatus"/> status code.
        /// </summary>
        /// <param name="webException">The web exception.</param>
        /// <param name="url">The URL.</param>
        /// <param name="proxyHost">The proxy host.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private static string ParseWebRequestException(WebException webException, Uri url, string proxyHost,
            out HttpWebClientServiceExceptionStatus status)
        {
            StringBuilder messageBuilder = new StringBuilder();
            switch (webException.Status)
            {
                case WebExceptionStatus.ProtocolError:
                    HttpWebResponse response = (HttpWebResponse)webException.Response;
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.ProxyAuthenticationRequired:
                            status = HttpWebClientServiceExceptionStatus.ProxyError;
                            messageBuilder.AppendFormat(
                                ExceptionMessages.ProxyAuthenticationFailure, proxyHost, url.Host);
                            break;

                        default:
                            status = HttpWebClientServiceExceptionStatus.ServerError;
                            messageBuilder
                                .AppendFormat(ExceptionMessages.ServerError, url.Host)
                                .Append(response.StatusDescription);
                            break;
                    }
                    break;
                case WebExceptionStatus.ProxyNameResolutionFailure:
                    status = HttpWebClientServiceExceptionStatus.ProxyError;
                    messageBuilder.AppendFormat(ExceptionMessages.ProxyNameResolutionFailure, proxyHost);
                    break;
                case WebExceptionStatus.RequestProhibitedByProxy:
                    status = HttpWebClientServiceExceptionStatus.ProxyError;
                    messageBuilder.AppendFormat(ExceptionMessages.RequestProhibitedByProxy, url.Host, proxyHost);
                    break;
                case WebExceptionStatus.NameResolutionFailure:
                    status = HttpWebClientServiceExceptionStatus.NameResolutionFailure;
                    messageBuilder.AppendFormat(ExceptionMessages.NameResolutionFailure, proxyHost);
                    break;
                case WebExceptionStatus.ConnectFailure:
                    status = HttpWebClientServiceExceptionStatus.ConnectFailure;
                    messageBuilder.AppendFormat(ExceptionMessages.ConnectFailure, url.Host);
                    break;
                case WebExceptionStatus.Timeout:
                    status = HttpWebClientServiceExceptionStatus.Timeout;
                    messageBuilder.AppendFormat(ExceptionMessages.Timeout, url.Host);
                    break;
                default:
                    status = HttpWebClientServiceExceptionStatus.WebException;
                    messageBuilder.AppendFormat(ExceptionMessages.UnknownException, url.Host, webException.Status);
                    break;
            }

            return messageBuilder.ToString();
        }

        /// <summary>
        /// Parses an HTTP request exception to get an error message and a <see cref="HttpWebClientServiceExceptionStatus" /> status code.
        /// </summary>
        /// <param name="httpRequestException">The HTTP request exception.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="url">The URL.</param>
        /// <param name="proxyHost">The proxy host.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private static string ParseHttpRequestException(HttpRequestException httpRequestException, HttpStatusCode httpStatusCode,
            Uri url, string proxyHost, out HttpWebClientServiceExceptionStatus status)
        {
            StringBuilder messageBuilder = new StringBuilder();
            switch (httpStatusCode)
            {
                // Informational
                case HttpStatusCode.Continue:
                    goto default;
                case HttpStatusCode.SwitchingProtocols:
                    goto default;

                    // Redirection
                case HttpStatusCode.MultipleChoices:
                    goto default;
                case HttpStatusCode.MovedPermanently:
                    goto default;
                case HttpStatusCode.Redirect:
                    goto default;
                case HttpStatusCode.RedirectMethod:
                    goto default;
                case HttpStatusCode.NotModified:
                    goto default;
                case HttpStatusCode.UseProxy:
                    status = HttpWebClientServiceExceptionStatus.ProxyError;
                    messageBuilder.AppendFormat(
                        ExceptionMessages.ProxyNameResolutionFailure, proxyHost);
                    break;
                case HttpStatusCode.Unused:
                    goto default;
                case HttpStatusCode.TemporaryRedirect:
                    goto default;

                    // Client Error
                case HttpStatusCode.BadRequest:
                    goto default;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.PaymentRequired:
                case HttpStatusCode.Forbidden:
                    status = HttpWebClientServiceExceptionStatus.Forbidden;
                    messageBuilder.AppendFormat(ExceptionMessages.Forbidden, url.Host);
                    break;
                case HttpStatusCode.NotFound:
                    goto default;
                case HttpStatusCode.MethodNotAllowed:
                    goto default;
                case HttpStatusCode.NotAcceptable:
                    goto default;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    status = HttpWebClientServiceExceptionStatus.ProxyError;
                    messageBuilder.AppendFormat(
                        ExceptionMessages.ProxyAuthenticationFailure, proxyHost, url.Host);
                    break;
                case HttpStatusCode.RequestTimeout:
                    status = HttpWebClientServiceExceptionStatus.Timeout;
                    messageBuilder.AppendFormat(ExceptionMessages.Timeout, url.Host);
                    break;
                case HttpStatusCode.Conflict:
                    goto default;
                case HttpStatusCode.Gone:
                    goto default;
                case HttpStatusCode.LengthRequired:
                    goto default;
                case HttpStatusCode.PreconditionFailed:
                    goto default;
                case HttpStatusCode.RequestEntityTooLarge:
                    goto default;
                case HttpStatusCode.RequestUriTooLong:
                    goto default;
                case HttpStatusCode.UnsupportedMediaType:
                    goto default;
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    goto default;
                case HttpStatusCode.ExpectationFailed:
                    goto default;
                case HttpStatusCode.UpgradeRequired:
                    goto default;

                    // Server Error
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotImplemented:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.HttpVersionNotSupported:
                    status = HttpWebClientServiceExceptionStatus.ServerError;
                    messageBuilder
                        .AppendFormat(ExceptionMessages.ServerError, url.Host)
                        .Append($"{(int)httpStatusCode} ({httpStatusCode.ToString().ConvertUpperCamelCaseToString()})");
                    break;

                default:
                    status = HttpWebClientServiceExceptionStatus.Exception;
                    messageBuilder.AppendFormat(ExceptionMessages.UnknownException, url.Host,
                        $"{(int)httpStatusCode} ({httpStatusCode.ToString().ConvertUpperCamelCaseToString()})");
                    break;
            }

            return messageBuilder.ToString();
        }

        /// <summary>
        /// Factory method to create an HttpWebServiceException of type 'Exception'
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <param name="ex">The exception that was thrown</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static HttpWebClientServiceException Exception(Uri url, Exception ex)
        {
            url.ThrowIfNull(nameof(url));

            return new HttpWebClientServiceException(HttpWebClientServiceExceptionStatus.Exception, ex, url,
                "An Exception occurred.");
        }

        /// <summary>
        /// Factory method to create an HttpWebServiceException of type 'RedirectsExceeded'
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static HttpWebClientServiceException RedirectsExceededException(Uri url)
        {
            url.ThrowIfNull(nameof(url));

            return new HttpWebClientServiceException(HttpWebClientServiceExceptionStatus.RedirectsExceeded, url,
                string.Format(CultureConstants.DefaultCulture, ExceptionMessages.RedirectsExceeded, url.Host));
        }

        /// <summary>
        /// Factory method to create an HttpWebServiceException of type 'RequestsDisabled'
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static HttpWebClientServiceException RequestsDisabledException(Uri url)
        {
            url.ThrowIfNull(nameof(url));

            return new HttpWebClientServiceException(HttpWebClientServiceExceptionStatus.RequestsDisabled, url,
                string.Format(CultureConstants.DefaultCulture, ExceptionMessages.RequestsDisabled, url.Host));
        }

        /// <summary>
        /// Factory method to create an HttpWebServiceException for Xml download requests that fail due to an XmlException
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <param name="ex">The XmlException that was thrown</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static HttpWebClientServiceException XmlException(Uri url, Exception ex)
        {
            url.ThrowIfNull(nameof(url));

            return new HttpWebClientServiceException(HttpWebClientServiceExceptionStatus.XmlException, ex, url,
                string.Format(CultureConstants.DefaultCulture, ExceptionMessages.XmlException, url.Host));
        }

        /// <summary>
        /// Factory method to create an HttpWebServiceException for Image download requests that fail because a valid image was not returned
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <param name="ex">The exception that was thrown loading the image</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static HttpWebClientServiceException ImageException(Uri url, Exception ex)
        {
            url.ThrowIfNull(nameof(url));

            return new HttpWebClientServiceException(HttpWebClientServiceExceptionStatus.ImageException, ex, url,
                string.Format(CultureConstants.DefaultCulture, ExceptionMessages.ImageException, url.Host));
        }

        /// <summary>
        /// Factory method to create an HttpWebServiceException for File download requests that fail because the file could not be written
        /// </summary>
        /// <param name="url">The url of the request that failed</param>
        /// <param name="ex">The exception that was thrown creating the file</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static HttpWebClientServiceException FileError(Uri url, Exception ex)
        {
            url.ThrowIfNull(nameof(url));

            return new HttpWebClientServiceException(HttpWebClientServiceExceptionStatus.FileError, ex, url,
                string.Format(CultureConstants.DefaultCulture, ExceptionMessages.FileException, url.Host));
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [Obsolete("Base method is obselete")]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.ThrowIfNull(nameof(info));

            info.AddValue("Status", Status);
            info.AddValue("Url", Url);
            info.AddValue("HostName", HostName);

            base.GetObjectData(info, context);
        }
    }
}
