namespace BluDay.FluentNoiseRemover.Windows;

public partial class SettingsWindow
{
    private void ApplyLocalizedContent()
    {
        string displayName = Package.Current.DisplayName;

        string settingsText = this.GetLocalizedString("Common/Settings");

        TitleBar.Title = displayName;

        Title = settingsText;

        HeaderTextBlock.Text = settingsText;

        AppearanceSettingsSectionHeader.Header = this.GetLocalizedString("Common/Appearance");

        AlwaysOnTopSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/Appearance/AlwaysOnTop/Header");
        AlwaysOnTopSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Appearance/AlwaysOnTop/Description");

        ApplicationThemeSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/Appearance/ApplicationTheme/Header");
        ApplicationThemeSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Appearance/ApplicationTheme/Description");

        ApplicationThemeComboBox.ItemsSource = LocalizedApplicationThemes.Keys;

        SystemBackdropSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/Appearance/SystemBackdrop/Header");
        SystemBackdropSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Appearance/SystemBackdrop/Description");

        SystemBackdropComboBox.ItemsSource = LocalizedSystemBackdrops.Keys;

        GeneralSettingsSectionHeader.Header = this.GetLocalizedString("Common/General");

        AutoplayOnLaunchSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/General/AutoplayOnLaunch/Header");
        AutoplayOnLaunchSettingsCard.Description = this.GetLocalizedString("SettingsWindow/General/AutoplayOnLaunch/Description");

        AutoplayOnLaunchToggleSwitch.OnContent  = this.GetLocalizedString("Common/On");
        AutoplayOnLaunchToggleSwitch.OffContent = this.GetLocalizedString("Common/Off");

        DefaultNoisePresetSettingsCard.Header      = this.GetLocalizedString("SettingsWindow/General/DefaultNoisePreset/Header");
        DefaultNoisePresetSettingsCard.Description = this.GetLocalizedString("SettingsWindow/General/DefaultNoisePreset/Description");

        DefaultNoisePresetComboBox.ItemsSource = LocalizedNoisePresets.Keys;

        LanguageSettingsCard.Header      = this.GetLocalizedString("Common/Language");
        LanguageSettingsCard.Description = this.GetLocalizedString("SettingsWindow/General/Language/Description");

        LanguageComboBox.ItemsSource = LocalizedLanguages.Keys;

        SoundSettingsSectionHeader.Header = this.GetLocalizedString("Common/Sound");

        AudioSampleRateSettingsCard.Header      = this.GetLocalizedString("Common/SampleRate");
        AudioSampleRateSettingsCard.Description = this.GetLocalizedString("SettingsWindow/Sound/SampleRate/Description");

        AudioSampleRateComboBox.ItemsSource = LocalizedAudioSampleRates.Keys;

        AboutSettingsSectionHeader.Header = this.GetLocalizedString("Common/About");

        AboutSettingsExpander.Header      = displayName;
        AboutSettingsExpander.Description = this.GetLocalizedString("General/CopyrightText");

        ApplicationVersionTextBlock.Text = GetApplicationVersionText();

        SessionIdentifierSettingsCard.Header = string.Format(
            format: this.GetLocalizedString("SettingsWindow/About/SessionIdentifierFormatString"),
            args:   [Guid.Empty]
        );

        RepositoryOnGitHubHyperlinkButton.Content     = this.GetLocalizedString("SettingsWindow/HyperlinkButtons/RepositoryOnGitHub");
        SendFeedbackHyperlinkButton.Content           = this.GetLocalizedString("SettingsWindow/HyperlinkButtons/SendFeedback");
        RepositoryOnGitHubHyperlinkButton.NavigateUri = this.GetUriFromLocalizedString("General/GitHubRepositoryUrl");
        SendFeedbackHyperlinkButton.NavigateUri       = this.GetUriFromLocalizedString("General/SendFeedbackUrl");
    }

    private void PopulateComboBoxControlsWithLocalizedValues()
    {
        List<int> audioSampleRates = [
            AudioSampleRates.Rate48000Hz,
            AudioSampleRates.Rate44100Hz
        ];

        List<string> noisePresets = [
            this.GetLocalizedString("Common/Blue"),
            this.GetLocalizedString("Common/Brownian"),
            this.GetLocalizedString("Common/White")
        ];

        string shortHertzText = this.GetLocalizedString("Units/Hertz/Short");

        LocalizedAudioSampleRates = audioSampleRates.ToDictionary(
            keySelector:     value => $"{value} {shortHertzText}",
            elementSelector: value => value
        );

        LocalizedApplicationThemes = new Dictionary<string, ElementTheme>
        {
            [this.GetLocalizedString("Common/System")] = ElementTheme.Default,
            [this.GetLocalizedString("Common/Dark")]   = ElementTheme.Dark,
            [this.GetLocalizedString("Common/Light")]  = ElementTheme.Light
        };

        LocalizedLanguages = ApplicationLanguages.ManifestLanguages
            .Select(language => new CultureInfo(language))
            .ToDictionary(
                keySelector:     cultureInfo => cultureInfo.NativeName,
                elementSelector: cultureInfo => cultureInfo
            );

        LocalizedNoisePresets = noisePresets.ToDictionary(preset => preset);

        LocalizedSystemBackdrops = new Dictionary<string, SystemBackdrop>
        {
            [this.GetLocalizedString("SystemBackdrop/Mica")]    = new MicaBackdrop(),
            [this.GetLocalizedString("SystemBackdrop/MicaAlt")] = new MicaBackdrop() { Kind = MicaKind.BaseAlt },
            [this.GetLocalizedString("SystemBackdrop/Acrylic")] = new DesktopAcrylicBackdrop(),
            [this.GetLocalizedString("Common/None")]            = null!,
        };
    }

    private void RefreshLocalizedContent()
    {
        _resourceLoader = _resourceLoaderFactory();

        PopulateComboBoxControlsWithLocalizedValues();

        ApplyLocalizedContent();
    }
}