using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        //Application.Quit();
    }
}
