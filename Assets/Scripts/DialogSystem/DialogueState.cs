using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueState
{

    protected Tutorial tutorial;
    protected Dialogue dialogue;
    protected string stateName;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public string StateName() { return stateName; }

    public DialogueState(Tutorial tutorial, Dialogue dialogue,string name)
    {
        this.tutorial = tutorial;
        this.stateName = name;
        this.dialogue = dialogue;
    }
}
