using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for managing the current theme of the application.
/// </summary>
public sealed class ThemeService
{
    #region Fields
    private SystemBackdrop? _currentSystemBackdrop;

    private ElementTheme _currentTheme;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the system backdrop to be used for all windows.
    /// </summary>
    public SystemBackdrop? CurrentSystemBackdrop
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
    public ElementTheme CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme == value) return;

            _currentTheme = value;

            _messenger.Send(new ApplicationThemeUpdatedMessage(value));
        }
    }
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
    }
    #endregion
}