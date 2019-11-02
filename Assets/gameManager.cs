using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float volume;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        volume = 1.0f;
    }
}
