using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Windows.Globalization;
using System;
using System.Globalization;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for retrieving and updating the current application language info.
/// </summary>
public sealed class LanguageService
{
    #region Fields
    private CultureInfo? _currentCultureInfo;

    private readonly IMessenger _messenger;
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

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public LanguageService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;
    }
    #endregion
}