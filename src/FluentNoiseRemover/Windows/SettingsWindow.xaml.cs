using BluDay.Net.WinUI3.Extensions;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.ApplicationModel;

namespace FluentNoiseRemover.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _resourceLoader = new ResourceLoader();

        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        ApplicationThemeChanged = (sender, e) => { };
        SystemBackdropChanged   = (sender, e) => { };

        LocalizedApplicationThemes = null!;
        LocalizedAudioSampleRates  = null!;
        LocalizedLanguages         = null!;
        LocalizedNoisePresets      = null!;
        LocalizedSystemBackdrops   = null!;

        InitializeComponent();

        PopulateComboBoxControlsWithLocalizedValues();

        RegisterEventHandlers();

        ConfigureAppWindow();
        ConfigureTitleBar();

        ApplyLocalizedContent();
    }

    private void ConfigureAppWindow()
    {
        int scaledMinimumWidth  = (int)(MINIMUM_WIDTH  / _dpiScaleFactor);
        int scaledMinimumHeight = (int)(MINIMUM_HEIGHT / _dpiScaleFactor);

        _overlappedPresenter.PreferredMinimumWidth  = scaledMinimumWidth;
        _overlappedPresenter.PreferredMinimumHeight = scaledMinimumHeight;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(scaledMinimumWidth, scaledMinimumHeight);
    }

    private void ConfigureTitleBar()
    {
        _appWindow.SetIcon(App.IconPath);

        TitleBar.Icon = App.IconPath;

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TitleBar);
    }

    private string GetApplicationVersionText()
    {
        PackageVersion version = Package.Current.Id.Version;

        return $"{version.Major}.{version.Minor}";
    }

    private void RegisterEventHandlers()
    {
        Closed += SettingsWindow_Closed;
    }

    /// <summary>
    /// Focuses the root element of the window.
    /// </summary>
    public void Focus()
    {
        Content?.Focus(FocusState.Programmatic);
    }

    /// <inheritdoc cref="OverlappedPresenter.Restore()"/>
    public void Restore()
    {
        _overlappedPresenter.Restore();
    }
}