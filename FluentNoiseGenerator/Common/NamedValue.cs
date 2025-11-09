using System;

namespace FluentNoiseGenerator.Common;

public readonly struct NamedValue<TValue>
{
    private readonly TValue _value; 

    private readonly string? _name;

    private readonly Func<TValue, string>? _formatter;

    public TValue Value => _value;

    public Func<TValue, string>? Formatter => _formatter;

    public NamedValue(TValue value)
    {
        _value = value;
    }

    public NamedValue(TValue value, string name) : this(value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        _name = name;
    }

    public NamedValue(TValue value, Func<TValue, string> formatter) : this(value)
    {
        ArgumentNullException.ThrowIfNull(formatter);

        _formatter = formatter;
    }

    public override string ToString()
    {
        return _formatter?.Invoke(_value) ?? _name!;
    }
}