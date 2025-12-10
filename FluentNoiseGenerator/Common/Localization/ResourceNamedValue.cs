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
    public StringResource Name { get; }

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
    /// <param name="name">
    /// The localized name for the value as a <see cref="StringResource"/> instance.
    /// </param>
    /// <param name="value">
    /// The value to store.
    /// </param>
    public ResourceNamedValue(StringResource name, TValue? value)
    {
        Name = name;

        Value = value;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Returns the localized name string resource for this value by returning the representable
    /// string value of the resource.
    /// </summary>
    /// <returns>
    /// A localized string corresponding to the resource identifier specified during
    /// construction of this instance.
    /// </returns>
    public override string ToString() => Name.Value!;
    #endregion
}