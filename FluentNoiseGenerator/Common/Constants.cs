namespace FluentNoiseGenerator.Common;

/// <summary>
/// Provides utility constants used throughout the application.
/// </summary>
internal static class Constants
{
    /// <summary>
    /// The absolute icon path to the application's icon.
    /// </summary>
    public static readonly string IconPath = System.IO.Path.Combine(
        System.AppContext.BaseDirectory,
        "Assets/Icon-64.ico"
    );
}