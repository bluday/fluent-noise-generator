using FluentNoiseGenerator.Extensions;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;

namespace FluentNoiseGenerator.UI.Controls;

/// <summary>
/// Interaction logic for PlaybackTopBar.xaml.
/// </summary>
public sealed partial class PlaybackTopBar : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Events
    /// <summary>
    /// Triggered when the close button is clicked.
    /// </summary>
    public event EventHandler CloseButtonClicked = delegate { };

    /// <summary>
    /// Triggered when the settings button is clicked.
    /// </summary>
    public event EventHandler SettingsButtonClicked = delegate { };
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

    #region Event handlers
    private void closeButton_Click(object sender, RoutedEventArgs e)
    {
        CloseButtonClicked.Invoke(this, EventArgs.Empty);
    }

    private void settingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsButtonClicked.Invoke(this, EventArgs.Empty);
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