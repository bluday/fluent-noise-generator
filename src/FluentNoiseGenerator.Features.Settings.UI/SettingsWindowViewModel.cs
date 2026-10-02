using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace FluentNoiseGenerator.Features.Settings.UI;

/// <summary>
/// Represents the view model for the settings window.
/// </summary>
public sealed partial class SettingsWindowViewModel : ObservableObject
{
    #region Instance properties
    /// <summary>
    /// Gets a read-only observable collection of available application themes.
    /// </summary>
    public ReadOnlyObservableCollection<object> ApplicationThemes { get; } = new([]);

    /// <summary>
    /// Gets a read-only observable collection of available audio sample rates.
    /// </summary>
    public ReadOnlyObservableCollection<object> AudioSampleRates { get; } = new([]);

    /// <summary>
    /// Gets a read-only observable collection of available languages.
    /// </summary>
    public ReadOnlyObservableCollection<object> Languages { get; } = new([]);

    /// <summary>
    /// Gets a read-only observable collection of available noise presets.
    /// </summary>
    public ReadOnlyObservableCollection<object> NoisePresets { get; } = new([]);

    /// <summary>
    /// Gets a read-only observable collection of available system backdrops.
    /// </summary>
    public ReadOnlyObservableCollection<object> SystemBackdrops { get; } = new([]);

    #endregion

    #region Observable properties
    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    [ObservableProperty]
    public partial object? SelectedApplicationTheme { get; set; }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    [ObservableProperty]
    public partial int? SelectedAudioSampleRate { get; set; }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    [ObservableProperty]
    public partial object? SelectedLanguage { get; set; }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    [ObservableProperty]
    public partial string? SelectedDefaultNoisePreset { get; set; }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    [ObservableProperty]
    public partial object? SelectedSystemBackdrop { get; set; }

    /// <summary>
    /// Gets the title displayed inside the title bar control.
    /// </summary>
    [ObservableProperty]
    public partial string? TitleBarTitle { get; private set; } = "Fluent Noise Generator";

    /// <summary>
    /// Gets the title of the window.
    /// </summary>
    [ObservableProperty]
    public partial string? WindowTitle { get; private set; } = $"Fluent Noise Generator — {nameof(Settings)}";
    #endregion
}