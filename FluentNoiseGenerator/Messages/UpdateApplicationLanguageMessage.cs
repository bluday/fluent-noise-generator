using System;
using System.Globalization;

namespace FluentNoiseGenerator.Messages;

/// <summary>
/// Represents a message for updating the current application language.
/// </summary>
public sealed class UpdateApplicationLanguageMessage
{
    #region Properties
    /// <summary>
    /// Gets the culture for the language to be set.
    /// </summary>
    public CultureInfo Culture { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateApplicationLanguageMessage"/>
    /// class.
    /// </summary>
    /// <param name="culture">
    /// The culture for the language to be set.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="culture"/> is <c>null</c>.
    /// </exception>
    public UpdateApplicationLanguageMessage(CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(culture);

        Culture = culture;
    }
    #endregion
}