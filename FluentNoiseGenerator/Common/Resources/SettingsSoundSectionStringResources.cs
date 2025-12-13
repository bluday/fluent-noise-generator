using FluentNoiseGenerator.Common.Localization;
using System;

namespace FluentNoiseGenerator.Common.Resources;

/// <summary>
/// Represents a collection of string resources used for the sound section in settings.
/// </summary>
public sealed class SettingsSoundSectionStringResources
{
    #region Properties
    /// <summary>
    /// Gets the string resource for the description text of the audio sample rate settings card.
    /// </summary>
    public StringResource AudioSampleRateSettingsCardDescription { get; }

    /// <summary>
    /// Gets the string resource for the header text of the audio sample rate settings card.
    /// </summary>
    public StringResource AudioSampleRateSettingsCardHeader { get; }

    /// <summary>
    /// Gets the string resource for the header text of the sound settings section.
    /// </summary>
    public StringResource Header { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSoundSectionStringResources"/> class.
    /// </summary>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public SettingsSoundSectionStringResources(LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        AudioSampleRateSettingsCardDescription = new StringResource(
            "SettingsWindow/Sound/SampleRate/Description",
            localizedResourceProvider
        );
        
        AudioSampleRateSettingsCardHeader = new StringResource(
            "Common/SampleRate",
            localizedResourceProvider
        );

        Header = new StringResource(
            "Common/Sound",
            localizedResourceProvider
        );
    }
    #endregion
}
