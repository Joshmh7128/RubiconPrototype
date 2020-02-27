using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    bool isExplosion = false; // is this an explosion of a rocket or grenade?
    float dmgMult = 1f;
    public ModApplication modApp;
    public int dmg;
    public int myID;
    public ParticleSystem burst;

    /*
    private void Start()
    {
        if(modApp.ChargeCheck())
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * 3, 
                this.gameObject.transform.localScale.y * 3, this.gameObject.transform.localScale.z * 3);
            dmgMult = 1.5f;
        }
    }
    */

    private void OnEnable()
    {
        StartCoroutine("SetUpObj");
    }

    IEnumerator SetUpObj()
    {
        yield return new WaitForEndOfFrame();
        if (modApp.ChargeCheck())
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * 3,
                this.gameObject.transform.localScale.y * 3, this.gameObject.transform.localScale.z * 3);
            dmgMult = 1.5f;
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            if(hit.gameObject.GetComponent<InfoTracker>().id != myID)
            {
                hit.collider.gameObject.GetComponent<InfoTracker>().TakeDamage((int)(dmg * dmgMult));
                Instantiate(burst, transform.position, Quaternion.identity);
                modApp.VampCheck((int)(dmg * dmgMult));
            }
        }
        else if (hit.collider.gameObject.CompareTag("Breakable"))
        {
            hit.collider.gameObject.GetComponent<BreakableObject>().TakeDamage((int)(dmg * dmgMult));
        }
        else if (hit.collider.gameObject.CompareTag("Joke"))
        {
            hit.collider.gameObject.GetComponent<JokeObject>().TakeDamage((int)(dmg * dmgMult));
        }
    }
}
