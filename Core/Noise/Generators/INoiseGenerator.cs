namespace BluDay.FluentNoiseRemover.Core.Noise.Generators;

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
    /// Generates a 16-bit PCM audio sample for playback.
    /// </summary>
    /// <returns>
    /// A 16-bit signed short representing the generated noise sample.
    /// </returns>
    short GenerateSample();
}