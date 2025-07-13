using BluDay.Net.WinUI3.Extensions;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;

namespace FluentNoiseGenerator.Controls;

/// <summary>
/// Interaction logic for TopActionBar.xaml.
/// </summary>
public sealed partial class TopActionBar : Microsoft.UI.Xaml.Controls.UserControl
{
    /// <summary>
    /// Triggered when the close button is clicked.
    /// </summary>
    public event EventHandler CloseButtonClicked;

    /// <summary>
    /// Triggered when the settings button is clicked.
    /// </summary>
    public event EventHandler SettingsButtonClicked;

    /// <summary>
    /// Initializes a new instance of the <see cref="TopActionBar"/> class.
    /// </summary>
    public TopActionBar()
    {
        CloseButtonClicked = (sender, e) => { };

        SettingsButtonClicked = (sender, e) => { };

        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseButtonClicked.Invoke(this, EventArgs.Empty);
    }

    private void SettingsButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsButtonClicked.Invoke(this, EventArgs.Empty);
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
        return CloseButton.GetBoundingBox(scaleFactor);
    }

    /// <inheritdoc cref="GetBoundingRectForCloseButton(double)"/>
    /// <summary>
    /// Gets the bounding box for the settings button.
    /// </summary>
    public RectInt32 GetBoundingRectForSettingsButton(double scaleFactor)
    {
        return SettingsButton.GetBoundingBox(scaleFactor);
    }
}