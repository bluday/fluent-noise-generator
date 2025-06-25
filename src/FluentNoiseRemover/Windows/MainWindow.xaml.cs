using BluDay.Net.WinUI3.Extensions;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.Windows.ApplicationModel.Resources;
using System;

namespace FluentNoiseRemover.Windows;

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

        _overlappedPresenter.SetBorderAndTitleBar(
            hasBorder:   true,
            hasTitleBar: false
        );

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(width: 450, height: 280, _dpiScaleFactor);
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
}