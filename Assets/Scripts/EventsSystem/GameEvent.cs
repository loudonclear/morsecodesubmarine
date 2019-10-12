using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventConstants
{
    public const string MONSTER_EVENT = "MONSTER_EVENT";
    public const string FIRE_EVENT = "FIRE_EVENT";
    public const string PRESSURE_EVENT = "PRESURE_EVENT";
}

public abstract class GameEvent 
{
    // Start is called before the first frame update

    protected float timeInEvent = 0.0f;

    protected float timeEndEvet = 6.0f;

    protected string eventName;

    protected string eventMessage;

    public abstract void startEvent();

    public abstract void updateEvent();

    public abstract void endEvent();

    public string getEventName()
    {
        return this.eventName;
    }

    public abstract string sendEventMessage();
    


}
