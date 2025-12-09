using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Extensions;
using FluentNoiseGenerator.Services;
using Microsoft.UI;
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
using Windows.UI;

namespace FluentNoiseGenerator.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    #region Constantsd
    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_HEIGHT = 1000;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 1000;
    #endregion

    #region Fields
    private ResourceLoader? _resourceLoader;

    private bool _hasClosed;

    private readonly OverlappedPresenter _overlappedPresenter = OverlappedPresenter.Create();

    private readonly LanguageService _languageService;

    private readonly ResourceService _resourceService;

    private readonly ThemeService _themeService;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a displayable application version text.
    /// </summary>
    public string ApplicationVersionText
    {
        get
        {
            PackageVersion version = Package.Current.Id.Version;

            return $"{version.Major}.{version.Minor}";
        }
    }

    /// <summary>
    /// Gets a read-only collection of available, mapped application themes, with localized
    /// keys.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<ElementTheme>> AvailableApplicationThemes { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped audio sample rates, with localized
    /// keys.
    /// </summary>
    public IReadOnlyCollection<NamedValue<int>> AvailableAudioSampleRates { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped languages, with localized keys.
    /// </summary>
    public IReadOnlyCollection<NamedValue<CultureInfo>> AvailableLanguages { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped noise presets, with localized keys.
    /// </summary>
    /// <remarks>
    /// Value type of <see cref="string"/> is used for now, until a type for noise preset is
    /// implemented.
    /// </remarks>
    public IReadOnlyCollection<ResourceNamedValue<string>> AvailableNoisePresets { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped system backdrops, with localized keys.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<SystemBackdrop>> AvailableSystemBackdrops { get; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class using default
    /// paramaeter values.
    /// </summary>
    /// <remarks>
    /// This parameterless constructor is required for design-time support in Visual Studio and
    /// to play nice with the XAML designer.
    /// </remarks>
    public SettingsWindow() : this(null!, null!, null!) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class using the specified
    /// resource service instance.
    /// </summary>
    /// <param name="languageService">
    /// The language service for retrieving and updating application language info.
    /// </param>
    /// <param name="resourceService">
    /// The resource service for retrieving application resources.
    /// </param>
    /// <param name="themeService">
    /// The theme service for retrieving and updating the current application theme.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    public SettingsWindow(
        LanguageService languageService,
        ResourceService resourceService,
        ThemeService    themeService)
    {
        _languageService = languageService;
        _resourceService = resourceService;
        _themeService    = themeService;

        ResourceLoader GetResourceLoaderFactory() => _resourceLoader!;

        AvailableAudioSampleRates = [
            new NamedValue<int>(AudioSampleRates.Rate48000Hz, GetDisplayableAudioSampleRateString),
            new NamedValue<int>(AudioSampleRates.Rate44100Hz, GetDisplayableAudioSampleRateString)
        ];

        AvailableLanguages = [
            ..ApplicationLanguages.ManifestLanguages.Select(
                language => new NamedValue<CultureInfo>(
                    value:     new(language),
                    formatter: language => language.NativeName
                )
            )
        ];

        AvailableApplicationThemes = [
            new ResourceNamedValue<ElementTheme>(ElementTheme.Default, "Common/System", GetResourceLoaderFactory),
            new ResourceNamedValue<ElementTheme>(ElementTheme.Dark, "Common/Dark", GetResourceLoaderFactory),
            new ResourceNamedValue<ElementTheme>(ElementTheme.Light, "Common/Light", GetResourceLoaderFactory)
        ];

        AvailableNoisePresets = [
            new ResourceNamedValue<string>(null, "Common/Blue", GetResourceLoaderFactory),
            new ResourceNamedValue<string>(null, "Common/Brownian", GetResourceLoaderFactory),
            new ResourceNamedValue<string>(null, "Common/White", GetResourceLoaderFactory)
        ];

        AvailableSystemBackdrops = [
            new ResourceNamedValue<SystemBackdrop>(
                new MicaBackdrop(),
                "SystemBackdrop/Mica",
                GetResourceLoaderFactory
            ),
            new ResourceNamedValue<SystemBackdrop>(
                new MicaBackdrop { Kind = MicaKind.BaseAlt },
                "SystemBackdrop/MicaAlt",
                GetResourceLoaderFactory
            ),
            new ResourceNamedValue<SystemBackdrop>(
                new DesktopAcrylicBackdrop(),
                "SystemBackdrop/Acrylic",
                GetResourceLoaderFactory
            ),
            new ResourceNamedValue<SystemBackdrop>(
                null,
                "Common/None",
                GetResourceLoaderFactory
            )
        ];

        RegisterEventHanders();
        ConfigureAppWindow();
        InitializeComponent();
        ConfigureTitleBar();
    }
    #endregion

    #region Event handlers
    private void _languageService_CurrentCultureInfoChanged(CultureInfo? value)
    {
        RefreshLocalizedContent();
    }

    private void _themeService_CurrentThemeChanged(ElementTheme value)
    {
        layoutRoot.RequestedTheme = value;

        RefreshBackgroundBrush();
        RefreshTitleBarColors();
    }

    private void _themeService_CurrentSystemBackdropChanged(SystemBackdrop? value)
    {
        SystemBackdrop = value;

        RefreshBackgroundBrush();
        RefreshTitleBarColors();
    }

    private void availableApplicationThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is ResourceNamedValue<ElementTheme> namedValue)
        {
            _themeService.CurrentTheme = namedValue.Value;
        }
    }

    private void availableLanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is NamedValue<CultureInfo> namedValue)
        {
            _languageService.CurrentCultureInfo = namedValue.Value;
        }
    }

    private void availableSystemBackdropsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is ResourceNamedValue<SystemBackdrop> namedValue)
        {
            _themeService.CurrentSystemBackdrop = namedValue.Value;
        }
    }

    private void SettingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }
    #endregion

    #region Methods
    private void ApplyLocalizedContent()
    {
        if (_resourceLoader is null) return;

        string displayName  = _resourceLoader.GetString("General/AppDisplayName");
        string settingsText = _resourceLoader.GetString("Common/Settings");

        Title = settingsText;

        settingsTitleBar.Title = displayName;

        headerTextBlock.Text = settingsText;

        appearanceSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Appearance");

        alwaysOnTopSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Header");
        alwaysOnTopSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Description");

        applicationThemeSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Header");
        applicationThemeSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Description");

        systemBackdropSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Header");
        systemBackdropSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Description");

        generalSettingsSectionHeader.Text = _resourceLoader.GetString("Common/General");

        autoplayOnLaunchSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Header");
        autoplayOnLaunchSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Description");

        autoplayOnLaunchToggleSwitch.OnContent  = _resourceLoader.GetString("Common/On");
        autoplayOnLaunchToggleSwitch.OffContent = _resourceLoader.GetString("Common/Off");

        defaultNoisePresetSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Header");
        defaultNoisePresetSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Description");

        languageSettingsCard.Header      = _resourceLoader.GetString("Common/Language");
        languageSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/Language/Description");

        soundSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Sound");

        audioSampleRateSettingsCard.Header      = _resourceLoader.GetString("Common/SampleRate");
        audioSampleRateSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Sound/SampleRate/Description");

        aboutSettingsSectionHeader.Text = _resourceLoader.GetString("Common/About");

        aboutSettingsExpander.Header      = displayName;
        aboutSettingsExpander.Description = _resourceLoader.GetString("General/CopyrightText");

        sessionIdentifierSettingsCard.Header = string.Format(
            format: _resourceLoader.GetString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        repositoryOnGitHubHyperlinkButton.Content     = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        sendFeedbackHyperlinkButton.Content           = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/SendFeedback");
        repositoryOnGitHubHyperlinkButton.NavigateUri = new Uri(_resourceLoader.GetString("General/GitHubRepositoryUrl"));
        sendFeedbackHyperlinkButton.NavigateUri       = new Uri(_resourceLoader.GetString("General/SendFeedbackUrl"));
    }

    private void ConfigureAppWindow()
    {
        _overlappedPresenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        _overlappedPresenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        AppWindow.SetPresenter(_overlappedPresenter);
        AppWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
    }

    private void ConfigureTitleBar()
    {
        string iconPath = _resourceService.AppIconPath;

        AppWindow.SetIcon(iconPath);

        settingsTitleBar.Icon = iconPath;

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(settingsTitleBar);
    }

    private string GetDisplayableAudioSampleRateString(int value)
    {
        if (_resourceLoader is null)
        {
            return value.ToString();
        }

        return $"{value} {_resourceLoader.GetString("Units/Hertz/Short")}";
    }

    private void RefreshBackgroundBrush()
    {
        if (SystemBackdrop is not null)
        {
            layoutRoot.Background = null;

            return;
        }

        layoutRoot.Background = new SolidColorBrush(
            layoutRoot.RequestedTheme is ElementTheme.Light
                ? Colors.White
                : Colors.Black
        );
    }

    private void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        ApplyLocalizedContent();
    }

    private void RefreshTitleBarColors()
    {
        AppWindowTitleBar titleBar = AppWindow.TitleBar;

        Color buttonForegroundColor;
        Color hoverPressedBackgroundColor;

        titleBar.ButtonBackgroundColor         = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (layoutRoot.RequestedTheme is ElementTheme.Light)
        {
            hoverPressedBackgroundColor = Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD);

            buttonForegroundColor = Colors.Black;
        }
        else
        {
            hoverPressedBackgroundColor = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);

            buttonForegroundColor = Colors.White;
        }

        titleBar.ButtonHoverBackgroundColor   = hoverPressedBackgroundColor;
        titleBar.ButtonPressedBackgroundColor = hoverPressedBackgroundColor;

        titleBar.ButtonForegroundColor        = buttonForegroundColor;
        titleBar.ButtonHoverForegroundColor   = buttonForegroundColor;
        titleBar.ButtonPressedForegroundColor = buttonForegroundColor;
    }

    private void RegisterEventHanders()
    {
        _languageService?.CurrentCultureInfoChanged += _languageService_CurrentCultureInfoChanged;

        _themeService?.CurrentSystemBackdropChanged += _themeService_CurrentSystemBackdropChanged;
        _themeService?.CurrentThemeChanged          += _themeService_CurrentThemeChanged;

        Closed += SettingsWindow_Closed;
    }

    private void UnregisterEventHanders()
    {
        _languageService?.CurrentCultureInfoChanged -= _languageService_CurrentCultureInfoChanged;

        _themeService?.CurrentSystemBackdropChanged -= _themeService_CurrentSystemBackdropChanged;
        _themeService?.CurrentThemeChanged          -= _themeService_CurrentThemeChanged;

        Closed -= SettingsWindow_Closed;
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
    #endregion
}