using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentNoiseGenerator.UI.Settings.Controls;

/// <summary>
/// Interaction logic for SettingsSectionHeader.xaml.
/// </summary>
public sealed partial class SettingsSectionHeader : Control
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="Icon"> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(IconElement),
        typeof(SettingsSectionHeader),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(SettingsSectionHeader),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    public IconElement? Icon
    {
        get => GetValue(IconProperty) as IconElement;
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string? Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSectionHeader"/> class.
    /// </summary>
    public SettingsSectionHeader()
    {
        DefaultStyleKey = typeof(SettingsSectionHeader);
    }
    #endregion
}