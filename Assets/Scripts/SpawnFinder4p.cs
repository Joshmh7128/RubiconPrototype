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

        if(other.gameObject.name == "SpawnDetectBottom")
        {
            rm.SpawnBottom = sp;
        }
    }
}
