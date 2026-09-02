using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace FluentNoiseGenerator.Features.Settings.UI.Controls;

/// <summary>
/// Interaction logic for SettingsSoundSection.xaml.
/// </summary>
public sealed partial class SettingsSoundSection : UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="AvailableAudioSampleRates"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AvailableAudioSampleRatesProperty = DependencyProperty.Register(
        nameof(AvailableAudioSampleRates),
        typeof(IEnumerable<object>),
        typeof(SettingsSoundSection),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SelectedAudioSampleRate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedAudioSampleRateProperty = DependencyProperty.Register(
        nameof(SelectedAudioSampleRate),
        typeof(object),
        typeof(SettingsSoundSection),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets or sets the items source instance for the available
    /// audio sample rate collection.
    /// </summary>
    public IEnumerable<object> AvailableAudioSampleRates
    {
        get => (IEnumerable<object>)GetValue(AvailableAudioSampleRatesProperty);
        set => SetValue(AvailableAudioSampleRatesProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected audio sample rate.
    /// </summary>
    public object? SelectedAudioSampleRate
    {
        get => GetValue(SelectedAudioSampleRateProperty);
        set => SetValue(SelectedAudioSampleRateProperty, value);
    }
    #endregion

    #region Instance constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSoundSection"/>
    /// class.
    /// </summary>
    public SettingsSoundSection()
    {
        InitializeComponent();
    }
    #endregion
}