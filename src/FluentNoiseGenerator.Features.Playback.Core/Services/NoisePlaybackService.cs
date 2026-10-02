using System.Collections.Generic;

namespace FluentNoiseGenerator.Features.Playback.Core.Services;

/// <summary>
/// Represents a service for managing noise playback.
/// </summary>
public sealed class NoisePlaybackService
{
    /// <summary>
    /// Gets a read-only list with audio sample rates.
    /// </summary>
    public IReadOnlyList<int> AudioSampleRates { get; } = [48000, 44100];
}