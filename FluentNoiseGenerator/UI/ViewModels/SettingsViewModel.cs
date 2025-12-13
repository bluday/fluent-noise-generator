using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Globalization;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.Infrastructure.Messages;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentNoiseGenerator.UI.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsViewModel : ObservableObject
{
    #region Fields
    private readonly bool _isInitializing;

    private readonly NoisePlaybackService _noisePlaybackService;

    private readonly IMessenger _messenger;
    #endregion

    #region Observable properties
    /// <summary>
    /// Gets the current <see cref="ElementTheme"/> for the application.
    /// </summary>
    [ObservableProperty]
    public partial ElementTheme CurrentTheme { get; private set; }

    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    [ObservableProperty]
    public partial ResourceNamedValue<ElementTheme>? SelectedApplicationTheme { get; set; }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    [ObservableProperty]
    public partial NamedValue<int>? SelectedAudioSampleRate { get; set; }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    [ObservableProperty]
    public partial NamedValue<ILanguage>? SelectedLanguage { get; set; }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    [ObservableProperty]
    public partial ResourceNamedValue<string>? SelectedDefaultNoisePreset { get; set; }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    [ObservableProperty]
    public partial ResourceNamedValue<SystemBackdrop>? SelectedSystemBackdrop { get; set; }
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
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public SettingsWindowStringResources StringResources { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="noisePlaybackService">
    /// The noise playback service for managing playback within the app.
    /// </param>
    /// <param name="languageService">
    /// The language service for managing the current application language.
    /// </param>
    /// <param name="themeService">
    /// The theme service for managing the current application theme.
    /// </param>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources
    /// specifically for this view.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsViewModel(
        NoisePlaybackService          noisePlaybackService,
        LanguageService               languageService,
        ThemeService                  themeService,
        LocalizedResourceProvider     localizedResourceProvider,
        SettingsWindowStringResources stringResources,
        IMessenger                    messenger)
    {
        ArgumentNullException.ThrowIfNull(noisePlaybackService);
        ArgumentNullException.ThrowIfNull(languageService);
        ArgumentNullException.ThrowIfNull(themeService);
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(messenger);

        _isInitializing = true;

        _messenger = messenger;

        _noisePlaybackService = noisePlaybackService;

        AvailableApplicationThemes = new ResourceNamedValue<ElementTheme>[]
        {
            new(
                new StringResource("Common/System", localizedResourceProvider),
                ElementTheme.Default
            ),
            new(
                new StringResource("Common/Dark", localizedResourceProvider),
                ElementTheme.Dark
            ),
            new(
                new StringResource("Common/Light", localizedResourceProvider),
                ElementTheme.Light
            )
        };

        AvailableNoisePresets = new ResourceNamedValue<string>[]
        {
            new(
                new StringResource("Common/Blue", localizedResourceProvider),
                string.Empty
            ),
            new(
                new StringResource("Common/Brownian", localizedResourceProvider),
                string.Empty
            ),
            new(
                new StringResource("Common/White", localizedResourceProvider),
                string.Empty
            )
        };

        AvailableSystemBackdrops = new ResourceNamedValue<SystemBackdrop>[]
        {
            new(
                new StringResource("SystemBackdrop/Mica", localizedResourceProvider),
                new MicaBackdrop()
            ),
            new(
                new StringResource("SystemBackdrop/MicaAlt", localizedResourceProvider),
                new MicaBackdrop { Kind = MicaKind.BaseAlt }
            ),
            new(
                new StringResource("SystemBackdrop/Acrylic", localizedResourceProvider),
                new DesktopAcrylicBackdrop()
            ),
            new(
                new StringResource("Common/None", localizedResourceProvider),
                null!
            )
        };

        AvailableAudioSampleRates = noisePlaybackService.AvailableSampleRates
            .Select(
                sampleRateValue => new NamedValue<int>(
                    value:     sampleRateValue,
                    formatter: _ => $"{sampleRateValue} {stringResources.Sound.SampleRateHertzUnit.Value}"
                )
            )
            .ToArray();

        AvailableLanguages = languageService.AvailableLanguages
            .Select(
                language => new NamedValue<ILanguage>(
                    value:     language,
                    formatter: _ => language.DisplayName
                )
            )
            .ToArray();

        StringResources = stringResources;

        SelectedApplicationTheme   = AvailableApplicationThemes.FirstOrDefault();
        SelectedAudioSampleRate    = AvailableAudioSampleRates.FirstOrDefault();
        SelectedDefaultNoisePreset = AvailableNoisePresets.FirstOrDefault();
        SelectedLanguage           = AvailableLanguages.FirstOrDefault();
        SelectedSystemBackdrop     = AvailableSystemBackdrops.FirstOrDefault();

        SubscribeToMessages();

        _isInitializing = false;
    }
    #endregion

    #region Property changing and changed methods
    partial void OnSelectedLanguageChanged(
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

    partial void OnSelectedApplicationThemeChanged(
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
    private bool CanPerformPostPropertyValueChangeOperation<TValue>(TValue? oldValue, TValue? newValue)
    {
        return !_isInitializing && oldValue?.Equals(newValue) is false;
    }

    private void RefreshLocalizedContent()
    {
        OnPropertyChanged(nameof(AvailableApplicationThemes));
        OnPropertyChanged(nameof(AvailableAudioSampleRates));
        OnPropertyChanged(nameof(AvailableLanguages));
        OnPropertyChanged(nameof(AvailableNoisePresets));
        OnPropertyChanged(nameof(AvailableSystemBackdrops));

        OnPropertyChanged(nameof(StringResources));
    }

    private void SubscribeToMessages()
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
    #endregion

    #region Message handlers
    private void HandleApplicationThemeUpdatedMessage(
        object                         recipient,
        ApplicationThemeUpdatedMessage message)
    {
        CurrentTheme = message.Value;
    }

    private void HandleLocalizedResourceProviderUpdatedMessage(
        object                                  recipient,
        LocalizedResourceProviderUpdatedMessage message)
    {
        RefreshLocalizedContent();
    }
    #endregion
}