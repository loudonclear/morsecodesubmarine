using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : Tutorial
{
    // Start is called before the first frame update

    public GameObject monsterSound;
    private AudioSource monsterSoundClip;
    public GameObject monsterEye;
    private GameObject submarine;
    private GameObject monster;
    private ShakeGUI shakePanel;
    private bool monsterCanAttack;
    private bool mosterAttacked;

    void Start()
    {
        monsterCanAttack = false;
        mosterAttacked = false;
        Button LogbookBtn = Logbook.GetComponent<Button>();
        LogbookBtn.interactable = false;
        indexCurrentDialog = 0;
        this.shakePanel = GameObject.Find("SubControlPanel").GetComponent<ShakeGUI>();
        if (this.shakePanel == null)
        {
            Debug.Log("error");
        }
        monsterSoundClip = monsterSound.GetComponent<AudioSource>();
        Button btn = starButton.GetComponent<Button>();
        btn.onClick.AddListener(InitTutorialOnClick);
        monsterEye.SetActive(false);
        submarine = GameObject.Find("Submarine");
        monster = GameObject.FindGameObjectWithTag("monster");
    }

    public override void Update()
    {
        
        if (monsterCanAttack)
        {
            if (!mosterAttacked)
            {
                Debug.Log("away");
                if (monster.GetComponent<MonsterAI>().getCurrentStateName() == "CollisionMonsterState")
                {
                    Debug.Log("attacked");
                    this.shakePanel.Shake();
                    mosterAttacked = true;
                }

            }
            else if (mosterAttacked && monster.GetComponent<MonsterAI>().getCurrentStateName() == "ChasePlayer"
                || mosterAttacked && monster.GetComponent<MonsterAI>().getCurrentStateName() == "UnAware")
            {
                mosterAttacked = false;
            }
        }

        base.Update();

    }

    void InitTutorialOnClick()
    {

        Button LogbookBtn = Logbook.GetComponent<Button>();
        LogbookBtn.interactable = true;
        Button btn = starButton.GetComponent<Button>();
        btn.transform.position = new Vector3(2000, 2000, 0);
        currentDialog = tutorialDialogs[indexCurrentDialog];
        setState(new MonsterTuTorialInitialDialogue(this, currentDialog));
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
           return  monsterSoundClip.isPlaying;
        }
        return false;
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

    public void SetmonsterAttack(bool attack)
    {
        this.monsterCanAttack = attack;
    }
}
