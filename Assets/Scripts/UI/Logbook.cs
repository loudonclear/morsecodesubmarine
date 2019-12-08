using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logbook : MonoBehaviour
{
    public GameObject LogbookPage;

    private Image image;
    private Button btn;
    private RectTransform rt;

    void Start()
    {
        btn = GetComponent<Button>();
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        LogbookPage.SetActive(false);
    }

    public void OpenLogbook()
    {
        LogbookPage.SetActive(true);
    }

    public void CloseLogbook()
    {
        LogbookPage.SetActive(false);
    }
}
