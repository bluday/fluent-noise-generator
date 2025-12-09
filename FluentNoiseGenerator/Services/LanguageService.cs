using Microsoft.Windows.Globalization;
using System.Globalization;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for retrieving and updating the current application language info.
/// </summary>
public sealed class LanguageService
{
    #region Fields
    private CultureInfo? _currentCultureInfo;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the current <see cref="CultureInfo"/> instance.
    /// </summary>
    public CultureInfo? CurrentCultureInfo
    {
        get => _currentCultureInfo;
        set
        {
            _currentCultureInfo = value;

            ApplicationLanguages.PrimaryLanguageOverride = value?.Name;

            CurrentCultureInfoChanged?.Invoke(value);
        }
    }
    #endregion

    #region Events
    /// <summary>
    /// Fires when <see cref="CurrentCultureInfo"/> gets updated.
    /// </summary>
    public event CurrentCultureInfoChangedHandler CurrentCultureInfoChanged = delegate { };
    #endregion

    #region Delegates
    /// <summary>
    /// Method signature for the <see cref="CurrentCultureInfoChanged"/> event.
    /// </summary>
    /// <param name="value">
    /// A <see cref="CultureInfo"/> instance for the new, targeted language.
    /// </param>
    public delegate void CurrentCultureInfoChangedHandler(CultureInfo? value);
    #endregion
}