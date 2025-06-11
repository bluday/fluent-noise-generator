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
    /// The minimum width, in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 700;
    
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

        DefaultNoisePresetSettingsCard.Description = GetLocalizedString("SettingsWindow/Audio/DefaultNoisePreset/Description");
        DefaultNoisePresetSettingsCard.Header      = GetLocalizedString("SettingsWindow/Audio/DefaultNoisePreset/Header");

        GeneralSettingsSectionHeader.Header = GetLocalizedString("Common/General");

        LanguageSettingsCard.Description = GetLocalizedString("SettingsWindow/General/Language/Description");
        LanguageSettingsCard.Header      = GetLocalizedString("Common/Language");

        InterfaceSettingsSectionHeader.Header = GetLocalizedString("Common/Interface");

        ApplicationThemeSettingsCard.Description = GetLocalizedString("SettingsWindow/Interface/ApplicationTheme/Description");
        ApplicationThemeSettingsCard.Header      = GetLocalizedString("SettingsWindow/Interface/ApplicationTheme/Header");
        SystemBackdropSettingsCard.Description   = GetLocalizedString("SettingsWindow/Interface/SystemBackdrop/Description");
        SystemBackdropSettingsCard.Header        = GetLocalizedString("SettingsWindow/Interface/SystemBackdrop/Header");

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
        int scaledMinimumWidth = (int)(MINIMUM_WIDTH * _dpiScaleFactor);

        _overlappedPresenter.PreferredMinimumHeight = scaledMinimumWidth;
        _overlappedPresenter.PreferredMinimumWidth  = scaledMinimumWidth;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(
            width:       MINIMUM_WIDTH,
            height:      MINIMUM_WIDTH,
            scaleFactor: _dpiScaleFactor
        );
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
        /*
        GetLocalizedString("Common/System"),
        GetLocalizedString("Common/Dark"),
        GetLocalizedString("Common/Light")
        */
        ApplicationThemeComboBox.ItemsSource = new List<ElementTheme>
        {
            ElementTheme.Default,
            ElementTheme.Dark,
            ElementTheme.Light
        };

        // CultureInfo.NativeName
        LanguageComboBox.ItemsSource = ApplicationLanguages.ManifestLanguages
            .Select(language => new CultureInfo(language))
            .ToList();

        NoisePresetComboBox.ItemsSource = new List<string>
        {
            GetLocalizedString("Common/Blue"),
            GetLocalizedString("Common/Brownian"),
            GetLocalizedString("Common/White")
        };

        /*
        GetLocalizedString("SystemBackdrop/Mica"),
        GetLocalizedString("SystemBackdrop/MicaAlt"),
        GetLocalizedString("SystemBackdrop/Acrylic"),
        GetLocalizedString("Common/None")
        */
        SystemBackdropComboBox.ItemsSource = new List<WindowsSystemBackdrop>
        {
            WindowsSystemBackdrop.Mica,
            WindowsSystemBackdrop.MicaAlternative,
            WindowsSystemBackdrop.Acrylic,
            WindowsSystemBackdrop.None
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