using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Foundation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentNoiseGenerator.Features.Settings.UI.ViewModels;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindowViewModel : ObservableObject, IDisposable
{
    #region Instance fields
    private readonly IMessenger _messenger;
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets an enumerable of available application themes.
    /// </summary>
    public IEnumerable<object> AvailableApplicationThemes { get; } = [];

    /// <summary>
    /// Gets an enumerable of available audio sample rates.
    /// </summary>
    public IEnumerable<object> AvailableAudioSampleRates { get; } = [];

    /// <summary>
    /// Gets an enumerable of available languages.
    /// </summary>
    public IEnumerable<object> AvailableLanguages { get; } = [];

    /// <summary>
    /// Gets an enumerable of available noise presets.
    /// </summary>
    public IEnumerable<object> AvailableNoisePresets { get; } = [];

    /// <summary>
    /// Gets an enumerable of available system backdrops.
    /// </summary>
    public IEnumerable<object> AvailableSystemBackdrops { get; } = [];

    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    public object? SelectedApplicationTheme { get; set; }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    public int? SelectedAudioSampleRate { get; set; }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    public object? SelectedLanguage { get; set; }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    public string? SelectedDefaultNoisePreset { get; set; }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    public object? SelectedSystemBackdrop { get; set; }

    /// <summary>
    /// Gets or sets the window title.
    /// </summary>
    [ObservableProperty]
    public partial string? Title { get; set; }
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindowViewModel"/>
    /// class using the specified dependencies.
    /// </summary>
    /// <param name="messenger">
    /// The messenger used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any parameter is <see langword="null"/>.
    /// </exception>
    public SettingsWindowViewModel(IMessenger messenger)
    {
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger = messenger;

        RegisterMessageHandlers();
    }
    #endregion

    #region Message handlers
    private void HandleApplicationThemeUpdatedMessage(
        object                         recipient,
        ApplicationThemeUpdatedMessage message)
    {
        SelectedApplicationTheme = AvailableApplicationThemes.FirstOrDefault(
            theme => message.Value == theme
        );
    }
    #endregion

    #region Instance methods
    private void RegisterMessageHandlers()
    {
        _messenger.Register<ApplicationThemeUpdatedMessage>(
            this,
            HandleApplicationThemeUpdatedMessage
        );
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _messenger.UnregisterAll(this);
    }
    
    /// <summary>
    /// Sends a <see cref="SettingsWindowClosedMessage"/> to notify
    /// that the window has closed.
    /// </summary>
    public void NotifyWindowClosed()
    {
        _messenger.Send(new SettingsWindowClosedMessage());
    }
    #endregion
}