using FluentNoiseGenerator.Extensions;
using Microsoft.UI.Xaml;
using System.Windows.Input;
using Windows.Graphics;

namespace FluentNoiseGenerator.UI.Controls;

/// <summary>
/// Interaction logic for PlaybackTopBar.xaml.
/// </summary>
public sealed partial class PlaybackTopBar : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="CloseButtonClickCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CloseButtonClickCommandProperty = DependencyProperty.Register(
        nameof(CloseButtonClickCommand),
        typeof(ICommand),
        typeof(PlaybackTopBar),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SettingsButtonClickCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SettingsButtonClickCommandProperty = DependencyProperty.Register(
        nameof(SettingsButtonClickCommand),
        typeof(ICommand),
        typeof(PlaybackTopBar),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the command to be executed when the close button gets clicked.
    /// </summary>
    public ICommand CloseButtonClickCommand
    {
        get => (ICommand)GetValue(CloseButtonClickCommandProperty);
        set => SetValue(CloseButtonClickCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to be executed when the settings button gets clicked.
    /// </summary>
    public ICommand SettingsButtonClickCommand
    {
        get => (ICommand)GetValue(SettingsButtonClickCommandProperty);
        set => SetValue(SettingsButtonClickCommandProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackTopBar"/> class.
    /// </summary>
    public PlaybackTopBar()
    {
        InitializeComponent();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Gets the bounding box for the settings button.
    /// </summary>
    /// <param name="scaleFactor">
    /// The scale factor to multiply the width and height with.
    /// </param>
    /// <returns>
    /// A scaled rect of the bounding box.
    /// </returns>
    public RectInt32 GetBoundingRectForCloseButton(double scaleFactor)
    {
        return closeButton.GetBoundingBox(scaleFactor);
    }

    /// <inheritdoc cref="GetBoundingRectForCloseButton(double)"/>
    /// <summary>
    /// Gets the bounding box for the settings button.
    /// </summary>
    public RectInt32 GetBoundingRectForSettingsButton(double scaleFactor)
    {
        return settingsButton.GetBoundingBox(scaleFactor);
    }
    #endregion
}