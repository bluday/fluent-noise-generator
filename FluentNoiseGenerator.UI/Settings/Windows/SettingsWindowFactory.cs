using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.UI.Common.Resources;
using FluentNoiseGenerator.UI.Settings.Resources;
using FluentNoiseGenerator.UI.Settings.ViewModels;
using System;

namespace FluentNoiseGenerator.UI.Settings.Windows;

/// <summary>
/// Represents a factory for creating <see cref="SettingsWindow"/> instances.
/// </summary>
public sealed class SettingsWindowFactory
{
    #region Fields
    private readonly IAppSettings _appSettings;

    private readonly IMessenger _messenger;

    private readonly SettingsWindowResources _resources;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class using the
    /// specified dependencies.
    /// </summary>
    /// <param name="appResources">
    /// An <see cref="AppResources"/> instance consisting of localized app resources.
    /// </param>
    /// <param name="appSettings">
    /// An <see cref="IAppSettings"/> instance with current settings for the application.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsWindowFactory(
        AppResources appResources,
        IAppSettings appSettings,
        IMessenger   messenger)
    {
        ArgumentNullException.ThrowIfNull(appResources);
        ArgumentNullException.ThrowIfNull(appSettings);
        ArgumentNullException.ThrowIfNull(messenger);

        _resources   = new SettingsWindowResources();
        _appSettings = appSettings;
        _messenger   = messenger;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Creates a new <see cref="SettingsWindow"/> instance with its required dependencies.
    /// </summary>
    /// <returns>
    /// The created window instance.
    /// </returns>
    public SettingsWindow Create()
    {
        return new()
        {
            ViewModel = new SettingsViewModel(_resources, _appSettings, _messenger)
        };
    }
    #endregion
}