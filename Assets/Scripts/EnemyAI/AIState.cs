using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState 
{
    protected GameObject character;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public AIState(GameObject character)
    {
        this.character = character;
    }
}
