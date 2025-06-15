namespace BluDay.FluentNoiseRemover.Common;

/// <summary>
/// Defines an interface for objects that provide access to application resources.
/// </summary>
public interface IApplicationResourceAware
{
    /// <summary>
    /// Gets the resource loader instance, which provides localized resources for
    /// the application.
    /// </summary>
    ResourceLoader ResourceLoader { get; }
}