namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private SettingsWindow? _settingsWindow;

    private bool _hasClosed;

    private double _dpiScaleFactor;
    
    private readonly InputNonClientPointerSource _nonClientPointerSource;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly ResourceLoader _resourceLoader;

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _appWindow = AppWindow;

        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(_appWindow.Id);
        
        _overlappedPresenter = OverlappedPresenter.Create();

        _resourceLoader = new ResourceLoader();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        RegisterEventHandlers();

        InitializeComponent();

        ConfigureTitleBar();

        ConfigureWindow();
    }

    private void ConfigureTitleBar()
    {
        _appWindow.SetIcon(GetLocalizedString("Assets/IconPaths/64x64"));

        Title = _resourceLoader.GetString("General/AppDisplayName");

        SetTitleBar(TopActionBar);

        ExtendsContentIntoTitleBar = true;
    }

    private void ConfigureWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(
            hasBorder:   true,
            hasTitleBar: false
        );

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(
            width:       200,
            height:      120,
            scaleFactor: _dpiScaleFactor
        );
    }

    private void RegisterEventHandlers()
    {
        Closed += MainWindow_Closed;
    }

    private void ShowSettingsWindow()
    {
        if (_settingsWindow?.HasClosed is false)
        {
            _settingsWindow.Restore();
            _settingsWindow.Focus();

            return;
        }

        if (_settingsWindow is not null)
        {
            _settingsWindow.ApplicationThemeChanged -= _settingsWindow_ApplicationThemeChanged;
        }

        _settingsWindow = new SettingsWindow();

        _settingsWindow.ApplicationThemeChanged += _settingsWindow_ApplicationThemeChanged;

        _settingsWindow.Activate();
    }

    private void TogglePlayback()
    {
        bool isPlaying = !IsPlaying;

        IsPlaying = isPlaying;

        MainControlPanel.IsPlaying = isPlaying;
    }

    private string GetLocalizedString(string key)
    {
        return _resourceLoader.GetString(key);
    }

    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        if (Content is FrameworkElement frameworkElement)
        {
            frameworkElement.RequestedTheme = e;
        }

        if (_settingsWindow?.Content is FrameworkElement settingsFrameworkElement)
        {
            settingsFrameworkElement.RequestedTheme = e;
        }

        AppWindowTitleBar? settingsWindowTitleBar = _settingsWindow?.AppWindow.TitleBar;

        if (settingsWindowTitleBar is null)
        {
            return;
        }

        settingsWindowTitleBar.ButtonBackgroundColor         = Colors.Transparent;
        settingsWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        settingsWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (e is ElementTheme.Light)
        {
            settingsWindowTitleBar.ButtonHoverBackgroundColor    = Colors.DarkGray;
            settingsWindowTitleBar.ButtonPressedBackgroundColor  = Colors.LightGray;

            settingsWindowTitleBar.ButtonForegroundColor        = Colors.Black;
            settingsWindowTitleBar.ButtonHoverForegroundColor   = Colors.Black;
            settingsWindowTitleBar.ButtonPressedForegroundColor = Colors.Black;
        }
        else
        {
            settingsWindowTitleBar.ButtonHoverBackgroundColor    = Colors.LightGray;
            settingsWindowTitleBar.ButtonPressedBackgroundColor  = Colors.Gray;

            settingsWindowTitleBar.ButtonForegroundColor        = Colors.White;
            settingsWindowTitleBar.ButtonHoverForegroundColor   = Colors.White;
            settingsWindowTitleBar.ButtonPressedForegroundColor = Colors.White;
        }
    }

    private void UpdateNonClientInputRegions()
    {
        /**
         * Required to prevent the window from throwing a <see cref="ObjectDisposedException"/>.
         * Operations on the pointer source are not allowed once the window has been closed.
         */
        if (_hasClosed) return;

        _nonClientPointerSource.ClearAllRegionRects();

        /**
         * Region kind for drag must be set to `Caption` in order to set a drag region for the
         * title bar control. Really bizarre that one can't hide the native close chrome button
         * without making external calls to the Win32 API.
         * 
         * I am lazy and this is the easiest way of specifying drag regions after setting title
         * bar to false using <see cref="OverlappedPresenter.SetBorderAndTitleBar(bool, bool)"/>.
         */
        _nonClientPointerSource.SetRegionRects(
            NonClientRegionKind.Caption,
            [TopActionBar.GetBoundingBox(_dpiScaleFactor)]
        );

        _nonClientPointerSource.SetRegionRects(
            NonClientRegionKind.Passthrough,
            [
                TopActionBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
                TopActionBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
            ]
        );
    }

    private void MainControlPanel_PlaybackButtonClicked(object sender, EventArgs e)
    {
        TogglePlayback();
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }

    private void LayoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void TopActionBar_CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void TopActionBar_SettingsButtonClicked(object sender, EventArgs e)
    {
        ShowSettingsWindow();
    }
}