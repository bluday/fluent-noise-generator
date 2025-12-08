using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for accessing application resources.
/// </summary>
public sealed class ResourceService
{
    #region Fields
    private readonly Func<ResourceDictionary> _resourceDictionaryFactory;
    #endregion

    #region Properties
    /// <summary>
    /// Absolute path for the 64x64 sized application icon as a string.
    /// </summary>
    public string IconPath => System.IO.Path.Combine(
        AppContext.BaseDirectory, Get<string>("AppIconPath")
    );
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceService"/> class.
    /// </summary>
    /// <param name="resourceDictionaryFactory">
    /// A factory for accessing the top-level <see cref="ResourceDictionary"/> instance
    /// through 
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="resourceDictionaryFactory"/> is <c>null</c>.
    /// </exception>
    public ResourceService(Func<ResourceDictionary> resourceDictionaryFactory)
    {
        ArgumentNullException.ThrowIfNull(resourceDictionaryFactory);

        _resourceDictionaryFactory = resourceDictionaryFactory;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Gets a resource using the specified key.
    /// </summary>
    /// <typeparam name="T">
    /// The type to cast the retrieved resource as.
    /// </typeparam>
    /// <param name="key">
    /// The key of the resource.
    /// </param>
    /// <returns>
    /// The resource cast as <typeparamref name="T"/>.
    /// </returns>
    public T Get<T>(string key) => (T)_resourceDictionaryFactory()[key];
    #endregion
}