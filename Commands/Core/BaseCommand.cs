

namespace Commands.Core;

public class BaseCommand<T> : ICommand<T>
{
    public string Name { get; init; } = string.Empty;
    public Func<List<string>, T> Action { get; init; } = delegate (List<string> _) { return default!; };

    public BaseCommand()
    {

    }
    public BaseCommand(string Name, Func<List<string>, T> Callback)
    {
        this.Name = Name;
        Action = Callback;
    }

    public T Invoke(List<string> arguments)
        => Action(arguments);

    public Task<T> BeginInvoke(List<string> arguments)
        => Task.Run(() => Invoke(arguments));
}
public class BaseCommand : ICommand
{
    public string Name { get; init; } = string.Empty;
    public Action<List<string>> Action { get; init; } = delegate (List<string> _) {};

    public BaseCommand()
    {

    }
    public BaseCommand(string Name, Action<List<string>> Callback)
    {
        this.Name = Name;
        this.Action = Callback;
    }

    public void Invoke(List<string> arguments)
        => Action(arguments);
    public Task BeginInvoke(List<string> arguments)
        => Task.Run(() => Invoke(arguments));
}

public class Command : BaseCommand 
{
    public Command(string Name, Action<List<string>> Callback)
        : base(Name, Callback)
    { }

    public static Command Create(string Name, Action<List<string>> Callback)
        => new(Name, Callback);
}
public class Command<T> : BaseCommand<T> 
{
    public Command(string Name, Func<List<string>, T> Callback)
    : base(Name, Callback)
    { }

    public static Command<T> Create(string Name, Func<List<string>, T> Callback)
        => new(Name, Callback);
}
