using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Infrastructure.Constants;
using FluentNoiseGenerator.Infrastructure.Messages;
using FluentNoiseGenerator.UI.Infrastructure.Extensions;
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

    private readonly RectInt32 _displayWorkArea;

    private readonly InputNonClientPointerSource _inputNonClientPointerSource;

    private readonly IMessenger _messenger;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets the view model instance.
    /// </summary>
    public PlaybackViewModel ViewModel { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/> class
    /// using the specified dependencies.
    /// </summary>
    /// <param name="viewModel">
    /// The view model instance for the window.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws if any of the parameters are <c>null</c>.
    /// </exception>
    public PlaybackWindow(PlaybackViewModel viewModel, IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        ArgumentNullException.ThrowIfNull(messenger);

        _displayWorkArea = this.GetDisplayArea().WorkArea;

        _dpiScaleFactor = this.GetCurrentDpiScaleFactor();

        _inputNonClientPointerSource = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);

        _messenger = messenger;

        ViewModel = viewModel;

        SystemBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TopBar);

        RegisterMessageHandlers();

        ConfigureNativeWindow();
        ConfigureNativeTitleBar();

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
        ViewModel?.Dispose();

        _hasClosed = true;
    }
    #endregion

    #region Message handlers
    private void HandleClosePlaybackWindowMessage(
        object recipient,
        ClosePlaybackWindowMessage message)
    {
        Close();
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

    private void RegisterMessageHandlers()
    {
        _messenger.Register<ClosePlaybackWindowMessage>(
            this,
            HandleClosePlaybackWindowMessage
        );
    }

    private void UnregisterMessageHandlers()
    {
        _messenger.UnregisterAll(this);
    }

    private void UpdateNonClientInputRegions()
    {
        /**
         * Required to prevent the window from throwing a <see cref="ObjectDisposedException"/>.
         * Operations on the pointer source are not allowed once the window has been closed.
         */
        if (_hasClosed) return;

        /**
         * Region kind for drag must be set to `Caption` in order to set a drag region for the
         * title bar control. Really bizarre that one can't hide the native close chrome button
         * without making external calls to the Win32 API.
         * 
         * I am lazy and this is the easiest way of specifying drag regions after setting title
         * bar to false using <see cref="OverlappedPresenter.SetBorderAndTitleBar(bool, bool)"/>.
         */
        _inputNonClientPointerSource.ReplaceRegionRects(NonClientRegionKind.Caption, [
            TopBar.GetBoundingBox(_dpiScaleFactor)
        ]);

        _inputNonClientPointerSource.ReplaceRegionRects(NonClientRegionKind.Passthrough, [
            TopBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
            TopBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
        ]);
    }

    /// <summary>
    /// Configures the underlying, native title bar for the window.
    /// </summary>
    public void ConfigureNativeTitleBar()
    {
        AppWindow.SetIcon(Icons.IconPath);
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
}