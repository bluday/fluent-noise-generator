using System;

namespace FluentNoiseGenerator.Common.Localization.Attributes;

/// <summary>
/// Specifies the unique resource identifier for a property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ResourceIdAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the identifier of the targeted resource.
    /// </summary>
    public string Value { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceIdAttribute"/> class.
    /// </summary>
    /// <param name="value">
    /// The identifier of the targeted resource, as a <see cref="string"/>.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws when <see cref="value"/> is <c>null</c>.
    /// </exception>
    public ResourceIdAttribute(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }
    #endregion
}