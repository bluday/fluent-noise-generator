namespace FluentNoiseGenerator.Common.Globalization;

/// <summary>
/// Defines a contract for an abstracted application language.
/// </summary>
public interface ILanguage
{
    /// <summary>
    /// Gets the full localized name for the language.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Gets the unqiue name of the locale for the language.
    /// </summary>
    /// <remarks>
    /// This value is used for distinguishing between languages and locales.
    /// </remarks>
    string Name { get; }
}