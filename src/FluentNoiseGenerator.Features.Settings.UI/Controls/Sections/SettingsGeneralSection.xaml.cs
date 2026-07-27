using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FluentNoiseGenerator.Features.Settings.UI.Controls;

/// <summary>
/// Interaction logic for SettingsGeneralSection.xaml.
/// </summary>
public sealed partial class SettingsGeneralSection : UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AvailableLanguages"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableLanguagesProperty = DependencyProperty.Register(
        nameof(AvailableLanguages),
        typeof(IEnumerable<object>),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableNoisePresets"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableNoisePresetsProperty = DependencyProperty.Register(
        nameof(AvailableNoisePresets),
        typeof(IEnumerable<object>),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SelectedDefaultNoisePreset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedDefaultNoisePresetProperty = DependencyProperty.Register(
        nameof(SelectedDefaultNoisePreset),
        typeof(object),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SelectedLanguage"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedLanguageProperty = DependencyProperty.Register(
        nameof(SelectedLanguage),
        typeof(object),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets or sets an enumerable with available languages.
    /// </summary>
    public IEnumerable<object> AvailableLanguages
    {
        get => (IEnumerable<object>)GetValue(AvailableLanguagesProperty);
        set => SetValue(AvailableLanguagesProperty, value);
    }

    /// <summary>
    /// Gets or sets an enumerable with available noise presets.
    /// </summary>
    public IEnumerable<object> AvailableNoisePresets
    {
        get => (IEnumerable<object>)GetValue(AvailableNoisePresetsProperty);
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
    /// Initializes a new instance of the <see cref="SettingsGeneralSection"/>
    /// class.
    /// </summary>
    public SettingsGeneralSection()
    {
        InitializeComponent();
    }
    #endregion
}