using FluentNoiseGenerator.Foundation.Paths;
using FluentNoiseGenerator.UI.Extensions;

using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using System;

namespace FluentNoiseGenerator.Features.Settings.UI;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    #region Constants
    private const int MinimumHeight = 700;

    private const int MinimumWidth = 700;
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets the view model.
    /// </summary>
    public SettingsWindowViewModel ViewModel { get; }
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/>
    /// class.
    /// </summary>
    /// <param name="viewModel">
    /// The view model.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="viewModel"/> is <see langword="null"/>.
    /// </exception>
    public SettingsWindow(SettingsWindowViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        ExtendsContentIntoTitleBar = true;

        ViewModel = viewModel;

        ConfigureAppWindow();

        InitializeComponent();

        SetTitleBar(TitleBar);
    }
    #endregion

    #region Instance methods
    private void ConfigureAppWindow()
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
        window.SetIcon(Icons.WindowIcon.FullName);
    }
    #endregion
}