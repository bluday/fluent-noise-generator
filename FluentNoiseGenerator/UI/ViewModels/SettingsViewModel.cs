using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.StringResources;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FluentNoiseGenerator.UI.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsViewModel : ObservableObject
{
    #region Fields
    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a read-only collection of available application themes.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<ElementTheme>> AvailableApplicationThemes { get; }

    /// <summary>
    /// Gets a read-only collection of available audio sample rates.
    /// </summary>
    public IReadOnlyCollection<NamedValue<int>> AvailableAudioSampleRates { get; }

    /// <summary>
    /// Gets a read-only collection of available languages.
    /// </summary>
    public IReadOnlyCollection<NamedValue<CultureInfo>> AvailableLanguages { get; }

    /// <summary>
    /// Gets a read-only collection of available noise presets.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<string>> AvailableNoisePresets { get; }

    /// <summary>
    /// Gets a read-only collection of available system backdrops.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<SystemBackdrop>> AvailableSystemBackdrops { get; }

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

        _messenger = messenger;

        string audioSampleRateUnit = localizedResourceProvider.Get("Units/Hertz/Short");

        string GetDisplayableAudioSampleRateValue(int value)
        {
            return $"{value} {audioSampleRateUnit}";
        }

        AvailableApplicationThemes = new List<ResourceNamedValue<ElementTheme>>
        {
            new(
                name:  new StringResource("Common/System", localizedResourceProvider),
                value: ElementTheme.Default
            ),
            new(
                name:  new StringResource("Common/Dark", localizedResourceProvider),
                value: ElementTheme.Dark
            ),
            new(
                name:  new StringResource("Common/Light", localizedResourceProvider),
                value: ElementTheme.Light
            )
        };

        AvailableAudioSampleRates = new List<NamedValue<int>>
        {
            new(
                value:     AudioSampleRates.Rate48000Hz,
                formatter: GetDisplayableAudioSampleRateValue
            ),
            new(
                value:     AudioSampleRates.Rate44100Hz,
                formatter: GetDisplayableAudioSampleRateValue
            )
        };

        AvailableLanguages = ApplicationLanguages.ManifestLanguages
            .Select(
                language => new NamedValue<CultureInfo>(
                    value:     new(language),
                    formatter: language => language.NativeName
                )
            )
            .ToList()
            .AsReadOnly();

        AvailableNoisePresets = new List<ResourceNamedValue<string>>
        {
            new(
                name:  new StringResource("Common/Blue", localizedResourceProvider),
                value: null
            ),
            new(
                name:  new StringResource("Common/Brownian", localizedResourceProvider),
                value: null
            ),
            new(
                name:  new StringResource("Common/White", localizedResourceProvider),
                value: null
            )
        };

        AvailableSystemBackdrops = new List<ResourceNamedValue<SystemBackdrop>>
        {
            new(
                name:  new StringResource("SystemBackdrop/Mica", localizedResourceProvider),
                value: new MicaBackdrop()
            ),
            new(
                name:  new StringResource("SystemBackdrop/MicaAlt", localizedResourceProvider),
                value: new MicaBackdrop { Kind = MicaKind.BaseAlt }
            ),
            new(
                name:  new StringResource("SystemBackdrop/Acrylic", localizedResourceProvider),
                value: new DesktopAcrylicBackdrop()
            ),
            new(
                name:  new StringResource("Common/None", localizedResourceProvider),
                value: null
            )
        };

        StringResources = stringResources;

        TitleBarIconPath = Path.Combine(AppContext.BaseDirectory, "Assets/Icon-64.ico");
    }
    #endregion
}