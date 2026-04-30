using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.UI.Playback.ViewModels;

/// <summary>
/// Represents the view model for the playback window.
/// </summary>
public sealed partial class PlaybackViewModel : ObservableObject, IDisposable
{
    #region Instance fields
    private readonly IMessenger _messenger;
    #endregion

    #region Observable properties
    /// <summary>
    /// Gets the current <see cref="ElementTheme"/> for the application.
    /// </summary>
    [ObservableProperty]
    public partial object? CurrentTheme { get; private set; }

    /// <summary>
    /// Gets or sets the current non-negative volume value.
    /// </summary>
    [ObservableProperty]
    public partial uint CurrentVolume { get; set; }

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    [ObservableProperty]
    public partial bool IsPlaying { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackViewModel"/> class using
    /// the specified dependencies.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public PlaybackViewModel(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Relay commands
    /// <summary>
    /// Invokes when the close button on the top bar gets clicked.
    /// </summary>
    [RelayCommand]
    private void CloseWindow()
    {
        _messenger.Send(new ClosePlaybackWindowMessage());
    }

    /// <summary>
    /// Invokes when the toggle playback button on the control panel gets clicked.
    /// </summary>
    [RelayCommand]
    private void TogglePlayback()
    {
        IsPlaying = !IsPlaying;
    }

    /// <summary>
    /// Invokes when the settings button on the top bar gets clicked.
    /// </summary>
    [RelayCommand]
    private void ShowSettings()
    {
        _messenger.Send(new OpenSettingsWindowMessage());
    }
    #endregion

    #region Message handlers
    private void HandleApplicationThemeUpdatedMessage(
        object recipient,
        ApplicationThemeUpdatedMessage message)
    {
        CurrentTheme = message.Value;
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers()
    {
        _messenger.Register<ApplicationThemeUpdatedMessage>(
            this,
            HandleApplicationThemeUpdatedMessage
        );
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}