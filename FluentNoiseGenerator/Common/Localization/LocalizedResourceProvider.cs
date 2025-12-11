using Microsoft.Windows.ApplicationModel.Resources;

namespace FluentNoiseGenerator.Common.Localization;

/// <summary>
/// Represents a provider for retrieving localized strings from resource files within the
/// application.
/// </summary>
public sealed class LocalizedResourceProvider
{
    #region Fields
    private ResourceLoader _resourceLoader = null!;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageService"/> class.
    /// </summary>
    public LocalizedResourceProvider()
    {
        UpdateResourceLoader();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Gets the localized resource value using the specified key.
    /// </summary>
    /// <param name="key">
    /// The key for the resource.
    /// </param>
    /// <returns>
    /// The localized resource value as a <see cref="string"/>.
    /// </returns>
    public string Get(string key)
    {
        return _resourceLoader.GetString(key);
    }

    /// <summary>
    /// Updates the current <see cref="ResourceLoader"/> instance.
    /// </summary>
    public void UpdateResourceLoader()
    {
        _resourceLoader = new ResourceLoader();
    }
    #endregion
}