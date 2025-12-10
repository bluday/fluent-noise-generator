using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.UI.Controls;

/// <summary>
/// Interaction logic for SettingsGeneralSection.xaml.
/// </summary>
public sealed partial class SettingsGeneralSection : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AutoplayOnLaunchSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AutoplayOnLaunchSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(AutoplayOnLaunchSettingsCardDescription),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AutoplayOnLaunchSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AutoplayOnLaunchSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(AutoplayOnLaunchSettingsCardHeader),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AutoplayOnLaunchToggleSwitchOff"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AutoplayOnLaunchToggleSwitchOffProperty = DependencyProperty.Register(
        nameof(AutoplayOnLaunchToggleSwitchOff),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AutoplayOnLaunchToggleSwitchOn"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AutoplayOnLaunchToggleSwitchOnProperty = DependencyProperty.Register(
        nameof(AutoplayOnLaunchToggleSwitchOn),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableLanguages"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableLanguagesProperty = DependencyProperty.Register(
        nameof(AvailableLanguages),
        typeof(object),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableNoisePresets"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableNoisePresetsProperty = DependencyProperty.Register(
        nameof(AvailableNoisePresets),
        typeof(object),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="DefaultNoisePresetSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefaultNoisePresetSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(DefaultNoisePresetSettingsCardDescription),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="DefaultNoisePresetSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DefaultNoisePresetSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(DefaultNoisePresetSettingsCardHeader),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="LanguageSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LanguageSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(LanguageSettingsCardDescription),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="LanguageSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LanguageSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(LanguageSettingsCardHeader),
        typeof(string),
        typeof(SettingsGeneralSection),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the description text for the autoplay-on-launch settings card.
    /// </summary>
    public string AutoplayOnLaunchSettingsCardDescription
    {
        get => (string)GetValue(AutoplayOnLaunchSettingsCardDescriptionProperty);
        set => SetValue(AutoplayOnLaunchSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the autoplay-on-launch settings card.
    /// </summary>
    public string AutoplayOnLaunchSettingsCardHeader
    {
        get => (string)GetValue(AutoplayOnLaunchSettingsCardHeaderProperty);
        set => SetValue(AutoplayOnLaunchSettingsCardHeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the "Off" text for the autoplay-on-launch toggle switch.
    /// </summary>
    public string AutoplayOnLaunchToggleSwitchOff
    {
        get => (string)GetValue(AutoplayOnLaunchToggleSwitchOffProperty);
        set => SetValue(AutoplayOnLaunchToggleSwitchOffProperty, value);
    }

    /// <summary>
    /// Gets or sets the "On" text for the autoplay-on-launch toggle switch.
    /// </summary>
    public string AutoplayOnLaunchToggleSwitchOn
    {
        get => (string)GetValue(AutoplayOnLaunchToggleSwitchOnProperty);
        set => SetValue(AutoplayOnLaunchToggleSwitchOnProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available language collection.
    /// </summary>
    public object AvailableLanguages
    {
        get => GetValue(AvailableLanguagesProperty);
        set => SetValue(AvailableLanguagesProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available noise preset collection.
    /// </summary>
    public object AvailableNoisePresets
    {
        get => GetValue(AvailableNoisePresetsProperty);
        set => SetValue(AvailableNoisePresetsProperty, value);
    }

    /// <summary>
    /// Gets or sets the description text for the default noise preset settings card.
    /// </summary>
    public string DefaultNoisePresetSettingsCardDescription
    {
        get => (string)GetValue(DefaultNoisePresetSettingsCardDescriptionProperty);
        set => SetValue(DefaultNoisePresetSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the default noise preset settings card.
    /// </summary>
    public string DefaultNoisePresetSettingsCardHeader
    {
        get => (string)GetValue(DefaultNoisePresetSettingsCardHeaderProperty);
        set => SetValue(DefaultNoisePresetSettingsCardHeaderProperty, value);
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
    /// Gets or sets the description text for the language settings card.
    /// </summary>
    public string LanguageSettingsCardDescription
    {
        get => (string)GetValue(LanguageSettingsCardDescriptionProperty);
        set => SetValue(LanguageSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the language settings card.
    /// </summary>
    public string LanguageSettingsCardHeader
    {
        get => (string)GetValue(LanguageSettingsCardHeaderProperty);
        set => SetValue(LanguageSettingsCardHeaderProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsGeneralSection"/> class.
    /// </summary>
    public SettingsGeneralSection()
    {
        InitializeComponent();
    }
    #endregion
}