using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.Windows.Globalization;
using System;
using System.Globalization;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for retrieving and updating the current application language info.
/// </summary>
public sealed class LanguageService
{
    #region Fields
    private CultureInfo? _culture;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the culture.
    /// </summary>
    public CultureInfo? Culture
    {
        get => _culture;
        set
        {
            _culture = value;

            ApplicationLanguages.PrimaryLanguageOverride = value?.Name;

            _messenger.Send(new ApplicationLanguageChangedMessage());
        }
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

        _messenger = messenger;

        _messenger.Register<UpdateApplicationLanguageMessage>(
            this,
            HandleUpdateApplicationLanguageMessage
        );
    }
    #endregion

    #region Message handlers
    private void HandleUpdateApplicationLanguageMessage(
        object                           recipient,
        UpdateApplicationLanguageMessage message)
    {
        Culture = message.Culture;
    }
    #endregion
}