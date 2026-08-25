using System;
using Google.Apis.Logging;
using Google.Apis.Util;

namespace EVEMon.Common.Helpers
{
    /// <summary>
    /// A logger that forwards Google API client library log messages to the EVEMon trace file.
    /// </summary>
    public sealed class GoogleApisTraceLogger : BaseLogger
    {
        /// <summary>
        /// Construct a <see cref="GoogleApisTraceLogger"/>.
        /// </summary>
        /// <param name="minimumLogLevel">Logging will be enabled at this level and all higher levels.</param>
        /// <param name="clock">Optional <see cref="IClock"/>; will use the system clock if <c>null</c>.</param>
        public GoogleApisTraceLogger(LogLevel minimumLogLevel = LogLevel.All, IClock clock = null)
            : this(minimumLogLevel, clock, null)
        {
        }

        private GoogleApisTraceLogger(LogLevel minimumLogLevel, IClock clock, Type forType)
            : base(minimumLogLevel, clock, forType)
        {
        }

        /// <inheritdoc/>
        protected override ILogger BuildNewLogger(Type type)
            => new GoogleApisTraceLogger(MinimumLogLevel, Clock, type);

        /// <inheritdoc/>
        protected override void Log(LogLevel logLevel, string formattedMessage)
            => EveMonClient.Trace($"GoogleApis {formattedMessage}", printMethod: false);
    }
}
