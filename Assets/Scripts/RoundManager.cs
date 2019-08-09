using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public int Player1Score;
    public int Player2Score;
    public int won;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Player1Score = 0;
        Player2Score = 0;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Manager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator ShowScore()
    {
        
        Debug.Log("Player " + won + " wins!");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ScoreScreen");
        yield return new WaitForSeconds(1);
        if (won == 1)
        {
            Player1Score++;
        }
        else
        {
            Player2Score++;
        }
        GameObject.FindGameObjectWithTag("Counter").GetComponent<ScoreSetter>().updateText(Player1Score, Player2Score);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("RoundSetup");
    }
}
