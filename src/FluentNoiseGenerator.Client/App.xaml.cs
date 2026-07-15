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
    /// Thrown when any parameter is <see langword="null"/>.
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
        CloseWindow(ref _playbackWindow);
    }

    private void HandleOpenPlaybackWindowMessage(object sender, OpenPlaybackWindowMessage message)
    {
        CreateWindow(ref _playbackWindow, _playbackWindowFactory);
    }

    private void HandleOpenSettingsWindowMessage(object sender, OpenSettingsWindowMessage message)
    {
        CreateWindow(ref _settingsWindow, _settingsWindowFactory);
    }

    private void HandleSettingsWindowClosedMessage(object sender, SettingsWindowClosedMessage message)
    {
        _settingsWindow = null;
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers()
    {
        Subscribe<ClosePlaybackWindowMessage>(HandleClosePlaybackWindowMessage);
        Subscribe<OpenPlaybackWindowMessage>(HandleOpenPlaybackWindowMessage);
        Subscribe<OpenSettingsWindowMessage>(HandleOpenSettingsWindowMessage);
        Subscribe<SettingsWindowClosedMessage>(HandleSettingsWindowClosedMessage);
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
        CreateWindow(ref _playbackWindow, _playbackWindowFactory);
        CreateWindow(ref _settingsWindow, _settingsWindowFactory);
    }
    #endregion

    #region Static methods
    private static void CloseWindow<TWindow>(ref TWindow? window)
        where TWindow : Window
    {
        window?.Close();

        window = null;
    }

    private static TWindow CreateWindow<TWindow>(ref TWindow? window, Func<TWindow> factory)
        where TWindow : Window
    {
        if (window is null)
        {
            window = factory();

            window.Activate();
        }

        return window;
    }
    #endregion
}