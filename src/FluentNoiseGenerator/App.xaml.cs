using FluentNoiseGenerator.UI.Playback.Windows;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// Interaction logic for App.xaml.
/// </summary>
public partial class App : Application
{
    #region Instance fields
    private readonly Func<PlaybackWindow> _playbackWindowFactory;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class using
    /// the specified dependencies.
    /// </summary>
    /// <param name="playbackWindowFactory">
    /// A factory for creating <see cref="PlaybackWindow"/> instances with.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if any of the parameters are <c>null</c>.
    /// </exception>
    public App(Func<PlaybackWindow> playbackWindowFactory)
    {
        ArgumentNullException.ThrowIfNull(playbackWindowFactory);

        _playbackWindowFactory = playbackWindowFactory;

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
        _playbackWindowFactory().Activate();
    }
    #endregion
}