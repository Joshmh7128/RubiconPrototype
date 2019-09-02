using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public int Player1Kills = 0;
    public int Player2Kills = 0;

    public GameObject Player1;
    public GameObject Player2;

    public GameObject Player1Cam;
    public GameObject Player2Cam;

    public GameObject Player1Canvas;
    public GameObject Player2Canvas;

    //private Text ScoreText1;
    //private Text ScoreText2;

    private CubeAction Rotator;

    public Transform SpawnTop;
    public Transform SpawnBottom;

    private int needed = 3;

    private CubeAction myArena;
    private int shuffles = 20;

    private void Start()
    {
        myArena = GameObject.Find("PivotManager").GetComponent<CubeAction>();

        Player1 = GameObject.Find("Player1");
        Player1Cam = GameObject.Find("PlayerCam");

        Player2 = GameObject.Find("Player2");
        Player2Cam = GameObject.Find("PlayerCam2");

        Player1Canvas = GameObject.Find("PlayerCanvas");
        Player2Canvas = GameObject.Find("PlayerCanvas2");

        Rotator = GameObject.Find("PivotManager").GetComponent<CubeAction>();

        myArena.shuffle(shuffles);
        StartCoroutine("SetupRound");
    }

    public void updateScore(int loser)
    {
        if(loser == 1)
        {
            Player2Kills++;
            //ScoreText2.text = Player2Kills.ToString();
        }
        else if(loser == 2)
        {
            Player1Kills++;
            //ScoreText1.text = Player1Kills.ToString();
        }

        Rotator.live = false;
        StartCoroutine(PlayerDeath(loser));
    }

    public void resetScore()
    {
        Player1Kills = 0;
        Player2Kills = 0;
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

            yield return new WaitForSeconds(10);

            Player1Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player1Cam.GetComponent<PlayerController>()._weaponSystems.magSize;
            Player2Cam.GetComponent<PlayerController>()._weaponSystems.mag = Player2Cam.GetComponent<PlayerController>()._weaponSystems.magSize;

            Player1Cam.GetComponent<PlayerController>().enabled = true;
            Player1.GetComponent<Rigidbody>().useGravity = false;
            Player1Cam.transform.SetParent(null);
            Player1.GetComponent<InfoTracker>().ResetStats();
            Player2.GetComponent<InfoTracker>().ResetStats();
        }

        else if(id == 2)
        {
            Player2Cam.GetComponent<PlayerController>().enabled = false;
            Player2.GetComponent<Rigidbody>().useGravity = true;
            Player2Cam.transform.SetParent(Player2.transform);
            Player1.GetComponent<InfoTracker>().Hide();

            yield return new WaitForSeconds(10);

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
        yield return null;
    }
}
