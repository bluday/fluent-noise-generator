namespace BluDay.FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private double _dpiScaleFactor;
    
    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly ResourceLoader _resourceLoader;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _resourceLoader = new ResourceLoader();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        InitializeComponent();

        ConfigureTitleBar();

        Configure();
    }

    private void Configure()
    {
        int size = (int)(600 * _dpiScaleFactor);

        _overlappedPresenter.PreferredMinimumHeight = size;
        _overlappedPresenter.PreferredMinimumWidth  = size;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(width: 600, height: 600, _dpiScaleFactor);
    }

    private void ConfigureTitleBar()
    {
        string iconPath = _resourceLoader.GetString("AppIconPath/64x64");
        string title    = _resourceLoader.GetString("AppDisplayName");

        _appWindow.SetIcon(iconPath);

        TitleBarControl.Icon = new BitmapImage(new Uri(iconPath));

        TitleBarControl.Title = title;

        ExtendsContentIntoTitleBar = true;

        Title = title;

        SetTitleBar(TitleBarControl);
    }
}