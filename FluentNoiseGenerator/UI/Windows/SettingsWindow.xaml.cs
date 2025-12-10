using FluentNoiseGenerator.Extensions;
using FluentNoiseGenerator.UI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace FluentNoiseGenerator.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    #region Constants
    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_HEIGHT = 1000;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 1000;
    #endregion

    #region Fields
    private SettingsViewModel? _viewModel;

    private bool _hasClosed;

    private readonly AppWindow _appWindow;

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
    public SettingsViewModel? ViewModel
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
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        Closed += SettingsWindow_Closed;

        InitializeComponent();

        ConfigureAppWindow();
        ConfigureTitleBar();
    }
    #endregion

    #region Methods
    private void ConfigureAppWindow()
    {
        _overlappedPresenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        _overlappedPresenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        _appWindow.SetPresenter(_overlappedPresenter);
        _appWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
    }

    private void ConfigureTitleBar()
    {
        if (_viewModel?.TitleBarIconPath is string iconPath)
        {
            _appWindow.SetIcon(iconPath);
        }

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(settingsTitleBar);
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

    private void RefreshTitleBarColors()
    {
        AppWindowTitleBar titleBar = _appWindow.TitleBar;

        Color buttonForegroundColor;
        Color hoverPressedBackgroundColor;

        titleBar.ButtonBackgroundColor         = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (layoutRoot.RequestedTheme is ElementTheme.Light)
        {
            hoverPressedBackgroundColor = Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD);

            buttonForegroundColor = Colors.Black;
        }
        else
        {
            hoverPressedBackgroundColor = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);

            buttonForegroundColor = Colors.White;
        }

        titleBar.ButtonHoverBackgroundColor   = hoverPressedBackgroundColor;
        titleBar.ButtonPressedBackgroundColor = hoverPressedBackgroundColor;

        titleBar.ButtonForegroundColor        = buttonForegroundColor;
        titleBar.ButtonHoverForegroundColor   = buttonForegroundColor;
        titleBar.ButtonPressedForegroundColor = buttonForegroundColor;
    }
    #endregion

    #region Event handlers
    private void SettingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;

        Closed -= SettingsWindow_Closed;
    }
    #endregion
}