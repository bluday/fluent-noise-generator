using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.UI.Settings.Resources;

/// <summary>
/// Represents a collection of string resources used for the sound section in settings.
/// </summary>
[ResourceCollection]
public sealed partial class SettingsSoundSectionResources
{
    /// <summary>
    /// Gets the string resource for the description text of the audio sample rate settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Sound/SampleRate/Description")]
    public StringResource AudioSampleRateSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the audio sample rate settings card.
    /// </summary>
    [ResourceId("Common/SampleRate")]
    public StringResource AudioSampleRateSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the header text of the sound settings section.
    /// </summary>
    [ResourceId("Common/Sound")]
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the "Hz" unit.
    /// </summary>
    [ResourceId("Units/Hertz/Short")]
    public StringResource SampleRateHertzUnit { get; }
}