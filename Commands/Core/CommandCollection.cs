
using System.Collections;

namespace Commands.Core;

public class CommandCollection : ICollection<ICommand>
{
    public int Count => _commands.Count;
    public bool IsReadOnly => false;

    private List<ICommand> _commands = new();

    public void Add(ICommand item)
    {
        _commands.Add(item);
    }

    public void Clear()
    {
        _commands.Clear();
    }

    public bool Contains(ICommand item)
        => _commands.Contains(item);

    public void CopyTo(ICommand[] array, int arrayIndex)
    {
        _commands.CopyTo(array, arrayIndex);
    }

    public IEnumerator<ICommand> GetEnumerator()
        => _commands.GetEnumerator();

    public bool Remove(ICommand item)
        => _commands.Remove(item);

    IEnumerator IEnumerable.GetEnumerator()
        => _commands.GetEnumerator();
}
public class CommandCollection<T> : ICollection<ICommand<T>>
{
    public int Count => _commands.Count;
    public bool IsReadOnly => false;

    private readonly List<ICommand<T>> _commands = new();

    public void Add(ICommand<T> item)
    {
        _commands.Add(item);
    }

    public void Clear()
    {
        _commands.Clear();
    }

    public bool Contains(ICommand<T> item)
        => _commands.Contains(item);

    public void CopyTo(ICommand<T>[] array, int arrayIndex)
    {
        _commands.CopyTo(array, arrayIndex);
    }

    public IEnumerator<ICommand<T>> GetEnumerator()
        => _commands.GetEnumerator();

    public bool Remove(ICommand<T> item)
        => _commands.Remove(item);

    IEnumerator IEnumerable.GetEnumerator()
        => _commands.GetEnumerator();
}
