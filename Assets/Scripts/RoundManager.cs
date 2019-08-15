using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public int Player1Kills = 0;
    public int Player2Kills = 0;

    private int needed = 3;

    private CubeAction myArena;
    private int shuffles = 20;

    private void Start()
    {
        myArena = GameObject.Find("PivotManager").GetComponent<CubeAction>();
        myArena.shuffle(shuffles);
    }

    public void updateScore(int loser)
    {
        if(loser == 1)
        {
            Player1Kills++;
            if(Player1Kills >= needed)
            {
                Debug.Log("Player 1 Wins the Round!");
                resetScore();
            }
        }
        else if(loser == 2)
        {
            Player2Kills++;
            if (Player2Kills >= needed)
            {
                Debug.Log("Player 2 Wins the Round!");
                resetScore();
            }
        }
    }

    public void resetScore()
    {
        Player1Kills = 0;
        Player2Kills = 0;
    }
}
