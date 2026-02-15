using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator.UI.Playback.ViewModels;

namespace FluentNoiseGenerator.UI.Playback.Windows;

/// <summary>
/// Represents a factory for creating <see cref="PlaybackWindow"/> instances.
/// </summary>
public sealed class PlaybackWindowFactory
{
    /// <summary>
    /// Creates a new <see cref="PlaybackWindow"/> instance with its required dependencies.
    /// </summary>
    /// <returns>
    /// The created window instance.
    /// </returns>
    public static PlaybackWindow Create()
    {
        return new()
        {
            ViewModel = Ioc.Default.GetRequiredService<PlaybackViewModel>()
        };
    }
}