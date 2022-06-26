using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Core;

/// <summary>
/// represents an object managing commands.
/// </summary>
public interface ICommandManager
{
    /// <summary>
    /// The collection containing the ICommand instances.
    /// </summary>
    public CommandCollection Commands { get; }

    /// <summary>
    /// The amount of commands currently being managed. 
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Add a command to this collection.
    /// </summary>
    /// <param name="command">The command to add</param>
    public void Add(ICommand command);

    /// <summary>
    /// Remove a command from this collection.
    /// </summary>
    /// <param name="command"></param>
    public void Remove(ICommand command);

    /// <summary>
    /// Check if a command exists.
    /// </summary>
    /// <param name="command">The command to verify exists</param>
    /// <returns>True if it exists, otherwise False</returns>
    public bool Contains(ICommand command);

    /// <summary>
    /// Execute a command by it's name.
    /// </summary>
    /// <param name="name">The name of the command to execute</param>
    /// <param name="arguments">The arguments to pass to the command</param>
    /// <exception cref="Exceptions.NoSuchCommandException">Thrown when the command does not exist.</exception>
    /// <exception cref="NullReferenceException">Thrown when the command instance in the collection was null.</exception>
    public void ExecuteByName(string name, List<string> arguments);

    /// <summary>
    /// Execute a command by it's name asynchronously
    /// </summary>
    /// <param name="name">The name of the command to execute</param>
    /// <param name="arguments">The arguments to pass to the command</param>
    /// <exception cref="Exceptions.NoSuchCommandException">Thrown when the command does not exist.</exception>
    /// <exception cref="NullReferenceException">Thrown when the command instance in the collection was null.</exception>
    /// <returns>The task that represents the command</returns>
    public Task? ExecuteByNameAsync(string name, List<string> arguments);
}

public interface ICommandManager<T>
{
    /// <summary>
    /// The collection containing the ICommand instances.
    /// </summary>
    public CommandCollection<T> Commands { get; }

    /// <summary>
    /// The amount of commands currently being managed. 
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Add a command to this collection.
    /// </summary>
    /// <param name="command">The command to add</param>
    public void Add(ICommand<T> command);

    /// <summary>
    /// Remove a command from this collection.
    /// </summary>
    /// <param name="command"></param>
    public void Remove(ICommand<T> command);

    /// <summary>
    /// Check if a command exists.
    /// </summary>
    /// <param name="command">The command to verify exists</param>
    /// <returns>True if it exists, otherwise False</returns>
    public bool Contains(ICommand<T> command);

    /// <summary>
    /// Execute a command by it's name.
    /// </summary>
    /// <param name="name">The name of the command to execute</param>
    /// <param name="arguments">The arguments to pass to the command</param>
    /// <exception cref="Exceptions.NoSuchCommandException">Thrown when the command does not exist.</exception>
    /// <exception cref="NullReferenceException">Thrown when the command instance in the collection was null.</exception>
    /// <returns>The value that the command returns</returns>
    public T? ExecuteByName(string name, List<string> arguments);

    /// <summary>
    /// Execute a command by it's name asynchronously
    /// </summary>
    /// <param name="name">The name of the command to execute</param>
    /// <param name="arguments">The arguments to pass to the command</param>
    /// <exception cref="Exceptions.NoSuchCommandException">Thrown when the command does not exist.</exception>
    /// <exception cref="NullReferenceException">Thrown when the command instance in the collection was null.</exception>
    /// <returns>The task that represents the command</returns>
    public Task<T>? ExecuteByNameAsync(string name, List<string> arguments);
}
