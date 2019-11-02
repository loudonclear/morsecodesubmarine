using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionmenu : MonoBehaviour
{
    public static float min = 0.0f;
    public static float max = 1.0f;
    public Slider volumeSlider;
    void Start()
    {
        volumeSlider.minValue = min;
        volumeSlider.maxValue = max;
    }
    public void VolumeController()
    {
        gameManager managerScript = GameObject.FindGameObjectWithTag("audio").GetComponent<gameManager>();
        managerScript.volume = volumeSlider.value;
    }
}
