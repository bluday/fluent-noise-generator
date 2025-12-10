using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.StringResources;
using FluentNoiseGenerator.UI.ViewModels;
using FluentNoiseGenerator.UI.Windows;
using System;

namespace FluentNoiseGenerator.Factories;

/// <summary>
/// Represents a factory for creating <see cref="PlaybackWindow"/> instances.
/// </summary>
internal sealed class PlaybackWindowFactory
{
    #region Fields
    private readonly IMessenger _messenger;

    private readonly AppStringResources _stringResources;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackViewModel"/> class using the
    /// specified string resource collection and event messenger.
    /// </summary>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    internal PlaybackWindowFactory(AppStringResources stringResources, IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger       = messenger;
        _stringResources = stringResources;
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
            ViewModel = new PlaybackViewModel(
                _stringResources.PlaybackWindow,
                _messenger
            )
        };
    }
    #endregion
}