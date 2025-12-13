using FluentNoiseGenerator.Common.MethodExtensions;
using FluentNoiseGenerator.UI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.Win32;
using Windows.Win32.Foundation;
using WinRT.Interop;

namespace FluentNoiseGenerator.UI.Windows;

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
    private PlaybackViewModel? _viewModel;

    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly AppWindow _appWindow;

    private readonly InputNonClientPointerSource _inputNonClientPointerSource;

    private readonly OverlappedPresenter _overlappedPresenter;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets or sets the view model instance associated with this window type.
    /// </summary>
    public PlaybackViewModel? ViewModel
    {
        get => _viewModel;
        set
        {
            _viewModel = value;

            ConfigureTitleBar();
        }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/> class.
    public PlaybackWindow()
    {
        _appWindow = AppWindow;

        _inputNonClientPointerSource = InputNonClientPointerSource.GetForWindowId(_appWindow.Id);

        _overlappedPresenter = OverlappedPresenter.CreateForToolWindow();

        Closed += PlaybackWindow_Closed;

        InitializeComponent();

        RetrieveAndUpdateDpiScaleFactor();
        ConfigureAppWindow();
        ConfigureTitleBar();
    }
    #endregion

    #region Methods
    private void ConfigureAppWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = true;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        _appWindow.SetPresenter(_overlappedPresenter);
        _appWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
        _appWindow.MoveToCenter();
    }

    private void ConfigureTitleBar()
    {
        if (_viewModel?.TitleBarIconPath is string iconPath)
        {
            _appWindow.SetIcon(iconPath);
        }

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(playbackTopBar);
    }

    private void RefreshBackgroundBrush()
    {
        if (SystemBackdrop is not null)
        {
            layoutRoot.Background = null;

            return;
        }

        layoutRoot.Background = new SolidColorBrush(
            layoutRoot.RequestedTheme is ElementTheme.Light
                ? Colors.White
                : Colors.Black
        );
    }

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
    #endregion

    #region Event handlers
    private void layoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void PlaybackWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;

        Closed -= PlaybackWindow_Closed;
    }
    #endregion
}