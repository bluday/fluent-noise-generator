using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Messages;
using System;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Service for retrieving and updating the current application language info.
/// </summary>
public sealed class LocalizationService : IDisposable
{
    #region Fields
    private LocalizedResourceProvider _localizedResourceProvider;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the current localized resource provider instance.
    /// </summary>
    public LocalizedResourceProvider ResourceProvider => _localizedResourceProvider;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizationService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public LocalizationService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _localizedResourceProvider = new LocalizedResourceProvider();

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Methods
    private void RegisterMessageHandlers()
    {
        _messenger.Register<ApplicationLanguageChangedMessage>(
            this,
            HandleApplicationLanguageChangedMessage
        );
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }

    /// <summary>
    /// Updates the resource loader instance stored in the resource provider.
    /// </summary>
    public void UpdateResourceProvider()
    {
        _localizedResourceProvider.UpdateResourceLoader();
    }
    #endregion

    #region Message handlers
    private void HandleApplicationLanguageChangedMessage(
        object                            recipient,
        ApplicationLanguageChangedMessage message)
    {
        UpdateResourceProvider();

        _messenger.Send(new LocalizedResourceProviderUpdatedMessage());
    }
    #endregion
}