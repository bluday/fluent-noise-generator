namespace BluDay.FluentNoiseRemover.Windows;

public partial class SettingsWindow
{
    /// <summary>
    /// Gets a read-only dictionary of mapped application themes, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, ElementTheme> LocalizedApplicationThemes { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped audio sample rates, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, int> LocalizedAudioSampleRates { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped languages, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, CultureInfo> LocalizedLanguages { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped noise presets, with localized keys.
    /// </summary>
    /// <remarks>
    /// Value type of <see cref="string"/> is used for now, until a type for noise preset is implemented.
    /// </remarks>
    public IReadOnlyDictionary<string, string> LocalizedNoisePresets { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped system backdrops, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, SystemBackdrop> LocalizedSystemBackdrops { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    ResourceLoader IApplicationResourceAware.ResourceLoader => _resourceLoader;
}