using FluentNoiseGenerator.Infrastructure.Globalization;

namespace FluentNoiseGenerator.Infrastructure;

/// <summary>
/// Implementation for the <see cref="IAppSettings"/> interface.
/// </summary>
[Serializable]
public sealed class AppSettings : IAppSettings
{
    /// <inheritdoc cref="IAppSettings.ApplicationTheme"/>
    public object? ApplicationTheme { get; private set; }

    /// <inheritdoc cref="IAppSettings.AudioSampleRate"/>
    public int? AudioSampleRate { get; private set; }

    /// <inheritdoc cref="IAppSettings.Language"/>
    public ILanguage? Language { get; private set; }

    /// <inheritdoc cref="IAppSettings.DefaultNoisePreset"/>
    public object? DefaultNoisePreset { get; private set; }

    /// <inheritdoc cref="IAppSettings.SystemBackdrop"/>
    public object? SystemBackdrop { get; private set; }
}