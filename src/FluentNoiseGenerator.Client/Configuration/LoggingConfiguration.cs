using Microsoft.Extensions.Logging;
using System;

namespace FluentNoiseGenerator.Client.Configuration;

/// <summary>
/// Provides a method for configuring logging for the client.
/// </summary>
internal static class LoggingConfiguration
{
    /// <summary>
    /// Configures the specified logging builder.
    /// </summary>
    /// <param name="logging">
    /// The logging builder instance to configure.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if <paramref name="logging"/> is <see langword="null"/>.
    /// </exception>
    internal static void Configure(ILoggingBuilder logging)
    {
        ArgumentNullException.ThrowIfNull(logging);

        logging.AddConsole();
        logging.AddDebug();

        logging.SetMinimumLevel(LogLevel.Debug);
    }
}