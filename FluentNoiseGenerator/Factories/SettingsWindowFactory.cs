using CommunityToolkit.Mvvm.Messaging;
using FluentNoiseGenerator.Common.Localization;
using FluentNoiseGenerator.Common.StringResources;
using FluentNoiseGenerator.UI.ViewModels;
using FluentNoiseGenerator.UI.Windows;
using System;

namespace FluentNoiseGenerator.Factories;

/// <summary>
/// Represents a factory for creating <see cref="SettingsWindow"/> instances.
/// </summary>
internal sealed class SettingsWindowFactory
{
    #region Fields
    private readonly IMessenger _messenger;

    private readonly AppStringResources _stringResources;

    private readonly LocalizedResourceProvider _localizedResourceProvider;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsViewModel"/> class using the
    /// specified string resource collection and event messenger.
    /// </summary>
    /// <param name="stringResources">
    /// The string resource collection instance for retreiving localized resources.
    /// </param>
    /// <param name="localizedResourceProvider">
    /// The localized resource provider for retrieving localized resource values.
    /// </param>
    /// <param name="messenger">
    /// The messenger instance used for sending messages within the application.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when any of the parameters is <c>null</c>.
    /// </exception>
    internal SettingsWindowFactory(
        AppStringResources        stringResources,
        LocalizedResourceProvider localizedResourceProvider,
        IMessenger                messenger)
    {
        ArgumentNullException.ThrowIfNull(stringResources);
        ArgumentNullException.ThrowIfNull(localizedResourceProvider);
        ArgumentNullException.ThrowIfNull(messenger);

        _messenger                 = messenger;
        _stringResources           = stringResources;
        _localizedResourceProvider = localizedResourceProvider;
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
                _stringResources.SettingsWindow,
                _localizedResourceProvider,
                _messenger
            )
        };
    }
    #endregion
}