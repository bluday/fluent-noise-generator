using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator.Features.Playback.UI.ViewModels;
using FluentNoiseGenerator.Foundation.Constants;
using FluentNoiseGenerator.UI.Extensions;
using FluentNoiseGenerator.UI.Windowing;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Features.Playback.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaybackWindow : Window, IConfigurableWindow
{
    #region Constants
    /// <summary>
    /// The minimum height, in pixels.
    /// </summary>
    public const int MinimumHeight = 110;

    /// <summary>
    /// The minimum width, in pixels.
    /// </summary>
    public const int MinimumWidth = 170;
    #endregion

    #region Instance fields
    private readonly PlaybackWindowViewModel _viewModel;
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets the view model.
    /// </summary>
    public PlaybackWindowViewModel ViewModel => _viewModel;
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/>
    /// class.
    /// </summary>
    public PlaybackWindow()
    {
        _viewModel = Ioc.Default.GetRequiredService<PlaybackWindowViewModel>();

        ExtendsContentIntoTitleBar = true;

        Closed += PlaybackWindow_Closed;

        SetTitleBar(TopBar);

        InitializeComponent();

        TopBar.Configure(
            this.GetCurrentDpiScaleFactor(),
            this.GetInputNonClientPointerSource()
        );

        TopBar.CanConfigureNonClientRegions = true;
    }
    #endregion

    #region Event handlers
    private void PlaybackWindow_Closed(object sender, WindowEventArgs args)
    {
        TopBar.CanConfigureNonClientRegions = false;

        _viewModel.NotifyWindowClosed();
        _viewModel.Dispose();
    }
    #endregion

    #region Instance methods
    /// <inheritdoc/>
    public void ApplyConfiguration()
    {
        AppWindow window = AppWindow;

        if (window.Presenter is not OverlappedPresenter presenter)
        {
            presenter = OverlappedPresenter.CreateForToolWindow();

            window.SetPresenter(presenter);
        }

        presenter.IsAlwaysOnTop = true;
        presenter.IsMaximizable = false;
        presenter.IsMinimizable = true;
        presenter.IsResizable   = false;

        presenter.SetBorderAndTitleBar(hasBorder: true, hasTitleBar: false);

        double scaleFactor = this.GetCurrentDpiScaleFactor();

        int minimumHeight = (int)(MinimumHeight * scaleFactor);
        int minimumWidth  = (int)(MinimumWidth  * scaleFactor);

        window.Resize(minimumWidth, minimumHeight);
        window.MoveToCenter();
        window.SetIcon(Icons.IconPath);
    }
    #endregion
}