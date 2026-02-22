using CommunityToolkit.Mvvm.DependencyInjection;
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
    private readonly Container _container;

    private readonly IWindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _container = new Container();

        IKeyedServiceProvider rootServiceProvider = _container.RootServiceProvider;

        // TODO: Resolve critical services in order to run the application.

        _windowService = rootServiceProvider.GetRequiredService<IWindowService>();

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