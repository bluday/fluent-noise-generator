using Microsoft.Windows.ApplicationModel.Resources;

namespace FluentNoiseGenerator.Common.Localization;

/// <summary>
/// Represents a provider for retrieving localized strings from resource files within the
/// application.
/// </summary>
public sealed class LocalizedResourceProvider
{
    #region Fields
    private readonly ResourceLoader _resourceLoader = new();
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
    public string Get(string key) => _resourceLoader.GetString(key);
    #endregion
}