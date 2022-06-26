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

    public bool Contains(ICommand command)
        => Commands.Contains(command);

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
