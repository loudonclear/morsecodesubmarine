using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnAware : AIState
{
    private Vector3 direction = new Vector3(1, 0, 0);
    private Vector3 wanderDirection ;
    private int currentDirection = 1;
    private Vector3 nextDestination;
    private float timer = 0f;
    private float wanderTime = 5f;
    private float randomAwarenessTime = 5f;
    private float randomAwarenessTimer = 0f;
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

    public UnAware(MonsterAI monster, Vector3 lastDirection) : base(monster)
    {
        this.wanderDirection = lastDirection;
    }

    public override void OnStateEnter()
    {
        this.nextDestination = this.wanderDirection;
        submarine = GameObject.Find("mysubmarine");
    }

    public override void Tick()
    {
        
        monster.moveTo(nextDestination);

        if (submarine != null)
        {
            if(monster.playerInRangeOfVision())
            {
                monster.setState(new ChasePlayer(monster,this.timer,this.currentDirection));
            }
        }



        this.timer += Time.deltaTime;
        this.randomAwarenessTimer += Time.deltaTime;


        /*if (this.timer >= wanderTime)
        {
            selectDestination();
            timer = 0;
        }*/

        /*if (this.randomAwarenessTimer >= randomAwarenessTime)
        {
            if (monster.chanceToDetectPlayer())
            {
                monster.setState(new RandomChasePlayer(monster));
            }
            this.randomAwarenessTimer = 0;
        }*/
    }

    private void selectDestination()
    {
        this.currentDirection *= -1;
        this.nextDestination = this.direction * this.currentDirection;
    }
}
