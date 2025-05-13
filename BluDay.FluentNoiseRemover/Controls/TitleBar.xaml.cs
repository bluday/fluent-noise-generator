namespace BluDay.FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for TitleBar.xaml.
/// </summary>
public sealed partial class TitleBar : UserControl
{
    #region Events
    public event EventHandler? CloseButtonClick;
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleBar"/> class.
    /// </summary>
    public TitleBar() => InitializeComponent();

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseButtonClick?.Invoke(this, EventArgs.Empty);
    }
}