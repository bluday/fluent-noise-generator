using System;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Features.Playback.UI.Windows;
using FluentNoiseGenerator.Features.Settings.UI.Windows;
using FluentNoiseGenerator.Foundation.Messages;
using FluentNoiseGenerator.UI.Windowing;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.Client;

/// <summary>
/// Represents a service for managing windows within the application.
/// </summary>
public sealed partial class WindowService : IDisposable
{
    #region Instance fields
    private bool _isDisposed;

    private PlaybackWindow? _playbackWindow;

    private SettingsWindow? _settingsWindow;
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowService"/>
    /// class.
    /// </summary>
    public WindowService()
    {
        RegisterMessageHandlers();
    }
    #endregion

    #region Instance methods
    public void ClosePlaybackWindow()
    {
        CloseWindow(ref _playbackWindow);
    }

    public void CloseSettingsWindow()
    {
        CloseWindow(ref _settingsWindow);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_isDisposed) return;

        WeakReferenceMessenger.Default.UnregisterAll(this);

        _isDisposed = true;
    }

    public void OpenPlaybackWindow()
    {
        CreateAndActivateWindow(ref _playbackWindow);
    }

    public void OpenSettingsWindow()
    {
        CreateAndActivateWindow(ref _settingsWindow);
    }

    private void RegisterMessageHandlers()
    {
        Subscribe<ClosePlaybackWindowMessage>(
            (sender, message) => ClosePlaybackWindow()
        );

        Subscribe<CloseSettingsWindowMessage>(
            (sender, message) => CloseSettingsWindow()
        );

        Subscribe<OpenPlaybackWindowMessage>(
            (sender, message) => OpenPlaybackWindow()
        );

        Subscribe<OpenSettingsWindowMessage>(
            (sender, message) => OpenSettingsWindow()
        );
    }

    private void Subscribe<TMessage>(MessageHandler<object, TMessage> handler)
        where TMessage : class
    {
        WeakReferenceMessenger.Default.Register(this, handler);
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