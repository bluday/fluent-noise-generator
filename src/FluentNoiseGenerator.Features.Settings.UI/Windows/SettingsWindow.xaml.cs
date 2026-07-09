using FluentNoiseGenerator.Foundation.Constants;
using FluentNoiseGenerator.Foundation.UI.Extensions;
using FluentNoiseGenerator.Features.Settings.UI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Windows.UI;

namespace FluentNoiseGenerator.Features.Settings.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    #region Constants
    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MinimumUnscaledHeight = 700;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MinimumUnscaledWidth = MinimumUnscaledHeight;
    #endregion
    
    #region Instance properties
    /// <summary>
    /// Gets the view model.
    /// </summary>
    public SettingsViewModel ViewModel { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/>
    /// class using the specified view model.
    /// </summary>
    /// <param name="viewModel">
    /// The view model.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any parameter is <c>null</c>.
    /// </exception>
    public SettingsWindow(SettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        ExtendsContentIntoTitleBar = true;

        ViewModel = viewModel;

        SetTitleBar(TitleBar);

        ConfigureAppWindow();

        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshTitleBarColors(LayoutRoot.RequestedTheme);
    }
    #endregion

    #region Instance methods
    private void ConfigureAppWindow()
    {
        AppWindow appWindow = AppWindow;

        if (appWindow.Presenter is not OverlappedPresenter presenter)
        {
            presenter = OverlappedPresenter.Create();

            appWindow.SetPresenter(presenter);
        }

        double dpiScaleFactor = this.GetCurrentDpiScaleFactor();

        int scaledMinimumHeight = (int)(MinimumUnscaledHeight * dpiScaleFactor);
        int scaledMinimumWidth  = (int)(MinimumUnscaledWidth  * dpiScaleFactor);

        presenter.PreferredMinimumWidth  = scaledMinimumWidth;
        presenter.PreferredMinimumHeight = scaledMinimumHeight;

        appWindow.Resize(scaledMinimumHeight, scaledMinimumWidth);
        appWindow.SetIcon(Icons.IconPath);
    }

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
    #endregion
}