using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    [SerializeField] private Sprite[] _allSprites;
    public Text[] pages;
    private Image _image;
    private Button btn;
    private RectTransform rt;

    private int index = 0;

    public Vector2 largeSize;
    public Vector2 largePos;

    private Vector2 smallSize;
    private Vector2 smallPos;


    void Start()
    {
        btn = GetComponent<Button>();
        _image = GetComponent<Image>();

        rt = GetComponent<RectTransform>();
        smallSize = rt.localScale;
        smallPos = rt.localPosition;

        foreach (Text page in pages)
        {
            page.enabled = false;
        }

        _image.sprite = _allSprites[index];
        pages[0].enabled = true;
        pages[1].enabled = true;

        btn.onClick.AddListener(() =>
        {
            pages[2 * index].enabled = false;
            pages[2 * index + 1].enabled = false;
            index = (index + 1) % _allSprites.Length;
            _image.sprite = _allSprites[index];
            pages[2 * index].enabled = true;
            pages[2 * index + 1].enabled = true;

            if (index == 0)
            {
                rt.localScale = smallSize;
                rt.localPosition = smallPos;
            } else
            {
                rt.localScale = largeSize;
                rt.localPosition = largePos;
            }
        });
    }

    public int GetPageIndex()
    {
        return index;
    }

    public void SetPageIndex(int indx)
    {
        this.index = indx;
    }

    public int GetAllSpritesLength()
    {
        return _allSprites.Length;
    }


}
