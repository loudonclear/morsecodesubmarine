﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float volume;
    public float timeUnit;
    public string shipName;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        volume = 0.5f;
        timeUnit = 0.2f;
        shipName = null;
    }
}
