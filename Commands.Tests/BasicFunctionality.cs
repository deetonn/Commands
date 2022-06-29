using Xunit;

using Commands.Core;
using Commands.Extensions;

using System.Linq;

namespace Commands.Tests;

public class BasicFunctionality
{
    [Fact]
    public void Command_Int_Functionality()
    {
        ICommand<int> @command = Command<int>.Create("ShouldReturn2010", (_) =>
        {
            return 2010;
        });

        Assert.Equal("2010", command.Action(null!).ToString()!);
    }

    [Fact]
    public void CorrectArgumentsPassed()
    {
        var expected = new[] { "--help" };

        ICommand @command = Command.Create("CorrectArgumentsPassed", (args) =>
        {
            Assert.Equal(args, expected);
        });

        command.Action(expected.ToList());
    }

    [Fact]
    public void CustomCommandFunctioning()
    {
        BindedCommand<CustomCommandOptions, CustomCommandOptions> custom = new TestCustomCommand();

        Assert.True(custom.Action((new string[1] { "-v" }).ToList()).Verbose);
    }
}
