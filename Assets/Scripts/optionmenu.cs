using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionmenu : MonoBehaviour
{
    public static float min = 0.0f;
    public static float max = 1.0f;
    public Slider volumeSlider;
    public Slider keyPressSlider;
    public gameManager managerScript;

    void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
        volumeSlider.minValue = min;
        volumeSlider.maxValue = max;
        keyPressSlider.minValue = min;
        keyPressSlider.maxValue = max;
    }
    public void VolumeController()
    {
        managerScript.volume = volumeSlider.value;
    }
    public void keyController()
    {
        managerScript.timeUnit = keyPressSlider.value;
    }
}
