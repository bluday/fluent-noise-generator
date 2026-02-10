using FluentNoiseGenerator.Common.Localization.Attributes;
using FluentNoiseGenerator.UI.Playback.Resources;
using FluentNoiseGenerator.UI.Settings.Resources;

namespace FluentNoiseGenerator.UI.Common.Resources;

/// <summary>
/// Represents a collection of resources used in the entire application.
/// </summary>
[ResourceCollection]
[HasResourceSection<PlaybackWindowResources>("PlaybackWindow")]
[HasResourceSection<SettingsWindowResources>("SettingsWindow")]
public sealed partial class AppResources { }