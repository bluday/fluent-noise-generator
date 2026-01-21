using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Messages;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for retrieving and updating the current application language info.
/// </summary>
public sealed class LanguageService : IDisposable
{
    #region Fields
    private ILanguage _currentLanguage;

    private readonly IEnumerable<ILanguage> _availableLanguages;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the culture.
    /// </summary>
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

    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    public IEnumerable<ILanguage> AvailableLanguages => _availableLanguages;
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

        _availableLanguages = []; /*ApplicationLanguages.ManifestLanguages
            .Select(language => new Language(language))
            .Cast<ILanguage>();*/

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
            handler:   (_, message) => CurrentLanguage = message.Value
        );
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}