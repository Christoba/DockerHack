// ----------------------------------------------------------------------------
// <copyright file="SerilogLogService.cs" company="kCura Corp">
//   kCura Corp (C) 2015 All Rights Reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace kCura.Hack.Logging
{
    using System;
    using System.Globalization;

    using Serilog;

    /// <summary>
    /// Represents a custom <c>Serilog</c> based <see cref="ILogService"/> implementation.
    /// </summary>
    [CLSCompliant(false)]
    public class SerilogLogService : ILogService
    {
        /// <summary>
        /// The logger instance.
        /// </summary>        
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogLogService" /> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        public SerilogLogService(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.logger = logger;
        }

        /// <inheritdoc />
        public DateTime? LastLogTimestamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a unique log filename that includes the application name and date time value.
        /// </summary>
        /// <param name="application">
        /// The application.
        /// </param>
        /// <returns>
        /// The filename.
        /// </returns>
        public static string CreateTimstampLogFileName(string application)
        {
            var logFileName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}_{1}.log",
                application,
                DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            return logFileName;
        }

        /// <summary>
        /// Creates a non-unique log filename that includes the application name.
        /// </summary>
        /// <param name="application">
        /// The application.
        /// </param>
        /// <returns>
        /// The filename.
        /// </returns>
        public static string CreateLogFileName(string application)
        {
            var logFileName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.log",
                application);
            return logFileName;
        }

        /// <inheritdoc />
        public void LogInfo(string message, params object[] args)
        {
            this.logger.Information(message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogInfo(Exception exception, string message, params object[] args)
        {
            this.logger.Information(exception, message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogDebug(string message, params object[] args)
        {
            this.logger.Debug(message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogDebug(Exception exception, string message, params object[] args)
        {
            this.logger.Debug(exception, message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogWarning(string message, params object[] args)
        {
            this.logger.Warning(message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            this.logger.Warning(exception, message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogError(string message, params object[] args)
        {
            this.logger.Error(message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public void LogError(Exception exception, string message, params object[] args)
        {
            this.logger.Error(exception, message, args);
            this.LastLogTimestamp = DateTime.UtcNow;
        }
    }
}
