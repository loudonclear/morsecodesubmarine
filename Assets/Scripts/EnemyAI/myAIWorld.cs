﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myAIWorld : MonoBehaviour
{
    enum worldState
    {
        notSpawn,
        running, 
        Spawned
    }

    GameObject monster;
    MonsterAI monsterAi;
    GameObject submarine;

    public float spawnTime;
    private float spawnTimer = 0.0f;
    private Vector3 farAwayPosition = new Vector3(1000,0 , 1000);

    private worldState currentWorldState;
    private float monsterSpeed;

    public float fastCruisingSpeed;
    public float fastChaseSpeed;
    public float slowCruisingSpeed;
    public float slowChaseSpeed;

    public float highVision;
    public float lowVision;

    public Text monsterTypeText;
    public float monsterProbability;
    public float fastDumbmonsterProbability;
    public float fastSmartmonsterProbability;
    public float slowDumbmonsterProbability;
    public float slowFastermonsterProbability;

    public Vector3 topLeft;
    public float width;
    public float height;
    
    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
        monsterAi = (MonsterAI)monster.GetComponent<MonsterAI>();

        monster.transform.position = farAwayPosition;
        monsterAi.setState(new StandState(monsterAi, Vector3.zero));

        submarine = GameObject.Find("Submarine");

        currentWorldState = worldState.notSpawn;
        monsterSpeed = 1;
    }

    void Update()
    {
        if (currentWorldState == worldState.notSpawn)
        {
            if (!monsterAi.getIInSight())
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnTime)
                {

                    float generateMonsterProbability = Random.Range(0.0f, 1.0f);
                    generateMonsterProbability = 0.19f;

                    if (generateMonsterProbability < monsterProbability)
                    {
                        if (generateMonsterProbability <= fastDumbmonsterProbability)
                        {
                            monsterAi.moveSpeed = fastCruisingSpeed;
                            monsterAi.chaseSpeed = fastChaseSpeed;
                            monsterAi.radioOfVision = lowVision;
                            if(monsterTypeText!= null)
                              monsterTypeText.text = "Fast/Dumb ";
                        }
                        else if (generateMonsterProbability > fastDumbmonsterProbability && generateMonsterProbability <= fastSmartmonsterProbability)
                        {
                            monsterAi.moveSpeed = fastCruisingSpeed;
                            monsterAi.chaseSpeed = fastChaseSpeed;
                            monsterAi.radioOfVision = highVision;
                            if (monsterTypeText != null)
                                monsterTypeText.text = "Fast/Smart";
                        }
                        else if (generateMonsterProbability > fastSmartmonsterProbability && generateMonsterProbability <= slowDumbmonsterProbability)
                        {
                            monsterAi.moveSpeed = slowCruisingSpeed;
                            monsterAi.chaseSpeed = slowChaseSpeed;
                            monsterAi.radioOfVision = lowVision;
                            if (monsterTypeText != null)
                                monsterTypeText.text = "Slow/Dumb";

                        }
                        else if (generateMonsterProbability > slowDumbmonsterProbability && generateMonsterProbability <= slowFastermonsterProbability)
                        {
                            monsterAi.moveSpeed = slowCruisingSpeed;
                            monsterAi.chaseSpeed = slowChaseSpeed;
                            monsterAi.radioOfVision = highVision;
                            if (monsterTypeText != null)
                                monsterTypeText.text = "Slow/Smart";
                        }
                        spawnTimer = 0;
                        monsterAi.moveSpeed = monsterSpeed;
                        monsterAi.chaseSpeed = monsterSpeed;
                        monsterAi.setState(new SelectDirectionState(monsterAi));
                        currentWorldState = worldState.Spawned;
                    }
                    else {
                        spawnTimer = 0;
                    }
    
                }

            }

        }
        else if (currentWorldState == worldState.Spawned)
        {
            if (monsterAi.GetWandering())
            {
                monsterSpeed = monsterAi.moveSpeed;
                currentWorldState = worldState.running;
            }
        }
        else if (currentWorldState == worldState.running)
        {
            if (!monsterAi.GetWandering())
            {
                monsterAi.moveSpeed = 0;
                currentWorldState = worldState.notSpawn;
            }
        }
        
    }
}
