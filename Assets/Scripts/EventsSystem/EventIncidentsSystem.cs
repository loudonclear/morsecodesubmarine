using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventIncidentsSystem : MonoBehaviour
{
    // Start is called before the first frame update

    struct editorActionToEvent
    {
        int id;
        string command;
    };

    public Text evenMessageText;
    public Text playerInfo;
    public Text inputInfo;

    public List<int> eventsId;
    public List<string> eventsActionResolved;
    

    public List<GameEvent> gameEvents = new List<GameEvent>();
    private Queue<string> receivedCommands = new Queue<string>();

    Hashtable myEventsTable = new Hashtable();

    enum State { noevent = 0 ,init , running, end, maxEvent };

    float timeSinceLastIncident = 0.0f;
    float timeForNextIncident = 0.0f;
    float timeInCurrentIncident = 0.0f;
    float timeForNextEvent = 5.0f;
    int playerHealth = 10;
    float lastRandom = 0;
    int countRandom = 0;
    
    State currentState = State.noevent;
    GameEvent currentEvent = null;

    public bool enableTimer = true;

    public EventIncidentsSystem()
    {
    
    
    }


    void Start()
    {
        /*if (eventsId.Count == eventsActionResolved.Count)
        {
            for (int i = 0; i < eventsId.Count; i++)
            {
                
            }
        }*/

        
         myEventsTable.Add(EventConstants.MONSTER_EVENT, new MonsterEvent());
         myEventsTable.Add(EventConstants.FIRE_EVENT, new FireEvent());
         myEventsTable.Add(EventConstants.PRESSURE_EVENT, new PressureEvent());
        
        
        playerInfo.text = "Player HP: " + playerHealth;
        inputInfo.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        playerInfo.text = "Player HP: " + playerHealth;
        if (currentState == State.noevent)
        {
            timeSinceLastIncident += Time.deltaTime;
            if (timeSinceLastIncident > timeForNextEvent)
            {
                // fire an incident
                float maxEvent = (float)State.maxEvent;
                int whichEvent = -1;
                while (true)
                {
                    
                    whichEvent = (int)Random.Range(1.0f, maxEvent - 1.0f);
                    if (lastRandom == whichEvent
                        && countRandom < 2)
                    {
                        countRandom++;
                        break;
                    }
                    else if (lastRandom == whichEvent && countRandom == 2)
                    {
                        countRandom = 0;
                        lastRandom = 0;
                        whichEvent = 3;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                

                lastRandom = whichEvent;
               // whichEvent = 3;
                switch (whichEvent)
                {
                    case 1:
                        currentEvent = (GameEvent)myEventsTable[EventConstants.MONSTER_EVENT];
                        break;
                    case 2:
                        currentEvent = (GameEvent)myEventsTable[EventConstants.FIRE_EVENT];
                        break;
                    case 3:
                        currentEvent = (GameEvent)myEventsTable[EventConstants.PRESSURE_EVENT];
                        break;
                    default:
                        break;
                }
                timeSinceLastIncident = 0;
                if (currentEvent == null)
                {
                    Debug.Log("ERROR HERE");
                }
                else {
                    currentEvent.startEvent();
                }

                currentState = State.running;
            }
            else {
                evenMessageText.text = "Time For Next Event in " + (int)(6 - timeSinceLastIncident);
            }
        }
        else {

            if(currentEvent != null)
            {
                if (currentEvent.currentSubState == GameEvent.SubState.end)
                {
                    timeSinceLastIncident += Time.deltaTime;
                    if (timeSinceLastIncident > timeForNextEvent)
                    {
                        if (!currentEvent.succeed)
                        {
                            playerHealth -= currentEvent.damage;
                        }
                        currentEvent = null;
                        currentState = State.noevent;
                        timeSinceLastIncident = 0;
                        inputInfo.text = "";
                    }

                }
                else {

                    if (Input.anyKeyDown)
                    {
                        foreach (char c in Input.inputString)
                        {
                            if(c >= 'a'  && c <= 'z')
                            inputInfo.text += c;

                        }
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                           // Debug.Log("Return key was pressed.");
                            string code = inputInfo.text;
                            resolveCode(code);
                            inputInfo.text = "";
                        }

                    }

                    if (currentEvent.receivedCode != "" && receivedCommands.Count != 0)
                    {
                        currentEvent.receivedCode = receivedCommands.Dequeue();
                    }
                    currentEvent.updateEvent(Time.deltaTime);

                    evenMessageText.text = currentEvent.getEventMessage();
                }
                
            }
            
        }
    }

    public void resolveCode(string interpretedCode)
    {
        if (interpretedCode != "")
        {
            receivedCommands.Enqueue(interpretedCode);
        }
        
    }
    
}
