using Xunit;

using Commands.Core;
using Commands.Extensions;

using System.Linq;
using System.Collections.Generic;
using Commands.Text;

namespace Commands.Tests;

public class BasicFunctionality
{
    [Fact]
    public void Command_Int_Functionality()
    {
        ICommand<int> @command = Command<int>.Create("ShouldReturn2010", _ =>
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

    /// <summary>
    /// Somehow this is running below 1ms lmao so that's nice I suppose
    /// </summary>
    [Fact]
    public void DefaultParserExpectedResult()
    {
        string text = "info --help -v \"...\"";
        string expected_command = "info";
        List<string> expected_arguments = new()
        {
            "--help",
            "-v",
            "\"...\""
        };

        var result = CommandParser.DefaultParse(text);
        Assert.Equal(expected_command, result.Name);
        Assert.Equal(expected_arguments, result.Args);
    }

    /// TODO: make tests for all classes in "DefaultCommandManagers.cs".
    /// most of them just wrap a standard class, so chances of them being fine are high.
    /// but just to be on the safe side.
}
