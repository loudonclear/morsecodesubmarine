using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGUI : MonoBehaviour
{
    public float power = 0.7f;
    public float duration = 2;
    public bool shouldShake;
    public float slowDownAmmount = 1.0f;

    private float initialDuration;

    public RectTransform Panel;
    public Vector3 initialPanelPos;
    private RectTransform panelRectTransform;


    void Start()
    {
        shouldShake = false;
        initialDuration = duration;
        panelRectTransform = Panel.GetComponent<RectTransform>();
        initialPanelPos = Panel.GetComponent<RectTransform>().localPosition;
    }

    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                panelRectTransform.localPosition = initialPanelPos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                panelRectTransform.localPosition = initialPanelPos;
            }
        }
    }

    public void Shake()
    {
        shouldShake = true;
    }

    public bool isShaking()
    {
        return shouldShake;
    }
}
