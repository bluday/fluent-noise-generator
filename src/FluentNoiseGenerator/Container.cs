using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.Common.Services;
using FluentNoiseGenerator.UI.Playback.ViewModels;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.ViewModels;
using FluentNoiseGenerator.UI.Settings.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Represents the pre-configured IoC container for the application.
/// </summary>
public sealed partial class Container
{
    #region Properties
    /// <summary>
    /// Gets the <see cref="ServiceProvider"/> instance for the root scope.
    /// </summary>
    public ServiceProvider RootServiceProvider { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class.
    /// </summary>
    public Container()
    {
        ServiceCollection serviceDescriptors = new();

        Configure(serviceDescriptors);

        RootServiceProvider = serviceDescriptors.BuildServiceProvider();
    }
    #endregion

    #region Static methods
    private static void Configure(ServiceCollection services)
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

        services.AddTransient<PlaybackViewModel>();
        services.AddSingleton<PlaybackWindowFactory>();

        services.AddTransient<SettingsViewModel>();
        services.AddSingleton<SettingsWindowFactory>();
    }
    #endregion
}