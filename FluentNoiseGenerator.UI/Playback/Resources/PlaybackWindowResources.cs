using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.UI.Playback.Resources;

/// <summary>
/// Represents a collection of string resources used for the playback window.
/// </summary>
[ResourceCollection]
public sealed partial class PlaybackWindowResources
{
    /// <summary>
    /// Gets the string resource for the window title.
    /// </summary>
    [ResourceId("General/AppDisplayName")]
    public StringResource Title { get; }
}