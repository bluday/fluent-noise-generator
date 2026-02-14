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
    /// <param name="title">
    /// The title or the primary text control.
    /// </param>
    /// <param name="content">
    /// The content or the secondary text control. Is nullable.
    /// </param>
    void Send(string title, string? content);
    #endregion
}