using FluentNoiseGenerator.Client.Configuration;
using FluentNoiseGenerator.Features.Playback.UI;
using FluentNoiseGenerator.Features.Settings.UI;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Client;

/// <summary>
/// Provides application-specific behavior to supplement the base class.
/// </summary>
public sealed partial class App : Application
{
    #region Instance fields
    private readonly ServiceProvider _rootServiceProvider = ServiceConfiguration.BuildServiceProvider();
    #endregion

    #region Instance constructor
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
    /// <param name="e">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        PlaybackWindow playbackWindow = new(
            _rootServiceProvider.GetRequiredService<PlaybackWindowViewModel>()
        );

        playbackWindow.Activate();

        SettingsWindow settingsWindow = new(
            _rootServiceProvider.GetRequiredService<SettingsWindowViewModel>()
        );

        settingsWindow.Activate();
    }
    #endregion
}