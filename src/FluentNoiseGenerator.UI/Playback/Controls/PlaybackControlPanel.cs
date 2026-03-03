using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace FluentNoiseGenerator.UI.Playback.Controls;

/// <summary>
/// Interaction logic for PlaybackControlPanel.xaml.
/// </summary>
public sealed partial class PlaybackControlPanel : Control
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="IsPlaying"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
        nameof(IsPlaying),
        typeof(bool),
        typeof(PlaybackControlPanel),
        new PropertyMetadata(defaultValue: false)
    );

    /// <summary>
    /// Identifies the <see cref="NoisePresetItemsSource"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty NoisePresetItemsSourceProperty = DependencyProperty.Register(
        nameof(NoisePresetItemsSource),
        typeof(object),
        typeof(PlaybackControlPanel),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="PlaybackButtonClickCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty PlaybackButtonClickCommandProperty = DependencyProperty.Register(
        nameof(PlaybackButtonClickCommand),
        typeof(ICommand),
        typeof(PlaybackControlPanel),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="Volume"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register(
        nameof(Volume),
        typeof(uint),
        typeof(PlaybackControlPanel),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Instance roperties
    /// <summary>
    /// Gets or sets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
    }

    /// <summary>
    /// Gets or sets the items source instance for the noise preset collection.
    /// </summary>
    public object? NoisePresetItemsSource
    {
        get => GetValue(NoisePresetItemsSourceProperty);
        set => SetValue(NoisePresetItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to be executed when the close button gets clicked.
    /// </summary>
    public ICommand? PlaybackButtonClickCommand
    {
        get => GetValue(PlaybackButtonClickCommandProperty) as ICommand;
        set => SetValue(PlaybackButtonClickCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the volume value as an unsigned integer.
    /// </summary>
    public uint Volume
    {
        get => (uint)GetValue(VolumeProperty);
        set => SetValue(VolumeProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackControlPanel"/> class.
    /// </summary>
    public PlaybackControlPanel()
    {
        DefaultStyleKey = typeof(PlaybackControlPanel);
    }
    #endregion
}