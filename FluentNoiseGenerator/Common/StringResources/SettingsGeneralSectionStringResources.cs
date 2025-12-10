using FluentNoiseGenerator.Common.Localization;
using System;

namespace FluentNoiseGenerator.Common.StringResources;

/// <summary>
/// Represents a collection of string resources used for the general section in settings.
/// </summary>
public sealed class SettingsGeneralSectionStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource for the header text of the autoplay-on-launch settings card.
    /// </summary>
    public StringResource AutoplayOnLaunchSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the autoplay-on-launch settings card.
    /// </summary>
    public StringResource AutoplayOnLaunchSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the "On" text in the autoplay-on-launch toggle switch.
    /// </summary>
    public StringResource AutoplayOnLaunchToggleSwitchOn { get; }

    /// <summary>
    /// Gets the string resource for the "Off" text in the autoplay-on-launch toggle switch.
    /// </summary>
    public StringResource AutoplayOnLaunchToggleSwitchOff { get; }

    /// <summary>
    /// Gets the string resource for the header text of the default noise preset settings card.
    /// </summary>
    public StringResource DefaultNoisePresetSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the default noise preset settings card.
    /// </summary>
    public StringResource DefaultNoisePresetSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the general settings section.
    /// </summary>
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the header text of the language settings card.
    /// </summary>
    public StringResource LanguageSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the language settings card.
    /// </summary>
    public StringResource LanguageSettingsCardDescription { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsGeneralSectionStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public SettingsGeneralSectionStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        AutoplayOnLaunchSettingsCardHeader = new StringResource(
            "SettingsWindow/General/AutoplayOnLaunch/Header",
            localizedResourceProvider
        );

        AutoplayOnLaunchSettingsCardDescription = new StringResource(
            "SettingsWindow/General/AutoplayOnLaunch/Description",
            localizedResourceProvider
        );

        AutoplayOnLaunchToggleSwitchOn = new StringResource(
            "Common/On",
            localizedResourceProvider
        );

        AutoplayOnLaunchToggleSwitchOff = new StringResource(
            "Common/Off",
            localizedResourceProvider
        );

        DefaultNoisePresetSettingsCardHeader = new StringResource(
            "SettingsWindow/General/DefaultNoisePreset/Header",
            localizedResourceProvider
        );

        DefaultNoisePresetSettingsCardDescription = new StringResource(
            "SettingsWindow/General/DefaultNoisePreset/Description",
            localizedResourceProvider
        );

        Header = new StringResource(
            "Common/General",
            localizedResourceProvider
        );

        LanguageSettingsCardHeader = new StringResource(
            "Common/Language",
            localizedResourceProvider
        );

        LanguageSettingsCardDescription = new StringResource(
            "SettingsWindow/General/Language/Description",
            localizedResourceProvider
        );
    }
    #endregion
}