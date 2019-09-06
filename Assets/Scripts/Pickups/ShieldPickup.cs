using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public int shieldBoost;
    public ParticleSystem burst;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<InfoTracker>().AddShield(shieldBoost);
            Instantiate(burst, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
