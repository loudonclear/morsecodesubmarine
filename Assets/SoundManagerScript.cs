using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip monsterClipSound;
    static AudioSource audioSrc;
    
    // Start is called before the first frame update
    void Start()
    {
        monsterClipSound = Resources.Load<AudioClip>("monsterClip");
        audioSrc.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "monster":
                audioSrc.PlayOneShot(monsterClipSound);
                break;
            default:
                break;
        }
    }
}
