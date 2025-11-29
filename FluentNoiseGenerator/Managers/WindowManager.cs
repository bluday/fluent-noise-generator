using FluentNoiseGenerator.Windows;
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
    #endregion

    #region Delegates
    /// <summary>
    /// The method that will handle the application theme change request.
    /// </summary>
    /// <param name="theme">
    /// The new <see cref="ElementTheme"/> to apply.
    /// </param>
    public delegate void ApplicationThemeChangeRequestHandler(ElementTheme theme);

    /// <summary>
    /// The method that will handle the system backdrop change request.
    /// </summary>
    /// <param name="systemBackdrop">
    /// The new <see cref="SystemBackdrop"/> to apply.
    /// </param>
    public delegate void SystemBackdropChangeRequestHandler(SystemBackdrop? systemBackdrop);
    #endregion

    #region Events
    /// <summary>
    /// Fires when the <see cref="SettingsWindow.ApplicationThemeChanged"/> event gets
    /// fired.
    /// </summary>
    public event ApplicationThemeChangeRequestHandler ApplicationThemeChangeRequest;

    /// <summary>
    /// Fires when the <see cref="SettingsWindow.SystemBackdropChanged"/> event gets
    /// fired.
    /// </summary>
    public event SystemBackdropChangeRequestHandler SystemBackdropChangeRequest;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowManager"/> class.
    /// </summary>
    public WindowManager()
    {
        ApplicationThemeChangeRequest = delegate { };
        SystemBackdropChangeRequest   = delegate { };
    }
    #endregion

    #region Event handlers
    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        ApplicationThemeChangeRequest?.Invoke(e);
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
        SystemBackdropChangeRequest?.Invoke(e);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Displays the main window, setting up necessary configurations like DPI scaling, native
    /// app window settings, and the title bar.
    /// </summary>
    public void ShowMainWindow()
    {
        if (_mainWindow?.HasClosed is false)
        {
            _mainWindow.Restore();
            _mainWindow.Focus();

            return;
        }

        _mainWindow = new MainWindow
        {
            SettingsWindowFactory = ShowSettingsWindow
        };

        _mainWindow.RefreshLocalizedContent();
        _mainWindow.RetrieveAndUpdateDpiScale();
        _mainWindow.ConfigureAppWindow();
        _mainWindow.ConfigureTitleBar();
        _mainWindow.Activate();
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
            _settingsWindow.Restore();
            _settingsWindow.Focus();

            return;
        }

        if (_settingsWindow is not null)
        {
            _settingsWindow.ApplicationThemeChanged -= _settingsWindow_ApplicationThemeChanged;
            _settingsWindow.SystemBackdropChanged   -= _settingsWindow_SystemBackdropChanged;
        }

        _settingsWindow = new SettingsWindow();

        _settingsWindow.ApplicationThemeChanged += _settingsWindow_ApplicationThemeChanged;
        _settingsWindow.SystemBackdropChanged   += _settingsWindow_SystemBackdropChanged;

        _settingsWindow.ConfigureAppWindow();
        _settingsWindow.ConfigureTitleBar();
        _settingsWindow.RefreshLocalizedContent();
        _settingsWindow.Activate();
    }

    /// <summary>
    /// Applies the specified <see cref="ElementTheme"/> to all active windows.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="ElementTheme"/> to apply on each window.
    /// </param>
    public void BulkApplyRequestedTheme(ElementTheme value)
    {
        _mainWindow?.UpdateRequestedTheme(value);
        _settingsWindow?.UpdateRequestedTheme(value);
    }

    /// <summary>
    /// Applies the specified <see cref="SystemBackdrop"/> to all active windows.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="SystemBackdrop"/> to apply on each window.
    /// </param>
    public void BulkApplySystemBackdrop(SystemBackdrop? value)
    {
        _mainWindow?.UpdateSystemBackdrop(value);
        _settingsWindow?.UpdateSystemBackdrop(value);
    }
    #endregion
}