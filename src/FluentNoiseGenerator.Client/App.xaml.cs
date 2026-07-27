using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Features.Playback.UI.Windows;
using FluentNoiseGenerator.Features.Settings.UI.Windows;
using FluentNoiseGenerator.Foundation.Messages;
using FluentNoiseGenerator.UI.Windowing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Client;

/// <summary>
/// Provides application-specific behavior to supplement the base class.
/// </summary>
public sealed partial class App : Application
{
    #region Instance fields
    private PlaybackWindow? _playbackWindow;

    private SettingsWindow? _settingsWindow;

    private readonly ServiceProvider _rootServiceProvider;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        _rootServiceProvider = ServiceProviderFactory.Create();

        RegisterMessageHandlers();

        InitializeComponent();
    }
    #endregion

    #region Message handlers
    private void HandleClosePlaybackWindowMessage(object sender, ClosePlaybackWindowMessage message)
    {
        CloseWindow(ref _playbackWindow);
    }

    private void HandleCloseSettingsWindowMessage(object sender, CloseSettingsWindowMessage message)
    {
        CloseWindow(ref _settingsWindow);
    }

    private void HandleOpenPlaybackWindowMessage(object sender, OpenPlaybackWindowMessage message)
    {
        CreateAndActivateWindow(ref _playbackWindow);
    }

    private void HandleOpenSettingsWindowMessage(object sender, OpenSettingsWindowMessage message)
    {
        CreateAndActivateWindow(ref _settingsWindow);
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers()
    {
        Subscribe<ClosePlaybackWindowMessage>(HandleClosePlaybackWindowMessage);
        Subscribe<CloseSettingsWindowMessage>(HandleCloseSettingsWindowMessage);
        Subscribe<OpenPlaybackWindowMessage>(HandleOpenPlaybackWindowMessage);
        Subscribe<OpenSettingsWindowMessage>(HandleOpenSettingsWindowMessage);
    }

    private void Subscribe<TMessage>(MessageHandler<object, TMessage> handler)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Register(this, handler);
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="e">
    /// Details about the launch request and process.
    /// </param>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        CreateAndActivateWindow(ref _playbackWindow);
    }
    #endregion

    #region Static methods
    private static bool CloseWindow<TWindow>(ref TWindow? window)
        where TWindow : Window
    {
        if (window is null) return false;

        window.Close();

        window = null;

        return true;
    }

    private static void CreateAndActivateWindow<TWindow>(ref TWindow? window)
        where TWindow : Window, new()
    {
        if (CreateWindow(ref window))
        {
            window!.Activate();
        }
    }

    private static bool CreateWindow<TWindow>(ref TWindow? window)
        where TWindow : Window, new()
    {
        if (window is not null)
        {
            return false;
        }

        window = new();

        if (window is IConfigurableWindow configurableWindow)
        {
            configurableWindow.ApplyConfiguration();
        }

        return true;
    }
    #endregion
}