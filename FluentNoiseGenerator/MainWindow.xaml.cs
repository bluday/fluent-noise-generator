using BluDay.Net.WinUI3.Extensions;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.Windows.ApplicationModel.Resources;
using System;

namespace FluentNoiseGenerator;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Microsoft.UI.Xaml.Window
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;
    
    private readonly InputNonClientPointerSource _nonClientPointerSource;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    private readonly Action _settingsWindowFactory;

    /// <summary>
    /// Triggers when a new settings window has been created.
    /// </summary>
    public event EventHandler SettingsWindowCreated;

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    /// <param name="settingsWindowFactory">
    /// The <see cref="SettingsWindow"/> factory.
    /// </param>
    public MainWindow(Action settingsWindowFactory)
    {
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);

        _resourceLoader = new ResourceLoader();

        _appWindow = AppWindow;

        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(_appWindow.Id);
        
        _overlappedPresenter = OverlappedPresenter.Create();

        _settingsWindowFactory = settingsWindowFactory;

        _dpiScaleFactor = this.GetDpiScaleFactorInDecimal();

        SettingsWindowCreated = (sender, e) => { };

        RegisterEventHandlers();

        InitializeComponent();

        ConfigureAppWindow();
        ConfigureTitleBar();
    }

    private void ConfigureAppWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(260, 160);
    }

    private void ConfigureTitleBar()
    {
        _appWindow.SetIcon(App.IconPath);

        ExtendsContentIntoTitleBar = true;

        Title = _resourceLoader.GetString("General/AppDisplayName");

        SetTitleBar(TopActionBar);
    }

    private void RegisterEventHandlers()
    {
        Closed += MainWindow_Closed;
    }

    private void TogglePlayback()
    {
        bool isPlaying = !IsPlaying;

        IsPlaying = isPlaying;

        PlaybackControlPanel.IsPlaying = isPlaying;
    }

    private void UpdateNonClientInputRegions()
    {
        /**
         * Required to prevent the window from throwing a <see cref="ObjectDisposedException"/>.
         * Operations on the pointer source are not allowed once the window has been closed.
         */
        if (_hasClosed) return;

        _nonClientPointerSource.ClearAllRegionRects();

        /**
         * Region kind for drag must be set to `Caption` in order to set a drag region for the
         * title bar control. Really bizarre that one can't hide the native close chrome button
         * without making external calls to the Win32 API.
         * 
         * I am lazy and this is the easiest way of specifying drag regions after setting title
         * bar to false using <see cref="OverlappedPresenter.SetBorderAndTitleBar(bool, bool)"/>.
         */
        _nonClientPointerSource.SetRegionRects(
            region: NonClientRegionKind.Caption,
            rects:  [TopActionBar.GetBoundingBox(_dpiScaleFactor)]
        );

        _nonClientPointerSource.SetRegionRects(
            region: NonClientRegionKind.Passthrough,
            rects:  [
                TopActionBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
                TopActionBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
            ]
        );
    }

    private void MainWindow_Closed(object sender, Microsoft.UI.Xaml.WindowEventArgs args)
    {
        _hasClosed = true;
    }

    private void LayoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void PlaybackControlPanel_PlaybackButtonClicked(object sender, EventArgs e)
    {
        TogglePlayback();
    }

    private void TopActionBar_CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void TopActionBar_SettingsButtonClicked(object sender, EventArgs e)
    {
        _settingsWindowFactory();
    }
}