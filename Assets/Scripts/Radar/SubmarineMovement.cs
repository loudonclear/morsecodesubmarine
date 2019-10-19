using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    private FlankSpeed desiredSpeed;
    private bool engineOn;
    private Rigidbody rigidbody;

    public const float maxThrust = 1.5f;
    public const float speedMultiplier = 4.0f;
    public const float thrustMultiplier = 2.0f;

    private float desiredAngle;
    private float leftDegrees, rightDegrees;
    public const float torqueMultiplier = 0.001f;
    public const float turnDelta = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        desiredSpeed = FlankSpeed.STANDARD;
        engineOn = true;
        rigidbody = GetComponent<Rigidbody>();

        desiredAngle = 0;
        leftDegrees = 0;
        rightDegrees = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("SLOW");
            desiredSpeed = (FlankSpeed)Mathf.Clamp((int)(desiredSpeed - 1), (int)FlankSpeed.ONE_THIRD, (int)FlankSpeed.EMERGENCY);
        }
        else if (Input.GetKeyDown("w"))
        {
            desiredSpeed = (FlankSpeed)Mathf.Clamp((int)(desiredSpeed + 1), (int)FlankSpeed.ONE_THIRD, (int)FlankSpeed.EMERGENCY);
        }
        else if (Input.GetKeyDown("g"))
        {
            engineOn = engineOn ? false : true;
        }
        else if (Input.GetKeyDown("a"))
        {
            leftDegrees += turnDelta;
        }
        else if (Input.GetKeyDown("d"))
        {
            rightDegrees += turnDelta;
        }
        //Debug.Log(rightDegrees - leftDegrees);
    }

    private void FixedUpdate()
    {
        if(engineOn) {
            // Speed calculations
            Vector3 localVelocity = this.transform.InverseTransformDirection(rigidbody.velocity);
            float desiredSpeedValue = speedMultiplier * (float)(desiredSpeed == FlankSpeed.EMERGENCY ? FlankSpeed.FULL : desiredSpeed);

            float effectiveMaxThrust = desiredSpeed == FlankSpeed.EMERGENCY ? 2.0f * maxThrust : maxThrust;
            float thrust = Mathf.Clamp(thrustMultiplier * (desiredSpeedValue - localVelocity.y), -effectiveMaxThrust, effectiveMaxThrust);
       
            //Debug.Log("Velocity: " + localVelocity.y + "\tTarget: " + desiredSpeedValue + "\tThrust: " + thrust);

            rigidbody.AddRelativeForce(Vector3.up * thrust);

            // Rotation calculations
            desiredAngle = rightDegrees - leftDegrees;

            Vector3 desiredVector = Quaternion.AngleAxis(desiredAngle, Vector3.up) * Vector3.forward;

            float currentDelta = Vector3.SignedAngle(this.transform.up, desiredVector, Vector3.up);
            // Make it positive or negative depending on the side it's on
            // currentDelta = currentDelta * (desiredAngle >= 0 ? -1 : 1);

            if(desiredAngle >= 0) {
                currentDelta = currentDelta <= 0 ? currentDelta + 360 : currentDelta;
            }
            if (desiredAngle >= 0)
            {
                currentDelta = currentDelta >= 0 ? currentDelta - 360 : currentDelta;
            }

            float torqueForce = torqueMultiplier * currentDelta;

            //Debug.Log(desiredAngle);
            rigidbody.AddTorque(torqueForce * Vector3.up, ForceMode.Force);
            Debug.Log("Torque: " + torqueForce + "\tcurrentDelta: " + currentDelta + "\tdesiredVector: " + desiredVector + "\tus:" + this.transform.up + "\teuler: " + transform.localRotation.eulerAngles.y);
        }
        else {
            Vector3 localVelocity = this.transform.InverseTransformDirection(rigidbody.velocity);
           // Debug.Log("ENGINE OFF. VELOCITY: " + localVelocity.y);
        }
    }
}
