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

    private readonly Func<ResourceLoader> _resourceLoaderFactory;

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
    public IReadOnlyList<ResourceNamedValue<ElementTheme>> ApplicationThemes { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped audio sample rates, with localized keys.
    /// </summary>
    public IReadOnlyList<NamedValue<int>> AudioSampleRates { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped languages, with localized keys.
    /// </summary>
    public IReadOnlyList<NamedValue<CultureInfo>> Languages { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped noise presets, with localized keys.
    /// </summary>
    /// <remarks>
    /// Value type of <see cref="string"/> is used for now, until a type for noise preset is implemented.
    /// </remarks>
    public IReadOnlyList<ResourceNamedValue<string>> NoisePresets { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of mapped system backdrops, with localized keys.
    /// </summary>
    public IReadOnlyList<ResourceNamedValue<SystemBackdrop>> SystemBackdrops { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _resourceLoader = null!;

        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _resourceLoaderFactory = () => _resourceLoader;

        ApplicationThemeChanged = delegate { };
        SystemBackdropChanged = delegate { };

        AudioSampleRates = [
            new NamedValue<int>(48000, GetDisplayableAudioSampleRateString),
            new NamedValue<int>(44100, GetDisplayableAudioSampleRateString)
        ];

        ApplicationThemes = [
            new(ElementTheme.Default, "Common/System", _resourceLoaderFactory),
            new(ElementTheme.Dark, "Common/Dark", _resourceLoaderFactory),
            new(ElementTheme.Light, "Common/Light", _resourceLoaderFactory)
        ];

        Languages = ApplicationLanguages.ManifestLanguages
            .Select(value =>
            {
                CultureInfo cultureInfo = new(value);

                return new NamedValue<CultureInfo>(cultureInfo, cultureInfo.NativeName);
            })
            .ToList();

        NoisePresets = [
            new(string.Empty, "Common/Blue", _resourceLoaderFactory),
            new(string.Empty, "Common/Brownian", _resourceLoaderFactory),
            new(string.Empty, "Common/White", _resourceLoaderFactory),
        ];

        SystemBackdrops = [
            new(
                new MicaBackdrop(),
                "SystemBackdrop/Mica",
                _resourceLoaderFactory
            ),
            new(
                new MicaBackdrop() { Kind = MicaKind.BaseAlt },
                "SystemBackdrop/MicaAlt",
                _resourceLoaderFactory
            ),
            new(
                new DesktopAcrylicBackdrop(),
                "SystemBackdrop/Acrylic",
                _resourceLoaderFactory
            ),
            new(
                null!,
                "Common/None",
                _resourceLoaderFactory
            ),
        ];

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

        SystemBackdropSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Header");
        SystemBackdropSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Description");

        GeneralSettingsSectionHeader.Text = _resourceLoader.GetString("Common/General");

        AutoplayOnLaunchSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Header");
        AutoplayOnLaunchSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Description");

        AutoplayOnLaunchToggleSwitch.OnContent  = _resourceLoader.GetString("Common/On");
        AutoplayOnLaunchToggleSwitch.OffContent = _resourceLoader.GetString("Common/Off");

        DefaultNoisePresetSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Header");
        DefaultNoisePresetSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Description");

        LanguageSettingsCard.Header      = _resourceLoader.GetString("Common/Language");
        LanguageSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/Language/Description");

        SoundSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Sound");

        AudioSampleRateSettingsCard.Header      = _resourceLoader.GetString("Common/SampleRate");
        AudioSampleRateSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Sound/SampleRate/Description");

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

    private string GetDisplayableAudioSampleRateString(int value)
    {
        return $"{value} {_resourceLoader.GetString("Units/Hertz/Short")}";
    }

    private void RegisterEventHandlers()
    {
        Closed += SettingsWindow_Closed;
    }

    private void ApplicationThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not ResourceNamedValue<ElementTheme> value)
        {
            return;
        }

        ApplicationThemeChanged.Invoke(this, value.Value);
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not NamedValue<CultureInfo> value)
        {
            return;
        }

        ApplicationLanguages.PrimaryLanguageOverride = value.Value.Name;

        RefreshLocalizedContent();
    }

    private void SystemBackdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not ResourceNamedValue<SystemBackdrop> value)
        {
            return;
        }

        SystemBackdropChanged?.Invoke(this, value.Value);
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