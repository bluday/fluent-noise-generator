using FluentNoiseGenerator.UI.Windows;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace FluentNoiseGenerator.Managers;

/// <summary>
/// Manages the main and settings windows. This class ensures that windows are properly
/// created, restored, and updated when necessary.
/// </summary>
public sealed class WindowManager
{
    #region Fields
    private MainWindow? _mainWindow;

    private SettingsWindow? _settingsWindow;

    private readonly ThemeManager _themeManager;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowManager"/> class.
    /// </summary>
    /// <param name="themeManager">
    /// The application theme manager for updating the current theme across all windows
    /// and views.
    /// </param>
    public WindowManager(ThemeManager themeManager)
    {
        _themeManager = themeManager;

        RegisterEventHandlers();
    }
    #endregion

    #region Event handlers
    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        _themeManager.CurrentTheme = e;
    }

    private void _settingsWindow_Closed(object sender, WindowEventArgs args)
    {
        UnregisterSettingsWindowEventHandlers();
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
        _themeManager.CurrentSystemBackdrop = e;
    }

    private void _themeManager_CurrentThemeChanged(ElementTheme value)
    {
        BulkApplyRequestedTheme(value);
    }

    private void _themeManager_CurrentSystemBackdropChanged(SystemBackdrop? value)
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

    private void CreateMainWindow()
    {
        _mainWindow = new MainWindow
        {
            SettingsWindowFactory = ShowSettingsWindow
        };

        _mainWindow.RefreshLocalizedContent();
        _mainWindow.RetrieveAndUpdateDpiScaleFactor();
        _mainWindow.ConfigureAppWindow();
        _mainWindow.ConfigureTitleBar();
        _mainWindow.Activate();
    }

    private void CreateSettingsWindow()
    {
        _settingsWindow = new SettingsWindow();

        RegisterSettingsWindowEventHandlers();

        _settingsWindow.ConfigureAppWindow();
        _settingsWindow.ConfigureTitleBar();
        _settingsWindow.RefreshLocalizedContent();
        _settingsWindow.Activate();
    }

    private void RegisterEventHandlers()
    {
        _themeManager.CurrentSystemBackdropChanged += _themeManager_CurrentSystemBackdropChanged;
        _themeManager.CurrentThemeChanged          += _themeManager_CurrentThemeChanged;
    }

    private void RegisterSettingsWindowEventHandlers()
    {
        _settingsWindow!.ApplicationThemeChanged += _settingsWindow_ApplicationThemeChanged;
        _settingsWindow.Closed                   += _settingsWindow_Closed;
        _settingsWindow.SystemBackdropChanged    += _settingsWindow_SystemBackdropChanged;
    }

    private void RestoreMainWindow()
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
    /// Displays the main window, setting up necessary configurations like DPI scaling, native
    /// app window settings, and the title bar.
    /// </summary>
    public void ShowMainWindow()
    {
        if (_mainWindow?.HasClosed is false)
        {
            RestoreMainWindow();

            return;
        }

        CreateMainWindow();
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