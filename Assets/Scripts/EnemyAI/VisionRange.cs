using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    public LineRenderer line;
    GameObject submarine = null;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        submarine = GameObject.Find("Submarine");
        line.positionCount = segments + 1;
        line.useWorldSpace = true;
        CreatePoints();
        playerPos = submarine.transform.position;
    }

    void CreatePoints()
    {

        float angle = 20f;
        Vector3[] positions = new Vector3[segments + 1];
        for (int i = 0; i < (segments +1 ); i++)
        {
            Vector3 v = new Vector3(submarine.transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * xradius
                ,0
                , submarine.transform.position.z + Mathf.Cos(Mathf.Deg2Rad * angle) * yradius);

            positions[i] = v;
            
            angle += (360f / segments);
        }

        line.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerPos, submarine.transform.position) > 0.1 )
        {
            CreatePoints();
            playerPos = submarine.transform.position;
        }
       
    }
}
