namespace FluentNoiseGenerator.Common.Localization;

/// <summary>
/// Represents a value of type <typeparamref name="TValue"/> that is associated with a localized
/// string resource.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value contained in the <see cref="ResourceNamedValue{TValue}"/>.
/// </typeparam>
public readonly struct ResourceNamedValue<TValue>
{
    #region Properties
    /// <summary>
    /// Gets the the <see cref="StringResource"/> instance associated the value.
    /// </summary>
    public StringResource Resource { get; }

    /// <summary>
    /// Gets the underlying value stored in this <see cref="ResourceNamedValue{TValue}"/>.
    /// </summary>
    public TValue? Value { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceNamedValue{TValue}"/> structure
    /// using the specified value and a <see cref="StringResource"/> instance for its name.
    /// </summary>
    /// <param name="resource">
    /// The resource instance to associate the value with.
    /// </param>
    /// <param name="value">
    /// The value to store.
    /// </param>
    public ResourceNamedValue(StringResource resource, TValue? value)
    {
        Resource = resource;

        Value = value;
    }
    #endregion
}