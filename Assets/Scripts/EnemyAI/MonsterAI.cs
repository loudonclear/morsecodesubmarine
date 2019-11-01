using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed ;
    public float radioOfVision ;
    private AIState currentState;
    GameObject submarine = null;

    public float wanderTime;
    private float wanderTimer = 0;

    public float visionWeigth;
    public float distanceWeigth;
    public float noiseWeigth;

    public float maximumChance;
    public float maximumChancePercentage ;
    private float currentNoise = 0;

    public List<Vector3> spawnPositions;

    public float minAngle;
    public float maxAngle;

    // Start is called before the first frame update
    void Start()
    {
        
        submarine = GameObject.Find("mysubmarine");

      //  Debug.DrawLine(transform.position, submarine.transform.position, Color.red, 2.5f);

        setState(new SpawnState(this));

        //int lowerCount = 0;
        //int middleCount = 0;
        //int higherCount = 0;

        /*for (int i = 0; i < 10000; i++)
        {
            float number = UnityEngine.Random.Range(0.0f, 1.0f);
            if (number < 0.025)
            {
                lowerCount++;
            }
            else if (number >= 0.475 && number < 0.525)
            {
                middleCount++;
            }
            else if (number >= 0.9975)
            {
                higherCount++;
            }
            
        }*/

        //Debug.Log("higherCount " + lowerCount);
        //Debug.Log("middleCount" + middleCount);
        //Debug.Log("higherCount" + higherCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (submarine != null)
        {
           // Debug.DrawLine(transform.position, submarine.transform.position, Color.red, 2.5f);
        }
        

        if (Input.GetKeyDown("n"))
        {
            currentNoise++;
        }
        if (Input.GetKeyDown("m"))
        {
            if (currentNoise > 0)
            {
                currentNoise--;
            }
        }


        if (currentState != null)
        {
          currentState.Tick();
        }

    
    }

    public bool chanceToDetectPlayer()
    {

        Debug.Log("chanceToDetectPlayer");
        if (submarine != null)
        {
            /*float chanceOfDistance = ((1 / distanceFromPlayer() ) * this.distanceWeigth) / this.maximumChance;
            float chanceOfVision = (this.radioOfVision * this.visionWeigth) / this.maximumChance;
            float chanceOfNoise = (this.currentNoise * this.noiseWeigth) / this.maximumChance;*/

            float chanceOfDistance = this.distanceWeigth;
            float chanceOfVision =  this.visionWeigth;
            float chanceOfNoise = this.noiseWeigth;

            float totalChance = chanceOfDistance * chanceOfVision * chanceOfNoise;
            float randomNumber = UnityEngine.Random.Range(0.0f, 1.0f);

            //Debug.Log("randomNumber: "+ randomNumber+", totalChance: " + totalChance);

            if (totalChance < randomNumber  )
            {
              //  Debug.Log( "EVENT TRIGGERS");
                return true;
            }

            return false;
        }

        return false;
    }

    public void setState(AIState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "Monster - " + state.GetType().Name;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }

    }


    public void moveTo(Vector3 direction)
    {
        //var direction = getDirection(destination);
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    public void moveTowards(Vector3 destination)
    {
        var direction = getDirection(destination);
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    private Vector3 getDirection(Vector3 destination)
    {
        return (destination - transform.position).normalized;
    }

    public bool playerInRangeOfVision()
    {
        if (submarine != null) {
           
            if ( distanceFromPlayer() < Mathf.Pow(radioOfVision, 2))
            {
                return true;
            }
        }
        
        return false;
    }

    private float distanceFromPlayer()
    {
        Vector3 center = transform.position;
        return Mathf.Pow(submarine.transform.position.x - center.x, 2)
                + Mathf.Pow(submarine.transform.position.y - center.y, 2);
    }

    public List<Vector3> getSpawnPositions()
    {
        return spawnPositions;
    }
}
