using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Default implementation for the <see cref="IBackdropService"/> service.
/// </summary>
public sealed partial class BackdropService : IBackdropService, IDisposable
{
    #region Fields
    private object? _currentBackdrop;

    private readonly Collection<object> _backdrops;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <inheritdoc cref="IBackdropService.CurrentBackdrop"/>
    public object? CurrentBackdrop
    {
        get => _currentBackdrop;
        set
        {
            if (_currentBackdrop != value)
            {
                _currentBackdrop = value;
            }
        }
    }

    /// <inheritdoc cref="IBackdropService.Backdrops"/>
    public IEnumerable<object> Backdrops => _backdrops;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="BackdropService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="messenger"> is <c>null</c>.
    /// </exception>
    public BackdropService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        _backdrops = [];
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