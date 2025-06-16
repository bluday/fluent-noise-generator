namespace BluDay.FluentNoiseRemover;

public partial class App
{
    private void _settingsWindow_ApplicationThemeChanged(object? sender, ElementTheme e)
    {
        _elementTheme = e;

        _mainWindow?.SetRequestedTheme(e);
        _settingsWindow?.SetRequestedTheme(e);

        UpdateSettingsWindowTitleBarColors();
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, SystemBackdrop? e)
    {
        _mainWindow?.SetSystemBackdrop(e);
        _settingsWindow?.SetSystemBackdrop(e);
    }

#if DEBUG
    private void App_UnhandledException(object sender, Muxc.UnhandledExceptionEventArgs e)
    {
        Debug.WriteLine(e.Exception);
    }
#endif
}