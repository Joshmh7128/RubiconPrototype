using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public Camera otherPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(otherPlayer.transform.position, -Vector3.up);
    }
}
