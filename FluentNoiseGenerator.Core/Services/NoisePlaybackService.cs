using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;

namespace FluentNoiseGenerator.Core.Services;

/// <summary>
/// Represents a class for managing the playback of a noise preset within the application.
/// </summary>
public sealed class NoisePlaybackService
{
    #region Fields
    private IEnumerable<int> _audioSampleRates;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets an enumerable with audio sample rates.
    /// </summary>
    public IEnumerable<int> AudioSampleRates => _audioSampleRates;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="NoisePlaybackService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public NoisePlaybackService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _audioSampleRates = [48000, 44100];

        _messenger = messenger;
    }
    #endregion
}