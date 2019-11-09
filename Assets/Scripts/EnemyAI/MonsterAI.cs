﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed ;
    public float chaseSpeed;

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
    public float currentNoise;

    public List<Vector3> spawnPositions;

    public float minAngle;
    public float maxAngle;

    private bool isVisible;

    [SerializeField]
    public Text collisionText;

    private Vector3 currentDirection;

    [SerializeField]
    private LineRenderer lineOrientation;
    private Vector3 orientationStart;
    private Vector3 orientationEnd;

    private float vision = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        submarine = GameObject.Find("mysubmarine");

        lineOrientation.positionCount = 2;
        orientationStart = transform.position;
        orientationEnd = orientationStart;
        vision = 0;
        chaseSpeed = 1.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineOrientation != null)
        {
            
              orientationStart = transform.position;
              lineOrientation.SetPosition(0, orientationStart);
              lineOrientation.SetPosition(1, orientationEnd);
            
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


    public void moveOnDirection(Vector3 direction)
    {
        //var direction = getDirection(destination);
        this.currentDirection = direction;
        transform.Translate(this.currentDirection * Time.deltaTime * moveSpeed, Space.World);
    }

    public void moveTowards(Vector3 destination)
    {
        var direction = getDirection(destination);

        transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
    }

    private Vector3 getDirection(Vector3 destination)
    {
        return (destination - transform.position).normalized;
    }

    public float checkPlayerInRangeOfVision()
    {
        this.vision = 0;
        if (submarine != null) {

            float distance = distanceFromPlayer();
            //Debug.Log("radioOfVision: "+ radioOfVision + "Distance from player: " + distance);
            if (distance < Mathf.Pow(2, 2))
            {
                //this.vision = 1 - (distance / radioOfVision);
                return 1;
            }

        }
        
        return this.vision;
    }

    public bool playerInRangeOfVision()
    {
        if (submarine != null)
        {

            float distance = distanceFromPlayer();
            //Debug.Log("radioOfVision: " + radioOfVision + "Distance from player: " + distance);
            if (distance < Mathf.Pow(2, 2))
            {
                return true;
            }

        }

        return false;
    }


    private float distanceFromPlayer()
    {
        return Vector3.Distance(transform.position, submarine.transform.position);  
    }

    public List<Vector3> getSpawnPositions()
    {
        return spawnPositions;
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }

    public bool getIsVisible()
    {
        return isVisible;
        //return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionText != null)
        {
            
            if (other.gameObject.name == "mysubmarine")
            {
                setState(new CollisionMonsterState(this,this.currentDirection));
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collisionText != null)
        {
            collisionText.text = "";
        }
    }

    public void setOrientationEndPos(Vector3 endPos)
    {
        this.orientationEnd = endPos;
    }
}
