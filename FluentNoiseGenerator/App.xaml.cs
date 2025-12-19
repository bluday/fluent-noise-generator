using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.Windows;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly LanguageService _languageService;

    private readonly LocalizationService _localizationService;

    private readonly NoisePlaybackService _noisePlaybackService;

    private readonly SettingsService _settingsService;

    private readonly ThemeService _themeService;

    private readonly WindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        IMessenger messenger = WeakReferenceMessenger.Default;

        _languageService = new LanguageService(messenger);

        _localizationService = new LocalizationService(messenger);

        _noisePlaybackService = new NoisePlaybackService(messenger);

        _settingsService = new SettingsService(messenger);

        _themeService = new ThemeService(messenger);

        AppResources appResources = new();

        _windowService = new WindowService(
            new PlaybackWindowFactory(appResources, messenger),
            new SettingsWindowFactory(
                appResources,
                _settingsService.CurrentSettings,
                messenger
            ),
            messenger
        );

        InitializeComponent();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _windowService.ShowPlaybackWindow();
    }
    #endregion
}
