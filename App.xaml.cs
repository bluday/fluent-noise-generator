namespace BluDay.FluentNoiseRemover;

/// <summary>
/// Interaction logic for App.xaml and the entrypoint for the application.
/// </summary>
public partial class App : Application
{
    private MainWindow? _mainWindow;

    private SettingsWindow? _settingsWindow;

    private ElementTheme _elementTheme;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
#if DEBUG
        UnhandledException += App_UnhandledException;
#endif

        InitializeComponent();
    }

    private ResourceLoader CreateResourceLoader() => new();

    private void SetRequestedTheme(Window? window, ElementTheme value)
    {
        if (window?.Content is FrameworkElement element)
        {
            element.RequestedTheme = value;
        }
    }

    private void SetSystemBackdrop(Window? window, SystemBackdrop? value)
    {
        if (window is null) return;

        window.SystemBackdrop = value;
    }

    private void ShowSettingsWindow()
    {
        if (_settingsWindow?.HasClosed is false)
        {
            _settingsWindow.Restore();
            _settingsWindow.Focus();

            return;
        }

        if (_settingsWindow is not null)
        {
            _settingsWindow.ApplicationThemeChanged -= _settingsWindow_ApplicationThemeChanged;
            _settingsWindow.SystemBackdropChanged   -= _settingsWindow_SystemBackdropChanged;
        }

        _settingsWindow = new SettingsWindow(CreateResourceLoader);

        _settingsWindow.ApplicationThemeChanged += _settingsWindow_ApplicationThemeChanged;
        _settingsWindow.SystemBackdropChanged   += _settingsWindow_SystemBackdropChanged;

        _settingsWindow.Activate();
    }

    private void UpdateSettingsWindowTitleBarColors()
    {
        if (_settingsWindow?.AppWindow.TitleBar is not AppWindowTitleBar settingsWindowTitleBar)
        {
            return;
        }

        settingsWindowTitleBar.ButtonBackgroundColor         = Colors.Transparent;
        settingsWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        settingsWindowTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (_elementTheme is ElementTheme.Light)
        {
            settingsWindowTitleBar.ButtonHoverBackgroundColor   = Colors.DarkGray;
            settingsWindowTitleBar.ButtonPressedBackgroundColor = Colors.LightGray;

            settingsWindowTitleBar.ButtonForegroundColor        = Colors.Black;
            settingsWindowTitleBar.ButtonHoverForegroundColor   = Colors.Black;
            settingsWindowTitleBar.ButtonPressedForegroundColor = Colors.Black;

            return;
        }

        settingsWindowTitleBar.ButtonHoverBackgroundColor   = Colors.LightGray;
        settingsWindowTitleBar.ButtonPressedBackgroundColor = Colors.Gray;

        settingsWindowTitleBar.ButtonForegroundColor        = Colors.White;
        settingsWindowTitleBar.ButtonHoverForegroundColor   = Colors.White;
        settingsWindowTitleBar.ButtonPressedForegroundColor = Colors.White;
    }

    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        _elementTheme = e;

        SetRequestedTheme(_mainWindow, e);
        SetRequestedTheme(_settingsWindow, e);

        UpdateSettingsWindowTitleBarColors();
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
        SetSystemBackdrop(_mainWindow, e);
        SetSystemBackdrop(_settingsWindow, e);
    }

    private void App_UnhandledException(object sender, Muxc.UnhandledExceptionEventArgs e)
    {
        Debug.WriteLine(e.Exception);
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _mainWindow = new MainWindow(ShowSettingsWindow, CreateResourceLoader);

        _mainWindow.Activate();
    }
}