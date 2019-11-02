using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSweeper : MonoBehaviour
{
    public SubmarineRadar radar;
    private float lastAngle = 0;

    void Start()
    {
        lastAngle = radar.sweepAngle;
    }

    void Update()
    {
        this.transform.RotateAround(radar.transform.position, new Vector3(0, 1, 0), radar.sweepAngle - lastAngle);
        lastAngle = radar.sweepAngle;
        this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
    }

}
