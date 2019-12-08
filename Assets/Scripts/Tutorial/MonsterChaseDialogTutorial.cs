using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterChaseDialogTutorial : DialogueState
{
    private float chaseTime = 17;
    private float chaseTimer = 0;
    private bool chaseTimeFinish = false;

    public MonsterChaseDialogTutorial(Tutorial tutorial, Dialogue dialogue) : base(tutorial, dialogue, "MonsterChaseDialogTutorial")
    {

    }

    public override void OnStateEnter()
    {
        Button btn = tutorial.continueButton.GetComponent<Button>();
        btn.interactable = false;
        tutorial.dialogueManager.StartDialog(dialogue);
        tutorial.HideMonsterEye();
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
        chaseTimer += Time.deltaTime;
        if (chaseTimer > chaseTime && !chaseTimeFinish)
        {
            Button btn = tutorial.continueButton.GetComponent<Button>();
            btn.interactable = true;
            btn.onClick.Invoke();
            chaseTimeFinish = true;
            tutorial.UnSetMonsterInRadar();
            tutorial.SetMonsterCanAttack(false);
        }
    }
}
