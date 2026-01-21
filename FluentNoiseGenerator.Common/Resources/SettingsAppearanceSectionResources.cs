using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of resources used for the appearance section in settings.
/// </summary>
[ResourceCollection]
public sealed partial class SettingsAppearanceSectionResources
{
    /// <summary>
    /// Gets the string resource for the header text of the always on top settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Appearance/AlwaysOnTop/Header")]
    public StringResource AlwaysOnTopSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the always on top settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Appearance/AlwaysOnTop/Description")]
    public StringResource AlwaysOnTopSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the application theme settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Appearance/ApplicationTheme/Header")]
    public StringResource ApplicationThemeSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the application theme settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Appearance/ApplicationTheme/Description")]
    public StringResource ApplicationThemeSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the appearance settings section.
    /// </summary>
    [ResourceId("Common/Appearance")]
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the header text of the system backdrop settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Appearance/SystemBackdrop/Header")]
    public StringResource SystemBackdropSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the description text of the system backdrop settings card.
    /// </summary>
    [ResourceId("SettingsWindow/Appearance/SystemBackdrop/Description")]
    public StringResource SystemBackdropSettingsCardDescription { get; }
}
