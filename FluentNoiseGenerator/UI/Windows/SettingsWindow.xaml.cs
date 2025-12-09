using FluentNoiseGenerator.Common;
using FluentNoiseGenerator.Extensions;
using FluentNoiseGenerator.Services;
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI;

namespace FluentNoiseGenerator.UI.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    #region Constants
    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_HEIGHT = 1000;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 1000;
    #endregion

    #region Fields
    private ResourceLoader? _resourceLoader;

    private bool _hasClosed;

    private readonly OverlappedPresenter _overlappedPresenter = OverlappedPresenter.Create();

    private readonly ResourceService _resourceService;
    #endregion

    #region Properties
    /// <summary>
    /// Gets a read-only collection of available, mapped application themes, with localized
    /// keys.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<ElementTheme>> AvailableApplicationThemes { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped audio sample rates, with localized
    /// keys.
    /// </summary>
    public IReadOnlyCollection<NamedValue<int>> AvailableAudioSampleRates { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped languages, with localized keys.
    /// </summary>
    public IReadOnlyCollection<NamedValue<CultureInfo>> AvailableLanguages { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped noise presets, with localized keys.
    /// </summary>
    /// <remarks>
    /// Value type of <see cref="string"/> is used for now, until a type for noise preset is
    /// implemented.
    /// </remarks>
    public IReadOnlyCollection<ResourceNamedValue<string>> AvailableNoisePresets { get; }

    /// <summary>
    /// Gets a read-only collection of available, mapped system backdrops, with localized keys.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<SystemBackdrop>> AvailableSystemBackdrops { get; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets a displayable application version text.
    /// </summary>
    public string ApplicationVersionText
    {
        get
        {
            PackageVersion version = Package.Current.Id.Version;

            return $"{version.Major}.{version.Minor}";
        }
    }
    #endregion

    #region Events
    /// <summary>
    /// Triggers when a new application theme gets selected.
    /// </summary>
    public event EventHandler<ElementTheme> ApplicationThemeChanged = delegate { };

    /// <summary>
    /// Triggers when a new system backdrop gets selected.
    /// </summary>
    public event EventHandler<SystemBackdrop?> SystemBackdropChanged = delegate { };
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class using default
    /// paramaeter values.
    /// </summary>
    /// <remarks>
    /// This parameterless constructor is required for design-time support in Visual Studio and
    /// to play nice with the XAML designer.
    /// </remarks>
    public SettingsWindow() : this(null!) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class using the specified
    /// resource service instance.
    /// </summary>
    /// <param name="resourceService">
    /// A <see cref="ResourceService"/> instance for accessing common application resources.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="resourceService"/> is <c>null</c>.
    /// </exception>
    public SettingsWindow(ResourceService resourceService)
    {
        ArgumentNullException.ThrowIfNull(resourceService);

        _resourceService = resourceService;

        Closed += SettingsWindow_Closed;

        ResourceLoader GetResourceLoaderFactory() => _resourceLoader!;

        AvailableAudioSampleRates = [
            new NamedValue<int>(AudioSampleRates.Rate48000Hz, GetDisplayableAudioSampleRateString),
            new NamedValue<int>(AudioSampleRates.Rate44100Hz, GetDisplayableAudioSampleRateString)
        ];

        AvailableLanguages = [
            ..ApplicationLanguages.ManifestLanguages.Select(
                language => new NamedValue<CultureInfo>(
                    value:     new(language),
                    formatter: language => language.NativeName
                )
            )
        ];

        AvailableApplicationThemes = [
            new ResourceNamedValue<ElementTheme>(ElementTheme.Default, "Common/System", GetResourceLoaderFactory),
            new ResourceNamedValue<ElementTheme>(ElementTheme.Dark, "Common/Dark", GetResourceLoaderFactory),
            new ResourceNamedValue<ElementTheme>(ElementTheme.Light, "Common/Light", GetResourceLoaderFactory)
        ];

        AvailableNoisePresets = [
            new ResourceNamedValue<string>(null, "Common/Blue", GetResourceLoaderFactory),
            new ResourceNamedValue<string>(null, "Common/Brownian", GetResourceLoaderFactory),
            new ResourceNamedValue<string>(null, "Common/White", GetResourceLoaderFactory)
        ];

        AvailableSystemBackdrops = [
            new ResourceNamedValue<SystemBackdrop>(
                new MicaBackdrop(),
                "SystemBackdrop/Mica",
                GetResourceLoaderFactory
            ),
            new ResourceNamedValue<SystemBackdrop>(
                new MicaBackdrop { Kind = MicaKind.BaseAlt },
                "SystemBackdrop/MicaAlt",
                GetResourceLoaderFactory
            ),
            new ResourceNamedValue<SystemBackdrop>(
                new DesktopAcrylicBackdrop(),
                "SystemBackdrop/Acrylic",
                GetResourceLoaderFactory
            ),
            new ResourceNamedValue<SystemBackdrop>(
                null,
                "Common/None",
                GetResourceLoaderFactory
            )
        ];

        InitializeComponent();
    }
    #endregion

    #region Event handlers
    private void AvailableApplicationThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is ResourceNamedValue<ElementTheme> namedValue)
        {
            ApplicationThemeChanged.Invoke(this, namedValue.Value);
        }
    }

    private void AvailableLanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is NamedValue<CultureInfo> namedValue)
        {
            ApplicationLanguages.PrimaryLanguageOverride = namedValue.Value.Name;

            RefreshLocalizedContent();
        }
    }

    private void AvailableSystemBackdropsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is ResourceNamedValue<SystemBackdrop> namedValue)
        {
            SystemBackdropChanged?.Invoke(this, namedValue.Value);
        }
    }

    private void SettingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }
    #endregion

    #region Methods
    private void ApplyLocalizedContent()
    {
        if (_resourceLoader is null) return;

        string displayName  = _resourceLoader.GetString("General/AppDisplayName");
        string settingsText = _resourceLoader.GetString("Common/Settings");

        Title = settingsText;

        TitleBar.Title = displayName;

        HeaderTextBlock.Text = settingsText;

        AppearanceSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Appearance");

        AlwaysOnTopSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Header");
        AlwaysOnTopSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Description");

        ApplicationThemeSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Header");
        ApplicationThemeSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Description");

        SystemBackdropSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Header");
        SystemBackdropSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Description");

        GeneralSettingsSectionHeader.Text = _resourceLoader.GetString("Common/General");

        AutoplayOnLaunchSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Header");
        AutoplayOnLaunchSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Description");

        AutoplayOnLaunchToggleSwitch.OnContent  = _resourceLoader.GetString("Common/On");
        AutoplayOnLaunchToggleSwitch.OffContent = _resourceLoader.GetString("Common/Off");

        DefaultNoisePresetSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Header");
        DefaultNoisePresetSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Description");

        LanguageSettingsCard.Header      = _resourceLoader.GetString("Common/Language");
        LanguageSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/Language/Description");

        SoundSettingsSectionHeader.Text = _resourceLoader.GetString("Common/Sound");

        AudioSampleRateSettingsCard.Header      = _resourceLoader.GetString("Common/SampleRate");
        AudioSampleRateSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Sound/SampleRate/Description");

        AboutSettingsSectionHeader.Text = _resourceLoader.GetString("Common/About");

        AboutSettingsExpander.Header      = displayName;
        AboutSettingsExpander.Description = _resourceLoader.GetString("General/CopyrightText");

        SessionIdentifierSettingsCard.Header = string.Format(
            format: _resourceLoader.GetString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        RepositoryOnGitHubHyperlinkButton.Content     = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        SendFeedbackHyperlinkButton.Content           = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/SendFeedback");
        RepositoryOnGitHubHyperlinkButton.NavigateUri = new Uri(_resourceLoader.GetString("General/GitHubRepositoryUrl"));
        SendFeedbackHyperlinkButton.NavigateUri       = new Uri(_resourceLoader.GetString("General/SendFeedbackUrl"));
    }

    private string GetDisplayableAudioSampleRateString(int value)
    {
        if (_resourceLoader is null)
        {
            return value.ToString();
        }

        return $"{value} {_resourceLoader.GetString("Units/Hertz/Short")}";
    }

    /// <summary>
    /// Applies configuration to the underlying, native window specific to the settings window.
    /// </summary>
    public void ConfigureAppWindow()
    {
        _overlappedPresenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        _overlappedPresenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        AppWindow.SetPresenter(_overlappedPresenter);
        AppWindow.Resize(MINIMUM_WIDTH, MINIMUM_HEIGHT);
    }

    /// <summary>
    /// Configures the native title bar and specifies the custom <see cref="TitleBar"/>
    /// control as the primary title bar to ensure that the bounds for the custom title bar
    /// is set correctly.
    /// </summary>
    public void ConfigureTitleBar()
    {
        string iconPath = _resourceService.AppIconPath;

        AppWindow.SetIcon(iconPath);

        TitleBar.Icon = iconPath;

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TitleBar);
    }

    /// <summary>
    /// Refreshes the background brush of the root element based on its current
    /// <see cref="FrameworkElement.RequestedTheme"/> value.
    /// </summary>
    public void RefreshBackgroundBrush()
    {
        if (SystemBackdrop is not null)
        {
            LayoutRoot.Background = null;

            return;
        }

        LayoutRoot.Background = new SolidColorBrush(
            LayoutRoot.RequestedTheme is ElementTheme.Light
                ? Colors.White
                : Colors.Black
        );
    }

    /// <summary>
    /// Creates a new <see cref="ResourceLoader"/> instances and refreshes all named values
    /// with localized strings from the new resource loader.
    /// </summary>
    /// <remarks>
    /// This enables complete and real-time update of localized content, rather than forcing
    /// the user to restart the whole application after updating the application language.
    /// </remarks>
    public void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        ApplyLocalizedContent();
    }

    /// <summary>
    /// Refreshes 
    /// </summary>
    public void RefreshTitleBarColors()
    {
        AppWindowTitleBar titleBar = AppWindow.TitleBar;

        Color buttonForegroundColor;
        Color hoverPressedBackgroundColor;

        titleBar.ButtonBackgroundColor         = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

        if ((Content as FrameworkElement)?.RequestedTheme is ElementTheme.Light)
        {
            hoverPressedBackgroundColor = Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD);

            buttonForegroundColor = Colors.Black;
        }
        else
        {
            hoverPressedBackgroundColor = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);

            buttonForegroundColor = Colors.White;
        }

        titleBar.ButtonHoverBackgroundColor   = hoverPressedBackgroundColor;
        titleBar.ButtonPressedBackgroundColor = hoverPressedBackgroundColor;

        titleBar.ButtonForegroundColor        = buttonForegroundColor;
        titleBar.ButtonHoverForegroundColor   = buttonForegroundColor;
        titleBar.ButtonPressedForegroundColor = buttonForegroundColor;
    }

    /// <summary>
    /// Focuses the root element of the window.
    /// </summary>
    public void Focus()
    {
        Content?.Focus(FocusState.Programmatic);
    }

    /// <inheritdoc cref="OverlappedPresenter.Restore()"/>
    public void Restore()
    {
        _overlappedPresenter.Restore();
    }

    /// <summary>
    /// Updates the current theme within the window using the specified value.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="ElementTheme"/> to apply on the window.
    /// </param>
    public void UpdateRequestedTheme(ElementTheme value)
    {
        (Content as FrameworkElement)?.RequestedTheme = value;

        RefreshBackgroundBrush();
        RefreshTitleBarColors();
    }

    /// <summary>
    /// Updates the current system backdrop within the window using the specified value.
    /// </summary>
    /// <param name="value">
    /// The new <see cref="SystemBackdrop"/> to apply on the window.
    /// </param>
    public void UpdateSystemBackdrop(SystemBackdrop? value)
    {
        SystemBackdrop = value;

        RefreshBackgroundBrush();
        RefreshTitleBarColors();
    }
    #endregion
}