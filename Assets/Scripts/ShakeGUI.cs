using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGUI : MonoBehaviour
{

    public float power = 0.7f;
    public float duration = 2;
    public bool shouldShake;
    public float slowDownAmmount = 1.0f;
    //public Transform cameraTransform;

    private float initialDuration;
    //private Vector3 cameraInitialTransform;

    public RectTransform Panel;
    public Vector3 initialPanelPos;
    private RectTransform panelRectTransform;


    // Start is called before the first frame update
    void Start()
    {
        //power = 0.7f;
        //duration = 2;
        shouldShake = false;
        //slowDownAmmount = 1.0f;
        initialDuration = duration;
        //cameraTransform = Camera.main.transform;
        //cameraInitialTransform = cameraTransform.localPosition;
        panelRectTransform = Panel.GetComponent<RectTransform>();
        initialPanelPos = Panel.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (shouldShake)
        {
            if (duration > 0)
            {
                panelRectTransform.localPosition = initialPanelPos + Random.insideUnitSphere * power;
                //cameraTransform.localPosition = cameraInitialTransform + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                panelRectTransform.localPosition = initialPanelPos;
            }
        }

       /* if (bShake)
        {
            float duration = 0.50f;
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            shareOffset = Mathf.Lerp(0, shakeAmount, lerp);
            RectTransform rt = Panel.GetComponent<RectTransform>();
            rt.localPosition += Vector3.right;
        } */
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
