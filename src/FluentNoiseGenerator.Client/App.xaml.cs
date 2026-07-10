using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Features.Playback.UI.Windows;
using FluentNoiseGenerator.Features.Settings.UI.Windows;
using FluentNoiseGenerator.Foundation.Messages;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.Client;

/// <summary>
/// Provides application-specific behavior to supplement the base class.
/// </summary>
public sealed partial class App : Application
{
    #region Instance fields
    private PlaybackWindow? _playbackWindow;

    private SettingsWindow? _settingsWindow;

    private readonly IMessenger _messenger;

    private readonly Func<PlaybackWindow> _playbackWindowFactory;

    private readonly Func<SettingsWindow> _settingsWindowFactory;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class using
    /// the specified dependencies.
    /// </summary>
    /// <param name="playbackWindowFactory">
    /// A factory for creating the playback window.
    /// </param>
    /// <param name="settingsWindowFactory">
    /// A factory for creating the settings window.
    /// </param>
    /// <param name="messenger">
    /// The messenger for sending messages within the app.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any parameter is <c>null</c>.
    /// </exception>
    public App(
        Func<PlaybackWindow> playbackWindowFactory,
        Func<SettingsWindow> settingsWindowFactory,
        IMessenger           messenger)
    {
        ArgumentNullException.ThrowIfNull(playbackWindowFactory);
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;
        
        _playbackWindowFactory = playbackWindowFactory;

        _settingsWindowFactory = settingsWindowFactory;

        RegisterMessageHandlers();

        InitializeComponent();
    }
    #endregion

    #region Message handlers
    private void HandleClosePlaybackWindowMessage(object sender, ClosePlaybackWindowMessage message)
    {
        if (_playbackWindow is null) return;

        _playbackWindow.Close();

        _playbackWindow = null;
    }

    private void HandleOpenPlaybackWindowMessage(object sender, OpenPlaybackWindowMessage message)
    {
        _playbackWindow ??= CreateWindow(_playbackWindowFactory);
    }

    private void HandleOpenSettingsWindowMessage(object sender, OpenSettingsWindowMessage message)
    {
        _settingsWindow ??= CreateWindow(_settingsWindowFactory);
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers()
    {
        Subscribe<ClosePlaybackWindowMessage>(HandleClosePlaybackWindowMessage);
        Subscribe<OpenPlaybackWindowMessage>(HandleOpenPlaybackWindowMessage);
        Subscribe<OpenSettingsWindowMessage>(HandleOpenSettingsWindowMessage);
    }

    private void Subscribe<TMessage>(MessageHandler<object, TMessage> handler)
        where TMessage : class
    {
        _messenger.Register(this, handler);
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="e">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        _messenger.Send(new OpenPlaybackWindowMessage());
        _messenger.Send(new OpenSettingsWindowMessage());
    }
    #endregion

    #region Static methods
    private static TWindow CreateWindow<TWindow>(Func<TWindow> factory)
        where TWindow : Window
    {
        TWindow window = factory();

        window.Activate();

        return window;
    }
    #endregion
}