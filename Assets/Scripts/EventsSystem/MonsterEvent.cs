using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvent : GameEvent
{

    private Event currentEvent;
   

    public MonsterEvent()
    {
        eventName = EventConstants.MONSTER_EVENT;
        damage = 3;
    }

    public override void endEvent()
    {
        if (currentEvent.codeCompleted)
        {
            eventMessage = "Well Done, You avoided attack";
            succeed = true;
        }
        else {
            eventMessage = "Player Receives " + damage + " damage";
            succeed = false;
        }
        currentEvent = null;
        timeInEvent = 0;

        currentSubState = SubState.end;


    }

    public override void startEvent()
    {
        eventMessage = "This is a " + eventName;

        if (currentEvent == null)
        {
            KeyCode[] codes = new KeyCode[3] { KeyCode.M, KeyCode.M, KeyCode.M };
            currentEvent = new Event(codes);
        }
        currentSubState = SubState.init;
    }

    public override void updateEvent(float deltaTime)
    {

        if (currentSubState == SubState.init)
        {
            timeInEvent += deltaTime;
            if (timeInEvent >= timeToStartTheEvent)
            {
                currentSubState = SubState.running;
                timeInEvent = 0;
            }
            else {
                eventMessage = "This is a " + eventName +", Begins in " + (int)(6- timeInEvent);
            }
            
        }
        


        if (currentEvent != null && currentSubState == SubState.running)
        {
            timeInEvent += deltaTime;

            eventMessage = "Press M x 3 " + "\n to avoid damage \n " + (int)(timeEndEvet - timeInEvent);

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

}
