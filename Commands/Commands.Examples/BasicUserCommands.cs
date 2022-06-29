using Commands.Core;
using Commands.Text;

namespace Commands.Examples
{
    internal class BasicUserCommands
    {
        // switch to Console Application to run this!
        internal static void Main()
        {
            // create a command manager where commands do not return anything
            var Handler = new CommandManager();

            // register any commands you need
            Handler.Add(new BaseCommand("help", args =>
            {
                Console.WriteLine($"You ran help with arguments {string.Join(", ", args)}!");
            }));

            // or default add
            Handler.AddDefault("name", args =>
            {
                Console.WriteLine("my name is: Deeton Rushton");
            });

            // start command loop

            while (true)
            {
                Console.Write(">> ");
                var Input = Console.ReadLine();
                Console.Write("\n");

                // use default command format
                var CommandInfo = CommandParser.DefaultParse(Input!);

                // make sure parser didnt fail
                if (CommandInfo.Failed)
                {
                    Console.WriteLine("failed to parse input");
                    continue;
                }

                // make sure specified command exists
                if (!Handler.Contains(CommandInfo.Name!))
                {
                    Console.WriteLine($"unknown command '{CommandInfo.Name!}'");
                    continue;
                }

                // execute the command
                Handler.ExecuteByName(CommandInfo.Name!, CommandInfo.Args!);
            }
        }
    }

    internal class CustomCommands
    {
        internal sealed class CustomCommand<RetType> : BaseCommand<RetType> // OR ICommand<RetType>
        {
            public new string Name { get; private set; } = string.Empty;

            public new Func<List<string>, RetType> Action { get; private set; } = default!;

            // implement whatever you want below

            public CustomCommand(string name, Func<List<string>, RetType> callback)
                : base(name, callback)
            { }
        }

        internal static void Main2()
        {
            var Manager = new CommandManager<bool>();

            Manager.Add(new CustomCommand<bool>("help", args =>
            {
                Console.WriteLine("help: custom command!");
                return true;
            }));
        }
    }

    internal class CustomManagers
    {


        //internal sealed class CustomManager<T> : ICommandManager<T>
        //{

        //}
    }
}
