using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] locations = new GameObject[4];
    public GameObject[] pickups = new GameObject[3];
    public GameObject[] inPlay = new GameObject[3];

    public void SpawnPickups()
    {
        int noSpawn = Random.Range(0, 4);
        int numSpawned = 0;
        for(int i = 0; i < locations.Length; i++)
        {
            if(i != noSpawn)
            {
                int whichPickup = Random.Range(0, 3);
                GameObject spawned = GameObject.Instantiate(pickups[whichPickup], locations[i].transform.position, Quaternion.identity) as GameObject;
                inPlay[numSpawned] = spawned;
                numSpawned++;
            }
        }
    }

    public void ClearPickups()
    {
        for(int i = 0; i < inPlay.Length; i++)
        {
            Destroy(inPlay[i].gameObject);
        }
    }
}
