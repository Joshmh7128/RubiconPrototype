using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int hpBoost;
    public ParticleSystem burst;
    public SoundManager sm;
    public GameObject respawner;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<InfoTracker>() != null)
            {
                collision.gameObject.GetComponent<InfoTracker>().AddHealth(hpBoost);
            }
            if (collision.gameObject.GetComponent<InfoTracker4p>() != null)
            {
                collision.gameObject.GetComponent<InfoTracker4p>().AddHealth(hpBoost);
            }
            Instantiate(burst, this.transform.position, Quaternion.identity);
            sm.PlaySound("hpPickup");
            GameObject instantiated = Instantiate(respawner, this.transform.position, Quaternion.identity);
            instantiated.transform.SetParent(this.transform.parent);
            Destroy(this.gameObject);
        }
    }
}
