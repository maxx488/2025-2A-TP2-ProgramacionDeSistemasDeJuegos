using System.Collections.Generic;
using UnityEngine;

public class HelpCommand : ICommand
{
    private CommandRegistry registry;
    public HelpCommand(CommandRegistry registry) => this.registry = registry;

    public string Name => "help";
    public List<string> Aliasses => new List<string> { "h", "/help", "Help", "HELP" };
    public string Description => "Displays help for a specific command.";

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Debug.Log("Availale Commands: Aliasses, PlayAnimation\nUsage: help <command>");
            return;
        }
        Debug.Log(registry.GetHelp(args[0]));
    }
}
