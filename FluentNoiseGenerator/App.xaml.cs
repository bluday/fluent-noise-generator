using FluentNoiseGenerator.Factories;
using FluentNoiseGenerator.Services;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly LanguageService _languageService = new();

    private readonly ResourceService _resourceService = new();

    private readonly ThemeService _themeService = new();

    private readonly WindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _windowService = new WindowService(
            new PlaybackWindowFactory(
                _languageService,
                _resourceService,
                _themeService
            ),
            new SettingsWindowFactory(
                _languageService,
                _resourceService,
                _themeService
            )
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