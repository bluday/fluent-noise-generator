namespace FluentNoiseGenerator.Common.Messages;

/// <summary>
/// Represents a message for issuing a toast notification to the operating system.
/// </summary>
/// <param name="Title">
/// The title or first text element of the notification.
/// </param>
/// <param name="Content">
/// The content or second text element of the notification. Is nullable.
/// </param>
public sealed record IssueToastNotificationMessage(
    string  Title,
    string? Content = null
);