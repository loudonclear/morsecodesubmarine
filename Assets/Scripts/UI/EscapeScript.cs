using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeScript : MonoBehaviour
{
    public string sceneToLoad;
    public bool quitOnEscape = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (quitOnEscape)
            {
                Application.Quit();
            }
            if (sceneToLoad != "")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
