using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinder : MonoBehaviour
{
    private RoundManager rm;
    private Transform sp;


    private void Awake()
    {
        sp = this.gameObject.transform.GetChild(0);
        rm = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "SpawnDetectTop")
        {
            rm.SpawnTop = sp;
        }

        if(other.gameObject.name == "SpawnDetectBottom")
        {
            rm.SpawnBottom = sp;
        }

        if (other.gameObject.name == "SpawnDetectTop1")
        {
            rm.SpawnTop1 = sp;
        }

        if (other.gameObject.name == "SpawnDetectBottom1")
        {
            rm.SpawnBottom1 = sp;
        }
    }
}
