using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInterpreter : MonoBehaviour
{
    private string[] commandFunctions = new string[] { "Port", "Starboard", "Accelerate", "Deccelerate", "EnginesOff", "Fire" };
    [NamedArrayAttribute(new string[] { "Port", "Starboard", "Accelerate", "Deccelerate", "EnginesOff", "Fire" })]
    public string[] morseCodeCommands = new string[] { "p", "s", "a", "d", "o", "f" };

    public Dictionary<string, string> commandDictionary;

    public void Start()
    {
        commandDictionary = new Dictionary<string, string>();
        for (int i = 0; i < morseCodeCommands.Length; i++)
        {
            commandDictionary.Add(morseCodeCommands[i], commandFunctions[i]);
        }
    }

    public void InterpretCommand(string command)
    {
        string function;
        if (commandDictionary.TryGetValue(command, out function)) {
            Invoke(function, 0f);
        }
    }

    // TODO: Attach all commands to other scripts
    private void Starboard()
    {
        Debug.Log("Moving starboard");
    }

    private void Port()
    {
        Debug.Log("Moving to port");
    }

    private void Accelerate()
    {
        Debug.Log("Accelerating");
    }

    private void Decelerate()
    {
        Debug.Log("Decelerating");
    }

    private void EnginesOff()
    {
        Debug.Log("Engines off");
    }

    private void Fire()
    {
        Debug.Log("Firing");
    }
}