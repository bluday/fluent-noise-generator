using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Common.Messages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentNoiseGenerator.UI.Services;

namespace FluentNoiseGenerator.UI.Settings.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsViewModel : ObservableObject, IDisposable
{
    #region Fields
    private readonly IAppSettings _appSettings;

    private readonly bool _isInitializing;

    private readonly LanguageService _languageService;

    private readonly IMessenger _messenger;

    private readonly ThemeService _themeService;
    #endregion

    #region Properties
    /// <summary>
    /// Gets an enumerable of available application themes.
    /// </summary>
    public IEnumerable<ResourceNamedValue<object>> AvailableApplicationThemes { get; private set; }

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
    public ResourceNamedValue<object>? SelectedApplicationTheme { get; set; }

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
    /// <param name="languageService">
    /// The service for managing the application language.
    /// </param>
    /// <param name="themeService">
    /// The service for managing the application theme.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsViewModel(
        AppResources    appResources,
        IAppSettings    appSettings,
        LanguageService languageService,
        ThemeService    themeService,
        IMessenger      messenger)
    {
        ArgumentNullException.ThrowIfNull(appResources);
        ArgumentNullException.ThrowIfNull(appSettings);
        ArgumentNullException.ThrowIfNull(languageService);
        ArgumentNullException.ThrowIfNull(themeService);
        ArgumentNullException.ThrowIfNull(messenger);

        _appSettings = appSettings;

        _isInitializing = true;

        _languageService = languageService;

        _messenger = messenger;

        _themeService = themeService;

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
    private void OnSelectedLanguageChanged(NamedValue<ILanguage>? value)
    {
        if (!_isInitializing && value?.Value is ILanguage language)
        {
            _languageService.CurrentLanguage = language;
        }
    }

    private void OnSelectedApplicationThemeChanged(ResourceNamedValue<ElementTheme>? value)
    {
        if (!_isInitializing && value?.Value is ElementTheme elementTheme)
        {
            _themeService.CurrentTheme = elementTheme;
        }
    }
    #endregion

    #region Instance methods
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