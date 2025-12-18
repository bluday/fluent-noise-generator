using FluentNoiseGenerator.Common.Globalization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace FluentNoiseGenerator.Common;

/// <summary>
/// Defines a contract for currently-set application settings.
/// </summary>
public interface IAppSettings
{
    /// <summary>
    /// Gets the application theme.
    /// </summary>
    ElementTheme ApplicationTheme { get; }

    /// <summary>
    /// Gets the audio sample rate.
    /// </summary>
    int AudioSampleRate { get; }

    /// <summary>
    /// Gets the application language.
    /// </summary>
    ILanguage Language { get; }

    /// <summary>
    /// Gets the default noise preset.
    /// </summary>
    object DefaultNoisePreset { get; }

    /// <summary>
    /// Gets the system backdrop.
    /// </summary>
    SystemBackdrop? SystemBackdrop { get; }
}