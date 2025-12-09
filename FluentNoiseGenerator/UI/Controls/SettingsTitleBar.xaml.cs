using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator.UI.Controls;

/// <summary>
/// Interaction logic for SettingsTitleBar.xaml.
/// </summary>
public sealed partial class SettingsTitleBar : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="Icon"> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(ImageSource),
        typeof(SettingsTitleBar),
        new PropertyMetadata(null, OnIconChanged)
    );

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(SettingsTitleBar),
        new PropertyMetadata(null, OnTitleChanged)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets the image source for the icon.
    /// </summary>
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets the title value of the title bar.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsTitleBar"/> class.
    /// </summary>
    public SettingsTitleBar()
    {
        InitializeComponent();
    }
    #endregion

    #region Methods
    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (SettingsTitleBar)d;

        if (!Uri.TryCreate((string)e.NewValue, UriKind.Absolute, out Uri? uri))
        {
            control.iconImage.Source = null;

            return;
        }

        control.iconImage.Source = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(uri);
    }

    private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((SettingsTitleBar)d).titleTextBlock.Text = e.NewValue as string;
    }
    #endregion
}