using CommunityToolkit.Mvvm.Messaging;

using FluentNoiseGenerator.Features.Playback.Core.Services;
using FluentNoiseGenerator.Features.Playback.UI;
using FluentNoiseGenerator.Features.Settings.UI;

using Microsoft.Extensions.DependencyInjection;

namespace FluentNoiseGenerator.Client.Configuration;

/// <summary>
/// Provides a method for registering configured services to DI container.
/// </summary>
internal static class ServiceConfiguration
{
    private static void Configure(IServiceCollection services)
    {
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        services.AddSingleton<NoisePlaybackService>();

        services.AddTransient<PlaybackWindow>();
        services.AddTransient<PlaybackWindowViewModel>();

        services.AddTransient<SettingsWindow>();
        services.AddTransient<SettingsWindowViewModel>();
    }

    /// <summary>
    /// Builds a new <see cref="ServiceProvider"/> with all application
    /// services registered.
    /// </summary>
    /// <returns>
    /// The configured <see cref="ServiceProvider"/> instance.
    /// </returns>
    public static ServiceProvider BuildServiceProvider()
    {
        ServiceCollection services = new();

        Configure(services);

        return services.BuildServiceProvider();
    }
}