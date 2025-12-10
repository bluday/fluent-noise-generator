using FluentNoiseGenerator.Common.Localization;
using System;

namespace FluentNoiseGenerator.Common.StringResources;

/// <summary>
/// Represents a collection of string resources used in the entire application.
/// </summary>
public sealed class AppStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource collection for the playback window.
    /// </summary>
    public PlaybackWindowStringResources PlaybackWindow { get; }

    /// <summary>
    /// Gets the string resource collection for the settings window.
    /// </summary>
    public SettingsWindowStringResources SettingsWindow { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="AppStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <param name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public AppStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        PlaybackWindow = new PlaybackWindowStringResources(localizedResourceProvider);
        SettingsWindow = new SettingsWindowStringResources(localizedResourceProvider);
    }
    #endregion
}