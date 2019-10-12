using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvent : GameEvent
{
    


    public MonsterEvent()
    {
        eventName = EventConstants.MONSTER_EVENT;
    }

    public override void endEvent()
    {
        throw new System.NotImplementedException();
    }

    public override string sendEventMessage()
    {
        throw new System.NotImplementedException();
    }

    public override void startEvent()
    {
        
    }

    public override void updateEvent()
    {
        timeInEvent += Time.deltaTime;
        eventMessage = "In Monster Event";
        if (timeInEvent >= timeEndEvet)
        {
            this.endEvent();
            timeInEvent = 0;
        }

    }

}
