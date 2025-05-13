namespace BluDay.FluentNoiseRemover;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly OverlappedPresenter _presenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _presenter = OverlappedPresenter.Create();

        InitializeComponent();

        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        AppWindow appWindow = AppWindow;

        _presenter.IsAlwaysOnTop = true;
        _presenter.IsMaximizable = false;
        _presenter.IsMinimizable = false;
        _presenter.IsResizable   = false;

        _presenter.SetBorderAndTitleBar(true, false);

        appWindow.SetPresenter(_presenter);
        appWindow.ResizeClient(new SizeInt32(300, 200));

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(AppTitleBar);
    }

    private void AppTitleBar_CloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}