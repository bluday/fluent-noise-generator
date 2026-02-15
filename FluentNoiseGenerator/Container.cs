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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentNoiseGenerator;

/// <summary>
/// Represents the DI (Dependency Injection) container for the client, providing access
/// to the root service provider, a collection of all registered services, and a method
/// to create new service scopes for service resolution.
/// </summary>
internal sealed partial class Container : IDisposable
{
    private bool _isDisposed;

    private readonly ServiceProvider _rootServiceProvider;

    private readonly Collection<IServiceScope> _scopes;

    /// <summary>
    /// Gets a value indicating whether the container has been disposed.
    /// </summary>
    public bool IsDisposed => _isDisposed;

    /// <summary>
    /// Gets a read-only collection of all service descriptors that have been registered
    /// witin the container, providing information about the available services.
    /// </summary>
    internal IReadOnlyCollection<ServiceDescriptor> RegisteredServices { get; }

    /// <summary>
    /// Gets the <see cref="ServiceProvider"/> instance for the root scope of the container.
    /// </summary>
    internal IKeyedServiceProvider RootServiceProvider => _rootServiceProvider;

    /// <summary>
    /// Gets a read-only collection of all scopes.
    /// </summary>
    internal IReadOnlyCollection<IServiceScope> Scopes => _scopes.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class.
    /// </summary>
    internal Container()
    {
        ServiceCollection services = new();

        ConfigureServices(services);

        _rootServiceProvider = services.BuildServiceProvider();

        _scopes = [];

        RegisteredServices = services.AsReadOnly();

        Ioc.Default.ConfigureServices(_rootServiceProvider);
    }

    private static void ConfigureServices(IServiceCollection services)
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
    /// Creates a new limited scope for resolving services, allowing for isolated
    /// dependencies within a specific context or operation.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="IServiceScope"/> representing the new scope.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Throws if the current instance has been disposed of.
    /// </exception>
    internal IServiceScope CreateScope()
    {
        if (_isDisposed)
        {
            throw new InvalidOperationException();
        }

        IServiceScope scope = _rootServiceProvider.CreateScope();

        _scopes.Add(scope);

        return scope;
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    /// <remarks>
    /// Automatically disposes of all services within all active scopes before
    /// disposing of each scope.
    /// </remarks>
    public void Dispose()
    {
        if (_isDisposed) return;

        foreach (IServiceScope scope in _scopes)
        {
            scope?.Dispose();
        }

        _isDisposed = true;
    }
}