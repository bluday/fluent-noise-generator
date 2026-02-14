using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.UI.Common.Resources;
using FluentNoiseGenerator.UI.Playback.ViewModels;
using System;

namespace FluentNoiseGenerator.UI.Playback.Windows;

/// <summary>
/// Represents a factory for creating <see cref="PlaybackWindow"/> instances.
/// </summary>
public sealed class PlaybackWindowFactory
{
    #region Fields
    private readonly IMessenger _messenger;

    private readonly AppResources _appResources;
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
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public PlaybackWindowFactory(AppResources appResources, IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(appResources);
        ArgumentNullException.ThrowIfNull(messenger);

        _appResources = appResources;
        _messenger    = messenger;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Creates a new <see cref="PlaybackWindow"/> instance with its required dependencies.
    /// </summary>
    /// <returns>
    /// The created window instance.
    /// </returns>
    public PlaybackWindow Create()
    {
        return new()
        {
            ViewModel = new PlaybackViewModel(_appResources, _messenger)
        };
    }
    #endregion
}