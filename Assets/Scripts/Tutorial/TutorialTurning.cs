using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTurning : DialogueState
{

    enum DialogStages
    {
        BEFORE_EXCERCISE_STARTS,
        EXCERCISE_STARTS,
        EXCERCISE_ENDS
    };

    private int totalSentences;
    private TutorialTurning.DialogStages stage;
    private bool flag1 = false;
    private bool flag2 = false;


    public TutorialTurning(Tutorial tutorial, Dialogue dialogue) 
        : base(tutorial, dialogue, "TutorialTurningDialogue")
    {

    }

    public override void OnStateEnter()
    {
        
        this.totalSentences = dialogue.sentences.Length;
        Button btn = tutorial.continueButton.GetComponent<Button>();
        btn.interactable = true;
        tutorial.dialogueManager.StartDialog(dialogue);

        this.stage = TutorialTurning.DialogStages.BEFORE_EXCERCISE_STARTS;
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
        if (this.stage == TutorialTurning.DialogStages.BEFORE_EXCERCISE_STARTS)
        {
            int sentence = tutorial.dialogueManager.currentSentence();
            if (sentence == 4)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
                this.stage = TutorialTurning.DialogStages.EXCERCISE_STARTS;
            }
        }

        if (this.stage == TutorialTurning.DialogStages.EXCERCISE_STARTS)
        {
            string text = ((MorseCodeController)tutorial.morseCodeMachine.GetComponent<MorseCodeController>()).decodedText.text;

            int sentence = tutorial.dialogueManager.currentSentence();
            int index = text.Length > 0 ? text.Length - 1 : 0;
            

            if (sentence == 4)
            {
                if (text == "")
                {
                    return;
                }
                else {
                    if (text[index] == 'A' && !flag1)
                    {
                        Button btn = tutorial.continueButton.GetComponent<Button>();
                        btn.interactable = true;
                        btn.onClick.Invoke();
                        flag1 = true;
                    }
                }
                
            }
            if ( sentence == 8)
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;

                if (text == "")
                {
                    return;
                }
                
                if (text[index] == 'P' && !flag2)
                {
                    //Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = true;
                    btn.onClick.Invoke();
                    flag1 = true;

                    //this.stage = FirstExcercise.DialogStages.EXCERCISE_ENDS;
                }
            }

            if ( sentence == 11)
            {
                
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;

                if (text == "")
                {
                    return;
                }
                
                if (text[index] == 'S' && !flag2)
                {
                    //Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = true;
                    btn.onClick.Invoke();
                    flag1 = true;

                    //this.stage = FirstExcercise.DialogStages.EXCERCISE_ENDS;
                }
            }

            if ( sentence == 14)
            {
                
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;

                if (text == "")
                {
                    return;
                }
                
                if (text[index] == 'D' && !flag2)
                {
                    btn.onClick.Invoke();
                    flag2 = true;

                    this.stage = TutorialTurning.DialogStages.EXCERCISE_ENDS;
                }
            }

        }

        if (this.stage == TutorialTurning.DialogStages.EXCERCISE_ENDS)
        {
            Button btn = tutorial.continueButton.GetComponent<Button>();
            btn.interactable = true;
        }
       
    }
}
