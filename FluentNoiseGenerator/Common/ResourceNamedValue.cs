using Microsoft.Windows.ApplicationModel.Resources;
using System;

namespace FluentNoiseGenerator.Common;

public readonly struct ResourceNamedValue<TValue>
{
    private readonly string _resourceId;

    private readonly Func<ResourceLoader> _resourceLoaderFactory;

    public string ResourceId => _resourceId;

    public TValue Value { get; }

    public ResourceNamedValue(
        TValue                value,
        string                resourceId,
        Func<ResourceLoader>  resourceLoaderFactory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceId);
        ArgumentNullException.ThrowIfNull(resourceLoaderFactory);

        _resourceId = resourceId;

        _resourceLoaderFactory = resourceLoaderFactory;

        Value = value;
    }

    public override string ToString()
    {
        return _resourceLoaderFactory().GetString(_resourceId);
    }
}