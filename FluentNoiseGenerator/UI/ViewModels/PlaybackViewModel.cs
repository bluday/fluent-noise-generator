using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.ViewModels;

/// <summary>
/// Represents the view model for the playback window.
/// </summary>
public sealed partial class PlaybackViewModel : ObservableObject
{
    #region Fields
    private readonly IMessenger _messenger;
    #endregion

    #region Observable properties
    /// <summary>
    /// Gets the current <see cref="ElementTheme"/> for the application.
    /// </summary>
    [ObservableProperty]
    public partial ElementTheme CurrentTheme { get; private set; }

    /// <summary>
    /// Gets or sets the current non-negative volume value.
    /// </summary>
    [ObservableProperty]
    public partial uint CurrentVolume { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    [ObservableProperty]
    public partial bool IsPlaying { get; private set; }

    /// <summary>
    /// Gets a read-only collection of noise presets.
    /// </summary>
    [ObservableProperty]
    public partial IReadOnlyCollection<object> NoisePresets { get; private set; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public PlaybackWindowStringResources StringResources { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackViewModel"/> class using the
    /// specified string resource collection and event messenger.
    /// </summary>
    /// <param name="noisePlaybackService">
    /// The noise playback service for managing playback within the app.
    /// </param>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources
    /// specifically for this view.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public PlaybackViewModel(
        NoisePlaybackService          noisePlaybackService,
        PlaybackWindowStringResources stringResources,
        IMessenger                    messenger)
    {
        ArgumentNullException.ThrowIfNull(noisePlaybackService);
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        NoisePresets = [];

        StringResources = stringResources;

        SubscribeToMessages();
    }
    #endregion

    #region Commands
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
        throw new NotImplementedException();
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

    #region Methods
    private void SubscribeToMessages()
    {
        _messenger.Register<ApplicationThemeUpdatedMessage>(
            this,
            HandleApplicationThemeUpdatedMessage
        );

        _messenger.Register<LocalizedResourceProviderUpdatedMessage>(
            this,
            HandleLocalizedResourceProviderUpdatedMessage
        );
    }
    #endregion

    #region Message handlers
    private void HandleApplicationThemeUpdatedMessage(
        object                         recipient,
        ApplicationThemeUpdatedMessage message)
    {
        CurrentTheme = message.Value;
    }

    private void HandleLocalizedResourceProviderUpdatedMessage(
        object                                  recipient,
        LocalizedResourceProviderUpdatedMessage message)
    {
        OnPropertyChanged(nameof(StringResources));
    }
    #endregion
}