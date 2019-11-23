using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIFlash : MonoBehaviour
{
    public float flashSpeed = 3.0f;

    private Color baseColor;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        baseColor = image.color;
    }

    void Update()
    {
        image.color = Color.Lerp(image.color, baseColor, flashSpeed * Time.deltaTime);
    }

    public void Flash(Color color) {
        image.color = color;
    } 
      
}


