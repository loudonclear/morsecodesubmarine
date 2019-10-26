using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed ;
    public float radioOfVision ;
    private AIState currentState;

    // Start is called before the first frame update
    void Start()
    {
       
        setState(new UnAware(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }

    public void setState(AIState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "Monster - " + state.GetType().Name;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }

    }


    public void moveTo(Vector3 direction)
    {
        //var direction = getDirection(destination);
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    public void moveTowards(Vector3 destination)
    {
        var direction = getDirection(destination);
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    private Vector3 getDirection(Vector3 destination)
    {
        return (destination - transform.position).normalized;
    }

    public bool playerInRangeOfVision(GameObject submarine)
    {
        Vector3 center = transform.position;
        if (Mathf.Pow(submarine.transform.position.x - center.x,2) 
            + Mathf.Pow(submarine.transform.position.y - center.y, 2) < Mathf.Pow(radioOfVision,2))
        {
            return true;
        }

        return false;
    }
}
