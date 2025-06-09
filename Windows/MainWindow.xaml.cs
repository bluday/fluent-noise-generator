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
    /// Gets the command for hiding the window.
    /// </summary>
    public ICommand HideWindowCommand { get; }

    /// <summary>
    /// Gets the command for showing the settings window.
    /// </summary>
    public ICommand ShowSettingsWindowCommand { get; }

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

        HideWindowCommand = new RelayCommand(Close);

        ShowSettingsWindowCommand = new RelayCommand(ShowSettingsWindow);

        RegisterEventHandlers();

        InitializeComponent();

        ConfigureTitleBar();

        ConfigureWindow();
    }

    private void ConfigureTitleBar()
    {
        Title = _resourceLoader.GetString("AppDisplayName");

        _appWindow.SetIcon(_resourceLoader.GetString("AppIconPath/64x64"));

        SetTitleBar(TopActionBarControl);

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
        if (_settingsWindow is null || _settingsWindow.HasClosed)
        {
            _settingsWindow = new SettingsWindow();

            _settingsWindow.Activate();

            return;
        }

        _settingsWindow.Restore();
        _settingsWindow.Focus();
    }

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }

    private void RootGrid_LayoutUpdated(object sender, object e)
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
            [TopActionBarControl.GetBoundingBox(_dpiScaleFactor)]
        );

        _nonClientPointerSource.SetRegionRects(
            NonClientRegionKind.Passthrough,
            [
                TopActionBarControl.GetBoundingRectForSettingsButton(_dpiScaleFactor),
                TopActionBarControl.GetBoundingRectForCloseButton(_dpiScaleFactor)
            ]
        );
    }
}