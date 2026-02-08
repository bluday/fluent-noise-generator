using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.Common.Services;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Resources;
using FluentNoiseGenerator.UI.Services;
using FluentNoiseGenerator.UI.Settings.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace FluentNoiseGenerator;

/// <summary>
/// Represents the DI (Dependency Injection) container for the client, providing access
/// to the root service provider, a collection of all registered services, and a method
/// to create new service scopes for service resolution.
/// </summary>
internal sealed class Container
{
    private readonly ServiceProvider _rootServiceProvider;

    /// <summary>
    /// Gets the <see cref="ServiceProvider"/> instance for the root scope of the container.
    /// </summary>
    internal IKeyedServiceProvider RootServiceProvider => _rootServiceProvider;

    /// <summary>
    /// Gets a read-only collection of all service descriptors that have been registered
    /// witin the container, providing information about the available services.
    /// </summary>
    internal IReadOnlyCollection<ServiceDescriptor> RegisteredServices { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class.
    /// </summary>
    internal Container()
    {
        ServiceCollection services = new();

        ConfigureServices(services);

        _rootServiceProvider = services.BuildServiceProvider();

        RegisteredServices = services.AsReadOnly();

        Ioc.Default.ConfigureServices(_rootServiceProvider);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
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
    }

    /// <summary>
    /// Creates a new limited scope for resolving services, allowing for isolated
    /// dependencies within a specific context or operation.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IServiceScope"/> representing the new scope.
    /// </returns>
    internal IServiceScope CreateScope()
    {
        return _rootServiceProvider.CreateScope();
    }
}