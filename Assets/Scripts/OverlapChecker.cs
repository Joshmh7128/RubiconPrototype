using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    public int damage;
    public int myID;
    public ParticleSystem burst;
    public ModApplication modApp;


    private void Start()
    {
        Destroy(this.gameObject, 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            InfoTracker it = other.GetComponent<InfoTracker>();
            if (it.id != myID)
            {
                it.TakeDamage(damage);
                it.overlappedThisFrame = true;
                Instantiate(burst, other.transform.position, Quaternion.identity);
                float dmgMult = 1f;
                if(modApp.ChargeCheck())
                {
                    dmgMult = 1.5f;
                }
                modApp.VampCheck((int)(damage * dmgMult));
                Destroy(this.gameObject);
            }
        }
    }
}
