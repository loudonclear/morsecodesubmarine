using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstExcercise : DialogueState
{
    private int totalSentences;

    public FirstExcercise(Tutorial tutorial, Dialogue dialogue) 
        : base(tutorial, dialogue, "FirstExcerciseDialogue")
    {

    }

    public override void OnStateEnter()
    {
        this.totalSentences = dialogue.sentences.Length;
        Button btn = tutorial.continueButton.GetComponent<Button>();
        btn.interactable = true;
        tutorial.dialogueManager.StartDialog(dialogue);
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
        int sentence = tutorial.dialogueManager.currentSentence();
        if (sentence == 3)
        {
            Button btn = tutorial.continueButton.GetComponent<Button>();
            btn.interactable = false;
        }
    }
}
