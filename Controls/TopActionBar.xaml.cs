namespace BluDay.FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for TopActionBar.xaml.
/// </summary>
public sealed partial class TopActionBar : UserControl
{
    public event EventHandler? CloseButtonClick;

    public event EventHandler? SettingsButtonClick;

    /// <summary>
    /// Initializes a new instance of the <see cref="TopActionBar"/> class.
    /// </summary>
    public TopActionBar()
    {
        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseButtonClick?.Invoke(this, EventArgs.Empty);
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsButtonClick?.Invoke(this, EventArgs.Empty);
    }

    public RectInt32 GetBoundingRectForCloseButton(double scaleFactor)
    {
        return CloseButton.GetBoundingBox(scaleFactor);
    }

    public RectInt32 GetBoundingRectForSettingsButton(double scaleFactor)
    {
        return SettingsButton.GetBoundingBox(scaleFactor);
    }
}