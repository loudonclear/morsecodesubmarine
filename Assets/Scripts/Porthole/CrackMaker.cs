using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrackMaker : MonoBehaviour
{
    public SubmarineEntity submarine;
    private Image crackImage;

    public Sprite image0;
    public Sprite image25;
    public Sprite image50;
    public Sprite image75;
    public Sprite image100;

    private Sprite currentImage;

    // Start is called before the first frame update
    void Start()
    {
        crackImage = this.GetComponent<Image>();
        currentImage = image100;
    }

    // Update is called once per frame
    void Update()
    {
        float hullPercent = submarine.currentHullHealth / SubmarineEntity.HULL_HEALTH;
        if (hullPercent < 0.25f)
            setImage(image25);
        else if (hullPercent < 0.5f)
            setImage(image50);
        else if (hullPercent < 0.75f)
            setImage(image75);
        else if (hullPercent < 1.0f)
            setImage(image100);
    }

    private void setImage(Sprite image) {
        if (currentImage != image) {
            currentImage = image;
            crackImage.sprite = image;
        }
    }
}
