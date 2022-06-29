using Commands.Core;
using CommandLine;

namespace Commands.Extensions;

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
