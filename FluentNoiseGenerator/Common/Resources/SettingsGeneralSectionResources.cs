using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of resources used for the general section in settings.
/// </summary>
[ResourceCollection]
public sealed partial class SettingsGeneralSectionResources
{
    /// <summary>
    /// Gets the string resource for the header text of the autoplay-on-launch settings card.
    /// </summary>
    [ResourceId("SettingsWindow/General/AutoplayOnLaunch/Header")]
    public StringResource AutoplayOnLaunchSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the autoplay-on-launch settings card.
    /// </summary>
    [ResourceId("SettingsWindow/General/AutoplayOnLaunch/Description")]
    public StringResource AutoplayOnLaunchSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the "On" text in the autoplay-on-launch toggle switch.
    /// </summary>
    [ResourceId("Common/On")]
    public StringResource AutoplayOnLaunchToggleSwitchOn { get; }

    /// <summary>
    /// Gets the string resource for the "Off" text in the autoplay-on-launch toggle switch.
    /// </summary>
    [ResourceId("Common/Off")]
    public StringResource AutoplayOnLaunchToggleSwitchOff { get; }

    /// <summary>
    /// Gets the string resource for the header text of the default noise preset settings card.
    /// </summary>
    [ResourceId("SettingsWindow/General/DefaultNoisePreset/Header")]
    public StringResource DefaultNoisePresetSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the default noise preset settings card.
    /// </summary>
    [ResourceId("SettingsWindow/General/DefaultNoisePreset/Description")]
    public StringResource DefaultNoisePresetSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the general settings section.
    /// </summary>
    [ResourceId("Common/General")]
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the header text of the language settings card.
    /// </summary>
    [ResourceId("Common/Language")]
    public StringResource LanguageSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the language settings card.
    /// </summary>
    [ResourceId("SettingsWindow/General/Language/Description")]
    public StringResource LanguageSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the "Blue" noise preset value.
    /// </summary>
    [ResourceId("Common/Blue")]
    public StringResource NoisePresetBlue { get; }

    /// <summary>
    /// Gets the string resource for the "Brownian" noise preset value.
    /// </summary>
    [ResourceId("Common/Brownian")]
    public StringResource NoisePresetBrownian { get; }

    /// <summary>
    /// Gets the string resource for the "White" noise preset value.
    /// </summary>
    [ResourceId("Common/White")]
    public StringResource NoisePresetWhite { get; }
}