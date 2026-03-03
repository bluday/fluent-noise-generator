using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentNoiseGenerator.UI.Settings.Controls;

/// <summary>
/// Interaction logic for SettingsTitleBar.xaml.
/// </summary>
public sealed partial class SettingsTitleBar : Control
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(SettingsTitleBar),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string? Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsTitleBar"/> class.
    /// </summary>
    public SettingsTitleBar()
    {
        DefaultStyleKey = typeof(SettingsTitleBar);
    }
    #endregion
}