namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Service for managing the main and settings windows.
/// </summary>
public interface IWindowService
{
    /// <summary>
    /// Displays the playback window, setting up necessary configurations like DPI scaling,
    /// native app window settings, and the title bar.
    /// </summary>
    void ShowPlaybackWindow();

    /// <summary>
    /// Displays the settings window, either restoring it if it's already open or creating
    /// a new instance if it was closed. Listens for theme and backdrop changes.
    /// </summary>
    /// <remarks>
    /// Restores the window if it was previously closed. Otherwise, a new instance is
    /// created, and event handlers for theme and backdrop changes are registered.
    /// </remarks>
    void ShowSettingsWindow();
}