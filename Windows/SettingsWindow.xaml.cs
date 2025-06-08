namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private double _dpiScaleFactor;
    
    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        InitializeComponent();

        Configure();
    }

    private void Configure()
    {
        SetTitleBar(TitleBarControl);

        ExtendsContentIntoTitleBar = true;

        int size = (int)(600 * _dpiScaleFactor);

        _overlappedPresenter.PreferredMinimumHeight = size;
        _overlappedPresenter.PreferredMinimumWidth  = size;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(width: 600, height: 600, _dpiScaleFactor);

        // TODO: Center the window.
    }
}