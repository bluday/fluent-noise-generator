namespace FluentNoiseGenerator.Core.Services;

/// <summary>
/// Service for managing the playback of noise.
/// </summary>
public interface INoisePlaybackService
{
    /// <summary>
    /// Gets an enumerable with audio sample rates.
    /// </summary>
    IEnumerable<int> AudioSampleRates { get; }
}