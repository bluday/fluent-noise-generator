using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Infrastructure.Messages;

/// <summary>
/// Represents a message for when the application theme has been changed.
/// </summary>
/// <param name="Value">
/// The current application theme.
/// </param>
public sealed record ApplicationThemeUpdatedMessage(ElementTheme Value);