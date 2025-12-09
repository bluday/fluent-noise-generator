using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for managing the current theme of the application.
/// </summary>
public sealed class ThemeService
{
    #region Fields
    private SystemBackdrop? _currentSystemBackdrop;

    private ElementTheme _currentTheme;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the current system backdrop to be used for all windows.
    /// </summary>
    public SystemBackdrop? CurrentSystemBackdrop
    {
        get => _currentSystemBackdrop;
        set
        {
            _currentSystemBackdrop = value;

            CurrentSystemBackdropChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// Gets or sets the current theme of the application.
    /// </summary>
    public ElementTheme CurrentTheme
    {
        get => _currentTheme;
        set
        {
            _currentTheme = value;

            CurrentThemeChanged?.Invoke(value);
        }
    }
    #endregion

    #region Delegates
    /// <summary>
    /// Method signature for the <see cref="CurrentSystemBackdropChanged"/> event.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="SystemBackdrop"/> that is being applied.
    /// </param>
    public delegate void CurrentSystemBackdropChangedHandler(SystemBackdrop? value);

    /// <summary>
    /// Method signature for the <see cref="CurrentThemeChanged"/> event.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="ElementTheme"/> that is being applied.
    /// </param>
    public delegate void CurrentThemeChangedHandler(ElementTheme value);
    #endregion

    #region Events
    /// <summary>
    /// Fires when <see cref="CurrentTheme"/> gets updated.
    /// </summary>
    public event CurrentThemeChangedHandler CurrentThemeChanged = delegate { };

    /// <summary>
    /// Fires when <see cref="CurrentSystemBackdrop"/> gets updated.
    /// </summary>
    public event CurrentSystemBackdropChangedHandler CurrentSystemBackdropChanged = delegate { };
    #endregion
}