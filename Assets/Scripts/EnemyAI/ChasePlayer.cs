using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : AIState
{
    GameObject submarine = null;
    float lastTimer;
    int lastDirection;

    public ChasePlayer(MonsterAI monster,float lastTimer, int lastDirection) : base(monster)
    {
        this.lastTimer = lastTimer;
        this.lastDirection = lastDirection;
    }

    public override void OnStateEnter()
    {
        submarine = GameObject.Find("mysubmarine");
    }

    public override void Tick()
    {

        if (submarine != null)
        {
            monster.moveTowards(submarine.transform.position);
        }
        //throw new System.NotImplementedException();
        if (!monster.playerInRangeOfVision())
        {
            monster.setState(new UnAware(monster,0, this.lastDirection));
        }

    }

    
}
