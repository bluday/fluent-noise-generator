using FluentNoiseGenerator.Common.MethodExtensions;
using FluentNoiseGenerator.UI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.ComponentModel;
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
    public const int MINIMUM_WIDTH = MINIMUM_HEIGHT;
    #endregion

    #region Fields
    private SettingsViewModel? _settingsViewModel;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed { get; private set; }

    /// <summary>
    /// Gets the path for the title bar icon.
    /// </summary>
    public string TitleBarIconPath { get; }

    /// <summary>
    /// Gets or sets the view model instance associated with this window type.
    /// </summary>
    public SettingsViewModel? ViewModel
    {
        get => _settingsViewModel;
        set
        {
            if (_settingsViewModel == value) return;

            if (value is null)
            {
                _settingsViewModel?.PropertyChanged -= SettingsViewModel_PropertyChanged;
            }

            _settingsViewModel = value;

            value?.PropertyChanged += SettingsViewModel_PropertyChanged;
        }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        ExtendsContentIntoTitleBar = true;

        TitleBarIconPath = Common.Constants.IconPath;

        SetTitleBar(settingsTitleBar);

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
        AppWindow.SetIcon(TitleBarIconPath);
    }

    /// <summary>
    /// Configures the underlying native window.
    /// </summary>
    public void ConfigureNativeWindow()
    {
        if (AppWindow.Presenter is not OverlappedPresenter presenter)
        {
            presenter = OverlappedPresenter.Create();

            AppWindow.SetPresenter(presenter);
        }

        presenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        presenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        AppWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
    }
    #endregion

    #region Event handlers
    private void SettingsViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(_settingsViewModel.CurrentTheme))
        {
            RefreshTitleBarColors(_settingsViewModel!.CurrentTheme);
        }
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        HasClosed = true;
    }
    #endregion
}