using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class submarine : MonoBehaviour
{
    public Text submarineText;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit detected");
        if (submarineText != null)
        {
            submarineText.text = "hit detected";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (submarineText != null)
        {
            submarineText.text = "";
        }
    }

}
