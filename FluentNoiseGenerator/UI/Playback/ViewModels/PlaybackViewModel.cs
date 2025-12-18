using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.UI.Xaml;
using System;
using System.Collections.ObjectModel;

namespace FluentNoiseGenerator.UI.Playback.ViewModels;

/// <summary>
/// Represents the view model for the playback window.
/// </summary>
public sealed partial class PlaybackViewModel : ObservableObject, IDisposable
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
    public partial uint CurrentVolume { get; set; }

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    [ObservableProperty]
    public partial bool IsPlaying { get; private set; }
    #endregion

    #region Properties
    /// <summary>
    /// Gets a read-only observable collection of noise presets.
    /// </summary>
    public ReadOnlyObservableCollection<object> NoisePresets { get; }

    /// <summary>
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public PlaybackWindowResources StringResources { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackViewModel"/> class using the
    /// specified dependencies.
    /// </summary>
    /// <param name="appResources">
    /// An <see cref="AppResources"/> instance with localized app resources.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public PlaybackViewModel(AppResources appResources, IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(appResources);
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        NoisePresets = new ReadOnlyObservableCollection<object>([]);

        StringResources = new();

        RegisterMessageHandlers();
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
    private void RegisterMessageHandlers()
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

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
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