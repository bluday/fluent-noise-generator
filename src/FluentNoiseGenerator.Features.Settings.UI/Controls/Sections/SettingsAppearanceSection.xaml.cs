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
    /// Identifies the <see cref="ApplicationThemes"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ApplicationThemesProperty = DependencyProperty.Register(
        nameof(ApplicationThemes),
        typeof(IEnumerable<object>),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SystemBackdrops"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SystemBackdropsProperty = DependencyProperty.Register(
        nameof(SystemBackdrops),
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
    public IEnumerable<object> ApplicationThemes
    {
        get => (IEnumerable<object>)GetValue(ApplicationThemesProperty);
        set => SetValue(ApplicationThemesProperty, value);
    }

    /// <summary>
    /// Gets or sets an enumerable with available system backdrops.
    /// </summary>
    public IEnumerable<object> SystemBackdrops
    {
        get => (IEnumerable<object>)GetValue(SystemBackdropsProperty);
        set => SetValue(SystemBackdropsProperty, value);
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

    #region Instance constructor
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