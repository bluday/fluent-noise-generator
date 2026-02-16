using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator.UI.Settings.ViewModels;

namespace FluentNoiseGenerator.UI.Settings.Windows;

/// <summary>
/// Represents a factory for creating <see cref="SettingsWindow"/> instances.
/// </summary>
public sealed class SettingsWindowFactory
{
    /// <summary>
    /// Creates a new <see cref="SettingsWindow"/> instance with its required dependencies.
    /// </summary>
    /// <returns>
    /// The created window instance.
    /// </returns>
    public static SettingsWindow Create()
    {
        return new()
        {
            ViewModel = Ioc.Default.GetRequiredService<SettingsViewModel>()
        };
    }
}