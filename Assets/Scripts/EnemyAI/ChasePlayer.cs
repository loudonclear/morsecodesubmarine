using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : AIState
{
    GameObject submarine = null;
    float lastTimer;
    Vector3 lastDirection;
    float chasingSpeed;
    float moveSpeed;

    public ChasePlayer(MonsterAI monster, Vector3 lastDirection) : base(monster)
    {
        //this.lastTimer = lastTimer;
        this.lastDirection = lastDirection;
        chasingSpeed = monster.chaseSpeed;
        moveSpeed = monster.moveSpeed;
    }

    public override void OnStateEnter()
    {
        monster.moveSpeed = this.chasingSpeed;
        submarine = GameObject.Find("mysubmarine");
    }

    public override void Tick()
    {

        if (submarine != null)
        {
            
            monster.moveTowards(submarine.transform.position);
        }
        
        if (!monster.playerInRangeOfVision())
        {
            monster.moveSpeed = this.moveSpeed;
            monster.setState(new UnAware(monster, this.lastDirection));
        }

    }

    
}
