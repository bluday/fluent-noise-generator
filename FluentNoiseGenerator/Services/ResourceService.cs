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
    public string AppIconPath => System.IO.Path.Combine(
        AppContext.BaseDirectory, Get<string>("AppIconPath")
    );

    /// <summary>
    /// Gets the root <see cref="ResourceDictionary"/> instance.
    /// </summary>
    public ResourceDictionary Resources => _resourceDictionaryFactory();
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceService"/> class.
    /// </summary>
    public ResourceService()
    {
        _resourceDictionaryFactory = () => Application.Current.Resources;
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