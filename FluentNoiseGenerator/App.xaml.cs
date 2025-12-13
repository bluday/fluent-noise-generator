using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.UI.Factories;
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

        _localizationService = new(messenger);

        AppStringResources appStringResources = new(_localizationService.ResourceProvider);

        _languageService = new LanguageService(messenger);

        _themeService = new ThemeService(messenger);

        _windowService = new WindowService(
            new PlaybackWindowFactory(appStringResources, messenger),
            new SettingsWindowFactory(
                appStringResources,
                _localizationService.ResourceProvider,
                _languageService,
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