using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvent : GameEvent
{

    
    public MonsterEvent()
    {
        
        this.damage = 3;
        defaultAction = new Action();
        defaultAction.actionCodes = new KeyCode[3] { KeyCode.M, KeyCode.M, KeyCode.M };
        //defaultAction.actionMessage = "Press M x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        defaultAction.actionMessage = "Waiting for command" + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        defaultAction.messageToSuccess = "mmm";
        ActionResult result = new ActionResult();
        result.damage = this.damage;
        result.succcesMessage = "Well Done, You avoided attack";
        result.failMessage = "Player Receives " + damage + " damage";
        defaultAction.actionResult = result;

        eventName = EventConstants.MONSTER_EVENT;

    }

    public MonsterEvent(Action action)
    {
        this.damage = 3;
        defaultAction = action;
        eventName = EventConstants.MONSTER_EVENT;
    }
    

}
