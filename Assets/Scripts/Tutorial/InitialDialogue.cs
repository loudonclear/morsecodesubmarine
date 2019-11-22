using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialogue : DialogueState
{
    

    public InitialDialogue(Tutorial tutorial, Dialogue dialogue) 
        : base(tutorial, dialogue, "Tutorial1InitDialog")
    {
        
    }


    public override void OnStateEnter()
    {
        tutorial.dialogueManager.StartDialog(dialogue);
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
       
    }

}
