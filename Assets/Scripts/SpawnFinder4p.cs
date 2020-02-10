using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinder4p : MonoBehaviour
{
    private RoundManager4p rm;
    private Transform sp;


    private void Awake()
    {
        sp = this.gameObject.transform.GetChild(0);
        rm = GameObject.Find("RoundManager").GetComponent<RoundManager4p>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "SpawnDetectTop")
        {
            rm.SpawnTop = sp;
        }

        else if(other.gameObject.name == "SpawnDetectBottom")
        {
            rm.SpawnBottom = sp;
        }

        else if (other.gameObject.name == "SpawnDetectTop1")
        {
            rm.SpawnTop1 = sp;
        }

        else if (other.gameObject.name == "SpawnDetectBottom1")
        {
            rm.SpawnBottom1 = sp;
        }
    }
}
