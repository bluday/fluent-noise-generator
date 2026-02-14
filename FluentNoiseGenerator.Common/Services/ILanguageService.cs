using FluentNoiseGenerator.Common.Globalization;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for updating the language of the application.
/// </summary>
public interface ILanguageService
{
    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    IEnumerable<ILanguage> AvailableLanguages { get; }

    /// <summary>
    /// Gets or sets the culture.
    /// </summary>
    ILanguage CurrentLanguage { get; set; }
}