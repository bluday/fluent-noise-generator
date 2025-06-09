namespace BluDay.FluentNoiseRemover.Common;

/// <summary>
/// Defines the available system backdrop materials.
/// </summary>
public enum WindowsSystemBackdrop
{
    /// <summary>
    /// Uses the standard Mica material.
    /// </summary>
    Mica,

    /// <summary>
    /// Uses an alternative variant of the Mica material.
    /// </summary>
    MicaAlternative,

    /// <summary>
    /// Uses the Acrylic material for a translucent effect.
    /// </summary>
    Acrylic,

    /// <summary>
    /// No system backdrop is applied.
    /// </summary>
    None
}