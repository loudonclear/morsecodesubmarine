using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedcontroller : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    public float speedpercent = 0.0f;

    public float minTemperature = 0.0f;
    public float maxTemperature = 1.0f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector2 size = GetComponent<RectTransform>().sizeDelta;
        Vector2 pixelPivot = GetComponent<Image>().sprite.pivot;
        Vector2 percentPivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
        GetComponent<RectTransform>().pivot = percentPivot;
    }

    void Update()
    {
        rectTransform.rotation = Quaternion.AngleAxis(Mathf.Lerp(minTemperature, maxTemperature, speedpercent), Vector3.forward);
    }
}
