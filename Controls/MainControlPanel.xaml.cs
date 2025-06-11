namespace BluDay.FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for MainControlPanel.xaml.
/// </summary>
public sealed partial class MainControlPanel : UserControl
{
    /// <summary>
    /// Identifies the <see cref="IsPlaying"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
        nameof(IsPlaying),
        typeof(bool),
        typeof(MainControlPanel),
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
    /// Initializes a new instance of the <see cref="MainControlPanel"/> class.
    /// </summary>
    public MainControlPanel()
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
        ((MainControlPanel)d).UpdatePlaybackVisualState();
    }
}