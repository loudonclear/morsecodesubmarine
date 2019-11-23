using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCommand : MonoBehaviour
{
    public Dictionary<string, string> commandDictionary;
    public gameManager managerScript;
    public SubmarineMovement submarineMovement;

    public void Start()
    {
        
    }

    public void InterpretCommand(string command)
    {
        if (command != "")
        {
            managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
            if (managerScript != null)
            {
                managerScript.shipName = command;
            }
        }
    }

}