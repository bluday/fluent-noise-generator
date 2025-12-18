using FluentNoiseGenerator.Common.Localization.Attributes;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.Windows;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of resources used in the entire application.
/// </summary>
[ResourceCollection]
[HasResourceSection<PlaybackWindowResources>(nameof(PlaybackWindow))]
[HasResourceSection<SettingsWindowResources>(nameof(SettingsWindow))]
public sealed partial class AppResources { }