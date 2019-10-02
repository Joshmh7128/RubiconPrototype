using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button exitButton;
    public Button playButton;
    public Button optionsButton;
    public Button fullscreenToggle;
    public Button resetAudioButton;
    public GameObject optionsContainer;
    public bool optionsActive;
    public Slider masterVol;
    public Slider sfxVol;
    public Slider announcerVol;
    public Slider musicVol;
    public AudioValues audioValues;
    public Text masterVolDisplay;
    public Text sfxVolDisplay;
    public Text announcerVolDisplay;
    public Text musicVolDisplay;

    private void Start()
    {
        optionsActive = false;
        optionsButton.onClick.AddListener(OptionsMenuToggle);
        playButton.onClick.AddListener(LoadLevel);
        exitButton.onClick.AddListener(EndGame);
        fullscreenToggle.onClick.AddListener(ToggleFullscreen);
        resetAudioButton.onClick.AddListener(ResetAudio);

        audioValues.masterVolume = (int)masterVol.value;
        audioValues.sfxVolume = (int)sfxVol.value;
        audioValues.announcerVolume = (int)announcerVol.value;
        audioValues.musicVolume = (int)musicVol.value;
    }

    private void Update()
    {
        audioValues.masterVolume = (int)masterVol.value;
        audioValues.sfxVolume = (int)sfxVol.value;
        audioValues.announcerVolume = (int)announcerVol.value;
        audioValues.musicVolume = (int)musicVol.value;

        masterVolDisplay.text = audioValues.masterVolume.ToString();
        sfxVolDisplay.text = audioValues.sfxVolume.ToString();
        announcerVolDisplay.text = audioValues.announcerVolume.ToString();
        musicVolDisplay.text = audioValues.musicVolume.ToString();
    }

    public void ResetAudio()
    {
        audioValues.masterVolume = 75;
        masterVol.value = 75;
        audioValues.sfxVolume = 50;
        sfxVol.value = 50;
        audioValues.announcerVolume = 75;
        announcerVol.value = 75;
        audioValues.musicVolume = 80;
        musicVol.value = 60;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("CoreMods");
    }

    public void OptionsMenuToggle()
    {
        // activate and deactivate the options menu
        optionsActive = !optionsActive;
        optionsContainer.SetActive(optionsActive);
    }

    public void StartPlay()
    {
        // load the next scene

    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen toggled");
    }
}
