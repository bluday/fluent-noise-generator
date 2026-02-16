using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Service for managing the theme of the application.
/// </summary>
public interface IThemeService
{
    /// <summary>
    /// Gets or sets the current theme of the application.
    /// </summary>
    object CurrentTheme { get; set; }

    /// <summary>
    /// Gets an enumerable of themes.
    /// </summary>
    IEnumerable<object> Themes { get; }
}