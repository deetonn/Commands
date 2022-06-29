using Commands.Core.Exceptions;

namespace Commands.Core;

public class CommandManager : ICommandManager
{
    public CommandCollection Commands { get; private set; }

    public int Count => Commands.Count;

    public CommandManager()
    {
        Commands = new CommandCollection();
    }

    public void Add(ICommand command)
    {
        Commands.Add(command);
    }

    public void AddDefault(string name, Action<List<string>> action)
        => Add(new BaseCommand(name, action));

    public bool Contains(ICommand command)
        => Commands.Contains(command);
    public bool Contains(string name)
        => Commands.Where(c => c.Name == name).Any();

    public void ExecuteByName(string name, List<string> arguments)
    {
        if (Count == 0)
            throw new NoSuchCommandException($"no commands have been added yet, we therefore cannot execute '{name}'");

        var Matches = Commands.Where(x => x.Name == name).ToList();

        if (!Matches.Any())
            throw new NoSuchCommandException($"couldn't find a command named '{name}'");

        var Command = (BaseCommand?)Commands.First(x => x.Name == name);

        if (Command is null)
            throw new NullReferenceException($"found command '{name}', but it was null.");

        Command.Invoke(arguments);
    }
    public Task? ExecuteByNameAsync(string name, List<string> arguments)
    {
        if (Count == 0)
            throw new NoSuchCommandException($"no commands have been added yet, we therefore cannot execute '{name}'");

        var Matches = Commands.Where(x => x.Name == name).ToList();

        if (!Matches.Any())
            throw new NoSuchCommandException($"couldn't find a command named '{name}'");

        var Command = (BaseCommand?)Commands.FirstOrDefault(x => x.Name == name);

        if (Command is null)
            throw new NullReferenceException($"found command '{name}', but it was null.");

        return Command.BeginInvoke(arguments);
    }

    public void Remove(ICommand command)
        => Commands.Remove(command);    
}
public class CommandManager<T> : ICommandManager<T>
{
    public CommandCollection<T> Commands { get; private set; }

    public int Count => Commands.Count;

    public CommandManager()
    {
        Commands = new CommandCollection<T>();
    }

    public void Add(ICommand<T> command)
    {
        Commands.Add(command);
    }

    public void AddDefault(string name, Func<List<string>, T> action)
        => Add(new BaseCommand<T>(name, action));

    public bool Contains(ICommand<T> command)
        => Commands.Contains(command);

    public T? ExecuteByName(string name, List<string> arguments)
    {
        if (Count == 0)
            throw new NoSuchCommandException($"no commands have been added yet, we therefore cannot execute '{name}'");

        var Matches = Commands.Where(x => x.Name == name).ToList();

        if (!Matches.Any())
            throw new NoSuchCommandException($"couldn't find a command named '{name}'");

        var Command = (BaseCommand<T>?)Commands.FirstOrDefault(x => x.Name == name);

        if (Command is null)
            throw new NullReferenceException($"found command '{name}', but it was null.");

        return Command.Invoke(arguments);
    }

    public Task<T>? ExecuteByNameAsync(string name, List<string> arguments)
    {
        if (Count == 0)
            throw new NoSuchCommandException($"no commands have been added yet, we therefore cannot execute '{name}'");

        var Matches = Commands.Where(x => x.Name == name).ToList();

        if (!Matches.Any())
            throw new NoSuchCommandException($"couldn't find a command named '{name}'");

        var Command = (BaseCommand<T>?)Commands.FirstOrDefault(x => x.Name == name);

        if (Command is null)
            throw new NullReferenceException($"found command '{name}', but it was null.");

        return Command.BeginInvoke(arguments);
    }

    public void Remove(ICommand<T> command)
        => Commands.Remove(command);
}

/// <summary>
/// This class does not implement ICommandManager
/// </summary>
public class CommandQueue
{
    protected Queue<ICommand> @Commands { get; set; }
    public int Count => @Commands.Count;

    public CommandQueue()
    {
        @Commands = new Queue<ICommand>();
    }
    public CommandQueue(int capacity)
    {
        @Commands = new Queue<ICommand>(capacity);
    }

    public void Add(ICommand command)
        => @Commands.Enqueue(command);
    public ICommand GetNext()
        => Commands.Dequeue();
    public bool Contains(ICommand command)
        => @Commands.Contains(command);
    public bool Contains(string name)
        => Commands.Any(x => x.Name == name);
}

/// <summary>
/// This class does not implement ICommandManager
/// </summary>
/// <typeparam name="T">The type the command should return</typeparam>
public class CommandQueue<T>
{
    protected Queue<ICommand<T>> @Commands { get; set; }
    public int Count => @Commands.Count;

    public CommandQueue()
    {
        @Commands = new Queue<ICommand<T>>();
    }
    public CommandQueue(int capacity)
    {
        @Commands = new Queue<ICommand<T>>(capacity);
    }

    public void Add(ICommand<T> command)
        => @Commands.Enqueue(command);
    public ICommand<T> GetNext()
        => Commands.Dequeue();
    public bool Contains(ICommand<T> command)
        => @Commands.Contains(command);
    public bool Contains(string name)
        => Commands.Any(x => x.Name == name);
}

/// <summary>
/// This class does not implement ICommandManager
/// </summary>
public class CommandStack
{
    protected Stack<ICommand> @Commands { get; set; }
    public int Count => @Commands.Count;

    public CommandStack()
        => @Commands = new Stack<ICommand>();
    public CommandStack(int capacity)
        => @Commands = new Stack<ICommand>(capacity);

    public void Push(ICommand command)
        => @Commands.Push(command);
    public ICommand Pop()
        => @Commands.Pop();
    public ICommand Peek()
        => @Commands.Peek();
    public void Clear()
        => @Commands.Clear();
}

/// <summary>
/// This class does not implement ICommandManager
/// </summary>
/// <typeparam name="T">The type the command should return</typeparam>
public class CommandStack<T>
{
    protected Stack<ICommand<T>> @Commands { get; set; }
    public int Count => @Commands.Count;

    public CommandStack()
        => @Commands = new Stack<ICommand<T>>();
    public CommandStack(int capacity)
        => @Commands = new Stack<ICommand<T>>(capacity);

    public void Push(ICommand<T> command)
        => @Commands.Push(command);
    public ICommand<T> Pop()
        => @Commands.Pop();
    public ICommand<T> Peek()
        => @Commands.Peek();
    public void Clear()
        => @Commands.Clear();
}
