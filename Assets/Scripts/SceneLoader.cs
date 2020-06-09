using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Rewired;

public class SceneLoader : MonoBehaviour
{
    public Image progressBar;
    public Text tipText;
    public GameObject loadingCanvas;
    private Camera myCam;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private bool startedCutscene = false;
    private AsyncOperation gameLevel;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        SetupCutscene();
        StartCoroutine(DisplayTip());
        StartCoroutine(LoadAsyncOperation());
        videoPlayer.loopPointReached += EndReached;
    }

    private void Update()
    {
        if(startedCutscene)
        {
            
            for (int i = 0; i < ReInput.players.playerCount; i++)
            {
                if (ReInput.players.GetPlayer(i).GetButton("FireTrigger"))
                {
                    videoPlayer.Stop();
                    gameLevel.allowSceneActivation = true;
                    break;
                }
            }
            
            if(Input.GetKey(KeyCode.Space))
            {
                videoPlayer.Stop();
                gameLevel.allowSceneActivation = true;
            }
            
            /*
            if (Input.GetKeyDown("joystick button 0"))
            {
                videoPlayer.Stop();
            }
            */
            
            if (!videoPlayer.isPlaying)
            {
                gameLevel.allowSceneActivation = true;
            }
            
        }
    }

    private void SetupCutscene()
    {
        videoPlayer = myCam.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "Intro.mp4");
        videoPlayer.Prepare();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        gameLevel.allowSceneActivation = true;
    }

    private string RandomTip()
    {
        string tip = "Random Tip";
        int key = Random.Range(1, 38);
        switch(key)
        {
            case 1:
                tip = "Use the right and left bumpers to ascend and descend.";
                break;
            case 2:
                tip = "You can pause the game by pressing the Start or Menu button on your controller.";
                break;
            case 3:
                tip = "The 'Supercharge' modifier increases the speed, size, and damage of your bullets.";
                break;
            case 4:
                tip = "With the 'Vampirism' modifier, you steal a little bit of your opponent's health with each hit.";
                break;
            case 5:
                tip = "Yellow power-ups allow you to briefly see an indicator of your opponents' locations through walls.";
                break;
            case 6:
                tip = "Blue power-ups give you armor, providing limited protection from attacks.";
                break;
            case 7:
                tip = "Red power-ups restore health if you activate them while injured.";
                break;
            case 8:
                tip = "Pickups respawn after a cooldown; use their timing to your advantage.";
                break;
            case 9:
                tip = "If your opponent has the 'Shield' modifier, try attacking them from the sides or back.";
                break;
            case 10:
                tip = "If your opponent has the 'Tracking' modifier, you'll be able to follow the glowing trail they leave behind.";
                break;
            case 11:
                tip = "Sniper rifles are accurate, powerful long-range weapons, at the cost of a lower fire rate and small magazine.";
                break;
            case 12:
                tip = "Shotguns pack a punch up close, but their spread makes them unreliable at long range, because this is a video game.";
                break;
            case 13:
                tip = "Orange cubes throughout the arena are destructible-- take cover behind them at your own risk!";
                break;
            case 14:
                tip = "Every match has a random configuration of rooms, so stop trying to memorize the map layout. Hey! I said stop!";
                break;
            case 15:
                tip = "The arena's center room is the only area that never rotates. Use it to get your bearings.";
                break;
            case 16:
                tip = "Holding down the trigger auto-fires your weapon. Any weapon.";
                break;
            case 17:
                tip = "Disoriented by the six-sided omnidirectional puzzle cube? Check your gyroscope at the bottom center of your HUD.";
                break;
            case 18:
                tip = "The Machine Gun has a very high rate of fire, so keep an eye on your ammo so you don't have to reload in the middle of a firefight.";
                break;
            case 19:
                tip = "The Grenade Launcher is the only weapon whose projectiles are affected by gravity, so lead your shots accordingly.";
                break;
            case 20:
                tip = "Missile Launchers launch missiles, and those missiles are scary. The shock of their impact can disorient you if you're too close.";
                break;
            case 21:
                tip = "Blasters are reliable, moderately accurate, quick-firing sidearms. Please note that dual wielding is outlawed in the Rubicon.";
                break;
            case 22:
                tip = "TODD: Please input the rest of the loading screen tips by tomorrow morning at 7.";
                break;
            case 23:
                tip = "RUBICON HENTAI-- wait this isn't Google oh sweet lord how do I delete";
                break;
            case 24:
                tip = "Deal 69 damage to a certain object in the arena for a secret surprise.";
                break;
            case 25:
                tip = "The cube was invented in 1807 by Phineas T. Cube when he accidentally glued two pyramids together.";
                break;
            case 26:
                tip = "If you see a sphere, please stop the match and report it to the Announcer immediately.";
                break;
            case 27:
                tip = "Losing a match? Try shooting your opponent more than they shoot you.";
                break;
            case 28:
                tip = "NOTE TO HR: Todd has been exhibiting sphere-like behavior. Recommend surveillance.";
                break;
            case 29:
                tip = "Dogs are great. They can play Rubicon too, just not very well. Please remember to pet your dog, if you have one.";
                break;
            case 30:
                tip = "It's totally okay to prioritize your own health and wellbeing. You matter.";
                break;
            case 31:
                tip = "The Announcer is standing right behind you, shouting into your ears. Please tip him after each match.";
                break;
            case 32:
                tip = "Regardless of what you may have seen on the news, the 'Players' are just drones, so it's okay to have fun shooting them.";
                break;
            case 33:
                tip = "The player cubes are grown in vats and they're alive and bred to kill and they can feel pain oh god it hurts";
                break;
            case 34:
                tip = "The 'Glowing' modifier surrounds you with a bright halo, making you much easier to pinpoint. That's a bad thing.";
                break;
            case 35:
                tip = "With the 'Scatter' modifier, you fire three times as many bullets with each trigger pull, but your accuracy is decreased.";
                break;
            case 36:
                tip = "The 'Stealth' modifier lets you blend in with the environment, making you much harder to spot. Use it to spring an ambush.";
                break;
            case 37:
                tip = "The 'Large' modifier makes you present a bigger target, plus you might struggle with some tight squeezes.";
                break;
        }
        return tip;
    }

    IEnumerator LoadAsyncOperation()
    {
        tipText.text = RandomTip();
        yield return new WaitForSeconds(0.5f);
        if (WhichScene.SceneToLoad != null)
        {
            gameLevel = SceneManager.LoadSceneAsync(WhichScene.SceneToLoad);
            gameLevel.allowSceneActivation = false;

            while (gameLevel.progress < 0.8999)
            {
                progressBar.fillAmount = gameLevel.progress;
                yield return new WaitForEndOfFrame();
            }

            loadingCanvas.SetActive(false);
            videoPlayer.Play();
            startedCutscene = true;
        }
    }

    IEnumerator DisplayTip()
    {
        tipText.text = RandomTip();
        while(true)
        {
            yield return new WaitForSeconds(5);
            tipText.text = RandomTip();
        }
    }
}
