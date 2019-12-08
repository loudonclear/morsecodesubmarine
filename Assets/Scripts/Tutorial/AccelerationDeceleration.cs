using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstExcercise : DialogueState
{

    public FirstExcercise(Tutorial tutorial, Dialogue dialogue) 
        : base(tutorial, dialogue, "FirstExcerciseDialogue")
    {

    }

    public override void OnStateEnter()
    {
        Button btn = tutorial.continueButton.GetComponent<Button>();
        btn.interactable = true;
        tutorial.dialogueManager.StartDialog(dialogue);
    }

    public override void OnStateExit()
    {
        
    }

    string lastTyped;

    public override void Tick()
    {
        string decodedText = tutorial.morseCodeMachine.GetComponent<MorseCodeController>().decodedText.text;
        if (lastTyped != decodedText && decodedText.Length > 0)
        {
            lastTyped = decodedText;
        }

        int sentence = tutorial.dialogueManager.currentSentence();
        if (sentence == 3)
        {
            Button btn = tutorial.continueButton.GetComponent<Button>();
            if (lastTyped != "A")
            {
                btn.interactable = false;
            } else
            {
                btn.onClick.Invoke();
                btn.interactable = true;
            }
        }

        if (sentence == 9)
        {
            Button btn = tutorial.continueButton.GetComponent<Button>();
            if (lastTyped != "D")
            {
                btn.interactable = false;
            }
            else
            {
                btn.onClick.Invoke();
                btn.interactable = true;
            }
        }

        if (sentence > 9)
        {
            tutorial.morseCodeMachine.GetComponent<CommandInterpreter>().commandListen = true;
            int currentIndex = tutorial.indexCurrentDialog + 2;
            Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
            tutorial.setState(new TutorialTurning(tutorial, nextDialog));
        }
    }
}
