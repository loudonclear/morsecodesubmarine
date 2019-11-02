using System.Collections;
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
    private Vector3 farAwayPosition = new Vector3(1000, 1000, 0);

    private worldState currentWorldState;
    private float monsterSpeed;

    public float fastCruisingSpeed;
    public float fastChaseSpeed;
    public float slowCruisingSpeed;
    public float slowChaseSpeed;

    public float highVision;
    public float lowVision;

    public Text monsterTypeText;

    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("monster");
        monsterAi = (MonsterAI)monster.GetComponent<MonsterAI>();

        monster.transform.Translate(farAwayPosition);
        monsterAi.setState(new StandState(monsterAi, Vector3.zero));

        submarine = GameObject.Find("mysubmarine");

        currentWorldState = worldState.notSpawn;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWorldState == worldState.notSpawn)
        {

            if (!monsterAi.getIsVisible())
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnTime)
                {

                    float generateMonsterProbability = Random.Range(0.0f, 1.0f);

                    if (generateMonsterProbability < 0.2)
                    {
                        if (generateMonsterProbability <= 0.05)
                        {
                            monsterAi.moveSpeed = fastCruisingSpeed;
                            monsterAi.chaseSpeed = fastChaseSpeed;
                            monsterAi.radioOfVision = lowVision;
                            monsterTypeText.text = "Fast/Dumb ";
                        }
                        else if (generateMonsterProbability > 0.05 && generateMonsterProbability <= 0.10)
                        {
                            monsterAi.moveSpeed = fastCruisingSpeed;
                            monsterAi.chaseSpeed = fastChaseSpeed;
                            monsterAi.radioOfVision = highVision;
                            monsterTypeText.text = "Fast/Smart";
                        }
                        else if (generateMonsterProbability > 0.10 && generateMonsterProbability <= 0.15)
                        {
                            monsterAi.moveSpeed = slowCruisingSpeed;
                            monsterAi.chaseSpeed = slowChaseSpeed;
                            monsterAi.radioOfVision = lowVision;
                            monsterTypeText.text = "Slow/Dumb";

                        }
                        else if (generateMonsterProbability > 0.15 && generateMonsterProbability <= 0.20)
                        {
                            monsterAi.moveSpeed = slowCruisingSpeed;
                            monsterAi.chaseSpeed = slowChaseSpeed;
                            monsterAi.radioOfVision = highVision;
                            monsterTypeText.text = "Slow/Smart";
                        }
                        spawnTimer = 0;
                        monsterAi.moveSpeed = monsterSpeed;
                        monsterAi.setState(new SelectDirectionState(monsterAi));
                    }
                    else {
                        spawnTimer = 0;
                    }

                    
                    
                }

            }
            else
            {
                currentWorldState = worldState.Spawned;
            }


            

        }
        else if (currentWorldState == worldState.Spawned)
        {
            if (monsterAi.getIsVisible())
            {
                monsterSpeed = monsterAi.moveSpeed;
                currentWorldState = worldState.running;
            }
        }
        else if (currentWorldState == worldState.running)
        {
            if (!monsterAi.getIsVisible())
            {
                monsterAi.moveSpeed = 0;
                currentWorldState = worldState.notSpawn;
            }
        }
        
    }
}
