namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for managing the current theme of the application.
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Gets the current app settings instance.
    /// </summary>
    IAppSettings CurrentSettings { get; }
}