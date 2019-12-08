using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum MonsterTutorialEvent
{
    NONE,
    SHAKE,
    SCREEAM
};

public class Tutorial : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Button continueButton;
    public Button startButton;
    public Button Logbook;
    public GameObject LogbookPage;
    public GameObject dialogPanel;
    public Text shipName;
    public Rigidbody submarine;
    public Dialogue[] tutorialDialogs;
    protected Dialogue currentDialog;

    [HideInInspector]
    public int indexCurrentDialog;

    public SubmarineRadar submarineRadar;
    public GameObject morseCodeMachine;
    public GameObject monster;
    public AudioSource monsterSoundClip;
    public AudioSource explosionSoundClip;
    public Image monsterEye;

    private ShakeGUI shakePanel;
    private bool monsterCanAttack;
    private bool monsterAttacked;
    private int numCollisionsWithMonster;
    private MonsterTutorialEvent nextMonsterTutorialEvent;

    protected DialogueState currentState;

    void Start()
    {
        indexCurrentDialog = 0;
        morseCodeMachine.GetComponent<CommandInterpreter>().commandListen = false;
        shakePanel = GameObject.FindGameObjectWithTag("SubControlPanel").GetComponent<ShakeGUI>();
        monster.GetComponent<Collider>().enabled = false;

        currentDialog = tutorialDialogs[indexCurrentDialog];
        setState(new InitialDialogue(this, currentDialog));
    }

    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }

        if (monsterCanAttack)
        {
            if (!monsterAttacked)
            {
                if (monster.GetComponent<MonsterAI>().getCurrentStateName() == "CollisionMonsterState")
                {

                    if (numCollisionsWithMonster % 3 == 0)
                    {
                        PlayMonsterSound();
                        nextMonsterTutorialEvent = MonsterTutorialEvent.SHAKE;
                    }
                    else
                    {
                        PlayExplosionSound();
                        this.shakePanel.Shake();
                        nextMonsterTutorialEvent = MonsterTutorialEvent.SCREEAM;

                    }
                    numCollisionsWithMonster++;
                    monsterAttacked = true;
                }

            }
            else if (monsterAttacked && monster.GetComponent<MonsterAI>().getCurrentStateName() == "ChasePlayer"
                || monsterAttacked && monster.GetComponent<MonsterAI>().getCurrentStateName() == "UnAware")
            {
                monsterAttacked = false;
            }
        }
    }

    public void setState(DialogueState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void PlayMonsterSound()
    {
        if (monsterSoundClip != null)
        {
            monsterSoundClip.Play();
        }
    }

    public bool PlayMonsterSoundIsPlaying()
    {
        if (monsterSoundClip != null)
        {
            return monsterSoundClip.isPlaying;
        }
        return false;
    }

    public void PlayExplosionSound()
    {
        if (explosionSoundClip != null)
        {
            explosionSoundClip.Play();
        }
    }

    public void ShowMonsterEye()
    {
        if (monsterEye != null)
        {
            monsterEye.enabled = true;
        }
    }

    public void HideMonsterEye()
    {
        if (monsterEye != null)
        {
            monsterEye.enabled = false;
        }
    }

    public GameObject getSubmarine()
    {
        return submarine.gameObject;
    }

    public GameObject getMonster()
    {
        return monster;
    }

    public ShakeGUI getShakeGUI()
    {
        return this.shakePanel;
    }

    public void SetMonsterCanAttack(bool attack)
    {
        this.monsterCanAttack = attack;
    }

    public void SetMonsterInRadar()
    {
        this.submarineRadar.SetMonsterRadarSize();
    }

    public void UnSetMonsterInRadar()
    {
        this.submarineRadar.UnSetMonsterRadarSize();
    }

}
