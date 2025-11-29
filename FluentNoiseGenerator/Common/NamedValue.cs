using System;

namespace FluentNoiseGenerator.Common;

/// <summary>
/// Represents a value of type <typeparamref name="TValue"/> with an optional displayable name
/// and an optional custom formatter function for converting the value to a string. Useful for
/// associating a name and custom formatting to a value for display or logging purposes.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value contained in the <see cref="NamedValue{TValue}"/>.
/// </typeparam>
public readonly struct NamedValue<TValue>
{
    #region Fields
    private readonly TValue _value; 

    private readonly string? _name;

    private readonly Func<TValue, string>? _formatter;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the value stored in this <see cref="NamedValue{TValue}"/>.
    /// </summary>
    public TValue Value => _value;

    /// <summary>
    /// Gets the optional formatter function that can be used to convert the value to a string
    /// when <see cref="ToString"/> is called. 
    /// </summary>
    public Func<TValue, string>? Formatter => _formatter;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="NamedValue{TValue}"/> structure 
    /// with the specified value. The value has no associated name or formatter.
    /// </summary>
    /// <param name="value">
    /// The value to store in the <see cref="NamedValue{TValue}"/>.
    /// </param>
    public NamedValue(TValue value)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedValue{TValue}"/> structure with the
    /// specified value and a displayable name for the value. The name is used for string
    /// representation when no formatter is provided.
    /// </summary>
    /// <param name="value">
    /// The value to store in the <see cref="NamedValue{TValue}"/>.
    /// </param>
    /// <param name="name">
    /// The displayable name for the value. This can be used for logging or other purposes
    /// where a string representation is needed.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="name"/> is <c>null</c> or consists only of whitespace.
    /// </exception>
    public NamedValue(TValue value, string name) : this(value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        _name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NamedValue{TValue}"/> structure with the
    /// specified value, displayable name, and a custom formatter function to be used when
    /// calling <see cref="ToString"/>.
    /// </summary>
    /// <param name="value">
    /// The value to store in the <see cref="NamedValue{TValue}"/>.
    /// </param>
    /// <param name="formatter">
    /// A function used to format the value when calling <see cref="ToString"/>. The function
    /// takes a <typeparamref name="TValue"/> and returns a formatted string.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="formatter"/> is <c>null</c>.
    /// </exception>
    public NamedValue(TValue value, Func<TValue, string> formatter) : this(value)
    {
        ArgumentNullException.ThrowIfNull(formatter);

        _formatter = formatter;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Returns a string representation of the value, formatted either by the optional 
    /// <see cref="Formatter"/> function, or by the optional <see cref="Name"/> if no 
    /// formatter is provided.
    /// </summary>
    /// <returns>
    /// A string representation of the value, formatted by the formatter if available,
    /// or by the name if no formatter is provided.
    /// </returns>
    public override string ToString()
    {
        return _formatter?.Invoke(_value) ?? _name!;
    }
    #endregion
}