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
        player = Rewired.ReInput.players.GetPlayer(0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "MenuMouse") && (player.GetAxis("Click") > 0))
        {
            switch (buttonType)
            {
                case "options":
                    Debug.Log("options highlighted");
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
