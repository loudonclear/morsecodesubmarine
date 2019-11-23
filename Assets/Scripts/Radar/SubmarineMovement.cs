﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    private FlankSpeed desiredSpeed;
    private Rigidbody rb;

    public const float maxThrust = 1.5f;
    public const float speedMultiplier = 1.0f;
    public const float thrustMultiplier = 2.0f;

    private float desiredAngle;
    private float leftDegrees, rightDegrees;
    //public const float torqueMultiplier = 0.001f;
    public const float turningSpeed = 0.01f;
    public const float turnDelta = 45.0f;

    public VerticalGauge depthGauge;
    public SpinningGaugeSmooth speedGauge;
    public Compass compass;

    public SubmarineEntity submarine;

    public const float emergencyHeatIncrease = 4.0f;    // Fahrenheit

    public const float depthDelta = 4.0f;
    private float desiredDepth;
    public const float maxDiveSpeed = 0.5f;

    public const float maxDepthDelta = 3 * depthDelta;

    void Start()
    {
        desiredSpeed = FlankSpeed.OFF;
        speedGauge.percent = (float)desiredSpeed / 5;
        speedGauge.targetPercent = (float)desiredSpeed / 5;
        rb = GetComponent<Rigidbody>();

        desiredAngle = 0;
        leftDegrees = 0;
        rightDegrees = 0;

        desiredDepth = 0.0f;
    }

    private void Update()
    {
        Debug.Log("WT: " + this.transform.position.y);
        ventEngineHeat();
        float angle = Vector3.SignedAngle(this.transform.up, Vector3.forward, Vector3.up);
        //compass.transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.forward);
        compass.rotate(this.transform.eulerAngles.y);
        depthGauge.gaugeheight = this.transform.position.y / maxDepthDelta / 2 + 0.5f;
    }

    public void Descend() {
        desiredDepth = Mathf.Clamp(desiredDepth - depthDelta, -maxDepthDelta, maxDepthDelta);
    }

    public void Ascend() {
        desiredDepth = Mathf.Clamp(desiredDepth + depthDelta, -maxDepthDelta, maxDepthDelta);
    }

    public void Port()
    {
        leftDegrees += turnDelta;
    }

    public void Starboard()
    {
        rightDegrees += turnDelta;
    }

    public void Accelerate()
    {
        desiredSpeed = (FlankSpeed)Mathf.Clamp((int)(desiredSpeed + 1), 0, (int)FlankSpeed.EMERGENCY);

        speedGauge.targetPercent = (float)desiredSpeed / 5;
    }

    public void Decelerate()
    {
        desiredSpeed = (FlankSpeed)Mathf.Clamp((int)(desiredSpeed - 1), 0, (int)FlankSpeed.EMERGENCY);
 
        speedGauge.targetPercent = (float)desiredSpeed / 5;
    }

    public void EnginesOff()
    {
        desiredSpeed = 0;

        speedGauge.targetPercent = (float)desiredSpeed / 5;
    }

    private void FixedUpdate()
    {
        if(desiredSpeed != 0) {
            // Speed calculations
            Vector3 localVelocity = this.transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
            float desiredSpeedValue = speedMultiplier * (float)(desiredSpeed == FlankSpeed.EMERGENCY ? FlankSpeed.FULL : desiredSpeed);

            float effectiveMaxThrust = desiredSpeed == FlankSpeed.EMERGENCY ? 2.0f * maxThrust : maxThrust;
            float thrust = Mathf.Clamp(thrustMultiplier * (desiredSpeedValue - localVelocity.y), -effectiveMaxThrust, effectiveMaxThrust);
       
            rb.AddRelativeForce(Vector3.up * thrust);

            Vector3 v = rb.velocity;
            v.y = Mathf.Clamp(desiredDepth - this.transform.position.y, -maxDiveSpeed, maxDiveSpeed);
            rb.velocity = v;


            // Rotation calculations
            desiredAngle = rightDegrees - leftDegrees;

            Vector3 desiredVector = (Quaternion.AngleAxis(desiredAngle, Vector3.up) * Vector3.forward).normalized;

            float currentDelta = Vector3.SignedAngle(this.transform.up, desiredVector, Vector3.up);
            // Make it positive or negative depending on the side it's on
            // currentDelta = currentDelta * (desiredAngle >= 0 ? -1 : 1);

            if(desiredAngle >= 0) {
                currentDelta = currentDelta <= 0 ? currentDelta + 360 : currentDelta;
            }

            //float torqueForce = -torqueMultiplier * currentDelta;
            //rigidbody.AddTorque(torqueForce * Vector3.up, ForceMode.Force);

            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(Vector3.down, desiredVector), turningSpeed);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            //Destroy(collider.gameObject);
        }
    }

    private void ventEngineHeat() {
        float heatDelta = 0.0f;
        switch (desiredSpeed)
        {
            case FlankSpeed.OFF:
                break;
            case FlankSpeed.ONE_THIRD:
            case FlankSpeed.TWO_THIRDS:
            case FlankSpeed.STANDARD:
                heatDelta = emergencyHeatIncrease * 0.25f;
                break;
            case FlankSpeed.FULL:
                heatDelta = emergencyHeatIncrease * 0.5f;
                break;
            case FlankSpeed.EMERGENCY:
                heatDelta = emergencyHeatIncrease;
                break;
        }
        //Debug.Log("HD: " + heatDelta);
        submarine.temperature += heatDelta * Time.deltaTime;
    }
}
