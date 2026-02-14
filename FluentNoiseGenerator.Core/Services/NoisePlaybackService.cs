using CommunityToolkit.Mvvm.Messaging;

namespace FluentNoiseGenerator.Core.Services;

/// <summary>
/// Default implementation for the <see cref="INoisePlaybackService"/> service.
/// </summary>
public sealed class NoisePlaybackService : INoisePlaybackService
{
    #region Fields
    private IEnumerable<int> _audioSampleRates;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <inheritdoc cref="INoisePlaybackService.AudioSampleRates"/>
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

        RegisterMessageHandlers();
    }
    #endregion

    #region Methods
    private void RegisterMessageHandlers() { }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}