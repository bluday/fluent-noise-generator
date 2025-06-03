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

        InitializeComponent();

        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _appWindow.Resize(new Windows.Graphics.SizeInt32(
            (int)(220 * 1.5),
            (int)(150 * 1.5)
        ));

        _appWindow.Move(_appWindow.GetCenterPositionForWindow(DisplayAreaFallback.Primary));

        _appWindow.SetPresenter(_overlappedPresenter);

        SetTitleBar(AppTitleBar);

        ExtendsContentIntoTitleBar = true;
    }

    #region Event handlers
    private void AppTitleBar_CloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void AppTitleBar_SettingsButtonClick(object sender, EventArgs e)
    {
        // TODO: Open settings window.
    }
    #endregion
}