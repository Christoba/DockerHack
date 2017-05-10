// ----------------------------------------------------------------------------
// <copyright file="LogServiceFactory.cs" company="kCura Corp">
//   kCura Corp (C) 2015 All Rights Reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace kCura.Hack.Logging
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    using Serilog;
    using Serilog.Enrichers;

    /// <summary>
    /// Represents helper methods to create log services.
    /// </summary>
    public static class LogServiceFactory
    {
        /// <summary>
        /// The SEQ endpoint
        /// </summary>
        /// <remarks>
        /// The endpoint is the container host.
        /// </remarks>
        private const string SeqEndpoint = "http://172.23.224.1:5341";

        /// <summary>
        /// Creates the log file name.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns>
        /// The file name.
        /// </returns>
        public static string CreateLogFileName(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }

            return SerilogLogService.CreateLogFileName(applicationName);
        }

        /// <summary>
        /// Creates the log service.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// The <see cref="ILogService"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Do not allow an exception constructing a log service to break the application.")]
        public static ILogService CreateLogService()
        {
            try
            {
                Log.Logger = CreateLogger();
                var logService = new SerilogLogService(Log.Logger);
                return logService;
            }
            catch (Exception)
            {
            }

            return new NullLogService();
        }

     

        /// <summary>
        /// Flushes all remaining log entries.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Never allow this method to throw exceptions.")]
        public static void FlushLog()
        {
            try
            {
                Log.CloseAndFlush();
            }
            catch (Exception e)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    "A serious error occurred flushing the log. Exception: {0}.",
                    e);
                Trace.WriteLine(message);
            }
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <returns>
        /// The <see cref="ILogger"/>.
        /// </returns>
        public static ILogger CreateLogger()
        {
            var loggerConfiguration = CreateSerilogLoggerConfiguration();

            return loggerConfiguration.CreateLogger();
        }

        /// <summary>
        /// The create logger configuration.
        /// </summary>
        /// <returns>
        /// The <see cref="LoggerConfiguration"/>.
        /// </returns>
        public static LoggerConfiguration CreateSerilogLoggerConfiguration()
        {

            var loggerConfiguration =
                new LoggerConfiguration().MinimumLevel.Debug();

            loggerConfiguration.WriteTo.Seq(SeqEndpoint);

            return loggerConfiguration;
        }
    }
}