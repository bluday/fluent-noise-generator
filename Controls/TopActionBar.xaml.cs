namespace BluDay.FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for TopActionBar.xaml.
/// </summary>
public sealed partial class TopActionBar : UserControl
{
    /// <summary>
    /// Identifies the <see cref="CloseButtonCommand"/> dependency property.
    /// </summary>
    private readonly DependencyProperty CloseButtonCommandProperty = DependencyProperty.Register(
        nameof(CloseButton),
        typeof(ICommand),
        typeof(TopActionBar),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SettingsButtonCommand"/> dependency property.
    /// </summary>
    private readonly DependencyProperty SettingsButtonCommandProperty = DependencyProperty.Register(
        nameof(SettingsButton),
        typeof(ICommand),
        typeof(TopActionBar),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Gets or sets the command for the close button.
    /// </summary>
    public ICommand? CloseButtonCommand
    {
        get => GetValue(CloseButtonCommandProperty) as ICommand;
        set => SetValue(CloseButtonCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command for the settings button.
    /// </summary>
    public ICommand? SettingsButtonCommand
    {
        get => GetValue(SettingsButtonCommandProperty) as ICommand;
        set => SetValue(SettingsButtonCommandProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TopActionBar"/> class.
    /// </summary>
    public TopActionBar()
    {
        InitializeComponent();
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