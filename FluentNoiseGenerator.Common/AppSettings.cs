using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Globalization;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Implementation for the <see cref="IAppSettings"/> interface.
/// </summary>
[Serializable]
public sealed class AppSettings : IAppSettings
{
    #region Fields
    private readonly object _settingsService;
    #endregion

    #region Properties
    /// <inheritdoc cref="IAppSettings.ApplicationTheme"/>
    public object ApplicationTheme { get; private set; }

    /// <inheritdoc cref="IAppSettings.AudioSampleRate"/>
    public int AudioSampleRate { get; private set; }

    /// <inheritdoc cref="IAppSettings.Language"/>
    public ILanguage Language { get; private set; }

    /// <inheritdoc cref="IAppSettings.DefaultNoisePreset"/>
    public object DefaultNoisePreset { get; private set; }

    /// <inheritdoc cref="IAppSettings.SystemBackdrop"/>
    public object? SystemBackdrop { get; private set; }
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
    public AppSettings(object settingsService)
    {
        ArgumentNullException.ThrowIfNull(settingsService);

        _settingsService = settingsService;

        Language = null!;

        DefaultNoisePreset = null!;
    }
    #endregion
}