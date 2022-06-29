using Commands.Core;
using Commands.Extensions;

using CommandLine;
using System;

namespace Commands.Tests;

public class CustomCommandOptions
{
    [Option('v', "verbose")]
    public bool Verbose { get; set; } = false;
}

public class TestCustomCommand : BindedCommand<CustomCommandOptions, CustomCommandOptions>
{
    public TestCustomCommand()
        : base(null!)
    {
    }

    public override Func<CustomCommandOptions, CustomCommandOptions> Delegate => delegate (CustomCommandOptions commandOptions)
    {
        return commandOptions;
    }; 
}
