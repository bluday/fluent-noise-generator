namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private double _dpiScaleFactor;
    
    private readonly InputNonClientPointerSource _nonClientPointerSource;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _appWindow = AppWindow;

        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(_appWindow.Id);
        
        _overlappedPresenter = OverlappedPresenter.Create();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        InitializeComponent();

        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        SetTitleBar(TopActionBarControl);

        ExtendsContentIntoTitleBar = true;

        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(new SizeInt32(
            (int)(200 * _dpiScaleFactor),
            (int)(120 * _dpiScaleFactor)
        ));

        // TODO: Center the window.
    }

    private void TopActionBarControl_CloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void TopActionBarControl_SettingsButtonClick(object sender, EventArgs e)
    {
        // TODO: Open settings window.
    }

    private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        _nonClientPointerSource.ClearAllRegionRects();

        /**
         * Note:
         * 
         * Region kind for drag must be set to `Caption` in order to set a drag
         * region for the title bar control. Really bizarre that one can't hide
         * the native close chrome button without making external calls to the
         * Win32 API. This is a temporary workaround.
         */
        _nonClientPointerSource.SetRegionRects(
            NonClientRegionKind.Caption,
            [TopActionBarControl.GetBoundingBox(_dpiScaleFactor)]
        );

        _nonClientPointerSource.SetRegionRects(
            NonClientRegionKind.Close,
            [TopActionBarControl.GetBoundingRectForCloseButton(_dpiScaleFactor)]
        );

        _nonClientPointerSource.SetRegionRects(
            NonClientRegionKind.Passthrough,
            [TopActionBarControl.GetBoundingRectForSettingsButton(_dpiScaleFactor)]
        );
    }
}