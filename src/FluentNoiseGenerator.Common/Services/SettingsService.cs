using CommunityToolkit.Mvvm.Messaging;

namespace FluentNoiseGenerator.Common.Services;

/// <summary>
/// Default implementation for the <see cref="ISettingsService"/> service.
/// </summary>
public sealed class SettingsService : ISettingsService, IDisposable
{
    #region Fields
    private IAppSettings _currentSettings;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <inheritdoc cref="ISettingsService.CurrentSettings"/>
    public IAppSettings CurrentSettings => _currentSettings;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsService"/> class.
    /// </summary>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="messenger"> is <c>null</c>.
    /// </exception>
    public SettingsService(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _currentSettings = new AppSettings(this);

        _messenger = messenger;
    }
    #endregion

    #region Methods
    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion
}