using System;

namespace FluentNoiseGenerator.Common.Localization.Attributes;

/// <summary>
/// Specifies the unique resource identifier for a property.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HasResourceSectionAttribute : Attribute
{
    #region Properties
    /// <summary>
    /// Gets the name of the property that will be generated.
    /// </summary>
    public string? PropertyName { get; }

    /// <summary>
    /// The type of the section to set the generated property type to.
    /// </summary>
    public Type? SectionType { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="HasResourceSectionAttribute"/> class.
    /// </summary>
    public HasResourceSectionAttribute(Type sectionType) : this(sectionType, sectionType.Name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="HasResourceSectionAttribute"/> class
    /// using the specified name for the property that will be generated.
    /// </summary>
    /// <param name="sectionType">
    /// The section type that specifies the <see cref="ResourceCollectionAttribute"/> attribute.
    /// </param>
    /// <param name="propertyName">
    /// The name of the property for the section to generate.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws when any of the parameters are <c>null</c> or white space.
    /// </exception>
    public HasResourceSectionAttribute(Type sectionType, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(sectionType);
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        PropertyName = propertyName;

        SectionType = sectionType;
    }
    #endregion
}

/// <summary>
/// Specifies the unique resource identifier for a property.
/// </summary>
/// <typeparam name="TSection">
/// The resource collection type for the section that will be set as the type for the
/// generated property.
/// </typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class HasResourceSectionAttribute<TSection> : HasResourceSectionAttribute where TSection : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HasResourceSectionAttribute"/> class.
    /// </summary>
    public HasResourceSectionAttribute() : base(typeof(TSection)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="HasResourceSectionAttribute"/> class
    /// using the specified name for the property that will be generated.
    /// </summary>
    /// <param name="propertyName">
    /// The name of the property for the section to generate.
    /// </param>
    public HasResourceSectionAttribute(string propertyName) : base(typeof(TSection), propertyName) { }
}