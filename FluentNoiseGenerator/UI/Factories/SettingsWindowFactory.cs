using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.Resources;
using FluentNoiseGenerator.Common.Services;
using FluentNoiseGenerator.Core.Services;
using FluentNoiseGenerator.UI.ViewModels;
using FluentNoiseGenerator.UI.Windows;
using System;

namespace FluentNoiseGenerator.UI.Factories;

/// <summary>
/// Represents a factory for creating <see cref="SettingsWindow"/> instances.
/// </summary>
internal sealed class SettingsWindowFactory
{
    #region Fields
    private readonly LanguageService _languageService;

    private readonly LocalizedResourceProvider _localizedResourceProvider;

    private readonly NoisePlaybackService _noisePlaybackService;

    private readonly IMessenger _messenger;

    private readonly AppStringResources _stringResources;

    private readonly ThemeService _themeService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class using the
    /// specified dependencies.
    /// </summary>
    /// <param name="noisePlaybackService">
    /// The noise playback service for managing playback within the app.
    /// </param>
    /// <param name="languageService">
    /// The language service for managing the current application language.
    /// </param>
    /// <param name="themeService">
    /// The theme service for managing the current application theme.
    /// </param>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    internal SettingsWindowFactory(
        NoisePlaybackService      noisePlaybackService,
        LanguageService           languageService,
        ThemeService              themeService,
        LocalizedResourceProvider localizedResourceProvider,
        AppStringResources        stringResources,
        IMessenger                messenger)
    {
        ArgumentNullException.ThrowIfNull(noisePlaybackService);
        ArgumentNullException.ThrowIfNull(languageService);
        ArgumentNullException.ThrowIfNull(themeService);
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(messenger);

        _noisePlaybackService      = noisePlaybackService;
        _languageService           = languageService;
        _themeService              = themeService;
        _localizedResourceProvider = localizedResourceProvider;
        _messenger                 = messenger;
        _stringResources           = stringResources;
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
                _noisePlaybackService,
                _languageService,
                _themeService,
                _localizedResourceProvider,
                _stringResources.SettingsWindow,
                _messenger
            )
        };
    }
    #endregion
}