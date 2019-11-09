using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMonsterState : AIState
{

    private Vector3 lastDirection;
    private Vector3 normalizedReverseDirection;
    private float timer = 0f;
    private float backTime = 1.0f;
    private float monsterSpeed;

   public CollisionMonsterState(MonsterAI monster, Vector3 lastDirection) : base(monster, "CollisionMonsterState")
    {
        //this.lastTimer = lastTimer;
        this.lastDirection = lastDirection;
    }

    public override void OnStateEnter()
    {
        //submarine = GameObject.Find("mysubmarine");
        //monster.moveSpeed = 0;
        Vector3 directionVector = new Vector3(this.lastDirection.x, this.lastDirection.y, this.lastDirection.z);
        normalizedReverseDirection = directionVector.normalized * -1;
        this.monsterSpeed = monster.moveSpeed;
    }

    public override void Tick()
    {
        //throw new System.NotImplementedException();
        monster.moveOnDirection(normalizedReverseDirection);

        timer += Time.deltaTime;

        if (this.timer >= this.backTime)
        {

            monster.moveSpeed *= 0.5f;
            timer = 0;
            if (monster.moveSpeed < 0.1)
            {
                monster.moveSpeed = this.monsterSpeed;
                monster.setState(new UnAware(monster,this.lastDirection));
            }
        }
        

    }

    

   
}
