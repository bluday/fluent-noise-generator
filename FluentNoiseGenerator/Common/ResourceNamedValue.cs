using Microsoft.Windows.ApplicationModel.Resources;
using System;

namespace FluentNoiseGenerator.Common;

/// <summary>
/// Represents a value of type <typeparamref name="TValue"/> that is associated with a
/// localized resource identifier. When converted to a string via <see cref="ToString"/>,
/// the localized display name is retrieved using a <see cref="ResourceLoader"/> instance
/// provided by the supplied factory function.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value contained in the <see cref="ResourceNamedValue{TValue}"/>.
/// </typeparam>
public readonly struct ResourceNamedValue<TValue>
{
    #region Fields
    private readonly string _resourceId;

    private readonly Func<ResourceLoader> _resourceLoaderFactory;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the unique identifier of the localized resource used to resolve the display
    /// name for this value when calling <see cref="ToString"/>.
    /// </summary>
    public string ResourceId => _resourceId;

    /// <summary>
    /// Gets the underlying value stored in this <see cref="ResourceNamedValue{TValue}"/>.
    /// </summary>
    public TValue? Value { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNamedValue{TValue}"/> structure
    /// with the specified value, resource identifier, and factory function for obtaining
    /// <see cref="ResourceLoader"/> instances.
    /// </summary>
    /// <param name="value">
    /// The value to store in the <see cref="ResourceNamedValue{TValue}"/>.
    /// </param>
    /// <param name="resourceId">
    /// The unique identifier of the localized resource. This identifier is used to retrieve
    /// a displayable string when <see cref="ToString"/> is called.
    /// </param>
    /// <param name="resourceLoaderFactory">
    /// A factory function that produces <see cref="ResourceLoader"/> instances capable of
    /// resolving localized strings. This factory is invoked when converting the value to a
    /// string representation.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="resourceId"/> is <c>null</c> or consists only of whitespace
    /// characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="resourceLoaderFactory"/> is <c>null</c>.
    /// </exception>
    public ResourceNamedValue(
        TValue?               value,
        string                resourceId,
        Func<ResourceLoader>  resourceLoaderFactory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceId);
        ArgumentNullException.ThrowIfNull(resourceLoaderFactory);

        _resourceId = resourceId;

        _resourceLoaderFactory = resourceLoaderFactory;

        Value = value;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Returns the localized string representation of this value by resolving the resource
    /// identified by <see cref="ResourceId"/> using a <see cref="ResourceLoader"/> obtained
    /// from the <see cref="_resourceLoaderFactory"/>.
    /// </summary>
    /// <returns>
    /// A localized string corresponding to the resource identifier specified during
    /// construction of this instance.
    /// </returns>
    /// <remarks>
    /// This method does not use the underlying <see cref="Value"/> directly when forming the
    /// string representation. Instead, it retrieves a localized name or description for the
    /// value, allowing the value to be displayed in a user-friendly, localized manner.
    /// </remarks>
    public override string ToString()
    {
        return _resourceLoaderFactory().GetString(_resourceId);
    }
    #endregion
}