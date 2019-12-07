﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogueManager dialogueManager;
    public Button continueButton;
    public Button starButton;
    public Button Logbook;
    public GameObject falseLogbook;
    public GameObject dialogPanel;
    public Dialogue[] tutorialDialogs;
    protected Dialogue currentDialog;
    public int indexCurrentDialog;
    public GameObject morseCodeMachine;
    //protected GameObject monster;
    //protected bool monsterCanAttack;

    protected DialogueState currentState;

    void Start()
    {

        Button btn = starButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        Button btn1 = Logbook.GetComponent<Button>();
        Logbook.interactable = false;

        ((GameObject)falseLogbook).SetActive(false);
        


        indexCurrentDialog = 0;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //Debug.Log("Tutorial::Update()");
        if (currentState != null)
        {
            
            currentState.Tick();
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

    void TaskOnClick()
    {
        Button btn = starButton.GetComponent<Button>();
        btn.transform.position = new Vector3(2000, 2000, 0);
        currentDialog  = tutorialDialogs[indexCurrentDialog];
        setState(new InitialDialogue(this, currentDialog));
        
    }
}
