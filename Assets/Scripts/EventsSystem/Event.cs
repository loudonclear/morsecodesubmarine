using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    private float myAllowedDelay = 1f;
    private KeyCode[] myActions;
    private int myIndex = 0;
    private float myDelayTimer;

    public bool codeCompleted = false;

    public Event(KeyCode[] actions )
    {
        this.myActions = actions;
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
            if (Input.GetKeyDown(myActions[myIndex]))
            {
                myIndex++;
                myDelayTimer = 0f;
            }
            else
            {
                ResetCodeInput();
            }
        }

        if (myIndex == myActions.Length)
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
