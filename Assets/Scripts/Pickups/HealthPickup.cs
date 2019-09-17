using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int hpBoost;
    public ParticleSystem burst;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<InfoTracker>().AddHealth(hpBoost);
            Instantiate(burst, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
