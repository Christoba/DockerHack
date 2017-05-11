// ----------------------------------------------------------------------------
// <copyright file="NullLogService.cs" company="kCura Corp">
//   kCura Corp (C) 2015 All Rights Reserved.
// </copyright>
// ----------------------------------------------------------------------------

namespace kCura.Hack.Logging
{
    using System;

    /// <summary>
    /// Represents a Null log service that does nothing and is based on the <see cref="ILogService"/> implementation.
    /// </summary>
    public class NullLogService : ILogService
    {
        /// <inheritdoc />
        public DateTime? LastLogTimestamp
        {
            get
            {
                return null;
            }
        }

        /// <inheritdoc />
        public void LogInfo(string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogInfo(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogDebug(string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogDebug(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogWarning(string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogWarning(Exception exception, string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogError(string message, params object[] args)
        {
        }

        /// <inheritdoc />
        public void LogError(Exception exception, string message, params object[] args)
        {
        }
    }
}
