using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    private float myAllowedDelay = 1f;
    public Action myAction { get; set; }
    public ActionResult myActionResult { get; set; }
    private int myIndex = 0;
    private float myDelayTimer;

    public bool codeCompleted = false;

    public Event()
    {

    }

    public Event(Action action)
    {
        myAction = action;
    }

    public void Update(float deltaTime)
    {
        myDelayTimer += deltaTime;
        if (myDelayTimer > myAllowedDelay)
        {
            ResetCodeInput();
        }

        if (Input.anyKeyDown)
        {
            //Debug.Log("Event::GOT KEY");
            if (Input.GetKeyDown(myAction.actionCodes[myIndex]))
            {
                myIndex++;
                myDelayTimer = 0f;
            }
            else
            {
                ResetCodeInput();
            }
        }

        if (myIndex == myAction.actionCodes.Length)
        {
            ResetCodeInput();
            codeCompleted = true;
        }
    }

    public void  ResetCodeInput()
    {
        myIndex = 0;
        myDelayTimer = 0f;
    }
}
