using FluentNoiseGenerator.Features.Playback.UI.ViewModels;
using FluentNoiseGenerator.Foundation.Constants;
using FluentNoiseGenerator.Foundation.UI.Extensions;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.Features.Playback.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaybackWindow : Window
{
    #region Constants
    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MinimumUnscaledHeight = 110;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MinimumUnscaledWidth = 170;
    #endregion

    #region Instance fields
    private bool _hasClosed;

    private readonly InputNonClientPointerSource _inputNonClientPointerSource;
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets the view model.
    /// </summary>
    public PlaybackViewModel ViewModel { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/>
    /// class using the specified view model.
    /// </summary>
    /// <param name="viewModel">
    /// The view model.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="viewModel"/> is <c>null</c>.
    /// </exception>
    public PlaybackWindow(PlaybackViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        _inputNonClientPointerSource = this.GetInputNonClientPointerSource();

        ExtendsContentIntoTitleBar = true;

        ViewModel = viewModel;

        Closed += Window_Closed;

        SetTitleBar(TopBar);

        ConfigureAppWindow();

        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void LayoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;

        Closed -= Window_Closed;
    }
    #endregion

    #region Instance methods
    private void ConfigureAppWindow()
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

        presenter.SetBorderAndTitleBar(
            hasBorder:   true,
            hasTitleBar: false
        );

        double dpiScaleFactor = this.GetCurrentDpiScaleFactor();

        appWindow.Resize(
            (int)(MinimumUnscaledWidth  * dpiScaleFactor),
            (int)(MinimumUnscaledHeight * dpiScaleFactor)
        );

        appWindow.MoveToCenter();
        appWindow.SetIcon(Icons.IconPath);
    }

    private void UpdateNonClientInputRegions()
    {
        /**
         * Required to prevent the window from throwing a <see cref="ObjectDisposedException"/>.
         * Operations on the pointer source are not allowed once the window has been closed.
         */
        if (_hasClosed) return;

        double dpiScaleFactor = this.GetCurrentDpiScaleFactor();

        /**
         * Region kind for drag must be set to `Caption` in order to set a drag region for the
         * title bar control. Really bizarre that one can't hide the native close chrome button
         * without making external calls to the Win32 API.
         * 
         * I am lazy and this is the easiest way of specifying drag regions after setting title
         * bar to false using <see cref="OverlappedPresenter.SetBorderAndTitleBar(bool, bool)"/>.
         */
        _inputNonClientPointerSource.ReplaceRegionRects(
            NonClientRegionKind.Caption,
            [TopBar.GetBoundingBox(dpiScaleFactor)]
        );

        _inputNonClientPointerSource.ReplaceRegionRects(
            NonClientRegionKind.Passthrough,
            [
                TopBar.GetBoundingRectForSettingsButton(dpiScaleFactor),
                TopBar.GetBoundingRectForCloseButton(dpiScaleFactor)
            ]
        );
    }
    #endregion
}