using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupTimer : MonoBehaviour
{
    public float myTimer;
    public Image[] myBars;
    private float timerLength = 45f;

    // Start is called before the first frame update
    void Start()
    {
        myTimer = timerLength;
    }

    private void Update()
    {
        myTimer -= Time.deltaTime;
        foreach(Image x in myBars)
        {
            x.fillAmount = 1 - (myTimer / timerLength);
        }
    }

}
