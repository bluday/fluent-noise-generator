using FluentNoiseGenerator.Factories;
using FluentNoiseGenerator.UI.Windows;
using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.Services;

/// <summary>
/// Service for managing the main and settings windows. This class ensures that windows are
/// properly created, restored, and updated when necessary.
/// </summary>
internal sealed class WindowService
{
    #region Fields
    private PlaybackWindow? _playbackWindow;

    private SettingsWindow? _settingsWindow;

    private readonly SettingsWindowFactory _settingsWindowFactory;

    private readonly PlaybackWindowFactory _playbackWindowFactory;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowService"/> class.
    /// </summary>
    /// <param name="playbackWindowFactory">
    /// The <see cref="PlaybackWindowFactory"/> factory for creating new instances.
    /// </param>
    /// <param name="settingsWindowFactory">
    /// The <see cref="SettingsWindowFactory"/> factory for creating new instances.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    internal WindowService(
        PlaybackWindowFactory playbackWindowFactory,
        SettingsWindowFactory settingsWindowFactory)
    {
        ArgumentNullException.ThrowIfNull(playbackWindowFactory);
        ArgumentNullException.ThrowIfNull(settingsWindowFactory);

        _playbackWindowFactory = playbackWindowFactory;
        _settingsWindowFactory = settingsWindowFactory;
    }
    #endregion

    #region Event handlers
    private void _playbackWindow_Closed(object sender, WindowEventArgs args)
    {
        _playbackWindow!.Closed               -= _playbackWindow_Closed;
        _playbackWindow.SettingsButtonClicked -= _playbackWindow_SettingsButtonClicked;
    }

    private void _playbackWindow_SettingsButtonClicked(object? sender, EventArgs e)
    {
        ShowSettingsWindow();
    }

    private void _settingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _settingsWindow!.Closed -= _settingsWindow_Closed;
    }
    #endregion

    #region Methods
    private void CreatePlaybackWindow()
    {
        _playbackWindow = _playbackWindowFactory.Create();

        _playbackWindow.Closed                += _playbackWindow_Closed;
        _playbackWindow.SettingsButtonClicked += _playbackWindow_SettingsButtonClicked;

        _playbackWindow.Activate();
    }

    private void CreateSettingsWindow()
    {
        _settingsWindow = _settingsWindowFactory.Create();

        _settingsWindow.Closed += _settingsWindow_Closed;

        _settingsWindow.Activate();
    }

    private void RestorePlaybackWindow()
    {
        if (_playbackWindow is null) return;

        _playbackWindow.Restore();
        _playbackWindow.Focus();
    }

    private void RestoreSettingsWindow()
    {
        if (_settingsWindow is null) return;

        _settingsWindow.Restore();
        _settingsWindow.Focus();
    }

    /// <summary>
    /// Displays the playback window, setting up necessary configurations like DPI scaling, native
    /// app window settings, and the title bar.
    /// </summary>
    public void ShowPlaybackWindow()
    {
        if (_playbackWindow?.HasClosed is false)
        {
            RestorePlaybackWindow();

            return;
        }

        CreatePlaybackWindow();
    }

    /// <summary>
    /// Displays the settings window, either restoring it if it is open already or creating
    /// a new instance if it was closed. Listens for theme and backdrop changes.
    /// </summary>
    /// <remarks>
    /// Restores the window if it was previously closed. Otherwise, a new instance is created,
    /// and event handlers for theme and backdrop changes are registered.
    /// </remarks>
    public void ShowSettingsWindow()
    {
        if (_settingsWindow?.HasClosed is false)
        {
            RestoreSettingsWindow();

            return;
        }

        CreateSettingsWindow();
    }
    #endregion
}