using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public int shieldBoost;
    public ParticleSystem burst;
    public SoundManager sm;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<InfoTracker>().AddShield(shieldBoost);
            Instantiate(burst, this.transform.position, Quaternion.identity);
            sm.PlaySound("armorPickup");
            Destroy(this.gameObject);
        }
    }
}
