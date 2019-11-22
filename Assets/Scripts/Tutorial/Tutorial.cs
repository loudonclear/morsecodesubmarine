using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogueManager dialogueManager;
    public Button continueButton;
    public Button starButton;
    public Dialogue[] tutorialDialogs;
    private Dialogue currentDialog;
    private int indexCurrentDialog;

    private DialogueState currentState;

    void Start()
    {
        Button btn = starButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
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
        currentDialog  = tutorialDialogs[indexCurrentDialog];
        setState(new InitialDialogue(this, currentDialog));
        
    }
}
