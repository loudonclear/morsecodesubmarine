using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipFade : MonoBehaviour
{
    private IEnumerator coroutine;
    public float fadePerSecond = 0.001f;

    private Material material;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        color = material.color;

        color.a = 1.0f;
        material.color = color;

        coroutine = Fade();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if(color.a <= 0.001f) {
            Destroy(this.gameObject);
        }
    }

    // every 2 seconds perform the print()
    private IEnumerator Fade()
    {
        while (true)
        {
            color.a = color.a - 0.03f;
            color.a = color.a >= 0 ? color.a : 0;
            material.color = color;

            //Color c = this.gameObject.GetComponent<MeshRenderer>().material.color;
            yield return color;
        }
    }
}
