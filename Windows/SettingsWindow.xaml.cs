namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly ResourceLoader _resourceLoader;

    /// <summary>
    /// The minimum width, in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 700;

    /// <summary>
    /// Gets a read-only dictionary of localized strings for application themes.
    /// </summary>
    public IReadOnlyDictionary<AppTheme, string> LocalizedApplicationThemes { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of localized strings for noise presets.
    /// </summary>
    public IReadOnlyDictionary<string, string> LocalizedNoisePresets { get; private set; }

    /// <summary>
    /// Gets a read-only dictionary of localized strings for system backdrops.
    /// </summary>
    public IReadOnlyDictionary<WindowsSystemBackdrop, string> LocalizedSystemBackdrops { get; private set; }
    
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _resourceLoader = new ResourceLoader();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        LocalizedApplicationThemes = null!;
        LocalizedNoisePresets      = null!;
        LocalizedSystemBackdrops   = null!;

        PopulateMaps();

        RegisterEventHandlers();

        InitializeComponent();

        ConfigureTitleBar();

        ConfigureWindow();
    }

    private void ConfigureTitleBar()
    {
        string iconPath = _resourceLoader.GetString("AppIconPath/64x64");
        string title    = _resourceLoader.GetString("AppDisplayName");

        _appWindow.SetIcon(iconPath);

        TitleBarControl.Icon = new BitmapImage(new Uri(iconPath));

        TitleBarControl.Title = title;

        ExtendsContentIntoTitleBar = true;

        Title = title;

        SetTitleBar(TitleBarControl);
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

    private void PopulateMaps()
    {
        LocalizedApplicationThemes = new Dictionary<AppTheme, string>
        {
            [AppTheme.System] = _resourceLoader.GetString("SystemThemes/System"),
            [AppTheme.Dark]   = _resourceLoader.GetString("SystemThemes/Dark"),
            [AppTheme.Light]  = _resourceLoader.GetString("SystemThemes/Light")
        };

        LocalizedNoisePresets = new Dictionary<string, string>
        {
            ["Blue"]     = _resourceLoader.GetString("NoisePresets/Blue"),
            ["Brownian"] = _resourceLoader.GetString("NoisePresets/Brownian"),
            ["White"]    = _resourceLoader.GetString("NoisePresets/White")
        };

        LocalizedSystemBackdrops = new Dictionary<WindowsSystemBackdrop, string>
        {
            [WindowsSystemBackdrop.Mica]            = _resourceLoader.GetString("SystemBackdrops/Mica"),
            [WindowsSystemBackdrop.MicaAlternative] = _resourceLoader.GetString("SystemBackdrops/MicaAlternative"),
            [WindowsSystemBackdrop.Acrylic]         = _resourceLoader.GetString("SystemBackdrops/Acrylic"),
            [WindowsSystemBackdrop.None]            = _resourceLoader.GetString("SystemBackdrops/None")
        };
    }

    private void RegisterEventHandlers()
    {
        Closed += SettingsWindow_Closed;
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