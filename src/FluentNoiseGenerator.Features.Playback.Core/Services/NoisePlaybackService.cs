using CommunityToolkit.Mvvm.Messaging;

namespace FluentNoiseGenerator.Features.Playback.Core.Services;

/// <summary>
/// Default implementation for the <see cref="INoisePlaybackService"/> service.
/// </summary>
public sealed class NoisePlaybackService : INoisePlaybackService
{
    #region Instance fields
    private IEnumerable<object> _audioSampleRates;

    private readonly IMessenger _messenger;
    #endregion

    #region Instance properties
    /// <inheritdoc cref="INoisePlaybackService.AudioSampleRates"/>
    public IEnumerable<object> AudioSampleRates => _audioSampleRates;
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="NoisePlaybackService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any parameter is <see langword="null"/>.
    /// </exception>
    public NoisePlaybackService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _audioSampleRates = [48000, 44100];

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers() { }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}