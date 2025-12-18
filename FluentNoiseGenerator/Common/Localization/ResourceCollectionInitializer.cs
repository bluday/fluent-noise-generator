using FluentNoiseGenerator.Common.Localization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentNoiseGenerator.Common.Localization;

/// <summary>
/// A utility for initializing resource properties within a resource collection.
/// </summary>
public static class ResourceCollectionInitializer
{
    public static void Initialize<TRootResourceCollection>(TRootResourceCollection resourceCollection)
        where TRootResourceCollection : class
    {
        ArgumentNullException.ThrowIfNull(resourceCollection);

        Type resourceCollectionType = typeof(TRootResourceCollection);

        if (resourceCollectionType.GetCustomAttribute<ResourceCollectionAttribute>() is null)
        {
            throw new InvalidOperationException();
        }

        IEnumerable<HasResourceSectionAttribute> hasResourceSectionAttributes = resourceCollectionType
            .GetCustomAttributes(typeof(HasResourceSectionAttribute<>))
            .Cast<HasResourceSectionAttribute>();


    }
}