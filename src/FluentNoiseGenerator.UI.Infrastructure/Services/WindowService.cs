using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Infrastructure.Messages;
using System;

namespace FluentNoiseGenerator.UI.Infrastructure.Services;

/// <summary>
/// Default implementation for the <see cref="IWindowService"/> service.
/// </summary>
public sealed partial class WindowService : IWindowService, IDisposable
{
    #region Fields
    private readonly IMessenger _messenger;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the
    /// application, typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if any of the parameters are <c>null</c>.
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
        _messenger.Register<OpenSettingsWindowMessage>(this, HandleOpenSettingsWindowMessage);
        _messenger.Register<ClosePlaybackWindowMessage>(this, HandleClosePlaybackWindowMessage);
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }

    /// <inheritdoc cref="IWindowService.ShowPlaybackWindow()"/>
    public void ShowPlaybackWindow()
    {
        // TODO: Create or restore the playback window.
    }

    /// <inheritdoc cref="IWindowService.ShowSettingsWindow()"/>
    public void ShowSettingsWindow()
    {
        // TODO: Create or restore the settings window.
    }
    #endregion

    #region Message handlers
    private void HandleOpenSettingsWindowMessage(object recipient, OpenSettingsWindowMessage message)
    {
        ShowSettingsWindow();
    }

    private void HandleClosePlaybackWindowMessage(object recipient, ClosePlaybackWindowMessage message)
    {
        // TODO: Close the playback window.
    }
    #endregion
}