using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shipName : MonoBehaviour
{
    // Start is called before the first frame update
    public gameManager managerScript;
    public Text decodedText;

    void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
        decodedText.text = managerScript.shipName;
    }
    
}
