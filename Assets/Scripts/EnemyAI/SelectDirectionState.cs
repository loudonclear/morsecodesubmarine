using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SelectDirectionState : AIState
{

    private int startPosition = -1;
    private int endPosition = -1;

    public SelectDirectionState(MonsterAI monster):base(monster)
    {

    }

    public override void OnStateEnter()
    {
        CenterObject center = (CenterObject) GameObject.Find("centerObject").GetComponent<CenterObject>();

        if (center != null)
        {
            Debug.Log("HERE");
            int numPossiblePositions = monster.getSpawnPositions().Count;
          
            Vector3 startSpawnPosition = monster.getSpawnPositions().
                ElementAt((int)UnityEngine.Random.Range(0.0f, numPossiblePositions));
              

            monster.transform.position = startSpawnPosition;


            Vector3 directionToCenter = center.transform.position - monster.transform.position;
            //directionToCenter;

            //float directionVectorAngle =  (float)Math.Atan(directionToCenter.y  / directionToCenter.x  );

            float minAngle = monster.minAngle * (float)Math.PI / 180;
            float maxAngle = monster.maxAngle * (float)Math.PI / 180;


            float movementAngle = UnityEngine.Random.Range(  minAngle, maxAngle);

            //directionVectorAngle += movementAngle;

            Vector3 newDirection = new Vector3(
                (float)Math.Cos(movementAngle) * directionToCenter.x - (float)Math.Sin(movementAngle) * directionToCenter.y,
                (float)Math.Sin(movementAngle) * directionToCenter.x + (float)Math.Cos(movementAngle) * directionToCenter.y,
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

