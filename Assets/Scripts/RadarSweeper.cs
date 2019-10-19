using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSweeper : MonoBehaviour
{
    public SubmarineRadar radar;
    private float lastAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastAngle = radar.sweepAngle;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(radar.transform.position, new Vector3(0, 1, 0), radar.sweepAngle - lastAngle);
        lastAngle = radar.sweepAngle;
            /*.Rotate(
            rotatePointAroundAxis(this.transform.position, 0, radar.transform.up),
            Space.World);*/
    }

    Vector3 rotatePointAroundAxis(Vector3 point, float angle, Vector3 axis)
    {

        Quaternion q = Quaternion.AngleAxis(angle, axis);
        Debug.Log(q);
        return q * point; //Note: q must be first (point * q wouldn't compile)
    }
}
