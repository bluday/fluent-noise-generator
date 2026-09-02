using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Foundation.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Client;

/// <summary>
/// Provides application-specific behavior to supplement the base class.
/// </summary>
public sealed partial class App : Application
{
    #region Instance fields
    private readonly ServiceProvider _rootServiceProvider;

    private readonly WindowService _windowService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _rootServiceProvider = ServiceProviderFactory.Create();

        _windowService = new WindowService();

        InitializeComponent();
    }
    #endregion

    #region Instance methods
    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="e">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        _windowService.OpenPlaybackWindow();
    }
    #endregion
}