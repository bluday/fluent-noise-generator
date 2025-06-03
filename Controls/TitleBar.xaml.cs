namespace BluDay.FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for TitleBar.xaml.
/// </summary>
public sealed partial class TitleBar : UserControl
{
    #region Events
    public event EventHandler? CloseButtonClick;

    public event EventHandler? SettingsButtonClick;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="TitleBar"/> class.
    /// </summary>
    public TitleBar()
    {
        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseButtonClick?.Invoke(this, EventArgs.Empty);
    }

    private void SettigsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsButtonClick?.Invoke(this, EventArgs.Empty);
    }
    #endregion
}