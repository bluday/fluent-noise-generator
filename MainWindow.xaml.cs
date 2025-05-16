namespace BluDay.FluentNoiseRemover;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly WindowManager _windowManager;

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

        _windowManager = new WindowManager(this);

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

        _windowManager.ResizeUsingScaleFactorValue(220, 150);

        _appWindow.Move(_windowManager.GetCenterPositionForWindow());

        _appWindow.SetPresenter(_overlappedPresenter);

        SetTitleBar(AppTitleBar);

        ExtendsContentIntoTitleBar = true;
    }

    #region Event handlers
    private void AppTitleBar_CloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }
    #endregion
}