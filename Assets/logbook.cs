using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logbook : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            anim = GetComponent<Animator>();
            anim.speed = 0f;
            // switch to next scene with an increment of 0.15f
            anim.Play("logbook", 0, 0.15f);
        }
    }
}
