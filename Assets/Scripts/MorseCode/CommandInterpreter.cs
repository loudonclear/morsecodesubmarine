using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInterpreter : MonoBehaviour
{
    private string[] commandFunctions = new string[] { "Port", "Starboard", "Accelerate", "Decelerate", "EnginesOff", "Fire" };
    [NamedArrayAttribute(new string[] { "Port", "Starboard", "Accelerate", "Decelerate", "EnginesOff", "Fire" })]
    public string[] morseCodeCommands = new string[] { "p", "s", "a", "d", "o", "f" };

    public Dictionary<string, string> commandDictionary;

    public SubmarineMovement submarineMovement;

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
        if (commandDictionary.TryGetValue(command.ToLowerInvariant(), out function)) {
            Invoke(function, 0f);
        }
    }

    // TODO: Attach all commands to other scripts
    private void Starboard()
    {
        submarineMovement.Starboard();
    }

    private void Port()
    {
        submarineMovement.Port();
    }

    private void Accelerate()
    {
        submarineMovement.Accelerate();
    }

    private void Decelerate()
    {
        submarineMovement.Decelerate();
    }

    private void EnginesOff()
    {
        submarineMovement.EnginesOff();
    }

    private void Fire()
    {
        Debug.Log("Firing");
    }
}