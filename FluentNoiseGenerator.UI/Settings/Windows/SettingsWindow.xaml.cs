using FluentNoiseGenerator.UI.Common.Extensions;
using FluentNoiseGenerator.UI.Settings.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace FluentNoiseGenerator.UI.Settings.Windows;

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
    public const int MINIMUM_WIDTH = MINIMUM_HEIGHT;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed { get; private set; }

    /// <summary>
    /// Gets or sets the view model instance associated with this window type.
    /// </summary>
    public SettingsViewModel? ViewModel { get; set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        ExtendsContentIntoTitleBar = true;

        SetTitleBar(settingsTitleBar);

        ConfigureNativeWindow();
        ConfigureNativeTitleBar();

        InitializeComponent();
    }
    #endregion

    #region Methods
    private void RefreshTitleBarColors(ElementTheme elementTheme)
    {
        if (!AppWindowTitleBar.IsCustomizationSupported())
        {
            return;
        }

        AppWindowTitleBar titleBar = AppWindow.TitleBar;

        Color buttonForegroundColor;
        Color hoverPressedBackgroundColor;

        titleBar.ButtonBackgroundColor         = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if (elementTheme is ElementTheme.Light)
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

    /// <summary>
    /// Configures the underlying, native title bar for the window.
    /// </summary>
    public void ConfigureNativeTitleBar()
    {
        AppWindow.SetIcon(FluentNoiseGenerator.Common.Constants.IconPath);
    }

    /// <summary>
    /// Configures the underlying native window.
    /// </summary>
    public void ConfigureNativeWindow()
    {
        AppWindow appWindow = AppWindow;

        if (appWindow.Presenter is not OverlappedPresenter presenter)
        {
            presenter = OverlappedPresenter.Create();

            appWindow.SetPresenter(presenter);
        }

        presenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        presenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        appWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
    }
    #endregion

    #region Event handlers
    private void Window_Closed(object sender, WindowEventArgs args)
    {
        ViewModel?.Dispose();

        HasClosed = true;
    }
    #endregion
}