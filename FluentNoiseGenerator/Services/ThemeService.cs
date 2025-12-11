using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for managing the current theme of the application.
/// </summary>
public sealed class ThemeService
{
    #region Fields
    private SystemBackdrop? _systemBackdrop;

    private ElementTheme _theme;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the system backdrop to be used for all windows.
    /// </summary>
    public SystemBackdrop? SystemBackdrop
    {
        get => _systemBackdrop;
        set
        {
            _systemBackdrop = value;

            // TODO: Send message.
        }
    }

    /// <summary>
    /// Gets or sets the current theme of the application.
    /// </summary>
    public ElementTheme CurrentTheme
    {
        get => _theme;
        set
        {
            _theme = value;

            // TODO: Send message.
        }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// This is typically a <see cref="WeakReferenceMessenger"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public ThemeService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;
    }
    #endregion
}