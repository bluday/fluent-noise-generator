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
    /// Identifies the <see cref="AboutDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AboutDescriptionProperty =
        DependencyProperty.Register(
            nameof(AboutDescription),
            typeof(string),
            typeof(SettingsAboutSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="AppDisplayName"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AppDisplayNameProperty =
        DependencyProperty.Register(
            nameof(AppDisplayName),
            typeof(string),
            typeof(SettingsAboutSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="ApplicationVersionText"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ApplicationVersionTextProperty =
        DependencyProperty.Register(
            nameof(ApplicationVersionText),
            typeof(string),
            typeof(SettingsAboutSection),
            new PropertyMetadata(defaultValue: null)
        );

    /// <summary>
    /// Identifies the <see cref="Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SettingsAboutSection),
            new PropertyMetadata(defaultValue: null)
        );

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
    /// Gets or sets the description text displayed in the About expander.
    /// </summary>
    public string AboutDescription
    {
        get => (string)GetValue(AboutDescriptionProperty);
        set => SetValue(AboutDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the application's display name.
    /// </summary>
    public string AppDisplayName
    {
        get => (string)GetValue(AppDisplayNameProperty);
        set => SetValue(AppDisplayNameProperty, value);
    }

    /// <summary>
    /// Gets or sets the application version text.
    /// </summary>
    public string ApplicationVersionText
    {
        get => (string)GetValue(ApplicationVersionTextProperty);
        set => SetValue(ApplicationVersionTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text.
    /// </summary>
    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

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