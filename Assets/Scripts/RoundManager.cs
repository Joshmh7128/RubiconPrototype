using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class RoundManager : MonoBehaviour
{
    /*
     * this script handles:
     * Game score, scene items, both player tracking
     * battle modifiers, and round modifiers
     */

    // all variable definitions
    #region
    [Header("Mode")]
    [Range(2, 4)]
    public int players = 2;

    [Header("Score")]

    public int Player1Kills = 0;
    public int Player2Kills = 0;
    public int Player3Kills = 0;
    public int Player4Kills = 0;

    public Text Player1Score;
    public Text Player2Score;
    public Text Player3Score;
    public Text Player4Score;

    public int Player1RoundsWon = 0;
    public int Player2RoundsWon = 0;
    public int Player3RoundsWon = 0;
    public int Player4RoundsWon = 0;

    public Text Player1Rounds;
    public Text Player2Rounds;
    public Text Player3Rounds;
    public Text Player4Rounds;

    public int roundNum = 1; // needed to make public
    public Text RoundCounter;

    [Header("Scene")]

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject Player1Cam;
    public GameObject Player2Cam;
    public GameObject Player3Cam;
    public GameObject Player4Cam;

    public GameObject Player1Canvas;
    public GameObject Player2Canvas;
    public GameObject Player3Canvas;
    public GameObject Player4Canvas;

    public GameObject Player1Countdown;
    public GameObject Player2Countdown;
    public GameObject Player3Countdown;
    public GameObject Player4Countdown;

    public GameObject player1xray;
    public GameObject player2xray;
    public GameObject player3xray;
    public GameObject player4xray;

    public GameObject RoundCanvas;
    public GameObject InterRound;
    public GameObject MatchEnd;
    public GameObject PauseMenu;
    public bool isOver = false;
    public bool isPaused = false;

    public CubeAction Rotator;

    public Transform SpawnTop;
    public Transform SpawnBottom;
    public Transform SpawnTop1;
    public Transform SpawnBottom1;

    private int needed = 3;

    public CubeAction myArena;
    public ModDisplay md;
    public PickupManager pm;
    public SoundManager sm;

    private int shuffles = 20;

    private int downtime = 5;

    public PostProcessVolume post;
    DepthOfField depthOfField = null;

    [Header("Modifiers")]

    public string[] weaponList;
    public PlayerController.Weapons[] weaponTypeList;

    public ModApplication ma1;
    public ModApplication ma2;
    public ModApplication ma3;
    public ModApplication ma4;

    [Header("Others")]
    public SoundManager soundManager;

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
    private int goodMod = 0;

    #endregion

    // start

    private void Start()
    {
        StartGame();
    }

    //pre-first round setup and display

    private void StartGame()
    {
        post.profile.TryGetSettings(out depthOfField);
        GenerateWeapons();
        AssignWeapons();
        myArena.shuffle(shuffles);
        StartCoroutine(SetupRound());
        StartCoroutine(DisplayFirst());
    }

    private IEnumerator DisplayFirst()
    {
        RoundCanvas.SetActive(false);
        depthOfField.active = true;
        InterRound.SetActive(true);
        Player1Canvas.SetActive(false);
        Player2Canvas.SetActive(false);
        float baseSpeed = Player1Cam.GetComponent<PlayerController>().speed;
        Player1Cam.GetComponent<PlayerController>().speed = 0;
        Player2Cam.GetComponent<PlayerController>().speed = 0;
        Player1Cam.GetComponent<PlayerController>().weaponLocked = true;
        Player2Cam.GetComponent<PlayerController>().weaponLocked = true;
        InterRound.transform.Find("TopText").GetComponent<Text>().text = "Prepare for Battle";
        InterRound.transform.Find("P1").GetComponent<Text>().text = Player1RoundsWon.ToString();
        InterRound.transform.Find("P2").GetComponent<Text>().text = Player2RoundsWon.ToString();
        InterRound.transform.Find("BottomText").GetComponent<Text>().text = "This Round: ";
        InterRound.transform.Find("WeaponText").GetComponent<Text>().text = weaponList[roundNum -1].ToString();
        if (players == 4)
        {
            Player3Canvas.SetActive(false);
            Player4Canvas.SetActive(false);
            Player3Cam.GetComponent<PlayerController>().speed = 0;
            Player4Cam.GetComponent<PlayerController>().speed = 0;
            Player3Cam.GetComponent<PlayerController>().weaponLocked = true;
            Player4Cam.GetComponent<PlayerController>().weaponLocked = true;
            InterRound.transform.Find("P3").GetComponent<Text>().text = Player1RoundsWon.ToString();
            InterRound.transform.Find("P4").GetComponent<Text>().text = Player2RoundsWon.ToString();
        }
        //sm.PlaySound("roundStart");
        yield return new WaitForSeconds(1);
        sm.PlaySound("round1");

        yield return new WaitForSeconds(9);

        RoundCanvas.SetActive(true);
        Player1Canvas.SetActive(true);
        Player2Canvas.SetActive(true);
        depthOfField.active = false;
        InterRound.SetActive(false);
        Player1Cam.GetComponent<PlayerController>().speed = baseSpeed;
        Player2Cam.GetComponent<PlayerController>().speed = baseSpeed;
        if(players == 4)
        {
            Player3Canvas.SetActive(true);
            Player4Canvas.SetActive(true);
            Player3Cam.GetComponent<PlayerController>().speed = baseSpeed;
            Player4Cam.GetComponent<PlayerController>().speed = baseSpeed;
        }
        StartCoroutine(CountDown());
        PlayMusic(1);
    }

    private IEnumerator CountDown()
    {
        sm.PlaySound("countdown");
        float baseSpeed = Player1Cam.GetComponent<PlayerController>().speed;
        Player1Cam.GetComponent<PlayerController>().speed = 0;
        Player2Cam.GetComponent<PlayerController>().speed = 0;
        Player1Countdown.SetActive(true);
        Player2Countdown.SetActive(true);
        if(players == 4)
        {
            Player3Cam.GetComponent<PlayerController>().speed = 0;
            Player4Cam.GetComponent<PlayerController>().speed = 0;
            Player3Countdown.SetActive(true);
            Player4Countdown.SetActive(true);
        }
        Rotator.live = true;
        yield return new WaitForSeconds(3.75f);
        Player1Cam.GetComponent<PlayerController>().speed = baseSpeed;
        Player2Cam.GetComponent<PlayerController>().speed = baseSpeed;
        Player1Cam.GetComponent<PlayerController>().weaponLocked = false;
        Player2Cam.GetComponent<PlayerController>().weaponLocked = false;
        if(players == 4)
        {
            Player3Cam.GetComponent<PlayerController>().speed = baseSpeed;
            Player4Cam.GetComponent<PlayerController>().speed = baseSpeed;
            Player3Cam.GetComponent<PlayerController>().weaponLocked = false;
            Player4Cam.GetComponent<PlayerController>().weaponLocked = false;
        }
        sm.PlaySound("enterTheRubicon");
        yield return new WaitForSeconds(1f);
        Player1Countdown.SetActive(false);
        Player2Countdown.SetActive(false);
        if(players == 4)
        {
            Player3Countdown.SetActive(false);
            Player4Countdown.SetActive(false);
        }
        yield return null;
        //PlayMusic(roundNum);
    }

    // weapon generation

    public void GenerateWeapons()
    {
        string[] weapons = new string[5];
        PlayerController.Weapons[] myWeapons = new PlayerController.Weapons[5];
        for(int i = 0; i <= 4; i++)
        {
            bool original = false;
            while(original == false)
            {
                int key = Random.Range(1, 7);
                if (key == 1)
                {
                    weapons[i] = "Blasters";
                    myWeapons[i] = PlayerController.Weapons.Blaster;
                }
                else if (key == 2)
                {
                    weapons[i] = "Shotguns";
                    myWeapons[i] = PlayerController.Weapons.Shotgun;
                }
                else if (key == 3)
                {
                    weapons[i] = "Sniper Rifles";
                    myWeapons[i] = PlayerController.Weapons.Sniper;
                }
                else if (key == 4)
                {
                    weapons[i] = "Machine Guns";
                    myWeapons[i] = PlayerController.Weapons.Machine;
                }
                else if (key == 5)
                {
                    weapons[i] = "Missile Launchers";
                    myWeapons[i] = PlayerController.Weapons.Missile;
                }
                else if (key == 6)
                {
                    weapons[i] = "Grenade Launchers";
                    myWeapons[i] = PlayerController.Weapons.Grenade;
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
        weaponTypeList = myWeapons;
    }

    private void AssignWeapons()
    {
        if (roundNum > 1 && (Player1Kills + Player2Kills  + Player3Kills + Player4Kills == 0))
        {
            /*
            string weaponKey = weaponTypeList[roundNum - 1].ToString();
            if (weaponKey == "Blaster")
            {
                sm.PlaySound("blasterRound");
            }
            if (weaponKey == "Grenade")
            {
                sm.PlaySound("grenadeRound");
            }
            if (weaponKey == "Machine")
            {
                sm.PlaySound("machineRound");
            }
            if (weaponKey == "Missile")
            {
                sm.PlaySound("missileRound");
            }
            if (weaponKey == "Shotgun")
            {
                sm.PlaySound("shotgunRound");
            }
            if (weaponKey == "Sniper")
            {
                sm.PlaySound("sniperRound");
            }
            */
        }
        Player1Cam.GetComponent<PlayerController>().activeWeapon = weaponTypeList[roundNum - 1];
        Player2Cam.GetComponent<PlayerController>().activeWeapon = weaponTypeList[roundNum - 1];
        if(players == 4)
        {
            Player3Cam.GetComponent<PlayerController>().activeWeapon = weaponTypeList[roundNum - 1];
            Player4Cam.GetComponent<PlayerController>().activeWeapon = weaponTypeList[roundNum - 1];
        }
    }

    // score update

    public void updateScore(int loser)
    {
        if(players == 2)
        {
            if (loser == 1)
            {
                Player2Kills++;
                Player2Score.text = Player2Kills.ToString();
            }
            else if (loser == 2)
            {
                Player1Kills++;
                Player1Score.text = Player1Kills.ToString();
            }

            Rotator.live = false;
            StartCoroutine(PlayerDeath(loser));
        }

        else if(players == 4)
        {
            if(GetLivingFourPlayers() <= 1)
            {
                int winner = GetBattleWinner();
                if(winner == 1)
                {
                    Player1Kills++;
                    Player1Score.text = Player1Kills.ToString();
                }
                else if(winner == 2)
                {
                    Player2Kills++;
                    Player2Score.text = Player2Kills.ToString();
                }
                else if(winner == 3)
                {
                    Player3Kills++;
                    Player3Score.text = Player3Kills.ToString();
                }
                else
                {
                    Player4Kills++;
                    Player4Score.text = Player4Kills.ToString();
                }
                Rotator.live = false;
            }
            StartCoroutine(PlayerDeath(loser));
        }
    }

    public int GetLivingFourPlayers()
    {
        int alive = 0;
        if(!Player1.GetComponent<InfoTracker>().dead)
        {
            alive++;
        }
        if (!Player2.GetComponent<InfoTracker>().dead)
        {
            alive++;
        }
        if (!Player3.GetComponent<InfoTracker>().dead)
        {
            alive++;
        }
        if (!Player4.GetComponent<InfoTracker>().dead)
        {
            alive++;
        }
        return alive;
    }

    public int GetBattleWinner()
    {
        if(!Player1.GetComponent<InfoTracker>().dead)
        {
            return 1;
        }
        else if (!Player2.GetComponent<InfoTracker>().dead)
        {
            return 2;
        }
        else if (!Player3.GetComponent<InfoTracker>().dead)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    // round end coroutine

    private void EndMatch(int winner)
    {
        isOver = true;
        if(winner == 1)
        {
            sm.PlaySound("congratsPlayerOne");
        }
        if (winner == 2)
        {
            sm.PlaySound("congratsPlayerTwo");
        }
        if (winner == 3)
        {
            sm.PlaySound("congratsPlayerThree");
        }
        if (winner == 4)
        {
            sm.PlaySound("congratsPlayerFour");
        }
        RoundCanvas.SetActive(false);
        Player1.GetComponent<InfoTracker>().Hide();
        Player2.GetComponent<InfoTracker>().Hide();
        depthOfField.active = true;
        MatchEnd.transform.Find("TopText").GetComponent<Text>().text = "Player " + winner.ToString() + " is the Champion";
        MatchEnd.transform.Find("P1").GetComponent<Text>().text = Player1RoundsWon.ToString();
        MatchEnd.transform.Find("P2").GetComponent<Text>().text = Player2RoundsWon.ToString();
        MatchEnd.SetActive(true);
        if(players == 4)
        {
            Player3.GetComponent<InfoTracker>().Hide();
            Player4.GetComponent<InfoTracker>().Hide();
            MatchEnd.transform.Find("P3").GetComponent<Text>().text = Player3RoundsWon.ToString();
            MatchEnd.transform.Find("P4").GetComponent<Text>().text = Player4RoundsWon.ToString();
        }
    }

    public void PauseGame()
    {
        Player1Cam.GetComponent<PlayerController>().enabled = false;
        Player2Cam.GetComponent<PlayerController>().enabled = false;
        if(players == 4)
        {
            Player3Cam.GetComponent<PlayerController>().enabled = false;
            Player4Cam.GetComponent<PlayerController>().enabled = false;
        }
        depthOfField.active = true;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Player1Cam.GetComponent<PlayerController>().enabled = true;
        Player2Cam.GetComponent<PlayerController>().enabled = true;
        if(players == 4)
        {
            Player3Cam.GetComponent<PlayerController>().enabled = true;
            Player4Cam.GetComponent<PlayerController>().enabled = true;
        }
        depthOfField.active = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void layerXray(int activator)
    {
        int newLayer = 27 + activator;
        player1xray.layer = newLayer;
        player2xray.layer = newLayer;
        player3xray.layer = newLayer;
        player4xray.layer = newLayer;

    }

    // full reset and next round start and setup
    #region

    public void resetScore()
    {
        Player1Kills = 0;
        Player2Kills = 0;
        Player1Score.text = "0";
        Player2Score.text = "0";
        if(players == 4)
        {
            Player3Kills = 0;
            Player4Kills = 0;
            Player3Score.text = "0";
            Player4Score.text = "0";
        }

    }

    public void resetPlayers(int id)
    {
        /*
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
        */

        Player1Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
        Player2Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
        Player1Cam.GetComponent<PlayerController>().enabled = true;
        Player2Cam.GetComponent<PlayerController>().enabled = true;
        Player1.GetComponent<Rigidbody>().useGravity = false;
        Player2.GetComponent<Rigidbody>().useGravity = false;
        Player1Cam.transform.SetParent(null);
        Player2Cam.transform.SetParent(null);
        Player1.GetComponent<InfoTracker>().ResetStats();
        Player2.GetComponent<InfoTracker>().ResetStats();

        if (players == 4)
        {
            Player3Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
            Player4Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
            Player3Cam.GetComponent<PlayerController>().enabled = true;
            Player4Cam.GetComponent<PlayerController>().enabled = true;
            Player3.GetComponent<Rigidbody>().useGravity = false;
            Player4.GetComponent<Rigidbody>().useGravity = false;
            Player3Cam.transform.SetParent(null);
            Player4Cam.transform.SetParent(null);
            Player3.GetComponent<InfoTracker>().ResetStats();
            Player4.GetComponent<InfoTracker>().ResetStats();
        }

        StartCoroutine(SetupRound());
        Rotator.live = true;
    }

    private IEnumerator NextRound(int loser)
    { 
        if(players == 2)
        {
            if (loser == 1)
            {
                Player2RoundsWon++;
                Player2Rounds.text = Player2RoundsWon.ToString();
            }
            else if (loser == 2)
            {
                Player1RoundsWon++;
                Player1Rounds.text = Player1RoundsWon.ToString();
            }
            ma1.ResetMods();
            ma2.ResetMods();

            if (Player1RoundsWon < 3 && Player2RoundsWon < 3)
            {
                RoundCanvas.SetActive(false);
                Player1.GetComponent<InfoTracker>().Hide();
                Player2.GetComponent<InfoTracker>().Hide();
                depthOfField.active = true;
                InterRound.SetActive(true);
                InterRound.transform.Find("TopText").GetComponent<Text>().text = "Round " + roundNum.ToString() + " Complete";
                InterRound.transform.Find("P1").GetComponent<Text>().text = Player1RoundsWon.ToString();
                InterRound.transform.Find("P2").GetComponent<Text>().text = Player2RoundsWon.ToString();
                InterRound.transform.Find("WeaponText").GetComponent<Text>().text = weaponList[roundNum].ToString();
                sm.PlaySound("roundOver");

                yield return new WaitForSeconds(5);

                sm.PlaySound("round" + ((roundNum + 1).ToString()));

                yield return new WaitForSeconds(5);

                depthOfField.active = false;
                InterRound.SetActive(false);
                resetScore();
                roundNum++; // advances to next round
                player1Mods = new int[4];
                player2Mods = new int[4];
                player3Mods = new int[4];
                player4Mods = new int[4];
                md.ResetPanels();
                resetPlayers(loser);

                yield return new WaitForSeconds(0.1f);

                RoundCanvas.SetActive(true);
                Player1Canvas.transform.Find("ToHide").gameObject.SetActive(true);
                Player2Canvas.transform.Find("ToHide").gameObject.SetActive(true);
                Player1Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
                Player2Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
                RoundCounter.text = "Round " + roundNum.ToString();
                StartCoroutine(CountDown());
            }
            else
            {
                EndMatch(Mathf.Abs(loser - 3));
            }
            PlayMusic(roundNum);
        }
        else if(players == 4)
        {
            int won = GetBattleWinner();
            if(won == 1)
            {
                Player1RoundsWon++;
                Player1Rounds.text = Player1RoundsWon.ToString();
            }
            else if (won == 2)
            {
                Player2RoundsWon++;
                Player2Rounds.text = Player2RoundsWon.ToString();
            }
            else if (won == 3)
            {
                Player3RoundsWon++;
                Player3Rounds.text = Player3RoundsWon.ToString();
            }
            else if (won == 4)
            {
                Player4RoundsWon++;
                Player4Rounds.text = Player4RoundsWon.ToString();
            }
            ma1.ResetMods();
            ma2.ResetMods();
            ma3.ResetMods();
            ma4.ResetMods();
            if(Player1RoundsWon < 2 && Player2RoundsWon < 2 && Player3RoundsWon < 2 && Player4RoundsWon < 2)
            {
                RoundCanvas.SetActive(false);
                Player1.GetComponent<InfoTracker>().Hide();
                Player2.GetComponent<InfoTracker>().Hide();
                Player3.GetComponent<InfoTracker>().Hide();
                Player4.GetComponent<InfoTracker>().Hide();
                depthOfField.active = true;
                InterRound.SetActive(true);
                InterRound.transform.Find("TopText").GetComponent<Text>().text = "Round " + roundNum.ToString() + " Complete";
                InterRound.transform.Find("P1").GetComponent<Text>().text = Player1RoundsWon.ToString();
                InterRound.transform.Find("P2").GetComponent<Text>().text = Player2RoundsWon.ToString();
                InterRound.transform.Find("P3").GetComponent<Text>().text = Player3RoundsWon.ToString();
                InterRound.transform.Find("P4").GetComponent<Text>().text = Player4RoundsWon.ToString();
                InterRound.transform.Find("WeaponText").GetComponent<Text>().text = weaponList[roundNum].ToString();
                sm.PlaySound("roundOver");

                yield return new WaitForSeconds(5);

                sm.PlaySound("round" + ((roundNum + 1).ToString()));

                yield return new WaitForSeconds(5);

                depthOfField.active = false;
                InterRound.SetActive(false);
                resetScore();
                roundNum++; // advances to next round
                player1Mods = new int[4];
                player2Mods = new int[4];
                player3Mods = new int[4];
                player4Mods = new int[4];
                md.ResetPanels();
                resetPlayers(loser);

                yield return new WaitForSeconds(0.1f);

                RoundCanvas.SetActive(true);
                Player1Canvas.transform.Find("ToHide").gameObject.SetActive(true);
                Player2Canvas.transform.Find("ToHide").gameObject.SetActive(true);
                Player3Canvas.transform.Find("ToHide").gameObject.SetActive(true);
                Player4Canvas.transform.Find("ToHide").gameObject.SetActive(true);
                Player1Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
                Player2Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
                Player3Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player3Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
                Player4Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player4Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
                RoundCounter.text = "Round " + roundNum.ToString();
                StartCoroutine(CountDown());
            }
            else
            {
                EndMatch(won);
            }
        }
    }

    private IEnumerator SetupRound()
    {
        yield return new WaitForEndOfFrame();
        if(players == 2)
        {
            int index = Random.Range(1, 3);
            if (index == 1)
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
        else if(players == 4)
        {
            Player1.transform.position = SpawnTop.position;
            Player2.transform.position = SpawnBottom.position;
            Player3.transform.position = SpawnTop1.position;
            Player4.transform.position = SpawnBottom1.position;
        }
        AssignWeapons();
        pm.ClearPickups();
        pm.SpawnPickups();
    }

    private IEnumerator PlayerDeath(int id)
    {
        if (players == 2)
        {
            Debug.Log("Player died, running coroutine...");
            int posNeg = Random.Range(1, 11);
            if (id == 1)
            {
                Player1Cam.GetComponent<PlayerController>().enabled = false;
                Player1.GetComponent<Rigidbody>().useGravity = true;
                Player1Cam.transform.SetParent(Player1.transform);
                Player2.GetComponent<InfoTracker>().Hide();

                if (Player2Kills < 3)
                {
                    sm.PlaySound("kill");
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

            else if (id == 2)
            {
                Player2Cam.GetComponent<PlayerController>().enabled = false;
                Player2.GetComponent<Rigidbody>().useGravity = true;
                Player2Cam.transform.SetParent(Player2.transform);
                Player1.GetComponent<InfoTracker>().Hide();

                if (Player1Kills < 3)
                {
                    sm.PlaySound("kill");
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

        else if(players == 4)
        {
            sm.PlaySound("kill");
            if (id == 1)
            {
                Player1Cam.GetComponent<PlayerController>().enabled = false;
                Player1.GetComponent<Rigidbody>().useGravity = true;
                Player1Cam.transform.SetParent(Player1.transform);
            }
            else if (id == 2)
            {
                Player2Cam.GetComponent<PlayerController>().enabled = false;
                Player2.GetComponent<Rigidbody>().useGravity = true;
                Player2Cam.transform.SetParent(Player2.transform);
            }
            else if (id == 3)
            {
                Player3Cam.GetComponent<PlayerController>().enabled = false;
                Player3.GetComponent<Rigidbody>().useGravity = true;
                Player3Cam.transform.SetParent(Player3.transform);
            }
            else if (id == 4)
            {
                Player4Cam.GetComponent<PlayerController>().enabled = false;
                Player4.GetComponent<Rigidbody>().useGravity = true;
                Player4Cam.transform.SetParent(Player4.transform);
            }
            if(GetLivingFourPlayers() <= 1)
            {
                if(GetBattleWinner() == 1)
                {
                    Player1.GetComponent<InfoTracker>().Hide();
                }
                else if(GetBattleWinner() == 2)
                {
                    Player2.GetComponent<InfoTracker>().Hide();
                }
                else if (GetBattleWinner() == 3)
                {
                    Player3.GetComponent<InfoTracker>().Hide();
                }
                else if (GetBattleWinner() == 4)
                {
                    Player4.GetComponent<InfoTracker>().Hide();
                }

                //if round end
                if(Player1Kills > 1 || Player2Kills > 1 || Player3Kills > 1 || Player4Kills > 1)
                {
                    StartCoroutine(NextRound(id));
                }

                //if battle end
                else
                {
                    int victor = GetBattleWinner();
                    yield return new WaitForSeconds(downtime);
                    resetPlayers(id);
                    int modChooser = Random.Range(1, 4);
                    if(modChooser == 1)
                    {
                        if(victor == 1)
                        {
                            BattleModAssign(false, 1);
                            BattleModActivate(newMod);
                        }
                        else if (victor == 2)
                        {
                            BattleModAssign(false, 2);
                            BattleModActivate(newMod);
                        }
                        else if (victor == 3)
                        {
                            BattleModAssign(false, 3);
                            BattleModActivate(newMod);
                        }
                        else if (victor == 4)
                        {
                            BattleModAssign(false, 4);
                            BattleModActivate(newMod);
                        }
                    }
                    else
                    {
                        if (victor == 1)
                        {
                            BattleModAssign(true, 2);
                            BattleModAssign(true, 3);
                            BattleModAssign(true, 4);
                            BattleModActivate(newMod);
                        }
                        else if (victor == 2)
                        {
                            BattleModAssign(true, 1);
                            BattleModAssign(true, 3);
                            BattleModAssign(true, 4);
                            BattleModActivate(newMod);
                        }
                        else if (victor == 3)
                        {
                            BattleModAssign(true, 1);
                            BattleModAssign(true, 2);
                            BattleModAssign(true, 4);
                            BattleModActivate(newMod);
                        }
                        else if (victor == 4)
                        {
                            BattleModAssign(true, 1);
                            BattleModAssign(true, 2);
                            BattleModAssign(true, 3);
                            BattleModActivate(newMod);
                        }
                    }
                    Debug.Log("Good mod is: " + goodMod);
                    goodMod = 0;
                    switch (newMod)
                    {
                        case 1:
                            sm.PlaySound("shieldMod");
                            break;
                        case 2:
                            sm.PlaySound("hpRegenMod");
                            break;
                        case 3:
                            sm.PlaySound("scatterMod");
                            break;
                        case 4:
                            sm.PlaySound("armorMod");
                            break;
                        case 5:
                            sm.PlaySound("speedMod");
                            break;
                        case 6:
                            sm.PlaySound("invisMod");
                            break;
                        case 7:
                            sm.PlaySound("superchargeMod");
                            break;
                        case 8:
                            sm.PlaySound("vampMod");
                            break;
                        case 9:
                            sm.PlaySound("glowingMod");
                            break;
                        case 10:
                            sm.PlaySound("largeMod");
                            break;
                        case 11:
                            sm.PlaySound("trackingMod");
                            break;
                        case 12:
                            sm.PlaySound("cineMod");
                            break;
                    }
                }
            }
        }
    }

    private void PlayMusic(int currentRoundNum)
    {
        // play music
        switch (currentRoundNum)
        {
            case 1:
                soundManager.PlaySound("battleThemeA");
                break;
            case 2:
                soundManager.PlaySound("battleThemeC");
                break;
            case 3:
                soundManager.PlaySound("battleThemeB");
                break;
            case 4:
                soundManager.PlaySound("battleThemeA");
                break;
            case 5:
                soundManager.PlaySound("battleThemeB");
                break;

        }
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
                        j = Random.Range((int)1, (int)9); // choose a good mod
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
                                j = Random.Range((int)1, (int)9); // choose a good mod
                            }
                            else
                            {
                                j = Random.Range((int)9, (int)13); // choose a bad mod
                            }
                        }
                    }

                if(players == 4 && isGood)
                {
                    if(goodMod == 0)
                    {
                        Debug.Log("generating good mod: " + j);
                        goodMod = j;
                    }
                    else
                    {
                        Debug.Log("copying from good mod: " + goodMod);
                        j = goodMod;
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
