using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterChaseDialogTutorial : DialogueState
{
    public MonsterChaseDialogTutorial(Tutorial tutorial, Dialogue dialogue) : base(tutorial, dialogue, "MonsterChaseDialogTutorial")
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

    public override void Tick()
    {
        
    }
}
