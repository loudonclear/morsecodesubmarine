using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class RandomChasePlayer : AIState
{
    GameObject submarine = null;

    public RandomChasePlayer(MonsterAI monster) : base(monster, "RandomChasePlayer")
    {

    }

    public override void OnStateEnter()
    {
     
        submarine = GameObject.Find("Submarine");
    }

    public override void Tick()
    {
        // throw new NotImplementedException();
        if (submarine != null)
        {
            monster.moveTowards(submarine.transform.position);
        }
    }
}

