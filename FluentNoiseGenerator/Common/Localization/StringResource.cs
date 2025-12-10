using Microsoft.Windows.ApplicationModel.Resources;
using System;

namespace FluentNoiseGenerator.Common.Localization;

/// <summary>
/// Represents a localized string resource with a unique identifier whose value can be
/// retrieved through a <see cref="ResourceLoader"/> instance if available.
/// </summary>
public readonly struct StringResource
{
    #region Fields
    private readonly LocalizedResourceProvider? _localizedResourceProvider;

    private readonly string _key;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the unique identifier of the localized resource used to resolve the display
    /// name for this value when calling <see cref="ToString"/>.
    /// </summary>
    public string Key => _key;

    /// <summary>
    /// Gets the localized resource value.
    /// </summary>
    public string? Value => _localizedResourceProvider?.Get(_key);
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNamedValue{TValue}"/> structure
    /// using the specified key or resource identifier.
    /// </summary>
    /// <param name="key">
    /// The unique identifier of the resource.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="key"/> is <c>null</c> or contains only whitespace.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="localizedResourceProvider"/> is <c>null</c>.
    /// </exception>
    public StringResource(string key, LocalizedResourceProvider localizedResourceProvider)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);

        _key = key;

        _localizedResourceProvider = localizedResourceProvider;
    }
    #endregion
}