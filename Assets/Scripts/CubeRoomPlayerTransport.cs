using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRoomPlayerTransport : MonoBehaviour
{
    
    void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            player.transform.SetParent(transform);
        } 
    }   

    void OnTriggerExit(Collider player)
    {
        if(player.CompareTag("Player"))
        {
            //player.transform.parent = null;
        }
    }
}
