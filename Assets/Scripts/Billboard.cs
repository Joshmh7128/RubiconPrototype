using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject target;
    public int playerKey;

    private void Start()
    {
        foreach ( GameObject x in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            if(x.name == "PlayerCam" + playerKey.ToString())
            {
                target = x;
                break;
            }
        }
        if(target == null)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position, -Vector3.up);
    }
}
