using System.Collections.Generic;
using UnityEngine;

public class AliassesCommand : ICommand
{
    private CommandRegistry registry;
    public AliassesCommand(CommandRegistry registry) => this.registry = registry;

    public string Name => "aliasses";
    public List<string> Aliasses => new List<string> { "alias", "a", "/alias" };
    public string Description => "Lists aliasses for a command.";

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Debug.Log("Usage: aliasses <command>");
            return;
        }

        var cmd = registry.GetCommand(args[0]);
        if (cmd != null)
        {
            Debug.Log($"Aliasses for '{cmd.Name}': {string.Join(", ", cmd.Aliasses)}");
        }
        else
        {
            Debug.Log($"Command '{args[0]}' not found.");
        }
    }
}
