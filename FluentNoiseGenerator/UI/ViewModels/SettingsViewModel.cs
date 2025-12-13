using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace FluentNoiseGenerator.UI.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsViewModel : ObservableObject
{
    #region Fields
    private ResourceNamedValue<ElementTheme>[] _availableApplicationThemes;

    private NamedValue<int>[] _availableAudioSampleRates;

    private NamedValue<CultureInfo>[] _availableLanguages;

    private ResourceNamedValue<string>[] _availableNoisePresets;

    private ResourceNamedValue<SystemBackdrop>[] _availableSystemBackdrops;

    private readonly bool _isInitializing;

    private readonly LanguageService _languageService;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    [ObservableProperty]
    public partial ResourceNamedValue<ElementTheme>? SelectedApplicationTheme { get; set; }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    [ObservableProperty]
    public partial NamedValue<int>? SelectedAudioSampleRate { get; set; }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    [ObservableProperty]
    public partial NamedValue<CultureInfo>? SelectedLanguage { get; set; }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    [ObservableProperty]
    public partial ResourceNamedValue<string>? SelectedDefaultNoisePreset { get; set; }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    [ObservableProperty]
    public partial ResourceNamedValue<SystemBackdrop>? SelectedSystemBackdrop { get; set; }

    /// <summary>
    /// Gets an observable collection of available application themes.
    /// </summary>
    public ObservableCollection<ResourceNamedValue<ElementTheme>> AvailableApplicationThemes
    {
        get => [.._availableApplicationThemes];
    }

    /// <summary>
    /// Gets an observable collection of available audio sample rates.
    /// </summary>
    public ObservableCollection<NamedValue<int>> AvailableAudioSampleRates
    {
        get => [.. _availableAudioSampleRates];
    }

    /// <summary>
    /// Gets an observable collection of available languages.
    /// </summary>
    public ObservableCollection<NamedValue<CultureInfo>> AvailableLanguages
    {
        get => [.. _availableLanguages];
    }

    /// <summary>
    /// Gets an observable collection of available noise presets.
    /// </summary>
    public ObservableCollection<ResourceNamedValue<string>> AvailableNoisePresets
    {
        get => [.. _availableNoisePresets];
    }

    /// <summary>
    /// Gets an observable collection of available system backdrops.
    /// </summary>
    public ObservableCollection<ResourceNamedValue<SystemBackdrop>> AvailableSystemBackdrops
    {
        get => [.. _availableSystemBackdrops];
    }

    /// <summary>
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public SettingsWindowStringResources StringResources { get; }

    /// <summary>
    /// Gets the path for the title bar icon.
    /// </summary>
    public string TitleBarIconPath { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources
    /// specifically for this view.
    /// </param>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <param name="languageService">
    /// The language service for managing the current application language.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsViewModel(
        SettingsWindowStringResources stringResources,
        LocalizedResourceProvider     localizedResourceProvider,
        LanguageService               languageService,
        IMessenger                    messenger)
    {
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);
        ArgumentNullException.ThrowIfNull(languageService);
        ArgumentNullException.ThrowIfNull(messenger);

        _availableApplicationThemes = [];
        _availableAudioSampleRates  = [];
        _availableLanguages         = [];
        _availableNoisePresets      = [];
        _availableSystemBackdrops   = [];

        _isInitializing = true;

        _languageService = languageService;

        _messenger = messenger;

        StringResources = stringResources;

        TitleBarIconPath = System.IO.Path.Combine(
            AppContext.BaseDirectory,
            "Assets/Icon-64.ico"
        );

        InitializeValueNamedOptions(localizedResourceProvider);
        InitializeResourceNamedOptions(localizedResourceProvider);

        _isInitializing = false;
    }
    #endregion

    #region Property changing and changed methods
    partial void OnSelectedLanguageChanged(
        NamedValue<CultureInfo>? oldValue,
        NamedValue<CultureInfo>? newValue)
    {
        if (oldValue?.Value == newValue?.Value || _isInitializing)
        {
            return;
        }

        if (newValue?.Value is not CultureInfo culture)
        {
            return;
        }

        try
        {
            _languageService.Culture = culture;

            RefreshLocalizedContent();
        } catch { }
    }
    #endregion

    #region Instance methods
    private void InitializeResourceNamedOptions(LocalizedResourceProvider localizedResourceProvider)
    {
        _availableApplicationThemes = new ResourceNamedValue<ElementTheme>[]
        {
            new(
                new StringResource("Common/System", localizedResourceProvider),
                ElementTheme.Default
            ),
            new(
                new StringResource("Common/Dark", localizedResourceProvider),
                ElementTheme.Dark
            ),
            new(
                new StringResource("Common/Light", localizedResourceProvider),
                ElementTheme.Light
            )
        };

        _availableNoisePresets = new ResourceNamedValue<string>[]
        {
            new(
                new StringResource("Common/Blue", localizedResourceProvider),
                string.Empty
            ),
            new(
                new StringResource("Common/Brownian", localizedResourceProvider),
                string.Empty
            ),
            new(
                new StringResource("Common/White", localizedResourceProvider),
                string.Empty
            )
        };

        _availableSystemBackdrops = new ResourceNamedValue<SystemBackdrop>[]
        {
            new(
                new StringResource("SystemBackdrop/Mica", localizedResourceProvider),
                new MicaBackdrop()
            ),
            new(
                new StringResource("SystemBackdrop/MicaAlt", localizedResourceProvider),
                new MicaBackdrop { Kind = MicaKind.BaseAlt }
            ),
            new(
                new StringResource("SystemBackdrop/Acrylic", localizedResourceProvider),
                new DesktopAcrylicBackdrop()
            ),
            new(
                new StringResource("Common/None", localizedResourceProvider),
                null!
            )
        };

    }

    private void InitializeValueNamedOptions(LocalizedResourceProvider localizedResourceProvider)
    {
        string audioSampleRateUnit = localizedResourceProvider.Get("Units/Hertz/Short");

        string GetUnitSuffixedStringFormat(int value)
        {
            return $"{value} {audioSampleRateUnit}";
        }

        _availableAudioSampleRates = new NamedValue<int>[]
        {
            new(AudioSampleRates.Rate48000Hz, GetUnitSuffixedStringFormat),
            new(AudioSampleRates.Rate44100Hz, GetUnitSuffixedStringFormat)
        };

        _availableLanguages = ApplicationLanguages.ManifestLanguages
            .Select(language => new NamedValue<CultureInfo>(
                value:     new CultureInfo(language),
                formatter: language => language.NativeName
            ))
            .ToArray();
    }

    private void RefreshLocalizedContent()
    {
        OnPropertyChanged(nameof(StringResources));
    }
    #endregion
}