﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayPickup4p : MonoBehaviour
{
    private RoundManager4p rm;
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
        rm = GameObject.Find("RoundManager").GetComponent<RoundManager4p>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int id = collision.gameObject.GetComponent<InfoTracker4p>().id;
            switch(id)
            {
                case 1:
                    rm.Player2.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player3.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player4.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    break;
                case 2:
                    rm.Player1.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player3.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player4.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    break;
                case 3:
                    rm.Player1.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player2.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player4.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    break;
                case 4:
                    rm.Player1.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player2.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    rm.Player3.GetComponent<InfoTracker4p>().AddXray(activeTime);
                    break;
            }
            Instantiate(burst, this.transform.position, Quaternion.identity);
            sm.PlaySound("xrayPickup");
            GameObject instantiated = Instantiate(respawner, this.transform.position, Quaternion.identity);
            instantiated.transform.SetParent(this.transform.parent);
            Destroy(this.gameObject);
        }
    }
}
