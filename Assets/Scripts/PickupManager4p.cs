using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager4p : MonoBehaviour
{
    //I'm well aware this code is ugly, but it works in the build. At least it's not that inefficient, since the lists[] would be short anyway.

    public Transform location1;
    public Transform location2;
    public Transform location3;
    public Transform location4;

    public GameObject pickupHealth;
    public GameObject pickupShield;
    public GameObject pickupXray;

    public GameObject spawned1;
    public GameObject spawned2;
    public GameObject spawned3;
    public GameObject spawned4;

    public SoundManager sm;

    public void SpawnPickups()
    {
        int noSpawn = Random.Range(0, 4);
        if(noSpawn != 0)
        {
            int spawnKey = Random.Range(0, 3);
            if(spawnKey == 0)
            {
                spawned1 = Instantiate(pickupHealth, location1.position, Quaternion.identity);
                spawned1.transform.SetParent(location1);
                spawned1.GetComponent<HealthPickup>().sm = sm;
            }
            if (spawnKey == 1)
            {
                spawned1 = Instantiate(pickupShield, location1.position, Quaternion.identity);
                spawned1.transform.SetParent(location1);
                spawned1.GetComponent<ArmorPickup>().sm = sm;
            }
            if (spawnKey == 2)
            {
                spawned1 = Instantiate(pickupXray, location1.position, Quaternion.identity);
                spawned1.transform.SetParent(location1);
                spawned1.GetComponent<XrayPickup>().sm = sm;
            }
        }
        if (noSpawn != 1)
        {
            int spawnKey = Random.Range(0, 3);
            if (spawnKey == 0)
            {
                spawned2 = Instantiate(pickupHealth, location2.position, Quaternion.identity);
                spawned2.transform.SetParent(location2);
                spawned2.GetComponent<HealthPickup>().sm = sm;
            }
            if (spawnKey == 1)
            {
                spawned2 = Instantiate(pickupShield, location2.position, Quaternion.identity);
                spawned2.transform.SetParent(location2);
                spawned2.GetComponent<ArmorPickup>().sm = sm;
            }
            if (spawnKey == 2)
            {
                spawned2 = Instantiate(pickupXray, location2.position, Quaternion.identity);
                spawned2.transform.SetParent(location2);
                spawned2.GetComponent<XrayPickup>().sm = sm;
            }
        }
        if (noSpawn != 2)
        {
            int spawnKey = Random.Range(0, 3);
            if (spawnKey == 0)
            {
                spawned3 = Instantiate(pickupHealth, location3.position, Quaternion.identity);
                spawned3.transform.SetParent(location3);
                spawned3.GetComponent<HealthPickup>().sm = sm;
            }
            if (spawnKey == 1)
            {
                spawned3 = Instantiate(pickupShield, location3.position, Quaternion.identity);
                spawned3.transform.SetParent(location3);
                spawned3.GetComponent<ArmorPickup>().sm = sm;
            }
            if (spawnKey == 2)
            {
                spawned3 = Instantiate(pickupXray, location3.position, Quaternion.identity);
                spawned3.transform.SetParent(location3);
                spawned3.GetComponent<XrayPickup>().sm = sm;
            }
        }
        if (noSpawn != 3)
        {
            int spawnKey = Random.Range(0, 3);
            if (spawnKey == 0)
            {
                spawned4 = Instantiate(pickupHealth, location4.position, Quaternion.identity);
                spawned4.transform.SetParent(location4);
                spawned4.GetComponent<HealthPickup>().sm = sm;
            }
            if (spawnKey == 1)
            {
                spawned4 = Instantiate(pickupShield, location4.position, Quaternion.identity);
                spawned4.transform.SetParent(location4);
                spawned4.GetComponent<ArmorPickup>().sm = sm;
            }
            if (spawnKey == 2)
            {
                spawned4 = Instantiate(pickupXray, location4.position, Quaternion.identity);
                spawned4.transform.SetParent(location4);
                spawned4.GetComponent<XrayPickup>().sm = sm;
            }
        }
    }

    public void ClearPickups()
    {
        foreach (Transform child in location1.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in location2.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in location3.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in location4.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        /*
        if(spawned1 != null)
        {
            Destroy(spawned1);
        }
        if (spawned2 != null)
        {
            Destroy(spawned2);
        }
        if (spawned3 != null)
        {
            Destroy(spawned3);
        }
        if (spawned4 != null)
        {
            Destroy(spawned4);
        }
        */
    }
}
