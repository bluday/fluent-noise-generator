using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.StringResources;
using FluentNoiseGenerator.Messages;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FluentNoiseGenerator.UI.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsViewModel : ObservableObject
{
    #region Fields
    private readonly bool _isInitializing;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
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
    public partial NamedValue<CultureInfo>? SelectedLanguage { get; set; }

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

    /// <summary>
    /// Gets an enumerable of available application themes.
    /// </summary>
    public IEnumerable<ResourceNamedValue<ElementTheme>> AvailableApplicationThemes { get; }

    /// <summary>
    /// Gets an enumerable of available audio sample rates.
    /// </summary>
    public IEnumerable<NamedValue<int>> AvailableAudioSampleRates { get; }

    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    public IEnumerable<NamedValue<CultureInfo>> AvailableLanguages { get; }

    /// <summary>
    /// Gets an enumerable of available noise presets.
    /// </summary>
    public IEnumerable<ResourceNamedValue<string>> AvailableNoisePresets { get; }

    /// <summary>
    /// Gets an enumerable of available system backdrops.
    /// </summary>
    public IEnumerable<ResourceNamedValue<SystemBackdrop>> AvailableSystemBackdrops { get; }

    /// <summary>
    /// Gets the string resource collection instance specific to this window.
    /// </summary>
    public SettingsWindowStringResources StringResources { get; }

    /// <summary>
    /// Gets the path for the title bar icon.
    /// </summary>
    public string TitleBarIconPath { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
    /// </summary>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources
    /// specifically for this view.
    /// </param>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsViewModel(
        SettingsWindowStringResources stringResources,
        LocalizedResourceProvider     localizedResourceProvider,
        IMessenger                    messenger)
    {
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);
        ArgumentNullException.ThrowIfNull(messenger);

        _isInitializing = true;

        _messenger = messenger;

        AvailableApplicationThemes = CreateAvailableApplicationThemes(localizedResourceProvider);
        AvailableAudioSampleRates  = CreateAvailableAudioSampleRates(localizedResourceProvider);
        AvailableLanguages         = CreateAvailableLanguages();
        AvailableNoisePresets      = CreateAvailableNoisePresets(localizedResourceProvider);
        AvailableSystemBackdrops   = CreateAvailableSystemBackrops(localizedResourceProvider);

        SelectedApplicationTheme   = AvailableApplicationThemes.First();
        SelectedAudioSampleRate    = AvailableAudioSampleRates.First();
        SelectedDefaultNoisePreset = AvailableNoisePresets.First();
        SelectedLanguage           = AvailableLanguages.First();
        SelectedSystemBackdrop     = AvailableSystemBackdrops.First();

        StringResources = stringResources;

        TitleBarIconPath = System.IO.Path.Combine(
            AppContext.BaseDirectory,
            "Assets/Icon-64.ico"
        );

        _messenger.Register<LocalizedResourceProviderUpdatedMessage>(
            this,
            HandleLocalizedResourceProviderUpdatedMessage
        );

        _isInitializing = false;
    }
    #endregion

    #region Property changing and changed methods
    partial void OnSelectedLanguageChanged(NamedValue<CultureInfo>? value)
    {
        if (!_isInitializing && value?.Value is CultureInfo cultureInfo)
        {
            _messenger.Send(new UpdateApplicationLanguageMessage(cultureInfo));
        }
    }
    #endregion

    #region Methods
    private static IEnumerable<ResourceNamedValue<ElementTheme>> CreateAvailableApplicationThemes(
        LocalizedResourceProvider localizedResourceProvider)
    {
        return [
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
        ];
    }

    private static IEnumerable<NamedValue<int>> CreateAvailableAudioSampleRates(
        LocalizedResourceProvider localizedResourceProvider)
    {
        string audioSampleRateUnit = localizedResourceProvider.Get("Units/Hertz/Short");

        string GetDisplayableAudioSampleRateValue(int value)
        {
            return $"{value} {audioSampleRateUnit}";
        }

        return [
            new(AudioSampleRates.Rate48000Hz, GetDisplayableAudioSampleRateValue),
            new(AudioSampleRates.Rate44100Hz, GetDisplayableAudioSampleRateValue)
        ];
    }

    private static IEnumerable<NamedValue<CultureInfo>> CreateAvailableLanguages()
    {
        return ApplicationLanguages.ManifestLanguages.Select(
            language => new NamedValue<CultureInfo>(
                new(language),
                language => language.NativeName
            )
        );
    }

    private static IEnumerable<ResourceNamedValue<string>> CreateAvailableNoisePresets(
        LocalizedResourceProvider localizedResourceProvider)
    {
        return [
            new(
                new StringResource("Common/Blue", localizedResourceProvider),
                null
            ),
            new(
                new StringResource("Common/Brownian", localizedResourceProvider),
                null
            ),
            new(
                new StringResource("Common/White", localizedResourceProvider),
                null
            )
        ];
    }

    private static IEnumerable<ResourceNamedValue<SystemBackdrop>> CreateAvailableSystemBackrops(
        LocalizedResourceProvider localizedResourceProvider)
    {
        return [
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
                null
            )
        ];
    }
    #endregion

    #region Message handlers
    private void HandleLocalizedResourceProviderUpdatedMessage(
        object                                  recipient,
        LocalizedResourceProviderUpdatedMessage message)
    {
        OnPropertyChanged(nameof(StringResources));
    }
    #endregion
}