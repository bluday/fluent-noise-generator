namespace FluentNoiseGenerator.Infrastructure.Messages;

/// <summary>
/// Represents a message for updating the current application language.
/// </summary>
/// <param name="Value">
/// The new application language to set.
/// </param>
public sealed record UpdateApplicationLanguageMessage(Common.Globalization.ILanguage Value);