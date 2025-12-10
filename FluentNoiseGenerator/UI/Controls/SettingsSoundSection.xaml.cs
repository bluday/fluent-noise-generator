using FluentNoiseGenerator.Common.Localization;
using Microsoft.UI.Xaml;

namespace FluentNoiseGenerator.UI.Controls;

/// <summary>
/// Interaction logic for SettingsSoundSection.xaml.
/// </summary>
public sealed partial class SettingsSoundSection : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AudioSampleRateSettingsCardDescription"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AudioSampleRateSettingsCardDescriptionProperty = DependencyProperty.Register(
        nameof(AudioSampleRateSettingsCardDescription),
        typeof(string),
        typeof(SettingsSoundSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AudioSampleRateSettingsCardHeader"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AudioSampleRateSettingsCardHeaderProperty = DependencyProperty.Register(
        nameof(AudioSampleRateSettingsCardHeader),
        typeof(string),
        typeof(SettingsSoundSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="AvailableAudioSampleRates"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableAudioSampleRatesProperty = DependencyProperty.Register(
        nameof(AvailableAudioSampleRates),
        typeof(object),
        typeof(SettingsSoundSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(string),
        typeof(SettingsSoundSection),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the description text for the audio sample rate settings card.
    /// </summary>
    public string AudioSampleRateSettingsCardDescription
    {
        get => (string)GetValue(AudioSampleRateSettingsCardDescriptionProperty);
        set => SetValue(AudioSampleRateSettingsCardDescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text for the audio sample rate settings card.
    /// </summary>
    public string AudioSampleRateSettingsCardHeader
    {
        get => (string)GetValue(AudioSampleRateSettingsCardHeaderProperty);
        set => SetValue(AudioSampleRateSettingsCardHeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the available audio sample rate collection.
    /// </summary>
    public object AvailableAudioSampleRates
    {
        get => GetValue(AvailableAudioSampleRatesProperty);
        set => SetValue(AvailableAudioSampleRatesProperty, value);
    }

    /// <summary>
    /// Gets or sets the header text.
    /// </summary>
    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSoundSection"/> class.
    /// </summary>
    public SettingsSoundSection()
    {
        InitializeComponent();
    }
    #endregion
}