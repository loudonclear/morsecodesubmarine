using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineAIHelper : MonoBehaviour
{
    public Text submarineText;

    public float internalRadius;
    public float externalRadius;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit detected");
        if (submarineText != null)
        {
            submarineText.text = "hit detected from SUBMARINE";
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (submarineText != null)
        {
            submarineText.text = "";
        }
    }

}
