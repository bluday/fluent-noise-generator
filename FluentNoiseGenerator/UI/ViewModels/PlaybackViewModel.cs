using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.StringResources;
using FluentNoiseGenerator.Messages;
using System;
using System.Collections.Generic;
using System.IO;

namespace FluentNoiseGenerator.UI.ViewModels;

/// <summary>
/// Represents the view model for the playback window.
/// </summary>
public sealed partial class PlaybackViewModel : ObservableObject
{
    #region Fields
    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the current non-negative volume value.
    /// </summary>
    public uint CurrentVolume { get; }

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying { get; private set; }

    /// <summary>
    /// Gets a read-only collection of noise presets.
    /// </summary>
    public IReadOnlyCollection<object> NoisePresets { get; }

    /// <summary>
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public PlaybackWindowStringResources StringResources { get; }

    /// <summary>
    /// Gets the path for the title bar icon.
    /// </summary>
    public string TitleBarIconPath { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackViewModel"/> class using the
    /// specified string resource collection and event messenger.
    /// </summary>
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
    public PlaybackViewModel(PlaybackWindowStringResources stringResources, IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        NoisePresets = [];

        StringResources = stringResources;

        TitleBarIconPath = Path.Combine(AppContext.BaseDirectory, "Assets/Icon-64.ico");
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
}