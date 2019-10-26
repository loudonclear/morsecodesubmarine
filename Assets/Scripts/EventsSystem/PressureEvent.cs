using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureEvent : GameEvent
{
 
    public PressureEvent()
    {

        this.damage = 1;
        defaultAction = new Action();
        defaultAction.actionCodes = new KeyCode[3] { KeyCode.P, KeyCode.P, KeyCode.P };
        //defaultAction.actionMessage = "Press P x 3 " + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        defaultAction.actionMessage = "Waiting for command" + "\n to avoid damage \n ";// + (int)(timeEndEvet - timeInEvent);
        defaultAction.messageToSuccess = "ppp";
        ActionResult result = new ActionResult();
        result.damage = this.damage;
        result.succcesMessage = "Well Done, You avoided attack";
        result.failMessage = "Player Receives " + damage + " damage";
        defaultAction.actionResult = result;
        eventName = EventConstants.PRESSURE_EVENT;
        
    }

    public PressureEvent(Action action)
    {
        this.damage = 1;
        defaultAction = action;
        eventName = EventConstants.PRESSURE_EVENT;
    }

     
    }
