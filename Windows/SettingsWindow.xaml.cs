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
        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        InitializeComponent();

        ConfigureWindow();
    }

    private void ConfigureWindow()
    {
        SetTitleBar(TitleBarControl);

        ExtendsContentIntoTitleBar = true;

        _overlappedPresenter.IsAlwaysOnTop = true;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(600, 600, _dpiScaleFactor);
        
        // TODO: Center the window.
    }
}