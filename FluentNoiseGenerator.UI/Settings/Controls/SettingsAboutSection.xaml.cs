using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentNoiseGenerator.UI.Settings.Controls;

/// <summary>
/// Interaction logic for SettingsAboutSection.xaml.
/// </summary>
public sealed partial class SettingsAboutSection : UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="SessionIdentifierFormatString"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SessionIdentifierFormatStringProperty =
        DependencyProperty.Register(
            nameof(SessionIdentifierFormatString),
            typeof(string),
            typeof(SettingsAboutSection),
            new PropertyMetadata(defaultValue: null)
        );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the format string used to display the session identifier.
    /// </summary>
    public string SessionIdentifierFormatString
    {
        get => (string)GetValue(SessionIdentifierFormatStringProperty);
        set => SetValue(SessionIdentifierFormatStringProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsAboutSection"/> class.
    /// </summary>
    public SettingsAboutSection()
    {
        InitializeComponent();
    }
    #endregion
}