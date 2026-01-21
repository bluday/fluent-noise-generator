using System;

namespace FluentNoiseGenerator.Common.Localization.Attributes;

/// <summary>
/// Specifies that the class is a collection of resource instances.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ResourceCollectionAttribute : Attribute { }