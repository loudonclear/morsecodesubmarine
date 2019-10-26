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

    public UnAware(Monster monster) : base(monster)
    {
    }

    public override void OnStateEnter()
    {
        nextDestination = direction * currentDirection;
        submarine = GameObject.Find("mysubmarine");
    }

    public override void Tick()
    {
        
        monster.moveTo(nextDestination);

        if (submarine != null)
        {
            if(monster.playerInRangeOfVision(submarine))
            {
                monster.setState(new ChasePlayer(monster));
            }
        }



        timer += Time.deltaTime;
        if (timer >= wanderTime)
        {
            selectDestination();
            timer = 0;
        }
    }

    private void selectDestination()
    {
        currentDirection *= -1;
        nextDestination = direction * currentDirection;
    }
}
