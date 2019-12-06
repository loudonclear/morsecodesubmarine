using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : Tutorial
{
    // Start is called before the first frame update
    

    void Start()
    {
        indexCurrentDialog = 0;
        Button btn = starButton.GetComponent<Button>();
        btn.onClick.AddListener(InitTutorialOnClick);
    }

    // Update is called once per frame
    /*void Update()
    {
        if (currentState != null)
        {

            currentState.Tick();
        }

    }

    public void setState(DialogueState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }

    }*/

    void InitTutorialOnClick()
    {

        Button btn = starButton.GetComponent<Button>();
        btn.transform.position = new Vector3(2000, 2000, 0);
        currentDialog = tutorialDialogs[indexCurrentDialog];
        setState(new MonsterTuTorialInitialDialogue(this, currentDialog));
    }
}
