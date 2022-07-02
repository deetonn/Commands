# Commands.Extensions

# Support for CommandLineParser
[CommandLine](https://github.com/commandlineparser/commandline)

This is still under development really as all behaviours needs to be tested.

The gist is, you create your own Options class with the Option attribute from commandline, as seen below:

```cs
internal sealed class Options
{
	[Option('v', "verbose")
	public bool Verbose { get; set; } = false;
}
```
(using "'" because otherwise it tries doing some format stuff)
instead of doing the parsing yourself, BindedCommand<'Binder>/BindedCommand<'Binder, 'T> will do it for you. It accepts either an Action<'Options> or a Func<`Options, T>, so your callback will just receive the parsed results instead of repeating code for each command.

So yeah, still looking into the library more.

## Example
```cs
using CommandLine;
using Commands.Core;
using Commands.Extensions;

CommandManager manager = new();

manager.Add(new CustomCommand("test.command", o =>
{
    if (o.Verbose)
    {
	foreach (var item in o.Opts)
        {
            Console.WriteLine($"Option: {item}");
        }
    }
}));

manager.ExecuteByName("test.command", new string[]
{
	"-v -O \"Option1\" \"Option2\""
}.ToList());

internal sealed class Options
{
	[Option('v', "verbose")]
	public bool Verbose { get; set; } = false;

	[Option('O', "options")]
	public IEnumerable<string> Opts { get; set; } = Array.Empty<string>();
}

internal sealed class CustomCommand : BindedCommand<Options>
{
	public CustomCommand(string name, Action<Options> action)
		: base(action)
	{
		Name = name;
	}
}
```
