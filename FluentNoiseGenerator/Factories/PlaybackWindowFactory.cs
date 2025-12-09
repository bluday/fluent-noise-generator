using FluentNoiseGenerator.Services;
using FluentNoiseGenerator.UI.Windows;
using System;

namespace FluentNoiseGenerator.Factories;

/// <summary>
/// Represents a factory for creating <see cref="PlaybackWindow"/> instances.
/// </summary>
internal sealed class PlaybackWindowFactory
{
    #region Fields
    private readonly LanguageService _languageService;

    private readonly ResourceService _resourceService;

    private readonly ThemeService _themeService;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackWindowFactory"/> class.
    /// </summary>
    /// <param name="languageService">
    /// The language service for retrieving and updating application language info.
    /// </param>
    /// <param name="resourceService">
    /// The resource service for retrieving application resources.
    /// </param>
    /// <param name="themeService">
    /// The theme service for retrieving and updating the current application theme.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when any of the specified parameters are <c>null</c>.
    /// </exception>
    internal PlaybackWindowFactory(
        LanguageService languageService,
        ResourceService resourceService,
        ThemeService    themeService)
    {
        ArgumentNullException.ThrowIfNull(languageService);
        ArgumentNullException.ThrowIfNull(resourceService);
        ArgumentNullException.ThrowIfNull(themeService);

        _languageService = languageService;
        _resourceService = resourceService;
        _themeService    = themeService;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Creates a new <see cref="PlaybackWindow"/> instance with its required dependencies.
    /// </summary>
    /// <returns>
    /// The created window instance.
    /// </returns>
    public PlaybackWindow Create()
    {
        return new(_languageService, _resourceService, _themeService);
    }
    #endregion
}