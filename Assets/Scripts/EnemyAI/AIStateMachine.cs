using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourScript : MonoBehaviour
{
    private AIState currentState;

    // Start is called before the first frame update
    void Start()
    {
        SetState(new ChasePlayer(new GameObject()));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Tick();
    }

    public void SetState(AIState state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        gameObject.name = "Cube - " + state.GetType().Name;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
