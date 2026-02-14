using FluentNoiseGenerator.UI.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Fields
    private readonly Container _container = new();

    private IWindowService _windowService = null!;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        InitializeCoreServices();
        InitializeComponent();
    }
    #endregion

    #region Instance methods
    private void InitializeCoreServices()
    {
        IServiceProvider rootServiceProvider = _container.RootServiceProvider;

        // TODO: Resolve critical services in order to run the application.

        _windowService = rootServiceProvider.GetRequiredService<IWindowService>();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _windowService.ShowPlaybackWindow();
    }
    #endregion
}