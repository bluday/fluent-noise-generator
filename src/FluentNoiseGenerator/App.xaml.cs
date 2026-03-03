using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator.Configuration;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.Windows;
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
    private readonly ServiceProvider _rootServiceProvider = CreateContainer();
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
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
        _rootServiceProvider.GetRequiredService<PlaybackWindow>().Activate();
        _rootServiceProvider.GetRequiredService<SettingsWindow>().Activate();
    }
    #endregion

    #region Static methods
    private static ServiceProvider CreateContainer()
    {
        ServiceCollection services = [];

        ServiceConfiguration.Configure(services);

        ServiceProvider rootServiceProvider = services.BuildServiceProvider();

        Ioc.Default.ConfigureServices(rootServiceProvider);

        return rootServiceProvider;
    }
    #endregion
}