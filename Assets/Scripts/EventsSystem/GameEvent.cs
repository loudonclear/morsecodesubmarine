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

    private Queue<Action> myListOfActions = new Queue<Action>();
    protected Event currentEvent;

    public enum SubState { noevent = 0, init, running, end, maxEvent };

    protected float timeInEvent = 0.0f;

    protected float timeEndEvet = 6.0f;

    protected float timeToStartTheEvent = 6.0f;

    protected string eventName;

    protected string eventMessage;

    protected string eventActionMessage;

    public int damage = 0 ;

    public bool succeed = false;

    public SubState currentSubState;

    protected string defaultSuccessMesasage = "Well Done, You avoided attack";
    protected string defaultFailMesasage;

    protected Action defaultAction;
    


    public virtual void startEvent() {
        eventMessage = "This is a " + eventName;

        if (currentEvent == null)
        {
            if (myListOfActions.Count == 0)
            {
                currentEvent = new Event(defaultAction);
            }
            else {
                currentEvent.myAction = myListOfActions.Dequeue();
            }
            
        }
        currentSubState = SubState.init;
    }

    public virtual void updateEvent(float deltaTime) {
        if (currentSubState == SubState.init)
        {
            timeInEvent += deltaTime;
            if (timeInEvent >= timeToStartTheEvent)
            {
                currentSubState = SubState.running;
                timeInEvent = 0;
            }
            else
            {
                eventMessage = "This is a " + eventName + ", Begins in " + (int)(6 - timeInEvent);
            }

        }



        if (currentEvent != null && currentSubState == SubState.running)
        {
            timeInEvent += deltaTime;

            eventMessage = currentEvent.myAction.actionMessage + (int)(timeEndEvet - timeInEvent);

            if (timeInEvent >= timeEndEvet)
            {
                this.endEvent();
                return;
            }

            currentEvent.Update(deltaTime);
            if (currentEvent.codeCompleted)
            {
                this.endEvent();
                return;
            }
        }
    }

    public virtual void endEvent() {

        if (currentEvent.codeCompleted)
        {
            eventMessage = currentEvent.myAction.actionResult.succcesMessage;
            succeed = true;
        }
        else
        {
            eventMessage = currentEvent.myAction.actionResult.failMessage;
            succeed = false;
        }
        currentEvent = null;
        timeInEvent = 0;

        currentSubState = SubState.end;
    }

    public string getEventName()
    {
        return this.eventName;
    }

    public virtual string getEventMessage()
    {
        return eventMessage;
    }
    


}
