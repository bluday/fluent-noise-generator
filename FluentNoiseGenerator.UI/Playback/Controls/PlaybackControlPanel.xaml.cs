using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.Windows.Input;

namespace FluentNoiseGenerator.UI.Playback.Controls;

/// <summary>
/// Interaction logic for PlaybackControlPanel.xaml.
/// </summary>
public sealed partial class PlaybackControlPanel : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Constants
    /// <summary>
    /// The "Normal" visual state name.
    /// </summary>
    public const string STATE_NAME_NORMAL = "Normal";

    /// <summary>
    /// The "Playing" visual state name.
    /// </summary>
    public const string STATE_NAME_PLAYING = "Playing";
    #endregion

    #region Depenedency properties
    /// <summary>
    /// Identifies the <see cref="IsPlaying"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
        nameof(IsPlaying),
        typeof(bool),
        typeof(PlaybackControlPanel),
        new PropertyMetadata(
            defaultValue:            false,
            propertyChangedCallback: OnIsPlayingChanged
        )
    );

    /// <summary>
    /// Identifies the <see cref="NoisePresetItemsSource"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty NoisePresetItemsSourceProperty = DependencyProperty.Register(
        nameof(NoisePresetItemsSource),
        typeof(IEnumerable<object>),
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

    #region Properties
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
    public IEnumerable<object> NoisePresetItemsSource
    {
        get => (IEnumerable<object>)GetValue(NoisePresetItemsSourceProperty);
        set => SetValue(NoisePresetItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to be executed when the close button gets clicked.
    /// </summary>
    public ICommand PlaybackButtonClickCommand
    {
        get => (ICommand)GetValue(PlaybackButtonClickCommandProperty);
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
        InitializeComponent();
    }
    #endregion

    #region Methods
    private void UpdatePlaybackVisualState()
    {
        VisualStateManager.GoToState(
            control:        this,
            stateName:      IsPlaying ? STATE_NAME_PLAYING : STATE_NAME_NORMAL,
            useTransitions: true
        );
    }
    #endregion

    #region Property callbacks
    private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PlaybackControlPanel)d).UpdatePlaybackVisualState();
    }
    #endregion
}