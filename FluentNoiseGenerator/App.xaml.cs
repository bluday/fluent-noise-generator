using FluentNoiseGenerator.Services;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly ThemeService _themeService;

    private readonly WindowService _windowService;

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
        _themeService  = new ThemeService();
        _windowService = new WindowService(_themeService);

        InitializeComponent();
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
        _windowService.ShowMainWindow();
    }
    #endregion
}