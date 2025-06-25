using FluentNoiseRemover.Common;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FluentNoiseRemover.Windows;

public partial class SettingsWindow
{
    private void ApplyLocalizedContent()
    {
        string displayName  = _resourceLoader.GetString("General/AppDisplayName");
        string settingsText = _resourceLoader.GetString("Common/Settings");

        Title = settingsText;

        TitleBar.Title = displayName;

        HeaderTextBlock.Text = settingsText;

        AppearanceSettingsSectionHeader.Header = _resourceLoader.GetString("Common/Appearance");

        AlwaysOnTopSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Header");
        AlwaysOnTopSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/AlwaysOnTop/Description");

        ApplicationThemeSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Header");
        ApplicationThemeSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/ApplicationTheme/Description");

        ApplicationThemeComboBox.ItemsSource = LocalizedApplicationThemes.Keys;

        SystemBackdropSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Header");
        SystemBackdropSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Appearance/SystemBackdrop/Description");

        SystemBackdropComboBox.ItemsSource = LocalizedSystemBackdrops.Keys;

        GeneralSettingsSectionHeader.Header = _resourceLoader.GetString("Common/General");

        AutoplayOnLaunchSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Header");
        AutoplayOnLaunchSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/AutoplayOnLaunch/Description");

        AutoplayOnLaunchToggleSwitch.OnContent  = _resourceLoader.GetString("Common/On");
        AutoplayOnLaunchToggleSwitch.OffContent = _resourceLoader.GetString("Common/Off");

        DefaultNoisePresetSettingsCard.Header      = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Header");
        DefaultNoisePresetSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/DefaultNoisePreset/Description");

        DefaultNoisePresetComboBox.ItemsSource = LocalizedNoisePresets.Keys;

        LanguageSettingsCard.Header      = _resourceLoader.GetString("Common/Language");
        LanguageSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/General/Language/Description");

        LanguageComboBox.ItemsSource = LocalizedLanguages.Keys;

        SoundSettingsSectionHeader.Header = _resourceLoader.GetString("Common/Sound");

        AudioSampleRateSettingsCard.Header      = _resourceLoader.GetString("Common/SampleRate");
        AudioSampleRateSettingsCard.Description = _resourceLoader.GetString("SettingsWindow/Sound/SampleRate/Description");

        AudioSampleRateComboBox.ItemsSource = LocalizedAudioSampleRates.Keys;

        AboutSettingsSectionHeader.Header = _resourceLoader.GetString("Common/About");

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

    private void PopulateComboBoxControlsWithLocalizedValues()
    {
        List<int> audioSampleRates = [
            AudioSampleRates.Rate48000Hz,
            AudioSampleRates.Rate44100Hz
        ];

        List<string> noisePresets = [
            _resourceLoader.GetString("Common/Blue"),
            _resourceLoader.GetString("Common/Brownian"),
            _resourceLoader.GetString("Common/White")
        ];

        string shortHertzText = _resourceLoader.GetString("Units/Hertz/Short");

        LocalizedAudioSampleRates = audioSampleRates.ToDictionary(
            keySelector:     value => $"{value} {shortHertzText}",
            elementSelector: value => value
        );

        LocalizedApplicationThemes = new Dictionary<string, ElementTheme>
        {
            [_resourceLoader.GetString("Common/System")] = ElementTheme.Default,
            [_resourceLoader.GetString("Common/Dark")]   = ElementTheme.Dark,
            [_resourceLoader.GetString("Common/Light")]  = ElementTheme.Light
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
            [_resourceLoader.GetString("SystemBackdrop/Mica")]    = new MicaBackdrop(),
            [_resourceLoader.GetString("SystemBackdrop/MicaAlt")] = new MicaBackdrop() { Kind = MicaKind.BaseAlt },
            [_resourceLoader.GetString("SystemBackdrop/Acrylic")] = new DesktopAcrylicBackdrop(),
            [_resourceLoader.GetString("Common/None")]            = null!,
        };
    }

    private void RefreshLocalizedContent()
    {
        _resourceLoader = new ResourceLoader();

        PopulateComboBoxControlsWithLocalizedValues();

        ApplyLocalizedContent();
    }
}