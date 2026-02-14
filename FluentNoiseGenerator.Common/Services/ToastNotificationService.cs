using CommunityToolkit.Mvvm.Messaging;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Default implementation for the <see cref="IToastNotificationService"/> service.
/// </summary>
public sealed class ToastNotificationService : IToastNotificationService, IDisposable
{
    #region Fields
    private readonly IMessenger _messenger;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ToastNotificationService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public ToastNotificationService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Methods
    private void RegisterMessageHandlers() { }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }

    /// <inheritdoc cref="IToastNotificationService.SendAsync()"/>
    public Task SendAsync()
    {
        return Task.CompletedTask;
    }
    #endregion
}