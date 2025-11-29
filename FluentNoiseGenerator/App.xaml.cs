using FluentNoiseGenerator.Managers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly ThemeManager _themeManager;

    private readonly WindowManager _windowManager;

    /// <summary>
    /// Absolute path for the 64x64 sized application icon as a string.
    /// </summary>
    public static readonly string IconPath = System.IO.Path.Combine(
        AppContext.BaseDirectory, "Assets", "Icon-64.ico"
    );
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _themeManager  = new ThemeManager();
        _windowManager = new WindowManager();

        _themeManager.CurrentSystemBackdropChanged += _themeManager_CurrentSystemBackdropChanged;
        _themeManager.CurrentThemeChanged          += _themeManager_CurrentThemeChanged;

        _windowManager.ApplicationThemeChangeRequest += _windowManager_ApplicationThemeChangeRequest;
        _windowManager.SystemBackdropChangeRequest   += _windowManager_SystemBackdropChangeRequest;

        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void _themeManager_CurrentSystemBackdropChanged(SystemBackdrop? systemBackdrop)
    {
        _windowManager.BulkApplySystemBackdrop(systemBackdrop);
    }

    private void _themeManager_CurrentThemeChanged(ElementTheme theme)
    {
        _windowManager.BulkApplyRequestedTheme(theme);
    }

    private void _windowManager_ApplicationThemeChangeRequest(ElementTheme theme)
    {
        _themeManager.CurrentTheme = theme;
    }

    private void _windowManager_SystemBackdropChangeRequest(SystemBackdrop? systemBackdrop)
    {
        _themeManager.CurrentSystemBackdrop = systemBackdrop;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _windowManager.ShowMainWindow();
    }
    #endregion
}