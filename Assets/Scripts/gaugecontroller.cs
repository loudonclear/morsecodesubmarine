using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaugecontroller : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float gaugeheight = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(-0.45f, gaugeheight * 0.02f + 0.2f, -6.2f);
        transform.position = pos;
    }
}