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
    /// Identifies the <see cref="Languages"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LanguagesProperty = DependencyProperty.Register(
        nameof(Languages),
        typeof(IEnumerable<object>),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="NoisePresets"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty NoisePresetsProperty = DependencyProperty.Register(
        nameof(NoisePresets),
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
    public IEnumerable<object>Languages
    {
        get => (IEnumerable<object>)GetValue(LanguagesProperty);
        set => SetValue(LanguagesProperty, value);
    }

    /// <summary>
    /// Gets or sets an enumerable with available noise presets.
    /// </summary>
    public IEnumerable<object>NoisePresets
    {
        get => (IEnumerable<object>)GetValue(NoisePresetsProperty);
        set => SetValue(NoisePresetsProperty, value);
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

    #region Instance constructor
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