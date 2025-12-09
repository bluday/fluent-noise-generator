using FluentNoiseGenerator.Extensions;
using FluentNoiseGenerator.Services;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using Windows.Win32;
using Windows.Win32.Foundation;
using WinRT.Interop;

namespace FluentNoiseGenerator.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaybackWindow : Window
{
    #region Fields
    private ResourceLoader? _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly Action _settingsWindowFactory;

    private readonly InputNonClientPointerSource _nonClientPointerSource;

    private readonly OverlappedPresenter _overlappedPresenter = OverlappedPresenter.Create();

    private readonly ResourceService _resourceService;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying { get; private set; }
    #endregion

    #region Events
    /// <summary>
    /// Triggers when a new settings window has been created.
    /// </summary>
    public event EventHandler SettingsWindowCreated = delegate { };
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/> class using default
    /// paramaeter values.
    /// </summary>
    /// <remarks>
    /// This parameterless constructor is required for design-time support in Visual Studio and
    /// to play nice with the XAML designer.
    /// </remarks>
    public PlaybackWindow() : this(null!, null!) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/> class using the specified
    /// resource service instance.
    /// </summary>
    /// <param name="resourceService">
    /// A <see cref="ResourceService"/> instance for accessing common application resources.
    /// </param>
    /// <param name="settingsWindowFactory">
    /// A factory for creating or restoring the settings window.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when one of the parameters is <c>null</c>.
    /// </exception>
    public PlaybackWindow(ResourceService resourceService, Action settingsWindowFactory)
    {
        ArgumentNullException.ThrowIfNull(resourceService);
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);

        _resourceService       = resourceService;
        _settingsWindowFactory = settingsWindowFactory;

        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);

        Closed += PlaybackWindow_Closed;

        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void PlaybackWindow_Closed(object sender, WindowEventArgs args)
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
    #endregion

    #region Methods
    private void ApplyLocalizedContent()
    {
        // TODO: Use localied strings.

        if (_resourceLoader is null) return;

        Title = _resourceLoader.GetString("General/AppDisplayName");
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

    /// <summary>
    /// Applies configuration to the underlying, native window specific to the playback window.
    /// </summary>
    public void ConfigureAppWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = true;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        AppWindow.SetPresenter(_overlappedPresenter);
        AppWindow.Resize(width: 260, height: 160);
        AppWindow.MoveToCenter();
    }

    /// <summary>
    /// Configures the native title bar and specifies the custom <see cref="TopActionBar"/>
    /// control as the primary title bar to ensure that the bounds for the custom title bar
    /// is set correctly.
    /// </summary>
    public void ConfigureTitleBar()
    {
        AppWindow.SetIcon(_resourceService.AppIconPath);

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TopActionBar);
    }

    /// <summary>
    /// Programmatically focuses the root element of the window.
    /// </summary>
    /// <remarks>
    /// Returns immediately if content is <c>null</c>.
    /// </remarks>
    public void Focus()
    {
        Content?.Focus(FocusState.Programmatic);
    }

    /// <summary>
    /// Updates the background brush of the root element based on its current
    /// <see cref="FrameworkElement.RequestedTheme"/> value.
    /// </summary>
    public void RefreshBackgroundBrush()
    {
        if (SystemBackdrop is not null)
        {
            LayoutRoot.Background = null;

            return;
        }

        LayoutRoot.Background = new SolidColorBrush(
            LayoutRoot.RequestedTheme is ElementTheme.Light
                ? Colors.White
                : Colors.Black
        );
    }

    /// <summary>
    /// Creates a new <see cref="ResourceLoader"/> instances and refreshes all named values
    /// with localized strings from the new resource loader.
    /// </summary>
    /// <remarks>
    /// This enables complete and real-time update of localized content, rather than forcing
    /// the user to restart the whole application after updating the application language.
    /// </remarks>
    public void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        ApplyLocalizedContent();
    }

    /// <summary>
    /// Retrieves and updates the current DPI scale factor based on the set DPI value of the
    /// display that the window currently is displayed on.
    /// </summary>
    public void RetrieveAndUpdateDpiScaleFactor()
    {
        var hwnd = (HWND)WindowNative.GetWindowHandle(this);

        uint value = PInvoke.GetDpiForWindow(hwnd);

        _dpiScaleFactor = (double)value / 96;
    }

    /// <inheritdoc cref="OverlappedPresenter.Restore()"/>
    public void Restore()
    {
        _overlappedPresenter.Restore();
    }

    /// <summary>
    /// Updates the current theme within the window using the specified value.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="ElementTheme"/> to apply on the window.
    /// </param>
    public void UpdateRequestedTheme(ElementTheme value)
    {
        (Content as FrameworkElement)?.RequestedTheme = value;

        RefreshBackgroundBrush();
    }

    /// <summary>
    /// Updates the current system backdrop within the window using the specified value.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="SystemBackdrop"/> to apply on the window.
    /// </param>
    public void UpdateSystemBackdrop(SystemBackdrop? value)
    {
        SystemBackdrop = value;

        RefreshBackgroundBrush();
    }
    #endregion
}