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
        new PropertyMetadata(
            defaultValue:            null,
            propertyChangedCallback: OnGlyphChanged
        )
    );

    /// <summary>
    /// Identifies the <see cref="Header"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(string),
        typeof(SettingsSectionHeader),
        new PropertyMetadata(
            defaultValue:            null,
            propertyChangedCallback: OnHeaderChanged
        )
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
    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSectionHeader"/> class.
    /// </summary>
    public SettingsSectionHeader()
    {
        InitializeComponent();
    }

    private static void OnGlyphChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((SettingsSectionHeader)d).HeaderFontIcon.Glyph = e.NewValue as string;
    }

    private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((SettingsSectionHeader)d).HeaderTextBlock.Text = e.NewValue as string;
    }
}