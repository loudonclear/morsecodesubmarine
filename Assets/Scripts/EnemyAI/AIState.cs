using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState 
{
    protected MonsterAI monster;
    protected string stateName;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public string StateName() { return stateName; }

    public AIState(MonsterAI character, string name)
    {
        this.monster = character;
        this.stateName = name;
    }
}
