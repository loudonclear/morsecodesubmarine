using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInterpreter : MonoBehaviour
{
    public bool commandListen = true;

    private string[] commandFunctions = new string[] { "Port", "Starboard", "Accelerate", "Decelerate", "EnginesOff", "Fire", "Higher", "Lower" };
    [NamedArrayAttribute(new string[] { "Port", "Starboard", "Accelerate", "Decelerate", "EnginesOff", "Fire", "Higher", "Lower" })]
    public string[] morseCodeCommands = new string[] { "p", "s", "a", "d", "o", "f", "h", "l" };

    public Dictionary<string, string> commandDictionary;

    private SubmarineMovement submarineMovement;

    public void Start()
    {
        commandDictionary = new Dictionary<string, string>();
        for (int i = 0; i < morseCodeCommands.Length; i++)
        {
            commandDictionary.Add(morseCodeCommands[i], commandFunctions[i]);
        }

        submarineMovement = GameObject.FindGameObjectWithTag("Submarine").GetComponent<SubmarineMovement>();
    }

    public void InterpretCommand(string command)
    {
        if (commandListen && command != "")
        {
            string function;
            if (GameObject.FindGameObjectWithTag("Interpreter") != null)
            {
                if (commandDictionary.TryGetValue(command.ToLowerInvariant(), out function))
                {
                    GameObject.FindGameObjectWithTag("Interpreter").GetComponent<UIFlash>().Flash(Color.green);
                    Invoke(function, 0f);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Interpreter").GetComponent<UIFlash>().Flash(Color.red);
                }
            }
        }

    }

    private void Starboard()
    {
        submarineMovement.Starboard();
        GameObject.FindGameObjectWithTag("Compass").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Port()
    {
        submarineMovement.Port();
        GameObject.FindGameObjectWithTag("Compass").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Accelerate()
    {
        submarineMovement.Accelerate();
        GameObject.FindGameObjectWithTag("SpeedGauge").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Decelerate()
    {
        submarineMovement.Decelerate();
        GameObject.FindGameObjectWithTag("SpeedGauge").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void EnginesOff()
    {
        submarineMovement.EnginesOff();
        GameObject.FindGameObjectWithTag("SpeedGauge").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Higher()
    {
        submarineMovement.Ascend();
        GameObject.FindGameObjectWithTag("DepthGauge").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Lower()
    {
        submarineMovement.Descend();
        GameObject.FindGameObjectWithTag("DepthGauge").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Fire()
    {
    }
}