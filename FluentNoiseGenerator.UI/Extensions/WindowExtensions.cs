using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.UI.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Window"/> instances.
/// </summary>
public static class WindowExtensions
{
    /// <summary>
    /// Focuses on the window programmatically.
    /// </summary>
    /// <param name="source">
    /// The targeted <see cref="Window"/> instance to focus.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void Focus(this Window source)
    {
        ArgumentNullException.ThrowIfNull(source);

        source.Content?.Focus(FocusState.Programmatic);
    }

    /// <summary>
    /// Focuses on the window using the specified <see cref="FocusState"/> value.
    /// </summary>
    /// <param name="source">
    /// The targeted <see cref="Window"/> instance to focus.
    /// </param>
    /// <param name="focusState">
    /// Describes how the content of the window obtained focus.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void Focus(this Window source, FocusState focusState)
    {
        ArgumentNullException.ThrowIfNull(source);

        source.Content?.Focus(focusState);
    }

    /// <summary>
    /// Restores the window on the desktop.
    /// </summary>
    /// <param name="source">
    /// The targeted <see cref="Window"/> instance to restore.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void Restore(this Window source)
    {
        ArgumentNullException.ThrowIfNull(source);

        (source.AppWindow.Presenter as OverlappedPresenter)?.Restore();
    }
}