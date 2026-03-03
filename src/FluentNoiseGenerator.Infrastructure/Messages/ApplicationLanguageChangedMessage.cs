using CommunityToolkit.Mvvm.Messaging.Messages;
using FluentNoiseGenerator.Infrastructure.Globalization;

namespace FluentNoiseGenerator.Infrastructure.Messages;

/// <summary>
/// Represents a message for when the application language has been changed.
/// </summary>
public sealed class ApplicationLanguageChangedMessage(ILanguage value)
    : ValueChangedMessage<ILanguage>(value) { }