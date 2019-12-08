using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : Tutorial
{
    enum MonsterTutorialEvent
    {
        NONE,
        SHAKE,
        SCREEAM
    };

    private AudioSource monsterSoundClip;
    private AudioSource explosionSoundClip;
    public GameObject monsterEye;
    private GameObject submarine;
    private GameObject monster;
    private ShakeGUI shakePanel;
    private bool monsterCanAttack;
    private bool monsterAttacked;
    private int numCollisionsWithMonster;
    private MonsterTutorialEvent nextMonsterTutorialEvent;
    private SubmarineRadar mySubmarineRadar;

    void Start()
    {
        numCollisionsWithMonster = 0;
        nextMonsterTutorialEvent = MonsterTutorialEvent.SHAKE;
        monsterCanAttack = false;
        monsterAttacked = false;
        Button LogbookBtn = Logbook.GetComponent<Button>();
        LogbookBtn.interactable = false;
        indexCurrentDialog = 0;
        this.shakePanel = GameObject.FindGameObjectWithTag("SubControlPanel").GetComponent<ShakeGUI>();

        this.mySubmarineRadar = GameObject.Find("Radar").GetComponent<SubmarineRadar>();
        monsterSoundClip = GameObject.Find("MonsterClip").GetComponent<AudioSource>();
        monsterEye.SetActive(false);
        submarine = GameObject.Find("Submarine");
        monster = GameObject.FindGameObjectWithTag("Monster");
        explosionSoundClip = GameObject.Find("CrashClip").GetComponent<AudioSource>();

    }

    public override void Update()
    {
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

        base.Update();
    }

    void InitTutorialOnClick()
    {
        Button LogbookBtn = Logbook.GetComponent<Button>();
        LogbookBtn.interactable = true;
        currentDialog = tutorialDialogs[indexCurrentDialog];
        setState(new MonsterTutorialInitialDialogue(this, currentDialog));
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
            monsterEye.SetActive(true);
        }
    }

    public void HideMonsterEye()
    {
        if (monsterEye != null)
        {
            monsterEye.SetActive(false);
        }
    }

    public GameObject getSubmarine()
    {
        return submarine;
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
        this.mySubmarineRadar.SetMonsterRadarSize();
    }

    public void UnSetMonsterInRadar()
    {
        this.mySubmarineRadar.UnSetMonsterRadarSize();
    }
}
