using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInterpreter : MonoBehaviour
{
    public static Dictionary<string, string> basicCommands = new Dictionary<string, string>() {
        {"p", "Port"},
        {"s", "Starboard"},
        {"a", "Accelerate"},
        {"d", "Deccelerate"},
        {"o", "EnginesOff"},
        {"f", "Fire"} };

    private Dictionary<string, string> commands;

    public void Start()
    {
        commands = basicCommands;
    }

    public void InterpretCommand(string command)
    {
        string function;
        if (commands.TryGetValue(command, out function)) {
            Invoke(function, 0f);
        }
    }

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
