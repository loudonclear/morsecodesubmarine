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

    bool isWanderDirectionSet = false;
    private float outsideTimer = 0;
    private float outsideTime = 3;
    
    public UnAware(MonsterAI monster) : base(monster, "UnAware")
    {
        
    }

    public UnAware(MonsterAI monster,float lastTimer, int lastDirection) : base(monster, "UnAware")
    {
        this.timer = lastTimer;
        this.currentDirection = lastDirection;
    }

    public UnAware(MonsterAI monster, Vector3 nextDirection) : base(monster, "UnAware")
    {
        this.wanderDirection = nextDirection;
    }

    public override void OnStateEnter()
    {
        this.nextDestination = this.wanderDirection;
        submarine = GameObject.Find("mysubmarine");
        isWanderDirectionSet = false;
    }

    public override void Tick()
    {

        if (monster.onExternalCircleSight())
        {
            if (!monster.GetWandering())
            {
                monster.moveTowards(submarine.transform.position);
            }
            else
            {
                outsideTimer+= Time.deltaTime;
                if (outsideTimer < outsideTime)
                {
                    monster.moveOnDirection(this.wanderDirection);
                }
                else {
                    monster.SetWandering(false);
                }
            }
        }
        else if (monster.onInnerCircleSight())
        {
            if (!isWanderDirectionSet)
            {

                Vector3 directionToCenter = submarine.transform.position 
                    - monster.transform.position;


                float minAngle = monster.minAngle;
                float maxAngle = monster.maxAngle;


                float movementAngle = UnityEngine.Random.Range(minAngle, maxAngle);

                Vector3 newDirection = Quaternion.Euler(0, movementAngle, 0) * directionToCenter;

                Vector3 normalizedNewDirection = newDirection.normalized;


                this.wanderDirection = normalizedNewDirection;
                isWanderDirectionSet = true;
            }
            else {
                monster.SetWandering(true);
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
            }
            
        }

        

        /*if (!monster.getIInSight())
        {
            
            // monster.setState(new SelectDirectionState(monster));
            
        }*/



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
