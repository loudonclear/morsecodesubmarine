using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CommandInterpreter))]
public class MorseCodeController : MonoBehaviour
{
    public Text morseCodeText;
    public Text decodedText;

    public float timeUnit = 0.2f;
    public int unitsBetweenCharacters = 3;
    public int unitsBetweenCommands = 7;

    public bool clearDecode = true;

    [Range(1, 20000)]
    public float frequency1 = 500;

    [Range(1, 20000)]
    public float frequency2 = 500;

    private float sampleRate;
    public float waveLengthInSeconds = 5.0f;

    private AudioSource audioSource;
    
    int timeIndex = 0;
    private float inputTime;
    private float pauseTime;
    private bool typed = false;

    private CommandInterpreter commandInterpreter;

    private static string dot = ".";
    private static string dash = "-";

    Dictionary<string, char> decoder = new Dictionary<string, char>() {
        {string.Concat(dot, dash), 'a'},
        {string.Concat(dash, dot, dot, dot), 'b'},
        {string.Concat(dash, dot, dash, dot), 'c'},
        {string.Concat(dash, dot, dot), 'd'},
        {dot.ToString(), 'e'},
        {string.Concat(dot, dot, dash, dot), 'f'},
        {string.Concat(dash, dash, dot), 'g'},
        {string.Concat(dot, dot, dot, dot), 'h'},
        {string.Concat(dot, dot), 'i'},
        {string.Concat(dot, dash, dash, dash), 'j'},
        {string.Concat(dash, dot, dash), 'k'},
        {string.Concat(dot, dash, dot, dot), 'l'},
        {string.Concat(dash, dash), 'm'},
        {string.Concat(dash, dot), 'n'},
        {string.Concat(dash, dash, dash), 'o'},
        {string.Concat(dot, dash, dash, dot), 'p'},
        {string.Concat(dash, dash, dot, dash), 'q'},
        {string.Concat(dot, dash, dot), 'r'},
        {string.Concat(dot, dot, dot), 's'},
        {string.Concat(dash), 't'},
        {string.Concat(dot, dot, dash), 'u'},
        {string.Concat(dot, dot, dot, dash), 'v'},
        {string.Concat(dot, dash, dash), 'w'},
        {string.Concat(dash, dot, dot, dash), 'x'},
        {string.Concat(dash, dot, dash, dash),'y'},
        {string.Concat(dash, dash, dot, dot), 'z'},
        {string.Concat(dash, dash, dash, dash, dash),'0'},
        {string.Concat(dot, dash, dash, dash, dash), '1'},
        {string.Concat(dot, dot, dash, dash, dash), '2'},
        {string.Concat(dot, dot, dot, dash, dash), '3'},
        {string.Concat(dot, dot, dot, dot, dash), '4'},
        {string.Concat(dot, dot, dot, dot, dot), '5'},
        {string.Concat(dash, dot, dot, dot, dot), '6'},
        {string.Concat(dash, dash, dot, dot, dot), '7'},
        {string.Concat(dash, dash, dash, dot, dot), '8'},
        {string.Concat(dash, dash, dash, dash, dot), '9'}
    };

    void Start()
    {
        commandInterpreter = GetComponent<CommandInterpreter>(); 
        morseCodeText.text = "";
        decodedText.text = "";

        sampleRate = AudioSettings.outputSampleRate;

        audioSource = gameObject.GetComponent<AudioSource>();
        if (GameObject.FindGameObjectWithTag("GameManager") != null)
        {
            gameManager managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
            audioSource.volume = managerScript.volume;
        } else
        {
            audioSource.volume = 1;
        }
        
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

            pauseTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (audioSource.isPlaying)
            {
                //timeIndex = 0;  //resets timer before playing sound
                audioSource.Stop();

                if (morseCodeText.text.Length < 5)
                {
                    if (Time.time <= inputTime + timeUnit)
                    {
                        morseCodeText.text += dot;
                    }
                    else
                    {
                        morseCodeText.text += dash;
                    }
                }
                
                pauseTime = Time.time;
                typed = true;
            }
        }

        if (typed && Time.time > pauseTime + unitsBetweenCharacters * timeUnit && Time.time <= pauseTime + unitsBetweenCommands * timeUnit)
        {
            // New character
            char c = '?';
            if (decoder.TryGetValue(morseCodeText.text, out c))
            {
                morseCodeText.text = "";
                decodedText.text += c.ToString().ToUpper();
            } else
            {
                morseCodeText.text = "";
                decodedText.text += "?";
            }
            
            typed = false;
            
        } else if (Time.time > pauseTime + unitsBetweenCommands * timeUnit)
        {
            // Space between words
            commandInterpreter.InterpretCommand(decodedText.text);

            typed = false;
            if (clearDecode)
            {
                decodedText.text = "";
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
