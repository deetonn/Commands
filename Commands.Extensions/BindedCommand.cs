using Commands.Core;
using CommandLine;

namespace Commands.Extensions;

// for support with https://github.com/commandlineparser/commandline

/// <summary>
/// a command that is binded to an option class. the project must be using the CommandLineParser
/// package from nuget. (see https://github.com/commandlineparser/commandline) 
/// </summary>
/// <typeparam name="Binder">A class which implements CommandLine.OptionAttribute's</typeparam>
public class BindedCommand<Binder> : ICommand
{
    public virtual string Name { get; } = default!;
    public Action<List<string>> Action { get; } = default!;
    public virtual Action<Binder> Delegate { get; set; } = default!;
    private Binder _binder { get; set; } = default!;

    public BindedCommand(Action<Binder> action)
    {
        Delegate = action;

        Action = (args) =>
        {
            _binder = Parser.Default.ParseArguments<Binder>(args).Value;
            Delegate(_binder);
        };
    }
}

/// <summary>
/// a command that returns <typeparamref name="T"/> that is binded to an option class. the project must be using the CommandLineParser
/// package from nuget. (see https://github.com/commandlineparser/commandline) 
/// </summary>
/// <typeparam name="Binder">A class which implements CommandLine.OptionAttribute's</typeparam>
public class BindedCommand<Binder, T> : ICommand<T>
{
    public virtual string Name { get; } = default!;
    public Func<List<string>, T> Action { get; } = default!;
    public virtual Func<Binder, T> Delegate { get; set; } = default!;
    private Binder _binder { get; set; } = default!;

    public BindedCommand(Func<Binder, T> action)
    {
        Delegate = action;

        Action = (args) =>
        {
            _binder = Parser.Default.ParseArguments<Binder>(args)
            .WithParsed(x =>
            {
                _binder = x;
            })
            .WithNotParsed(x =>
            {
                if (x.IsVersion())
                {

                }
            })
            .Value;
            return Delegate(_binder);
        };
    }
}
