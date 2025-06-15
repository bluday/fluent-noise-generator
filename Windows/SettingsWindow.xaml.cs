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

    private void RegisterEventHandlers()
    {
        Closed += SettingsWindow_Closed;
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