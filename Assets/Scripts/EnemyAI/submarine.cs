using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class submarine : MonoBehaviour
{
    public Text submarineText;

    public float internalRadius;
    public float externalRadius;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit detected");
        if (submarineText != null)
        {
            submarineText.text = "hit detected";
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
