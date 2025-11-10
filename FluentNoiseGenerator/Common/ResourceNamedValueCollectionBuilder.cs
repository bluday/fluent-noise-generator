using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;

namespace FluentNoiseGenerator.Common;

public sealed class ResourceNamedValueCollectionBuilder<TValue>
{
    private readonly Func<ResourceLoader> _resourceLoaderFactory;

    private readonly List<ResourceNamedValue<TValue>> _values = new();

    public ResourceNamedValueCollectionBuilder(Func<ResourceLoader> resourceLoaderFactory)
    {
        ArgumentNullException.ThrowIfNull(resourceLoaderFactory);

        _resourceLoaderFactory = resourceLoaderFactory;
    }

    public ResourceNamedValueCollectionBuilder<TValue> Add(string resourceId, TValue value)
    {
        _values.Add(new(value, resourceId, _resourceLoaderFactory));

        return this;
    }

    public List<ResourceNamedValue<TValue>> Build() => _values;
}