using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly ServiceProvider _rootServiceProvider;

    private readonly WindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _rootServiceProvider = CreateContainer();

        Ioc.Default.ConfigureServices(_rootServiceProvider);

        _windowService = _rootServiceProvider.GetRequiredService<WindowService>();

        InitializeComponent();
    }
    #endregion

    #region Instance methods
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

    #region Static methods
    private static ServiceProvider CreateContainer()
    {
        ServiceCollection services = new();

        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        services.AddSingleton<AppResources>();

        services.AddSingleton<IAppSettings>(
            serviceProvider => serviceProvider
                .GetRequiredService<SettingsService>()
                .CurrentSettings
        );

        services.AddSingleton<PlaybackWindowFactory>();
        services.AddSingleton<SettingsWindowFactory>();

        services.AddSingleton<LanguageService>();
        services.AddSingleton<LocalizationService>();
        services.AddSingleton<NoisePlaybackService>();
        services.AddSingleton<SettingsService>();
        services.AddSingleton<ThemeService>();
        services.AddSingleton<WindowService>();

        return services.BuildServiceProvider();
    }
    #endregion
}
