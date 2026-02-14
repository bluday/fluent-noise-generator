using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Messages;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentNoiseGenerator.UI.Settings.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsViewModel : ObservableObject, IDisposable
{
    #region Fields
    private readonly IAppSettings _appSettings;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets an enumerable of available application themes.
    /// </summary>
    public IEnumerable<object> AvailableApplicationThemes { get; private set; } = [];

    /// <summary>
    /// Gets an enumerable of available audio sample rates.
    /// </summary>
    public IEnumerable<int> AvailableAudioSampleRates { get; private set; } = [];

    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    public IEnumerable<ILanguage> AvailableLanguages { get; private set; } = [];

    /// <summary>
    /// Gets an enumerable of available noise presets.
    /// </summary>
    public IEnumerable<string> AvailableNoisePresets { get; private set; } = [];

    /// <summary>
    /// Gets an enumerable of available system backdrops.
    /// </summary>
    public IEnumerable<SystemBackdrop> AvailableSystemBackdrops { get; private set; } = [];

    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    public object? SelectedApplicationTheme { get; set; }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    public int? SelectedAudioSampleRate { get; set; }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    public ILanguage? SelectedLanguage { get; set; }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    public string? SelectedDefaultNoisePreset { get; set; }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    public SystemBackdrop? SelectedSystemBackdrop { get; set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class using
    /// the specified dependencies.
    /// </summary>
    /// <param name="appSettings">
    /// An <see cref="IAppSettings"/> instance with selected settings for the application.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsViewModel(IAppSettings appSettings, IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(appSettings);
        ArgumentNullException.ThrowIfNull(messenger);

        _appSettings = appSettings;

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers()
    {
        _messenger.Register<ApplicationThemeUpdatedMessage>(
            this,
            HandleApplicationThemeUpdatedMessage
        );
    }

    /// <inheritdoc cref="IDisposable.Dispose()"/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    #endregion

    #region Message handlers
    private void HandleApplicationThemeUpdatedMessage(
        object                         recipient,
        ApplicationThemeUpdatedMessage message)
    {
        SelectedApplicationTheme = AvailableApplicationThemes.FirstOrDefault(
            theme => message.Value == theme
        );
    }
    #endregion
}