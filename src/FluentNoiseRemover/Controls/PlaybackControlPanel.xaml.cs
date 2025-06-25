using Microsoft.UI.Xaml;
using System;

namespace FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for PlaybackControlPanel.xaml.
/// </summary>
public sealed partial class PlaybackControlPanel : Microsoft.UI.Xaml.Controls.UserControl
{
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
    /// Gets or sets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
    }

    /// <summary>
    /// Invokes when the playback button has been clicked.
    /// </summary>
    public event EventHandler PlaybackButtonClicked;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackControlPanel"/> class.
    /// </summary>
    public PlaybackControlPanel()
    {
        PlaybackButtonClicked = (sender, e) => { };

        InitializeComponent();
    }

    private void UpdatePlaybackVisualState()
    {
        VisualStateManager.GoToState(this, IsPlaying ? "Playing" : "Normal", true);
    }

    private void PlaybackButton_Click(object sender, RoutedEventArgs e)
    {
        PlaybackButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((PlaybackControlPanel)d).UpdatePlaybackVisualState();
    }
}