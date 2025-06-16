namespace BluDay.FluentNoiseRemover.Windows;

public partial class MainWindow
{
    /// <summary>
    /// Triggers when a new settings window has been created.
    /// </summary>
    public event EventHandler SettingsWindowCreated;

    private void MainWindow_Closed(object sender, WindowEventArgs args)
    {
        _hasClosed = true;
    }

    private void LayoutRoot_LayoutUpdated(object sender, object e)
    {
        UpdateNonClientInputRegions();
    }

    private void PlaybackControlPanel_PlaybackButtonClicked(object sender, EventArgs e)
    {
        TogglePlayback();
    }

    private void TopActionBar_CloseButtonClicked(object sender, EventArgs e)
    {
        Close();
    }

    private void TopActionBar_SettingsButtonClicked(object sender, EventArgs e)
    {
        _settingsWindowFactory();
    }
}