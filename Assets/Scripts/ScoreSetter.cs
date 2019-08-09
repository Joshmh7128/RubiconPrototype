using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSetter : MonoBehaviour
{
    public Text p1;
    public Text p2;

    private void Start()
    {
        p1.text = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManager>().Player1Score.ToString();
        p2.text = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoundManager>().Player2Score.ToString();
    }

    public void updateText(int a, int b)
    {
        p1.text = a.ToString();
        p2.text = b.ToString();
    }
}
