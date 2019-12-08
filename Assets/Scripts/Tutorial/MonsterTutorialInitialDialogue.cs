using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterTutorialInitialDialogue : DialogueState
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

    public MonsterTutorialInitialDialogue(Tutorial tutorial, Dialogue dialogue) : base(tutorial, dialogue, "MonsterInitDialogue")
    {

    }

    public override void OnStateEnter()
    {
        monsterClipSounded = false;
        currentStage = DialogStages.BEFORE_MONSTER_ATTACKS;
        tutorial.dialogueManager.StartDialog(dialogue);
        tutorial.monster.GetComponent<Collider>().enabled = true;
    }

    public override void OnStateExit()
    {
    }

    public override void Tick()
    {
        if (currentStage == DialogStages.BEFORE_MONSTER_ATTACKS)
        {
            int sentence = tutorial.dialogueManager.currentSentence();
            if (sentence == 3 && !tutorial.getShakeGUI().isShaking())
            {
                tutorial.PlayExplosionSound();
                tutorial.getShakeGUI().Shake();
                tutorial.submarine.velocity = Vector3.zero;
                tutorial.submarine.gameObject.GetComponent<SubmarineMovement>().EnginesOff();
                
                currentStage = DialogStages.GUI_SHAKING;
                Button btn = tutorial.continueButton.GetComponent<Button>();
                btn.interactable = false;
            }


        }
        else if (currentStage == DialogStages.GUI_SHAKING)
        {
            if (!tutorial.getShakeGUI().isShaking())
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
                    tutorial.PlayMonsterSound();
                    tutorial.SetMonsterInRadar();
                    monsterClipSounded = true;
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = false;
                }
                else if(!tutorial.PlayMonsterSoundIsPlaying())
                {
                    tutorial.ShowMonsterEye();
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = true;
                }
            }
            if (sentence == 7)
            {
                Rigidbody submarineRigidBody = tutorial.submarine;
                
                if (submarineRigidBody.velocity.magnitude < 0.9)
                {
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.interactable = false;
                    tutorial.SetMonsterCanAttack(true);
                }
                else
                {
                    Button btn = tutorial.continueButton.GetComponent<Button>();
                    btn.onClick.Invoke();
                    
                    int currentIndex = tutorial.indexCurrentDialog + 4;
                    Dialogue nextDialog = tutorial.tutorialDialogs[++currentIndex];
                    tutorial.setState(new MonsterChaseDialogTutorial(tutorial, nextDialog));  
                }

            }

        }
    }
}
