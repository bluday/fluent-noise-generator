using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Infrastructure.Messages;
using FluentNoiseGenerator.UI.Factories;
using FluentNoiseGenerator.UI.Windows;
using System;
using FluentNoiseGenerator.Common.MethodExtensions;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for managing the main and settings windows. This class ensures that windows are
/// properly created, restored, and updated when necessary.
/// </summary>
internal sealed class WindowService
{
    #region Fields
    private PlaybackWindow? _playbackWindow;

    private SettingsWindow? _settingsWindow;

    private readonly SettingsWindowFactory _settingsWindowFactory;

    private readonly PlaybackWindowFactory _playbackWindowFactory;

    private readonly IMessenger _messenger;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowService"/> class.
    /// </summary>
    /// <param name="playbackWindowFactory">
    /// The <see cref="PlaybackWindowFactory"/> factory for creating new instances.
    /// </param>
    /// <param name="settingsWindowFactory">
    /// The <see cref="SettingsWindowFactory"/> factory for creating new instances.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    internal WindowService(
        PlaybackWindowFactory playbackWindowFactory,
        SettingsWindowFactory settingsWindowFactory,
        IMessenger            messenger)
    {
        ArgumentNullException.ThrowIfNull(playbackWindowFactory);
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);

        _playbackWindowFactory = playbackWindowFactory;
        _settingsWindowFactory = settingsWindowFactory;
        _messenger             = messenger;

        _messenger.Register<OpenSettingsWindowMessage>(this, HandleOpenSettingsWindowMessage);
        _messenger.Register<ClosePlaybackWindowMessage>(this, HandleClosePlaybackWindowMessage);
    }
    #endregion

    #region Methods
    private void HandleOpenSettingsWindowMessage(object recipient, OpenSettingsWindowMessage message)
    {
        ShowSettingsWindow();
    }

    private void HandleClosePlaybackWindowMessage(object recipient, ClosePlaybackWindowMessage message)
    {
        _playbackWindow?.Close();
    }

    /// <summary>
    /// Displays the playback window, setting up necessary configurations like DPI scaling, native
    /// app window settings, and the title bar.
    /// </summary>
    public void ShowPlaybackWindow()
    {
        if (_playbackWindow?.HasClosed is false)
        {
            _playbackWindow.Restore();

            return;
        }

        _playbackWindow = _playbackWindowFactory.Create();

        _playbackWindow.Activate();
    }

    /// <summary>
    /// Displays the settings window, either restoring it if it is open already or creating
    /// a new instance if it was closed. Listens for theme and backdrop changes.
    /// </summary>
    /// <remarks>
    /// Restores the window if it was previously closed. Otherwise, a new instance is created,
    /// and event handlers for theme and backdrop changes are registered.
    /// </remarks>
    public void ShowSettingsWindow()
    {
        if (_settingsWindow?.HasClosed is false)
        {
            _settingsWindow.Restore();

            return;
        }

        _settingsWindow = _settingsWindowFactory.Create();

        _settingsWindow.Activate();
    }
    #endregion
}