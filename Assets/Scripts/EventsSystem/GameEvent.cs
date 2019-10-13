using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventConstants
{
    public const string MONSTER_EVENT = "MONSTER EVENT";
    public const string FIRE_EVENT = "FIRE EVENT";
    public const string PRESSURE_EVENT = "PRESURE EVENT";
}

public  class GameEvent
{
    // Start is called before the first frame update

    public enum SubState { noevent = 0, init, running, end, maxEvent };

    protected float timeInEvent = 0.0f;

    protected float timeEndEvet = 6.0f;

    protected float timeToStartTheEvent = 6.0f;

    protected string eventName;

    protected string eventMessage;

    public int damage = 0 ;

    public bool succeed = false;

    public SubState currentSubState;

    public virtual void startEvent() { }

    public virtual void updateEvent(float deltaTime) { }

    public virtual void endEvent() { }

    public string getEventName()
    {
        return this.eventName;
    }

    public virtual string getEventMessage()
    {
        return eventMessage;
    }
    


}
