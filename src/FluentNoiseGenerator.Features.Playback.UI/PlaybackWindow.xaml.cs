using FluentNoiseGenerator.Foundation.Paths;
using FluentNoiseGenerator.UI.Extensions;

using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using System;

namespace FluentNoiseGenerator.Features.Playback.UI;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlaybackWindow : Window
{
    #region Constants
    private const int MinimumHeight = 110;

    private const int MinimumWidth = 170;
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets the view model.
    /// </summary>
    public PlaybackWindowViewModel ViewModel { get; }
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindow"/>
    /// class.
    /// </summary>
    /// <param name="viewModel">
    /// The view model.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="viewModel"/> is <see langword="null"/>.
    /// </exception>
    public PlaybackWindow(PlaybackWindowViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        ExtendsContentIntoTitleBar = true;

        ViewModel = viewModel;

        Closed += PlaybackWindow_Closed;

        ConfigureAppWindow();

        InitializeComponent();

        SetTitleBar(TopBar);
    }
    #endregion

    #region Event handlers
    private void PlaybackWindow_Closed(object? sender, WindowEventArgs e)
    {
        Closed -= PlaybackWindow_Closed;
    }
    #endregion

    #region Instance methods
    private void ConfigureAppWindow()
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

        presenter.SetBorderAndTitleBar(
            hasBorder:   true,
            hasTitleBar: false
        );

        double scaleFactor = this.GetCurrentDpiScaleFactor();

        int minimumHeight = (int)(MinimumHeight * scaleFactor);
        int minimumWidth  = (int)(MinimumWidth  * scaleFactor);

        appWindow.Resize(minimumWidth, minimumHeight);
        appWindow.MoveToCenter();
        appWindow.SetIcon(Icons.WindowIcon.FullName);
    }
    #endregion
}