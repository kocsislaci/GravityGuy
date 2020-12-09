using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();

    private void Start()
    {
        ApplicationModel.InitApplicationModel();
        ApplicationModel.SetAudioMixer();

        Resolution[] tempResolutions = Screen.resolutions;
        int middleIndex = Mathf.RoundToInt(tempResolutions.Length / 2);
        for(int i = 0; i < tempResolutions.Length; i++)
            resolutions.Add(tempResolutions[i]);
    }
    public void PlayGameFromSinglePlayer()
    {
        SceneManager.LoadScene("ClassicGameScene");
    }
    public void PlayGameFromMultiplayer()
    {
        if (ApplicationModel.chosenColors.Count > 1)
        {
            SceneManager.LoadScene("ClassicGameScene");
        }
        else
        {

        }
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void setVolume(float volume)
    {
        ApplicationModel.volume = volume;
        if (ApplicationModel.audioMixer != null)
            ApplicationModel.audioMixer.SetFloat("volume", volume);
        else
            Debug.Log("Audiomixer volume didnt set");
    }
    public void setMultiplayer()
    {
        ApplicationModel.multiplayer = true;
    }
    public void resetMultiplayer()
    {
        ApplicationModel.multiplayer = false;
    }
    public void setDifficulty(int difficulty)
    {
        ApplicationModel.difficulty = difficulty;
        Debug.Log(string.Format("difficulty set to {0}", difficulty));
    }
    public void toggleChosenColor(string color)
    {
        if (ApplicationModel.chosenColors.Contains(color))
        {
            deleteChosenColors(color);
            ApplicationModel.numberOfPlayers--;
        }
        else
        {
            addChosenColors(color);
            ApplicationModel.numberOfPlayers++;
        }
    }
    private void addChosenColors(string color)
    {
        ApplicationModel.chosenColors.Add(color);
    }
    private void deleteChosenColors(string color)
    {
        ApplicationModel.chosenColors.Remove(color);
    }
}
