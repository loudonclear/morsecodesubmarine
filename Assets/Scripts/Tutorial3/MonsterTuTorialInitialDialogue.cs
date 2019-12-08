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
    
    private bool monsterClipSounded;

    public MonsterTuTorialInitialDialogue(Tutorial3 tutorial, Dialogue dialogue) : base(tutorial, dialogue, "MonsterInitDialogue")
    {

    }

    public override void OnStateEnter()
    {
        
        monsterClipSounded = false;
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
            if (sentence == 3 && !((Tutorial3)tutorial).getShakeGUI().isShaking())
            {
                ((Tutorial3)tutorial).PlayExplosionSound();
                ((Tutorial3)tutorial).getShakeGUI().Shake();
                
                currentStage = DialogStages.GUI_SHAKING;
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
            }


        }
        else if (currentStage == DialogStages.GUI_SHAKING)
        {
            if (!((Tutorial3)tutorial).getShakeGUI().isShaking())
            {
             
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = true;
                currentStage = DialogStages.MONSTER_ATTACKS;
            }
        }
        else if (currentStage == DialogStages.MONSTER_ATTACKS)
        {
            int sentence = tutorial.dialogueManager.currentSentence();
            if (sentence == 5)
            {
                if (!monsterClipSounded)
                {
                    ((Tutorial3)tutorial).PlayMonsterSound();
                    ((Tutorial3)tutorial).SetMonsterInRadar();
                    monsterClipSounded = true;
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = false;
                }
                else if(!((Tutorial3)tutorial).PlayMonsterSoundIsPlaying())
                {
                    ((Tutorial3)tutorial).ShowMonsterEye();
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = true;
                }
            }
            if (sentence == 7)
            {
                Rigidbody submarineRigidBody = ((Tutorial3)tutorial).getSubmarine().GetComponent<Rigidbody>();
                
                if (submarineRigidBody.velocity.magnitude < 0.9)
                {
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = false;
                    ((Tutorial3)tutorial).SetMonsterCanAttack(true);
                }
                else
                {
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.onClick.Invoke();
                    
                    int currentIndex = tutorial.indexCurrentDialog;
                    Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
                    tutorial.setState(new MonsterChaseDialogTutorial(tutorial, nextDialog));
                    
                }

               
            }


            
        }
    }
}
