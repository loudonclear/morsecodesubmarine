using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogueManager dialogueManager;
    public Button continueButton;
    public Dialogue[] tutorialDialogs;
    private Dialogue currentDialog;
    public int indexCurrentDialog;
    public GameObject morseCodeMachine;

    private DialogueState currentState;

    void Start()
    {
        indexCurrentDialog = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {

            currentState.Tick();
        }

    }

    public void setState(DialogueState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }

    }

    void TaskOnClick()
    {
       
       // setState(new InitialDialogue(this, currentDialog));
    }
}
