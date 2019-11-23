using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstExcercise : DialogueState
{

    enum DialogStages
    {
        BEFORE_EXCERCISE_STARTS,
        EXCERCISE_STARTS,
        EXCERCISE_ENDS
    };

    private int totalSentences;
    private FirstExcercise.DialogStages stage;
    private bool flag1 = false;


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

        this.stage = FirstExcercise.DialogStages.BEFORE_EXCERCISE_STARTS;
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
        if (this.stage == FirstExcercise.DialogStages.BEFORE_EXCERCISE_STARTS)
        {
            int sentence = tutorial.dialogueManager.currentSentence();
            if (sentence == 4)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
                this.stage = FirstExcercise.DialogStages.EXCERCISE_STARTS;
            }
        }

        if (this.stage == FirstExcercise.DialogStages.EXCERCISE_STARTS)
        {
            string text = ((MorseCodeController)tutorial.morseCodeMachine.GetComponent<MorseCodeController>()).decodedText.text;

            int sentence = tutorial.dialogueManager.currentSentence();
            if (text[text.Length-1] == 'A' && !flag1)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = true;
                btn.onClick.Invoke();
                flag1 = true;
            }
        }
       
    }
}
