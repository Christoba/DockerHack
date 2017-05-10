// ----------------------------------------------------------------------------
// <copyright file="ILogService.cs" company="kCura Corp">
//   kCura Corp (C) 2017 All Rights Reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace kCura.Hack.Logging
{
    using System;

    /// <summary>
    /// Represents an abstract log service.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Gets the timestamp when the last log entry was made.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        DateTime? LastLogTimestamp
        {
            get;
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogInfo(string message, params object[] args);

        /// <summary>
        /// Logs an information exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogInfo(Exception exception, string message, params object[] args);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogDebug(string message, params object[] args);

        /// <summary>
        /// Logs a debug exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogDebug(Exception exception, string message, params object[] args);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Logs a warning exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogWarning(Exception exception, string message, params object[] args);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogError(string message, params object[] args);

        /// <summary>
        /// Logs an error exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="args">
        /// The optional arguments.
        /// </param>
        void LogError(Exception exception, string message, params object[] args);
    }
}
