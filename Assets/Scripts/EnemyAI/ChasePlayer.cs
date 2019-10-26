using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : AIState
{
    GameObject submarine = null;

    public ChasePlayer(Monster monster) : base(monster)
    {

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
    }

    
}
