using System.Collections.Generic;
using UnityEngine;

public class CommandRegistry
{
    private Dictionary<string, ICommand> commandLookup = new Dictionary<string, ICommand>();
    private Dictionary<string, string> aliasLookup = new Dictionary<string, string>();

    public void Register(ICommand command)
    {
        commandLookup[command.Name.ToLower()] = command;
        foreach (var alias in command.Aliasses)
        {
            aliasLookup[alias.ToLower()] = command.Name.ToLower();
        }
    }

    public ICommand GetCommand(string input)
    {
        string key = input.ToLower();
        if (commandLookup.ContainsKey(key))
            return commandLookup[key];

        if (aliasLookup.ContainsKey(key))
            return commandLookup[aliasLookup[key]];

        return null;
    }

    public Dictionary<string, List<string>> GetAllAliases()
    {
        Dictionary<string, List<string>> aliasses = new Dictionary<string, List<string>>();
        foreach (var cmd in commandLookup.Values)
        {
            aliasses[cmd.Name] = cmd.Aliasses;
        }
        return aliasses;
    }

    public string GetHelp(string commandName)
    {
        var command = GetCommand(commandName);
        return command != null ? $"{command.Name}: {command.Description}" : $"No help available for '{commandName}'";
    }
}
