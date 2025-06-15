namespace BluDay.FluentNoiseRemover.Windows;

public sealed partial class MainWindow : IApplicationResourceAware
{
    private ResourceLoader _resourceLoader;

    /// <summary>
    /// Gets a value indicating whether the playback is currently active.
    /// </summary>
    public bool IsPlaying { get; private set; }

    ResourceLoader IApplicationResourceAware.ResourceLoader => _resourceLoader;
}