using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentNoiseGenerator.Common;

/// <summary>
/// Provides a builder for creating collections of <see cref="ResourceNamedValue{TValue}"/>
/// structs.
/// </summary>
/// <remarks>
/// The builder ensures that all values added to the collection use the same value type
/// specified by <typeparamref name="TValue"/>.
/// </remarks>
/// <typeparam name="TValue">
/// The type of the value associated with each resource identifier. All entries added to the
/// builder must use this same value type. For example, if <typeparamref name="TValue"/> is
/// <c>int</c>, then only integer values can be added.
/// </typeparam>
public sealed class ResourceNamedValueCollectionBuilder<TValue>
{
    #region Fields
    private readonly Func<ResourceLoader> _resourceLoaderFactory;

    private readonly Collection<ResourceNamedValue<TValue>> _values;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNamedValueCollectionBuilder{TValue}"/>
    /// class using the specified <see cref="ResourceLoader"/> factory.
    /// </summary>
    /// <param name="resourceLoaderFactory">
    /// The factory function used to create <see cref="ResourceLoader"/> instances for retrieving
    /// localized resources associated with the added values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="resourceLoaderFactory"/> is <c>null</c>.
    /// </exception>
    public ResourceNamedValueCollectionBuilder(Func<ResourceLoader> resourceLoaderFactory)
    {
        ArgumentNullException.ThrowIfNull(resourceLoaderFactory);

        _resourceLoaderFactory = resourceLoaderFactory;

        _values = [];
    }
    #endregion

    #region Methods
    /// <summary>
    /// Adds a new <see cref="ResourceNamedValue{TValue}"/> to the collection using the specified
    /// resource identifier and value. The value must be of the type specified by
    /// <typeparamref name="TValue"/>.
    /// </summary>
    /// <param name="resourceId">
    /// The unique identifier used to resolve the localized resource.
    /// </param>
    /// <param name="value">
    /// The value associated with the resource identifier. Must be of type <typeparamref name="TValue"/>.
    /// </param>
    /// <returns>
    /// The current instance, enabling additional calls to be chained.
    /// </returns>
    public ResourceNamedValueCollectionBuilder<TValue> Add(string resourceId, TValue value)
    {
        _values.Add(new(value, resourceId, _resourceLoaderFactory));

        return this;
    }

    /// <summary>
    /// Builds the final read-only collection of resource-named structs.
    /// </summary>
    /// <returns>
    /// A read-only collection containing all configured <see cref="ResourceNamedValue{TValue}"/>
    /// structs, each using the value type specified by <typeparamref name="TValue"/>.
    /// </returns>
    public IReadOnlyCollection<ResourceNamedValue<TValue>> Build()
    {
        return _values.AsReadOnly();
    }
    #endregion
}