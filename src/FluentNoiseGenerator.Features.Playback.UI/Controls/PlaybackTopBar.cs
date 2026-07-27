using FluentNoiseGenerator.UI.Extensions;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Windows.Input;
using Windows.Graphics;

namespace FluentNoiseGenerator.Features.Playback.UI.Controls;

/// <summary>
/// Interaction logic for PlaybackTopBar.xaml.
/// </summary>
[TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
[TemplatePart(Name = PART_SettingsButton, Type = typeof(Button))]
public sealed partial class PlaybackTopBar : Control
{
    #region Constants
    /// <summary>
    /// The default DPI scale factor to use input region rects.
    /// </summary>
    public const double DefaultDpiScaleFactor = 1.0;

    /// <summary>
    /// The "PART_CloseButton" string literal.
    /// </summary>
    public const string PART_CloseButton = nameof(PART_CloseButton);

    /// <summary>
    /// The "PART_SettingsButton" string literal.
    /// </summary>
    public const string PART_SettingsButton = nameof(PART_SettingsButton);
    #endregion

    #region Instance fields
    private bool _canConfigureNonClientRegions;

    private Button _closeButton;

    private double _dpiScaleFactor;

    private InputNonClientPointerSource? _inputNonClientPointerSource;

    private Button _settingsButton;
    #endregion

    #region Dependency properties
    /// <summary>
    /// Identifies the <see cref="CloseButtonClickCommand"/>
    /// dependency property.
    /// </summary>
    public static readonly DependencyProperty CloseButtonClickCommandProperty = DependencyProperty.Register(
        nameof(CloseButtonClickCommand),
        typeof(ICommand),
        typeof(PlaybackTopBar),
        new PropertyMetadata(defaultValue: null)
    );

    /// <summary>
    /// Identifies the <see cref="SettingsButtonClickCommand"/>
    /// dependency property.
    /// </summary>
    public static readonly DependencyProperty SettingsButtonClickCommandProperty = DependencyProperty.Register(
        nameof(SettingsButtonClickCommand),
        typeof(ICommand),
        typeof(PlaybackTopBar),
        new PropertyMetadata(defaultValue: null)
    );
    #endregion

    #region Instance properties
    /// <summary>
    /// Gets or sets a value indicating whether the non-client regions
    /// can be configured.
    /// </summary>
    public bool CanConfigureNonClientRegions
    {
        get => _canConfigureNonClientRegions;
        set => _canConfigureNonClientRegions = value;
    }

    /// <summary>
    /// Gets or sets the close button click command.
    /// </summary>
    public ICommand? CloseButtonClickCommand
    {
        get => GetValue(CloseButtonClickCommandProperty) as ICommand;
        set => SetValue(CloseButtonClickCommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the targeted DPI scale factor.
    /// </summary>
    public double? DpiScaleFactor
    {
        get => _dpiScaleFactor;
        set => _dpiScaleFactor = value ?? DefaultDpiScaleFactor;
    }

    /// <summary>
    /// Gets or sets the <see cref="InputNonClientPointerSource"> for
    /// configuring the non-client input regions.
    /// </summary>
    public InputNonClientPointerSource? InputNonClientPointerSource
    {
        get => _inputNonClientPointerSource;
        set => _inputNonClientPointerSource = value;
    }

    /// <summary>
    /// Gets or sets the settings button click command.
    /// </summary>
    public ICommand? SettingsButtonClickCommand
    {
        get => GetValue(SettingsButtonClickCommandProperty) as ICommand;
        set => SetValue(SettingsButtonClickCommandProperty, value);
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybackTopBar"/>
    /// class.
    /// </summary>
    public PlaybackTopBar()
    {
        _closeButton = null!;

        _settingsButton = null!;

        _dpiScaleFactor = DefaultDpiScaleFactor;

        DefaultStyleKey = typeof(PlaybackTopBar);

        RegisterEventHandlers();
    }
    #endregion

    #region Event handlers
    private void PlaybackTopBar_LayoutUpdated(object? sender, object e)
    {
        UpdatePassthroughRegionRects();
    }

    private void PlaybackTopBar_Unloaded(object? sender, RoutedEventArgs e)
    {
        UnregisterEventHandlers();
    }
    #endregion

    #region Instance methods
    private RectInt32[] GetPassthroughRegionRects()
    {
        return [
            _closeButton.GetBoundingBox(_dpiScaleFactor),
            _settingsButton.GetBoundingBox(_dpiScaleFactor)
        ];
    }

    private void RegisterEventHandlers()
    {
        LayoutUpdated += PlaybackTopBar_LayoutUpdated;
        Unloaded      += PlaybackTopBar_Unloaded;
    }

    private void UnregisterEventHandlers()
    {
        LayoutUpdated -= PlaybackTopBar_LayoutUpdated;
        Unloaded      -= PlaybackTopBar_Unloaded;
    }

    private void UpdatePassthroughRegionRects()
    {
        if (!_canConfigureNonClientRegions || _inputNonClientPointerSource is null)
        {
            return;
        }

        var region = NonClientRegionKind.Passthrough;

        RectInt32[] rects = GetPassthroughRegionRects();

        _inputNonClientPointerSource.ClearRegionRects(region);
        _inputNonClientPointerSource.SetRegionRects(region, rects);
    }

    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _closeButton    = (Button)GetTemplateChild(PART_CloseButton);
        _settingsButton = (Button)GetTemplateChild(PART_SettingsButton);
    }

    /// <summary>
    /// Configures the control using the specified parameters.
    /// </summary>
    /// <param name="dpiScaleFactor">
    /// The DPI scale factor to use.
    /// </param>
    /// <param name="inputNonClientPointerSource">
    /// The non-client input pointer handler.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="inputNonClientPointerSource"/> is <see langword="null"/>
    /// </exception>
    public void Configure(
        double                      dpiScaleFactor,
        InputNonClientPointerSource inputNonClientPointerSource)
    {
        ArgumentNullException.ThrowIfNull(inputNonClientPointerSource);

        _dpiScaleFactor = dpiScaleFactor;

        _inputNonClientPointerSource = inputNonClientPointerSource;
    }
    #endregion
}