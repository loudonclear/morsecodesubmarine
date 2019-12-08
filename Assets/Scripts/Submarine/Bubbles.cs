using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubbles : MonoBehaviour
{
    public Rigidbody submarine;

    private Image image;
    private Animator animator;

    void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float xcomp = Mathf.Abs(submarine.velocity.x) + Mathf.Abs(submarine.velocity.z);

        if (xcomp > 0.0001)
        {
            image.enabled = true;
            animator.speed = Mathf.Clamp(submarine.velocity.magnitude / 3, 0.5f, 1);

            float ycomp = -45f * submarine.velocity.y * 2;
            image.rectTransform.localRotation = Quaternion.Euler(0, 0, ycomp);
        }
        else
        {
            image.enabled = false;
            animator.speed = 0;
        }

    }
}
