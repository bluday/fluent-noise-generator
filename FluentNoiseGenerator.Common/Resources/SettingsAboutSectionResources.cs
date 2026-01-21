using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Localization.Attributes;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of string resources used for the about section in settings.
/// </summary>
[ResourceCollection]
public sealed partial class SettingsAboutSectionResources
{
    /// <summary>
    /// Gets the string resource for the description text displayed in the about expander.
    /// </summary>
    [ResourceId("General/CopyrightText")]
    public StringResource AboutDescription { get; }

    /// <summary>
    /// Gets the string resource containing the application's display name.
    /// </summary>
    [ResourceId("General/AppDisplayName")]
    public StringResource AppDisplayName { get; }

    /// <summary>
    /// Gets the string resource for the header text of the about settings section.
    /// </summary>
    [ResourceId("Common/About")]
    public StringResource Header { get; }

    /// <summary>
    /// Gets the string resource for the format string used to display the session identifier.
    /// </summary>
    [ResourceId("SettingsWindow/About/SessionIdentifierFormatString")]
    public StringResource SessionIdentifierFormatString { get; }
}