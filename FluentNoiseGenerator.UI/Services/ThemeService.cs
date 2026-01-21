using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Messages;
using System;
using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Services;

/// <summary>
/// Service for managing the current theme of the application.
/// </summary>
public sealed class ThemeService : IDisposable
{
    #region Fields
    private object? _currentSystemBackdrop;

    private object _currentTheme;

    private readonly IEnumerable<object> _systemBackdrops;

    private readonly IEnumerable<object> _themes;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the system backdrop to be used for all windows.
    /// </summary>
    public object? CurrentSystemBackdrop
    {
        get => _currentSystemBackdrop;
        set
        {
            if (_currentSystemBackdrop == value) return;

            _currentSystemBackdrop = value;

            // TODO: Send message.
        }
    }

    /// <summary>
    /// Gets or sets the current theme of the application.
    /// </summary>
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

    /// <summary>
    /// Gets an enumerable of system backdrops.
    /// </summary>
    public IEnumerable<object> SystemBackdrops => _systemBackdrops;

    /// <summary>
    /// Gets an enumerable of themes.
    /// </summary>
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

        _messenger = messenger;

        _themes = null; /*Enum.GetValues<object>();*/

        _systemBackdrops = []; /*[
            new MicaBackdrop(),
            new MicaBackdrop { Kind = MicaKind.BaseAlt },
            new DesktopAcrylicBackdrop()
        ];*/
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