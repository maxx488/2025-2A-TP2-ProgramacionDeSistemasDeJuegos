using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string Name { get; }
    List<string> Aliasses { get; }
    string Description { get; }
    void Execute(string[] args);
}
