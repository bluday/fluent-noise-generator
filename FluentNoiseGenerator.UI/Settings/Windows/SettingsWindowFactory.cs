using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.UI.Resources;
using FluentNoiseGenerator.UI.Services;
using FluentNoiseGenerator.UI.Settings.ViewModels;
using System;

namespace FluentNoiseGenerator.UI.Settings.Windows;

/// <summary>
/// Represents a factory for creating <see cref="SettingsWindow"/> instances.
/// </summary>
public sealed class SettingsWindowFactory
{
    #region Fields
    private readonly AppResources _appResources;

    private readonly IAppSettings _appSettings;

    private readonly LanguageService _languageService;

    private readonly IMessenger _messenger;

    private readonly ThemeService _themeService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class using the
    /// specified dependencies.
    /// </summary>
    /// <param name="appResources">
    /// An <see cref="AppResources"/> instance with localized app resources.
    /// </param>
    /// <param name="appSettings">
    /// An <see cref="IAppSettings"/> instance with current settings for the application.
    /// </param>
    /// <param name="languageService">
    /// The service for managing the application language.
    /// </param>
    /// <param name="themeService">
    /// The service for managing the application theme.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    public SettingsWindowFactory(
        AppResources    appResources,
        IAppSettings    appSettings,
        LanguageService languageService,
        ThemeService    themeService,
        IMessenger      messenger)
    {
        ArgumentNullException.ThrowIfNull(appResources);
        ArgumentNullException.ThrowIfNull(appSettings);
        ArgumentNullException.ThrowIfNull(languageService);
        ArgumentNullException.ThrowIfNull(themeService);
        ArgumentNullException.ThrowIfNull(messenger);

        _appResources    = appResources;
        _appSettings     = appSettings;
        _languageService = languageService;
        _messenger       = messenger;
        _themeService    = themeService;
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
            ViewModel = new SettingsViewModel(
                _appResources,
                _appSettings,
                _languageService,
                _themeService,
                _messenger
            )
        };
    }
    #endregion
}