using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Messages;
using FluentNoiseGenerator.UI.Common.Extensions;
using FluentNoiseGenerator.UI.Playback.Windows;
using FluentNoiseGenerator.UI.Settings.Windows;
using System;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Default implementation for the <see cref="IWindowService"/> service.
/// </summary>
public sealed partial class WindowService : IWindowService, IDisposable
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
    public WindowService(
        PlaybackWindowFactory playbackWindowFactory,
        SettingsWindowFactory settingsWindowFactory,
        IMessenger            messenger)
    {
        ArgumentNullException.ThrowIfNull(playbackWindowFactory);
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);
        ArgumentNullException.ThrowIfNull(messenger);

        _playbackWindowFactory = playbackWindowFactory;
        _settingsWindowFactory = settingsWindowFactory;
        _messenger             = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Methods
    private void RegisterMessageHandlers()
    {
        _messenger.Register<OpenSettingsWindowMessage>(
            this,
            HandleOpenSettingsWindowMessage
        );

        _messenger.Register<ClosePlaybackWindowMessage>(
            this,
            HandleClosePlaybackWindowMessage
        );
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }

    /// <inheritdoc cref="IWindowService.ShowPlaybackWindow()"/>
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

    /// <inheritdoc cref="IWindowService.ShowSettingsWindow()"/>
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

    #region Message handlers
    private void HandleOpenSettingsWindowMessage(
        object                    recipient,
        OpenSettingsWindowMessage message)
    {
        ShowSettingsWindow();
    }

    private void HandleClosePlaybackWindowMessage(
        object                     recipient,
        ClosePlaybackWindowMessage message)
    {
        _playbackWindow?.Close();
    }
    #endregion
}