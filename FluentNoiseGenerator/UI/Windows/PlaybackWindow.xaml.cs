using FluentNoiseGenerator.Extensions;
using FluentNoiseGenerator.Services;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Globalization;
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
    private ResourceLoader? _resourceLoader;

    private bool _hasClosed;

    private double _dpiScaleFactor;

    private readonly InputNonClientPointerSource _nonClientPointerSource;

    private readonly OverlappedPresenter _overlappedPresenter = OverlappedPresenter.Create();

    private readonly LanguageService _languageService;

    private readonly ResourceService _resourceService;

    private readonly ThemeService _themeService;
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
    /// Triggers when the settings button has been clicked.
    /// </summary>
    public event EventHandler SettingsButtonClicked = delegate { };
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
    public PlaybackWindow() : this(null!, null!, null!) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/> class using the specified
    /// resource service instance.
    /// </summary>
    /// <param name="languageService">
    /// The language service for retrieving and updating application language info.
    /// </param>
    /// <param name="resourceService">
    /// The resource service for retrieving application resources.
    /// </param>
    /// <param name="themeService">
    /// The theme service for retrieving and updating the current application theme.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    public PlaybackWindow(
        LanguageService languageService,
        ResourceService resourceService,
        ThemeService    themeService)
    {
        _nonClientPointerSource = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);

        _languageService = languageService;
        _resourceService = resourceService;
        _themeService    = themeService;

        RegisterEventHanders();
        RetrieveAndUpdateDpiScaleFactor();
        ConfigureAppWindow();
        RefreshLocalizedContent();
        InitializeComponent();
        ConfigureTitleBar();
    }
    #endregion

    #region Event handlers
    private void _languageService_CurrentCultureInfoChanged(CultureInfo? value)
    {
        RefreshLocalizedContent();
    }

    private void _themeService_CurrentThemeChanged(ElementTheme value)
    {
        layoutRoot.RequestedTheme = value;

        RefreshBackgroundBrush();
    }

    private void _themeService_CurrentSystemBackdropChanged(SystemBackdrop? value)
    {
        SystemBackdrop = value;

        RefreshBackgroundBrush();
    }

    private void layoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void playbackControlPanel_PlaybackButtonClicked(object sender, EventArgs e)
    {
        TogglePlayback();
    }

    private void playbackTopBar_CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void playbackTopBar_SettingsButtonClicked(object sender, EventArgs e)
    {
        SettingsButtonClicked.Invoke(this, EventArgs.Empty);
    }

    private void PlaybackWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }
    #endregion

    #region Methods
    private void ApplyLocalizedContent()
    {
        if (_resourceLoader is null) return;

        Title = _resourceLoader.GetString("General/AppDisplayName");
    }

    private void ConfigureAppWindow()
    {
        _overlappedPresenter.IsAlwaysOnTop = true;
        _overlappedPresenter.IsMaximizable = false;
        _overlappedPresenter.IsMinimizable = true;
        _overlappedPresenter.IsResizable   = false;

        _overlappedPresenter.SetBorderAndTitleBar(true, false);

        AppWindow.SetPresenter(_overlappedPresenter);
        AppWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
        AppWindow.MoveToCenter();
    }

    private void ConfigureTitleBar()
    {
        AppWindow.SetIcon(_resourceService.AppIconPath);

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

    private void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        ApplyLocalizedContent();
    }

    private void RegisterEventHanders()
    {
        _languageService?.CurrentCultureInfoChanged += _languageService_CurrentCultureInfoChanged;

        _themeService?.CurrentSystemBackdropChanged += _themeService_CurrentSystemBackdropChanged;
        _themeService?.CurrentThemeChanged          += _themeService_CurrentThemeChanged;

        Closed += PlaybackWindow_Closed;
    }

    private void RetrieveAndUpdateDpiScaleFactor()
    {
        var hwnd = (HWND)WindowNative.GetWindowHandle(this);

        uint value = PInvoke.GetDpiForWindow(hwnd);

        _dpiScaleFactor = (double)value / DEFAULT_DPI_SCALE;
    }

    private void TogglePlayback()
    {
        bool isPlaying = !IsPlaying;

        IsPlaying = isPlaying;

        playbackControlPanel.IsPlaying = isPlaying;
    }

    private void UnregisterEventHanders()
    {
        _languageService?.CurrentCultureInfoChanged -= _languageService_CurrentCultureInfoChanged;

        _themeService?.CurrentSystemBackdropChanged -= _themeService_CurrentSystemBackdropChanged;
        _themeService?.CurrentThemeChanged          -= _themeService_CurrentThemeChanged;

        Closed -= PlaybackWindow_Closed;
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
         * bar to false using <see cref="OverlappedPresenter.SetBorderAndSettingsTitleBar(bool, bool)"/>.
         */
        _nonClientPointerSource.SetRegionRects(NonClientRegionKind.Caption, [
            playbackTopBar.GetBoundingBox(_dpiScaleFactor)
        ]);

        _nonClientPointerSource.SetRegionRects(NonClientRegionKind.Passthrough, [
            playbackTopBar.GetBoundingRectForSettingsButton(_dpiScaleFactor),
            playbackTopBar.GetBoundingRectForCloseButton(_dpiScaleFactor)
        ]);
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

    /// <inheritdoc cref="OverlappedPresenter.Restore()"/>
    public void Restore()
    {
        _overlappedPresenter.Restore();
    }
    #endregion
}