using System.Collections.Generic;

namespace FluentNoiseGenerator.UI.Common.Services;

/// <summary>
/// Service for managing the shared system backdrop between all windows.
/// </summary>
public interface IBackdropService
{
    /// <summary>
    /// Gets or sets the current system backdrop.
    /// </summary>
    object? CurrentBackdrop { get; set; }

    /// <summary>
    /// Gets an enumerable of system backdrops.
    /// </summary>
    IEnumerable<object> Backdrops { get; }
}