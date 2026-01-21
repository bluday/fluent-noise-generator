using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FluentNoiseGenerator.Common.Messages;

/// <summary>
/// Represents a message for when the application theme has been changed.
/// </summary>
public sealed class ApplicationThemeUpdatedMessage(object value)
    : ValueChangedMessage<object>(value) { }