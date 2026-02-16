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

    private readonly IMessenger _messenger;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    public WindowService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

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

        _playbackWindow = PlaybackWindowFactory.Create();

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

        _settingsWindow = SettingsWindowFactory.Create();
        
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