using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    bool isExplosion = false; // is this an explosion of a rocket or grenade?
    //public ModApplication modApp;
    public int dmg;

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            hit.collider.gameObject.GetComponent<InfoTracker>().TakeDamage(dmg);
            //modApp.VampCheck(dmg);
        }
        else if (hit.collider.gameObject.CompareTag("Breakable"))
        {
            hit.collider.gameObject.GetComponent<BreakableObject>().TakeDamage();
        }
    }
}
