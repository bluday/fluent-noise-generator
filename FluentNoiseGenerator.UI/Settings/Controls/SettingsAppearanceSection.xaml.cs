using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Settings.Controls;

/// <summary>
/// Interaction logic for SettingsAppearanceSection.xaml.
/// </summary>
public sealed partial class SettingsAppearanceSection : UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AvailableApplicationThemes"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableApplicationThemesProperty =
        DependencyProperty.Register(
            nameof(AvailableApplicationThemes),
            typeof(IEnumerable<object>),
            typeof(SettingsAppearanceSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="AvailableSystemBackdrops"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableSystemBackdropsProperty =
        DependencyProperty.Register(
            nameof(AvailableSystemBackdrops),
            typeof(IEnumerable<SystemBackdrop>),
            typeof(SettingsAppearanceSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="SelectedApplicationTheme"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedApplicationThemeProperty =
        DependencyProperty.Register(
            nameof(SelectedApplicationTheme),
            typeof(object),
            typeof(SettingsAppearanceSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="SelectedSystemBackdrop"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedSystemBackdropProperty =
        DependencyProperty.Register(
            nameof(SelectedSystemBackdrop),
            typeof(object),
            typeof(SettingsAppearanceSection),
            new PropertyMetadata(defaultValue: null)
        );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the items source instance for the available application themes collection.
    /// </summary>
    public IEnumerable<object> AvailableApplicationThemes
    {
        get => (IEnumerable<object>)GetValue(AvailableApplicationThemesProperty);
        set => SetValue(AvailableApplicationThemesProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available system backdrops collection.
    /// </summary>
    public IEnumerable<SystemBackdrop> AvailableSystemBackdrops
    {
        get => (IEnumerable<SystemBackdrop>)GetValue(AvailableSystemBackdropsProperty);
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
    /// Initializes a new instance of the <see cref="SettingsAppearanceSection"/> class.
    /// </summary>
    public SettingsAppearanceSection()
    {
        InitializeComponent();
    }
    #endregion
}