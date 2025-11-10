using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.IO;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    private MainWindow? _mainWindow;

    private SettingsWindow? _settingsWindow;

    private ElementTheme _elementTheme;

    /// <summary>
    /// Absolute path for the 64x64 sized application icon as a string.
    /// </summary>
    public static readonly string IconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Icon-64.ico");

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
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

        _settingsWindow.ConfigureAppWindow();
        _settingsWindow.ConfigureTitleBar();
        _settingsWindow.RefreshLocalizedContent();
        _settingsWindow.Activate();
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

        if (_settingsWindow?.AppWindow.TitleBar is not AppWindowTitleBar titleBar)
        {
            return;
        }

        titleBar.ButtonBackgroundColor         = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (_elementTheme is ElementTheme.Light)
        {
            titleBar.ButtonHoverBackgroundColor   = Colors.DarkGray;
            titleBar.ButtonPressedBackgroundColor = Colors.LightGray;

            titleBar.ButtonForegroundColor        = Colors.Black;
            titleBar.ButtonHoverForegroundColor   = Colors.Black;
            titleBar.ButtonPressedForegroundColor = Colors.Black;

            return;
        }

        titleBar.ButtonHoverBackgroundColor   = Colors.LightGray;
        titleBar.ButtonPressedBackgroundColor = Colors.Gray;

        titleBar.ButtonForegroundColor        = Colors.White;
        titleBar.ButtonHoverForegroundColor   = Colors.White;
        titleBar.ButtonPressedForegroundColor = Colors.White;
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
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
}