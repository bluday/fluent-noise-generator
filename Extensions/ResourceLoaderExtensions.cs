namespace BluDay.FluentNoiseRemover.Extensions;

/// <summary>
/// A utility with method extensions for the <see cref="ResourceLoader"/> type.
/// </summary>
public static class ResourceLoaderExtensions
{
    /// <inheritdoc cref="ResourceLoader.GetString(string)"/>
    /// <param name="source">
    /// The object providing access to the application resource loader.
    /// </param>
    public static string GetLocalizedString(this IApplicationResourceAware source, string resourceId)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(resourceId);

        return source.ResourceLoader.GetString(resourceId);
    }

    /// <inheritdoc cref="GetUriFromLocalizedString(IApplicationResourceAware, string, UriKind)"/>
    public static Uri GetUriFromLocalizedString(this IApplicationResourceAware source, string resourceId)
    {
        return source.GetUriFromLocalizedString(resourceId, uriKind: default);
    }

    /// <summary>
    /// Gets a URI instance from a resource string.
    /// </summary>
    /// <param name="source">
    /// The object providing access to the application resource loader.
    /// </param>
    /// <param name="resourceId">
    /// The resource identifier.
    /// </param>
    /// <param name="uriKind">
    /// The URI kind.
    /// </param>
    /// <returns>
    /// A <see cref="Uri"/> instance configured by the localized string.
    /// </returns>
    public static Uri GetUriFromLocalizedString(this IApplicationResourceAware source, string resourceId, UriKind uriKind)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(resourceId);

        return new Uri(source.ResourceLoader.GetString(resourceId));
    }
}