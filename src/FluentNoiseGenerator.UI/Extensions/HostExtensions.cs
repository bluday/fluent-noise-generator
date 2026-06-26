#if USES_COMMUNITY_TOOLKIT_MVVM
using CommunityToolkit.Mvvm.DependencyInjection;
#endif
using Microsoft.Extensions.Hosting;
using System;

namespace FluentNoiseGenerator.UI.Extensions;

/// <summary>
/// Provides method extensions for <see cref="IHost"/> instances.
/// </summary>
public static class HostExtensions
{
    #region Static methods
    #if USES_COMMUNITY_TOOLKIT_MVVM
    /// <summary>
    /// Configures <see cref="Ioc.Default"/> to use the host's service provider.
    /// </summary>
    /// <param name="source">
    /// The host whose <see cref="IHost.Services"/> will be used.
    /// </param>
    /// <returns>
    /// The specified <see cref="IHost"/> to enable chaining.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static IHost ConfigureCommunityToolkitIoc(this IHost source)
    {
        ArgumentNullException.ThrowIfNull(source);

        Ioc.Default.ConfigureServices(source.Services);

        return source;
    }
    #endif
    #endregion
}