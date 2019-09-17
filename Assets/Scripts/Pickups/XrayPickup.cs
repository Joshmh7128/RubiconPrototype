using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayPickup : MonoBehaviour
{
    private RoundManager rm;
    public int activeTime;
    public ParticleSystem burst;

    private void Awake()
    {
        rm = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int id = collision.gameObject.GetComponent<InfoTracker>().id;
            if(id == 1)
            {
                rm.Player2.GetComponent<InfoTracker>().AddXray(activeTime);
            }
            else
            {
                rm.Player1.GetComponent<InfoTracker>().AddXray(activeTime);
            }
            Instantiate(burst, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
