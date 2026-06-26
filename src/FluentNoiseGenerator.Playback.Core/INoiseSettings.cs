namespace FluentNoiseGenerator.Core;

/// <summary>
/// Defines adjustable parameters for a noise generator.
/// </summary>
public interface INoiseSettings
{
    /// <summary>
    /// The overall intensity of the noise.
    /// </summary>
    double Amplitude { get; }

    /// <summary>
    /// Determines how randomly the noise evolves with each noise.
    /// </summary>
    double Randomness { get; }

    /// <summary>
    /// The level of smoothing applied to the noise.
    /// </summary>
    double Smoothing { get; }
}