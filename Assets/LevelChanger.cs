using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public gameManager managerScript;
    public Text texty;
    private int levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void ToTheMain()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
        managerScript.shipName = texty.text;
        SceneManager.LoadScene("Main");
    }
}
