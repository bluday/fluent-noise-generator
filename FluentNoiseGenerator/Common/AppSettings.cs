using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator.Common;

/// <summary>
/// Implementation for the <see cref="IAppSettings"/> interface.
/// </summary>
[Serializable]
public sealed class AppSettings : IAppSettings
{
    #region Fields
    private readonly SettingsService _settingsService;
    #endregion

    #region Properties
    /// <inheritdoc cref="IAppSettings.ApplicationTheme"/>
    public ElementTheme ApplicationTheme { get; private set; }

    /// <inheritdoc cref="IAppSettings.AudioSampleRate"/>
    public int AudioSampleRate { get; private set; }

    /// <inheritdoc cref="IAppSettings.Language"/>
    public ILanguage Language { get; private set; }

    /// <inheritdoc cref="IAppSettings.DefaultNoisePreset"/>
    public object DefaultNoisePreset { get; private set; }

    /// <inheritdoc cref="IAppSettings.SystemBackdrop"/>
    public SystemBackdrop? SystemBackdrop { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="AppSettings"/> class using
    /// the specified settings service instance.
    /// </summary>
    /// <param name="settingsService">
    /// The settings service to extract currently set values from.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if <paramref name="settingsService"/> is <c>null</c>.
    /// </exception>
    public AppSettings(SettingsService settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);

        _settingsService = settingsService;

        Language = null!;

        DefaultNoisePreset = null!;
    }
    #endregion
}