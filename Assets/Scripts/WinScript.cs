using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public Text winner;
    public Text p1score;
    public Text p2score;

    // Start is called before the first frame update
    void Start()
    {
        GameObject src = GameObject.FindGameObjectWithTag("Manager");
        winner.text = "Player " + src.GetComponent<RoundManager>().won.ToString();
        p1score.text = src.GetComponent<RoundManager>().Player1Score.ToString();
        p2score.text = src.GetComponent<RoundManager>().Player2Score.ToString();
    }
}
