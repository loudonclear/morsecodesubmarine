using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : GameEvent
{
    

    public FireEvent()
    {
        this.damage = 2;
        defaultAction = new Action();
        defaultAction.actionCodes = new KeyCode[3] { KeyCode.F, KeyCode.F, KeyCode.F };
        defaultAction.actionMessage = "Press F x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        ActionResult result = new ActionResult();
        result.damage = this.damage;
        result.succcesMessage = "Well Done, You avoided attack";
        result.failMessage = "Player Receives " + damage + " damage";
        defaultAction.actionResult = result;

        eventName = EventConstants.FIRE_EVENT;
        
        
    }

    /*public override void endEvent()
    {
        if (currentEvent.codeCompleted)
        {
            eventMessage = "Well Done, You avoided attack";
            succeed = true;
        }
        else
        {
            eventMessage = "Player Receives " + damage + " damage";
            succeed = false;
        }
        currentEvent = null;
        timeInEvent = 0;

        currentSubState = SubState.end;
    }*/

    /*public override void startEvent()
    {
        eventMessage = "This is a " + eventName;

        if (currentEvent == null)
        {
            
            currentEvent = new Event(codes, this.damage, "Well Done, You avoided attack", "Player Receives " + damage + " damage");
        }
        currentSubState = SubState.init;
    }*/

    /*public override void updateEvent(float deltaTime)
    {
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

            eventMessage = "Press F x 3 " + "\n to avoid damage \n " + (int)(timeEndEvet - timeInEvent);

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

    }*/
}

