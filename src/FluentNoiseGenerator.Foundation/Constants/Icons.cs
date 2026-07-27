namespace FluentNoiseGenerator.Foundation.Constants;

/// <summary>
/// Provides a collection of icon paths.
/// </summary>
public static class Icons
{
    /// <summary>
    /// The absolute path to the 64x64 application icon.
    /// </summary>
    public static readonly string IconPath = Path.Combine(
        AppContext.BaseDirectory,
        "Assets",
        "logo_64x64.ico"
    );
}