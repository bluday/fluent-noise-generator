namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for resolving localized application resources.
/// </summary>
public interface ILocalizationService
{
    /// <summary>
    /// Gets the current localized resource provider instance.
    /// </summary>
    Localization.LocalizedResourceProvider ResourceProvider { get; }
}