using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace FluentNoiseRemover.Controls;

/// <summary>
/// Interaction logic for SettingsSectionHeader.xaml.
/// </summary>
public sealed partial class SettingsSectionHeader : Microsoft.UI.Xaml.Controls.UserControl
{
    /// <summary>
    /// Identifies the <see cref="Glyph"> dependency property.
    /// </summary>
    public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(
        nameof(Glyph),
        typeof(ImageSource),
        typeof(SettingsSectionHeader),
        new PropertyMetadata(defaultValue: string.Empty)
    );

    /// <summary>
    /// Identifies the <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(SettingsSectionHeader),
        new PropertyMetadata(defaultValue: string.Empty)
    );

    /// <summary>
    /// Gets the font icon glyph.
    /// </summary>
    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    /// <summary>
    /// Gets the header.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSectionHeader"/> class.
    /// </summary>
    public SettingsSectionHeader()
    {
        InitializeComponent();
    }
}