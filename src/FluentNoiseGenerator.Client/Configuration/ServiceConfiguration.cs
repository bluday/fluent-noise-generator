using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Features.Playback.Core.Services;
using FluentNoiseGenerator.Features.Playback.UI.ViewModels;
using FluentNoiseGenerator.Features.Settings.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentNoiseGenerator.Client.Configuration;

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
    /// Throws if <paramref name="services"/> is <see langword="null"/>.
    /// </exception>
    internal static void Configure(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        services.AddSingleton<INoisePlaybackService, NoisePlaybackService>();

        services.AddTransient<PlaybackWindowViewModel>();

        services.AddTransient<SettingsWindowViewModel>();
    }
}