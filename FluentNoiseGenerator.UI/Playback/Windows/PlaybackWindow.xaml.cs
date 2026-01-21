using FluentNoiseGenerator.UI.Extensions;
using FluentNoiseGenerator.UI.Playback.ViewModels;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Win32;
using Windows.Win32.Foundation;
using WinRT.Interop;

namespace FluentNoiseGenerator.UI.Playback.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaybackWindow : Window
{
    #region Constants
    /// <summary>
    /// The standard or user-default screen DPI value.
    /// </summary>
    public const int DEFAULT_DPI_SCALE = 96;

    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_HEIGHT = 160;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 260;
    #endregion

    #region Fields
    private double _dpiScaleFactor;

    private bool _hasClosed;

    private readonly InputNonClientPointerSource _inputNonClientPointerSource;
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
    public PlaybackWindow()
    {
        _inputNonClientPointerSource = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(playbackTopBar);

        RetrieveAndUpdateDpiScaleFactor();

        ConfigureNativeWindow();
        ConfigureNativeTitleBar();

        InitializeComponent();
    }
    #endregion

    #region Methods
    private void RetrieveAndUpdateDpiScaleFactor()
    {
        uint value = PInvoke.GetDpiForWindow((HWND)WindowNative.GetWindowHandle(this));

        _dpiScaleFactor = (double)value / DEFAULT_DPI_SCALE;
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
            rects: [playbackTopBar.GetBoundingBox(_dpiScaleFactor)]
        );

        _inputNonClientPointerSource.SetRegionRects(
            region: NonClientRegionKind.Passthrough,
            rects: [
                playbackTopBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
                playbackTopBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
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

        OverlappedPresenter presenter = OverlappedPresenter.CreateForToolWindow();

        presenter.IsAlwaysOnTop = true;
        presenter.IsMaximizable = false;
        presenter.IsMinimizable = true;
        presenter.IsResizable   = false;

        presenter.SetBorderAndTitleBar(hasBorder: true, hasTitleBar: false);

        appWindow.SetPresenter(presenter);
        appWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
        appWindow.MoveToCenter();
    }
    #endregion

    #region Event handlers
    private void LayoutUpdated(object sender, object e)
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