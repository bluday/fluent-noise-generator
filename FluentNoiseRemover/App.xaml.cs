using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.IO;

namespace FluentNoiseRemover;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    private MainWindow? _mainWindow;

    private SettingsWindow? _settingsWindow;

    private ElementTheme _elementTheme;

    private SystemBackdrop? _systemBackdrop;

    /// <summary>
    /// Gets the absolute path for the 64x64 application icon.
    /// </summary>
    public static readonly string IconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Icon-64.ico");

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        // TODO: Configure and initialize the core.

        InitializeComponent();
    }

    private void ShowSettingsWindow()
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

        _settingsWindow.Activate();
    }

    private void UpdateSettingsWindowTitleBarColors()
    {
        if (_settingsWindow?.AppWindow.TitleBar is not AppWindowTitleBar settingsWindowTitleBar)
        {
            return;
        }

        settingsWindowTitleBar.ButtonBackgroundColor         = Colors.Transparent;
        settingsWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        settingsWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (_elementTheme is ElementTheme.Light)
        {
            settingsWindowTitleBar.ButtonHoverBackgroundColor   = Colors.DarkGray;
            settingsWindowTitleBar.ButtonPressedBackgroundColor = Colors.LightGray;

            settingsWindowTitleBar.ButtonForegroundColor        = Colors.Black;
            settingsWindowTitleBar.ButtonHoverForegroundColor   = Colors.Black;
            settingsWindowTitleBar.ButtonPressedForegroundColor = Colors.Black;

            return;
        }

        settingsWindowTitleBar.ButtonHoverBackgroundColor   = Colors.LightGray;
        settingsWindowTitleBar.ButtonPressedBackgroundColor = Colors.Gray;

        settingsWindowTitleBar.ButtonForegroundColor        = Colors.White;
        settingsWindowTitleBar.ButtonHoverForegroundColor   = Colors.White;
        settingsWindowTitleBar.ButtonPressedForegroundColor = Colors.White;
    }

    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        _elementTheme = e;

        if (_mainWindow?.HasClosed is false)
        {
            (_mainWindow.Content as FrameworkElement)?.RequestedTheme = e;
        }

        if (_settingsWindow?.HasClosed is false)
        {
            (_settingsWindow.Content as FrameworkElement)?.RequestedTheme = e;
        }

        UpdateSettingsWindowTitleBarColors();
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
        _systemBackdrop = e;

        if (_mainWindow?.HasClosed is false)
        {
            _mainWindow.SystemBackdrop = e;
        }

        if (_settingsWindow?.HasClosed is false)
        {
            _settingsWindow.SystemBackdrop = e;
        }
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _mainWindow = new MainWindow(ShowSettingsWindow);

        _mainWindow.Activate();
    }
}