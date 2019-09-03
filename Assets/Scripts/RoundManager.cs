using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [Header("Score")]

    public int Player1Kills = 0;
    public int Player2Kills = 0;

    public Text Player1Score;
    public Text Player2Score;

    public int Player1RoundsWon = 0;
    public int Player2RoundsWon = 0;

    public Text Player1Rounds;
    public Text Player2Rounds;

    private int roundNum = 1;
    public Text RoundCounter;

    [Header("Scene")]

    public GameObject Player1;
    public GameObject Player2;

    public GameObject Player1Cam;
    public GameObject Player2Cam;

    public GameObject Player1Canvas;
    public GameObject Player2Canvas;

    public CubeAction Rotator;

    public Transform SpawnTop;
    public Transform SpawnBottom;

    private int needed = 3;

    public CubeAction myArena;
    private int shuffles = 20;

    private int downtime = 10;

    [Header("Modifiers")]

    public string weapon1;
    public string weapon2;
    public string weapon3;
    public string weapon4;
    public string weapon5;

    private void Start()
    {
        GenerateWeapons();
        myArena.shuffle(shuffles);
        StartCoroutine("SetupRound");
    }

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
                    weapons[i] = "blaster";
                }
                else if (key == 2)
                {
                    weapons[i] = "shotgun";
                }
                else if (key == 3)
                {
                    weapons[i] = "sniper";
                }
                else if (key == 4)
                {
                    weapons[i] = "machinegun";
                }
                else if (key == 5)
                {
                    weapons[i] = "missile";
                }
                else if (key == 6)
                {
                    weapons[i] = "grenade";
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
        weapon1 = weapons[0];
        weapon2 = weapons[1];
        weapon3 = weapons[2];
        weapon4 = weapons[3];
        weapon5 = weapons[4];
    }

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

    private IEnumerator EndRound(int winner)
    {
        yield return null;
    }

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
        yield return new WaitForSeconds(downtime);
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
        resetScore();
        roundNum++;
        RoundCounter.text = "Round " + roundNum.ToString();
        resetPlayers(loser);
        SetupRound();
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
        if(id == 1)
        {
            Player1Cam.GetComponent<PlayerController>().enabled = false;
            Player1.GetComponent<Rigidbody>().useGravity = true;
            Player1Cam.transform.SetParent(Player1.transform);
            Player2.GetComponent<InfoTracker>().Hide();

            if(Player2Kills < 3)
            {
                yield return new WaitForSeconds(downtime);
                resetPlayers(id);
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
                StartCoroutine(SetupRound());
            }

            else
            {
                StartCoroutine(NextRound(id));
            }
        }
        Rotator.live = true;
        yield return null;
    }
}
