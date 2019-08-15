using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRoomPlayerTransport : MonoBehaviour
{
    
    void OnTriggerEnter(Collider player)
    {
        player.transform.SetParent(transform);
    }   

    void OnTriggerExit(Collider player)
    {
        player.transform.parent = null;
    }
}
