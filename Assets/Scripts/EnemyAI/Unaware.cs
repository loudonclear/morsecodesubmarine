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

    
    private float wanderTime = 5.0f;
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

    public UnAware(MonsterAI monster, Vector3 nextDirection) : base(monster)
    {
        this.wanderDirection = nextDirection;
    }

    public override void OnStateEnter()
    {
        this.nextDestination = this.wanderDirection;
        submarine = GameObject.Find("mysubmarine");
    }

    public override void Tick()
    {
        
        monster.moveOnDirection(this.wanderDirection);

        if (submarine != null)
        {
            float randomNumber = Random.Range(0.0f, 20.0f);

            if (1 <= monster.checkPlayerInRangeOfVision() * monster.currentNoise)
            {
                // monster.setState(new ChasePlayer(monster,this.timer,this.currentDirection));
                monster.setState(new ChasePlayer(monster, this.nextDestination));
            }
        }

        if (!monster.getIsVisible())
        {
            
            // monster.setState(new SelectDirectionState(monster));
            
        }



        //this.timer += Time.deltaTime;
        //this.randomAwarenessTimer += Time.deltaTime;


        /*if (this.timer >= this.wanderTime)
        {
            Debug.Log("change destination");
            selectDestination();
            this.timer = 0;
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
        monster.setState(new SelectDirectionState(monster));
    }

    private bool ReachedDestination()
    {
        return Vector3.Distance(monster.transform.position, nextDestination) < 0.5f;
    }
    
}
