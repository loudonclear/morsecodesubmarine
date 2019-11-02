using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineRadarDetector : MonoBehaviour
{
    public GameObject submarine;
    public SubmarineRadar radarScript;

    void OnTriggerStay(Collider collisionInfo)
    {
        radarScript.CollisionDetected(collisionInfo, submarine);
    }

}
