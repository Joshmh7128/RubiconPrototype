using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    public int damage;
    public int myID;

    private void Start()
    {
        //Destroy(this.gameObject, 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().TakeDamage(damage);
            //Destroy(this.gameObject);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponent<InfoTracker>().id != myID)
            {
                other.GetComponent<InfoTracker>().TakeDamage(damage);
                //Destroy(this.gameObject);
            }
        }
    }
}
