namespace BluDay.FluentNoiseRemover;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly nint _windowHandle;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _appWindow = AppWindow;
        
        _overlappedPresenter = OverlappedPresenter.Create();

        _windowHandle = WindowNative.GetWindowHandle(this);

        InitializeComponent();

        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        ResizeUsingScaleFactorValue(220, 150);

        _appWindow.Move(GetCenterPositionForWindow());

        _appWindow.SetPresenter(_overlappedPresenter);

        SetTitleBar(AppTitleBar);

        ExtendsContentIntoTitleBar = true;
    }

    private void ResizeUsingScaleFactorValue(int width, int height)
    {
        double scaleFactor = GetScaleFactorForWindow();

        _appWindow.Resize(new SizeInt32(
            (int)(width  * scaleFactor),
            (int)(height * scaleFactor)
        ));
    }

    private PointInt32 GetCenterPositionForWindow()
    {
        return GetCenterPositionForWindow(
            DisplayArea.GetFromWindowId(_appWindow.Id, DisplayAreaFallback.Primary)
        );
    }

    private PointInt32 GetCenterPositionForWindow(DisplayArea displayArea)
    {
        RectInt32 displayWorkArea = displayArea.WorkArea;

        SizeInt32 windowSize = _appWindow.Size;

        return new(
            (displayWorkArea.Width  - windowSize.Width)  / 2,
            (displayWorkArea.Height - windowSize.Height) / 2
        );
    }

    private double GetScaleFactorForWindow()
    {
        return GetDpiForWindow(_windowHandle) / 96.0;
    }

    #region Event handlers
    private void AppTitleBar_CloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }
    #endregion

    #region External methods
    [DllImport("user32.dll")]
    public static extern int GetDpiForWindow(nint hwnd);
    #endregion
}