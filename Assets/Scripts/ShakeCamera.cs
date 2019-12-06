

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float power = 0.7f;
    public float duration = 2;
    public bool shouldShake;
    public float slowDownAmmount = 1.0f;
    public Transform cameraTransform;

    private float initialDuration;
    private Vector3 cameraInitialTransform;

    
    // Start is called before the first frame update
    void Start()
    {
        power = 0.7f;
        duration = 2;
        shouldShake = false;
        slowDownAmmount = 1.0f;
        initialDuration  = duration;
        cameraTransform = Camera.main.transform;
        cameraInitialTransform = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                cameraTransform.localPosition = cameraInitialTransform + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmmount;
            }
            else {
                shouldShake = false;
                duration = initialDuration;
                cameraTransform.localPosition = cameraInitialTransform;
            }
        }
    }
}
