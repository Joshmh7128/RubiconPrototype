using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupTimer : MonoBehaviour
{
    public float myTimer;
    public Image[] myBars;
    private float timerLength = 90f;

    public GameObject pickup1;
    public GameObject pickup2;
    public GameObject pickup3;

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
            if(x != null)
            {
                x.fillAmount = 1 - (myTimer / timerLength);
            }
        }
        if(myTimer <= 0)
        {
            int spawnKey = Random.Range(0, 3);
            switch (spawnKey)
            {
                case 0:
                    GameObject spawned1 = Instantiate(pickup1, this.transform.position, Quaternion.identity);
                    spawned1.transform.SetParent(this.transform.parent);
                    break;
                case 1:
                    GameObject spawned2 = Instantiate(pickup2, this.transform.position, Quaternion.identity);
                    spawned2.transform.SetParent(this.transform.parent);
                    break;
                case 2:
                    GameObject spawned3 = Instantiate(pickup3, this.transform.position, Quaternion.identity);
                    spawned3.transform.SetParent(this.transform.parent);
                    break;
            }
            Destroy(this.gameObject);
        }
    }

}
