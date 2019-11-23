using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinningGaugeSmooth : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float percent = 0.0f;
    public float targetPercent = 0.0f;

    public float min = 0.0f;
    public float max = 1.0f;

    private RectTransform rectTransform;

    public const float needleSpeed = 0.5f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector2 size = GetComponent<RectTransform>().sizeDelta;
        Vector2 pixelPivot = GetComponent<Image>().sprite.pivot;
        Vector2 percentPivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
        GetComponent<RectTransform>().pivot = percentPivot;

        rectTransform.rotation = Quaternion.AngleAxis(Mathf.Lerp(min, max, percent), Vector3.forward);
    }

    void Update()
    {
        float delta = targetPercent - percent;
        if (Mathf.Abs(delta) >= 0.01)
        {
            int direction = (int)((float)(targetPercent - percent) / Mathf.Abs(targetPercent - percent));

            percent += direction * needleSpeed * Time.deltaTime;
            rectTransform.rotation = Quaternion.AngleAxis(Mathf.Lerp(min, max, percent), Vector3.forward);
        }
    }
}
