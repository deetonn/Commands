# Commands
An easy way to create a user-command interface with a console.
# Usage
```cs
using Commands.Core;
using Commands.Text;

CommandManager<bool> Commands = new();

Commands.Add(Command<bool>.Create("help", (args) =>
{
	Console.WriteLine($"You ran the help command with arguments '{string.Join(", ", args)}'");
	return true;
}));

// add your own commands!

while (true)
{
	Console.Write(">> ");
	var Input = Console.ReadLine();
	Console.WriteLine();

	if (Input == null)
		continue;

	var TextResult = CommandParser.DefaultParse(Input);

	// below shows nullable errors, but they won't ever be null if this check is ok.
	if (TextResult.Failed)
	{
		Console.WriteLine("failed to parse your command, type 'help' for more info.");
		continue;
	}

	Commands.ExecuteByName(TextResult.Name, TextResult.Args);
}
```
Super easy to use, and can be customized for your own usage with the ICommand & the generic ICommand interfaces.

## Info
This supports commands with no return type & commands with a return type. It also uses a generic interface which means you can implement your own command if you needed to.

It supports async command execution.

It supports default text splitting, which uses the command prompt style command input which is perfect for applications accepting user input.
