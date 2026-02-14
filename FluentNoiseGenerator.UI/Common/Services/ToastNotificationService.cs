using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Messages;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using System;
using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Default implementation for the <see cref="IToastNotificationService"/> service.
/// </summary>
public sealed partial class ToastNotificationService : IToastNotificationService, IDisposable
{
    #region Fields
    private readonly Dictionary<uint, AppNotification> _issuedNotifications;

    private readonly IMessenger _messenger;

    private readonly AppNotificationManager _notificationManager;
    #endregion

    #region Properties
    /// <inheritdoc cref="IToastNotificationService.IssuedNotifications"/>
    public IReadOnlyDictionary<uint, AppNotification> IssuedNotifications
    {
        get => _issuedNotifications.AsReadOnly();
    }
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

        _issuedNotifications = [];

        _messenger = messenger;

        _notificationManager = AppNotificationManager.Default;

        _notificationManager.NotificationInvoked += _notificationManager_NotificationInvoked;

        RegisterMessageHandlers();
    }
    #endregion

    #region Event handlers
    private void _notificationManager_NotificationInvoked(
        AppNotificationManager            sender,
        AppNotificationActivatedEventArgs args)
    {
        // TODO: Parse arguments to perform certain actions.
    }
    #endregion

    #region Methods
    private AppNotification BuildNotification(string title, string? content)
    {
        AppNotificationBuilder builder = new();

        builder.AddText(title);

        if (!string.IsNullOrWhiteSpace(content))
        {
            builder.AddText(content);
        }

        return builder.BuildNotification();
    }

    private void RegisterMessageHandlers()
    {
        _messenger.Register<IssueToastNotificationMessage>(
            this,
            (_, message) => Send(message.Title, message.Content)
        );
    }

    private static void ConfigureNotificationProperties(AppNotification notification)
    {
        notification.ExpiresOnReboot = true;
        notification.Priority        = AppNotificationPriority.High;
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);

        _notificationManager.NotificationInvoked -= _notificationManager_NotificationInvoked;
    }

    /// <inheritdoc cref="IToastNotificationService.Send(string, string?)"/>
    /// <exception cref="ArgumentException">
    /// Throws if <paramref name="title"/> is null or whitespace.
    /// </exception>
    public void Send(string title, string? content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        AppNotification notification = BuildNotification(title, content);

        ConfigureNotificationProperties(notification);

        _notificationManager.Show(notification);

        _issuedNotifications.TryAdd(notification.Id, notification);
    }
    #endregion
}