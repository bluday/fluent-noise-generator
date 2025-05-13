namespace BluDay.FluentNoiseRemover;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly AppWindow _appWindow;

    private readonly CompactOverlayPresenter _presenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _appWindow = AppWindow;

        _presenter = CompactOverlayPresenter.Create();

        InitializeComponent();

        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        _presenter.InitialSize = CompactOverlaySize.Small;

        _appWindow.SetPresenter(_presenter);

        _appWindow.ResizeClient(new SizeInt32(300, 200));

        SetTitleBar(AppTitleBar);

        ExtendsContentIntoTitleBar = true;
    }
}