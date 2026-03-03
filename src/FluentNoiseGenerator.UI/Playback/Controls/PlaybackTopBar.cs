using FluentNoiseGenerator.UI.Infrastructure.Extensions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;
using Windows.Graphics;

namespace FluentNoiseGenerator.UI.Playback.Controls;

/// <summary>
/// Interaction logic for PlaybackTopBar.xaml.
/// </summary>
[TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
[TemplatePart(Name = PART_SettingsButton, Type = typeof(Button))]
public sealed partial class PlaybackTopBar : Control
{
    #region Constants
    /// <summary>
    /// The "PART_CloseButton" string literal.
    /// </summary>
    public const string PART_CloseButton = "PART_CloseButton";

    /// <summary>
    /// The "PART_SettingsButton" string literal.
    /// </summary>
    public const string PART_SettingsButton = "PART_SettingsButton";
    #endregion

    #region Fields
    private Button _closeButton;

    private Button _settingsButton;
    #endregion

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
    public ICommand? CloseButtonClickCommand
    {
        get => GetValue(CloseButtonClickCommandProperty) as ICommand;
        set => SetValue(CloseButtonClickCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command to be executed when the settings button gets clicked.
    /// </summary>
    public ICommand? SettingsButtonClickCommand
    {
        get => GetValue(SettingsButtonClickCommandProperty) as ICommand;
        set => SetValue(SettingsButtonClickCommandProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackTopBar"/> class.
    /// </summary>
    public PlaybackTopBar()
    {
        _closeButton = null!;

        _settingsButton = null!;

        DefaultStyleKey = typeof(PlaybackTopBar);
    }
    #endregion

    #region Methods
    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _closeButton    = (Button)GetTemplateChild(PART_CloseButton);
        _settingsButton = (Button)GetTemplateChild(PART_SettingsButton);
    }

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
        return _closeButton.GetBoundingBox(scaleFactor);
    }

    /// <inheritdoc cref="GetBoundingRectForCloseButton(double)"/>
    /// <summary>
    /// Gets the bounding box for the settings button.
    /// </summary>
    public RectInt32 GetBoundingRectForSettingsButton(double scaleFactor)
    {
        return _settingsButton.GetBoundingBox(scaleFactor);
    }
    #endregion
}