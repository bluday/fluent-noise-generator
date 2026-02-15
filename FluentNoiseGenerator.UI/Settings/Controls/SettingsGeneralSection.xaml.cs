using FluentNoiseGenerator.Common.Globalization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Settings.Controls;

/// <summary>
/// Interaction logic for SettingsGeneralSection.xaml.
/// </summary>
public sealed partial class SettingsGeneralSection : UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AvailableLanguages"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableLanguagesProperty =
        DependencyProperty.Register(
            nameof(AvailableLanguages),
            typeof(IEnumerable<ILanguage>),
            typeof(SettingsGeneralSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="AvailableNoisePresets"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableNoisePresetsProperty =
        DependencyProperty.Register(
            nameof(AvailableNoisePresets),
            typeof(IEnumerable<string>),
            typeof(SettingsGeneralSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="SelectedDefaultNoisePreset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedDefaultNoisePresetProperty =
        DependencyProperty.Register(
            nameof(SelectedDefaultNoisePreset),
            typeof(object),
            typeof(SettingsGeneralSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="SelectedLanguage"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedLanguageProperty =
        DependencyProperty.Register(
            nameof(SelectedLanguage),
            typeof(object),
            typeof(SettingsGeneralSection),
            new PropertyMetadata(defaultValue: null)
        );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the items source instance for the available language collection.
    /// </summary>
    public IEnumerable<ILanguage> AvailableLanguages
    {
        get => (IEnumerable<ILanguage>)GetValue(AvailableLanguagesProperty);
        set => SetValue(AvailableLanguagesProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available noise preset collection.
    /// </summary>
    public IEnumerable<string> AvailableNoisePresets
    {
        get => (IEnumerable<string>)GetValue(AvailableNoisePresetsProperty);
        set => SetValue(AvailableNoisePresetsProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected default noise preset.
    /// </summary>
    public object? SelectedDefaultNoisePreset
    {
        get => GetValue(SelectedDefaultNoisePresetProperty);
        set => SetValue(SelectedDefaultNoisePresetProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected application language.
    /// </summary>
    public object? SelectedLanguage
    {
        get => GetValue(SelectedLanguageProperty);
        set => SetValue(SelectedLanguageProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsGeneralSection"/> class.
    /// </summary>
    public SettingsGeneralSection()
    {
        InitializeComponent();
    }
    #endregion
}