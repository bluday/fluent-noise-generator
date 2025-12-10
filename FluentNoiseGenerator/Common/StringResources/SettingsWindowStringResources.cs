using FluentNoiseGenerator.Common.Localization;
using System;

namespace FluentNoiseGenerator.Common.StringResources;

/// <summary>
/// Represents a collection of string resources used for the settings window.
/// </summary>
public sealed class SettingsWindowStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource collection for the about section.
    /// </summary>
    public SettingsAboutSectionStringResources About { get; }

    /// <summary>
    /// Gets the string resource collection for the appearance section.
    /// </summary>
    public SettingsAppearanceSectionStringResources Appearance { get; }

    /// <summary>
    /// Gets the string resource collection for the general section.
    /// </summary>
    public SettingsGeneralSectionStringResources General { get; }

    /// <summary>
    /// Gets the string resource for the header text block control.
    /// </summary>
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the title text displayed on the title bar control.
    /// </summary>
    public StringResource InAppWindowTitle { get; }

    /// <summary>
    /// Gets the string resource for the repository hyperlink button text in the settings window.
    /// </summary>
    public StringResource RepositoryOnGitHubHyperlinkButton { get; }

    /// <summary>
    /// Gets the string resource for the send feedback hyperlink button text in the settings window.
    /// </summary>
    public StringResource SendFeedbackHyperlinkButton { get; }

    /// <summary>
    /// Gets the string resource collection for the sound section.
    /// </summary>
    public SettingsSoundSectionStringResources Sound { get; }

    /// <summary>
    /// Gets the string resource for the window title displayed by the operating system.
    /// </summary>
    public StringResource WindowCaptionTitle { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindowStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public SettingsWindowStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        About      = new SettingsAboutSectionStringResources(localizedResourceProvider);
        Appearance = new SettingsAppearanceSectionStringResources(localizedResourceProvider);
        General    = new SettingsGeneralSectionStringResources(localizedResourceProvider);
        Sound      = new SettingsSoundSectionStringResources(localizedResourceProvider);

        Header = new StringResource(
            "Common/Settings",
            localizedResourceProvider
        );

        InAppWindowTitle = new StringResource(
            "General/AppDisplayName",
            localizedResourceProvider
        );

        RepositoryOnGitHubHyperlinkButton = new StringResource(
            "SettingsWindow/HyperlinkButtons/RepositoryOnGitHub",
            localizedResourceProvider
        );

        SendFeedbackHyperlinkButton = new StringResource(
            "SettingsWindow/HyperlinkButtons/SendFeedback",
            localizedResourceProvider
        );

        WindowCaptionTitle = new StringResource(
            "Common/Settings",
            localizedResourceProvider
        );
    }
    #endregion
}