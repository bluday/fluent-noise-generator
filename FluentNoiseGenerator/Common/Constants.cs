namespace FluentNoiseGenerator.Common;

/// <summary>
/// A utility for accessing application-specific constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The absolute icon path as a <see cref="string"/>.
    /// </summary>
    public static readonly string IconPath = System.IO.Path.Combine(
        System.AppContext.BaseDirectory,
        "Assets/Icon-64.ico"
    );
}