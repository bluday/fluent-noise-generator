namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for sending toast notifications to the operating system.
/// </summary>
public interface IToastNotificationService
{
    /// <summary>
    /// Sends the specified toast notification.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task SendAsync();
}