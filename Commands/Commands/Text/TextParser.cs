namespace Commands.Text;

public sealed class CommandParserResult
{
    public bool Failed { get; init; }
    public string? Name { get; init; }
    public List<string>? Args { get; init; }
}

public static class CommandParser
{ 
    /// <summary>
    /// Will split user input and turns it into something more practical.
    /// This functions expects the input to be like a command prompt command.
    /// example: "command arg1 arg2"
    /// </summary>
    /// <param name="input">The input to split up</param>
    public static CommandParserResult DefaultParse(string input)
    {
        var Split = input.Split(' ');

        if (Split.Length == 0)
            return new CommandParserResult() { Failed = true };

        if (Split.Length == 1)
        {
            return new CommandParserResult() { Name = Split[0], Args = Array.Empty<string>().ToList(), Failed = false };
        }

        return new CommandParserResult() { Name = Split[0], Args = Split.After(1), Failed = false };
    }
}

/// <summary>
/// extension for use in CommandParser
/// </summary>
public static class CommandParserExtensions
{
    public static List<T>? After<T>(this List<T> List, int StartIndex)
    {
        if (StartIndex < 0 || StartIndex >= List.Count)
            return null;

        List<T> Result = new();

        for (int i = StartIndex; i < List.Count; i++)
        {
            Result.Add(List[i]);
        }

        return Result;
    }

    public static List<T>? After<T>(this T[] List, int StartIndex)
    {
        if (StartIndex < 0 || StartIndex >= List.Length)
            return null;

        List<T> Result = new();

        for (int i = StartIndex; i < List.Length; i++)
        {
            Result.Add(List[i]);
        }

        return Result;
    }
}
