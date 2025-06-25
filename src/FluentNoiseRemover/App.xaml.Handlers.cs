using FluentNoiseRemover.Extensions;

namespace FluentNoiseRemover;

public partial class App
{
    private void _settingsWindow_ApplicationThemeChanged(object? sender, Microsoft.UI.Xaml.ElementTheme e)
    {
        _elementTheme = e;

        _mainWindow?.SetRequestedTheme(e);
        _settingsWindow?.SetRequestedTheme(e);

        UpdateSettingsWindowTitleBarColors();
    }

    private void _settingsWindow_SystemBackdropChanged(object? sender, Microsoft.UI.Xaml.Media.SystemBackdrop? e)
    {
        _mainWindow?.SetSystemBackdrop(e);
        _settingsWindow?.SetSystemBackdrop(e);
    }
}