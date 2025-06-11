namespace BluDay.FluentNoiseRemover.Core.Noise.Generators;

/// <summary>
/// Represents a noise generator that generates 16-bit PCM (Pulse Code Modulation) audio samples.
/// </summary>
/// <remarks>
/// Implementations define specific noise types such as:
/// <list type="bullet">
///     <item>Blue noise (which is a variation of Brownian noise)</item>
///     <item>Brownian noise</item>
///     <item>White noise</item>
/// </list>
/// </remarks>
public interface INoiseGenerator
{
    /// <summary>
    /// Generates a simple audio sample for playback.
    /// </summary>
    /// <returns>
    /// A 16-bit signed short representing the generated noise sample.
    /// </returns>
    short GenerateSample();
}