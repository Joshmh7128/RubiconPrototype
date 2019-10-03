using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    //I'm well aware this code is ugly, but it works in the build. At least it's not that inefficient, since the lists[] would be short anyway.

    public Transform location1;
    public Transform location2;
    public Transform location3;
    public Transform location4;

    public GameObject pickup1;
    public GameObject pickup2;
    public GameObject pickup3;

    public GameObject spawned1;
    public GameObject spawned2;
    public GameObject spawned3;
    public GameObject spawned4;

    public void SpawnPickups()
    {
        int noSpawn = Random.Range(0, 4);
        if(noSpawn != 0)
        {
            int spawnKey = Random.Range(0, 3);
            if(spawnKey == 0)
            {
                spawned1 = Instantiate(pickup1, location1.position, Quaternion.identity);
                spawned1.transform.SetParent(location1);
            }
            if (spawnKey == 1)
            {
                spawned1 = Instantiate(pickup2, location1.position, Quaternion.identity);
                spawned1.transform.SetParent(location1);
            }
            if (spawnKey == 2)
            {
                spawned1 = Instantiate(pickup3, location1.position, Quaternion.identity);
                spawned1.transform.SetParent(location1);
            }
        }
        if (noSpawn != 1)
        {
            int spawnKey = Random.Range(0, 3);
            if (spawnKey == 0)
            {
                spawned2 = Instantiate(pickup1, location2.position, Quaternion.identity);
                spawned2.transform.SetParent(location2);
            }
            if (spawnKey == 1)
            {
                spawned2 = Instantiate(pickup2, location2.position, Quaternion.identity);
                spawned2.transform.SetParent(location2);
            }
            if (spawnKey == 2)
            {
                spawned2 = Instantiate(pickup3, location2.position, Quaternion.identity);
                spawned2.transform.SetParent(location2);
            }
        }
        if (noSpawn != 2)
        {
            int spawnKey = Random.Range(0, 3);
            if (spawnKey == 0)
            {
                spawned3 = Instantiate(pickup1, location3.position, Quaternion.identity);
                spawned3.transform.SetParent(location3);
            }
            if (spawnKey == 1)
            {
                spawned3 = Instantiate(pickup2, location3.position, Quaternion.identity);
                spawned3.transform.SetParent(location3);
            }
            if (spawnKey == 2)
            {
                spawned3 = Instantiate(pickup3, location3.position, Quaternion.identity);
                spawned3.transform.SetParent(location3);
            }
        }
        if (noSpawn != 3)
        {
            int spawnKey = Random.Range(0, 3);
            if (spawnKey == 0)
            {
                spawned4 = Instantiate(pickup1, location4.position, Quaternion.identity);
                spawned4.transform.SetParent(location4);
            }
            if (spawnKey == 1)
            {
                spawned4 = Instantiate(pickup2, location4.position, Quaternion.identity);
                spawned4.transform.SetParent(location4);
            }
            if (spawnKey == 2)
            {
                spawned4 = Instantiate(pickup3, location4.position, Quaternion.identity);
                spawned4.transform.SetParent(location4);
            }
        }
    }

    public void ClearPickups()
    {
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
    }
}
