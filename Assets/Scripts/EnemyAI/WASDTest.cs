﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDTest : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed,
            Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed, 0);

    }
}