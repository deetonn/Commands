namespace Commands.Core;

public interface ICommand<T>
{
    public string Name { get; }
    public Func<List<string>, T> Action { get; } 
}
public interface ICommand
{
    public string Name { get; }
    public Action<List<string>> Action { get; }
}
