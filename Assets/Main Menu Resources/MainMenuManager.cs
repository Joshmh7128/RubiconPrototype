using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button exitButton;
    public Button playButton;
    public Button optionsButton;
    public GameObject optionsContainer;
    public bool optionsActive;
    public bool clicked;

    private void Start()
    {
        optionsActive = false;   
    }

    private void Update()
    {
        
    }

    public void OptionsMenuToggle()
    {
        if (!clicked)
        clicked = true;
        // activate and deactivate the options menu
        optionsActive = !optionsActive;
        optionsContainer.SetActive(optionsActive);
    }

    public void StartPlay()
    {
        // load the next scene

    }

}
