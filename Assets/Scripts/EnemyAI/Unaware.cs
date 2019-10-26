using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnAware : AIState
{
    private Vector3 direction = new Vector3(1, 0, 0);
    private int currentDirection = 1;
    private Vector3 nextDestination;
    private float timer = 0f;
    private float wanderTime = 5f;
    // This returns the GameObject named Hand.
    GameObject submarine = null;

    public UnAware(MonsterAI monster) : base(monster)
    {

    }

    public UnAware(MonsterAI monster,float lastTimer, int lastDirection) : base(monster)
    {
        this.timer = lastTimer;
        this.currentDirection = lastDirection;
    }
    
    public override void OnStateEnter()
    {
        this.nextDestination = direction * currentDirection;
        submarine = GameObject.Find("mysubmarine");
    }

    public override void Tick()
    {
        
        monster.moveTo(nextDestination);

        if (submarine != null)
        {
            if(monster.playerInRangeOfVision(submarine))
            {
                monster.setState(new ChasePlayer(monster,this.timer,this.currentDirection));
            }
        }



        this.timer += Time.deltaTime;
        if (this.timer >= wanderTime)
        {
            selectDestination();
            timer = 0;
        }
    }

    private void selectDestination()
    {
        this.currentDirection *= -1;
        this.nextDestination = this.direction * this.currentDirection;
    }
}
