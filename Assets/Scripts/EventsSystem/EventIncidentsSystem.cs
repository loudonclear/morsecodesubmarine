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
    

    public Queue<GameEvent> gameEvents = new Queue<GameEvent>();
    private Queue<string> receivedCommands = new Queue<string>();

    Hashtable myEventsTable = new Hashtable();

    enum State { noevent = 0 ,init , running, end, maxEvent };

    float timeSinceLastIncident = 0.0f;
    float timeForNextIncident = 0.0f;
    float timeInCurrentIncident = 0.0f;
    float timeForNextEvent = 5.0f;
    float lastRandom = 0;
    int countRandom = 0;
    
    State currentState = State.noevent;
    GameEvent currentEvent = null;

    public bool enableTimer = true;

    public SubmarineEntity submarine;

    public EventIncidentsSystem()
    {
    
    
    }


    void Start()
    {
        if (eventsId.Count == eventsActionResolved.Count)
        {
            for (int i = 0; i < eventsId.Count; i++)
            {
                int evetnType = eventsId[i];
                string eventResolveCommand = eventsActionResolved[i];

                switch (evetnType)
                {
                    case 1:
                        {
                            Action action = new Action();
                            action.actionCodes = new KeyCode[3] { KeyCode.M, KeyCode.M, KeyCode.M };
                            //defaultAction.actionMessage = "Press M x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
                            action.actionMessage = "Waiting for command" + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
                            action.messageToSuccess = eventResolveCommand;
                            ActionResult result = new ActionResult();
                            result.damage = 3;
                            result.succcesMessage = "Well Done, You avoided attack";
                            result.failMessage = "Player Receives " + 3 + " damage";
                            action.actionResult = result;
                            gameEvents.Enqueue(new MonsterEvent(action));
                        }
                        
                        break;
                    case 2:
                        {
                            Action action = new Action();
                            action.actionCodes = new KeyCode[3] { KeyCode.M, KeyCode.M, KeyCode.M };
                            //defaultAction.actionMessage = "Press M x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
                            action.actionMessage = "Waiting for command" + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
                            action.messageToSuccess = eventResolveCommand;
                            ActionResult result = new ActionResult();
                            result.damage = 2;
                            result.succcesMessage = "Well Done, You avoided attack";
                            result.failMessage = "Player Receives " + 2 + " damage";
                            action.actionResult = result;
                            gameEvents.Enqueue(new FireEvent(action));
                        }
                        
                        break;
                    case 3:
                        {
                            Action action = new Action();
                            action.actionCodes = new KeyCode[3] { KeyCode.M, KeyCode.M, KeyCode.M };
                            //defaultAction.actionMessage = "Press M x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
                            action.actionMessage = "Waiting for command" + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
                            action.messageToSuccess = eventResolveCommand;
                            ActionResult result = new ActionResult();
                            result.damage = 1;
                            result.succcesMessage = "Well Done, You avoided attack";
                            result.failMessage = "Player Receives " + 1 + " damage";
                            action.actionResult = result;
                            gameEvents.Enqueue(new PressureEvent(action));
                        }
                        
                        break;
                    default:
                        break;
                }

            }
        }


         myEventsTable.Add(EventConstants.MONSTER_EVENT, new MonsterEvent());
         myEventsTable.Add(EventConstants.FIRE_EVENT, new FireEvent());
         myEventsTable.Add(EventConstants.PRESSURE_EVENT, new PressureEvent());


        playerInfo.text = "Hull: " + (submarine.currentHullHealth / SubmarineEntity.HULL_HEALTH * 100).ToString("F1") + "%\n";
        inputInfo.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        playerInfo.text = "Hull: " + (submarine.currentHullHealth / SubmarineEntity.HULL_HEALTH * 100).ToString("F1") + "%\n";

        if (currentState == State.noevent)
        {
            timeSinceLastIncident += Time.deltaTime;
            if (timeSinceLastIncident > timeForNextEvent)
            {
                // fire a random incident
                if (gameEvents.Count == 0)
                {
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
                   
                }
                else {
                    currentEvent = gameEvents.Dequeue();
                }

                timeSinceLastIncident = 0;
                if (currentEvent == null)
                {
                    Debug.Log("ERROR HERE");
                }
                else
                {
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
                            submarine.currentHullHealth -= currentEvent.damage;
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
