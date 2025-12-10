using FluentNoiseGenerator.Common.Localization;
using System;

namespace FluentNoiseGenerator.Common.StringResources;

/// <summary>
/// Represents a collection of string resources used for the appearance section in settings.
/// </summary>
public sealed class SettingsAppearanceSectionStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource for the header text of the always on top settings card.
    /// </summary>
    public StringResource AlwaysOnTopSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the always on top settings card.
    /// </summary>
    public StringResource AlwaysOnTopSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the application theme settings card.
    /// </summary>
    public StringResource ApplicationThemeSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the application theme settings card.
    /// </summary>
    public StringResource ApplicationThemeSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the appearance settings section.
    /// </summary>
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the header text of the system backdrop settings card.
    /// </summary>
    public StringResource SystemBackdropSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the system backdrop settings card.
    /// </summary>
    public StringResource SystemBackdropSettingsCardDescription { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsAppearanceSectionStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public SettingsAppearanceSectionStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        AlwaysOnTopSettingsCardHeader = new StringResource(
            "SettingsWindow/Appearance/AlwaysOnTop/Header",
            localizedResourceProvider
        );

        AlwaysOnTopSettingsCardDescription = new StringResource(
            "SettingsWindow/Appearance/AlwaysOnTop/Description",
            localizedResourceProvider
        );

        ApplicationThemeSettingsCardHeader = new StringResource(
            "SettingsWindow/Appearance/ApplicationTheme/Header",
            localizedResourceProvider
        );

        ApplicationThemeSettingsCardDescription = new StringResource(
            "SettingsWindow/Appearance/ApplicationTheme/Description",
            localizedResourceProvider
        );

        Header = new StringResource(
            "Common/Appearance",
            localizedResourceProvider
        );

        SystemBackdropSettingsCardHeader = new StringResource(
            "SettingsWindow/Appearance/SystemBackdrop/Header",
            localizedResourceProvider
        );

        SystemBackdropSettingsCardDescription = new StringResource(
            "SettingsWindow/Appearance/SystemBackdrop/Description",
            localizedResourceProvider
        );
    }
    #endregion
}
