using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerClick : MonoBehaviour
{
    public MainMenuManager mainMenuManager;
    public string buttonType;
    private Rewired.Player player;

    private void Start()
    {
        if(Rewired.ReInput.players.playerCount > 0)
        {
            player = Rewired.ReInput.players.GetPlayer(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "MenuMouse") && (player.GetButton("Click")))
        {
            switch (buttonType)
            {
                case "options":
                    mainMenuManager.OptionsMenuToggle();
                    break;

                case "play":
                    mainMenuManager.StartPlay();
                    break;

                case "exit":
                    Application.Quit();
                    break;
            }
        }
    }
}
