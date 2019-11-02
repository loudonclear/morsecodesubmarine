using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private static int numAudioSources = 1;
    public AudioClip[] audioClips;

    private int activeAudioIndex = 0;

    AudioSource[] audioSources;


    void Start()
    {
        audioSources = new AudioSource[1];
        for (int i = 0; i < numAudioSources; i++)
        {
            AudioSource aud = gameObject.AddComponent<AudioSource>();
            aud.Stop();
            aud.clip = audioClips[0];
            audioSources[i] = aud;
        }
    }

    IEnumerator FadeAudio(AudioSource audioSource)
    {
        while (audioSource.volume > 0.001f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, 100f * Time.deltaTime);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }


    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource activeAudioSource = gameObject.GetComponents<AudioSource>()[activeAudioIndex];

            if (!activeAudioSource.isPlaying)
            {
                activeAudioSource.volume = 1;
                activeAudioSource.Play();
            } 
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            AudioSource activeAudioSource = gameObject.GetComponents<AudioSource>()[activeAudioIndex];
            StartCoroutine("FadeAudio", activeAudioSource);
            activeAudioIndex = (activeAudioIndex + 1) % numAudioSources;
        }
        
    }
}
