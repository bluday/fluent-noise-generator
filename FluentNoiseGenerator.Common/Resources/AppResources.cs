using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of resources used in the entire application.
/// </summary>
[ResourceCollection]
[HasResourceSection<PlaybackWindowResources>("PlaybackWindow")]
[HasResourceSection<SettingsWindowResources>("SettingsWindow")]
public sealed partial class AppResources { }