using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.Foundation.Extensions;
using FluentNoiseGenerator.Foundation.Services;
using FluentNoiseGenerator.UI.Hosting;
using FluentNoiseGenerator.UI.Services;
using FluentNoiseGenerator.Playback.UI.ViewModels;
using FluentNoiseGenerator.Playback.UI.Windows;
using FluentNoiseGenerator.Settings.UI.ViewModels;
using FluentNoiseGenerator.Settings.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.Configuration;

/// <summary>
/// Provides a method for configuring and registering client-specific services.
/// </summary>
internal static class ServiceConfiguration
{
    /// <summary>
    /// Registers configured services to the specified service collection.
    /// </summary>
    /// <param name="services">
    /// The service descriptor collection to register all of the configured
    /// client services to.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if <paramref name="services"/> is <c>null</c>.
    /// </exception>
    internal static void Configure(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHostedService<HostedWinUI3ApplicationService>();

        services.AddTransient<Application, App>();

        services.AddLogging(LoggingConfiguration.Configure);

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
        services.AddTransientWithFactory<PlaybackWindow>();

        services.AddTransient<SettingsViewModel>();
        services.AddTransientWithFactory<SettingsWindow>();
    }
}