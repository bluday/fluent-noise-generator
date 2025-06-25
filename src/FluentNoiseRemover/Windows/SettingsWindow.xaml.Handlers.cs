using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FluentNoiseRemover.Windows;

public partial class SettingsWindow
{
    /// <summary>
    /// Triggers when a new application theme gets selected.
    /// </summary>
    public event EventHandler<ElementTheme> ApplicationThemeChanged;

    /// <summary>
    /// Triggers when a new system backdrop gets selected.
    /// </summary>
    public event EventHandler<SystemBackdrop?> SystemBackdropChanged;

    private void ApplicationThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not string key)
        {
            return;
        }

        if (!LocalizedApplicationThemes.TryGetValue(key, out ElementTheme theme))
        {
            return;
        }

        ApplicationThemeChanged.Invoke(this, theme);
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not string key)
        {
            return;
        }

        if (!LocalizedLanguages.TryGetValue(key, out CultureInfo? cultureInfo))
        {
            return;
        }

        if (ApplicationLanguages.PrimaryLanguageOverride == cultureInfo!.Name)
        {
            return;
        }

        ApplicationLanguages.PrimaryLanguageOverride = cultureInfo.Name;

        RefreshLocalizedContent();
    }

    private void SystemBackdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.FirstOrDefault() is not string key)
        {
            return;
        }

        SystemBackdrop? systemBackdrop = LocalizedSystemBackdrops.GetValueOrDefault(key);

        if (SystemBackdrop == systemBackdrop)
        {
            return;
        }

        SystemBackdrop = systemBackdrop;

        SystemBackdropChanged?.Invoke(this, systemBackdrop);
    }

    private void SettingsWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }
}