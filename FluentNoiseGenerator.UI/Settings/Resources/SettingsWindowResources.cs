using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.UI.Settings.Resources;

/// <summary>
/// Represents a collection of string resources used for the settings window.
/// </summary>
[ResourceCollection]
[HasResourceSection<SettingsAboutSectionResources>("About")]
[HasResourceSection<SettingsAppearanceSectionResources>("Appearance")]
[HasResourceSection<SettingsGeneralSectionResources>("General")]
[HasResourceSection<SettingsSoundSectionResources>("Sound")]
public sealed partial class SettingsWindowResources
{
    /// <summary>
    /// Gets the string resource for the header text block control.
    /// </summary>
    [ResourceId("Common/Settings")]
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the title text displayed on the title bar control.
    /// </summary>
    [ResourceId("General/AppDisplayName")]
    public StringResource InAppWindowTitle { get; }

    /// <summary>
    /// Gets the string resource for the repository hyperlink button text in the settings window.
    /// </summary>
    [ResourceId("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub")]
    public StringResource RepositoryOnGitHubHyperlinkButton { get; }

    /// <summary>
    /// Gets the string resource for the send feedback hyperlink button text in the settings window.
    /// </summary>
    [ResourceId("SettingsWindow/HyperlinkButtons/SendFeedback")]
    public StringResource SendFeedbackHyperlinkButton { get; }

    /// <summary>
    /// Gets the string resource for the window title displayed by the operating system.
    /// </summary>
    [ResourceId("Common/Settings")]
    public StringResource WindowCaptionTitle { get; }
}