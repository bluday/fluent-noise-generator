using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.UI.Xaml;
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

    private readonly bool _isInitializing;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets an enumerable of available application themes.
    /// </summary>
    public IEnumerable<ResourceNamedValue<ElementTheme>> AvailableApplicationThemes { get; private set; }

    /// <summary>
    /// Gets an enumerable of available audio sample rates.
    /// </summary>
    public IEnumerable<NamedValue<int>> AvailableAudioSampleRates { get; private set; }

    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    public IEnumerable<NamedValue<ILanguage>> AvailableLanguages { get; private set; }

    /// <summary>
    /// Gets an enumerable of available noise presets.
    /// </summary>
    public IEnumerable<ResourceNamedValue<string>> AvailableNoisePresets { get; private set; }

    /// <summary>
    /// Gets an enumerable of available system backdrops.
    /// </summary>
    public IEnumerable<ResourceNamedValue<SystemBackdrop>> AvailableSystemBackdrops { get; private set; }

    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    public ResourceNamedValue<ElementTheme>? SelectedApplicationTheme { get; set; }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    public NamedValue<int>? SelectedAudioSampleRate { get; set; }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    public NamedValue<ILanguage>? SelectedLanguage { get; set; }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    public ResourceNamedValue<string>? SelectedDefaultNoisePreset { get; set; }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    public ResourceNamedValue<SystemBackdrop>? SelectedSystemBackdrop { get; set; }

    /// <summary>
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public SettingsWindowResources StringResources { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="appResources">
    /// An <see cref="AppResources"/> instance with localized app resources.
    /// </param>
    /// <param name="appSettings">
    /// An <see cref="IAppSettings"/> instance with selected settings for the application.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsViewModel(
        AppResources appResources,
        IAppSettings appSettings,
        IMessenger   messenger)
    {
        ArgumentNullException.ThrowIfNull(appResources);
        ArgumentNullException.ThrowIfNull(appSettings);
        ArgumentNullException.ThrowIfNull(messenger);

        _appSettings = appSettings;

        _isInitializing = true;

        _messenger = messenger;

        AvailableApplicationThemes = [];
        AvailableAudioSampleRates  = [];
        AvailableLanguages         = [];
        AvailableNoisePresets      = [];
        AvailableSystemBackdrops   = [];

        StringResources = new();

        RegisterMessageHandlers();

        _isInitializing = false;
    }
    #endregion

    #region Property changed methods
    private void OnSelectedLanguageChanged(
        NamedValue<ILanguage>? oldValue,
        NamedValue<ILanguage>? newValue)
    {
        if (!CanPerformPostPropertyValueChangeOperation(oldValue?.Value, newValue?.Value))
        {
            return;
        }

        if (newValue?.Value is ILanguage language)
        {
            _messenger.Send(new UpdateApplicationLanguageMessage(language));
        }
    }

    private void OnSelectedApplicationThemeChanged(
        ResourceNamedValue<ElementTheme>? oldValue,
        ResourceNamedValue<ElementTheme>? newValue)
    {
        if (!CanPerformPostPropertyValueChangeOperation(oldValue?.Value, newValue?.Value))
        {
            return;
        }

        if (newValue?.Value is ElementTheme elementTheme)
        {
            _messenger.Send(new UpdateApplicationThemeMessage(elementTheme));
        }
    }
    #endregion

    #region Instance methods
    private bool CanPerformPostPropertyValueChangeOperation<TValue>(
        TValue? oldValue,
        TValue? newValue)
    {
        return !_isInitializing && oldValue?.Equals(newValue) is false;
    }

    private void RefreshLocalizedContent()
    {
        OnPropertyChanged(nameof(StringResources));
    }

    private void RegisterMessageHandlers()
    {
        _messenger.Register<ApplicationThemeUpdatedMessage>(
            this,
            HandleApplicationThemeUpdatedMessage
        );

        _messenger.Register<LocalizedResourceProviderUpdatedMessage>(
            this,
            HandleLocalizedResourceProviderUpdatedMessage
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
            theme => theme.Value == message.Value
        );
    }

    private void HandleLocalizedResourceProviderUpdatedMessage(
        object                                  recipient,
        LocalizedResourceProviderUpdatedMessage message)
    {
        RefreshLocalizedContent();
    }
    #endregion
}