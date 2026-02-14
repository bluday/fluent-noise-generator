using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Messages;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Default implementation for the <see cref="ILanguageService"/> service.
/// </summary>
public sealed class LanguageService : ILanguageService, IDisposable
{
    #region Fields
    private ILanguage _currentLanguage;

    private readonly IEnumerable<ILanguage> _availableLanguages;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <inheritdoc cref="ILanguageService.AvailableLanguages"/>
    public IEnumerable<ILanguage> AvailableLanguages => _availableLanguages;

    /// <inheritdoc cref="ILanguageService.CurrentLanguage"/>
    public ILanguage CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            _currentLanguage = value;

            // ApplicationLanguages.PrimaryLanguageOverride = value.Name;

            _messenger.Send(new ApplicationLanguageChangedMessage(value));
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

        _availableLanguages = [];

        _currentLanguage = new Language("en-US");

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Methods
    private void RegisterMessageHandlers()
    {
        _messenger.Register<UpdateApplicationLanguageMessage>(
            recipient: this,
            handler: (_, message) => CurrentLanguage = message.Value
        );
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}