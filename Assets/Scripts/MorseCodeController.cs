using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseCodeController : MonoBehaviour
{
    public Text morseCodeText;

    public float shortThreshold = 0.1f;

    [Range(1, 20000)]
    public float frequency1 = 500;

    [Range(1, 20000)]
    public float frequency2 = 500;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    private AudioSource audioSource;
    int timeIndex = 0;
    private float inputTime;

    void Start()
    {
        morseCodeText.text = "";

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                inputTime = Time.time;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (audioSource.isPlaying)
            {
                timeIndex = 0;  //resets timer before playing sound
                audioSource.Stop();

                if (Time.time <= inputTime + shortThreshold)
                {
                    morseCodeText.text += ".";
                } else
                {
                    morseCodeText.text += "-";
                }
            }
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, frequency1, sampleRate);

            if (channels == 2)
                data[i + 1] = CreateSine(timeIndex, frequency2, sampleRate);

            timeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
}
