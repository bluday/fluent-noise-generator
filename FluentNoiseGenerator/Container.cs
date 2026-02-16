using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentNoiseGenerator;

/// <summary>
/// A wrapper for <see cref="ServiceProvider"/>, providing additional information about the
/// container, such as registered service descriptors, active scopes, and whether the
/// container has been disposed of.
/// </summary>
public sealed class Container : IDisposable
{
    #region Fields
    private bool _isDisposed;

    private readonly ServiceProvider _rootServiceProvider;

    private readonly Collection<IServiceScope> _scopes;

    private readonly ServiceCollection _services;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the container has been disposed.
    /// </summary>
    public bool IsDisposed => _isDisposed;

    /// <summary>
    /// Gets a read-only collection of all service descriptors that have been registered
    /// witin the container, providing information about the available services.
    /// </summary>
    public IReadOnlyCollection<ServiceDescriptor> RegisteredServices => _services.AsReadOnly();

    /// <summary>
    /// Gets the <see cref="ServiceProvider"/> instance for the root scope of the container.
    /// </summary>
    public IKeyedServiceProvider RootServiceProvider => _rootServiceProvider;

    /// <summary>
    /// Gets a read-only collection of all scopes.
    /// </summary>
    public IReadOnlyCollection<IServiceScope> Scopes => _scopes.AsReadOnly();
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class.
    /// </summary>
    /// <param name="servicesConfigurator">
    /// A function that registers all service descriptors for the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if <paramref name="servicesConfigurator"/> is <c>null</c>.
    /// </exception>
    public Container(Action<IServiceCollection> servicesConfigurator)
    {
        ArgumentNullException.ThrowIfNull(servicesConfigurator);

        _services = [];

        servicesConfigurator(_services);

        _rootServiceProvider = _services.BuildServiceProvider();

        _scopes = [];
    }
    #endregion

    #region Methods
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

        GC.SuppressFinalize(this);

        _isDisposed = true;
    }
    #endregion
}