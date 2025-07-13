namespace FluentNoiseGenerator.Core;

/// <summary>
/// Represents a noise generator that generates 16-bit PCM (Pulse Code Modulation) audio samples.
/// </summary>
/// <remarks>
/// Implementations define specific noise types such as:
/// <list type="bullet">
///     <item>
///         <b>Blue noise</b>: A variation of Brownian noise with a balanced spectrum.
///     </item>
///     <item>
///         <b>Brownian noise</b>: A random-walk noise with deep and smooth characteristics.
///     </item>
///     <item>
///         <b>White noise</b>: Completely random noise with equal energy across frequencies.
///     </item>
/// </list>
/// </remarks>
public interface INoiseGenerator
{
    /// <summary>
    /// Gets the configuration parameters that define the behavior of the noise generator.
    /// </summary>
    /// <remarks>
    /// Controls things like amplitude, randomness, and smoothing.
    /// </remarks>
    INoiseSettings Settings { get; }

    /// <summary>
    /// Generates a 16-bit PCM audio sample for playback.
    /// </summary>
    /// <returns>
    /// A <see cref="short"/> representing the generated noise sample.
    /// </returns>
    short GenerateSample();
}