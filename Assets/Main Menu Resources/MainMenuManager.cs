using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuManager : MonoBehaviour
{
    [Header("Joining")]
    public int joined = 0;
    public bool players2readyToJoin = false;
    public bool players4readyToJoin = false;
    public int[] players2joined;
    public int[] players4joined;

    public Button exitButton;
    public Button playButton;
    public Button optionsButton;
    public Button fullscreenToggle;
    public Button resetAudioButton;
    public Button closeOptions;
    public Button pButton;
    public Button pBackButton;
    public Button twoJoin;
    public Button fourJoin;
    public Button backToMode;
    public Button startGame;
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
    public PostProcessVolume post;
    DepthOfField depthOfField = null;
    public GameObject[] modeMenu;
    public GameObject[] toHide;
    public Camera uicam;
    public GameObject[] hideWhenOptions;
    public GameObject[] hideWhenJoin;
    public GameObject[] showJoin2p;
    public GameObject[] showJoin4p;

    private void Start()
    {
        post.profile.TryGetSettings(out depthOfField);
        foreach (GameObject obj in modeMenu)
        {
            obj.SetActive(false);
        }
        uicam.depth = 0;

        // on click listeners
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        optionsActive = false;
        optionsButton.onClick.AddListener(OptionsMenuToggle);
        closeOptions.onClick.AddListener(OptionsMenuToggle);
        pBackButton.onClick.AddListener(ReturnToMenu);
        playButton.onClick.AddListener(ModeSelect);
        exitButton.onClick.AddListener(EndGame);
        twoJoin.onClick.AddListener(Join2p);
        fourJoin.onClick.AddListener(Join4p);
        startGame.onClick.AddListener(LoadLevel);
        backToMode.onClick.AddListener(ModeSelect);
        fullscreenToggle.onClick.AddListener(ToggleFullscreen);
        resetAudioButton.onClick.AddListener(ResetAudio);

        // add the hover checks

        audioValues.masterVolume = (int)masterVol.value;
        audioValues.sfxVolume = (int)sfxVol.value;
        audioValues.announcerVolume = (int)announcerVol.value;
        audioValues.musicVolume = (int)musicVol.value;

        // get our sound manager
        soundManager.PlaySound("menuMusic");

        resetJoined();
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

    public void resetJoined()
    {
        players2joined = new int[2];
        players4joined = new int[4];

        players2joined[0] = 0;
        players2joined[1] = 0;

        players4joined[0] = 0;
        players4joined[1] = 0;
        players4joined[2] = 0;
        players4joined[3] = 0;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void OptionsMenuToggle()
    {
        // activate and deactivate the options menu
        optionsActive = !optionsActive;
        optionsContainer.SetActive(optionsActive);
        foreach(GameObject obj in hideWhenOptions)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
        if(optionsActive)
        {
            resetAudioButton.Select();
        }
        else
        {
            optionsButton.Select();
        }
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

    public void ModeSelect()
    {
        uicam.depth = 1;
        depthOfField.active = true;
        foreach(GameObject obj in toHide)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in modeMenu)
        {
            obj.SetActive(true);
        }
        pButton.Select();
        foreach (GameObject obj in showJoin2p)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in showJoin4p)
        {
            obj.SetActive(false);
        }
        resetJoined();
        players2readyToJoin = false;
        players4readyToJoin = false;
    }

    public void ReturnToMenu()
    {
        uicam.depth = 0;
        depthOfField.active = false;
        foreach (GameObject obj in toHide)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in modeMenu)
        {
            obj.SetActive(false);
        }
        playButton.Select();
    }

    public void Join2p()
    {
        WhichScene.SceneToLoad = "GameplayBase";
        foreach(GameObject obj in hideWhenJoin)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in showJoin2p)
        {
            obj.SetActive(true);
        }
        backToMode.Select();
        startGame.interactable = false;
        players2readyToJoin = true;
    }

    public void Join4p()
    {
        WhichScene.SceneToLoad = "GameplayBase4p";
        foreach (GameObject obj in hideWhenJoin)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in showJoin4p)
        {
            obj.SetActive(true);
        }
        backToMode.Select();
        startGame.interactable = false;
        players4readyToJoin = true;
    }
}
