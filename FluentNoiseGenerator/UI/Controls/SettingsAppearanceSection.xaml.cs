using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.UI.Controls;

/// <summary>
/// Interaction logic for SettingsAppearanceSection.xaml.
/// </summary>
public sealed partial class SettingsAppearanceSection : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AlwaysOnTopSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AlwaysOnTopSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(AlwaysOnTopSettingsCardDescription),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AlwaysOnTopSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AlwaysOnTopSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(AlwaysOnTopSettingsCardHeader),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="ApplicationThemeSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ApplicationThemeSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(ApplicationThemeSettingsCardDescription),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="ApplicationThemeSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ApplicationThemeSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(ApplicationThemeSettingsCardHeader),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableApplicationThemes"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableApplicationThemesProperty = DependencyProperty.Register(
        nameof(AvailableApplicationThemes),
        typeof(object),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableSystemBackdrops"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableSystemBackdropsProperty = DependencyProperty.Register(
        nameof(AvailableSystemBackdrops),
        typeof(object),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SystemBackdropSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SystemBackdropSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(SystemBackdropSettingsCardDescription),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SystemBackdropSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SystemBackdropSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(SystemBackdropSettingsCardHeader),
        typeof(string),
        typeof(SettingsAppearanceSection),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the description text for the Always on Top settings card.
    /// </summary>
    public string AlwaysOnTopSettingsCardDescription
    {
        get => (string)GetValue(AlwaysOnTopSettingsCardDescriptionProperty);
        set => SetValue(AlwaysOnTopSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the Always on Top settings card.
    /// </summary>
    public string AlwaysOnTopSettingsCardHeader
    {
        get => (string)GetValue(AlwaysOnTopSettingsCardHeaderProperty);
        set => SetValue(AlwaysOnTopSettingsCardHeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the description text for the application theme settings card.
    /// </summary>
    public string ApplicationThemeSettingsCardDescription
    {
        get => (string)GetValue(ApplicationThemeSettingsCardDescriptionProperty);
        set => SetValue(ApplicationThemeSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the application theme settings card.
    /// </summary>
    public string ApplicationThemeSettingsCardHeader
    {
        get => (string)GetValue(ApplicationThemeSettingsCardHeaderProperty);
        set => SetValue(ApplicationThemeSettingsCardHeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available application themes collection.
    /// </summary>
    public object AvailableApplicationThemes
    {
        get => GetValue(AvailableApplicationThemesProperty);
        set => SetValue(AvailableApplicationThemesProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available system backdrops collection.
    /// </summary>
    public object AvailableSystemBackdrops
    {
        get => GetValue(AvailableSystemBackdropsProperty);
        set => SetValue(AvailableSystemBackdropsProperty, value);
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
    /// Gets or sets the description text for the system backdrop settings card.
    /// </summary>
    public string SystemBackdropSettingsCardDescription
    {
        get => (string)GetValue(SystemBackdropSettingsCardDescriptionProperty);
        set => SetValue(SystemBackdropSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the system backdrop settings card.
    /// </summary>
    public string SystemBackdropSettingsCardHeader
    {
        get => (string)GetValue(SystemBackdropSettingsCardHeaderProperty);
        set => SetValue(SystemBackdropSettingsCardHeaderProperty, value);
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