namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window, IApplicationResourceAware
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;
    
    private readonly InputNonClientPointerSource _nonClientPointerSource;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly Action _settingsWindowFactory;

    private readonly Func<ResourceLoader> _resourceLoaderFactory;

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying { get; private set; }

    ResourceLoader IApplicationResourceAware.ResourceLoader => _resourceLoader;

    /// <summary>
    /// Triggers when a new settings window has been created.
    /// </summary>
    public event EventHandler SettingsWindowCreated;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    /// <param name="settingsWindowFactory">
    /// The <see cref="SettingsWindow"/> factory.
    /// </param>
    /// <param name="resourceLoaderFactory">
    /// The <see cref="ResourceLoader"/> factory.
    /// </param>
    public MainWindow(Action settingsWindowFactory, Func<ResourceLoader> resourceLoaderFactory)
    {
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);

        _appWindow = AppWindow;

        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(_appWindow.Id);
        
        _overlappedPresenter = OverlappedPresenter.Create();

        _settingsWindowFactory = settingsWindowFactory;

        _resourceLoaderFactory = resourceLoaderFactory;

        _resourceLoader = resourceLoaderFactory();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        SettingsWindowCreated = (sender, e) => { };

        RegisterEventHandlers();

        InitializeComponent();

        ConfigureTitleBar();

        ConfigureWindow();
    }

    private void ConfigureTitleBar()
    {
        _appWindow.SetIcon(this.GetLocalizedString("Assets/IconPaths/64x64"));

        ExtendsContentIntoTitleBar = true;

        Title = Package.Current.DisplayName;

        SetTitleBar(TopActionBar);
    }

    private void ConfigureWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(hasBorder: true, hasTitleBar: false);

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(width: 450, height: 280, _dpiScaleFactor);
    }

    private void RegisterEventHandlers()
    {
        Closed += MainWindow_Closed;
    }

    private void TogglePlayback()
    {
        bool isPlaying = !IsPlaying;

        IsPlaying = isPlaying;

        PlaybackControlPanel.IsPlaying = isPlaying;
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
            region: NonClientRegionKind.Caption,
            rects:  [TopActionBar.GetBoundingBox(_dpiScaleFactor)]
        );

        _nonClientPointerSource.SetRegionRects(
            region: NonClientRegionKind.Passthrough,
            rects:  [
                TopActionBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
                TopActionBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
            ]
        );
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }

    private void LayoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void PlaybackControlPanel_PlaybackButtonClicked(object sender, EventArgs e)
    {
        TogglePlayback();
    }

    private void TopActionBar_CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void TopActionBar_SettingsButtonClicked(object sender, EventArgs e)
    {
        _settingsWindowFactory();
    }
}