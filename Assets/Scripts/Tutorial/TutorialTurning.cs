using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTurning : DialogueState
{

    public TutorialTurning(Tutorial tutorial, Dialogue dialogue) 
        : base(tutorial, dialogue, "TutorialTurningDialogue")
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

    private string lastTyped = "";

    public override void Tick()
    {
        string decodedText = tutorial.morseCodeMachine.GetComponent<MorseCodeController>().decodedText.text;
        if (lastTyped != decodedText && decodedText.Length > 0)
        {
            lastTyped = decodedText;
        }

        int sentence = tutorial.dialogueManager.currentSentence();
        if (sentence == 4)
        {
            Button btn = tutorial.continueButton.GetComponent<Button>();
            if (lastTyped != "S" && lastTyped != "P" || tutorial.submarine.velocity.magnitude < 0.5)
            {
                btn.interactable = false;
            }
            else
            {
                btn.onClick.Invoke();
                btn.interactable = true;
            }
        }

        if (sentence > 5)
        {
            int currentIndex = tutorial.indexCurrentDialog + 3;
            Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
            tutorial.setState(new MonsterTutorialInitialDialogue(tutorial, nextDialog));
        }
    }
}
