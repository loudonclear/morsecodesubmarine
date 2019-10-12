using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventIncidentsSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public Text evenMessageText;

    Hashtable myEventsTable = new Hashtable();

    enum State { noevent = 0 ,init , running, end, maxEvent };

    float timeSinceLastIncident = 0.0f;
    float timeForNextIncident = 0.0f;
    float timeInCurrentIncident = 0.0f;
    float waitTime = 3.0f;

    
    State currentState = State.noevent;
    GameEvent currentEvent = null;

    public EventIncidentsSystem()
    {
    
    
    }


    void Start()
    {
        myEventsTable.Add(EventConstants.MONSTER_EVENT, new MonsterEvent());
        myEventsTable.Add(EventConstants.FIRE_EVENT, new FireEvent());
        myEventsTable.Add(EventConstants.PRESSURE_EVENT, new PressureEvent());
        evenMessageText.text = "HELLO WORLD";
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastIncident += Time.deltaTime;
        if (timeSinceLastIncident > waitTime)
        {
            if (currentState == State.noevent)
            {
                // fire an incident
                float maxEvent = (float)State.maxEvent;
                int whichEvent = (int)Random.Range(0.0f, maxEvent - 1.0f);
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

                currentEvent.startEvent();
            }
        }
    }


    
}
