using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Default implementation for the <see cref="IThemeService"/> service.
/// </summary>
public sealed partial class ThemeService : IThemeService, IDisposable
{
    #region Fields
    private object _currentTheme;

    private readonly Collection<object> _themes;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <inheritdoc cref="IThemeService.CurrentTheme"/>
    public object CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme == value) return;

            _currentTheme = value;

            _messenger.Send(new ApplicationThemeUpdatedMessage(value));
        }
    }

    /// <inheritdoc cref="IThemeService.Themes"/>
    public IEnumerable<object> Themes => _themes;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="messenger"> is <c>null</c>.
    /// </exception>
    public ThemeService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _currentTheme = null!;

        _messenger = messenger;

        _themes = null!;
    }
    #endregion

    #region Methods
    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}