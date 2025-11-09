using FluentNoiseGenerator.Common;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.ApplicationModel;

namespace FluentNoiseGenerator;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// The minimum width unscaled in pixels.
    /// </summary>
    public const int MINIMUM_HEIGHT = 1000;

    /// <summary>
    /// The minimum width unscaled in pixels.
    /// </summary>
    public const int MINIMUM_WIDTH = 1000;

    /// <summary>
    /// Triggers when a new application theme gets selected.
    /// </summary>
    public event EventHandler<ElementTheme> ApplicationThemeChanged;

    /// <summary>
    /// Triggers when a new system backdrop gets selected.
    /// </summary>
    public event EventHandler<SystemBackdrop?> SystemBackdropChanged;

    /// <summary>
    /// Gets a read-only dictionary of mapped application themes, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, ElementTheme> LocalizedApplicationThemes { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped audio sample rates, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, int> LocalizedAudioSampleRates { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped languages, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, CultureInfo> LocalizedLanguages { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped noise presets, with localized keys.
    /// </summary>
    /// <remarks>
    /// Value type of <see cref="string"/> is used for now, until a type for noise preset is implemented.
    /// </remarks>
    public IReadOnlyDictionary<string, string> LocalizedNoisePresets { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped system backdrops, with localized keys.
    /// </summary>
    public IReadOnlyDictionary<string, SystemBackdrop> LocalizedSystemBackdrops { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _resourceLoader = new ResourceLoader();

        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        ApplicationThemeChanged = (sender, e) => { };
        SystemBackdropChanged   = (sender, e) => { };

        LocalizedApplicationThemes = null!;
        LocalizedAudioSampleRates  = null!;
        LocalizedLanguages         = null!;
        LocalizedNoisePresets      = null!;
        LocalizedSystemBackdrops   = null!;

        InitializeComponent();

        RegisterEventHandlers();
    }

    public void ApplyLocalizedContent()
    {
        string displayName  = _resourceLoader.GetString("General/AppDisplayName");
        string settingsText = _resourceLoader.GetString("Common/Settings");

        Title = settingsText;

        TitleBar.Title = displayName;

        HeaderTextBlock.Text = settingsText;

        AppearanceSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Appearance");

        AlwaysOnTopSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Header");
        AlwaysOnTopSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Description");

        ApplicationThemeSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Header");
        ApplicationThemeSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Description");

        ApplicationThemeComboBox.ItemsSource = LocalizedApplicationThemes.Keys;

        SystemBackdropSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Header");
        SystemBackdropSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Description");

        SystemBackdropComboBox.ItemsSource = LocalizedSystemBackdrops.Keys;

        GeneralSettingsSectionHeader.Text = _resourceLoader.GetString("Common/General");

        AutoplayOnLaunchSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Header");
        AutoplayOnLaunchSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Description");

        AutoplayOnLaunchToggleSwitch.OnContent  = _resourceLoader.GetString("Common/On");
        AutoplayOnLaunchToggleSwitch.OffContent = _resourceLoader.GetString("Common/Off");

        DefaultNoisePresetSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Header");
        DefaultNoisePresetSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Description");

        DefaultNoisePresetComboBox.ItemsSource = LocalizedNoisePresets.Keys;

        LanguageSettingsCard.Header      = _resourceLoader.GetString("Common/Language");
        LanguageSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/Language/Description");

        LanguageComboBox.ItemsSource = LocalizedLanguages.Keys;

        SoundSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Sound");

        AudioSampleRateSettingsCard.Header      = _resourceLoader.GetString("Common/SampleRate");
        AudioSampleRateSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Sound/SampleRate/Description");

        AudioSampleRateComboBox.ItemsSource = LocalizedAudioSampleRates.Keys;

        AboutSettingsSectionHeader.Text = _resourceLoader.GetString("Common/About");

        AboutSettingsExpander.Header      = displayName;
        AboutSettingsExpander.Description = _resourceLoader.GetString("General/CopyrightText");

        ApplicationVersionTextBlock.Text = GetApplicationVersionText();

        SessionIdentifierSettingsCard.Header = string.Format(
            format: _resourceLoader.GetString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        RepositoryOnGitHubHyperlinkButton.Content     = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        SendFeedbackHyperlinkButton.Content           = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/SendFeedback");
        RepositoryOnGitHubHyperlinkButton.NavigateUri = new Uri(_resourceLoader.GetString("General/GitHubRepositoryUrl"));
        SendFeedbackHyperlinkButton.NavigateUri       = new Uri(_resourceLoader.GetString("General/SendFeedbackUrl"));
    }

    private string GetApplicationVersionText()
    {
        PackageVersion version = Package.Current.Id.Version;

        return $"{version.Major}.{version.Minor}";
    }

    private void PopulateComboBoxControlsWithLocalizedValues()
    {
        List<int> audioSampleRates = [
            AudioSampleRates.Rate48000Hz,
            AudioSampleRates.Rate44100Hz
        ];

        List<string> noisePresets = [
            _resourceLoader.GetString("Common/Blue"),
            _resourceLoader.GetString("Common/Brownian"),
            _resourceLoader.GetString("Common/White")
        ];

        string shortHertzText = _resourceLoader.GetString("Units/Hertz/Short");

        LocalizedAudioSampleRates = audioSampleRates.ToDictionary(
            keySelector:     value => $"{value} {shortHertzText}",
            elementSelector: value => value
        );

        LocalizedApplicationThemes = new Dictionary<string, ElementTheme>
        {
            [_resourceLoader.GetString("Common/System")] = ElementTheme.Default,
            [_resourceLoader.GetString("Common/Dark")]   = ElementTheme.Dark,
            [_resourceLoader.GetString("Common/Light")]  = ElementTheme.Light
        };

        LocalizedLanguages = ApplicationLanguages.ManifestLanguages
            .Select(language => new CultureInfo(language))
            .ToDictionary(
                keySelector:     cultureInfo => cultureInfo.NativeName,
                elementSelector: cultureInfo => cultureInfo
            );

        LocalizedNoisePresets = noisePresets.ToDictionary(preset => preset);

        LocalizedSystemBackdrops = new Dictionary<string, SystemBackdrop>
        {
            [_resourceLoader.GetString("SystemBackdrop/Mica")]    = new MicaBackdrop(),
            [_resourceLoader.GetString("SystemBackdrop/MicaAlt")] = new MicaBackdrop() { Kind = MicaKind.BaseAlt },
            [_resourceLoader.GetString("SystemBackdrop/Acrylic")] = new DesktopAcrylicBackdrop(),
            [_resourceLoader.GetString("Common/None")]            = null!,
        };
    }

    private void RegisterEventHandlers()
    {
        Closed += SettingsWindow_Closed;
    }

    private void ApplicationThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not string key)
        {
            return;
        }

        if (!LocalizedApplicationThemes.TryGetValue(key, out ElementTheme theme))
        {
            return;
        }

        ApplicationThemeChanged.Invoke(this, theme);
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not string key)
        {
            return;
        }

        if (!LocalizedLanguages.TryGetValue(key, out CultureInfo? cultureInfo))
        {
            return;
        }

        if (ApplicationLanguages.PrimaryLanguageOverride == cultureInfo!.Name)
        {
            return;
        }

        ApplicationLanguages.PrimaryLanguageOverride = cultureInfo.Name;

        RefreshLocalizedContent();
    }

    private void SystemBackdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not string key)
        {
            return;
        }

        SystemBackdrop? systemBackdrop = LocalizedSystemBackdrops.GetValueOrDefault(key);

        if (SystemBackdrop == systemBackdrop)
        {
            return;
        }

        SystemBackdropChanged?.Invoke(this, systemBackdrop);
    }

    private void SettingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }

    public void ConfigureAppWindow()
    {
        _overlappedPresenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        _overlappedPresenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(new Windows.Graphics.SizeInt32(MINIMUM_WIDTH, MINIMUM_HEIGHT));
    }

    public void ConfigureTitleBar()
    {
        _appWindow.SetIcon(App.IconPath);

        TitleBar.Icon = App.IconPath;

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TitleBar);
    }

    public void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        PopulateComboBoxControlsWithLocalizedValues();

        ApplyLocalizedContent();
    }

    /// <summary>
    /// Focuses the root element of the window.
    /// </summary>
    public void Focus()
    {
        Content?.Focus(FocusState.Programmatic);
    }

    /// <inheritdoc cref="OverlappedPresenter.Restore()"/>
    public void Restore()
    {
        _overlappedPresenter.Restore();
    }
}