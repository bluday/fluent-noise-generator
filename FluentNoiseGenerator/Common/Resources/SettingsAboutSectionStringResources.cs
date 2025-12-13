using FluentNoiseGenerator.Common.Localization;
using System;
using Windows.ApplicationModel;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of string resources used for the about section in settings.
/// </summary>
public sealed class SettingsAboutSectionStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource for the description text displayed in the about expander.
    /// </summary>
    public StringResource AboutDescription { get; }

    /// <summary>
    /// Gets the string resource containing the application's display name.
    /// </summary>
    public StringResource AppDisplayName { get; }

    /// <summary>
    /// Gets a displayable application version text.
    /// </summary>
    public string ApplicationVersionText
    {
        get
        {
            PackageVersion version = Package.Current.Id.Version;

            return $"{version.Major}.{version.Minor}";
        }
    }

    /// <summary>
    /// Gets the string resource for the header text of the about settings section.
    /// </summary>
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the format string used to display the session identifier.
    /// </summary>
    public StringResource SessionIdentifierFormatString { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsAboutSectionStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public SettingsAboutSectionStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        AboutDescription = new StringResource(
            "General/CopyrightText",
            localizedResourceProvider
        );

        AppDisplayName = new StringResource(
            "General/AppDisplayName",
            localizedResourceProvider
        );

        Header = new StringResource(
            "Common/About",
            localizedResourceProvider
        );

        SessionIdentifierFormatString = new StringResource(
            "SettingsWindow/About/SessionIdentifierFormatString",
            localizedResourceProvider
        );
    }
    #endregion
}
