using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator.Common.Services;
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
    private readonly Container _container;

    private readonly WindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _container = new Container();

        IServiceProvider rootServiceProvider = _container.RootServiceProvider;

        _windowService = rootServiceProvider.GetRequiredService<WindowService>();

        Ioc.Default.ConfigureServices(rootServiceProvider);

        InitializeComponent();
    }
    #endregion

    #region Instance methods
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