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

    private submarine mySubmarine;

    public SelectDirectionState(MonsterAI monster):base(monster, "SelectDirectionState")
    {

    }

    public override void OnStateEnter()
    {
        mySubmarine = (submarine)GameObject.Find("mysubmarine").GetComponent<submarine>();
        CenterObject center = (CenterObject) GameObject.Find("centerObject").GetComponent<CenterObject>();
        Debug.Log("CENTER POSITION "+center.transform.position);

        if (mySubmarine != null)
        {
           
            float internalRadious = mySubmarine.internalRadius;
            float externalRadious = mySubmarine.externalRadius;

            float randomRadious = UnityEngine.Random.Range(internalRadious, externalRadious);
            float randomAngle = UnityEngine.Random.Range(0, 360);

            Vector3 randomPos = new Vector3(
                mySubmarine.transform.position.x + randomRadious * (float)Math.Cos(Math.PI / 180.0) ,
                0,
                mySubmarine.transform.position.z + randomRadious * (float)Math.Sin(Math.PI / 180.0));


            monster.transform.position = randomPos;

            monster.setState(new UnAware(monster, Vector3.zero));
            
        }

        
    }
    
    public override void Tick()
    {
        //throw new NotImplementedException();
    }
}

