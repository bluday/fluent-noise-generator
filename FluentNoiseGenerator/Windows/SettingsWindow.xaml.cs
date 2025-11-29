using FluentNoiseGenerator.Common;
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
using Windows.Graphics;
using Windows.UI;

namespace FluentNoiseGenerator.Windows;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsWindow : Window
{
    private ResourceLoader _resourceLoader;

    private bool _hasClosed;

    private readonly AppWindow _appWindow;

    private readonly OverlappedPresenter _overlappedPresenter;

    /// <summary>
    /// The minimum height in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_HEIGHT = 1000;

    /// <summary>
    /// The minimum width in pixels, unscaled.
    /// </summary>
    public const int MINIMUM_WIDTH = 1000;

    /// <summary>
    /// Triggers when a new application theme gets selected.
    /// </summary>
    public event EventHandler<ElementTheme> ApplicationThemeChanged;

    /// <summary>
    /// Triggers when a new system backdrop gets selected.
    /// </summary>
    public event EventHandler<SystemBackdrop?> SystemBackdropChanged;

    /// <summary>
    /// Gets a read-only collection of mapped application themes, with localized keys.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<ElementTheme>> ApplicationThemes { get; }

    /// <summary>
    /// Gets a read-only collection of mapped audio sample rates, with localized keys.
    /// </summary>
    public IReadOnlyCollection<NamedValue<int>> AudioSampleRates { get; }

    /// <summary>
    /// Gets a value indicating whether the window has been closed.
    /// </summary>
    public bool HasClosed => _hasClosed;

    /// <summary>
    /// Gets a read-only collection of mapped languages, with localized keys.
    /// </summary>
    public IReadOnlyCollection<NamedValue<CultureInfo>> Languages { get; }

    /// <summary>
    /// Gets a read-only collection of mapped noise presets, with localized keys.
    /// </summary>
    /// <remarks>
    /// Value type of <see cref="string"/> is used for now, until a type for noise preset is implemented.
    /// </remarks>
    public IReadOnlyCollection<ResourceNamedValue<string>> NoisePresets { get; }

    /// <summary>
    /// Gets a read-only collection of mapped system backdrops, with localized keys.
    /// </summary>
    public IReadOnlyCollection<ResourceNamedValue<SystemBackdrop>> SystemBackdrops { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
    /// </summary>
    public SettingsWindow()
    {
        _resourceLoader = null!;

        _appWindow = AppWindow;

        _overlappedPresenter = OverlappedPresenter.Create();

        ApplicationThemeChanged = delegate { };
        SystemBackdropChanged   = delegate { };

        Closed += SettingsWindow_Closed;

        AudioSampleRates = new List<NamedValue<int>>
        {
            new(Common.AudioSampleRates.Rate48000Hz, GetDisplayableAudioSampleRateString),
            new(Common.AudioSampleRates.Rate44100Hz, GetDisplayableAudioSampleRateString)
        };

        Languages = ApplicationLanguages.ManifestLanguages
            .Select(
                value => new NamedValue<CultureInfo>(
                    value:     new CultureInfo(value),
                    formatter: value => value.NativeName
                )
            )
            .ToList();

        Func<ResourceLoader> resourceLoaderFactory = () => _resourceLoader;

        ApplicationThemes = new ResourceNamedValueCollectionBuilder<ElementTheme>(resourceLoaderFactory)
            .Add("Common/System", ElementTheme.Default)
            .Add("Common/Dark", ElementTheme.Dark)
            .Add("Common/Light", ElementTheme.Light)
            .Build();

        NoisePresets = new ResourceNamedValueCollectionBuilder<string>(resourceLoaderFactory)
            .Add("Common/Blue", null!)
            .Add("Common/Brownian", null!)
            .Add("Common/White", null!)
            .Build();

        SystemBackdrops = new ResourceNamedValueCollectionBuilder<SystemBackdrop>(resourceLoaderFactory)
            .Add("SystemBackdrop/Mica", new MicaBackdrop())
            .Add("SystemBackdrop/MicaAlt", new MicaBackdrop()
            {
                Kind = MicaKind.BaseAlt
            })
            .Add("SystemBackdrop/Acrylic", new DesktopAcrylicBackdrop())
            .Add("Common/None", null!)
            .Build();

        InitializeComponent();
    }

    #region Event handlers
    private void ApplicationThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is ResourceNamedValue<ElementTheme> namedValue)
        {
            ApplicationThemeChanged.Invoke(this, namedValue.Value);
        }
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is NamedValue<CultureInfo> namedValue)
        {
            ApplicationLanguages.PrimaryLanguageOverride = namedValue.Value.Name;

            RefreshLocalizedContent();
        }
    }

    private void SystemBackdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        ApplicationVersionTextBlock.Text = GetApplicationVersionText();

        SessionIdentifierSettingsCard.Header = string.Format(
            format: _resourceLoader.GetString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        RepositoryOnGitHubHyperlinkButton.Content     = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        SendFeedbackHyperlinkButton.Content           = _resourceLoader.GetString("SettingsWindow/HyperlinkButtons/SendFeedback");
        RepositoryOnGitHubHyperlinkButton.NavigateUri = new Uri(_resourceLoader.GetString("General/GitHubRepositoryUrl"));
        SendFeedbackHyperlinkButton.NavigateUri       = new Uri(_resourceLoader.GetString("General/SendFeedbackUrl"));
    }

    private string GetApplicationVersionText()
    {
        PackageVersion version = Package.Current.Id.Version;

        return $"{version.Major}.{version.Minor}";
    }

    private string GetDisplayableAudioSampleRateString(int value)
    {
        return $"{value} {_resourceLoader.GetString("Units/Hertz/Short")}";
    }

    /// <summary>
    /// Applies configuration to the underlying, native window specific to the settings window.
    /// </summary>
    public void ConfigureAppWindow()
    {
        _overlappedPresenter.PreferredMinimumWidth  = MINIMUM_WIDTH;
        _overlappedPresenter.PreferredMinimumHeight = MINIMUM_HEIGHT;

        _appWindow.SetPresenter(_overlappedPresenter);

        _appWindow.Resize(new SizeInt32(MINIMUM_WIDTH, MINIMUM_HEIGHT));
    }

    /// <summary>
    /// Configures the native title bar and specifies the custom <see cref="TitleBar"/>
    /// control as the primary title bar to ensure that the bounds for the custom title bar
    /// is set correctly.
    /// </summary>
    public void ConfigureTitleBar()
    {
        _appWindow.SetIcon(App.IconPath);

        TitleBar.Icon = App.IconPath;

        ExtendsContentIntoTitleBar = true;

        SetTitleBar(TitleBar);
    }

    /// <summary>
    /// Refreshes the background brush of the root element based on its current
    /// <see cref="FrameworkElement.RequestedTheme"/> value.
    /// </summary>
    public void RefreshBackgroundBrush()
    {
        LayoutRoot.Background = SystemBackdrop is not null
            ? null
            : LayoutRoot.RequestedTheme is ElementTheme.Light
                ? new SolidColorBrush(Colors.White)
                : new SolidColorBrush(Colors.Black);
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