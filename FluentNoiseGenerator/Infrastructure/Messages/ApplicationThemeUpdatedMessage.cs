using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Infrastructure.Messages;

/// <summary>
/// Represents a message for when the application theme has been changed.
/// </summary>
public sealed class ApplicationThemeUpdatedMessage(ElementTheme value)
    : ValueChangedMessage<ElementTheme>(value) { }