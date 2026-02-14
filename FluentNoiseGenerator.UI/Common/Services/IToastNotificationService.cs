using Microsoft.Windows.AppNotifications;
using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Service for sending toast notifications to the operating system.
/// </summary>
public interface IToastNotificationService
{
    #region Properties
    /// <summary>
    /// Gets a read-only map of issued notfications.
    /// </summary>
    IReadOnlyDictionary<uint, AppNotification> IssuedNotifications { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Sends a toast notification using the specified title and content.
    /// </summary>
    void Send(string title, string? content);
    #endregion
}