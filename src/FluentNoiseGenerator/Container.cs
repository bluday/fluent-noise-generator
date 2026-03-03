using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Represents a pre-configured IoC container for the client.
/// </summary>
internal sealed class Container
{
    #region Instance properties
    /// <summary>
    /// Gets the service provider for the root scope of the container.
    /// </summary>
    internal IKeyedServiceProvider RootServiceProvider { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Container"/> class
    /// using the specified service configurator.
    /// </summary>
    /// <param name="serviceConfigurator">
    /// The function that registers configured services to the container.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if <paramref name="serviceConfigurator"/> is <c>null</c>.
    /// </exception>
    internal Container(Action<IServiceCollection> serviceConfigurator)
    {
        ArgumentNullException.ThrowIfNull(serviceConfigurator);

        ServiceCollection services = [];

        serviceConfigurator(services);

        RootServiceProvider = services.BuildServiceProvider();
    }
    #endregion
}