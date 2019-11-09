using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandState : AIState
{
    private Vector3 lastDirection;
    private float currentMoveSpeed;

    public StandState(MonsterAI monster, Vector3 lastDirection) : base(monster)
    {
        this.lastDirection = lastDirection;
    }

    public override void OnStateEnter() {

        currentMoveSpeed = monster.moveSpeed;
        monster.moveSpeed = 0;
    }

    public override void OnStateExit() {
        monster.moveSpeed = currentMoveSpeed;
    }

    public override void Tick()
    {
        
    }
}
