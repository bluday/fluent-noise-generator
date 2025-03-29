namespace BluDay.FluentNoiseRemover;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private DisplayArea _displayArea;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _appWindow = AppWindow;

        _displayArea = DisplayArea.GetFromWindowId(_appWindow.Id, DisplayAreaFallback.Primary);

        _overlappedPresenter = (OverlappedPresenter)_appWindow.Presenter;

        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        _appWindow.ResizeClient(new SizeInt32(300, 300));

        InitializeComponent();

        AppTitleBar.SetWindow(this);
    }
}