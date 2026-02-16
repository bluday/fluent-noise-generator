using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.Common.Services;
using FluentNoiseGenerator.UI.Playback.ViewModels;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.ViewModels;
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
    private readonly Container _container;

    private readonly IWindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _container = new Container(ConfigureServices);

        IKeyedServiceProvider rootServiceProvider = _container.RootServiceProvider;

        // TODO: Resolve critical services in order to run the application.

        _windowService = rootServiceProvider.GetRequiredService<IWindowService>();

        Ioc.Default.ConfigureServices(rootServiceProvider);

        InitializeComponent();
    }
    #endregion

    #region Instance methods
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        services.AddSingleton(
            serviceProvider => serviceProvider
                .GetRequiredService<ISettingsService>()
                .CurrentSettings
        );

        services.AddSingleton<ILanguageService, LanguageService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IToastNotificationService, ToastNotificationService>();

        services.AddSingleton<INoisePlaybackService, NoisePlaybackService>();

        services.AddSingleton<IBackdropService, BackdropService>();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IWindowService, WindowService>();

        services.AddSingleton<PlaybackWindowFactory>();
        services.AddSingleton<SettingsWindowFactory>();

        services.AddSingleton<PlaybackViewModel>();
        services.AddSingleton<SettingsViewModel>();
    }

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