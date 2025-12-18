using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;

namespace FluentNoiseGenerator.UI.Settings.Controls;

/// <summary>
/// Interaction logic for SettingsTitleBar.xaml.
/// </summary>
public sealed partial class SettingsTitleBar : Microsoft.UI.Xaml.Controls.UserControl
{
    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="IconPath"> dependency property.
    /// </summary>
    public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
        nameof(IconPath),
        typeof(ImageSource),
        typeof(SettingsTitleBar),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(SettingsTitleBar),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Properties
    /// <summary>
    /// Gets the icon path.
    /// </summary>
    public string IconPath
    {
        get => (string)GetValue(IconPathProperty);
        set => SetValue(IconPathProperty, value);
    }

    /// <summary>
    /// Gets the title.
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
}