namespace FluentNoiseGenerator.Infrastructure.Messages;

/// <summary>
/// Represents a message for updating the current application theme.
/// </summary>
/// <param name="Value">
/// The new application theme to set.
/// </param>
public sealed record UpdateApplicationThemeMessage(Microsoft.UI.Xaml.ElementTheme Value);