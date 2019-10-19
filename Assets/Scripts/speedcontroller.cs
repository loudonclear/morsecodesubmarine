using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedcontroller : MonoBehaviour
{

    [Range(0.0f, 100.0f)]
    public float speedpercent = 0.0f;

    //public Vector3 Pivot;
   
    void Update()
    {
        //transform.position += (transform.rotation*Pivot);
        
        transform.rotation = Quaternion.AngleAxis(speedpercent * 3.6f, Vector3.forward);
 
        //transform.position -= (transform.rotation*Pivot);
       
    }
}
