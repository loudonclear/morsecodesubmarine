using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterObject : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 center;
    

    void Start()
    {
        center.x  = Screen.width / 2;
        center.y = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        center.x = Screen.width / 2;
        center.y = Screen.height / 2;
    }

    public Vector3 getCenter()
    {
        return center;
    }
}
