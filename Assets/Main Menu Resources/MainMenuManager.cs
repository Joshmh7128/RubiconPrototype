using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Header("Joining")]
    public bool players2readyToJoin = false;
    public bool players4readyToJoin = false;
    public int[] players2joined;
    public int[] players4joined;

    [Header("Player Previews")]
    public GameObject[] players2press;
    public GameObject[] players4press;
    public GameObject[] players2preview;
    public GameObject[] players4preview;
    public GameObject[] miniPlayers;

    [Header("Players")]
    public Rewired.Player player1;
    public Rewired.Player player2;
    public Rewired.Player player3;
    public Rewired.Player player4;

    public Button exitButton;
    public Button playButton;
    public Button tutorialButton;
    public Button optionsButton;
    public Button creditsButton;
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
    public GameObject tutorialContainer;
    public bool optionsActive;
    public bool tutorialActive;
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
    public GameObject[] hideWhenTutorial;
    public GameObject[] hideWhenJoin;
    public GameObject[] showJoin2p;
    public GameObject[] showJoin4p;
    public GameObject creditsPage;
    public Button creditsBack;
    private AudioSource joinSource;
    public AudioClip join;
    public AudioClip leave;
    public GameObject fade;
    public Button tutorialBack;
    private int tutorialIndex = 0;
    public GameObject[] tutorialSlides;

    private void Start()
    {
        FindPlayers();

        ResetAudio();
        if(AudioLevels.firstLoad)
        {
            ResetDefaults();
        }

        post.profile.TryGetSettings(out depthOfField);
        foreach (GameObject obj in modeMenu)
        {
            obj.SetActive(false);
        }
        uicam.depth = 0;

        // on click listeners
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        optionsActive = false;
        tutorialActive = false;
        optionsButton.onClick.AddListener(OptionsMenuToggle);
        tutorialButton.onClick.AddListener(ToggleTutorial);
        creditsButton.onClick.AddListener(ShowCredits);
        creditsBack.onClick.AddListener(ReturnToMenu);
        closeOptions.onClick.AddListener(OptionsMenuToggle);
        pBackButton.onClick.AddListener(ReturnToMenu);
        playButton.onClick.AddListener(ModeSelect);
        exitButton.onClick.AddListener(EndGame);
        twoJoin.onClick.AddListener(Join2p);
        fourJoin.onClick.AddListener(Join4p);
        startGame.onClick.AddListener(LoadLevel);
        backToMode.onClick.AddListener(ModeSelect);
        fullscreenToggle.onClick.AddListener(ToggleFullscreen);
        resetAudioButton.onClick.AddListener(ResetDefaults);
        tutorialBack.onClick.AddListener(ToggleTutorial);

        // add the hover checks

        audioValues.masterVolume = (int)masterVol.value;
        audioValues.sfxVolume = (int)sfxVol.value;
        audioValues.announcerVolume = (int)announcerVol.value;
        audioValues.musicVolume = (int)musicVol.value;

        // get our sound manager
        soundManager.PlaySound("menuMusic");

        resetJoined();

        StartCoroutine("AddPlaySound");
        joinSource = soundManager.beeperMain;
        fade.SetActive(true);
    }

    private void FindPlayers()
    {
        player1 = Rewired.ReInput.players.GetPlayer(0);
        player2 = Rewired.ReInput.players.GetPlayer(1);
        player3 = Rewired.ReInput.players.GetPlayer(2);
        player4 = Rewired.ReInput.players.GetPlayer(3);
    }

    IEnumerator AddPlaySound()
    {
        EventTrigger et = playButton.GetComponent<EventTrigger>();
        et.enabled = false;
        yield return new WaitForSeconds(0.1f);
        et.enabled = true;
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

        if(players2readyToJoin)
        {
            if(player1.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(players2joined[0] == 0)
                {
                    players2joined[0] = 1;
                    players2press[0].SetActive(false);
                    players2preview[0].SetActive(true);
                    miniPlayers[0].SetActive(true);
                    checkJoined();
                    joinSource.PlayOneShot(join);
                }
                else
                {
                    players2joined[0] = 0;
                    players2press[0].SetActive(true);
                    players2preview[0].SetActive(false);
                    miniPlayers[0].SetActive(false);
                    checkJoined();
                    joinSource.PlayOneShot(leave);
                }
            }
            if (player2.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (players2joined[1] == 0)
                {
                    players2joined[1] = 1;
                    players2press[1].SetActive(false);
                    players2preview[1].SetActive(true);
                    miniPlayers[1].SetActive(true);
                    checkJoined();
                    joinSource.PlayOneShot(join);
                }
                else
                {
                    players2joined[1] = 0;
                    players2press[1].SetActive(true);
                    players2preview[1].SetActive(false);
                    miniPlayers[1].SetActive(false);
                    checkJoined();
                    joinSource.PlayOneShot(leave);
                }
            }
        }
        else if (players4readyToJoin)
        {
            if (player1.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (players4joined[0] == 0)
                {
                    players4joined[0] = 1;
                    players4press[0].SetActive(false);
                    players4preview[0].SetActive(true);
                    miniPlayers[0].SetActive(true);
                    checkJoined();
                    joinSource.PlayOneShot(join);
                }
                else
                {
                    players4joined[0] = 0;
                    players4press[0].SetActive(true);
                    players4preview[0].SetActive(false);
                    miniPlayers[0].SetActive(false);
                    checkJoined();
                    joinSource.PlayOneShot(leave);
                }
            }
            if (player2.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (players4joined[1] == 0)
                {
                    players4joined[1] = 1;
                    players4press[1].SetActive(false);
                    players4preview[1].SetActive(true);
                    miniPlayers[1].SetActive(true);
                    checkJoined();
                    joinSource.PlayOneShot(join);
                }
                else
                {
                    players4joined[1] = 0;
                    players4press[1].SetActive(true);
                    players4preview[1].SetActive(false);
                    miniPlayers[1].SetActive(false);
                    checkJoined();
                    joinSource.PlayOneShot(leave);
                }
            }
            if (player3.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (players4joined[2] == 0)
                {
                    players4joined[2] = 1;
                    players4press[2].SetActive(false);
                    players4preview[2].SetActive(true);
                    miniPlayers[2].SetActive(true);
                    checkJoined();
                    joinSource.PlayOneShot(join);
                }
                else
                {
                    players4joined[2] = 0;
                    players4press[2].SetActive(true);
                    players4preview[2].SetActive(false);
                    miniPlayers[2].SetActive(false);
                    checkJoined();
                    joinSource.PlayOneShot(leave);
                }
            }
            if (player4.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (players4joined[3] == 0)
                {
                    players4joined[3] = 1;
                    players4press[3].SetActive(false);
                    players4preview[3].SetActive(true);
                    miniPlayers[3].SetActive(true);
                    checkJoined();
                    joinSource.PlayOneShot(join);
                }
                else
                {
                    players4joined[3] = 0;
                    players4press[3].SetActive(true);
                    players4preview[3].SetActive(false);
                    miniPlayers[3].SetActive(false);
                    checkJoined();
                    joinSource.PlayOneShot(leave);
                }
            }
        }

    }

    private void checkJoined()
    {
        if(players2readyToJoin)
        {
            if(players2joined[0] + players2joined[1] >= 2)
            {
                startGame.interactable = true;
            }
            else
            {
                startGame.interactable = false;
                backToMode.Select();
            }
        }
        else if(players4readyToJoin)
        {
            if(players4joined[0] + players4joined[1] + players4joined[2] + players4joined[3] >= 4)
            {
                startGame.interactable = true;
            }
            else
            {
                startGame.interactable = false;
                backToMode.Select();
            }
        }
    }

    public void ResetDefaults()
    {
        masterVol.value = (70);
        audioValues.SetMaster(70);
        sfxVol.value = (80);
        audioValues.SetSFX(AudioLevels.sfxVol + 80);
        announcerVol.value = (85);
        audioValues.SetMatt(85);
        musicVol.value = (80);
        audioValues.SetMusic(80);
    }

    public void ResetAudio()
    {
        /*
        audioValues.masterVolume = 70;
        masterVol.value = 70;
        audioValues.SetMaster(70);

        audioValues.sfxVolume = 80;
        sfxVol.value = 80;
        audioValues.SetSFX(80);

        audioValues.announcerVolume = 85;
        announcerVol.value = 85;
        audioValues.SetMatt(85);

        audioValues.musicVolume = 80;
        musicVol.value = 80;
        audioValues.SetMusic(80);
        */
        masterVol.value = (int)(AudioLevels.masterVol + 80);
        audioValues.SetMaster(AudioLevels.masterVol + 80);
        sfxVol.value = (int)(AudioLevels.sfxVol + 80);
        audioValues.SetSFX(AudioLevels.sfxVol + 80);
        announcerVol.value = (int)(AudioLevels.announcerVol + 80);
        audioValues.SetMatt(AudioLevels.announcerVol + 80);
        musicVol.value = (int)(AudioLevels.musicVol + 80);
        audioValues.SetMusic(AudioLevels.musicVol + 80);
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

        if(players2readyToJoin)
        {
            foreach (GameObject obj in players2press)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in players2preview)
            {
                obj.SetActive(false);
            }
        }
        else if(players4readyToJoin)
        {
            foreach (GameObject obj in players4press)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in players4preview)
            {
                obj.SetActive(false);
            }
        }
        
        players2readyToJoin = false;
        players4readyToJoin = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ToggleTutorial()
    {
        tutorialActive = !tutorialActive;
        tutorialContainer.SetActive(tutorialActive);
        if(tutorialActive)
        {
            foreach (GameObject obj in hideWhenTutorial)
            {
                obj.SetActive(false);
            }
            uicam.depth = 1;
            depthOfField.active = true;
        }
        else
        {
            uicam.depth = 0;
            depthOfField.active = false;
            foreach (GameObject obj in hideWhenTutorial)
            {
                obj.SetActive(!obj.activeInHierarchy);
            }
        }
        if(tutorialActive)
        {
            tutorialBack.Select();
        }
        else
        {
            tutorialButton.Select();
        }
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

    public void ShowCredits()
    {
        uicam.depth = 1;
        depthOfField.active = true;
        foreach (GameObject obj in toHide)
        {
            obj.SetActive(false);
        }
        creditsPage.SetActive(true);
        creditsBack.Select();
    }

    public void StartPlay()
    {
        // load the next scene

    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
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
        if(players2readyToJoin)
        {
            foreach (GameObject obj in showJoin2p)
            {
                obj.SetActive(false);
            }
        }
        else if (players4readyToJoin)
        {
            foreach (GameObject obj in showJoin4p)
            {
                obj.SetActive(false);
            }
        }
        resetJoined();
        
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
        creditsPage.SetActive(false);
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
