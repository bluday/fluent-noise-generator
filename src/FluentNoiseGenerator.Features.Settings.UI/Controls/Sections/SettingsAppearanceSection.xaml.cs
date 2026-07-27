using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FluentNoiseGenerator.Features.Settings.UI.Controls;

/// <summary>
/// Interaction logic for SettingsAppearanceSection.xaml.
/// </summary>
public sealed partial class SettingsAppearanceSection : UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AvailableApplicationThemes"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableApplicationThemesProperty = DependencyProperty.Register(
        nameof(AvailableApplicationThemes),
        typeof(IEnumerable<object>),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableSystemBackdrops"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableSystemBackdropsProperty = DependencyProperty.Register(
        nameof(AvailableSystemBackdrops),
        typeof(IEnumerable<object>),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SelectedApplicationTheme"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedApplicationThemeProperty = DependencyProperty.Register(
        nameof(SelectedApplicationTheme),
        typeof(object),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SelectedSystemBackdrop"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedSystemBackdropProperty = DependencyProperty.Register(
        nameof(SelectedSystemBackdrop),
        typeof(object),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets or sets an enumerable with available application themes.
    /// </summary>
    public IEnumerable<object> AvailableApplicationThemes
    {
        get => (IEnumerable<object>)GetValue(AvailableApplicationThemesProperty);
        set => SetValue(AvailableApplicationThemesProperty, value);
    }

    /// <summary>
    /// Gets or sets an enumerable with available system backdrops.
    /// </summary>
    public IEnumerable<object> AvailableSystemBackdrops
    {
        get => (IEnumerable<object>)GetValue(AvailableSystemBackdropsProperty);
        set => SetValue(AvailableSystemBackdropsProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected application theme.
    /// </summary>
    public object? SelectedApplicationTheme
    {
        get => GetValue(SelectedApplicationThemeProperty);
        set => SetValue(SelectedApplicationThemeProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected system backdrop.
    /// </summary>
    public object? SelectedSystemBackdrop
    {
        get => GetValue(SelectedSystemBackdropProperty);
        set => SetValue(SelectedSystemBackdropProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsAppearanceSection"/>
    /// class.
    /// </summary>
    public SettingsAppearanceSection()
    {
        InitializeComponent();
    }
    #endregion
}