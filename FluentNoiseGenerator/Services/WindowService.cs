using FluentNoiseGenerator.UI.Windows;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for managing the main and settings windows. This class ensures that windows are
/// properly created, restored, and updated when necessary.
/// </summary>
internal sealed class WindowService
{
    #region Fields
    private PlaybackWindow? _mainWindow;

    private SettingsWindow? _settingsWindow;

    private readonly ResourceService _resourceService;

    private readonly ThemeService _themeService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowService"/> class.
    /// </summary>
    /// <param name="themeService">
    /// The theme service for retrieving and updating the current application theme.
    /// </param>
    /// <param name="resourceService">
    /// The resource service for retrieving application resources.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    internal WindowService(ThemeService themeService, ResourceService resourceService)
    {
        ArgumentNullException.ThrowIfNull(themeService);
        ArgumentNullException.ThrowIfNull(resourceService);

        _resourceService = resourceService;
        _themeService    = themeService;

        RegisterEventHandlers();
    }
    #endregion

    #region Event handlers
    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        _themeService.CurrentTheme = e;
    }

    private void _settingsWindow_Closed(object sender, WindowEventArgs args)
    {
        UnregisterSettingsWindowEventHandlers();
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
        _themeService.CurrentSystemBackdrop = e;
    }

    private void _themeService_CurrentThemeChanged(ElementTheme value)
    {
        BulkApplyRequestedTheme(value);
    }

    private void _themeService_CurrentSystemBackdropChanged(SystemBackdrop? value)
    {
        BulkApplySystemBackdrop(value);
    }
    #endregion

    #region Methods
    private void BulkApplyRequestedTheme(ElementTheme value)
    {
        _mainWindow?.UpdateRequestedTheme(value);
        _settingsWindow?.UpdateRequestedTheme(value);
    }

    private void BulkApplySystemBackdrop(SystemBackdrop? value)
    {
        _mainWindow?.UpdateSystemBackdrop(value);
        _settingsWindow?.UpdateSystemBackdrop(value);
    }

    private void CreatePlaybackWindow()
    {
        _mainWindow = new PlaybackWindow(_resourceService, ShowSettingsWindow);

        _mainWindow.RefreshLocalizedContent();
        _mainWindow.RetrieveAndUpdateDpiScaleFactor();
        _mainWindow.ConfigureAppWindow();
        _mainWindow.ConfigureTitleBar();
        _mainWindow.Activate();
    }

    private void CreateSettingsWindow()
    {
        _settingsWindow = new SettingsWindow(_resourceService);

        RegisterSettingsWindowEventHandlers();

        _settingsWindow.ConfigureAppWindow();
        _settingsWindow.ConfigureTitleBar();
        _settingsWindow.RefreshLocalizedContent();
        _settingsWindow.Activate();
    }

    private void RegisterEventHandlers()
    {
        _themeService.CurrentSystemBackdropChanged += _themeService_CurrentSystemBackdropChanged;
        _themeService.CurrentThemeChanged          += _themeService_CurrentThemeChanged;
    }

    private void RegisterSettingsWindowEventHandlers()
    {
        _settingsWindow!.ApplicationThemeChanged += _settingsWindow_ApplicationThemeChanged;
        _settingsWindow.Closed                   += _settingsWindow_Closed;
        _settingsWindow.SystemBackdropChanged    += _settingsWindow_SystemBackdropChanged;
    }

    private void RestorePlaybackWindow()
    {
        _mainWindow!.Restore();
        _mainWindow.Focus();
    }

    private void RestoreSettingsWindow()
    {
        _settingsWindow!.Restore();
        _settingsWindow.Focus();
    }

    private void UnregisterSettingsWindowEventHandlers()
    {
        _settingsWindow!.ApplicationThemeChanged -= _settingsWindow_ApplicationThemeChanged;
        _settingsWindow.SystemBackdropChanged    -= _settingsWindow_SystemBackdropChanged;

        _settingsWindow = null;
    }

    /// <summary>
    /// Displays the playback window, setting up necessary configurations like DPI scaling, native
    /// app window settings, and the title bar.
    /// </summary>
    public void ShowPlaybackWindow()
    {
        if (_mainWindow?.HasClosed is false)
        {
            RestorePlaybackWindow();

            return;
        }

        CreatePlaybackWindow();
    }

    /// <summary>
    /// Displays the settings window, either restoring it if it is open already or creating
    /// a new instance if it was closed. Listens for theme and backdrop changes.
    /// </summary>
    /// <remarks>
    /// Restores the window if it was previously closed. Otherwise, a new instance is created,
    /// and event handlers for theme and backdrop changes are registered.
    /// </remarks>
    public void ShowSettingsWindow()
    {
        if (_settingsWindow?.HasClosed is false)
        {
            RestoreSettingsWindow();

            return;
        }

        CreateSettingsWindow();
    }
    #endregion
}