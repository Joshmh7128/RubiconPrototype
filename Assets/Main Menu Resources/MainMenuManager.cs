﻿using System.Collections;
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
    public SoundManager soundManager;

    private void Start()
    {
        // on click listeners
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        optionsActive = false;
        optionsButton.onClick.AddListener(OptionsMenuToggle);
        playButton.onClick.AddListener(LoadLevel);
        exitButton.onClick.AddListener(EndGame);
        fullscreenToggle.onClick.AddListener(ToggleFullscreen);
        resetAudioButton.onClick.AddListener(ResetAudio);

        // add the hover checks

        audioValues.masterVolume = (int)masterVol.value;
        audioValues.sfxVolume = (int)sfxVol.value;
        audioValues.announcerVolume = (int)announcerVol.value;
        audioValues.musicVolume = (int)musicVol.value;

        // get our sound manager
        soundManager.PlaySound("menuMusic");
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
        audioValues.masterVolume = 10;
        masterVol.value = 10;
        audioValues.sfxVolume = 80;
        sfxVol.value = 80;
        audioValues.announcerVolume = 80;
        announcerVol.value = 80;
        audioValues.musicVolume = 100;
        musicVol.value = 100;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        WhichScene.SceneToLoad = "GameplayBase";
        SceneManager.LoadScene("LoadingScene");
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
