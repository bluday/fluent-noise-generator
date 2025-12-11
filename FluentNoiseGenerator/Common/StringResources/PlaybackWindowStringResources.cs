using FluentNoiseGenerator.Common.Localization;
using System;

namespace FluentNoiseGenerator.Common.StringResources;

/// <summary>
/// Represents a collection of string resources used for the playback window.
/// </summary>
public sealed class PlaybackWindowStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource for the window title.
    /// </summary>
    public StringResource Title { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindowStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public PlaybackWindowStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        Title = new StringResource(
            "General/AppDisplayName",
            localizedResourceProvider
        );
    }
    #endregion
}