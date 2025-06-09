namespace BluDay.FluentNoiseRemover.Common;

/// <summary>
/// Represents an implementation for the <see cref="ICommand"/> interface.
/// </summary>
public sealed class RelayCommand : ICommand
{
    private readonly Action<object> _execute;

    private readonly Func<object, bool>? _canExecute;

    /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
    public event EventHandler? CanExecuteChanged;

    /// <inheritdoc cref="RelayCommand(Action{object}, Func{object, bool}?)"/>
    public RelayCommand(Action execute) : this((_) => execute(), null) { }

    /// <inheritdoc cref="RelayCommand(Action{object}, Func{object, bool}?)"/>
    public RelayCommand(Action<object> execute) : this(execute, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">
    /// The function to execute.
    /// </param>
    /// <param name="canExecute">
    /// A predicate function returning a <see cref="bool"/> value for determining
    /// whether the command can be executed or not.
    /// </param>
    public RelayCommand(Action<object> execute, Func<object, bool>? canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);

        _execute = execute;

        _canExecute = canExecute;
    }

    /// <inheritdoc cref="ICommand.CanExecute(object?)"/>
    public bool CanExecute(object? parameter)
    {
        bool? canExecute = _canExecute?.Invoke(parameter!);

        if (canExecute.HasValue && canExecute.Value)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        return canExecute ?? true;
    }

    /// <inheritdoc cref="ICommand.Execute(object?)"/>
    public void Execute(object? parameter)
    {
        _execute.Invoke(parameter!);
    }
}