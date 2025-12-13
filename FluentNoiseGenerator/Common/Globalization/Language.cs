using System;

namespace FluentNoiseGenerator.Common.Globalization;

/// <summary>
/// Represents the default implementation for the <see cref="ILanguage"/> interface.
/// </summary>
public class Language : ILanguage
{
    #region Properties
    /// <inheritdoc cref="ILanguage.DisplayName"/>
    public string DisplayName { get; }

    /// <inheritdoc cref="ILanguage.Name"/>
    public string Name { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class.
    /// </summary>
    /// <param name="name">
    /// The IETF BCP 47-compatible name of the language.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws when <paramref name="name"/> is <c>null</c>, contains an empty
    /// string value or only whitespace characters.
    /// </exception>
    public Language(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        System.Globalization.CultureInfo culture = new(name);

        DisplayName = culture.DisplayName;

        Name = culture.Name;
    }
    #endregion
}