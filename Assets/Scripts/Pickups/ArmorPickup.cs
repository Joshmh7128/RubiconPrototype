using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    public int armorBoost;
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
            if (collision.gameObject.GetComponent<InfoTracker>() != null)
            {
                collision.gameObject.GetComponent<InfoTracker>().AddArmor(armorBoost);
            }
            else if (collision.gameObject.GetComponent<InfoTracker4p>() != null)
            {
                collision.gameObject.GetComponent<InfoTracker4p>().AddArmor(armorBoost);
            }
            Instantiate(burst, this.transform.position, Quaternion.identity);
            sm.PlaySound("armorPickup");
            GameObject instantiated = Instantiate(respawner, this.transform.position, Quaternion.identity);
            instantiated.transform.SetParent(this.transform.parent);
            Destroy(this.gameObject);
        }
    }
}
