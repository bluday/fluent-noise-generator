namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window, IApplicationResourceAware
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly Func<ResourceLoader> _resourceLoaderFactory;

    /// <summary>
    /// The minimum width unscaled in pixels.
    /// </summary>
    public const int MINIMUM_HEIGHT = 1400;

    /// <summary>
    /// The minimum width unscaled in pixels.
    /// </summary>
    public const int MINIMUM_WIDTH = 1400;

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

    ResourceLoader IApplicationResourceAware.ResourceLoader => _resourceLoader;

    /// <summary>
    /// Triggers when a new application theme gets selected.
    /// </summary>
    public event EventHandler<ElementTheme> ApplicationThemeChanged;

    /// <summary>
    /// Triggers when a new system backdrop gets selected.
    /// </summary>
    public event EventHandler<SystemBackdrop?> SystemBackdropChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    /// <param name="resourceLoaderFactory">
    /// The <see cref="ResourceLoader"/> factory.
    /// </param>
    public SettingsWindow(Func<ResourceLoader> resourceLoaderFactory)
    {
        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _resourceLoaderFactory = resourceLoaderFactory;

        _resourceLoader = resourceLoaderFactory();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        ApplicationThemeChanged = (sender, e) => { };
        SystemBackdropChanged   = (sender, e) => { };

        LocalizedApplicationThemes = null!;
        LocalizedAudioSampleRates  = null!;
        LocalizedLanguages         = null!;
        LocalizedNoisePresets      = null!;
        LocalizedSystemBackdrops   = null!;

        InitializeComponent();

        PopulateComboBoxControlsWithLocalizedValues();

        RegisterEventHandlers();

        ConfigureTitleBar();

        ConfigureWindow();

        ApplyLocalizedContent();
    }

    private void ApplyLocalizedContent()
    {
        string displayName = Package.Current.DisplayName;

        string settingsText = this.GetLocalizedString("Common/Settings");

        TitleBar.Title = displayName;

        Title = settingsText;

        HeaderTextBlock.Text = settingsText;

        AppearanceSettingsSectionHeader.Header = this.GetLocalizedString("Common/Appearance");

        AlwaysOnTopSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/Appearance/AlwaysOnTop/Header");
        AlwaysOnTopSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Appearance/AlwaysOnTop/Description");

        ApplicationThemeSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/Appearance/ApplicationTheme/Header");
        ApplicationThemeSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Appearance/ApplicationTheme/Description");

        ApplicationThemeComboBox.ItemsSource = LocalizedApplicationThemes.Keys;

        SystemBackdropSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/Appearance/SystemBackdrop/Header");
        SystemBackdropSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Appearance/SystemBackdrop/Description");

        SystemBackdropComboBox.ItemsSource = LocalizedSystemBackdrops.Keys;

        GeneralSettingsSectionHeader.Header = this.GetLocalizedString("Common/General");

        AutoplayOnLaunchSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/General/AutoplayOnLaunch/Header");
        AutoplayOnLaunchSettingsCard.Description = this.GetLocalizedString("SettingsWindow/General/AutoplayOnLaunch/Description");

        AutoplayOnLaunchToggleSwitch.OnContent  = this.GetLocalizedString("Common/On");
        AutoplayOnLaunchToggleSwitch.OffContent = this.GetLocalizedString("Common/Off");

        DefaultNoisePresetSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/General/DefaultNoisePreset/Header");
        DefaultNoisePresetSettingsCard.Description = this.GetLocalizedString("SettingsWindow/General/DefaultNoisePreset/Description");

        DefaultNoisePresetComboBox.ItemsSource = LocalizedNoisePresets.Keys;

        LanguageSettingsCard.Header      = this.GetLocalizedString("Common/Language");
        LanguageSettingsCard.Description = this.GetLocalizedString("SettingsWindow/General/Language/Description");

        LanguageComboBox.ItemsSource = LocalizedLanguages.Keys;

        SoundSettingsSectionHeader.Header = this.GetLocalizedString("Common/Sound");

        AudioSampleRateSettingsCard.Header      = this.GetLocalizedString("Common/SampleRate");
        AudioSampleRateSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Sound/SampleRate/Description");

        AudioSampleRateComboBox.ItemsSource = LocalizedAudioSampleRates.Keys;

        AboutSettingsSectionHeader.Header = this.GetLocalizedString("Common/About");

        AboutSettingsExpander.Header      = displayName;
        AboutSettingsExpander.Description = this.GetLocalizedString("General/CopyrightText");

        ApplicationVersionTextBlock.Text = GetApplicationVersionText();

        SessionIdentifierSettingsCard.Header = string.Format(
            format: this.GetLocalizedString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        RepositoryOnGitHubHyperlinkButton.Content     = this.GetLocalizedString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        SendFeedbackHyperlinkButton.Content           = this.GetLocalizedString("SettingsWindow/HyperlinkButtons/SendFeedback");
        RepositoryOnGitHubHyperlinkButton.NavigateUri = this.GetUriFromLocalizedString("General/GitHubRepositoryUrl");
        SendFeedbackHyperlinkButton.NavigateUri       = this.GetUriFromLocalizedString("General/SendFeedbackUrl");
    }

    private void ConfigureTitleBar()
    {
        string iconPath = this.GetLocalizedString("Assets/IconPaths/64x64");

        _appWindow.SetIcon(iconPath);

        TitleBar.Icon = $"ms-appx:///{iconPath}";

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TitleBar);
    }

    private void ConfigureWindow()
    {
        int scaledMinimumWidth  = (int)(MINIMUM_WIDTH  / _dpiScaleFactor);
        int scaledMinimumHeight = (int)(MINIMUM_HEIGHT / _dpiScaleFactor);

        _overlappedPresenter.PreferredMinimumWidth  = scaledMinimumWidth;
        _overlappedPresenter.PreferredMinimumHeight = scaledMinimumHeight;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(scaledMinimumWidth, scaledMinimumHeight);
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
            this.GetLocalizedString("Common/Blue"),
            this.GetLocalizedString("Common/Brownian"),
            this.GetLocalizedString("Common/White")
        ];

        string shortHertzText = this.GetLocalizedString("Units/Hertz/Short");

        LocalizedAudioSampleRates = audioSampleRates.ToDictionary(
            keySelector:     value => $"{value} {shortHertzText}",
            elementSelector: value => value
        );

        LocalizedApplicationThemes = new Dictionary<string, ElementTheme>
        {
            [this.GetLocalizedString("Common/System")] = ElementTheme.Default,
            [this.GetLocalizedString("Common/Dark")]   = ElementTheme.Dark,
            [this.GetLocalizedString("Common/Light")]  = ElementTheme.Light
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
            [this.GetLocalizedString("SystemBackdrop/Mica")]    = new MicaBackdrop(),
            [this.GetLocalizedString("SystemBackdrop/MicaAlt")] = new MicaBackdrop() { Kind = MicaKind.BaseAlt },
            [this.GetLocalizedString("SystemBackdrop/Acrylic")] = new DesktopAcrylicBackdrop(),
            [this.GetLocalizedString("Common/None")]            = null!,
        };
    }

    private void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        PopulateComboBoxControlsWithLocalizedValues();

        ApplyLocalizedContent();
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

        if (ApplicationLanguages.PrimaryLanguageOverride == cultureInfo.Name)
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

        SystemBackdrop = systemBackdrop;

        SystemBackdropChanged?.Invoke(this, systemBackdrop);
    }

    private void SettingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
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