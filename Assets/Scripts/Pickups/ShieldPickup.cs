using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public int shieldBoost;
    public ParticleSystem burst;
    public SoundManager sm;
    public GameObject respawner;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<InfoTracker>().AddShield(shieldBoost);
            Instantiate(burst, this.transform.position, Quaternion.identity);
            sm.PlaySound("armorPickup");
            GameObject instantiated = Instantiate(respawner, this.transform.position, Quaternion.identity);
            instantiated.transform.SetParent(this.transform.parent);
            Destroy(this.gameObject);
        }
    }
}
