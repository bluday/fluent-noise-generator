using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.StringResources;
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
    private readonly LanguageService _languageService;

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

        LocalizedResourceProvider localizedResourceProvider = new();

        AppStringResources appStringResources = new(localizedResourceProvider);

        _languageService = new LanguageService(messenger);

        _themeService = new ThemeService(messenger);

        _windowService = new WindowService(
            new PlaybackWindowFactory(appStringResources, messenger),
            new SettingsWindowFactory(
                appStringResources,
                localizedResourceProvider,
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