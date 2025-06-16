namespace BluDay.FluentNoiseRemover.Extensions;

/// <summary>
/// A utility with method extensions for the <see cref="Window"/> type.
/// </summary>
public static class WindowExtensions
{
    /// <summary>
    /// Sets the element theme of the window.
    /// </summary>
    /// <param name="window">
    /// The target window instance.
    /// </param>
    /// <param name="value">
    /// The element theme.
    /// </param>
    public static void SetRequestedTheme(this Window window, ElementTheme value)
    {
        if (window?.Content is FrameworkElement element)
        {
            element.RequestedTheme = value;
        }
    }

    /// <summary>
    /// Sets the system backdrop for the window.
    /// </summary>
    /// <param name="window">
    /// The target window instance.
    /// </param>
    /// <param name="value">
    /// The system backdrop.
    /// </param>
    public static void SetSystemBackdrop(this Window window, SystemBackdrop? value)
    {
        window.SystemBackdrop = value;
    }
}