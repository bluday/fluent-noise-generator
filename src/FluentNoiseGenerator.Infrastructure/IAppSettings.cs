namespace FluentNoiseGenerator.Infrastructure;

/// <summary>
/// Defines a contract for currently-set application settings.
/// </summary>
public interface IAppSettings
{
    /// <summary>
    /// Gets the application theme.
    /// </summary>
    object? ApplicationTheme { get; }

    /// <summary>
    /// Gets the audio sample rate.
    /// </summary>
    int? AudioSampleRate { get; }

    /// <summary>
    /// Gets the application language.
    /// </summary>
    Globalization.ILanguage? Language { get; }

    /// <summary>
    /// Gets the default noise preset.
    /// </summary>
    object? DefaultNoisePreset { get; }

    /// <summary>
    /// Gets the system backdrop.
    /// </summary>
    object? SystemBackdrop { get; }
}