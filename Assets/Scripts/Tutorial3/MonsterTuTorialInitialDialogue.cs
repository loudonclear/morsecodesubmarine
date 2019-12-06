using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterTuTorialInitialDialogue : DialogueState
{
    enum DialogStages
    {
        BEFORE_MONSTER_ATTACKS,
        GUI_SHAKING,
        MONSTER_ATTACKS,
        AFTER_MONSTER_ATTACKS
    };

    DialogStages currentStage;
    ShakeGUI shakePanel;

    public MonsterTuTorialInitialDialogue(Tutorial3 tutorial, Dialogue dialogue) : base(tutorial, dialogue, "MonsterInitDialogue")
    {

    }

    public override void OnStateEnter()
    {
        this.shakePanel = GameObject.Find("SubControlPanel").GetComponent<ShakeGUI>();
        if (this.shakePanel == null)
        {
            Debug.Log("error");
        }
        currentStage = DialogStages.BEFORE_MONSTER_ATTACKS;
        tutorial.dialogueManager.StartDialog(dialogue);
    }

    public override void OnStateExit()
    {
        

    }

    public override void Tick()
    {
        if (currentStage == DialogStages.BEFORE_MONSTER_ATTACKS)
        {
            int sentence = tutorial.dialogueManager.currentSentence();
            if (sentence == 3 && !this.shakePanel.isShaking())
            {
                shakePanel.Shake();
                currentStage = DialogStages.GUI_SHAKING;
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
            }
            
            
        }
        else if (currentStage == DialogStages.GUI_SHAKING)
        {
            if (!this.shakePanel.isShaking())
            {
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = true;
            }
        }
    }
}
