using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseGenerator.UI.Controls;

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
    #endregion

    #region Events
    /// <summary>
    /// Invokes when the playback button has been clicked.
    /// </summary>
    public event EventHandler PlaybackButtonClicked = delegate { };
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

    #region Event handlers
    private void PlaybackButton_Click(object sender, RoutedEventArgs e)
    {
        PlaybackButtonClicked?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region Property callbacks
    private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PlaybackControlPanel)d).UpdatePlaybackVisualState();
    }
    #endregion
}