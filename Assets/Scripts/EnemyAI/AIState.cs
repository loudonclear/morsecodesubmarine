using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState 
{
    protected MonsterAI monster;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public AIState(MonsterAI character)
    {
        this.monster = character;
    }
}
