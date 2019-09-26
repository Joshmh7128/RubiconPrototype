using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    /*
     * this script handles:
     * Game score, scene items, both player tracking
     * battle modifiers, and round modifiers
     */

    // all variable definitions
    #region
    [Header("Score")]

    public int Player1Kills = 0;
    public int Player2Kills = 0;

    public Text Player1Score;
    public Text Player2Score;

    public int Player1RoundsWon = 0;
    public int Player2RoundsWon = 0;

    public Text Player1Rounds;
    public Text Player2Rounds;

    public int roundNum = 1; // needed to make public
    public Text RoundCounter;

    [Header("Scene")]

    public GameObject Player1;
    public GameObject Player2;

    public GameObject Player1Cam;
    public GameObject Player2Cam;

    public GameObject Player1Canvas;
    public GameObject Player2Canvas;

    public GameObject RoundCanvas;
    public GameObject InterRound;
    public GameObject MatchEnd;

    public CubeAction Rotator;

    public Transform SpawnTop;
    public Transform SpawnBottom;

    public SoundManager soundManager;

    private int needed = 3;

    public CubeAction myArena;
    private int shuffles = 20;

    private int downtime = 5;

    [Header("Modifiers")]

    public string[] weaponList;

    public ModApplication ma1;
    public ModApplication ma2;

    public ModDisplay md;

    // what mods do we have?
    public enum battleMods
    {
        none, Armor, Regen, Scatter, Shield, Speed, Stealth, Supercharge, Vampirism, Glowing, Large, Tracking, TunnelVision
    }

    // what mods do the players have? // remember to set the length of these before using them so we don't have to recreate them over and over
    public int[] player1Mods = new int[4];
    public int[] player2Mods = new int[4];
    public int[] player3Mods = new int[4];
    public int[] player4Mods = new int[4];

    public int newMod;

    #endregion

    // start

    private void Start()
    {
        GenerateWeapons();
        myArena.shuffle(shuffles);
        StartCoroutine("SetupRound");
    }

    // weapon generation

    public void GenerateWeapons()
    {
        string[] weapons = new string[5];
        for(int i = 0; i <= 4; i++)
        {
            bool original = false;
            while(original == false)
            {
                int key = Random.Range(1, 7);
                if (key == 1)
                {
                    weapons[i] = "Blasters";
                }
                else if (key == 2)
                {
                    weapons[i] = "Shotguns";
                }
                else if (key == 3)
                {
                    weapons[i] = "Sniper Rifles";
                }
                else if (key == 4)
                {
                    weapons[i] = "Machine Guns";
                }
                else if (key == 5)
                {
                    weapons[i] = "Missile Launchers";
                }
                else if (key == 6)
                {
                    weapons[i] = "Grenade Launchers";
                }

                original = true;

                for (int x = 0; x < i; x++)
                {
                    if (weapons[i] == weapons[x])
                    {
                        original = false;
                    }
                }
            }
        }
        weaponList = weapons;
    }

    // score update

    public void updateScore(int loser)
    {
        if(loser == 1)
        {
            Player2Kills++;
            Player2Score.text = Player2Kills.ToString();
        }
        else if(loser == 2)
        {
            Player1Kills++;
            Player1Score.text = Player1Kills.ToString();
        }

        Rotator.live = false;
        StartCoroutine(PlayerDeath(loser));
    }

    // round end coroutine

    private void EndMatch(int winner)
    {
        RoundCanvas.SetActive(false);
        Player1.GetComponent<InfoTracker>().Hide();
        Player2.GetComponent<InfoTracker>().Hide();
        MatchEnd.transform.Find("TopText").GetComponent<Text>().text = "Player " + winner.ToString() + " is the Champion";
        MatchEnd.transform.Find("P1").GetComponent<Text>().text = Player1RoundsWon.ToString();
        MatchEnd.transform.Find("P2").GetComponent<Text>().text = Player2RoundsWon.ToString();
        MatchEnd.SetActive(true);
    }

    // full reset and next round start and setup
    #region

    public void resetScore()
    {
        Player1Kills = 0;
        Player2Kills = 0;
        Player1Score.text = "0";
        Player2Score.text = "0";

    }

    public void resetPlayers(int id)
    {
        if (id == 1)
        {
            Player1Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
            Player2Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;

            Player1Cam.GetComponent<PlayerController>().enabled = true;
            Player1.GetComponent<Rigidbody>().useGravity = false;
            Player1Cam.transform.SetParent(null);
            Player1.GetComponent<InfoTracker>().ResetStats();
            Player2.GetComponent<InfoTracker>().ResetStats();
        }

        else if (id == 2)
        {
            Player1Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
            Player2Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;

            Player2Cam.GetComponent<PlayerController>().enabled = true;
            Player2.GetComponent<Rigidbody>().useGravity = false;
            Player2Cam.transform.SetParent(null);
            Player1.GetComponent<InfoTracker>().ResetStats();
            Player2.GetComponent<InfoTracker>().ResetStats();
        }

        StartCoroutine(SetupRound());
        Rotator.live = true;
    }

    private IEnumerator NextRound(int loser)
    {
        if(loser == 1)
        {
            Player2RoundsWon++;
            Player2Rounds.text = Player2RoundsWon.ToString();
        }
        else if(loser == 2)
        {
            Player1RoundsWon++;
            Player1Rounds.text = Player1RoundsWon.ToString();
        }
        ma1.ResetMods();
        ma2.ResetMods();

        if(Player1RoundsWon < 3 && Player2RoundsWon < 3)
        {
            RoundCanvas.SetActive(false);
            Player1.GetComponent<InfoTracker>().Hide();
            Player2.GetComponent<InfoTracker>().Hide();
            InterRound.SetActive(true);
            InterRound.transform.Find("TopText").GetComponent<Text>().text = "Round " + roundNum.ToString() + " Complete";
            InterRound.transform.Find("P1").GetComponent<Text>().text = Player1RoundsWon.ToString();
            InterRound.transform.Find("P2").GetComponent<Text>().text = Player2RoundsWon.ToString();
            InterRound.transform.Find("WeaponText").GetComponent<Text>().text = weaponList[roundNum].ToString();

            yield return new WaitForSeconds(10);

            RoundCanvas.SetActive(true);
            Player1Canvas.SetActive(true);
            Player2Canvas.SetActive(true);
            InterRound.SetActive(false);
            resetScore();
            roundNum++;
            RoundCounter.text = "Round " + roundNum.ToString();
            resetPlayers(loser);
            player1Mods = new int[4];
            player2Mods = new int[4];
            player3Mods = new int[4];
            player4Mods = new int[4];
            md.ResetPanels();
            SetupRound();
        }
        else
        {
            EndMatch(Mathf.Abs(loser - 3));
        }
    }

    private IEnumerator SetupRound()
    {
        yield return new WaitForEndOfFrame();
        int index = Random.Range(1, 3);
        if(index == 1)
        {
            Player1.transform.position = SpawnTop.position;
            Player2.transform.position = SpawnBottom.position;
        }
        else
        {
            Player1.transform.position = SpawnBottom.position;
            Player2.transform.position = SpawnTop.position;
        }
    }

    private IEnumerator PlayerDeath(int id)
    {
        // sound time
        soundManager.PlaySound("kill");

        Debug.Log("Player died, running coroutine...");
        int posNeg = Random.Range(1, 11);
        if (id == 1)
        {
            Player1Cam.GetComponent<PlayerController>().enabled = false;
            Player1.GetComponent<Rigidbody>().useGravity = true;
            Player1Cam.transform.SetParent(Player1.transform);
            Player2.GetComponent<InfoTracker>().Hide();

            if(Player2Kills < 3)
            {
                yield return new WaitForSeconds(downtime);
                resetPlayers(id);
                if (posNeg < 8)
                {
                    BattleModAssign(true, 1); // choose a modifier
                    BattleModActivate(newMod); // set it
                }
                else
                {
                    BattleModAssign(false, 2); // choose a modifier
                    BattleModActivate(newMod); // set it
                } 
            }
            else
            {
                StartCoroutine(NextRound(id));
            } 
        }

        else if(id == 2)
        {
            Player2Cam.GetComponent<PlayerController>().enabled = false;
            Player2.GetComponent<Rigidbody>().useGravity = true;
            Player2Cam.transform.SetParent(Player2.transform);
            Player1.GetComponent<InfoTracker>().Hide();

            if (Player1Kills < 3)
            {
                yield return new WaitForSeconds(downtime);
                resetPlayers(id);
                if (posNeg < 8)
                {
                    BattleModAssign(true, 2); // choose a modifier
                    BattleModActivate(newMod); // set it
                }
                else
                {
                    BattleModAssign(false, 1); // choose a modifier
                    BattleModActivate(newMod); // set it
                }
            }

            else
            {
                StartCoroutine(NextRound(id));
            }
        }
        Rotator.live = true;
        yield return null;
    }

    #endregion

    // battle mod framework
    #region

    private int[] targetMods; // we'll create an array to hold the mods before we apply them
    private int[] setMods; // we'll create an array to copy our mods off of and apply them
    

    public void BattleModAssign(bool isGood, int targetPlayer)
    {
        // this script will choose one of the battle mods and assign it
        // if a mod is good or bad will be assigned externally
        // this script will add mods to an array

        // which player are we working with?
        #region 
        if (targetPlayer == 1)
        { targetMods = player1Mods; };

        if (targetPlayer == 2)
        { targetMods = player2Mods; };

        if (targetPlayer == 3)
        { targetMods = player3Mods; };

        if (targetPlayer == 4)
        { targetMods = player4Mods; };
        #endregion

        for (int i = 0; i < targetMods.Length;  i++)
        {

                if (targetMods[i] == 0)
                {

                    int j; // declare the number that will be used to determine our mod

                    if (isGood)
                    {
                        j = Random.Range((int)1, (int)8); // choose a good mod
                    }
                    else
                    {
                        j = Random.Range((int)9, (int)13); // choose a bad mod
                    }

                    for (int f = 0; f < targetMods.Length; f++) // check for no dupes
                    {
                        while (targetMods[f] == j)
                        {
                            if (isGood)
                            {
                                j = Random.Range((int)1, (int)7); // choose a good mod
                            }
                            else
                            {
                                j = Random.Range((int)8, (int)11); // choose a bad mod
                            }
                        }
                    }

                    targetMods[i] = j; // set it
                    newMod = j;
                    Debug.Log("mod is " + j);
                    md.ActivateMod(targetPlayer, isGood, i, j);
                break;
                }
        }
    }
    #endregion

    // working with and activating battlemods
    public void BattleModActivate(int mod)
    {
        // this script will activate the mods and make the necessary applications to all players
        #region
        if (mod == (int)battleMods.Armor)
        {
            Debug.Log(mod.ToString() + ": Armor");
            // add armor

        }

        if (mod == (int)battleMods.TunnelVision)
        {
            Debug.Log(mod.ToString() + ": Tunnel Vision");
            // change the camera

        }

        if (mod == (int)battleMods.Glowing)
        {
            Debug.Log(mod.ToString() + ": Glowing");
            // change the target player's material

        }

        if (mod == (int)battleMods.Stealth)
        {
            Debug.Log(mod.ToString() + ": Stealth");
            // change the target player's material

        }

        if (mod == (int)battleMods.Large)
        {
            Debug.Log(mod.ToString() + ": Large");
            // change the target player's size

        }

        if (mod == (int)battleMods.Scatter)
        {
            Debug.Log(mod.ToString() + ": Scatter");
        }

        if (mod == (int)battleMods.Supercharge)
        {
            Debug.Log(mod.ToString() + ": Supercharge");
        }

        if (mod == (int)battleMods.Regen)
        {
            Debug.Log(mod.ToString() + ": Regen");
            // add HP regen to a player

        }

        if (mod == (int)battleMods.Shield)
        {
            Debug.Log(mod.ToString() + ": Shield");
            // change the target player's material

        }

        if (mod == (int)battleMods.Speed)
        {
            Debug.Log(mod.ToString() + ": Speed");
            // increase the speed of the player

        }

        if (mod == (int)battleMods.Tracking)
        {
            Debug.Log(mod.ToString() + ": Tracking");
            // add tracking particles to the player

        }

        if (mod == (int)battleMods.Vampirism)
        {
            Debug.Log(mod.ToString() + ": Vampirism");
            // change the target player's material

        }
        #endregion
    }
}
