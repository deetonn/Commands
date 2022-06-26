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