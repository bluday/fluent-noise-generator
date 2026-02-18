using FluentNoiseGenerator.UI.Common.Extensions;
using FluentNoiseGenerator.UI.Playback.ViewModels;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;

namespace FluentNoiseGenerator.UI.Playback.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaybackWindow : Window
{
    #region Constants
    /// <summary>
    /// Reduction factor applied when a scaled dimension value exceeds the
    /// display work area.
    /// </summary>
    public const double DISPLAY_WORK_AREA_OVERFLOW_REDUCTION_FACTOR = 0.8;

    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_UNSCALED_HEIGHT = 110;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_UNSCALED_WIDTH = 170;
    #endregion

    #region Fields
    private bool _hasClosed;

    private readonly double _dpiScaleFactor;

    private readonly InputNonClientPointerSource _inputNonClientPointerSource;

    private readonly RectInt32 _displayWorkArea;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets or sets the view model instance associated with this window type.
    /// </summary>
    public PlaybackViewModel? ViewModel { get; set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/> class.
    /// </summary>
    public PlaybackWindow()
    {
        Microsoft.UI.WindowId windowId = AppWindow.Id;

        _displayWorkArea = DisplayArea
            .GetFromWindowId(windowId, DisplayAreaFallback.None)
            .WorkArea;

        _dpiScaleFactor = this.GetCurrentDpiScaleFactor();

        _inputNonClientPointerSource = InputNonClientPointerSource.GetForWindowId(windowId);

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TopBar);

        ConfigureNativeWindow();
        ConfigureNativeTitleBar();

        InitializeComponent();
    }
    #endregion

    #region Methods
    private int GetScaledMinimumHeight()
    {
        return (int)Math.Min(
            MINIMUM_UNSCALED_HEIGHT * _dpiScaleFactor,
            _displayWorkArea.Height * DISPLAY_WORK_AREA_OVERFLOW_REDUCTION_FACTOR
        );
    }

    private int GetScaledMinimumWidth()
    {
        return (int)Math.Min(
            MINIMUM_UNSCALED_WIDTH * _dpiScaleFactor,
            _displayWorkArea.Width * DISPLAY_WORK_AREA_OVERFLOW_REDUCTION_FACTOR
        );
    }

    private void UpdateNonClientInputRegions()
    {
        /**
         * Required to prevent the window from throwing a <see cref="ObjectDisposedException"/>.
         * Operations on the pointer source are not allowed once the window has been closed.
         */
        if (_hasClosed) return;

        _inputNonClientPointerSource.ClearAllRegionRects();

        /**
         * Region kind for drag must be set to `Caption` in order to set a drag region for the
         * title bar control. Really bizarre that one can't hide the native close chrome button
         * without making external calls to the Win32 API.
         * 
         * I am lazy and this is the easiest way of specifying drag regions after setting title
         * bar to false using <see cref="OverlappedPresenter.SetBorderAndTitleBar(bool, bool)"/>.
         */
        _inputNonClientPointerSource.SetRegionRects(
            region: NonClientRegionKind.Caption,
            rects: [
                TopBar.GetBoundingBox(_dpiScaleFactor)
            ]
        );

        _inputNonClientPointerSource.SetRegionRects(
            region: NonClientRegionKind.Passthrough,
            rects: [
                TopBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
                TopBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
            ]
        );
    }

    /// <summary>
    /// Configures the underlying, native title bar for the window.
    /// </summary>
    public void ConfigureNativeTitleBar()
    {
        AppWindow.SetIcon(FluentNoiseGenerator.Common.Constants.IconPath);
    }

    /// <summary>
    /// Configures the underlying, native window.
    /// </summary>
    public void ConfigureNativeWindow()
    {
        AppWindow appWindow = AppWindow;

        if (appWindow.Presenter is not OverlappedPresenter presenter)
        {
            presenter = OverlappedPresenter.CreateForToolWindow();

            appWindow.SetPresenter(presenter);
        }

        presenter.IsAlwaysOnTop = true;
        presenter.IsMaximizable = false;
        presenter.IsMinimizable = true;
        presenter.IsResizable   = false;

        presenter.SetBorderAndTitleBar(hasBorder: true, hasTitleBar: false);

        appWindow.Resize(GetScaledMinimumWidth(), GetScaledMinimumHeight());
        appWindow.MoveToCenter();
    }
    #endregion

    #region Event handlers
    private void LayoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        ViewModel?.Dispose();

        _hasClosed = true;
    }
    #endregion
}