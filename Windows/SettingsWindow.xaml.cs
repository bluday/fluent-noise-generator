namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// The minimum width unscaled in pixels.
    /// </summary>
    public const int MINIMUM_HEIGHT = 820;

    /// <summary>
    /// The minimum width unscaled in pixels.
    /// </summary>
    public const int MINIMUM_WIDTH = 800;

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
    public IReadOnlyDictionary<string, WindowsSystemBackdrop> LocalizedSystemBackdrops { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// The event to invoke when a new application theme gets selected.
    /// </summary>
    public event EventHandler<ElementTheme> ApplicationThemeChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _resourceLoader = new ResourceLoader();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        ApplicationThemeChanged = (sender, theme) => { };

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
        HeaderTextBlock.Text = GetLocalizedString("Common/Settings");

        AudioSettingsSectionHeader.Header = GetLocalizedString("Common/Audio");

        AudioSampleRateSettingsCard.Description = GetLocalizedString("SettingsWindow/Audio/SampleRate/Description");
        AudioSampleRateSettingsCard.Header      = GetLocalizedString("Common/SampleRate");

        AudioSampleRateComboBox.ItemsSource = LocalizedAudioSampleRates.Keys;

        GeneralSettingsSectionHeader.Header = GetLocalizedString("Common/General");

        DefaultNoisePresetSettingsCard.Description = GetLocalizedString("SettingsWindow/General/DefaultNoisePreset/Description");
        DefaultNoisePresetSettingsCard.Header      = GetLocalizedString("SettingsWindow/General/DefaultNoisePreset/Header");

        DefaultNoisePresetComboBox.ItemsSource = LocalizedNoisePresets.Keys;

        LanguageSettingsCard.Description = GetLocalizedString("SettingsWindow/General/Language/Description");
        LanguageSettingsCard.Header      = GetLocalizedString("Common/Language");

        LanguageComboBox.ItemsSource = LocalizedLanguages.Keys;

        InterfaceSettingsSectionHeader.Header = GetLocalizedString("Common/Interface");

        ApplicationThemeSettingsCard.Description = GetLocalizedString("SettingsWindow/Interface/ApplicationTheme/Description");
        ApplicationThemeSettingsCard.Header      = GetLocalizedString("SettingsWindow/Interface/ApplicationTheme/Header");

        ApplicationThemeComboBox.ItemsSource = LocalizedApplicationThemes.Keys;

        SystemBackdropSettingsCard.Description = GetLocalizedString("SettingsWindow/Interface/SystemBackdrop/Description");
        SystemBackdropSettingsCard.Header      = GetLocalizedString("SettingsWindow/Interface/SystemBackdrop/Header");

        SystemBackdropComboBox.ItemsSource = LocalizedSystemBackdrops.Keys;

        AboutSettingsSectionHeader.Header = GetLocalizedString("Common/About");

        ApplicationInfoSettingsExpander.Description = GetLocalizedString("General/CopyrightText");
        ApplicationInfoSettingsExpander.Header      = GetLocalizedString("General/AppDisplayName");

        ApplicationInfoSettingsExpanderVersionTextBlock.Text = GetApplicationVersionText();

        SessionIdentifierSettingsCard.Header = string.Format(
            format: GetLocalizedString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        RepositoryOnGitHubHyperlinkButton.Content     = GetLocalizedString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        SendFeedbackHyperlinkButton.Content           = GetLocalizedString("SettingsWindow/HyperlinkButtons/SendFeedback");
        RepositoryOnGitHubHyperlinkButton.NavigateUri = GetUriFromLocalizedString("General/GitHubRepositoryUrl");
        SendFeedbackHyperlinkButton.NavigateUri       = GetUriFromLocalizedString("General/SendFeedbackUrl");
    }

    private void ConfigureTitleBar()
    {
        string iconPath = GetLocalizedString("Assets/IconPaths/64x64");

        _appWindow.SetIcon(iconPath);

        TitleBar.Icon = $"ms-appx:///{iconPath}";

        TitleBar.Title = GetLocalizedString("General/AppDisplayName");

        ExtendsContentIntoTitleBar = true;

        Title = GetLocalizedString("Common/Settings");

        SetTitleBar(TitleBar);
    }

    private void ConfigureWindow()
    {
        int scaledMinimumWidth  = (int)(MINIMUM_WIDTH  * _dpiScaleFactor);
        int scaledMinimumHeight = (int)(MINIMUM_HEIGHT * _dpiScaleFactor);

        _overlappedPresenter.PreferredMinimumWidth  = scaledMinimumWidth;
        _overlappedPresenter.PreferredMinimumHeight = scaledMinimumHeight;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(scaledMinimumWidth, scaledMinimumHeight);
    }

    private string GetApplicationVersionText()
    {
        PackageVersion packageVersion = Package.Current.Id.Version;

        return $"{packageVersion.Major}.{packageVersion.Minor}";
    }

    private string GetLocalizedString(string key)
    {
        return _resourceLoader.GetString(key);
    }

    private Uri GetUriFromLocalizedString(string key)
    {
        return new Uri(_resourceLoader.GetString(key));
    }

    private void PopulateComboBoxControlsWithLocalizedValues()
    {
        List<int> audioSampleRates = [
            AudioSampleRates.Rate44100Hz,
            AudioSampleRates.Rate48000Hz
        ];

        List<string> noisePresets = [
            GetLocalizedString("Common/Blue"),
            GetLocalizedString("Common/Brownian"),
            GetLocalizedString("Common/White")
        ];

        string shortHertzText = GetLocalizedString("General/Unit/Hertz/Short");

        LocalizedAudioSampleRates = audioSampleRates.ToDictionary(
            keySelector:     value => $"{value} {shortHertzText}",
            elementSelector: value => value
        );

        LocalizedApplicationThemes = new Dictionary<string, ElementTheme>
        {
            [GetLocalizedString("Common/System")] = ElementTheme.Default,
            [GetLocalizedString("Common/Dark")]   = ElementTheme.Dark,
            [GetLocalizedString("Common/Light")]  = ElementTheme.Light
        };

        LocalizedLanguages = ApplicationLanguages.ManifestLanguages
            .Select(language => new CultureInfo(language))
            .ToDictionary(
                keySelector:     cultureInfo => cultureInfo.NativeName,
                elementSelector: cultureInfo => cultureInfo
            );

        LocalizedNoisePresets = noisePresets.ToDictionary(preset => preset);

        LocalizedSystemBackdrops = new Dictionary<string, WindowsSystemBackdrop>
        {
            [GetLocalizedString("Common/None")]            = WindowsSystemBackdrop.None,
            [GetLocalizedString("SystemBackdrop/Mica")]    = WindowsSystemBackdrop.Mica,
            [GetLocalizedString("SystemBackdrop/MicaAlt")] = WindowsSystemBackdrop.MicaAlt,
            [GetLocalizedString("SystemBackdrop/Acrylic")] = WindowsSystemBackdrop.Acrylic
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

        ApplicationThemeComboBox.SelectionChanged += ApplicationThemeComboBox_SelectionChanged;

        LanguageComboBox.SelectionChanged += LanguageComboBox_SelectionChanged;
    }

    private void ApplicationThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is ElementTheme theme)
        {
            ApplicationThemeChanged.Invoke(this, theme);
        }
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is CultureInfo cultureInfo)
        {
            ApplicationLanguages.PrimaryLanguageOverride = cultureInfo.Name;

            RefreshLocalizedContent();
        }
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