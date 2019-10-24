using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position, -Vector3.up);
    }
}
