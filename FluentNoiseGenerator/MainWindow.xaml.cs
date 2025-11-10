using FluentNoiseGenerator.Extensions;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using Windows.Graphics;
using Windows.Win32;
using WinRT.Interop;

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

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// The default height in pixels, unscaled.
    /// </summary>
    public const int DEFAULT_HEIGHT = 160;

    /// <summary>
    /// The default width in pixels, unscaled.
    /// </summary>
    public const int DEFAULT_WIDTH = 260;

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
    /// The factory for creating a <see cref="SettingsWindow"/> instance.
    /// </summary>
    public Action? SettingsWindowFactory { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        _resourceLoader = null!;

        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);
        
        _overlappedPresenter = OverlappedPresenter.Create();

        SettingsWindowCreated = delegate { };

        Closed += MainWindow_Closed;

        InitializeComponent();
    }

    public void ApplyLocalizedContent()
    {
        // TODO: Use localied strings.
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
        _nonClientPointerSource.SetRegionRects(NonClientRegionKind.Caption, [
            TopActionBar.GetBoundingBox(_dpiScaleFactor)
        ]);

        _nonClientPointerSource.SetRegionRects(NonClientRegionKind.Passthrough, [
            TopActionBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
            TopActionBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
        ]);
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
        SettingsWindowFactory?.Invoke();
    }

    public void ConfigureAppWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = false;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        AppWindow.SetPresenter(_overlappedPresenter);

        SizeInt32 size = new(DEFAULT_WIDTH, DEFAULT_HEIGHT);

        RectInt32 workArea = DisplayArea
            .GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary)
            .WorkArea;

        AppWindow.Resize(size);

        AppWindow.Move(new PointInt32(
            (workArea.Width - size.Width) / 2,
            (workArea.Height - size.Height) / 2
        ));
    }

    public void ConfigureTitleBar()
    {
        AppWindow.SetIcon(App.IconPath);

        ExtendsContentIntoTitleBar = true;

        Title = _resourceLoader.GetString("General/AppDisplayName");

        SetTitleBar(TopActionBar);
    }

    public void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        ApplyLocalizedContent();
    }

    public void RetrieveAndUpdateDpiScale()
    {
        IntPtr hwnd = WindowNative.GetWindowHandle(this);

        uint value = PInvoke.GetDpiForWindow((Windows.Win32.Foundation.HWND)hwnd);

        _dpiScaleFactor = (double)value / 96;
    }
}