using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ApplicationModel : MonoBehaviour
{
    public static float volume = 0.0f; // nullreferenceexception a sliderrel
    public static bool multiplayer = false;
    public static int difficulty = 2;
    public static int lastGameScore = 0;
    public static int numberOfPlayers = 0;
    public static List<string> chosenColors = new List<string>();
    public static AudioMixer audioMixer;
    public static bool debugMode = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public static void InitApplicationModel()
    {
        volume = 0.0f;
        multiplayer = false;
        difficulty = 2;
        numberOfPlayers = 0;
        chosenColors = new List<string>();
    }
    public static void SetAudioMixer()
    {
        if (null != GameObject.Find("Canvas").GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer)
        {
            audioMixer = GameObject.Find("Canvas").GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        }
        else
            Debug.Log("AudioMixer didnt set");
    }
    public static void toggleDebugMode()
    {
        debugMode = !debugMode;
    }
    
}
