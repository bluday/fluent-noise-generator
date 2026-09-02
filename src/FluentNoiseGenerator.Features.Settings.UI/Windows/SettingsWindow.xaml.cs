using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator.Features.Settings.UI.ViewModels;
using FluentNoiseGenerator.Foundation.Constants;
using FluentNoiseGenerator.UI.Extensions;
using FluentNoiseGenerator.UI.Windowing;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Features.Settings.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window, IConfigurableWindow
{
    #region Constants
    /// <summary>
    /// The minimum height, in pixels.
    /// </summary>
    public const int MinimumHeight = 700;

    /// <summary>
    /// The minimum width, in pixels.
    /// </summary>
    public const int MinimumWidth = 700;
    #endregion

    #region Instance fields
    private readonly SettingsWindowViewModel _viewModel;
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets the view model.
    /// </summary>
    public SettingsWindowViewModel ViewModel => _viewModel;
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/>
    /// class.
    /// </summary>
    public SettingsWindow()
    {
        _viewModel = Ioc.Default.GetRequiredService<SettingsWindowViewModel>();

        ExtendsContentIntoTitleBar = true;

        Closed += SettingsWindow_Closed;

        SetTitleBar(TitleBar);

        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshTitleBarColors(LayoutRoot.RequestedTheme);
    }

    private void SettingsWindow_Closed(object sender, WindowEventArgs e)
    {
        _viewModel.NotifyWindowClosed();
        _viewModel.Dispose();
    }
    #endregion

    #region Instance methods
    private void RefreshTitleBarColors(ElementTheme elementTheme)
    {
        if (elementTheme is ElementTheme.Light)
        {
            AppWindowConfigurator.ApplyLightTitleBarColors(AppWindow);
        }
        else
        {
            AppWindowConfigurator.ApplyDarkTitleBarColors(AppWindow);
        }
    }

    /// <inheritdoc/>
    public void ApplyConfiguration()
    {
        AppWindow window = AppWindow;

        if (window.Presenter is not OverlappedPresenter presenter)
        {
            presenter = OverlappedPresenter.Create();

            window.SetPresenter(presenter);
        }

        double scaleFactor = this.GetCurrentDpiScaleFactor();

        int minimumHeight = (int)(MinimumHeight * scaleFactor);
        int minimumWidth  = (int)(MinimumWidth  * scaleFactor);

        presenter.PreferredMinimumWidth  = minimumWidth;
        presenter.PreferredMinimumHeight = minimumHeight;

        window.Resize(minimumWidth, minimumHeight);
        window.SetIcon(Icons.IconPath);
    }
    #endregion
}