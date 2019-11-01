using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SpawnState : AIState
{
    

    public SpawnState(MonsterAI monster):base(monster)
    {

    }

    public override void OnStateEnter()
    {
        CenterObject center = (CenterObject) GameObject.Find("centerObject").GetComponent<CenterObject>();

        if (center != null)
        {
            Debug.Log("HERE");
            int numPossiblePositions = monster.getSpawnPositions().Count;
            Vector3 spawnPosition = monster.getSpawnPositions().
                ElementAt((int)UnityEngine.Random.Range(0.0f, numPossiblePositions));

            monster.transform.Translate(spawnPosition);


            Vector3 directionToCenter = center.transform.position - monster.transform.position;

            float direcitonVector = (float)Math.Atan(center.transform.position.y - monster.transform.position.y / center.transform.position.x - monster.transform.position.x);

            float minAngle = monster.minAngle * (float)Math.PI / 180;
            float maxAngle = monster.maxAngle * (float)Math.PI / 180;


            float movementAngle = UnityEngine.Random.Range(direcitonVector - minAngle, direcitonVector +maxAngle);

            direcitonVector += movementAngle;

            Vector3 newDirection = new Vector3(
                (float)Math.Cos( movementAngle) * directionToCenter.x - (float)Math.Sin( movementAngle ) * directionToCenter.y,
                (float)Math.Sin( movementAngle ) * directionToCenter.x + (float)Math.Cos( movementAngle ) * directionToCenter.y,
                0);

            newDirection = newDirection.normalized;

            monster.setState(new UnAware(monster, newDirection));
            
        }

        
    }
    
    public override void Tick()
    {
        //throw new NotImplementedException();
    }
}

