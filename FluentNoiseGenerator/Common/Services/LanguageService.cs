using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for retrieving and updating the current application language info.
/// </summary>
public sealed class LanguageService
{
    #region Fields
    private ILanguage? _currentLanguage;

    private readonly IMessenger _messenger;

    private readonly ImmutableList<ILanguage> _availableLanguages;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the culture.
    /// </summary>
    public ILanguage? CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            if (value is not null && !_availableLanguages.Contains(value))
            {
                // TODO: Throw invalid operation exception.
            }

            _currentLanguage = value;

            ApplicationLanguages.PrimaryLanguageOverride = value?.Name;

            _messenger.Send(new ApplicationLanguageChangedMessage());
        }
    }

    /// <summary>
    /// Gets a read-only list of available languages.
    /// </summary>
    public IReadOnlyList<ILanguage> AvailableLanguages
    {
        get => _availableLanguages.AsReadOnly();
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public LanguageService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _availableLanguages = [
            ..ApplicationLanguages.ManifestLanguages.Select(
                language => new Language(language)
            )
        ];

        _messenger = messenger;

        messenger.Register<UpdateApplicationLanguageMessage>(
            recipient: this,
            handler:   (_, message) => CurrentLanguage = message.Value
        );
    }
    #endregion
}