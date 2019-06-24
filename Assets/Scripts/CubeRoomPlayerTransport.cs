using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRoomPlayerTransport : MonoBehaviour
{
    
    void OnTriggerEnter(Collider player)
    {
        player.transform.SetParent(transform);
        //player.transform.gameObject.transform.rotation = transform.rotation;
        Debug.Log("Player captured");
    }   

    void OnTriggerExit(Collider player)
    {
        player.transform.parent = null;
        //player.transform.gameObject.transform.rotation = null;
        Debug.Log("Player released");
    }
}
