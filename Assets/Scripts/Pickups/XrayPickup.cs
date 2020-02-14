using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayPickup : MonoBehaviour
{
    private RoundManager rm;
    public int activeTime;
    public ParticleSystem burst;
    public SoundManager sm;
    public GameObject respawner;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void Awake()
    {
        rm = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int id = collision.gameObject.GetComponent<InfoTracker>().id;
            if(rm.players == 2)
            {
                if (id == 1)
                {
                    rm.Player2.GetComponent<InfoTracker>().AddXray(activeTime);
                }
                else
                {
                    rm.Player1.GetComponent<InfoTracker>().AddXray(activeTime);
                }
            }
            else if(rm.players == 4)
            {
                if (id == 1)
                {
                    rm.Player2.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player3.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player4.GetComponent<InfoTracker>().AddXray(activeTime);
                }
                else if (id == 2)
                {
                    rm.Player1.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player3.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player4.GetComponent<InfoTracker>().AddXray(activeTime);
                }
                else if (id == 3)
                {
                    rm.Player1.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player2.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player4.GetComponent<InfoTracker>().AddXray(activeTime);
                }
                else if (id == 4)
                {
                    rm.Player1.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player2.GetComponent<InfoTracker>().AddXray(activeTime);
                    rm.Player3.GetComponent<InfoTracker>().AddXray(activeTime);
                }
            }
            Instantiate(burst, this.transform.position, Quaternion.identity);
            sm.PlaySound("xrayPickup");
            GameObject instantiated = Instantiate(respawner, this.transform.position, Quaternion.identity);
            instantiated.transform.SetParent(this.transform.parent);
            Destroy(this.gameObject);
        }
    }
}
