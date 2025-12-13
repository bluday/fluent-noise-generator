using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.Factories;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly LocalizationService _localizationService;

    private readonly NoisePlaybackService _noisePlaybackService;

    private readonly WindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        IMessenger messenger = WeakReferenceMessenger.Default;

        LanguageService languageService = new(messenger);

        ThemeService themeService = new(messenger);

        _noisePlaybackService = new NoisePlaybackService(messenger);

        _localizationService = new(messenger);

        AppStringResources appStringResources = new(_localizationService.ResourceProvider);

        _windowService = new WindowService(
            new PlaybackWindowFactory(
                _noisePlaybackService,
                appStringResources,
                messenger
            ),
            new SettingsWindowFactory(
                _noisePlaybackService,
                languageService,
                themeService,
                _localizationService.ResourceProvider,
                appStringResources,
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
        _windowService.ShowSettingsWindow();
    }
    #endregion
}