using FluentNoiseRemover.Windows;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.IO;

namespace FluentNoiseRemover;

/// <summary>
/// Interaction logic for App.xaml and the entrypoint for the application.
/// </summary>
public partial class App : Application
{
    private MainWindow? _mainWindow;

    private SettingsWindow? _settingsWindow;

    private ElementTheme _elementTheme;

    /// <summary>
    /// Gets the absolute path for the 64x64 application icon.
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