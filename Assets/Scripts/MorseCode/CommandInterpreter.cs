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
        if (command != "")
        {
            string function;
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

    // TODO: Attach all commands to other scripts
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
        GameObject.FindGameObjectWithTag("Background").GetComponent<ScreenShaker>().ScreenShakeForTime(2, 2);
    }

    private void Decelerate()
    {
        submarineMovement.Decelerate();
        GameObject.FindGameObjectWithTag("SpeedGauge").GetComponent<UIFlash>().Flash(Color.green);
        GameObject.FindGameObjectWithTag("Background").GetComponent<ScreenShaker>().ScreenShakeForTime(2, 2);
    }

    private void EnginesOff()
    {
        submarineMovement.EnginesOff();
        GameObject.FindGameObjectWithTag("SpeedGauge").GetComponent<UIFlash>().Flash(Color.green);
    }

    private void Fire()
    {
        Debug.Log("Firing");
    }
}