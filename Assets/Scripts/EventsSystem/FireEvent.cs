﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : GameEvent
{
    

    public FireEvent()
    {
        this.damage = 2;
        defaultAction = new Action();
        defaultAction.actionCodes = new KeyCode[3] { KeyCode.F, KeyCode.F, KeyCode.F };
        //defaultAction.actionMessage = "Press F x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        defaultAction.actionMessage = "Waiting for command" + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        defaultAction.messageToSuccess = "fff";
        ActionResult result = new ActionResult();
        result.damage = this.damage;
        result.succcesMessage = "Well Done, You avoided attack";
        result.failMessage = "Player Receives " + damage + " damage";
        defaultAction.actionResult = result;

        eventName = EventConstants.FIRE_EVENT;
        
        
    }

    public FireEvent(Action action)
    {
        this.damage = 3;
        defaultAction = action;
        eventName = EventConstants.FIRE_EVENT;
    }

   
}

