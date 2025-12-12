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

    private readonly LocalizedResourceProvider _localizedResourceProvider;

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
    public IEnumerable<ResourceNamedValue<ElementTheme>>? AvailableApplicationThemes { get; private set; }

    /// <summary>
    /// Gets an enumerable of available audio sample rates.
    /// </summary>
    public IEnumerable<NamedValue<int>>? AvailableAudioSampleRates { get; private set; }

    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    public IEnumerable<NamedValue<CultureInfo>>? AvailableLanguages { get; private set; }

    /// <summary>
    /// Gets an enumerable of available noise presets.
    /// </summary>
    public IEnumerable<ResourceNamedValue<string>>? AvailableNoisePresets { get; private set; }

    /// <summary>
    /// Gets an enumerable of available system backdrops.
    /// </summary>
    public IEnumerable<ResourceNamedValue<SystemBackdrop>>? AvailableSystemBackdrops { get; private set; }

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

        _localizedResourceProvider = localizedResourceProvider;

        StringResources = stringResources;

        TitleBarIconPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Assets/Icon-64.ico");

        InitializeValueNamedOptions();
        InitializeResourceNamedOptions();

        RefreshLocalizedContent();

        SubscribeToMessages();

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

    #region Instance methods
    private IEnumerable<ResourceNamedValue<TValue>> CreateResourceNamedOptions<TValue>(
        IEnumerable<(string, TValue)> options)
    {
        foreach (var (resourceId, value) in options)
        {
            yield return new(
                new StringResource(resourceId, _localizedResourceProvider),
                value
            );
        }
    }

    private void InitializeValueNamedOptions()
    {
        string audioSampleRateUnit = _localizedResourceProvider.Get("Units/Hertz/Short");

        string GetUnitSuffixedStringFormat(int value)
        {
            return $"{value} {audioSampleRateUnit}";
        }

        AvailableAudioSampleRates = [
            new(AudioSampleRates.Rate48000Hz, GetUnitSuffixedStringFormat),
            new(AudioSampleRates.Rate44100Hz, GetUnitSuffixedStringFormat)
        ];

        AvailableLanguages = ApplicationLanguages.ManifestLanguages.Select(
            language => new NamedValue<CultureInfo>(
                new(language),
                language => language.NativeName
            )
        );

        SelectedAudioSampleRate = AvailableAudioSampleRates.First();
        SelectedLanguage        = AvailableLanguages.First();
    }

    private void InitializeResourceNamedOptions()
    {
        AvailableApplicationThemes = CreateResourceNamedOptions([
            ("Common/System", ElementTheme.Default),
            ("Common/Dark",   ElementTheme.Dark),
            ("Common/Light",  ElementTheme.Light)
        ]);

        AvailableNoisePresets = CreateResourceNamedOptions([
            ("Common/Blue",     string.Empty),
            ("Common/Brownian", string.Empty),
            ("Common/White",    string.Empty)
        ]);

        AvailableSystemBackdrops = CreateResourceNamedOptions([
            ("SystemBackdrop/Mica",    new MicaBackdrop()),
            ("SystemBackdrop/MicaAlt", new MicaBackdrop { Kind = MicaKind.BaseAlt }),
            ("SystemBackdrop/Acrylic", new DesktopAcrylicBackdrop()),
            ("Common/None",            (SystemBackdrop)null!)
        ]);

        SelectedApplicationTheme   = AvailableApplicationThemes.First();
        SelectedDefaultNoisePreset = AvailableNoisePresets.First();
        SelectedSystemBackdrop     = AvailableSystemBackdrops.First();
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
        _messenger.Register<LocalizedResourceProviderUpdatedMessage>(
            this,
            HandleLocalizedResourceProviderUpdatedMessage
        );
    }
    #endregion

    #region Message handlers
    private void HandleLocalizedResourceProviderUpdatedMessage(
        object                                  recipient,
        LocalizedResourceProviderUpdatedMessage message)
    {
        RefreshLocalizedContent();
    }
    #endregion
}