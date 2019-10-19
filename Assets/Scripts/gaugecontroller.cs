using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaugecontroller : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float gaugeheight = 1.0f;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.localPosition = Vector2.Lerp(minPosition, maxPosition, gaugeheight);
    }
}