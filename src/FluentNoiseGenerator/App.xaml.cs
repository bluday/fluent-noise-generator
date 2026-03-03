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
    private readonly Container _container;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _container = new Container(ServiceConfiguration.Configure);

        Ioc.Default.ConfigureServices(_container.RootServiceProvider);

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
        IKeyedServiceProvider serviceProvider = _container.RootServiceProvider;

        serviceProvider.GetRequiredService<PlaybackWindow>().Activate();
        serviceProvider.GetRequiredService<SettingsWindow>().Activate();
    }
    #endregion
}